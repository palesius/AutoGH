Public MustInherit Class clsController
    Implements IDisposable

    Protected buttonHi As Byte
    Protected buttonLo As Byte
    Protected joyRx As Byte
    Protected joyRY As Byte
    Protected joyLx As Byte
    Protected joyLy As Byte
    Protected LT As Byte
    Protected RT As Byte
    Private mut As New System.Threading.Mutex

    Public MustOverride ReadOnly Property IFType As String
    Protected MustOverride Sub update(Optional force As Boolean = False)
    Public MustOverride Sub dispose() Implements IDisposable.Dispose
    Public MustOverride Sub sendReport(newReport() As Byte)
    Public MustOverride Function baseReport() As Byte()
    Public MustOverride Function getReport() As Byte()

    Public Sub resetController()
        sendReport(baseReport)
    End Sub

    Public Structure joystickPosition
        Public x As SByte
        Public y As SByte
    End Structure

    Public Enum XBButtons
        btnUp = &H100
        btnDown = &H200
        btnLeft = &H400
        btnRight = &H800
        btnStart = &H1000
        btnBack = &H2000
        btnL3 = &H4000
        btnR3 = &H8000
        btnLB = &H1
        btnRB = &H2
        btnGuide = &H4
        btnA = &H10
        btnB = &H20
        btnX = &H40
        btnY = &H80
    End Enum

    Public Sub New()
        buttonHi = &H0
        buttonLo = &H0
        joyRx = 128
        joyRY = 128
        joyLx = 128
        joyLy = 128
        LT = 0
        RT = 0
    End Sub

    Public Sub reset(Optional doUpdate As Boolean = True)
        buttonHi = &H0
        buttonLo = &H0
        joyRx = 128
        joyRY = 128
        joyLx = 128
        joyLy = 128
        LT = 0
        RT = 0
        If doUpdate Then update(True)
    End Sub

    Public Sub setState(_buttonHi As Byte, _LT As Byte, _RT As Byte, _buttonLo As Byte, _joyLx As Byte, _joyLy As Byte, _joyRx As Byte, _joyRy As Byte)
        buttonHi = _buttonHi
        buttonLo = _buttonLo
        LT = _LT
        RT = _RT
        joyLx = _joyLx
        joyLy = _joyLy
        joyRx = _joyRx
        joyRY = _joyRy
        update()
    End Sub

    Public Sub pressButtons(map As XBButtons, _LT As Boolean, _RT As Boolean, Optional doUpdate As Boolean = True)
        Dim mapBytes() As Byte = BitConverter.GetBytes(map)
        buttonHi = buttonHi Or mapBytes(1)
        buttonLo = buttonLo Or mapBytes(0)
        If _LT Then LT = 255
        If _RT Then RT = 255
        If doUpdate Then update()
    End Sub

    Public Sub pressButtons(map As XBButtons, Optional doUpdate As Boolean = True)
        Dim mapBytes() As Byte = BitConverter.GetBytes(map)
        buttonHi = buttonHi Or mapBytes(1)
        buttonLo = buttonLo Or mapBytes(0)
        If doUpdate Then update()
    End Sub

    Public Sub releaseButtons(map As XBButtons, _LT As Boolean, _RT As Boolean, Optional doUpdate As Boolean = True)
        If doUpdate Then mut.WaitOne()
        Dim mapBytes() As Byte = BitConverter.GetBytes(map)
        buttonHi = buttonHi And (255 Xor mapBytes(1))
        buttonLo = buttonLo And (255 Xor mapBytes(0))
        If _LT Then LT = 0
        If _RT Then RT = 0
        If doUpdate Then update()
        If doUpdate Then mut.ReleaseMutex()
    End Sub

    Public Sub releaseButtons(map As XBButtons, Optional doUpdate As Boolean = True)
        If doUpdate Then mut.WaitOne()
        Dim mapBytes() As Byte = BitConverter.GetBytes(map)
        buttonHi = buttonHi And (255 Xor mapBytes(1))
        buttonLo = buttonLo And (255 Xor mapBytes(0))
        If doUpdate Then update()
        If doUpdate Then mut.ReleaseMutex()
    End Sub

    Public Sub setJoyL(pos As joystickPosition, Optional doUpdate As Boolean = True)
        If doUpdate Then mut.WaitOne()
        joyLx = pos.x + 128
        joyLy = pos.y + 128
        If doUpdate Then update()
        If doUpdate Then mut.ReleaseMutex()
    End Sub

    Public Sub setJoyR(pos As joystickPosition, Optional doUpdate As Boolean = True)
        If doUpdate Then mut.WaitOne()
        joyRx = pos.x + 128
        joyRY = pos.y + 128
        If doUpdate Then update()
        If doUpdate Then mut.ReleaseMutex()
    End Sub

    Public Sub setJoyL(x As SByte, y As SByte, Optional doUpdate As Boolean = True)
        If doUpdate Then mut.WaitOne()
        joyLx = x + 128
        joyLy = y + 128
        If doUpdate Then update()
        If doUpdate Then mut.ReleaseMutex()
    End Sub

    Public Sub setJoyR(x As SByte, y As SByte, Optional doUpdate As Boolean = True)
        If doUpdate Then mut.WaitOne()
        joyRx = x + 128
        joyRY = y + 128
        If doUpdate Then update()
        If doUpdate Then mut.ReleaseMutex()
    End Sub

    Public Sub setJoyLSX(x As SByte, Optional doUpdate As Boolean = True)
        If doUpdate Then mut.WaitOne()
        joyLx = x + 128
        If doUpdate Then update()
        If doUpdate Then mut.ReleaseMutex()
    End Sub

    Public Sub setJoyLSY(y As SByte, Optional doUpdate As Boolean = True)
        If doUpdate Then mut.WaitOne()
        joyLy = y + 128
        If doUpdate Then update()
        If doUpdate Then mut.ReleaseMutex()
    End Sub

    Public Sub setJoyRSX(x As SByte, Optional doUpdate As Boolean = True)
        If doUpdate Then mut.WaitOne()
        joyRx = x + 128
        If doUpdate Then update()
        If doUpdate Then mut.ReleaseMutex()
    End Sub

    Public Sub setJoyRSY(y As SByte, Optional doUpdate As Boolean = True)
        If doUpdate Then mut.WaitOne()
        joyRY = y + 128
        If doUpdate Then update()
        If doUpdate Then mut.ReleaseMutex()
    End Sub

    Public Sub setLT(val As Byte, Optional doUpdate As Boolean = True)
        If doUpdate Then mut.WaitOne()
        LT = val
        If doUpdate Then update()
        If doUpdate Then mut.ReleaseMutex()
    End Sub

    Public Sub setRT(val As Byte, Optional doUpdate As Boolean = True)
        If doUpdate Then mut.WaitOne()
        RT = val
        If doUpdate Then update()
        If doUpdate Then mut.ReleaseMutex()
    End Sub
End Class
