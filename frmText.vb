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

	Private Sub txtTime_Validation(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtDelay_Comma.Validating, txtDelay_Line.Validating, txtDelay_Other.Validating, txtDelay_Semicolon.Validating, txtDelay_Space.Validating, txtDelay_Tab.Validating
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
		SaveSetting(Application.ProductName, "TTS", "Delim_Required", IIf(chkDelRequired.Checked, "True", "False"))

		SaveSetting(Application.ProductName, "TTS", "Delim_Comma", IIf(chkDelComma.Checked, "True", "False"))
		SaveSetting(Application.ProductName, "TTS", "Delay_Comma", txtDelay_Comma.Text)

		SaveSetting(Application.ProductName, "TTS", "Delim_Tab", IIf(chkDelTab.Checked, "True", "False"))
		SaveSetting(Application.ProductName, "TTS", "Delay_Tab", txtDelay_Tab.Text)

		SaveSetting(Application.ProductName, "TTS", "Delim_Space", IIf(chkDelSpace.Checked, "True", "False"))
		SaveSetting(Application.ProductName, "TTS", "Delay_Space", txtDelay_Space.Text)

		SaveSetting(Application.ProductName, "TTS", "Delim_Semicolon", IIf(chkDelSemicolon.Checked, "True", "False"))
		SaveSetting(Application.ProductName, "TTS", "Delay_Semicolon", txtDelay_Semicolon.Text)

		SaveSetting(Application.ProductName, "TTS", "Delim_Line", IIf(chkDelLine.Checked, "True", "False"))
		SaveSetting(Application.ProductName, "TTS", "Delay_Line", txtDelay_Line.Text)

		SaveSetting(Application.ProductName, "TTS", "Delim_Other", IIf(chkDelOther.Checked, "True", "False"))
		SaveSetting(Application.ProductName, "TTS", "Delim_Other_Text", txtDelOther.Text)
		SaveSetting(Application.ProductName, "TTS", "Delay_Other", txtDelay_Other.Text)

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
		chkDelRequired.Checked = GetSetting(Application.ProductName, "TTS", "Delim_Required", "True") = "True"

		chkDelComma.Checked = GetSetting(Application.ProductName, "TTS", "Delim_Comma", "True") = "True"
		txtDelay_Comma.Text = GetSetting(Application.ProductName, "TTS", "Delay_Comma", vbNullString)

		chkDelTab.Checked = GetSetting(Application.ProductName, "TTS", "Delim_Tab", "True") = "True"
		txtDelay_Tab.Text = GetSetting(Application.ProductName, "TTS", "Delay_Tab", vbNullString)

		chkDelSpace.Checked = GetSetting(Application.ProductName, "TTS", "Delim_Space", "True") = "True"
		txtDelay_Space.Text = GetSetting(Application.ProductName, "TTS", "Delay_Space", vbNullString)

		chkDelSemicolon.Checked = GetSetting(Application.ProductName, "TTS", "Delim_Semicolon", "True") = "True"
		txtDelay_Semicolon.Text = GetSetting(Application.ProductName, "TTS", "Delay_Semicolon", vbNullString)

		chkDelLine.Checked = GetSetting(Application.ProductName, "TTS", "Delim_Line", "True") = "True"
		txtDelay_Line.Text = GetSetting(Application.ProductName, "TTS", "Delay_Line", vbNullString)

		chkDelOther.Checked = GetSetting(Application.ProductName, "TTS", "Delim_Other", "False") = "True"
		txtDelOther.Text = GetSetting(Application.ProductName, "TTS", "Delim_Other_Text", vbNullString)
		txtDelay_Other.Text = GetSetting(Application.ProductName, "TTS", "Delay_Other", vbNullString)

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
		Dim comActions As New List(Of String)
		Dim src As String = txtInput.Text
		src = src.Replace(vbCrLf, vbLf)
		src = src.Replace(vbCr, vbLf)

		Dim invalid As New HashSet(Of String)

		If Not chkDelRequired.Checked Then
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

			If chkDelComma.Checked Then tokens.Add(",", -1)
			If chkDelSpace.Checked Then tokens.Add(" ", -2)
			If chkDelSemicolon.Checked Then tokens.Add(";", -3)
			If chkDelTab.Checked Then tokens.Add(vbTab, -4)
			If chkDelLine.Checked Then tokens.Add(vbLf, -5)
			If chkDelOther.Checked Then
				For i = 0 To txtDelOther.Text.Length - 1
					tokens.Add(txtDelOther.Text.Substring(i, 1), -6)
				Next
			End If

			Dim c As Integer
			Do While c < src.Length
				Dim action As Integer = -1
				Dim token As String = src.Substring(c, 1)
				If tokens.TryGetValue(token, action) Then
					txtActions.Add(action)
					Dim sbDigits As New Text.StringBuilder
					If chkNumberSuffix.Checked Then
						While (c + 1) < src.Length
							Select Case src.Substring(c + 1, 1)
								Case "0" To "9"
									sbDigits.Append(src.Substring(c + 1, 1))
									c = c + 1
								Case Else
									Exit While
							End Select
						End While
					End If
					If sbDigits.Length > 0 Then
							rptActions.Add(CInt(sbDigits.ToString()))
						Else
							rptActions.Add(1)
						End If
						comActions.Add(vbNullString)
					Else
						Select Case token
						Case " ", vbLf, vbTab
						Case Else
							If Not invalid.Contains(token) Then invalid.Add(token)
					End Select
				End If
				c = c + 1
			Loop
		Else
			Dim delims As New List(Of String)
			If chkDelComma.Checked Then
				delims.Add(",")
				tokens.Add(",", -1)
			End If
			If chkDelSpace.Checked Then
				delims.Add(" ")
				tokens.Add(" ", -2)
			End If
			If chkDelSemicolon.Checked Then
				delims.Add(";")
				tokens.Add(";", -3)
			End If
			If chkDelTab.Checked Then
				delims.Add(vbTab)
				tokens.Add(vbTab, -4)
			End If
			If chkDelLine.Checked Then
				delims.Add(vbLf)
				tokens.Add(vbLf, -5)
			End If
			If chkDelOther.Checked Then
				For i = 0 To txtDelOther.Text.Length - 1
					delims.Add(txtDelOther.Text.Substring(i, 1))
					tokens.Add(txtDelOther.Text.Substring(i, 1), -6)
				Next
			End If
			If delims.Count = 0 Then
				MsgBox("You must select at least one delimiter.")
				Exit Sub
			End If

			For i As Integer = enumTextButtons.etbUp To enumTextButtons.etbPause
				If (Not lstTxtButton(i) Is Nothing) AndAlso lstTxtButton(i).Text <> vbNullString Then
					If tokens.ContainsKey(lstTxtButton(i).Text) Then
						MsgBox("Button values must be unique.")
						Exit Sub
					End If
					tokens.Add(lstTxtButton(i).Text, i)
				End If
			Next

			Dim segments() As String = splitDelims(src, delims.ToArray())
			Dim reRepeat As New Regex("^(?<token>\D+)(?<repeat>\d+)?(\{(?<comment>.*)\})?$", RegexOptions.Compiled)

			For Each segment As String In segments
				Dim action As Integer = -1
				Dim repeat As Integer = 1
				Dim comment As String = vbNullString
				If chkNumberSuffix.Checked Then
					Dim m As Match = reRepeat.Match(segment)
					If m.Success Then
						segment = m.Groups("token").Value
						If m.Groups("repeat").Length > 0 Then repeat = m.Groups("repeat").Value
						comment = m.Groups("comment").Value
					End If
				End If

				If tokens.TryGetValue(segment, action) Then
					txtActions.Add(action)
					rptActions.Add(repeat)
					comActions.Add(comment)
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
			ElseIf lstTxtWait(i).Text = vbNullString Then
				wait.Add(wait_default)
			Else
				wait.Add(unformatMS(lstTxtWait(i).Text))
			End If
		Next

		actions = New List(Of clsAction)

		Dim delay_comma As Integer = 0
		Dim delay_space As Integer = 0
		Dim delay_semicolon As Integer = 0
		Dim delay_tab As Integer = 0
		Dim delay_line As Integer = 0
		Dim delay_other As Integer = 0
		If txtDelay_Comma.Text <> vbNullString Then delay_comma = unformatMS(txtDelay_Comma.Text)
		If txtDelay_Space.Text <> vbNullString Then delay_space = unformatMS(txtDelay_Space.Text)
		If txtDelay_Semicolon.Text <> vbNullString Then delay_semicolon = unformatMS(txtDelay_Semicolon.Text)
		If txtDelay_Tab.Text <> vbNullString Then delay_tab = unformatMS(txtDelay_Tab.Text)
		If txtDelay_Line.Text <> vbNullString Then delay_line = unformatMS(txtDelay_Line.Text)
		If txtDelay_Other.Text <> vbNullString Then delay_other = unformatMS(txtDelay_Other.Text)

		Dim actIndex As Integer = 1
		For i = 0 To txtActions.Count - 1
			'	Private lstMask As New List(Of Integer)(New Integer() {&H100, &H800, &H200, &H400, &H10, &H20, &H40, &H80, &H1, &H2, -1, -2, &H4000, &H8000, &H2000, &H1000, &H4, -3, -4, -5, -6, -7, -8, -9, -10, -11})
			Dim txtAction As Integer = txtActions(i)
			Dim a As clsAction = Nothing
			Select Case txtAction
				Case -1 'Comma
					If delay_comma > 0 Then a = New clsActionWait(delay_comma, Nothing)
				Case -2 'Space
					If delay_space > 0 Then a = New clsActionWait(delay_space, Nothing)
				Case -3 'Semicolon
					If delay_semicolon > 0 Then a = New clsActionWait(delay_semicolon, Nothing)
				Case -4 'Tab
					If delay_tab > 0 Then a = New clsActionWait(delay_tab, Nothing)
				Case -5 'Line
					If delay_line > 0 Then a = New clsActionWait(delay_line, Nothing)
				Case -6 'Other
					If delay_other > 0 Then a = New clsActionWait(delay_other, Nothing)
				Case enumTextButtons.etbLT
					a = New clsActionPress(1, 0, 255, -1, New Point(-32768, -32768), New Point(-32768, -32768), press(txtAction), wait(txtAction), rptActions(i), Nothing)
				Case enumTextButtons.etbRT
					a = New clsActionPress(1, 0, -1, 255, New Point(-32768, -32768), New Point(-32768, -32768), press(txtAction), wait(txtAction), rptActions(i), Nothing)
				Case enumTextButtons.etbLU
					a = New clsActionPress(1, 0, -1, -1, New Point(0, -32512), New Point(-32768, -32768), press(txtAction), wait(txtAction), rptActions(i), Nothing)
				Case enumTextButtons.etbLR
					a = New clsActionPress(1, 0, -1, -1, New Point(32512, 0), New Point(-32768, -32768), press(txtAction), wait(txtAction), rptActions(i), Nothing)
				Case enumTextButtons.etbLD
					a = New clsActionPress(1, 0, -1, -1, New Point(0, 32512), New Point(-32768, -32768), press(txtAction), wait(txtAction), rptActions(i), Nothing)
				Case enumTextButtons.etbLL
					a = New clsActionPress(1, 0, -1, -1, New Point(-32512, 0), New Point(-32768, -32768), press(txtAction), wait(txtAction), rptActions(i), Nothing)
				Case enumTextButtons.etbRU
					a = New clsActionPress(1, 0, -1, -1, New Point(-32768, -32768), New Point(0, -32512), press(txtAction), wait(txtAction), rptActions(i), Nothing)
				Case enumTextButtons.etbRR
					a = New clsActionPress(1, 0, -1, -1, New Point(-32768, -32768), New Point(32512, 0), press(txtAction), wait(txtAction), rptActions(i), Nothing)
				Case enumTextButtons.etbRD
					a = New clsActionPress(1, 0, -1, -1, New Point(-32768, -32768), New Point(0, 32512), press(txtAction), wait(txtAction), rptActions(i), Nothing)
				Case enumTextButtons.etbRL
					a = New clsActionPress(1, 0, -1, -1, New Point(-32768, -32768), New Point(-32512, 0), press(txtAction), wait(txtAction), rptActions(i), Nothing)
				Case enumTextButtons.etbPause
					a = New clsActionWait(wait(txtAction), Nothing)
				Case Else
					a = New clsActionPress(1, lstMask(txtAction), -1, -1, New Point(-32768, -32768), New Point(-32768, -32768), press(txtAction), wait(txtAction), rptActions(i), Nothing)
			End Select
			If Not a Is Nothing Then
				a.comment = comActions(i)
				a.index = actIndex
				actions.Add(a)
				actIndex += 1
			End If
		Next

		Me.Close()
	End Sub

	Private Sub chkDelNone_CheckedChanged(sender As Object, e As EventArgs) Handles chkDelRequired.CheckedChanged
		'chkDelComma.Enabled = Not chkDelRequired.Checked
		'chkDelTab.Enabled = Not chkDelRequired.Checked
		'chkDelSpace.Enabled = Not chkDelRequired.Checked
		'chkDelSemicolon.Enabled = Not chkDelRequired.Checked
		'chkDelLine.Enabled = Not chkDelRequired.Checked
		'chkDelOther.Enabled = Not chkDelRequired.Checked
		'txtDelOther.Enabled = Not chkDelNone.Checked
		chkNumberSuffix.Enabled = Not chkDelRequired.Checked
	End Sub

	Private Sub gbText_Enter(sender As Object, e As EventArgs) Handles gbText.Enter

	End Sub

	Private Sub frmText_Shown(sender As Object, e As EventArgs) Handles Me.Shown
		txtInput.Focus()
	End Sub


End Class