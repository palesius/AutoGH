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
        'Debug.Print("Sent (" & _IP & "): " & BitConverter.ToString(newReport))
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
        If joyLx < 128 Then tmp(11) = joyLx + 128 Else tmp(11) = joyLx - 128
        If joyLy <= 128 Then tmp(13) = 128 - joyLy Else tmp(13) = 384 - joyLy
        If joyRx < 128 Then tmp(15) = joyRx + 128 Else tmp(15) = joyRx - 128
        If joyRY <= 128 Then tmp(17) = 128 - joyRY Else tmp(17) = 384 - joyRY
        Return tmp
    End Function

    Protected Overrides Sub update(Optional force As Boolean = False)
        reportData(6) = buttonHi
        reportData(7) = buttonLo
        reportData(8) = LT
        reportData(9) = RT
        If joyLx < 128 Then reportData(11) = joyLx + 128 Else reportData(11) = joyLx - 128
        If joyLy <= 128 Then reportData(13) = 128 - joyLy Else reportData(13) = 383 - joyLy
        If joyRx < 128 Then reportData(15) = joyRx + 128 Else reportData(15) = joyRx - 128
        If joyRY <= 128 Then reportData(17) = 128 - joyRY Else reportData(17) = 383 - joyRY
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
