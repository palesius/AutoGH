Imports HidLibrary
Public Class frmCMIdentify

    Dim devicePaths As New List(Of String)

    Private Sub btnDetect_Click(sender As System.Object, e As System.EventArgs) Handles btnDetect.Click
        For Each hdev As HidDevice In HidDevices.Enumerate()
            'Dim vpString As String = String.Format("{0:X4}{1:X4}", hdev.Attributes.VendorId, hdev.Attributes.ProductId)
            'Select Case vpString
            '    Case "0EB8F000", "0B67555E"
            '        portName = "USB"
            '        usbID = portName
            '        usbDev = hdev
            '        usbDev.OpenDevice()
            '        Exit Sub
            'End Select
            Dim vpString As String = String.Format("{0:X4}{1:X4}", hdev.Attributes.VendorId, hdev.Attributes.ProductId)
            Select Case vpString
                Case "20080001"
                    devicePaths.Add(hdev.DevicePath)
            End Select
        Next
        MsgBox("Found " & devicePaths.Count & " CM Devices")
        If devicePaths.Count > 0 Then
            devicePaths.Sort()
            cbDevices.Items.Clear()
            For i = 1 To devicePaths.Count
                cbDevices.Items.Add(i)
            Next
            cbDevices.SelectedIndex = 0
            btnIdentify.Enabled = True
        End If
    End Sub

    Private Sub btnExit_Click(sender As System.Object, e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnIdentify_Click(sender As System.Object, e As System.EventArgs) Handles btnIdentify.Click
        Dim dev As HidDevice = HidDevices.GetDevice(devicePaths(CInt(cbDevices.SelectedItem) - 1))
        dev.OpenDevice()
        dev.Write(New Byte() {0, 7, 0, 0, 1})
        System.Threading.Thread.Sleep(1000)
        dev.Write(New Byte() {0, 8, 0, 0, 1})
        dev.CloseDevice()
    End Sub
End Class