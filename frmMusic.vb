'editor with lines
'press <buttons>
'hold <buttons> of <n> ms
'release <buttons>
'goto <lineX>[,<repeatY>]
'ability to pause,stop,start from a certain line

Public Class frmMusic

    Private basepath As String

    Private arrGames(5) As clsGame
    Private lstSongs As New List(Of clsSong)
    Private lstTracks As New List(Of clsTrack)
    Private arrLevel(3) As clsLevel

    Private lastGame As clsGame
    Private lastSong As clsSong

    Private cbTrack(3) As ComboBox
    Private cbLevel(3) As ComboBox

    Public actions As List(Of clsAction)
    Public info As String

    Private Sub saveSettings()
        Dim val As String
        If lastGame Is Nothing Then val = "" Else val = lastGame.name
        SaveSetting(Application.ProductName, "Settings", "Game", val)
        If lastSong Is Nothing Then val = "" Else val = lastSong.name
        SaveSetting(Application.ProductName, "Settings", "Song", val)
        SaveSetting(Application.ProductName, "Settings", "Manual", IIf(cbManual.Checked, "True", "False"))
        For i As Integer = 0 To 3
            If cbTrack(i).SelectedItem Is Nothing Then val = "" Else val = cbTrack(i).SelectedItem.ToString
            SaveSetting(Application.ProductName, "Settings" & i, "Track", val)
            If cbLevel(i).SelectedItem Is Nothing Then val = "" Else val = cbLevel(i).SelectedItem.ToString
            SaveSetting(Application.ProductName, "Settings" & i, "Level", val)
        Next i
    End Sub

    Private Sub selectByName(cb As ComboBox, name As String)
        For i = 0 To cb.Items.Count - 1
            If cb.Items(i).ToString = name Then
                cb.SelectedIndex = i
                Exit Sub
            End If
        Next
    End Sub

    Private Sub frmMusic_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        cbSong.Focus()
    End Sub

    Private Sub frmMusic_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Not vocalOut Is Nothing Then
            If vocalOut.PlaybackState = NAudio.Wave.PlaybackState.Playing Then vocalOut.Stop()
            vocalOut.Dispose()
            vocalOut = Nothing
            vocalProvider = Nothing
            For Each vi As NAudio.Wave.WaveFileReader In vocalInputs
                vi.Close()
                vi.Dispose()
            Next
            For Each vf As IO.FileInfo In vocalFiles
                vf.Delete()
            Next
            vocalInputs = Nothing
        End If
    End Sub

    Private Sub frmMusic_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        basepath = IO.Path.GetDirectoryName(Reflection.Assembly.GetExecutingAssembly().Location) & "\GH\"
        cbTrack(0) = cbTrack0
        cbTrack(1) = cbTrack1
        cbTrack(2) = cbTrack2
        cbTrack(3) = cbTrack3
        cbLevel(0) = cbLevel0
        cbLevel(1) = cbLevel1
        cbLevel(2) = cbLevel2
        cbLevel(3) = cbLevel3

        arrGames(0) = New clsGame("Guitar Hero II", "GH2", enumGame.gmGH2)
        arrGames(1) = New clsGame("Guitar Hero III", "GH3", enumGame.gmGH3)
        arrGames(2) = New clsGame("Rock Band", "RB", enumGame.gmRB)
        arrGames(3) = New clsGame("Rock Band II", "RB2", enumGame.gmRB2)
        arrGames(4) = New clsGame("Rock Band Beatles", "RBB", enumGame.gmRBB)
        arrGames(5) = New clsGame("Lego Rock Band", "LRB", enumGame.gmLRB)

        For i As Integer = 0 To 3
            arrLevel(i) = New clsLevel(i)
        Next i

        Dim saved As String

        cbManual.Checked = GetSetting(Application.ProductName, "Settings", "Manual", "False") = "True"

        cbGame.Items.Clear()
        For Each game As clsGame In arrGames
            cbGame.Items.Add(game)
        Next
        saved = GetSetting(Application.ProductName, "Settings", "Game", "")
        If saved <> "" Then
            selectByName(cbGame, saved)
            cbGame_SelectedIndexChanged(cbGame, Nothing)
        End If
        AddHandler cbGame.SelectedIndexChanged, AddressOf cbGame_SelectedIndexChanged

        saved = GetSetting(Application.ProductName, "Settings", "Song", "")
        If saved <> "" Then
            selectByName(cbSong, saved)
            cbSong_SelectedIndexChanged(cbSong, Nothing)
        End If
        AddHandler cbSong.SelectedIndexChanged, AddressOf cbSong_SelectedIndexChanged

        For i As Integer = 0 To 3
            saved = GetSetting(Application.ProductName, "Settings" & i, "Track", "")
            If saved <> "" Then selectByName(cbTrack(i), saved)

            For Each level As clsLevel In arrLevel
                cbLevel(i).Items.Add(level)
            Next
            saved = GetSetting(Application.ProductName, "Settings" & i, "Level", "")
            If saved <> "" Then selectByName(cbLevel(i), saved)
        Next

    End Sub

    Private Sub cbGame_SelectedIndexChanged(sender As System.Object, e As System.EventArgs)
        If cbGame.SelectedItem Is lastGame Then Exit Sub
        lastGame = cbGame.SelectedItem
        populateSongs(cbSong, lastGame)
    End Sub

    Private Sub populateSongs(cb As ComboBox, game As clsGame)
        cb.Items.Clear()
        lstSongs.Clear()
        For Each fi As IO.FileInfo In New IO.DirectoryInfo(basepath & game.path).GetFiles("*.mid")
            Dim song As New clsSong(fi, game)
            lstSongs.Add(song)
            cb.Items.Add(song)
        Next
        cb.SelectedIndex = -1
    End Sub

    Private Sub cbSong_SelectedIndexChanged(sender As System.Object, e As System.EventArgs)
        If cbSong.SelectedItem Is lastSong Then Exit Sub
        lastSong = cbSong.SelectedItem
        For i As Integer = 0 To 3
            populateTracks(cbTrack(i), lastGame, lastSong)
        Next i
    End Sub

    Private Sub populateTracks(cb As ComboBox, game As clsGame, song As clsSong)
        Dim oldPart As String = vbNullString
        If Not cb.SelectedItem Is Nothing AndAlso Not cb.SelectedItem.ToString = "" Then
            oldPart = CType(cb.SelectedItem, clsTrack).name
        End If
        cb.Items.Clear()
        lstTracks.Clear()
        cb.Items.Add("")
        With song.mf
            For i As Integer = 0 To .Tracks - 1
                For j = 0 To .Events(i).Count - 1
                    If .Events(i)(j).CommandCode = NAudio.Midi.MidiCommandCode.MetaEvent Then
                        If CType(.Events(i)(j), NAudio.Midi.MetaEvent).MetaEventType = NAudio.Midi.MetaEventType.SequenceTrackName Then
                            Dim name As String = CType(.Events(i)(j), NAudio.Midi.TextEvent).Text
                            Dim match As System.Text.RegularExpressions.Match = System.Text.RegularExpressions.Regex.Match(name, "^PART (?<part>.*)$")
                            If match.Success Then
                                Dim track As New clsTrack(song.mf, i, match.Groups("part").Value, song)
                                lstTracks.Add(track)
                                cb.Items.Add(track)
                                Select Case match.Groups("part").Value
                                    Case "GUITAR", "BASS", "RHYTHM", "GUITAR COOP"
                                        track = New clsTrack(song.mf, i, match.Groups("part").Value & "[LH]", song)
                                        track.lefty = True
                                        lstTracks.Add(track)
                                        cb.Items.Add(track)
                                    Case Else
                                End Select
                            End If
                            Exit For
                        End If
                    End If
                Next j
            Next i
        End With
        cb.SelectedIndex = -1
        If oldPart <> vbNullString Then
            For i = 0 To cb.Items.Count - 1
                If cb.Items(i).ToString <> "" AndAlso CType(cb.Items(i), clsTrack).name = oldPart Then
                    cb.SelectedIndex = i
                    Exit For
                End If
            Next
        End If
    End Sub

    Private Sub btnCancel_Click(sender As System.Object, e As System.EventArgs) Handles btnCancel.Click
        actions = Nothing
        Me.Close()
    End Sub

    Private Class clsNoteAction
        Implements IComparable(Of clsNoteAction)
        Public controller As Byte
        Public msOffset As Integer
        Public noteMask As Integer
        Public press As Boolean
        Public comment As String

        Public Sub New(_controller As Byte, _note As Integer, _msOffset As Integer, _press As Boolean, _comment As String)
            controller = _controller
            noteMask = _note
            msOffset = _msOffset
            press = _press
            comment = _comment
        End Sub

        Public Function CompareTo(other As clsNoteAction) As Integer Implements System.IComparable(Of clsNoteAction).CompareTo
            If msOffset < other.msOffset Then Return -1
            If msOffset > other.msOffset Then Return 1
            If controller.CompareTo(other.controller) <> 0 Then Return controller.CompareTo(other.controller)
            If noteMask < other.noteMask Then Return -1
            If noteMask > other.noteMask Then Return 1
            If press <> other.press Then Return IIf(press, -1, 1)
            Return 0
        End Function
    End Class



    Private Sub btnOk_Click(sender As System.Object, e As System.EventArgs) Handles btnOk.Click
        saveSettings()
        Dim allNotes As New List(Of clsNoteEntry)
        For i = 0 To 3
            If (Not cbTrack(i).SelectedItem Is Nothing) AndAlso (Not cbTrack(i).SelectedItem.ToString() = "") AndAlso (Not cbLevel(i).SelectedItem Is Nothing) Then
                allNotes.AddRange(getNotes(i + 1, cbTrack(i).SelectedItem, cbLevel(i).SelectedItem))
            End If
        Next
        allNotes.Sort()

        Dim mergedNotes As New List(Of clsNoteEntry)
        Dim lastNote As clsNoteEntry = allNotes(0)
        mergedNotes.Add(lastNote)
        For i = 1 To allNotes.Count - 1
            If allNotes(i).controller = lastNote.controller AndAlso allNotes(i).tickOffset = lastNote.tickOffset Then 'AndAlso allNotes(i).tickDuration = lastNote.tickDuration Then
                lastNote.merge(allNotes(i))
            Else
                mergedNotes.Add(allNotes(i))
                lastNote = allNotes(i)
            End If
        Next

        'Dim manlines() As String = IO.File.ReadAllLines("g:\th\pony.hopoc")
        'For i = 0 To mergedNotes.Count - 1
        '    Dim line As String = vbNullString
        '    Dim track As clsTrack = cbTrack(0).SelectedItem
        '    With mergedNotes(i)
        '        If .noteMask And track.noteGreen(True) Then line = line & "A"
        '        If .noteMask And track.noteRed(True) Then line = line & "B"
        '        If .noteMask And track.noteYellow(True) Then line = line & "C"
        '        If .noteMask And track.noteBlue(True) Then line = line & "D"
        '        If .noteMask And track.noteOrange(True) Then line = line & "E"
        '        If .hopo Then line = line.ToLower
        '    End With
        '    Dim manline = manlines(i)
        '    If line.ToUpper <> manline.ToUpper Then Stop
        '    If line <> manline Then
        '        If manline.ToUpper = manline Then
        '            mergedNotes(i).noteMask = mergedNotes(i).noteMask Or track.strumButton
        '        Else
        '            mergedNotes(i).noteMask = mergedNotes(i).noteMask And Not track.strumButton
        '        End If
        '    End If
        '    Debug.Print(line)
        'Next i

        Dim noteActions As New List(Of clsNoteAction)
        For Each ne As clsNoteEntry In mergedNotes
            noteActions.Add(New clsNoteAction(ne.controller, ne.noteMask, ne.msOffset + lastGame.loadTime, True, ne.comment))
            noteActions.Add(New clsNoteAction(ne.controller, ne.noteMask, ne.msOffset + ne.msDuration + lastGame.loadTime, False, ne.comment))
        Next

        noteActions.Sort()

        actions = New List(Of clsAction)
        Dim curOffset As Integer
        Dim a As clsAction
        If cbManual.Checked Then
            curOffset = noteActions(0).msOffset
            Dim endMS As DateTime = (New DateTime).AddMilliseconds(noteActions(noteActions.Count - 1).msOffset)
            info = "[" & noteActions(0).controller & "] starts at " & ((noteActions(0).msOffset - CType(cbGame.SelectedItem, clsGame).loadTime) / 1000) & "s (of " & endMS.ToString("m:ss") & ")"
        Else
            curOffset = 100
            a = New clsActionPress(1, clsController.XBButtons.btnA, -1, -1, New Point(-129, -129), New Point(-129, -129), 100, 100, 1, Nothing)
            a.index = actions.Count
            actions.Add(a)
        End If
        Dim curmask As Integer = 0
        For i As Integer = 0 To noteActions.Count - 1
            With noteActions(i)
                Dim notemask As Integer = .noteMask And &HFFFF
                Dim LT As Integer = IIf(.noteMask And &H10000, 255, -1)
                Dim RT As Integer = IIf(.noteMask And &H20000, 255, -1)
                If .msOffset > curOffset Then
                    a = New clsActionWait(.msOffset - curOffset, Nothing)
                    a.index = actions.Count
                    actions.Add(a)
                    curOffset = .msOffset
                End If
                'If .press AndAlso curmask = 0 AndAlso i < noteActions.Count - 1 Then
                '    If (Not noteActions(i + 1).press) AndAlso noteActions(i + 1).noteMask = .noteMask Then
                '        Dim holdTime As Integer = noteActions(i + 1).msOffset - .msOffset
                '        Dim waitTime As Integer
                '        If i < noteActions.Count - 2 AndAlso noteActions(i + 2).press Then
                '            waitTime = noteActions(i + 2).msOffset - .msOffset
                '        Else
                '            waitTime = holdTime
                '        End If
                '        a = New clsActionPress(.controller, notemask, LT, RT, New Point(-129, -129), New Point(-129, -129), holdTime, waitTime, 1)
                '        a.index = actions.Count
                '        actions.Add(a)
                '        i = i + 1
                '        curOffset = curOffset + waitTime
                '    End If
                'Else
                If .press Then
                    curmask = curmask Or .noteMask
                    a = New clsActionHold(.controller, notemask, LT, RT, New Point(-129, -129), New Point(-129, -129), Nothing)
                    a.comment = .comment
                    a.index = actions.Count
                    actions.Add(a)
                Else
                    curmask = curmask And Not .noteMask
                    a = New clsActionRelease(.controller, notemask, LT, RT, New Point(-129, -129), New Point(-129, -129), Nothing)
                    a.comment = .comment
                    a.index = actions.Count
                    actions.Add(a)
                End If
                'End If
            End With
        Next
        a = New clsActionWait(15000, Nothing)
        a.index = actions.Count
        actions.Add(a)

        a = New clsActionPress(1, clsController.XBButtons.btnGuide, -1, -1, New Point(-129, -129), New Point(-129, -129), 500, 500, 1, Nothing)
        a.index = actions.Count
        actions.Add(a)

        Me.Close()
    End Sub

    Private vocalFiles As List(Of IO.FileInfo)
    Private vocalInputs As List(Of NAudio.Wave.WaveFileReader)
    Private vocalProvider As NAudio.Wave.MultiplexingWaveProvider
    Private vocalOut As NAudio.Wave.WasapiOut

    Private Sub btnVocal_Click(sender As System.Object, e As System.EventArgs) Handles btnVocal.Click
        If Not vocalOut Is Nothing Then
            Dim origState As NAudio.Wave.PlaybackState = vocalOut.PlaybackState
            If vocalOut.PlaybackState = NAudio.Wave.PlaybackState.Playing Then vocalOut.Stop()
            vocalOut.Dispose()
            vocalOut = Nothing
            vocalProvider = Nothing
            For Each vi As NAudio.Wave.WaveFileReader In vocalInputs
                vi.Close()
                vi.Dispose()
            Next
            For Each vf As IO.FileInfo In vocalFiles
                vf.Delete()
            Next
            vocalInputs = Nothing
            If origState = NAudio.Wave.PlaybackState.Playing Then Exit Sub
        End If
        Dim song As clsSong = cbSong.SelectedItem
        vocalFiles = modVocal.generateWAV(song.fi.FullName)
        vocalInputs = New List(Of NAudio.Wave.WaveFileReader)
        For Each vf As IO.FileInfo In vocalFiles
            vocalInputs.Add(New NAudio.Wave.WaveFileReader(vf.FullName))
        Next
        vocalProvider = New NAudio.Wave.MultiplexingWaveProvider(vocalInputs, vocalInputs.Count)
        For i As Integer = 0 To vocalInputs.Count - 1
            vocalProvider.ConnectInputToOutput(i, i)
        Next
        Dim devs As New NAudio.CoreAudioApi.MMDeviceEnumerator()
        Dim outdev As NAudio.CoreAudioApi.MMDevice = Nothing
        outdev = devs.GetDefaultAudioEndpoint(NAudio.CoreAudioApi.DataFlow.Render, NAudio.CoreAudioApi.Role.Multimedia)
        vocalOut = New NAudio.Wave.WasapiOut(outdev, NAudio.CoreAudioApi.AudioClientShareMode.Shared, True, 0)
        vocalOut.Init(vocalProvider)
        MsgBox("Vocals ready to go." & vbCrLf & "Hit enter to start playing!")
        vocalOut.Play()
    End Sub

    Private Sub btnStar_Click(sender As System.Object, e As System.EventArgs) Handles btnStar.Click
        Dim spInput(2) As NAudio.Wave.WaveFileReader
        For i As Integer = 0 To 2
            spInput(i) = New NAudio.Wave.WaveFileReader("g:\gh\noise.wav")
        Next
        Dim spProvider As New NAudio.Wave.MultiplexingWaveProvider(spInput, spInput.Length)
        For i As Integer = 0 To spInput.Length - 1
            spProvider.ConnectInputToOutput(i, i)
        Next
        Dim spOut As New NAudio.Wave.WasapiOut(NAudio.CoreAudioApi.AudioClientShareMode.Shared, 0)
        spOut.Init(spProvider)
        spOut.Play()
        While spOut.PlaybackState = NAudio.Wave.PlaybackState.Playing
            System.Threading.Thread.Sleep(50)
        End While
        spOut.Dispose()
        spOut = Nothing
        spProvider = Nothing
        For i As Integer = 0 To 2
            spInput(i).Close()
            spInput(i).Dispose()
        Next
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        For Each fi As IO.FileInfo In (New IO.DirectoryInfo("G:\AutoGH\AutoGH\bin\Debug\GH\LRB").GetFiles("*.mid"))
            If Not IO.File.Exists(fi.FullName.Substring(0, fi.FullName.Length - 4) & ".wav") Then modVocal.generateWAV(fi.FullName)
        Next
    End Sub
End Class