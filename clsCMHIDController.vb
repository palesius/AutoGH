'Imports System.Runtime.InteropServices
Imports HidLibrary
Public Class clsCMHIDController
    Inherits clsController
    Dim reportData(40) As Byte
    Dim dev As HidDevice
    Private apiLoaded As Boolean = False

    Public Overrides ReadOnly Property IFType As String
        Get
            Return "CM"
        End Get
    End Property

    Public Sub New(ip As String)
        Dim idx As Integer
        If ip = "CM" Then idx = 1 Else idx = CInt(ip.Substring(2))
        idx -= 1
        Dim devicePaths As New List(Of String)
        For Each hdev As HidDevice In HidDevices.Enumerate()
            Dim vpString As String = String.Format("{0:X4}{1:X4}", hdev.Attributes.VendorId, hdev.Attributes.ProductId)
            Select Case vpString
                Case "20080001", "25080003", "25080001", "20080010"
                    devicePaths.Add(hdev.DevicePath)
            End Select
        Next
        If idx >= devicePaths.Count Then
            MsgBox("Can't find CM with index " & idx + 1 & ".")
            Exit Sub
        End If
        devicePaths.Sort()
        dev = HidDevices.GetDevice(devicePaths(idx))
        dev.OpenDevice()
        If Not dev.Write(New Byte() {0, 7, 0, 0, 1}, 5000) Then
            MsgBox("Communications timeout")
            Exit Sub
        End If

        reportData = baseReport()
        update()
    End Sub

    Public Overrides Sub sendReport(newReport() As Byte)
        Debug.Print(System.BitConverter.ToString(newReport))
        If Not dev.Write(newReport, 5000) Then
            MsgBox("Communications timeout")
            Exit Sub
        End If
    End Sub

    Public Overrides Function baseReport() As Byte()
        Return New Byte() {0, 4, 36, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
    End Function

    Public Overrides Function getReport() As Byte()
        Dim button As UInt16 = buttonHi * 256 + buttonLo
        Dim tmp(40) As Byte
        tmp(1) = 4
        tmp(2) = 36
        tmp(4) = 1
        tmp(CMButtons.XBOX) = IIf(button And XBButtons.btnGuide, 100, 0)
        tmp(CMButtons.BACK) = IIf(button And XBButtons.btnBack, 100, 0)
        tmp(CMButtons.START) = IIf(button And XBButtons.btnStart, 100, 0)
        tmp(CMButtons.RB) = IIf(button And XBButtons.btnRB, 100, 0)
        tmp(CMButtons.RS) = IIf(button And XBButtons.btnR3, 100, 0)
        tmp(CMButtons.LB) = IIf(button And XBButtons.btnLB, 100, 0)
        tmp(CMButtons.LS) = IIf(button And XBButtons.btnL3, 100, 0)
        tmp(CMButtons.UP) = IIf(button And XBButtons.btnUp, 100, 0)
        tmp(CMButtons.DOWN) = IIf(button And XBButtons.btnDown, 100, 0)
        tmp(CMButtons.LEFT) = IIf(button And XBButtons.btnLeft, 100, 0)
        tmp(CMButtons.RIGHT) = IIf(button And XBButtons.btnRight, 100, 0)
        tmp(CMButtons.Y) = IIf(button And XBButtons.btnY, 100, 0)
        tmp(CMButtons.B) = IIf(button And XBButtons.btnB, 100, 0)
        tmp(CMButtons.A) = IIf(button And XBButtons.btnA, 100, 0)
        tmp(CMButtons.X) = IIf(button And XBButtons.btnX, 100, 0)
        tmp(CMButtons.LT) = Math.Ceiling(CDec(LT) * 100 / 255)
        tmp(CMButtons.RT) = Math.Ceiling(CDec(RT) * 100 / 255)
        tmp(CMButtons.LX) = CMSByte(joyLx)
        tmp(CMButtons.LY) = CMSByte(joyLy)
        tmp(CMButtons.RX) = CMSByte(joyRx)
        tmp(CMButtons.RY) = CMSByte(joyRY)
        Return tmp
    End Function

    Private Function CMSByte(src As Byte) As Byte
        Dim tmp As Integer = Math.Ceiling(CDec(src - 128) * 100 / 127)
        If tmp < 0 Then tmp = tmp + 256
        Return CByte(tmp)
    End Function

    Enum CMButtons
        XBOX = 5
        BACK = 6
        START = 7
        RB = 8
        RT = 9
        RS = 10
        LB = 11
        LT = 12
        LS = 13
        RX = 14
        RY = 15
        LX = 16
        LY = 17
        UP = 18
        DOWN = 19
        LEFT = 20
        RIGHT = 21
        Y = 22
        B = 23
        A = 24
        X = 25
    End Enum

    Protected Overrides Sub update(Optional force As Boolean = False)
        Dim button As UInt16 = buttonHi * 256 And buttonLo
        reportData(CMButtons.XBOX) = IIf(button And XBButtons.btnGuide, 100, 0)
        reportData(CMButtons.BACK) = IIf(button And XBButtons.btnBack, 100, 0)
        reportData(CMButtons.START) = IIf(button And XBButtons.btnStart, 100, 0)
        reportData(CMButtons.RB) = IIf(button And XBButtons.btnRB, 100, 0)
        reportData(CMButtons.RS) = IIf(button And XBButtons.btnR3, 100, 0)
        reportData(CMButtons.LB) = IIf(button And XBButtons.btnLB, 100, 0)
        reportData(CMButtons.LS) = IIf(button And XBButtons.btnL3, 100, 0)
        reportData(CMButtons.UP) = IIf(button And XBButtons.btnUp, 100, 0)
        reportData(CMButtons.DOWN) = IIf(button And XBButtons.btnDown, 100, 0)
        reportData(CMButtons.LEFT) = IIf(button And XBButtons.btnLeft, 100, 0)
        reportData(CMButtons.RIGHT) = IIf(button And XBButtons.btnRight, 100, 0)
        reportData(CMButtons.Y) = IIf(button And XBButtons.btnY, 100, 0)
        reportData(CMButtons.B) = IIf(button And XBButtons.btnB, 100, 0)
        reportData(CMButtons.A) = IIf(button And XBButtons.btnA, 100, 0)
        reportData(CMButtons.X) = IIf(button And XBButtons.btnX, 100, 0)
        reportData(CMButtons.LT) = Math.Ceiling(CDec(LT) * 100 / 255)
        reportData(CMButtons.RT) = Math.Ceiling(CDec(RT) * 100 / 255)
        reportData(CMButtons.LX) = CMSByte(joyLx)
        reportData(CMButtons.LY) = CMSByte(joyLy)
        reportData(CMButtons.RX) = CMSByte(joyRx)
        reportData(CMButtons.RY) = CMSByte(joyRY)
        sendReport(reportData)
    End Sub

    Public Overrides Sub dispose()
        If Not dev Is Nothing Then
            If dev.IsOpen Then
                If Not dev.Write(New Byte() {0, 8, 0, 0, 1}, 5000) Then
                    MsgBox("Communications timeout")
                    Exit Sub
                End If
                dev.CloseDevice()
            End If
            dev.Dispose()
            dev = Nothing
        End If
    End Sub

    Protected Overrides Sub Finalize()
        If Not dev Is Nothing Then
            If dev.IsOpen Then
                If Not dev.Write(New Byte() {0, 8, 0, 0, 1}, 5000) Then
                    MsgBox("Communications timeout")
                    Exit Sub
                End If
                dev.CloseDevice()
            End If
            dev.Dispose()
            dev = Nothing
        End If
        MyBase.Finalize()
    End Sub
End Class
