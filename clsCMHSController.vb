﻿'Imports System.Runtime.InteropServices
Imports HidSharp
Imports HidSharp.Platform.Windows
Public Class clsCMHSController
    Inherits clsController
    Dim reportData(40) As Byte
    Dim device As HidDevice
    Dim devStream As DeviceStream
    Private apiLoaded As Boolean = False
    Private ready As Boolean = False
    Private dummy As Boolean = False

    Public Overrides Function isReady() As Boolean
        Return ready
    End Function

    Public Overrides ReadOnly Property IFType As String
        Get
            Return "CM"
        End Get
    End Property

    Public Sub New(ip As String)
        Dim idx As Integer
        If ip = "CM" Then idx = 1 Else idx = CInt(ip.Substring(2))
        If idx = 0 Then
            dummy = True
            ready = True
            reportData = baseReport()
            update()
            Exit Sub
        End If
        idx -= 1
        Dim devicePaths As New List(Of String)
        Dim deviceDict As New Dictionary(Of String, HidDevice)
        For Each hdev As HidDevice In DeviceList.Local.GetDevices(DeviceTypes.Hid)
            Dim vpString As String = String.Format("{0:X4}{1:X4}", hdev.VendorID, hdev.ProductID)
            Select Case vpString
                Case "20080001", "25080003", "25080001", "20080010", "25080032"
                    devicePaths.Add(hdev.DevicePath)
                    deviceDict.Add(hdev.DevicePath, hdev)
            End Select
        Next
        If idx >= devicePaths.Count Then
            MsgBox("Can't find CM with index " & idx + 1 & ".")
            Exit Sub
        End If

        devicePaths.Sort()
        device = deviceDict(devicePaths(idx))
        devStream = device.Open()
        devStream.WriteTimeout = 5000
        Try
            devStream.Write(New Byte() {0, 7, 0, 0, 1}, 0, 5)
            'System.Threading.Thread.Sleep(200)
        Catch e As TimeoutException
            devStream.Close()
            devStream.Dispose()
            devStream = Nothing
            device = Nothing
            MsgBox("Communications timeout")
            Exit Sub
        End Try
        ready = True

        reportData = baseReport()
        update()
        System.Threading.Thread.Sleep(25)
        update()
        System.Threading.Thread.Sleep(25)
    End Sub

    Public Overrides Sub sendReport(newReport() As Byte)
        If dummy Then Exit Sub
        'Debug.Print(System.BitConverter.ToString(newReport))
        Try
            devStream.Write(newReport, 0, newReport.Length)
        Catch ex As TimeoutException
            devStream.Close()
            devStream.Dispose()
            devStream = Nothing
            device = Nothing
            MsgBox("Communications timeout")
            Exit Sub
        End Try
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

        tmp(CMButtons.LX) = CMSInt(joyLXInt)
        tmp(CMButtons.LY) = CMSInt(joyLYInt)
        tmp(CMButtons.RX) = CMSInt(joyRXInt)
        tmp(CMButtons.RY) = CMSInt(joyRYInt)
        Return tmp
    End Function

    Private Function CMSByte(src As Byte) As Byte
        Dim tmp As Integer = Math.Ceiling(CDec(src - 128) * 100 / 127)
        If tmp < 0 Then tmp = tmp + 256
        Return CByte(tmp)
    End Function

    Private Function CMSInt(src As Int16) As Byte
        Dim tmp As Integer = Math.Ceiling(CDec(Math.Abs(src)) / 327.67)
        If src < 0 Then tmp = 256 - tmp
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

    Public Function exportReport(report() As Byte) As String
        Dim sbExport As New Text.StringBuilder
        Dim names() As String = New String() {"", "", "", "", "", "XB1_XBOX", "XB1_VIEW", "XB1_MENU", "XB1_RB", "XB1_RT", "XB1_RS", "XB1_LB", "XB1_LT", "XB1_LS", "XB1_RX", "XB1_RY", "XB1_LX", "XB1_LY", "XB1_UP", "XB1_DOWN", "XB1_LEFT", "XB1_RIGHT", "XB1_Y", "XB1_B", "XB1_A", "XB1_X"}
        For i As Integer = 5 To 25
            If report(i) > 0 Then
                Select Case i
                    Case 14 To 18
                        If report(i) > 127 Then
                            sbExport.Append("set_val(" & names(i) & "," & (report(i) - 256) & ");")
                        Else
                            sbExport.Append("set_val(" & names(i) & "," & report(i) & ");")
                        End If
                    Case Else
                        sbExport.Append("set_val(" & names(i) & "," & report(i) & ");")
                End Select
            End If
        Next
        Return sbExport.ToString()
    End Function

    Protected Overrides Sub update(Optional force As Boolean = False)
        Dim button As UInt16 = buttonHi * 256 Or buttonLo
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

        reportData(CMButtons.LX) = CMSInt(joyLXInt)
        reportData(CMButtons.LY) = CMSInt(joyLYInt)
        reportData(CMButtons.RX) = CMSInt(joyRXInt)
        reportData(CMButtons.RY) = CMSInt(joyRYInt)

        Dim x As Int16 = reportData(CMButtons.LX)
        Dim y As Int16 = reportData(CMButtons.LY)
        If x > 127 Then x = x - 256
        If y > 127 Then y = y - 256

        Debug.Print(String.Format("{0,4},{1,4}-{2,6},{3,6}", x, y, joyLXInt, joyLYInt))

        sendReport(reportData)
    End Sub

    Public Overrides Sub dispose()
        If Not devStream Is Nothing Then
            If devStream.CanWrite Then
                Try
                    devStream.Write(New Byte() {0, 8, 0, 0, 1}, 0, 5)
                Catch e As TimeoutException
                    devStream.Close()
                    devStream.Dispose()
                    devStream = Nothing
                    device = Nothing
                    MsgBox("Communications timeout")
                    Exit Sub
                End Try
                devStream.Close()
            End If
            devStream.Dispose()
            devStream = Nothing
        End If
    End Sub

    Protected Overrides Sub Finalize()
        Me.dispose()
        MyBase.Finalize()
    End Sub
End Class
