Public Class clsPS3Controller
    Inherits clsController
    Dim reportData(30) As Byte

    Private Enum PS3Buttons
        ps3A = &H2
        ps3B = &H4
        ps3Y = &H1
        ps3X = &H8
        ps3L1 = &H10
        ps3Start = &H2
    End Enum

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
        _IP = host.Substring(1)
        If Not setupTCP() Then Throw New Exception("TCP Setup Failed")
        ' 00 00 0F 80 80 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 02
        reportData(0) = 129
        reportData(1) = 1
        reportData(2) = 0
        reportData(3) = 27
        For i As Integer = 4 To 30
            reportData(i) = 0
        Next
        reportData(6) = 15
        reportData(7) = 128
        reportData(8) = 128
        reportData(30) = 2
    End Sub

    Public Overrides Sub sendReport(newReport() As Byte)
        'Debug.Print("Sent (" & _IP & "): " & BitConverter.ToString(newReport))
        'If newReport(4) <> 0 Or newReport(5) <> 0 Or newReport(6) <> 15 Then sck.Send(newReport)
        sck.Send(newReport)
    End Sub

    Public Overrides Function baseReport() As Byte()
        Return New Byte() {129, 1, 0, 27, 0, 0, 15, 128, 128, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2}
    End Function

    Public Overrides Function getReport() As Byte()
        Dim ps3buttonHi As Byte = 0
        Dim ps3buttonLo As Byte = 0
        Dim ps3hat As Byte = 0
        If buttonLo And &H1 Then ps3buttonHi = ps3buttonHi Or PS3Buttons.ps3L1
        If buttonLo And &H10 Then ps3buttonHi = ps3buttonHi Or PS3Buttons.ps3A
        If buttonLo And &H20 Then ps3buttonHi = ps3buttonHi Or PS3Buttons.ps3B
        If buttonLo And &H40 Then ps3buttonHi = ps3buttonHi Or PS3Buttons.ps3X
        If buttonLo And &H80 Then ps3buttonHi = ps3buttonHi Or PS3Buttons.ps3Y
        If buttonHi And &H10 Then ps3buttonLo = ps3buttonLo Or PS3Buttons.ps3Start

        'hat switch 0000 N, 0001 N/E, 0010 E, 0011 SE, 0100 S, 0101 SW, 0110 W, 0111, NW, 1000, nothing pressed
        Select Case buttonHi And &HF
            Case 1 'up
                ps3hat = 0
            Case 2 'down
                ps3hat = 4
            Case 4 'left
                ps3hat = 6
            Case 8 'right
                ps3hat = 2
            Case 9 'up/right
                ps3hat = 1
            Case 10 'down/right
                ps3hat = 3
            Case 6 'down/left
                ps3hat = 5
            Case 5   'up/left
                ps3hat = 7
            Case Else
                ps3hat = 15
        End Select

        Dim tmp(reportData.Length - 1) As Byte
        Array.Copy(reportData, tmp, reportData.Length)
        tmp(4) = ps3buttonHi
        tmp(5) = ps3buttonLo
        tmp(6) = ps3hat
        Return tmp
    End Function

    Protected Overrides Sub update(Optional force As Boolean = False)
        Dim ps3buttonHi As Byte = 0
        Dim ps3buttonLo As Byte = 0
        Dim ps3hat As Byte = 0
        If buttonLo And &H1 Then ps3buttonHi = ps3buttonHi Or PS3Buttons.ps3L1
        If buttonLo And &H10 Then ps3buttonHi = ps3buttonHi Or PS3Buttons.ps3A
        If buttonLo And &H20 Then ps3buttonHi = ps3buttonHi Or PS3Buttons.ps3B
        If buttonLo And &H40 Then ps3buttonHi = ps3buttonHi Or PS3Buttons.ps3X
        If buttonLo And &H80 Then ps3buttonHi = ps3buttonHi Or PS3Buttons.ps3Y
        If buttonHi And &H10 Then ps3buttonLo = ps3buttonLo Or PS3Buttons.ps3Start

        'hat switch 0000 N, 0001 N/E, 0010 E, 0011 SE, 0100 S, 0101 SW, 0110 W, 0111, NW, 1000, nothing pressed
        Select Case buttonHi And &HF
            Case 1 'up
                ps3hat = 0
            Case 2 'down
                ps3hat = 4
            Case 4 'left
                ps3hat = 6
            Case 8 'right
                ps3hat = 2
            Case 9 'up/right
                ps3hat = 1
            Case 10 'down/right
                ps3hat = 3
            Case 6 'down/left
                ps3hat = 5
            Case 5   'up/left
                ps3hat = 7
            Case Else
                ps3hat = 15
        End Select

        reportData(4) = ps3buttonHi
        reportData(5) = ps3buttonLo
        reportData(6) = ps3hat
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
