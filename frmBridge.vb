Imports System.Net
Public Class frmBridge

    Dim udp As Sockets.UdpClient
    Dim ep As IPEndPoint = New IPEndPoint(IPAddress.Any, 12345)
    Dim controller As clsController
    Dim snap As clsSnapshot

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        For i As Integer = 1 To 4
            Dim controller As String = GetSetting(Application.ProductName, "Settings", "Controller" & i, "")
            If controller <> vbNullString Then cmbController.Items.Add(controller)
        Next
        cmbController.SelectedIndex = 0

        txtWebcam.Text = GetSetting(Application.ProductName, "Settings", "Webcam Path", IO.Path.GetDirectoryName(Reflection.Assembly.GetExecutingAssembly().Location) & "\snapshot.jpg")
        chkWebcam.Checked = GetSetting(Application.ProductName, "Settings", "Webcam", vbNullString) <> vbNullString
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
                    If joyLy > 128 Then joyLy = 384 - joyLy Else joyLy = 128 - joyLy
                    If joyRy > 128 Then joyRy = 384 - joyRy Else joyRy = 128 - joyRy
                    controller.setState(data(6), data(7), data(8), data(9), joyLx, joyLy, joyRx, joyRy)
            End Select
        End If
        udp.BeginReceive(AddressOf handleData, Nothing)
    End Sub

    Private Sub btnStartBridge_Click(sender As System.Object, e As System.EventArgs) Handles btnStartBridge.Click
        Dim ip As String = cmbController.SelectedItem
        If System.Text.RegularExpressions.Regex.IsMatch(ip, "^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$") Then
            controller = New clsBBBController(ip)
        ElseIf System.Text.RegularExpressions.Regex.IsMatch(ip, "^P(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$") Then
            controller = New clsPS3Controller(ip)
        ElseIf System.Text.RegularExpressions.Regex.IsMatch(ip, "^COM[1-9][0-9]?$") Then
            controller = New clsPS2Controller(ip)
        ElseIf ip.StartsWith("CM") Then
            controller = New clsCMHIDController(ip)
        Else
            MsgBox("Invalid controller type.")
            Exit Sub
        End If

        If chkWebcam.Enabled AndAlso txtWebcam.Text <> vbNullString Then
            Dim curDevPath As String = GetSetting(Application.ProductName, "Settings", "CapDevPath", vbNullString)
            If curDevPath = vbNullString Then
                MsgBox("Capture Device not set.")
                Me.Close()
                Exit Sub
            End If
            Try
                snap = New clsSnapshot(curDevPath)
            Catch ex As Exception
                snap = Nothing
                MsgBox("Capture Device error:" & vbCrLf & ex.Message)
                Me.Close()
                Exit Sub
            End Try
            tmrSnap.Enabled = True
        End If

        btnStartBridge.Enabled = False
        cmbController.Enabled = False
        btnStopBridge.Enabled = True
        chkWebcam.Enabled = False
        btnWebcam.Enabled = False

        udp = New Net.Sockets.UdpClient(12345)
        udp.BeginReceive(AddressOf handleData, Nothing)
    End Sub

    Private Sub btnStopBridge_Click(sender As System.Object, e As System.EventArgs) Handles btnStopBridge.Click
        btnStartBridge.Enabled = True
        cmbController.Enabled = True
        btnStopBridge.Enabled = False
        chkWebcam.Enabled = True
        btnWebcam.Enabled = True
        tmrSnap.Enabled = False

        cleanup()
    End Sub


    Private Sub cleanup()
        If Not snap Is Nothing Then snap.Dispose()
        If Not controller Is Nothing Then controller.dispose()
        If Not udp Is Nothing Then
            udp.Close()
            udp = Nothing
        End If
    End Sub

    Private Sub chkWebcam_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkWebcam.CheckedChanged
        If chkWebcam.Checked AndAlso txtWebcam.Text = vbNullString Then btnWebcam_Click(Nothing, Nothing)
        SaveSetting(Application.ProductName, "Settings", "Webcam", IIf(chkWebcam.Checked, "Y", vbNullString))
    End Sub

    Private Sub btnWebcam_Click(sender As System.Object, e As System.EventArgs) Handles btnWebcam.Click
        fdSave.FileName = txtWebcam.Text
        fdSave.ShowDialog()
        If fdSave.FileName = vbNullString Then
            chkWebcam.Checked = False
            Exit Sub
        End If
        txtWebcam.Text = fdSave.FileName
        SaveSetting(Application.ProductName, "Settings", "Webcam Path", txtWebcam.Text)
    End Sub

    Protected Overrides Sub Finalize()
        cleanup()
        MyBase.Finalize()
    End Sub

    Private Sub tmrSnap_Tick(sender As System.Object, e As System.EventArgs) Handles tmrSnap.Tick
        Dim bmp As Bitmap = snap.capture()
        Dim bSml As New Bitmap(640, 360)
        Dim g As Graphics = Graphics.FromImage(bSml)
        g.DrawImage(bmp, 0, 0, bSml.Width, bSml.Height)
        g.Dispose()
        g = Nothing
        bmp.Dispose()
        bmp = Nothing
        bSml.Save(txtWebcam.Text, Drawing.Imaging.ImageFormat.Jpeg)
        bSml.Dispose()
        bSml = Nothing
    End Sub
End Class