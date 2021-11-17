Imports NAudio.Wave

Public Class frmAudio

    Private channelTest As Integer = -1
    Private channelTestEnd As Date = #1/1/1900#
    Private sndIn As New List(Of NAudio.Wave.IWaveProvider)

    Private ao As AsioOut = Nothing

    Private Sub frmAudio_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not AsioOut.isSupported Then
            MsgBox("Multi-channel output requires an ASIO sound driver." & vbCrLf &
                   "If your native driver does not provide an ASIO interface" & vbCrLf &
                   "ASIO4ALL provides a free generic ASIO driver that will work" & vbCrLf &
                   "with most soundcards. A Link has been copied to the clipboard.")
            Clipboard.SetText("https://www.asio4all.org/")
            Exit Sub
        End If
        OM.Add(New OMControls(1, lblOMChannel, btnOMTone, cmbOMPart, gbMappings, Me))
        Dim curDriver As String = GetSetting(Application.ProductName, "AudioSettings", "Driver", vbNullString)
        cmbDriver.Items.Clear()
        For Each drvName As String In AsioOut.GetDriverNames
            cmbDriver.Items.Add(drvName)
            If drvName = curDriver Then cmbDriver.SelectedItem = drvName
        Next
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        If cmbDriver.SelectedItem <> vbNullString Then
            SaveSetting(Application.ProductName, "AudioSettings", "Driver", cmbDriver.SelectedItem)
            For Each omc As OMControls In OM
                Dim part As String = omc.cmbPart.SelectedItem
                If part = "None" Then part = vbNullString
                SaveSetting(Application.ProductName, "AudioSettings", "Part" & omc.channel, omc.cmbPart.SelectedItem)
            Next
        End If

        Me.Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub cmbDriver_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbDriver.SelectedValueChanged
        If cmbDriver.SelectedItem = vbNullString Then
            btnTestAll.Enabled = False
            txtChannels.Text = vbNullString
            refreshChannels(0)
        Else
            If Not ao Is Nothing Then ao.Dispose()
            ao = New AsioOut(cmbDriver.SelectedItem.ToString)
            Dim channelCount As Integer = ao.DriverOutputChannelCount
            txtChannels.Text = channelCount
            btnTestAll.Enabled = True
            If channelCount > 8 Then channelCount = 8
            refreshChannels(channelCount)
        End If
    End Sub

    Private Class OMControls
        Implements IDisposable
        Public lblChannel As Label
        Public btnTone As Button
        Public cmbPart As ComboBox
        Public channel As Integer
        Public gb As GroupBox
        Private frm As frmAudio

        Public Sub New(_channel As Integer, _lblChannel As Label, _btnTone As Button, _cmbPart As ComboBox, _gb As GroupBox, _frm As frmAudio)
            channel = _channel
            lblChannel = _lblChannel
            btnTone = _btnTone
            cmbPart = _cmbPart
            gb = _gb
            frm = _frm
            setup()
        End Sub
        Public Sub New(_channel As Integer, template As OMControls, _gb As GroupBox, _frm As frmAudio)
            channel = _channel
            gb = _gb
            frm = _frm

            lblChannel = New Label
            lblChannel.AutoSize = True
            lblChannel.Location = New Drawing.Point(template.lblChannel.Left, template.lblChannel.Top + (channel - 1) * 27)
            lblChannel.Name = "lblOMChannel"
            lblChannel.Size = New System.Drawing.Size(13, 13)
            lblChannel.TabIndex = 0
            lblChannel.Text = "1"

            btnTone = New Button
            btnTone.Enabled = True
            btnTone.Location = New Drawing.Point(template.btnTone.Left, template.btnTone.Top + (channel - 1) * 27)
            btnTone.Name = "btnOMTone"
            btnTone.Size = New System.Drawing.Size(98, 23)
            btnTone.TabIndex = 1
            btnTone.Text = "Start Tone"
            btnTone.UseVisualStyleBackColor = True

            cmbPart = New ComboBox
            cmbPart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            cmbPart.Enabled = True
            cmbPart.FormattingEnabled = True
            cmbPart.Items.AddRange(New Object() {"None", "1", "2", "3"})
            cmbPart.Location = New Drawing.Point(template.cmbPart.Left, template.cmbPart.Top + (channel - 1) * 27)
            cmbPart.Name = "cmbOMPart"
            cmbPart.Size = New System.Drawing.Size(65, 21)
            cmbPart.TabIndex = 2

            gb.Controls.Add(lblChannel)
            gb.Controls.Add(btnTone)
            gb.Controls.Add(cmbPart)

            setup()
        End Sub

        Private Sub setup()
            lblChannel.Text = channel
            AddHandler btnTone.Click, AddressOf Me.testTone
            Dim curPart As String = GetSetting(Application.ProductName, "AudioSettings", "Part" & channel, vbNullString)
            If curPart <> vbNullString Then cmbPart.SelectedItem = curPart Else cmbPart.SelectedItem = "None"
        End Sub

        Public Sub dispose() Implements IDisposable.Dispose
            If channel > 1 Then
                gb.Controls.Remove(lblChannel)
                gb.Controls.Remove(btnTone)
                gb.Controls.Remove(cmbPart)
                lblChannel.Dispose()
                btnTone.Dispose()
                cmbPart.Dispose()
            End If
        End Sub

        Private Sub testTone(sender As Object, e As EventArgs)
            If frm.channelTest = channel Then
                btnTone.Text = "Start Tone"
                frm.channelTest = -1
            Else
                btnTone.Text = "Stop Tone"
                frm.channelTest = channel
                frm.tmrASIO.Enabled = True
            End If
        End Sub
    End Class

    Private OM As New List(Of OMControls)

    Private Sub refreshChannels(count As Integer)
        Me.SuspendLayout()
        gbMappings.SuspendLayout()

        Dim remove As New List(Of OMControls)
        If count = 0 Then
            'get rid of all past 1
            gbMappings.Visible = False
            btnOK.Top = gbDriver.Bottom + 6
            btnCancel.Top = gbDriver.Bottom + 6
        Else
            Dim maxChannel As Integer = 0
            For Each omc As OMControls In OM
                maxChannel = Math.Max(maxChannel, omc.channel)
                If omc.channel > 1 And omc.channel > count Then remove.Add(omc) Else omc.btnTone.Enabled = True
            Next
            For Each omc As OMControls In remove
                omc.dispose()
                OM.Remove(omc)
            Next
            For i = maxChannel + 1 To count
                OM.Add(New OMControls(i, OM(0), gbMappings, Me))
            Next
            gbMappings.Visible = True
            gbMappings.Height = OM(OM.Count - 1).btnTone.Bottom + 6
            btnOK.Top = gbMappings.Bottom + 6
            btnCancel.Top = gbMappings.Bottom + 6
        End If
        Me.ClientSize = New Drawing.Size(Me.ClientSize.Width, btnOK.Bottom + 15)

        gbMappings.ResumeLayout(False)
        gbMappings.PerformLayout()
        Me.ResumeLayout(False)
    End Sub

    Private Sub frmAudio_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        If Not ao Is Nothing Then
            If ao.PlaybackState <> PlaybackState.Stopped Then ao.Stop()
            ao.Dispose()
        End If
    End Sub

    Private Sub tmrASIO_Tick(sender As Object, e As EventArgs) Handles tmrASIO.Tick
        If Not ao Is Nothing Then
            If ao.PlaybackState <> PlaybackState.Stopped Then
                If Now > channelTestEnd Then
                    ao.Stop()
                Else
                    Exit Sub
                End If
            End If
            ao.Dispose()
            ao = Nothing
        End If
        For Each wp As NAudio.Wave.IWaveProvider In sndIn
            If wp.GetType.Name = "MediaFoundationReader" Then
                Dim mfr As MediaFoundationReader = wp
                mfr.Close()
                mfr.Dispose()
            End If
        Next
        sndIn.Clear()
        ao = New AsioOut(cmbDriver.SelectedItem.ToString)
        Select Case channelTest
            Case 0
                testAllChannels()
            Case 1 To 8
                testSingleChannel(channelTest)
            Case Else
                tmrASIO.Enabled = False
        End Select
    End Sub

    Private Sub testAllChannels()
        Dim channels As Integer = ao.DriverOutputChannelCount
        Dim maxMSLength As Integer = 0
        For i As Integer = 1 To channels
            Dim mfr As New NAudio.Wave.MediaFoundationReader("audio\" & i & ".mp3")
            sndIn.Add(mfr)
            Dim curMSLength As Integer = 1000 * mfr.Length / mfr.WaveFormat.AverageBytesPerSecond
            maxMSLength = Math.Max(maxMSLength, curMSLength)
        Next
        Dim mwp As New NAudio.Wave.MultiplexingWaveProvider(sndIn, channels)
        For i As Integer = 0 To channels - 1
            mwp.ConnectInputToOutput(i, i)
        Next

        ao.Init(mwp)
        channelTestEnd = Now.AddMilliseconds(maxMSLength)
        ao.Play()
    End Sub

    Private Sub testSingleChannel(idx As Integer)
        Dim channels As Integer = ao.DriverOutputChannelCount
        Dim mfr As New NAudio.Wave.MediaFoundationReader("audio\tone.mp3")
        sndIn.Add(mfr)
        sndIn.Add(New SilenceProvider(mfr.WaveFormat))
        Dim maxMSLength As Integer = 1000 * mfr.Length / mfr.WaveFormat.AverageBytesPerSecond
        Dim mwp As New NAudio.Wave.MultiplexingWaveProvider(sndIn, channels)
        For i As Integer = 0 To channels - 1
            If i = idx - 1 Then
                mwp.ConnectInputToOutput(0, i)
            Else
                mwp.ConnectInputToOutput(1, i)
            End If
        Next

        ao.Init(mwp)
        channelTestEnd = Now.AddMilliseconds(maxMSLength)
        ao.Play()
    End Sub

    Private Sub btnTestAll_Click(sender As Object, e As EventArgs) Handles btnTestAll.Click
        If channelTest = 0 Then
            btnTestAll.Text = "Test All"
            channelTest = -1
        Else
            btnTestAll.Text = "Stop Test"
            channelTest = 0
            tmrASIO.Enabled = True
        End If
    End Sub
End Class