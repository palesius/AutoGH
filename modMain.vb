'TODO: Upon compile show total time next to groups and in "ready" box
'TODO: Allow cancel option after compile
'TODO: Allow option to start at a particular place, and/or just run a selection (including just running a single group

Module modMain
    Private controller As clsController

    Sub main()
        test()
        'makeTone()
        'generateAllXMLMP3("\\winterfell\g$\grease\")
        'Dim x As New Melanchall.DryWetMidi.Core.MidiFile()
        'x = Melanchall.DryWetMidi.Core.MidiFile.Read("c:\users\pales\Downloads\arterialblack.mid")
        'Stop

        Dim frm As New frmEdit
        'Dim frm As New frmMusic
        frm.Show()
        frm.Activate()

        Application.Run(frm)
        End
        'Dim gp(2, 3) As clsGuitarPart
        'Dim x As New NAudio.Midi.MidiFile("g:\Netduino\ripxbdvd\freebird.mid")
        'Dim x As New NAudio.Midi.MidiFile("g:\Netduino\ripxbdvd\trogdor.mid")
        'Dim x As New NAudio.Midi.MidiFile("G:\Netduino\ripxbdvd\gh3\1.1 Slow Ride - Foghat\notes.mid")
        'Stop
        'For p As Integer = 0 To 2
        '    For d As Integer = 0 To 3
        '        gp(p, d) = New clsGuitarPart(x, p, d)
        '        Debug.Print(p & "," & d & ": " & gp(p, d).notes.Count)
        '    Next
        'Next
        'If controller Is Nothing Then controller = New clsController("COM7")

        '60-64 easy
        '72-76 med
        '84-88 hard
        '96-100 expert
        'gp(1, 0).play(controller)

        Stop
        End

        'End
        'Dim frmC1 As New frmController(controller)
        'Dim frmC2 As New frmController(controller)
        'Dim frmV As New frmVideo(controller)
        'frmC.Show()
        'frmV.Show()
        'frmC.Location = New Point(frmV.Location.X + (frmV.Width - frmC.Width) / 2, frmV.Location.Y + frmV.Height)
        'Application.Run(frmC)
    End Sub
End Module
