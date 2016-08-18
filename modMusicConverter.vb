
Friend Class clsSectionEntry
    Public tickStart As Integer
    Public tickEnd As Integer
    Public name As String

    Public Sub New(_tickstart As Integer, _name As String)
        tickStart = _tickstart
        name = _name
    End Sub

    Public Sub New(_tickstart As Integer, _name As String, prevSection As clsSectionEntry)
        tickStart = _tickstart
        name = _name
        tickEnd = Integer.MaxValue
        If Not prevSection Is Nothing Then
            prevSection.tickEnd = tickStart
        End If
    End Sub

End Class

Friend Class clsTempoEntry
    Public tickStart As Integer
    Public tickEnd As Integer
    Public usStart As Decimal
    Public rate As Decimal

    Public Sub New(newtickstart As Integer, newrate As Integer)
        tickStart = newtickstart
        rate = newrate
    End Sub

    Public Sub New(newtickstart As Integer, newrate As Decimal, prevTempo As clsTempoEntry)
        tickStart = newtickstart
        rate = newrate
        tickEnd = Integer.MaxValue
        If prevTempo Is Nothing Then
            usStart = 0
        Else
            Dim prevTicks As Integer = tickStart - prevTempo.tickStart
            Dim prevUS As Decimal = prevTicks * prevTempo.rate
            prevTempo.tickEnd = tickStart
            usStart = prevTempo.usStart + prevUS
        End If
    End Sub

End Class

Friend Class clsNoteEntry
    Implements IComparable(Of clsNoteEntry)
    Public controller As Byte
    Public tickOffset As Integer
    Public tickDuration As Integer
    Public msOffset As Integer
    Public msDuration As Integer
    Public noteValue As Integer
    Public noteMask As Integer
    Public hopo As Boolean = False
    Public solo As Boolean = False
    Public comment As String
    Public eventIndex As Integer
    Public section As String

    Private Function formatMS(ms As Integer) As String
        Dim t As New Date(10000 * CLng(ms))
        Return t.ToString("mm:ss." & t.Millisecond.ToString.PadLeft(3, "0"))
    End Function

    Public Sub New(_controller As String, _track As clsTrack, _level As clsLevel, _nev As NAudio.Midi.NoteOnEvent, lastnote As clsNoteEntry, dtpqn As Integer, _solo As Boolean, _eventIndex As Integer, _section As String)
        controller = _controller
        noteValue = _nev.NoteNumber - _level.baseNote
        tickOffset = _nev.AbsoluteTime
        tickDuration = _nev.NoteLength
        solo = _solo
        If Not lastnote Is Nothing AndAlso tickOffset - lastnote.tickOffset < dtpqn / 2 AndAlso tickOffset <> lastnote.tickOffset Then hopo = lastnote.noteValue <> noteValue
        hopo = False
        Select Case noteValue
            Case 0
                noteMask = _track.noteGreen(hopo, solo)
            Case 1
                noteMask = _track.noteRed(hopo, solo)
            Case 2
                noteMask = _track.noteYellow(hopo, solo)
            Case 3
                noteMask = _track.noteBlue(hopo, solo)
            Case 4
                noteMask = _track.noteOrange(hopo, solo)
        End Select
        eventIndex = _eventIndex
        section = _section
    End Sub

    Public Sub setComment()
        comment = section & "[" & eventIndex & "] " & formatMS(msOffset)
    End Sub

    Public Sub merge(ne As clsNoteEntry)
        noteMask = noteMask Or ne.noteMask
        hopo = False
    End Sub

    Public Function CompareTo(other As clsNoteEntry) As Integer Implements System.IComparable(Of clsNoteEntry).CompareTo
        If tickOffset < other.tickOffset Then Return -1
        If tickOffset > other.tickOffset Then Return 1
        If controller.CompareTo(other.controller) <> 0 Then Return controller.CompareTo(other.controller)
        If noteMask < other.noteMask Then Return -1
        If noteMask > other.noteMask Then Return 1
        Return 0
    End Function
End Class

