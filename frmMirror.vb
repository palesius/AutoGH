Public Class frmMirror

    Structure XIState
        Public eventCount As UInt32
        Public wButtons As UShort
        Public bLeftTrigger As Byte
        Public bRightTrigger As Byte
        Public sThumbLX As Short
        Public sThumbLY As Short
        Public sThumbRX As Short
        Public sThumbRY As Short
    End Structure

    <System.Runtime.InteropServices.DllImport("C:\Program Files (x86)\Steam\bin\xinput1_3.dll", EntryPoint:="#100", CallingConvention:=Runtime.InteropServices.CallingConvention.StdCall)>
    Public Shared Function getXInputState(idx As Integer, ByRef state As XIState) As Integer
    End Function

    Dim IPList As List(Of String)
    Dim controller(4) As clsController
    Dim running As Boolean = False
    Dim inputController As SharpDX.XInput.Controller
    Dim mirror(4) As Boolean
    Dim mirrorThread As System.Threading.Thread

    Public Sub New(controllerIPs As Dictionary(Of Byte, String))

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Dim uniqueControllers As New List(Of String)

        IPList = New List(Of String)
        IPList.Add(Nothing)
        For i As Integer = 1 To 4
            Dim cb As CheckBox = Nothing
            Select Case i
                Case 1
                    cb = cbTarget1
                Case 2
                    cb = cbTarget2
                Case 3
                    cb = cbTarget3
                Case 4
                    cb = cbTarget4
            End Select

            Dim controllerIP As String = vbNullString
            If controllerIPs.TryGetValue(i, controllerIP) AndAlso Not IPList.Contains(controllerIP) Then
                cb.Text = i & ": " & controllerIP
                cb.Enabled = True
                uniqueControllers.Add(controllerIP)
                IPList.Add(controllerIP)
            Else
                cb.Enabled = False
                cb.Text = "N/A"
                IPList.Add(Nothing)
            End If

        Next

    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
        For i As Integer = 1 To 4
            If Not controller(i) Is Nothing Then
                controller(i).dispose()
                controller(i) = Nothing
            End If
        Next
    End Sub

    Private Sub btnStart_Click(sender As System.Object, e As System.EventArgs) Handles btnStart.Click
        If inputController Is Nothing Then inputController = New SharpDX.XInput.Controller(SharpDX.XInput.UserIndex.One)
        If Not inputController.IsConnected Then
            MsgBox("No Input controller found.")
            Exit Sub
        End If
        btnStart.Enabled = False
        btnStop.Enabled = True

        For i As Integer = 1 To 4
            Select Case i
                Case 1
                    mirror(i) = cbTarget1.Checked
                Case 2
                    mirror(i) = cbTarget2.Checked
                Case 3
                    mirror(i) = cbTarget3.Checked
                Case 4
                    mirror(i) = cbTarget4.Checked
            End Select
            If IPList(i) <> vbNullString Then
                If System.Text.RegularExpressions.Regex.IsMatch(IPList(i), "^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$") Then
                    controller(i) = New clsBBBController(IPList(i))
                ElseIf System.Text.RegularExpressions.Regex.IsMatch(IPList(i), "^P(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$") Then
                    controller(i) = New clsPS3Controller(IPList(i))
                ElseIf System.Text.RegularExpressions.Regex.IsMatch(IPList(i), "^COM[1-9][0-9]?$") Then
                    controller(i) = New clsPS2Controller(IPList(i))
                ElseIf IPList(i).StartsWith("CM") Then
                    controller(i) = New clsCMHSController(IPList(i))
                Else
                    Stop
                End If
            End If
        Next

        running = True
        Dim ts As New System.Threading.ThreadStart(AddressOf Me.runMirror)
        mirrorThread = New System.Threading.Thread(ts)
        mirrorThread.Start()
    End Sub

    Private Sub runMirror()
        Dim packetNumber As Integer = 0
        Dim buttonHi As Byte
        Dim buttonLo As Byte
        Dim LT As Byte
        Dim RT As Byte
        Dim joyLXInt As Int16
        Dim joyLYInt As Int16
        Dim joyRXInt As Int16
        Dim joyRYInt As Int16

        Dim st As Date = Now
        While running
            Dim xs As SharpDX.XInput.State = inputController.GetState()
            If xs.PacketNumber > packetNumber Then
                packetNumber = xs.PacketNumber
                With xs.Gamepad
                    buttonHi = .Buttons And 255
                    buttonLo = .Buttons >> 8
                    LT = .LeftTrigger
                    RT = .RightTrigger
                    joyLXInt = .LeftThumbX
                    joyLYInt = .LeftThumbY
                    joyRXInt = .RightThumbX
                    joyRYInt = .RightThumbY
                End With
                For i = 1 To 4
                    If mirror(i) Then controller(i).setState(buttonHi, LT, RT, buttonLo, joyLXInt, joyLYInt, joyRXInt, joyRYInt)
                Next
            End If
        End While
    End Sub

    Private Sub btnStop_Click(sender As System.Object, e As System.EventArgs) Handles btnStop.Click
        running = False
        mirrorThread.Join()
        For i As Integer = 1 To 4
            If Not controller(i) Is Nothing Then
                controller(i).dispose()
                controller(i) = Nothing
            End If
        Next
        btnStart.Enabled = True
        btnStop.Enabled = False
        inputController = Nothing
    End Sub

    Private Function displayInput(buf() As Byte) As String
        Dim sb As New System.Text.StringBuilder
        For i As Integer = 0 To buf.Length - 1
            sb.Append(String.Format("{0:X1}:{1:X2}  ", i, buf(i)))
        Next
        Return sb.ToString
    End Function

    Private Sub cbTarget1_CheckedChanged(sender As Object, e As EventArgs) Handles cbTarget1.CheckedChanged
        mirror(1) = cbTarget1.Checked
    End Sub

    Private Sub cbTarget2_CheckedChanged(sender As Object, e As EventArgs) Handles cbTarget2.CheckedChanged
        mirror(2) = cbTarget2.Checked
    End Sub

    Private Sub cbTarget3_CheckedChanged(sender As Object, e As EventArgs) Handles cbTarget3.CheckedChanged
        mirror(3) = cbTarget3.Checked
    End Sub

    Private Sub cbTarget4_CheckedChanged(sender As Object, e As EventArgs) Handles cbTarget4.CheckedChanged
        mirror(4) = cbTarget4.Checked
    End Sub

    Private Sub btnClose_Click(sender As System.Object, e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class