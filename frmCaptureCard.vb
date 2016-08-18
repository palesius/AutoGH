Imports DirectShowLib
Public Class frmCaptureCard

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Dim capDevices() As DsDevice = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice)
        Dim curDevPath As String = GetSetting(Application.ProductName, "Settings", "CapDevPath", vbNullString)

        If capDevices.Length = 0 Then
            MsgBox("No capture devices found")
            Exit Sub
        End If
        cbDevice.Items.Clear()
        cbDevice.DisplayMember = "Name"
        For Each dev In capDevices
            cbDevice.Items.Add(dev)
            If dev.DevicePath = curDevPath Then cbDevice.SelectedItem = dev
        Next
    End Sub

    Private Sub btnTest_Click(sender As System.Object, e As System.EventArgs) Handles btnTest.Click
        Dim cap As New clsSnapshot(CType(cbDevice.SelectedItem, DsDevice).DevicePath)
        pbTest.Image = cap.capture
        pbTest.Refresh()
        cap.Dispose()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnOK_Click(sender As System.Object, e As System.EventArgs) Handles btnOK.Click
        If cbDevice.SelectedItem Is Nothing Then
            SaveSetting(Application.ProductName, "Settings", "CapDevPath", vbNullString)
        Else
            SaveSetting(Application.ProductName, "Settings", "CapDevPath", CType(cbDevice.SelectedItem, DsDevice).DevicePath)
        End If
        Me.Close()
    End Sub
End Class