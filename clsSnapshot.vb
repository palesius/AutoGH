Imports DirectShowLib
Imports System.Threading
Imports System.Runtime.InteropServices
Public Class clsSnapshot
    Implements ISampleGrabberCB, IDisposable

    Private m_FilterGraph As IFilterGraph2 = Nothing
    Private m_VidControl As IAMVideoControl = Nothing
    Private m_pinStill As IPin = Nothing
    Private m_PictureReady As ManualResetEvent = Nothing
    Private m_WantOne As Boolean = False
    Private m_videoWidth As Integer
    Private m_videoHeight As Integer
    Private m_Stride As Integer
    Private m_ipBuffer As IntPtr = IntPtr.Zero

    Private Declare Sub CopyMemory Lib "kernel32" Alias "RtlMoveMemory" (ByVal Destination As IntPtr, ByVal Source As IntPtr, <MarshalAs(UnmanagedType.U4)> ByVal Length As Integer)

    Public Sub New(sDevPath As String)
        Dim dev As DsDevice = Nothing
        For Each cDev As DsDevice In DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice)
            If cDev.DevicePath = sDevPath Then
                dev = cDev
                Exit For
            End If
        Next
        If dev Is Nothing Then Throw New Exception("Capture device not found!")
        Try
            SetupGraph(dev)
            m_PictureReady = New ManualResetEvent(False)
        Catch ex As Exception
            Dispose()
            Throw ex
        End Try
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        CloseInterfaces()
        If Not m_PictureReady Is Nothing Then
            m_PictureReady.Close()
        End If
        GC.SuppressFinalize(Me)
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
        Dispose()
    End Sub

    Public Function capture() As Bitmap
        If m_ipBuffer <> IntPtr.Zero Then
            Marshal.FreeCoTaskMem(m_ipBuffer)
            m_ipBuffer = IntPtr.Zero
        End If
        Dim hr As Integer
        m_PictureReady.Reset()
        m_ipBuffer = Marshal.AllocCoTaskMem(Math.Abs(m_Stride) * m_videoHeight)
        Try
            m_WantOne = True
            If Not m_VidControl Is Nothing Then
                hr = m_VidControl.SetMode(m_pinStill, VideoControlFlags.Trigger)
                DsError.ThrowExceptionForHR(hr)
            End If
            If (Not m_PictureReady.WaitOne(9000, False)) Then
                Throw New Exception("Timeout waiting to get picture")
            End If
        Catch e As Exception
            Marshal.FreeCoTaskMem(m_ipBuffer)
            m_ipBuffer = IntPtr.Zero
            Throw e
        End Try
        Dim b As New Bitmap(m_videoWidth, m_videoHeight, m_Stride, System.Drawing.Imaging.PixelFormat.Format24bppRgb, m_ipBuffer)
        b.RotateFlip(RotateFlipType.RotateNoneFlipY)
        Return b
    End Function

    Public ReadOnly Property width As Integer
        Get
            Return m_videoWidth
        End Get
    End Property

    Public ReadOnly Property height As Integer
        Get
            Return m_videoHeight
        End Get
    End Property

    Private Sub SetupGraph(dev As DsDevice)
        'TODO: translate setupGraph
        Dim hr As Integer
        Dim sampGrabber As ISampleGrabber = Nothing
        Dim capFilter As IBaseFilter = Nothing
        Dim pCaptureOut As IPin = Nothing
        Dim pSampleIn As IPin = Nothing
        Dim pRenderIn As IPin = Nothing

        m_FilterGraph = CType(New FilterGraph(), IFilterGraph2)

        Try
            hr = m_FilterGraph.AddSourceFilterForMoniker(dev.Mon, Nothing, dev.Name, capFilter)
            DsError.ThrowExceptionForHR(hr)
            m_pinStill = DsFindPin.ByCategory(capFilter, PinCategory.Still, 0)
            If m_pinStill Is Nothing Then m_pinStill = DsFindPin.ByCategory(capFilter, PinCategory.Preview, 0)

            If m_pinStill Is Nothing Then
                Dim pRaw As IPin = Nothing
                Dim psmart As IPin = Nothing
                m_VidControl = Nothing
                Dim iSmartTee As IBaseFilter = CType(New SmartTee, IBaseFilter)
                Try
                    hr = m_FilterGraph.AddFilter(iSmartTee, "SmartTee")
                    DsError.ThrowExceptionForHR(hr)
                    pRaw = DsFindPin.ByCategory(capFilter, PinCategory.Capture, 0)
                    psmart = DsFindPin.ByDirection(iSmartTee, PinDirection.Input, 0)

                    hr = m_FilterGraph.Connect(pRaw, psmart)
                    DsError.ThrowExceptionForHR(hr)

                    m_pinStill = DsFindPin.ByName(iSmartTee, "Preview")
                    pCaptureOut = DsFindPin.ByName(iSmartTee, "Capture")

                    autoPinConfig(pRaw)
                Finally
                    If Not pRaw Is Nothing Then Marshal.ReleaseComObject(pRaw)
                    If Not pRaw Is psmart Then Marshal.ReleaseComObject(psmart)
                    If Not pRaw Is iSmartTee Then Marshal.ReleaseComObject(iSmartTee)
                    pRaw = Nothing
                End Try
            Else
                m_VidControl = CType(capFilter, IAMVideoControl)
                pCaptureOut = DsFindPin.ByCategory(capFilter, PinCategory.Capture, 0)
            End If

            sampGrabber = CType(New SampleGrabber(), ISampleGrabber)
            Dim baseGrabFlt As IBaseFilter = CType(sampGrabber, IBaseFilter)
            ConfigureSampleGrabber(sampGrabber)
            pSampleIn = DsFindPin.ByDirection(baseGrabFlt, PinDirection.Input, 0)

            Dim pRenderer As IBaseFilter = CType(New NullRenderer, IBaseFilter)
            hr = m_FilterGraph.AddFilter(pRenderer, "Renderer")
            DsError.ThrowExceptionForHR(hr)

            pRenderIn = DsFindPin.ByDirection(pRenderer, PinDirection.Input, 0)

            hr = m_FilterGraph.AddFilter(baseGrabFlt, "Ds.NET Grabber")
            DsError.ThrowExceptionForHR(hr)

            If m_VidControl Is Nothing Then
                hr = m_FilterGraph.Connect(m_pinStill, pSampleIn)
                DsError.ThrowExceptionForHR(hr)

                hr = m_FilterGraph.Connect(pCaptureOut, pRenderIn)
                DsError.ThrowExceptionForHR(hr)
            Else
                hr = m_FilterGraph.Connect(pCaptureOut, pRenderIn)
                DsError.ThrowExceptionForHR(hr)

                hr = m_FilterGraph.Connect(m_pinStill, pSampleIn)
                DsError.ThrowExceptionForHR(hr)
            End If
            SaveSizeInfo(sampGrabber)
            'ConfigVideoWindow(hControl)
            Dim mediaCtrl As IMediaControl = CType(m_FilterGraph, IMediaControl)
            hr = mediaCtrl.Run()
            DsError.ThrowExceptionForHR(hr)
        Finally
            If Not sampGrabber Is Nothing Then
                Marshal.ReleaseComObject(sampGrabber)
                sampGrabber = Nothing
            End If
            If Not pCaptureOut Is Nothing Then
                Marshal.ReleaseComObject(pCaptureOut)
                pCaptureOut = Nothing
            End If
            If Not pRenderIn Is Nothing Then
                Marshal.ReleaseComObject(pRenderIn)
                pRenderIn = Nothing
            End If
            If Not pSampleIn Is Nothing Then
                Marshal.ReleaseComObject(pSampleIn)
                pSampleIn = Nothing
            End If
        End Try
    End Sub

    Private Sub autoPinConfig(pin As IPin)
        Dim hr As Integer
        Dim media As New AMMediaType
        hr = pin.ConnectionMediaType(media)
        DsError.ThrowExceptionForHR(hr)
        If media.formatType <> FormatType.VideoInfo Or media.formatPtr = IntPtr.Zero Then
            Throw New NotSupportedException("Unknown Grabber Media Format")
        End If
        Dim vih As VideoInfoHeader = CType(Marshal.PtrToStructure(media.formatPtr, GetType(VideoInfoHeader)), VideoInfoHeader)
        setConfigParms(pin, vih.BmiHeader.Width, vih.BmiHeader.Height, vih.BmiHeader.BitCount)
        DsUtils.FreeAMMediaType(media)
        media = Nothing
    End Sub

    Private Sub SaveSizeInfo(sampGrabber As ISampleGrabber)
        Dim hr As Integer
        Dim media As New AMMediaType
        hr = sampGrabber.GetConnectedMediaType(media)
        DsError.ThrowExceptionForHR(hr)
        If media.formatType <> FormatType.VideoInfo Or media.formatPtr = IntPtr.Zero Then
            Throw New NotSupportedException("Unknown Grabber Media Format")
        End If
        Dim vih As VideoInfoHeader = CType(Marshal.PtrToStructure(media.formatPtr, GetType(VideoInfoHeader)), VideoInfoHeader)
        m_videoWidth = vih.BmiHeader.Width
        m_videoHeight = vih.BmiHeader.Height
        m_Stride = m_videoWidth * (vih.BmiHeader.BitCount / 8)
        DsUtils.FreeAMMediaType(media)
        media = Nothing
    End Sub

    Private Sub ConfigureSampleGrabber(sampGrabber As ISampleGrabber)
        Dim hr As Integer
        Dim media As New AMMediaType
        media.majorType = MediaType.Video
        media.subType = MediaSubType.RGB24
        media.formatType = FormatType.VideoInfo
        hr = sampGrabber.SetMediaType(media)
        DsError.ThrowExceptionForHR(hr)
        DsUtils.FreeAMMediaType(media)
        media = Nothing
        hr = sampGrabber.SetCallback(Me, 1)
        DsError.ThrowExceptionForHR(hr)
    End Sub

    Private Sub setConfigParms(pStill As IPin, iWidth As Integer, iHeight As Integer, iBPP As Short)
        Dim hr As Integer
        Dim media As AMMediaType = Nothing
        Dim v As VideoInfoHeader = Nothing

        Dim videoStreamConfig As IAMStreamConfig = CType(pStill, IAMStreamConfig)
        hr = videoStreamConfig.GetFormat(media)
        DsError.ThrowExceptionForHR(hr)
        Try
            v = New VideoInfoHeader
            Marshal.PtrToStructure(media.formatPtr, v)
            If iWidth > 0 Then v.BmiHeader.Width = iWidth
            If iHeight > 0 Then v.BmiHeader.Height = iHeight
            If iBPP > 0 Then v.BmiHeader.BitCount = iBPP
            Marshal.StructureToPtr(v, media.formatPtr, False)
            hr = videoStreamConfig.SetFormat(media)
            DsError.ThrowExceptionForHR(hr)
        Catch ex As Exception
        Finally
            DsUtils.FreeAMMediaType(media)
            media = Nothing
        End Try
    End Sub

    Private Sub CloseInterfaces()
        Dim hr As Integer
        Try
            If Not m_FilterGraph Is Nothing Then
                hr = CType(m_FilterGraph, IMediaControl).Stop
                DsError.ThrowExceptionForHR(hr)
            End If
        Catch ex As Exception
            Debug.WriteLine(ex)
        End Try
        If Not m_FilterGraph Is Nothing Then
            Marshal.ReleaseComObject(m_FilterGraph)
            m_FilterGraph = Nothing
        End If
        If Not m_VidControl Is Nothing Then
            Marshal.ReleaseComObject(m_VidControl)
            m_VidControl = Nothing
        End If
        If Not m_pinStill Is Nothing Then
            Marshal.ReleaseComObject(m_pinStill)
            m_pinStill = Nothing
        End If
        If m_ipBuffer <> IntPtr.Zero Then
            Marshal.FreeCoTaskMem(m_ipBuffer)
            m_ipBuffer = IntPtr.Zero
        End If
    End Sub

    Public Function BufferCB(SampleTime As Double, pBuffer As System.IntPtr, BufferLen As Integer) As Integer Implements DirectShowLib.ISampleGrabberCB.BufferCB
        If m_WantOne Then
            Debug.Assert(BufferLen = Math.Abs(m_Stride) * m_videoHeight, "Incorrect buffer length [" & BufferLen & "] should be " & m_Stride & "*" & m_videoHeight)
            m_WantOne = False
            Debug.Assert(m_ipBuffer <> IntPtr.Zero, "Uninitialized buffer")
            CopyMemory(m_ipBuffer, pBuffer, BufferLen)
            m_PictureReady.Set()
        End If
        Return 0
    End Function

    Public Function SampleCB(SampleTime As Double, pSample As DirectShowLib.IMediaSample) As Integer Implements DirectShowLib.ISampleGrabberCB.SampleCB
        Marshal.ReleaseComObject(pSample)
        Return 0
    End Function
End Class
