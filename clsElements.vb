Enum enumGame
    gmGH2
    gmGH3
    gmRB
    gmRB2
    gmRBB
End Enum

Enum enumLevel
    lvlEasy = 0
    lvlMedium = 1
    lvlHard = 2
    lvlExpert = 3
End Enum

Friend Class clsGame
    Friend name As String
    Friend path As String
    Friend game As enumGame

    Friend Sub New(newName As String, newPath As String, newGame As enumGame)
        name = newName
        path = newPath
        game = newGame
    End Sub

    Friend ReadOnly Property strum As Boolean
        Get
            Select Case game
                Case enumGame.gmGH2, enumGame.gmGH3
                    Return False
                Case enumGame.gmRB, enumGame.gmRB2, enumGame.gmRBB
                    Return True
                Case Else
                    Return True
            End Select
        End Get
    End Property

    Friend ReadOnly Property noteGreen As Integer
        Get
            Select Case game
                Case enumGame.gmGH2, enumGame.gmGH3
                    Return &H10000
                Case enumGame.gmRB, enumGame.gmRB2, enumGame.gmRBB
                    Return clsController.XBButtons.btnA
                Case Else
                    Return True
            End Select
        End Get
    End Property

    Friend ReadOnly Property noteRed As Integer
        Get
            Select Case game
                Case enumGame.gmGH2, enumGame.gmGH3
                    Return clsController.XBButtons.btnLB
                Case enumGame.gmRB, enumGame.gmRB2, enumGame.gmRBB
                    Return clsController.XBButtons.btnB
                Case Else
                    Return True
            End Select
        End Get
    End Property

    Friend ReadOnly Property noteBlue As Integer
        Get
            Select Case game
                Case enumGame.gmGH2, enumGame.gmGH3
                    Return &H20000
                Case enumGame.gmRB, enumGame.gmRB2, enumGame.gmRBB
                    Return clsController.XBButtons.btnX
                Case Else
                    Return True
            End Select
        End Get
    End Property

    Friend ReadOnly Property noteYellow As Integer
        Get
            Select Case game
                Case enumGame.gmGH2, enumGame.gmGH3
                    Return clsController.XBButtons.btnRB
                Case enumGame.gmRB, enumGame.gmRB2, enumGame.gmRBB
                    Return clsController.XBButtons.btnY
                Case Else
                    Return True
            End Select
        End Get
    End Property

    Friend ReadOnly Property noteOrange As Integer
        Get
            Select Case game
                Case enumGame.gmGH2, enumGame.gmGH3
                    Return clsController.XBButtons.btnA
                Case enumGame.gmRB, enumGame.gmRB2, enumGame.gmRBB
                    Return clsController.XBButtons.btnLB
                Case Else
                    Return True
            End Select
        End Get
    End Property

    Friend ReadOnly Property dilation As Decimal
        Get
            Select Case game
                Case enumGame.gmGH2
                    Return 1.0025
                Case enumGame.gmGH3
                    Return 1
                Case Else
                    Return 1
            End Select
        End Get
    End Property

    Friend ReadOnly Property loadTime As Integer
        Get
            Select Case game
                Case enumGame.gmGH2
                    Return 2100
                Case enumGame.gmGH3
                    Return 3600
                Case enumGame.gmRB
                    Return 4000
                Case Else
                    Return 4000
            End Select
        End Get
    End Property

    Friend ReadOnly Property truncation As Integer
        Get
            Return 100
        End Get
    End Property

    Friend ReadOnly Property minimumDuration As Integer
        Get
            Select Case game
                Case enumGame.gmGH2
                    Return 50
                Case enumGame.gmGH3
                    Return 25
                Case Else
                    Return 50
            End Select
        End Get
    End Property

    Public Overrides Function toString() As String
        Return name
    End Function
End Class

