Imports System.Text.RegularExpressions

Public Class frmText
	Public actions As List(Of clsAction)

	Private lstName As New List(Of String)(New String() {"Up", "Right", "Down", "Left", "A", "B", "X", "Y", "LB", "RB", "LT", "RT", "LS", "RS", "Back", "Start", "Guide", "LU", "LR", "LD", "LL", "RU", "RR", "RD", "RL", "Pause"})
	Private lstMask As New List(Of Integer)(New Integer() {&H100, &H800, &H200, &H400, &H10, &H20, &H40, &H80, &H1, &H2, -1, -2, &H4000, &H8000, &H2000, &H1000, &H4, -3, -4, -5, -6, -7, -8, -9, -10, -11})
	Private lstTxtButton As List(Of TextBox)
	Private lstTxtPress As List(Of TextBox)
	Private lstTxtWait As List(Of TextBox)

	Public Sub New()

		' This call is required by the designer.
		InitializeComponent()

		' Add any initialization after the InitializeComponent() call.
		lstTxtButton = New List(Of TextBox)(New TextBox() {txtDU, txtDR, txtDD, txtDL, txtA, txtB, txtX, txtY, txtLB, txtRB, txtLT, txtRT, txtLS, txtRS, txtBack, txtStart, txtGuide, txtLU, txtLR, txtLD, txtLL, txtRU, txtRR, txtRD, txtRL, txtPause})
		lstTxtPress = New List(Of TextBox)(New TextBox() {txtDUPress, txtDRPress, txtDDPress, txtDLPress, txtAPress, txtBPress, txtXPress, txtYPress, txtLBPress, txtRBPress, txtLTPress, txtRTPress, txtLSPress, txtRSPress, txtBackPress, txtStartPress, txtGuidePress, txtLUPress, txtLRPress, txtLDPress, txtLLPress, txtRUPress, txtRRPress, txtRDPress, txtRLPress, Nothing})
		lstTxtWait = New List(Of TextBox)(New TextBox() {txtDUWait, txtDRWait, txtDDWait, txtDLWait, txtAWait, txtBWait, txtXWait, txtYWait, txtLBWait, txtRBWait, txtLTWait, txtRTWait, txtLSWait, txtRSWait, txtBackWait, txtStartWait, txtGuideWait, txtLUWait, txtLRWait, txtLDWait, txtLLWait, txtRUWait, txtRRWait, txtRDWait, txtRLWait, txtPauseWait})

		For Each txt As TextBox In lstTxtPress
			If Not txt Is Nothing Then AddHandler txt.Validating, AddressOf txtTime_Validation
		Next
		For Each txt As TextBox In lstTxtWait
			If Not txt Is Nothing Then AddHandler txt.Validating, AddressOf txtTime_Validation
		Next
		loadSettings()
	End Sub

	Private Sub txtTime_Validation(sender As Object, e As System.ComponentModel.CancelEventArgs)
		If sender.text = vbNullString Then Exit Sub
		Dim ms As Long = unformatMS(sender.text)
		If ms = -1 Then
			e.Cancel = True
			MsgBox("You may only enter times in this field." & vbCrLf & "Valid formats are:" & vbCrLf &
					"1:02:03.004" & vbCrLf &
					"62:03.004" & vbCrLf &
					"3723.004" & vbCrLf &
					"1h2m3s4ms" & vbCrLf &
					"62m3s4ms" & vbCrLf &
					"3723s4ms" & vbCrLf &
					"3723004ms" & vbCrLf)
		End If
		sender.text = formatMS(ms)
	End Sub

	Private Enum enumTextButtons
		etbUp
		etbRight
		etbDown
		etbLeft
		etbA
		etbB
		etbX
		etbY
		etbLB
		etbRB
		etbLT
		etbRT
		etbLS
		etbRS
		etbBack
		etbStart
		etbGuide
		etbLU
		etbLR
		etbLD
		etbLL
		etbRU
		etbRR
		etbRD
		etbRL
		etbPause
	End Enum

	Private Sub saveSettings()
		SaveSetting(Application.ProductName, "TTS", "Press_Default", txtDefaultPress.Text)
		SaveSetting(Application.ProductName, "TTS", "Wait_Default", txtDefaultWait.Text)
		For i As Integer = enumTextButtons.etbUp To enumTextButtons.etbPause
			If Not lstTxtButton(i) Is Nothing Then SaveSetting(Application.ProductName, "TTS", "Text_" & lstName(i), lstTxtButton(i).Text)
			If Not lstTxtPress(i) Is Nothing Then SaveSetting(Application.ProductName, "TTS", "Press_" & lstName(i), lstTxtPress(i).Text)
			If Not lstTxtWait(i) Is Nothing Then SaveSetting(Application.ProductName, "TTS", "Wait_" & lstName(i), lstTxtWait(i).Text)
		Next
		SaveSetting(Application.ProductName, "TTS", "Delim_None", IIf(chkDelNone.Checked, "True", "False"))
		SaveSetting(Application.ProductName, "TTS", "Delim_Comma", IIf(chkDelComma.Checked, "True", "False"))
		SaveSetting(Application.ProductName, "TTS", "Delim_Tab", IIf(chkDelTab.Checked, "True", "False"))
		SaveSetting(Application.ProductName, "TTS", "Delim_Space", IIf(chkDelSpace.Checked, "True", "False"))
		SaveSetting(Application.ProductName, "TTS", "Delim_Line", IIf(chkDelLine.Checked, "True", "False"))
		SaveSetting(Application.ProductName, "TTS", "Delim_Other", IIf(chkDelOther.Checked, "True", "False"))
		SaveSetting(Application.ProductName, "TTS", "Delim_Other_Text", txtDelOther.Text)
		SaveSetting(Application.ProductName, "TTS", "Number_Suffix", IIf(chkNumberSuffix.Checked, "True", "False"))
	End Sub

	Private Sub loadSettings()
		txtDefaultPress.Text = GetSetting(Application.ProductName, "TTS", "Press_Default", "150ms")
		txtDefaultWait.Text = GetSetting(Application.ProductName, "TTS", "Wait_Default", "500ms")
		For i As Integer = enumTextButtons.etbUp To enumTextButtons.etbPause
			If Not lstTxtButton(i) Is Nothing Then lstTxtButton(i).Text = GetSetting(Application.ProductName, "TTS", "Text_" & lstName(i), vbNullString)
			If Not lstTxtPress(i) Is Nothing Then lstTxtPress(i).Text = GetSetting(Application.ProductName, "TTS", "Press_" & lstName(i), vbNullString)
			If Not lstTxtWait(i) Is Nothing Then lstTxtWait(i).Text = GetSetting(Application.ProductName, "TTS", "Wait_" & lstName(i), vbNullString)
		Next
		chkDelNone.Checked = GetSetting(Application.ProductName, "TTS", "Delim_None", "True") = "True"
		chkDelComma.Checked = GetSetting(Application.ProductName, "TTS", "Delim_Comma", "True") = "True"
		chkDelTab.Checked = GetSetting(Application.ProductName, "TTS", "Delim_Tab", "True") = "True"
		chkDelSpace.Checked = GetSetting(Application.ProductName, "TTS", "Delim_Space", "True") = "True"
		chkDelLine.Checked = GetSetting(Application.ProductName, "TTS", "Delim_Line", "True") = "True"
		chkDelOther.Checked = GetSetting(Application.ProductName, "TTS", "Delim_Other", "False") = "True"
		txtDelOther.Text = GetSetting(Application.ProductName, "TTS", "Delim_Other_Text", vbNullString)
		chkNumberSuffix.Checked = GetSetting(Application.ProductName, "TTS", "Number_Suffix", "True") = "True"
	End Sub

	Private Sub btnPreset_Click(sender As Object, e As EventArgs) Handles btnPreset.Click
		If cmbPreset.SelectedItem Is Nothing Then Exit Sub
		Dim presets As New Dictionary(Of String, List(Of String))
		presets.Add("TA Icons", New List(Of String)(New String() {"cn_up", "cn_right", "cn_down", "cn_left", "cn_A", "cn_B", "cn_X", "cn_Y", "cn_LB", "cn_RB", "cn_LT", "cn_RT", "cn_LS", "cn_RS", "cn_back", "cn_start", "cn_guide", "cn_LSu", "cn_LSr", "cn_LSd", "cn_LSl", "cn_RSu", "cn_RSr", "cn_RSd", "cn_RSl", ""}))
		presets.Add("Text (One Char)", New List(Of String)(New String() {"U", "R", "D", "L", "A", "B", "X", "Y", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""}))
		presets.Add("Text (Multi Char)", New List(Of String)(New String() {"Up", "Right", "Down", "Left", "A", "B", "X", "Y", "LB", "RB", "LT", "RT", "LS", "RS", "Back", "Start", "Guide", "LU", "LR", "LD", "LL", "RU", "RR", "RD", "RL", "Pause"}))

		Dim preset As List(Of String) = Nothing
		If Not presets.TryGetValue(cmbPreset.SelectedItem, preset) Then Exit Sub
		For i As Integer = enumTextButtons.etbUp To enumTextButtons.etbPause
			If Not lstTxtButton(i) Is Nothing Then lstTxtButton(i).Text = preset(i)
		Next
	End Sub

	Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
		Me.Close()
	End Sub

	Private Sub btnGenerate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click
		saveSettings()
		Dim tokens As New Dictionary(Of String, Integer)
		Dim txtActions As New List(Of Integer)
		Dim rptActions As New List(Of Integer)
		Dim src As String = txtInput.Text

		Dim invalid As New HashSet(Of String)

		If chkDelNone.Checked Then
			For i As Integer = enumTextButtons.etbUp To enumTextButtons.etbPause
				If Not lstTxtButton Is Nothing Then
					Select Case lstTxtButton(i).Text.Length
						Case 0
						Case 1
							If tokens.ContainsKey(lstTxtButton(i).Text) Then
								MsgBox("Button values must be unique.")
								Exit Sub
							End If
							tokens.Add(lstTxtButton(i).Text, i)
						Case Else
							MsgBox("Delimeters must be used when buttons are represented by more than one character.")
							Exit Sub
					End Select
				End If
			Next

			For i As Integer = 0 To src.Length - 1
				Dim action As Integer = -1
				Dim token As String = src.Substring(i, 1)
				If tokens.TryGetValue(token, action) Then
					txtActions.Add(action)
					rptActions.add(1)
				Else
					Select Case token
						Case " ", vbCr, vbLf, vbTab
						Case Else
							If Not invalid.Contains(token) Then invalid.Add(token)
					End Select
				End If
			Next
		Else
			For i As Integer = enumTextButtons.etbUp To enumTextButtons.etbPause
				If (Not lstTxtButton(i) Is Nothing) AndAlso lstTxtButton(i).Text <> vbNullString Then
					If tokens.ContainsKey(lstTxtButton(i).Text) Then
						MsgBox("Button values must be unique.")
						Exit Sub
					End If
					tokens.Add(lstTxtButton(i).Text, i)
				End If
			Next

			Dim delims As New List(Of Char)
			If chkDelComma.Checked Then delims.Add(",")
			If chkDelSpace.Checked Then delims.Add(" ")
			If chkDelTab.Checked Then delims.Add("	")
			If chkDelLine.Checked Then delims.Add(vbCr)
			If chkDelLine.Checked Then delims.Add(vbLf)
			If chkDelOther.Checked Then
				For i = 0 To txtDelOther.Text.Length - 1
					delims.Add(txtDelOther.Text.Substring(i, 1))
				Next
			End If
			If delims.Count = 0 Then
				MsgBox("You must select at least one delimiter.")
				Exit Sub
			End If
			Dim segments() As String = src.Split(delims.ToArray(), StringSplitOptions.RemoveEmptyEntries)
			Dim reRepeat As New Regex("^(?<token>\D+)(?<repeat>\d+)$", RegexOptions.Compiled)

			For Each segment As String In segments
				Dim action As Integer = -1
				Dim repeat As Integer = 1
				If chkNumberSuffix.Checked Then
					Dim m As Match = reRepeat.Match(segment)
					If m.Success Then
						segment = m.Groups("token").Value
						repeat = m.Groups("repeat").Value
					End If
				End If

				If tokens.TryGetValue(segment, action) Then
					txtActions.Add(action)
					rptActions.Add(repeat)
				Else
					If Not invalid.Contains(segment) Then invalid.Add(segment)
				End If
			Next
		End If

		If invalid.Count > 0 Then
			Dim sbInvalid As New Text.StringBuilder
			sbInvalid.AppendLine("The following button names in the source were invalid and were ignored:")
			For Each i As String In invalid
				sbInvalid.AppendLine(i)
			Next
			sbInvalid.AppendLine()
			sbInvalid.AppendLine("Do you want to generate the script anyway?")
			If MsgBox(sbInvalid.ToString(), MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
		End If

		If txtActions.Count = 0 Then
			MsgBox("No button presses found.")
			Exit Sub
		End If

		Dim press As New List(Of Integer)
		Dim wait As New List(Of Integer)
		Dim press_default As Integer = unformatMS(txtDefaultPress.Text)
		Dim wait_default As Integer = unformatMS(txtDefaultWait.Text)
		For i As Integer = enumTextButtons.etbUp To enumTextButtons.etbPause
			If lstTxtPress(i) Is Nothing Then
				press.Add(0)
			ElseIf lstTxtPress(i).Text = vbNullString Then
				press.Add(press_default)
			Else
				press.Add(unformatMS(lstTxtPress(i).Text))
			End If
			If lstTxtWait(i) Is Nothing Then
				wait.Add(0)
			ElseIf lstTxtwait(i).Text = vbNullString Then
				wait.Add(wait_default)
			Else
				wait.Add(unformatMS(lstTxtWait(i).Text))
			End If
		Next

		actions = New List(Of clsAction)

		For i = 0 To txtActions.Count - 1
			'	Private lstMask As New List(Of Integer)(New Integer() {&H100, &H800, &H200, &H400, &H10, &H20, &H40, &H80, &H1, &H2, -1, -2, &H4000, &H8000, &H2000, &H1000, &H4, -3, -4, -5, -6, -7, -8, -9, -10, -11})
			Dim txtAction As Integer = txtActions(i)
			Select Case txtAction
				Case enumTextButtons.etbLT
					actions.Add(New clsActionPress(1, 0, 255, -1, New Point(-32768, -32768), New Point(-32768, -32768), press(txtAction), wait(txtAction), rptActions(i), Nothing))
				Case enumTextButtons.etbRT
					actions.Add(New clsActionPress(1, 0, -1, 255, New Point(-32768, -32768), New Point(-32768, -32768), press(txtAction), wait(txtAction), rptActions(i), Nothing))
				Case enumTextButtons.etbLU
					actions.Add(New clsActionPress(1, 0, -1, -1, New Point(0, -32512), New Point(-32768, -32768), press(txtAction), wait(txtAction), rptActions(i), Nothing))
				Case enumTextButtons.etbLR
					actions.Add(New clsActionPress(1, 0, -1, -1, New Point(32512, 0), New Point(-32768, -32768), press(txtAction), wait(txtAction), rptActions(i), Nothing))
				Case enumTextButtons.etbLD
					actions.Add(New clsActionPress(1, 0, -1, -1, New Point(0, 32512), New Point(-32768, -32768), press(txtAction), wait(txtAction), rptActions(i), Nothing))
				Case enumTextButtons.etbLL
					actions.Add(New clsActionPress(1, 0, -1, -1, New Point(-32512, 0), New Point(-32768, -32768), press(txtAction), wait(txtAction), rptActions(i), Nothing))
				Case enumTextButtons.etbRU
					actions.Add(New clsActionPress(1, 0, -1, -1, New Point(-32768, -32768), New Point(0, -32512), press(txtAction), wait(txtAction), rptActions(i), Nothing))
				Case enumTextButtons.etbRR
					actions.Add(New clsActionPress(1, 0, -1, -1, New Point(-32768, -32768), New Point(32512, 0), press(txtAction), wait(txtAction), rptActions(i), Nothing))
				Case enumTextButtons.etbRD
					actions.Add(New clsActionPress(1, 0, -1, -1, New Point(-32768, -32768), New Point(0, 32512), press(txtAction), wait(txtAction), rptActions(i), Nothing))
				Case enumTextButtons.etbRL
					actions.Add(New clsActionPress(1, 0, -1, -1, New Point(-32768, -32768), New Point(-32512, 0), press(txtAction), wait(txtAction), rptActions(i), Nothing))
				Case enumTextButtons.etbPause
					actions.Add(New clsActionWait(wait(txtAction), Nothing))
				Case Else
					actions.Add(New clsActionPress(1, lstMask(txtAction), -1, -1, New Point(-32768, -32768), New Point(-32768, -32768), press(txtAction), wait(txtAction), rptActions(i), Nothing))
			End Select
			actions(i).index = i
		Next

		Me.Close()
	End Sub

	Private Sub chkDelNone_CheckedChanged(sender As Object, e As EventArgs) Handles chkDelNone.CheckedChanged
		chkDelComma.Enabled = Not chkDelNone.Checked
		chkDelTab.Enabled = Not chkDelNone.Checked
		chkDelSpace.Enabled = Not chkDelNone.Checked
		chkDelLine.Enabled = Not chkDelNone.Checked
		chkDelOther.Enabled = Not chkDelNone.Checked
		txtDelOther.Enabled = Not chkDelNone.Checked
		chkNumberSuffix.Enabled = Not chkDelNone.Checked
	End Sub

	Private Sub gbText_Enter(sender As Object, e As EventArgs) Handles gbText.Enter

	End Sub

	Private Sub frmText_Shown(sender As Object, e As EventArgs) Handles Me.Shown
		txtInput.Focus()
	End Sub
End Class