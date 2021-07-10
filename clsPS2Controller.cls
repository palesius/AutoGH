Public Class clsPS2Controller
    Inherits clsController
    Dim reportData(6) As Byte
    Dim lastReport(6) As Byte

    Private Enum PS2Buttons
        ps2Select = &H100
        ps2L3 = &H200
        ps2R3 = &H400
        ps2Start = &H800
        ps2up = &H1000
        ps2right = &H2000
        ps2down = &H4000
        ps2left = &H8000
        ps2l2 = &H1
        ps2r2 = &H2
        ps2l1 = &H4
        ps2r1 = &H8
        ps2Y = &H10
        ps2B = &H20
        ps2A = &H40
        ps2X = &H80
    End Enum

    Private com As System.IO.Ports.SerialPort

    Public Overrides ReadOnly Property IFType As String
        Get
            Return "PS2"
        End Get
    End Property

    Public Property port As String
        Get
            Return com.PortName
        End Get
        Set(value As String)
            If com.PortName <> value Then
                If Not com Is Nothing Then
                    com.Close()
                    com = Nothing
                End If
                If value <> vbNullString Then
                    com = New System.IO.Ports.SerialPort
                    com.PortName = value
                    com.Open()
                    update()
                End If
            End If
        End Set
    End Property

    Public Sub New(newPort As String)
        com = New System.IO.Ports.SerialPort
        com.PortName = newPort
        com.Open()
        reportData = baseReport()
        lastReport = New Byte() {0, 0, 0, 0, 0, 0, 0}
        update()
    End Sub

    Public Overrides Function baseReport() As Byte()
        Return New Byte() {&H5A, &HFF, &HFF, &H80, &H80, &H80, &H80}
    End Function

    Public Overrides Sub sendReport(newReport() As Byte)
        com.Write(newReport, 0, 7)
    End Sub

    Public Overrides Function getReport() As Byte()
        Dim ps2buttonHi As Byte = 0
        Dim ps2buttonLo As Byte = 0
        If buttonHi And &H1 Then ps2buttonHi = ps2buttonHi Or (PS2Buttons.ps2up \ 256)
        If buttonHi And &H2 Then ps2buttonHi = ps2buttonHi Or (PS2Buttons.ps2down \ 256)
        If buttonHi And &H4 Then ps2buttonHi = ps2buttonHi Or (PS2Buttons.ps2left \ 256)
        If buttonHi And &H8 Then ps2buttonHi = ps2buttonHi Or (PS2Buttons.ps2right \ 256)
        If buttonHi And &H10 Then ps2buttonHi = ps2buttonHi Or (PS2Buttons.ps2Start \ 256)
        If buttonHi And &H20 Then ps2buttonHi = ps2buttonHi Or (PS2Buttons.ps2Select \ 256)
        If buttonHi And &H40 Then ps2buttonHi = ps2buttonHi Or (PS2Buttons.ps2L3 \ 256)
        If buttonHi And &H80 Then ps2buttonHi = ps2buttonHi Or (PS2Buttons.ps2R3 \ 256)
        If buttonLo And &H1 Then ps2buttonLo = ps2buttonLo Or PS2Buttons.ps2l1
        If buttonLo And &H2 Then ps2buttonLo = ps2buttonLo Or PS2Buttons.ps2r1
        If LT > 128 Then ps2buttonLo = ps2buttonLo Or PS2Buttons.ps2l2
        If RT > 128 Then ps2buttonLo = ps2buttonLo Or PS2Buttons.ps2r2
        If buttonLo And &H10 Then ps2buttonLo = ps2buttonLo Or PS2Buttons.ps2A
        If buttonLo And &H20 Then ps2buttonLo = ps2buttonLo Or PS2Buttons.ps2B
        If buttonLo And &H40 Then ps2buttonLo = ps2buttonLo Or PS2Buttons.ps2X
        If buttonLo And &H80 Then ps2buttonLo = ps2buttonLo Or PS2Buttons.ps2Y
        ps2buttonHi = Not ps2buttonHi
        ps2buttonLo = Not ps2buttonLo
        Dim tmp(6) As Byte
        tmp(0) = &H5A
        tmp(1) = ps2buttonHi
        tmp(2) = ps2buttonLo

        Dim uVal As Integer
        If joyRXInt >= 0 Then uVal = joyRXInt Else uVal = 65536 + joyRXInt
        tmp(3) = uVal >> 8
        If joyRYInt >= 0 Then uVal = joyRYInt Else uVal = 65536 + joyRYInt
        tmp(4) = uVal >> 8
        If joyLXInt >= 0 Then uVal = joyLXInt Else uVal = 65536 + joyLXInt
        tmp(5) = uVal >> 8
        If joyLYInt >= 0 Then uVal = joyLYInt Else uVal = 65536 + joyLYInt
        tmp(6) = uVal >> 8
        Return tmp
    End Function

    Protected Overrides Sub update(Optional force As Boolean = False)
        Dim ps2buttonHi As Byte = 0
        Dim ps2buttonLo As Byte = 0
        If buttonHi And &H1 Then ps2buttonHi = ps2buttonHi Or (PS2Buttons.ps2up \ 256)
        If buttonHi And &H2 Then ps2buttonHi = ps2buttonHi Or (PS2Buttons.ps2down \ 256)
        If buttonHi And &H4 Then ps2buttonHi = ps2buttonHi Or (PS2Buttons.ps2left \ 256)
        If buttonHi And &H8 Then ps2buttonHi = ps2buttonHi Or (PS2Buttons.ps2right \ 256)
        If buttonHi And &H10 Then ps2buttonHi = ps2buttonHi Or (PS2Buttons.ps2Start \ 256)
        If buttonHi And &H20 Then ps2buttonHi = ps2buttonHi Or (PS2Buttons.ps2Select \ 256)
        If buttonHi And &H40 Then ps2buttonHi = ps2buttonHi Or (PS2Buttons.ps2L3 \ 256)
        If buttonHi And &H80 Then ps2buttonHi = ps2buttonHi Or (PS2Buttons.ps2R3 \ 256)
        If buttonLo And &H1 Then ps2buttonLo = ps2buttonLo Or PS2Buttons.ps2l1
        If buttonLo And &H2 Then ps2buttonLo = ps2buttonLo Or PS2Buttons.ps2r1
        If LT > 128 Then ps2buttonLo = ps2buttonLo Or PS2Buttons.ps2l2
        If RT > 128 Then ps2buttonLo = ps2buttonLo Or PS2Buttons.ps2r2
        If buttonLo And &H10 Then ps2buttonLo = ps2buttonLo Or PS2Buttons.ps2A
        If buttonLo And &H20 Then ps2buttonLo = ps2buttonLo Or PS2Buttons.ps2B
        If buttonLo And &H40 Then ps2buttonLo = ps2buttonLo Or PS2Buttons.ps2X
        If buttonLo And &H80 Then ps2buttonLo = ps2buttonLo Or PS2Buttons.ps2Y
        ps2buttonHi = Not ps2buttonHi
        ps2buttonLo = Not ps2buttonLo
        reportData(1) = ps2buttonHi
        reportData(2) = ps2buttonLo

        Dim uVal As Integer
        If joyRXInt >= 0 Then uVal = joyRXInt Else uVal = 65536 + joyRXInt
        reportData(3) = uVal >> 8
        If joyRYInt >= 0 Then uVal = joyRYInt Else uVal = 65536 + joyRYInt
        reportData(4) = uVal >> 8
        If joyLXInt >= 0 Then uVal = joyLXInt Else uVal = 65536 + joyLXInt
        reportData(5) = uVal >> 8
        If joyLYInt >= 0 Then uVal = joyLYInt Else uVal = 65536 + joyLYInt
        reportData(6) = uVal >> 8
        sendReport(reportData)
    End Sub

    Public Overrides Sub dispose()
        com.Close()
        com = Nothing
    End Sub

    Protected Overrides Sub Finalize()
        If Not com Is Nothing Then
            com.Close()
            com = Nothing
        End If
        MyBase.Finalize()
    End Sub
End Class