Module modMusicConverter
    Public Function getNotes(controller As Byte, Track As clsTrack, Level As clsLevel) As List(Of clsNoteEntry)
        Dim baseNote As Integer = Level.baseNote
        Dim mf As NAudio.Midi.MidiFile = Track.mf
        Dim tempos As New List(Of clsTempoEntry)
        Dim tpq As Integer = mf.DeltaTicksPerQuarterNote
        Dim prevTempo As clsTempoEntry = Nothing
        Dim inSolo As Boolean = False
        For Each mev As NAudio.Midi.MidiEvent In mf.Events(0)
            If mev.CommandCode = NAudio.Midi.MidiCommandCode.MetaEvent AndAlso CType(mev, NAudio.Midi.MetaEvent).MetaEventType = NAudio.Midi.MetaEventType.SetTempo Then
                Dim tev As NAudio.Midi.TempoEvent = CType(mev, NAudio.Midi.TempoEvent)
                Dim tempo As New clsTempoEntry(tev.AbsoluteTime, tev.MicrosecondsPerQuarterNote / mf.DeltaTicksPerQuarterNote, prevTempo)
                tempos.Add(tempo)
                prevTempo = tempo
            End If
        Next

        Dim sections As New List(Of clsSectionEntry)
        Dim evtTrack As Integer = -1
        For i As Integer = 1 To mf.Tracks - 1
            Dim mev As NAudio.Midi.MidiEvent = mf.Events(i)(0)
            If mev.CommandCode = NAudio.Midi.MidiCommandCode.MetaEvent _
                AndAlso CType(mev, NAudio.Midi.MetaEvent).MetaEventType = NAudio.Midi.MetaEventType.SequenceTrackName _
                AndAlso CType(mev, NAudio.Midi.TextEvent).Text = "EVENTS" Then
                evtTrack = i
                Exit For
            End If
        Next

        Dim prevSection As clsSectionEntry = Nothing
        If evtTrack > 0 Then
            For i As Integer = 0 To mf.Events(evtTrack).Count - 1
                Dim mev As NAudio.Midi.MidiEvent = mf.Events(evtTrack)(i)
                If mev.CommandCode = NAudio.Midi.MidiCommandCode.MetaEvent _
                    AndAlso CType(mev, NAudio.Midi.MetaEvent).MetaEventType = NAudio.Midi.MetaEventType.TextEvent _
                    AndAlso CType(mev, NAudio.Midi.TextEvent).Text.StartsWith("[section ") Then
                    Dim sectName As String = CType(mev, NAudio.Midi.TextEvent).Text.Substring(9)
                    sectName = sectName.Substring(0, sectName.Length - 1)
                    Dim section As clsSectionEntry = New clsSectionEntry(mev.AbsoluteTime, sectName, prevSection)
                    sections.Add(section)
                    prevSection = section
                End If
            Next
        End If

        'Dim ringo As Boolean = True
        'Dim startTime As Integer = 0
        'Dim endTime As Integer = 0
        'If ringo Then
        '    Dim beatNumber As Integer = 86
        '    Dim bpm As Integer = 120
        '    Dim found As Boolean = False
        '    For Each mev As NAudio.Midi.MidiEvent In mf.Events(4)
        '        If mev.CommandCode = NAudio.Midi.MidiCommandCode.MetaEvent AndAlso CType(mev, NAudio.Midi.MetaEvent).MetaEventType = NAudio.Midi.MetaEventType.TextEvent Then
        '            Dim tev As NAudio.Midi.TextEvent = mev
        '            If found Then
        '                If tev.Text = "[end_drum_trainer_beat]" Then
        '                    endTime = tev.AbsoluteTime
        '                    Exit For
        '                End If
        '            Else
        '                If tev.Text = "[start_drum_trainer_beat drum_trainer_beat_" & beatNumber & "]" Then
        '                    found = True
        '                    startTime = tev.AbsoluteTime
        '                End If
        '            End If
        '        End If
        '    Next
        '    If startTime = 0 Or endTime = 0 Then Stop
        '    tempos(0).rate = (60000000 / CDec(bpm)) / mf.DeltaTicksPerQuarterNote

        'Dim beatstart As Integer = 0
        'For Each mev As NAudio.Midi.MidiEvent In mf.Events(4)
        '    If mev.CommandCode = NAudio.Midi.MidiCommandCode.MetaEvent AndAlso CType(mev, NAudio.Midi.MetaEvent).MetaEventType = NAudio.Midi.MetaEventType.TextEvent Then
        '        Dim tev As NAudio.Midi.TextEvent = mev
        '        If tev.Text.StartsWith("[start_drum_trainer_beat drum_trainer_beat_") Then beatstart = tev.AbsoluteTime
        '        If tev.Text.StartsWith("[bpm ") Then
        '            Dim bpm As Integer = tev.Text.Substring(5, tev.Text.Length - 6)
        '            Dim tempo As New clsTempoEntry(beatstart, (60000000 / CDec(bpm)) / mf.DeltaTicksPerQuarterNote, prevTempo)
        '            tempos.Add(tempo)
        '            prevTempo = tempo
        '            beatstart=0
        '        End If
        '        if text.text="[end_drum_trainer_beat]" and beatstart>0 then
        '            Dim bpm As Integer = 120
        '            Dim tempo As New clsTempoEntry(beatstart, (60000000 / CDec(bpm)) / mf.DeltaTicksPerQuarterNote, prevTempo)
        '            tempos.Add(tempo)
        '            prevTempo = tempo
        '            beatstart=0
        '           End if
        '    End If
        'Next
        'End If
        If sections.Count = 0 Then
            sections.Add(New clsSectionEntry(0, "Full"))
            sections(0).tickEnd = Integer.MaxValue
        End If

        Dim tmpNotes As New List(Of clsNoteEntry)
        Dim lastnote As clsNoteEntry = Nothing
        Dim forcestrum As Integer = 0
        Dim curSection As Integer = 0
        For i = 0 To mf.Events(Track.index).Count - 1
            Dim mev As NAudio.Midi.MidiEvent = mf.Events(Track.index)(i)
            While mev.AbsoluteTime >= sections(curSection).tickEnd
                curSection = curSection + 1
            End While
            Select Case mev.CommandCode
                Case NAudio.Midi.MidiCommandCode.MetaEvent
                    Dim meta As NAudio.Midi.MetaEvent = mev
                    If Track.name = "GUITAR" AndAlso meta.MetaEventType = NAudio.Midi.MetaEventType.TextEvent Then
                        Dim tev As NAudio.Midi.TextEvent = meta
                        Select Case tev.Text.Trim
                            Case "[play]", "[idle]", "[idle_realtime]", "[mellow]", "[intense]", "[idle_intense]"
                                inSolo = False
                            Case "[play_solo]"
                                'inSolo = True
                            Case Else
                                If Not tev.Text.StartsWith("[map ") Then Stop
                        End Select
                    End If
                Case NAudio.Midi.MidiCommandCode.NoteOn
                    Dim nev As NAudio.Midi.NoteOnEvent = CType(mev, NAudio.Midi.NoteOnEvent)
                    If nev.NoteNumber >= baseNote And nev.NoteNumber <= baseNote + 4 And nev.Velocity > 0 Then
                        Dim ne As clsNoteEntry = Nothing
                        ne = New clsNoteEntry(controller, Track, Level, nev, lastnote, mf.DeltaTicksPerQuarterNote, inSolo, i, sections(curSection).name)
                        'If forcestrum = ne.tickOffset And ne.hopo Then Stop
                        'If ringo Then
                        '    If ne.tickOffset >= startTime And ne.tickOffset <= endTime Then tmpNotes.Add(ne)
                        'Else
                        tmpNotes.Add(ne)
                        'End If
                        lastnote = ne
                        'If ne.hopo Then Debug.Print(tmpNotes.Count & " : " & nev.AbsoluteTime & " : " & ne.tickDuration)
                    End If
            End Select
        Next
        tmpNotes.Sort()
        Dim t As Integer = 0

        For Each ne As clsNoteEntry In tmpNotes
            While ne.tickOffset >= tempos(t).tickEnd
                t = t + 1
            End While
            ne.msOffset = Track._game.dilation * (tempos(t).usStart + tempos(t).rate * (ne.tickOffset - tempos(t).tickStart)) / 1000
            If ne.tickDuration + ne.tickOffset <= tempos(t).tickEnd Then
                ne.msDuration = ne.tickDuration * tempos(t).rate / 1000
            Else
                'If ne.tickDuration + ne.tickOffset > tempos(t + 1).tickEnd Then Stop
                ne.msDuration = (tempos(t).tickEnd - ne.tickOffset) * tempos(t).rate / 1000 + (ne.tickDuration + ne.tickOffset - tempos(t + 1).tickStart) * tempos(t + 1).rate / 1000
            End If
            ne.msDuration = ne.msDuration * Track._game.dilation - Track._game.truncation
            If ne.msDuration < Track._game.minimumDuration Then ne.msDuration = Track._game.minimumDuration
            ne.setComment()
        Next
        Return tmpNotes
    End Function


End Module
