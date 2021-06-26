Imports HidSharp
Public Class frmCMIdentify

    Dim devicePaths As New List(Of String)
    Dim deviceDict As New Dictionary(Of String, HidDevice)

    Private Sub btnDetect_Click(sender As System.Object, e As System.EventArgs) Handles btnDetect.Click
        MsgBox("New Version")
        devicePaths.Clear()
        deviceDict.Clear()
        For Each hdev As HidDevice In DeviceList.Local.GetDevices(DeviceTypes.Hid)
            'Dim vpString As String = String.Format("{0:X4}{1:X4}", hdev.Attributes.VendorId, hdev.Attributes.ProductId)
            'Select Case vpString
            '    Case "0EB8F000", "0B67555E"
            '        portName = "USB"
            '        usbID = portName
            '        usbDev = hdev
            '        usbDev.OpenDevice()
            '        Exit Sub
            'End Select
            Dim vpString As String = String.Format("{0:X4}{1:X4}", hdev.VendorID, hdev.ProductID)
            Select Case vpString
                Case "20080001", "25080003", "25080001", "20080010"
                    devicePaths.Add(hdev.DevicePath)
                    deviceDict.Add(hdev.DevicePath, hdev)
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
            btnIdentifyAll.Enabled = True
        End If
    End Sub

    Private Sub btnExit_Click(sender As System.Object, e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnIdentify_Click(sender As System.Object, e As System.EventArgs) Handles btnIdentify.Click
        Dim dev As HidDevice = deviceDict(devicePaths(CInt(cbDevices.SelectedItem) - 1))
        Dim ds As DeviceStream = dev.Open
        ds.Write(New Byte() {0, 7, 0, 0, 1}, 0, 5)
        System.Threading.Thread.Sleep(1000)
        ds.Write(New Byte() {0, 8, 0, 0, 1}, 0, 5)
        ds.Close()
        ds.Dispose()
    End Sub

    Private Sub btnIdentifyAll_Click(sender As System.Object, e As System.EventArgs) Handles btnIdentifyAll.Click
        'Dim dev As HidDevice = HidDevices.GetDevice(devicePaths(0))
        'dev.OpenDevice()
        'For i As Integer = 0 To 255
        '    dev.Write(New Byte() {0, 6, 11, 0, 1, 0, 0, 0, 0, 1, 1, 0, i Mod 8, 255})
        '    System.Threading.Thread.Sleep(100)
        'Next
        'dev.CloseDevice()

        For i As Integer = 0 To devicePaths.Count - 1
            Dim dev As HidDevice = deviceDict(devicePaths(i))
            Dim ds As DeviceStream = dev.Open
            ds.Write(New Byte() {0, 6, 11, 0, 1, 0, 0, 0, 0, 1, 1, 0, i + 1, 255}, 0, 14)
            ds.Close()
            ds.Dispose()
        Next
    End Sub
End Class