Friend Class clsSong
    Friend _game As clsGame
    Friend name As String
    Friend fi As IO.FileInfo
    Private _mf As NAudio.Midi.MidiFile
    Private _startTime As Date

    Friend ReadOnly Property mf As NAudio.Midi.MidiFile
        Get
            If _mf Is Nothing Then _mf = New NAudio.Midi.MidiFile(fi.FullName)
            Return _mf
        End Get
    End Property

    Friend Sub New(newFI As IO.FileInfo, game As clsGame)
        fi = newFI
        name = IO.Path.GetFileNameWithoutExtension(fi.FullName)
        _game = game
    End Sub

    Public Overrides Function toString() As String
        Return name
    End Function

    'Public Sub startPlay(c As clsController)
    '    Select Case _game.game
    '        Case enumGame.gmGH2
    '            c.pressButtons(clsController.XBButtons.btnStart, 100)
    '            System.Threading.Thread.Sleep(500)
    '            c.pressButtons(clsController.XBButtons.btnDown, 100)
    '            System.Threading.Thread.Sleep(500)
    '            c.pressButtons(clsController.XBButtons.btnA, 100)
    '            System.Threading.Thread.Sleep(500)
    '            c.pressButtons(clsController.XBButtons.btnA, 100)
    '            _startTime = Now
    '        Case enumGame.gmGH3
    '            c.pressButtons(clsController.XBButtons.btnStart, 100)
    '            System.Threading.Thread.Sleep(500)
    '            c.pressButtons(clsController.XBButtons.btnDown, 100)
    '            System.Threading.Thread.Sleep(500)
    '            c.pressButtons(clsController.XBButtons.btnA, 100)
    '            System.Threading.Thread.Sleep(500)
    '            c.pressButtons(clsController.XBButtons.btnDown, 100)
    '            System.Threading.Thread.Sleep(500)
    '            c.pressButtons(clsController.XBButtons.btnA, 100)
    '            _startTime = Now
    '    End Select
    '    _startTime = _startTime.AddMilliseconds(_game.loadTime)
    'End Sub

    'Public Sub startRetry(c As clsController)
    '    Select Case _game.game
    '        Case enumGame.gmGH2, enumGame.gmGH3
    '            c.pressButtons(clsController.XBButtons.btnA, 100)
    '            _startTime = Now
    '    End Select
    '    _startTime = _startTime.AddMilliseconds(_game.loadTime)
    'End Sub
End Class

