'Imports System.Runtime.InteropServices
'Public Class clsCMController
'    Inherits clsController
'    Dim reportData(35) As Byte
'    Private apiLoaded As Boolean = False

'    <DllImport("gcdapi.dll", CallingConvention:=Runtime.InteropServices.CallingConvention.StdCall)> _
'    Private Shared Function gcdapi_Load() As Byte
'    End Function

'    <DllImport("gcdapi.dll", CallingConvention:=Runtime.InteropServices.CallingConvention.StdCall)> _
'    Private Shared Sub gcdapi_Unload()
'    End Sub

'    <DllImport("gcdapi.dll", CallingConvention:=Runtime.InteropServices.CallingConvention.StdCall)> _
'    Private Shared Function gcapi_IsConnected() As Byte
'    End Function

'    <DllImport("gcdapi.dll", CallingConvention:=Runtime.InteropServices.CallingConvention.StdCall)> _
'    Private Shared Function gcapi_Write(ByVal output() As Byte) As Byte
'    End Function

'    Public Overrides ReadOnly Property IFType As String
'        Get
'            Return "CM"
'        End Get
'    End Property

'    Public Sub New()
'        If gcdapi_Load() = 0 Then

'            Stop
'            Exit Sub
'        End If
'        System.Threading.Thread.Sleep(250)
'        If gcapi_IsConnected() = 0 Then
'            gcdapi_Unload()
'            apiLoaded = False
'            Stop
'            Exit Sub
'        End If
'        apiLoaded = True
'        reportData = baseReport()
'        update()
'    End Sub

'    Public Overrides Sub sendReport(newReport() As Byte)
'        gcapi_Write(newReport)
'    End Sub

'    Public Overrides Function baseReport() As Byte()
'        Return New Byte() {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
'    End Function

'    Public Overrides Function getReport() As Byte()
'        Dim button As UInt16 = buttonHi * 256 + buttonLo
'        Dim tmp(35) As Byte
'        tmp(CMButtons.XBOX) = IIf(button And XBButtons.btnGuide, 100, 0)
'        tmp(CMButtons.BACK) = IIf(button And XBButtons.btnBack, 100, 0)
'        tmp(CMButtons.START) = IIf(button And XBButtons.btnStart, 100, 0)
'        tmp(CMButtons.RB) = IIf(button And XBButtons.btnRB, 100, 0)
'        tmp(CMButtons.RS) = IIf(button And XBButtons.btnR3, 100, 0)
'        tmp(CMButtons.LB) = IIf(button And XBButtons.btnLB, 100, 0)
'        tmp(CMButtons.LS) = IIf(button And XBButtons.btnL3, 100, 0)
'        tmp(CMButtons.UP) = IIf(button And XBButtons.btnUp, 100, 0)
'        tmp(CMButtons.DOWN) = IIf(button And XBButtons.btnDown, 100, 0)
'        tmp(CMButtons.LEFT) = IIf(button And XBButtons.btnLeft, 100, 0)
'        tmp(CMButtons.RIGHT) = IIf(button And XBButtons.btnRight, 100, 0)
'        tmp(CMButtons.Y) = IIf(button And XBButtons.btnY, 100, 0)
'        tmp(CMButtons.B) = IIf(button And XBButtons.btnB, 100, 0)
'        tmp(CMButtons.A) = IIf(button And XBButtons.btnA, 100, 0)
'        tmp(CMButtons.X) = IIf(button And XBButtons.btnX, 100, 0)
'        tmp(CMButtons.LT) = Math.Ceiling(CDec(LT) * 100 / 255)
'        tmp(CMButtons.RT) = Math.Ceiling(CDec(RT) * 100 / 255)
'        tmp(CMButtons.LX) = CMSByte(joyLx)
'        tmp(CMButtons.LY) = CMSByte(joyLy)
'        tmp(CMButtons.RX) = CMSByte(joyRx)
'        tmp(CMButtons.RY) = CMSByte(joyRY)
'        Return tmp
'    End Function

'    Private Function CMSByte(src As Byte) As Byte
'        Dim tmp As Integer = Math.Ceiling(CDec(src - 128) * 100 / 127)
'        If tmp < 0 Then tmp = tmp + 256
'        Return CByte(tmp)
'    End Function

'    Enum CMButtons
'        XBOX = 0
'        BACK = 1
'        START = 2
'        RB = 3
'        RT = 4
'        RS = 5
'        LB = 6
'        LT = 7
'        LS = 8
'        RX = 9
'        RY = 10
'        LX = 11
'        LY = 12
'        UP = 13
'        DOWN = 14
'        LEFT = 15
'        RIGHT = 16
'        Y = 17
'        B = 18
'        A = 19
'        X = 20
'    End Enum

'    Protected Overrides Sub update(Optional force As Boolean = False)
'        Dim button As UInt16 = buttonHi * 256 And buttonLo
'        reportData(CMButtons.XBOX) = IIf(button And XBButtons.btnGuide, 100, 0)
'        reportData(CMButtons.BACK) = IIf(button And XBButtons.btnBack, 100, 0)
'        reportData(CMButtons.START) = IIf(button And XBButtons.btnStart, 100, 0)
'        reportData(CMButtons.RB) = IIf(button And XBButtons.btnRB, 100, 0)
'        reportData(CMButtons.RS) = IIf(button And XBButtons.btnR3, 100, 0)
'        reportData(CMButtons.LB) = IIf(button And XBButtons.btnLB, 100, 0)
'        reportData(CMButtons.LS) = IIf(button And XBButtons.btnL3, 100, 0)
'        reportData(CMButtons.UP) = IIf(button And XBButtons.btnUp, 100, 0)
'        reportData(CMButtons.DOWN) = IIf(button And XBButtons.btnDown, 100, 0)
'        reportData(CMButtons.LEFT) = IIf(button And XBButtons.btnLeft, 100, 0)
'        reportData(CMButtons.RIGHT) = IIf(button And XBButtons.btnRight, 100, 0)
'        reportData(CMButtons.Y) = IIf(button And XBButtons.btnY, 100, 0)
'        reportData(CMButtons.B) = IIf(button And XBButtons.btnB, 100, 0)
'        reportData(CMButtons.A) = IIf(button And XBButtons.btnA, 100, 0)
'        reportData(CMButtons.X) = IIf(button And XBButtons.btnX, 100, 0)
'        reportData(CMButtons.LT) = Math.Ceiling(CDec(LT) * 100 / 255)
'        reportData(CMButtons.RT) = Math.Ceiling(CDec(RT) * 100 / 255)
'        reportData(CMButtons.LX) = CMSByte(joyLx)
'        reportData(CMButtons.LY) = CMSByte(joyLy)
'        reportData(CMButtons.RX) = CMSByte(joyRx)
'        reportData(CMButtons.RY) = CMSByte(joyRY)
'        sendReport(reportData)
'    End Sub

'    Public Overrides Sub dispose()
'        If apiLoaded Then
'            gcdapi_Unload()
'            apiLoaded = False
'        End If
'    End Sub

'    Protected Overrides Sub Finalize()
'        If apiLoaded Then
'            gcdapi_Unload()
'            apiLoaded = False
'        End If
'        MyBase.Finalize()
'    End Sub
'End Class
