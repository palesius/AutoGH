'Enum enumGame
'    gmGH2
'    gmGH3
'    gmRB
'    gmRB2
'    gmRBB
'    gmLRB
'End Enum

Enum enumLevel
    lvlEasy = 0
    lvlMedium = 1
    lvlHard = 2
    lvlExpert = 3
End Enum

'Friend Class clsGame
'    Friend name As String
'    Friend path As String
'    'Friend game As enumGame

'    Friend Sub New(newName As String, newPath As String, newGame As enumGame)
'        name = newName
'        path = newPath
'        game = newGame
'    End Sub

'    Friend ReadOnly Property strum As Boolean
'        Get
'            Select Case game
'                Case enumGame.gmGH2, enumGame.gmGH3
'                    Return False
'                Case enumGame.gmRB, enumGame.gmRB2, enumGame.gmRBB, enumGame.gmLRB
'                    Return True
'                Case Else
'                    Return True
'            End Select
'        End Get
'    End Property

'    Friend ReadOnly Property noteGreen As Integer
'        Get
'            Select Case game
'                Case enumGame.gmGH2, enumGame.gmGH3
'                    Return &H10000
'                Case enumGame.gmRB, enumGame.gmRB2, enumGame.gmRBB, enumGame.gmLRB
'                    Return clsController.XBButtons.btnA
'                Case Else
'                    Return True
'            End Select
'        End Get
'    End Property

'    Friend ReadOnly Property noteRed As Integer
'        Get
'            Select Case game
'                Case enumGame.gmGH2, enumGame.gmGH3
'                    Return clsController.XBButtons.btnLB
'                Case enumGame.gmRB, enumGame.gmRB2, enumGame.gmRBB, enumGame.gmLRB
'                    Return clsController.XBButtons.btnB
'                Case Else
'                    Return True
'            End Select
'        End Get
'    End Property

'    Friend ReadOnly Property noteBlue As Integer
'        Get
'            Select Case game
'                Case enumGame.gmGH2, enumGame.gmGH3
'                    Return &H20000
'                Case enumGame.gmRB, enumGame.gmRB2, enumGame.gmRBB, enumGame.gmLRB
'                    Return clsController.XBButtons.btnX
'                Case Else
'                    Return True
'            End Select
'        End Get
'    End Property

'    Friend ReadOnly Property noteYellow As Integer
'        Get
'            Select Case game
'                Case enumGame.gmGH2, enumGame.gmGH3
'                    Return clsController.XBButtons.btnRB
'                Case enumGame.gmRB, enumGame.gmRB2, enumGame.gmRBB, enumGame.gmLRB
'                    Return clsController.XBButtons.btnY
'                Case Else
'                    Return True
'            End Select
'        End Get
'    End Property

'    Friend ReadOnly Property noteOrange As Integer
'        Get
'            Select Case game
'                Case enumGame.gmGH2, enumGame.gmGH3
'                    Return clsController.XBButtons.btnA
'                Case enumGame.gmRB, enumGame.gmRB2, enumGame.gmRBB, enumGame.gmLRB
'                    Return clsController.XBButtons.btnLB
'                Case Else
'                    Return True
'            End Select
'        End Get
'    End Property

'    Friend ReadOnly Property dilation As Decimal
'        Get
'            Select Case game
'                Case enumGame.gmGH2
'                    Return 1.0025
'                Case enumGame.gmGH3
'                    Return 1.00008
'                Case Else
'                    Return 1
'            End Select
'        End Get
'    End Property

'    Friend ReadOnly Property loadTime As Integer
'        Get
'            Select Case game
'                Case enumGame.gmGH2
'                    Return 2100
'                Case enumGame.gmGH3
'                    Return 3600
'                Case enumGame.gmRB
'                    Return 4000
'                Case Else
'                    Return 4000
'            End Select
'        End Get
'    End Property

'    Friend ReadOnly Property truncation As Integer
'        Get
'            Return 100
'        End Get
'    End Property

'    Friend ReadOnly Property minimumDuration As Integer
'        Get
'            Select Case game
'                Case enumGame.gmGH2
'                    Return 50
'                Case enumGame.gmGH3
'                    Return 25
'                Case enumGame.gmLRB
'                    Return 35
'                Case Else
'                    Return 50
'            End Select
'        End Get
'    End Property

