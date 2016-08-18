Imports System.Net
Public Class frmBridge

    Dim udp As Sockets.UdpClient
    Dim ep As IPEndPoint = New IPEndPoint(IPAddress.Any, 12345)
    Dim controller As clsController

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        For i As Integer = 1 To 4
            Dim controller As String = GetSetting(Application.ProductName, "Settings", "Controller" & i, "")
            If controller <> vbNullString Then cmbController.Items.Add(controller)
        Next
        cmbController.SelectedIndex = 0
    End Sub

    Private Sub btnExit_Click(sender As System.Object, e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub handleData(res As IAsyncResult)
        If udp Is Nothing Then Exit Sub
        Dim data As Byte() = udp.EndReceive(res, ep)
        If data.Length = 24 Then
            Select Case controller.IFType
                Case "BBB"
                    controller.sendReport(data)
                Case "PS2", "CM"
                    Dim joyLx As Byte = data(11)
                    Dim joyLy As Byte = data(13)
                    Dim joyRx As Byte = data(15)
                    Dim joyRy As Byte = data(17)
                    If joyLx >= 128 Then joyLx = joyLx - 128 Else joyLx = joyLx + 128
                    If joyRx >= 128 Then joyRx = joyRx - 128 Else joyRx = joyRx + 128
                    If joyLy >= 128 Then joyLy = 384 - joyLy Else joyLy = 128 - joyLy
                    If joyRy >= 128 Then joyRy = 384 - joyRy Else joyRy = 128 - joyRy
                    controller.setState(data(6), data(7), data(8), data(9), joyLx, joyLy, joyRx, joyRy)
            End Select
        End If
        udp.BeginReceive(AddressOf handleData, Nothing)
    End Sub

    Private Sub btnStartBridge_Click(sender As System.Object, e As System.EventArgs) Handles btnStartBridge.Click
        Dim ip As String = cmbController.SelectedItem
        If System.Text.RegularExpressions.Regex.IsMatch(ip, "^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$") Then
            controller = New clsBBBController(ip)
        ElseIf System.Text.RegularExpressions.Regex.IsMatch(ip, "^COM[1-9][0-9]?$") Then
            controller = New clsPS2Controller(ip)
        ElseIf ip.StartsWith("CM") Then
            controller = New clsCMHIDController(ip)
        Else
            MsgBox("Invalid controller type.")
            Exit Sub
        End If

        btnStartBridge.Enabled = False
        cmbController.Enabled = False
        btnStopBridge.Enabled = True

        udp = New Net.Sockets.UdpClient(12345)
        udp.BeginReceive(AddressOf handleData, Nothing)
    End Sub

    Private Sub btnStopBridge_Click(sender As System.Object, e As System.EventArgs) Handles btnStopBridge.Click
        btnStartBridge.Enabled = True
        cmbController.Enabled = True
        btnStopBridge.Enabled = False
        If Not controller Is Nothing Then controller.dispose()
        udp.Close()
        udp = Nothing
    End Sub
End Class