Friend Class clsTrack
    Friend _game As clsGame
    Friend _song As clsSong
    Friend name As String
    Friend mf As NAudio.Midi.MidiFile
    Friend index As Integer
    Friend lefty As Boolean

    Friend ReadOnly Property strumButton(Optional solo As Boolean = False) As Integer
        Get
            If Not _game.strum Then Return 0
            Select Case name
                Case "GUITAR"
                    Return IIf(solo, clsController.XBButtons.btnL3, clsController.XBButtons.btnUp)
                Case "BASS", "RHYTHM", "GUITAR COOP"
                    Return clsController.XBButtons.btnUp
                Case Else
                    Return 0
            End Select
        End Get
    End Property

    Friend ReadOnly Property noteGreen(Optional nostrum As Boolean = False, Optional solo As Boolean = False) As Integer
        Get
            Select Case name
                Case "DRUMS"
                    Select Case _game.name
                        Case "Rock Band"
                            Return _game.noteGreen + IIf(nostrum, 0, strumButton)
                        Case "Rock Band Beatles", "Rock Band II"
                            Return _game.noteOrange + IIf(nostrum, 0, strumButton)
                        Case Else
                            Stop
                            Return 0
                    End Select
                Case "GUITAR"
                    If lefty Then
                        Return _game.noteOrange + IIf(nostrum, 0, strumButton(solo))
                    Else
                        Return _game.noteGreen + IIf(nostrum, 0, strumButton(solo))
                    End If
                Case Else
                    If lefty Then
                        Return _game.noteOrange + IIf(nostrum, 0, strumButton)
                    Else
                        Return _game.noteGreen + IIf(nostrum, 0, strumButton)
                    End If
            End Select
        End Get
    End Property

    Friend ReadOnly Property noteRed(Optional nostrum As Boolean = False, Optional solo As Boolean = False) As Integer
        Get
            Select Case name
                Case "DRUMS"
                    Select Case _game.name
                        Case "Rock Band"
                            Return _game.noteBlue + IIf(nostrum, 0, strumButton)
                        Case "Rock Band Beatles", "Rock Band II"
                            Return _game.noteRed + IIf(nostrum, 0, strumButton)
                        Case Else
                            Stop
                            Return 0
                    End Select
                Case "GUITAR"
                    If lefty Then
                        Return _game.noteBlue + IIf(nostrum, 0, strumButton(solo))
                    Else
                        Return _game.noteRed + IIf(nostrum, 0, strumButton(solo))
                    End If
                Case Else
                    If lefty Then
                        Return _game.noteBlue + IIf(nostrum, 0, strumButton)
                    Else
                        Return _game.noteRed + IIf(nostrum, 0, strumButton)
                    End If
            End Select
        End Get
    End Property

    Friend ReadOnly Property noteBlue(Optional nostrum As Boolean = False, Optional solo As Boolean = False) As Integer
        Get
            Select Case name
                Case "DRUMS"
                    Select Case _game.name
                        Case "Rock Band"
                            Return _game.noteRed + IIf(nostrum, 0, strumButton)
                        Case "Rock Band Beatles", "Rock Band II"
                            Return _game.noteBlue + IIf(nostrum, 0, strumButton)
                        Case Else
                            Stop
                            Return 0
                    End Select
                Case "GUITAR"
                    If lefty Then
                        Return _game.noteRed + IIf(nostrum, 0, strumButton(solo))
                    Else
                        Return _game.noteBlue + IIf(nostrum, 0, strumButton(solo))
                    End If
                Case Else
                    If lefty Then
                        Return _game.noteRed + IIf(nostrum, 0, strumButton)
                    Else
                        Return _game.noteBlue + IIf(nostrum, 0, strumButton)
                    End If
            End Select
        End Get
    End Property

    Friend ReadOnly Property noteYellow(Optional nostrum As Boolean = False, Optional solo As Boolean = False) As Integer
        Get
            Select Case name
                Case "GUITAR"
                    Return _game.noteYellow + IIf(nostrum, 0, strumButton(solo))
                Case Else
                    Return _game.noteYellow + IIf(nostrum, 0, strumButton)
            End Select
        End Get
    End Property

    Friend ReadOnly Property noteOrange(Optional nostrum As Boolean = False, Optional solo As Boolean = False) As Integer
        Get
            Select Case name
                Case "DRUMS"
                    Select Case _game.name
                        Case "Rock Band"
                            Return _game.noteOrange + IIf(nostrum, 0, strumButton)
                        Case "Rock Band Beatles", "Rock Band II"
                            Return _game.noteGreen + IIf(nostrum, 0, strumButton)
                        Case Else
                            Stop
                            Return 0
                    End Select
                Case "GUITAR"
                    If lefty Then
                        Return _game.noteGreen + IIf(nostrum, 0, strumButton(solo))
                    Else
                        Return _game.noteOrange + IIf(nostrum, 0, strumButton(solo))
                    End If
                Case Else
                    If lefty Then
                        Return _game.noteGreen + IIf(nostrum, 0, strumButton)
                    Else
                        Return _game.noteOrange + IIf(nostrum, 0, strumButton)
                    End If
            End Select
        End Get
    End Property

    Friend Sub New(newMF As NAudio.Midi.MidiFile, newIndex As Integer, newName As String, song As clsSong)
        mf = newMF
        index = newIndex
        name = newName
        _song = song
        _game = _song._game
        lefty = False
    End Sub

    Public Overrides Function toString() As String
        Return name
    End Function
End Class

Friend Class clsLevel
    Friend index As enumLevel

    Friend Sub New(newIndex As enumLevel)
        index = newIndex
    End Sub

    Friend ReadOnly Property baseNote As Integer
        Get
            Select Case index
                Case enumLevel.lvlEasy
                    Return 60
                Case enumLevel.lvlMedium
                    Return 72
                Case enumLevel.lvlHard
                    Return 84
                Case enumLevel.lvlExpert
                    Return 96
                Case Else
                    Return -1
            End Select
        End Get
    End Property

    Public Overrides Function toString() As String
        Select Case index
            Case enumLevel.lvlEasy
                Return "Easy"
            Case enumLevel.lvlMedium
                Return "Medium"
            Case enumLevel.lvlHard
                Return "Hard"
            Case enumLevel.lvlExpert
                Return "Expert"
            Case Else
                Return vbNullString
        End Select
    End Function
End Class