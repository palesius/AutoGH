Module modVocal
    Private Class clsVoiceNoteEntry
        Implements IComparable(Of clsVoiceNoteEntry)
        Public tickOffset As Integer
        Public tickDuration As Integer
        Public msOffset As Integer
        Public msDuration As Integer
        Public noteVal As Integer
        Public frequency As Decimal

        Public Sub New(newnote As Integer, newTime As Integer, newduration As Integer)
            noteVal = newnote
            tickOffset = newTime
            tickDuration = newduration
        End Sub

        Public Function CompareTo(other As clsVoiceNoteEntry) As Integer Implements System.IComparable(Of clsVoiceNoteEntry).CompareTo
            If tickOffset < other.tickOffset Then Return -1
            If tickOffset > other.tickOffset Then Return 1
            If noteVal < other.noteVal Then Return -1
            If noteVal > other.noteVal Then Return 1
            Return 0
        End Function

    End Class

    Private Class trackInfo
        Public number As Integer
        Public name As String
        Public samples As List(Of Short)
        Private notes As List(Of clsVoiceNoteEntry)

        Public Sub New(_number As Integer, _name As String)
            number = _number
            name = _name
        End Sub

        Public Function startTime() As Integer
            Return notes(0).msOffset
        End Function

        Public Function endTime() As Integer
            Return notes(notes.Count - 1).msOffset + notes(notes.Count - 1).msDuration
        End Function

        Public Sub readnotes(mf As NAudio.Midi.MidiFile, tempos As List(Of clsTempoEntry))
            notes = New List(Of clsVoiceNoteEntry)
            If name = "BEAT" Then
                For i As Integer = mf.Events(number).Count - 1 To 0 Step -1
                    Dim mev As NAudio.Midi.MidiEvent = mf.Events(number)(i)
                    If mev.CommandCode = NAudio.Midi.MidiCommandCode.NoteOn Then
                        Dim nev As NAudio.Midi.NoteOnEvent = CType(mev, NAudio.Midi.NoteOnEvent)
                        If (nev.NoteNumber = 12 Or nev.NoteNumber = 13 Or nev.NoteNumber = 36 Or nev.NoteNumber = 37) Then
                            Dim ne As clsVoiceNoteEntry = Nothing
                            ne = New clsVoiceNoteEntry(nev.NoteNumber, nev.AbsoluteTime, 120)
                            notes.Add(ne)
                            Exit For
                        Else
                            Stop
                        End If
                    End If
                Next
            Else
                For Each mev As NAudio.Midi.MidiEvent In mf.Events(number)
                    If mev.CommandCode = NAudio.Midi.MidiCommandCode.NoteOn Then
                        Dim nev As NAudio.Midi.NoteOnEvent = CType(mev, NAudio.Midi.NoteOnEvent)
                        If nev.NoteNumber >= 16 And nev.NoteNumber <= 95 And nev.Velocity > 0 Then
                            If nev.NoteNumber <= 28 Or nev.NoteNumber >= 84 Then Stop
                            Dim ne As clsVoiceNoteEntry = Nothing
                            ne = New clsVoiceNoteEntry(nev.NoteNumber, nev.AbsoluteTime, nev.NoteLength)
                            notes.Add(ne)
                        End If
                    End If
                Next
                notes.Sort()
            End If


            Dim t As Integer = 0
            For Each ne As clsVoiceNoteEntry In notes
                While ne.tickOffset >= tempos(t).tickEnd
                    t = t + 1
                End While
                ne.msOffset = (tempos(t).usStart + tempos(t).rate * (ne.tickOffset - tempos(t).tickStart)) / 1000
                If ne.tickDuration + ne.tickOffset <= tempos(t).tickEnd Then
                    ne.msDuration = ne.tickDuration * tempos(t).rate / 1000
                Else
                    ne.msDuration = (tempos(t).tickEnd - ne.tickOffset) * tempos(t).rate / 1000 + (ne.tickDuration + ne.tickOffset - tempos(t + 1).tickStart) * tempos(t + 1).rate / 1000
                End If
                While ne.noteVal < 58
                    ne.noteVal = ne.noteVal + 12
                End While
                ne.frequency = 440 * 2 ^ ((ne.noteVal - 69) / 12)
                Debug.Print(ne.msOffset & "(" & ne.msDuration & "):" & ne.frequency)
            Next

            Dim spreadMS_Pre As Integer = 250
            Dim spreadMS_Post As Integer = 100
            For i = 0 To notes.Count - 1
                With notes(i)
                    If i > 0 Then
                        .msOffset = .msOffset - spreadMS_Pre
                        .msDuration = .msDuration + spreadMS_Post + spreadMS_Pre
                    Else
                        .msDuration = .msDuration + spreadMS_Post
                    End If
                End With
            Next
            For i = 0 To notes.Count - 2
                With notes(i)
                    If .msOffset + .msDuration > notes(i + 1).msOffset Then
                        'Dim overage As Integer = .msOffset + .msDuration - tmpNotes(i + 1).msOffset
                        '.msDuration = .msDuration - overage * (spreadMS_Post / (spreadMS_Pre + spreadMS_Post))
                        'tmpNotes(i + 1).msOffset = tmpNotes(i + 1).msOffset + overage * (spreadMS_Pre / (spreadMS_Pre + spreadMS_Post))
                        .msDuration = notes(i + 1).msOffset - .msOffset
                    End If
                End With
            Next
        End Sub

        Public Sub generateSamples(startoffset As Integer, endoffset As Integer)
            samples = New List(Of Short)
            Dim silence As Integer
            For Each ne As clsVoiceNoteEntry In notes
                silence = Math.Floor((ne.msOffset - startoffset) * 44.1) - samples.Count
                If silence > 0 Then samples.AddRange(generateSilence(silence))
                If ne.msDuration > 0 Then samples.AddRange(generateTone(ne.frequency, ne.msDuration * 44.1))
            Next
            silence = Math.Floor((endoffset - startoffset) * 44.1) - samples.Count
            If silence > 0 Then samples.AddRange(generateSilence(silence))
        End Sub
    End Class

    Function generateWAV(midipath As String) As List(Of IO.FileInfo)
        Dim mf As New NAudio.Midi.MidiFile(midipath)
        Dim tracks As New List(Of trackInfo)
        Dim beat As trackInfo = Nothing

        With mf
            For i As Integer = 0 To .Tracks - 1
                For j = 0 To .Events(i).Count - 1
                    If .Events(i)(j).CommandCode = NAudio.Midi.MidiCommandCode.MetaEvent Then
                        If CType(.Events(i)(j), NAudio.Midi.MetaEvent).MetaEventType = NAudio.Midi.MetaEventType.SequenceTrackName Then
                            Dim name As String = CType(.Events(i)(j), NAudio.Midi.TextEvent).Text
                            Select Case name
                                Case "PART VOCALS" ', "PART HARM1", "PART HARM2", "PART HARM3"
                                    tracks.Add(New trackInfo(i, name.Substring(5)))
                                    Exit For
                                Case "BEAT"
                                    beat = New trackInfo(i, "BEAT")
                            End Select
                        End If
                    End If
                Next j
            Next i
        End With

        If tracks.Count > 1 Then
            For Each t As trackInfo In tracks
                If t.name = "VOCALS" Then
                    tracks.Remove(t)
                    Exit For
                End If
            Next
        End If

        Dim tempos As New List(Of clsTempoEntry)
        Dim tpq As Integer = mf.DeltaTicksPerQuarterNote
        Dim prevTempo As clsTempoEntry = Nothing
        For Each mev As NAudio.Midi.MidiEvent In mf.Events(0)
            If mev.CommandCode = NAudio.Midi.MidiCommandCode.MetaEvent AndAlso CType(mev, NAudio.Midi.MetaEvent).MetaEventType = NAudio.Midi.MetaEventType.SetTempo Then
                Dim tev As NAudio.Midi.TempoEvent = CType(mev, NAudio.Midi.TempoEvent)
                Dim tempo As New clsTempoEntry(tev.AbsoluteTime, tev.MicrosecondsPerQuarterNote / mf.DeltaTicksPerQuarterNote, prevTempo)
                tempos.Add(tempo)
                prevTempo = tempo
            End If
        Next

        Dim startoffset As Integer = Integer.MaxValue
        For Each track As trackInfo In tracks
            track.readnotes(mf, tempos)
            startoffset = Math.Min(startoffset, track.startTime)
        Next

        beat.readnotes(mf, tempos)

        'startoffset = 0
        Dim endoffset As Integer = beat.endTime

        Dim files As New List(Of IO.FileInfo)
        For Each track As trackInfo In tracks
            track.generateSamples(startoffset, endoffset)
            Dim filename As String
            If track.name = "VOCALS" Then
                filename = midipath.Substring(0, InStrRev(midipath, ".") - 1) & ".wav"
            Else
                filename = midipath.Substring(0, InStrRev(midipath, ".") - 1) & "-" & track.name & ".wav"
            End If
            saveWav(track.samples.ToArray(), filename)
            files.Add(New IO.FileInfo(filename))
        Next

        Return files
    End Function

    Function generateTone(freq As Decimal, samples As Integer) As Short()
        Dim tmp(samples - 1) As Short
        Dim t As Decimal = (Math.PI * 2 * freq) / 44100
        For i As Integer = 0 To samples - 1
            tmp(i) = Convert.ToInt16(26208 * Math.Sin(t * i))
        Next
        Return tmp
    End Function

    Private Function generateSilence(samples As Integer) As Short()
        Dim tmp(samples - 1) As Short
        For i = 0 To samples - 1
            tmp(i) = 0
        Next
        Return tmp
    End Function

    Private Sub saveWav(samples() As Short, path As String)
        Dim fs As New IO.FileStream(path, IO.FileMode.Create)
        Dim bw As New IO.BinaryWriter(fs)

        Const sGroupID As String = "RIFF"
        Dim dwFileLength As UInt32 = samples.Length * 2 + 36
        Dim dwDataChunkSize As UInt32 = 0

        Const sRiffType As String = "WAVE"
        Const sFormatChunkID As String = "fmt "
        Const dwFormatChunkSize As UInt32 = 16
        Const wFormatTag As UInt16 = 1
        Const wChannels As UInt16 = 1
        Const dwSamplesPerSec As UInt32 = 44100
        Const wBitsPerSample As UInt16 = 16
        Const wBlockAlign As UInt16 = CType(wChannels * (wBitsPerSample / 8), UInt16)
        Const dwAvgBytesPerSec As UInt32 = dwSamplesPerSec * wBlockAlign

        Const sDataChunkID As String = "data"

        dwDataChunkSize = samples.Length * (wBitsPerSample / 8)

        'header
        bw.Write(sGroupID.ToCharArray())
        bw.Write(dwFileLength)
        bw.Write(sRiffType.ToCharArray())

        'format
        bw.Write(sFormatChunkID.ToCharArray())
        bw.Write(dwFormatChunkSize)
        bw.Write(wFormatTag)
        bw.Write(wChannels)
        bw.Write(dwSamplesPerSec)
        bw.Write(dwAvgBytesPerSec)
        bw.Write(wBlockAlign)
        bw.Write(wBitsPerSample)

        'Data
        bw.Write(sDataChunkID.ToCharArray())
        bw.Write(dwDataChunkSize)
        For Each s As Short In samples
            bw.Write(s)
        Next

        bw.Close()
        fs.Close()
    End Sub

End Module
