Public MustInherit Class clsController
    Implements IDisposable

    Protected buttonHi As Byte
    Protected buttonLo As Byte
    Protected joyRXInt As Int16
    Protected joyRYInt As Int16
    Protected joyLXInt As Int16
    Protected joyLYInt As Int16
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
        Public x As Int16
        Public y As Int16
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
        joyRXInt = 0
        joyRYInt = 0
        joyLXInt = 0
        joyLYInt = 0
        LT = 0
        RT = 0
    End Sub

    Public Sub reset(Optional doUpdate As Boolean = True)
        buttonHi = &H0
        buttonLo = &H0
        joyRXInt = 0
        joyRYInt = 0
        joyLXInt = 0
        joyLYInt = 0
        LT = 0
        RT = 0
        If doUpdate Then update(True)
    End Sub

    Public Sub setState(_buttonHi As Byte, _LT As Byte, _RT As Byte, _buttonLo As Byte, _joyLx As Int16, _joyLy As Int16, _joyRx As Int16, _joyRy As Int16)
        buttonHi = _buttonHi
        buttonLo = _buttonLo
        LT = _LT
        RT = _RT
        joyLXInt = _joyLx
        joyLYInt = _joyLy
        joyRXInt = _joyRx
        joyRYInt = _joyRy
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
        joyLXInt = pos.x
        joyLYInt = pos.y
        If doUpdate Then update()
        If doUpdate Then mut.ReleaseMutex()
    End Sub

    Public Sub setJoyR(pos As joystickPosition, Optional doUpdate As Boolean = True)
        If doUpdate Then mut.WaitOne()
        joyRXInt = pos.x
        joyRYInt = pos.y
        If doUpdate Then update()
        If doUpdate Then mut.ReleaseMutex()
    End Sub

    Public Sub setJoyL(x As Int16, y As Int16, Optional doUpdate As Boolean = True)
        If doUpdate Then mut.WaitOne()
        joyLXInt = x
        joyLYInt = y
        If doUpdate Then update()
        If doUpdate Then mut.ReleaseMutex()
    End Sub

    Public Sub setJoyR(x As Int16, y As Int16, Optional doUpdate As Boolean = True)
        If doUpdate Then mut.WaitOne()
        joyRXInt = x
        joyRYInt = y
        If doUpdate Then update()
        If doUpdate Then mut.ReleaseMutex()
    End Sub

    Public Sub setJoyLSX(x As Int16, Optional doUpdate As Boolean = True)
        If doUpdate Then mut.WaitOne()
        joyLXInt = x
        If doUpdate Then update()
        If doUpdate Then mut.ReleaseMutex()
    End Sub

    Public Sub setJoyLSY(y As Int16, Optional doUpdate As Boolean = True)
        If doUpdate Then mut.WaitOne()
        joyLYInt = y
        If doUpdate Then update()
        If doUpdate Then mut.ReleaseMutex()
    End Sub

    Public Sub setJoyRSX(x As Int16, Optional doUpdate As Boolean = True)
        If doUpdate Then mut.WaitOne()
        joyRXInt = x
        If doUpdate Then update()
        If doUpdate Then mut.ReleaseMutex()
    End Sub

    Public Sub setJoyRSY(y As Int16, Optional doUpdate As Boolean = True)
        If doUpdate Then mut.WaitOne()
        joyRYInt = y
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