'    Public Overrides Function toString() As String
'        Return name
'    End Function
'End Class

Friend Class clsSong
    Friend _game As clsRhythmGame
    Friend name As String
    Friend title As String
    Friend fi As IO.FileInfo
    Private _mf As NAudio.Midi.MidiFile
    Private _startTime As Date

    Friend ReadOnly Property mf As NAudio.Midi.MidiFile
        Get
            If _mf Is Nothing Then _mf = New NAudio.Midi.MidiFile(fi.FullName, False)
            Return _mf
        End Get
    End Property

    Friend Sub New(newFI As IO.FileInfo, game As clsRhythmGame)
        fi = newFI
        name = IO.Path.GetFileNameWithoutExtension(fi.FullName)
        ' Only Rock Band MIDIs have song titles which will get loaded when the track list is parsed
        ' Provide a suitable default for Guitar Hero songs
        title = name
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
    Friend _game As clsRhythmGame
    Friend _song As clsSong
    Friend name As String
    Friend mf As NAudio.Midi.MidiFile
    Friend mfPath As String
    Friend index As Integer
    Friend lefty As Boolean
    Friend hopo As Boolean

    Friend ReadOnly Property strumButton(Optional solo As Boolean = False) As Integer
        Get
            Select Case name
                Case "GUITAR"
                    If solo Then Return _game.soloStrum Else Return _game.strum
                Case "BASS", "RHYTHM", "GUITAR COOP"
                    Return _game.strum
                Case Else
                    Return 0
            End Select
        End Get
    End Property

    Friend ReadOnly Property noteGreen(Optional nostrum As Boolean = False, Optional solo As Boolean = False) As Integer
        Get
            Select Case name
                Case "DRUMS"
                    Return _game.drumNotes(0) + IIf(nostrum, 0, strumButton(solo))
                Case Else
                    If lefty Then
                        Return _game.notes(4) + IIf(nostrum, 0, strumButton(solo))
                    Else
                        Return _game.notes(0) + IIf(nostrum, 0, strumButton(solo))
                    End If
            End Select
        End Get
    End Property

    Friend ReadOnly Property noteRed(Optional nostrum As Boolean = False, Optional solo As Boolean = False) As Integer
        Get
            Select Case name
                Case "DRUMS"
                    Return _game.drumNotes(1) + IIf(nostrum, 0, strumButton(solo))
                Case Else
                    If lefty Then
                        Return _game.notes(3) + IIf(nostrum, 0, strumButton(solo))
                    Else
                        Return _game.notes(1) + IIf(nostrum, 0, strumButton(solo))
                    End If
            End Select
        End Get
    End Property

    Friend ReadOnly Property noteYellow(Optional nostrum As Boolean = False, Optional solo As Boolean = False) As Integer
        Get
            Select Case name
                Case "DRUMS"
                    Return _game.drumNotes(2) + IIf(nostrum, 0, strumButton(solo))
                Case Else
                    Return _game.notes(2) + IIf(nostrum, 0, strumButton(solo))
            End Select
        End Get
    End Property

    Friend ReadOnly Property noteBlue(Optional nostrum As Boolean = False, Optional solo As Boolean = False) As Integer
        Get
            Select Case name
                Case "DRUMS"
                    Return _game.drumNotes(3) + IIf(nostrum, 0, strumButton(solo))
                Case Else
                    If lefty Then
                        Return _game.notes(1) + IIf(nostrum, 0, strumButton(solo))
                    Else
                        Return _game.notes(3) + IIf(nostrum, 0, strumButton(solo))
                    End If
            End Select
        End Get
    End Property

    Friend ReadOnly Property noteOrange(Optional nostrum As Boolean = False, Optional solo As Boolean = False) As Integer
        Get
            Select Case name
                Case "DRUMS"
                    Return _game.drumNotes(4) + IIf(nostrum, 0, strumButton(solo))
                Case Else
                    If lefty Then
                        Return _game.notes(0) + IIf(nostrum, 0, strumButton(solo))
                    Else
                        Return _game.notes(4) + IIf(nostrum, 0, strumButton(solo))
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