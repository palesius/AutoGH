Public Class clsBBBController
    Inherits clsController
    Dim reportData(23) As Byte

    Private _IP As String

    Public Property IP As String
        Get
            Return _IP
        End Get
        Set(value As String)
            If value = _IP Then Exit Property
            stopTCP()
            _IP = value
            setupTCP()
        End Set
    End Property

    Public Overrides Function isReady() As Boolean
        Return True
    End Function

    Public Overrides ReadOnly Property IFType As String
        Get
            Return "BBB"
        End Get
    End Property

    Private sck As Net.Sockets.Socket

    Private Function setupTCP()
        sck = New Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, Net.Sockets.SocketType.Dgram, Net.Sockets.ProtocolType.Udp)
        sck.Connect(_IP, 12345)
        Return True
    End Function

    Private Sub stopTCP()
        If sck Is Nothing Then Exit Sub
        'sck.Disconnect(False)
        sck.Dispose()
        sck = Nothing
    End Sub

    Public Sub New(host As String)
        _IP = host
        If Not setupTCP() Then Throw New Exception("TCP Setup Failed")
        reportData(0) = 129
        reportData(3) = 20
        reportData(4) = 0
        reportData(5) = 20
        For i As Integer = 6 To 23
            reportData(i) = 0
        Next
    End Sub

    Public Overrides Sub sendReport(newReport() As Byte)
        Debug.Print("Sent (" & _IP & "): " & BitConverter.ToString(newReport))
        sck.Send(newReport)
    End Sub

    Public Overrides Function baseReport() As Byte()
        Return New Byte() {129, 0, 0, 20, 0, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
    End Function

    Public Overrides Function getReport() As Byte()
        Dim tmp(reportData.Length - 1) As Byte
        Array.Copy(reportData, tmp, reportData.Length)
        tmp(6) = buttonHi
        tmp(7) = buttonLo
        tmp(8) = LT
        tmp(9) = RT

        Dim uVal As Integer
        If joyLXInt >= 0 Then uVal = joyLXInt Else uVal = 65536 + joyLXInt
        tmp(10) = uVal Mod 256
        tmp(11) = uVal >> 8

        If joyLYInt < 0 Then uVal = 1 - joyLYInt Else uVal = 65535 - joyLYInt
        tmp(12) = uVal Mod 256
        tmp(13) = uVal >> 8

        If joyRXInt >= 0 Then uVal = joyRXInt Else uVal = 65536 + joyRXInt
        tmp(14) = uVal Mod 256
        tmp(15) = uVal >> 8

        If joyRYInt < 0 Then uVal = 1 - joyRYInt Else uVal = 65535 - joyRYInt
        tmp(16) = uVal Mod 256
        tmp(17) = uVal >> 8

        Return tmp
    End Function

    Protected Overrides Sub update(Optional force As Boolean = False)
        reportData(6) = buttonHi
        reportData(7) = buttonLo
        reportData(8) = LT
        reportData(9) = RT

        Dim uVal As Integer
        If joyLXInt >= 0 Then uVal = joyLXInt Else uVal = 65536 + joyLXInt
        reportData(10) = uVal Mod 256
        reportData(11) = uVal >> 8

        If joyLYInt < 0 Then uVal = -1 - joyLYInt Else uVal = 65535 - joyLYInt
        reportData(12) = uVal Mod 256
        reportData(13) = uVal >> 8

        If joyRXInt >= 0 Then uVal = joyRXInt Else uVal = 65536 + joyRXInt
        reportData(14) = uVal Mod 256
        reportData(15) = uVal >> 8

        If joyRYInt < 0 Then uVal = -1 - joyRYInt Else uVal = 65535 - joyRYInt
        reportData(16) = uVal Mod 256
        reportData(17) = uVal >> 8
        sendReport(reportData)
    End Sub

    Public Overrides Sub dispose()
        stopTCP()
    End Sub

    Protected Overrides Sub Finalize()
        stopTCP()
        MyBase.Finalize()
    End Sub
End Class
