Imports System.Runtime.InteropServices



Public Class frmcaptureWizard
    'PixelWatch Wizard:
    'Record/stop grabs frame every Xms and stores in temp (ask about delete when you exit or start record)
    'Filmstrip view of thumbnails and seq number, when you click shows in main window (but scaled down).
    'Can select start frame and end frame. They will show in smaller pic view but larger than filmstrip.
    'You can click in main pic view to set target pixel. Shows in a 3rd small view zoomed in 400%, can click to tweak from there also.

    'It will scan selected range and preset tolerances (below) + some amount

    'It will show r,g,b, values, and let you pick tolerances, and will show the outer range for those tolerances (8 total).

    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
    Private Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal Msg As UInteger, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr
    End Function

    Public Function MakeLong(lowPart As Short, highPart As Short) As Integer
        Return CType(lowPart, UInt32) + CType(highPart, UInteger) * 65536
    End Function

    Private startIdx As Integer = -1
    Private endIdx As Integer = -1
    Public Canceled As Boolean = False
    Public pos As Point = Nothing
    Public zoomRect As Rectangle = Nothing
    Public minColor As Color = Nothing
    Public maxColor As Color = Nothing

    Public Sub ListViewItem_SetSpacing(lv As ListView, leftPadding As Short, topPadding As Short)
        Const LVM_FIRST As Integer = &H1000
        Const LVM_SETICONSPACING As Integer = LVM_FIRST + 53
        SendMessage(lv.Handle, LVM_SETICONSPACING, IntPtr.Zero, CType(MakeLong(leftPadding, topPadding), IntPtr))
    End Sub

    Private Sub btnOK_Click(sender As System.Object, e As System.EventArgs) Handles btnOK.Click
        Canceled = False
        If pos = Nothing Then
            MsgBox("You must select a location.")
            Exit Sub
        End If
        minColor = Color.FromArgb(nudRMin.Value, nudGMin.Value, nudBMin.Value)
        maxColor = Color.FromArgb(nudRMax.Value, nudGMax.Value, nudBMax.Value)
        Me.Close()
    End Sub

    Dim snap As clsSnapshot
    Dim snapIdx As Integer = 0

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ListViewItem_SetSpacing(lvCaptures, 136, 82)
        If Not IO.Directory.Exists("snaps") Then IO.Directory.CreateDirectory("snaps")

    End Sub

    Private Sub frmcaptureWizard_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If Not snap Is Nothing Then snap.Dispose()
    End Sub

    Private Sub frmcaptureWizard_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim curDevPath As String = GetSetting(Application.ProductName, "Settings", "CapDevPath", vbNullString)
        If curDevPath = vbNullString Then
            MsgBox("Capture Device not set.")
            Me.Close()
            Exit Sub
        End If
        Try
            snap = New clsSnapshot(curDevPath)
        Catch ex As Exception
            MsgBox("Capture Device error:" & vbCrLf & ex.Message)
            Me.Close()
            Exit Sub
        End Try
        If (New IO.DirectoryInfo("snaps")).GetFiles.Length > 0 Then loadImages()
    End Sub

    Private Sub btnPlay_Click(sender As System.Object, e As System.EventArgs) Handles btnPlay.Click
        tmrPreview.Enabled = Not tmrPreview.Enabled
        If tmrPreview.Enabled Then
            btnPlay.Text = ";"
            btnPlay.ForeColor = Color.Olive
        Else
            btnPlay.Text = "4"
            btnPlay.ForeColor = Color.Green
        End If
    End Sub

    Private Sub loadImages()
        lvCaptures.Items.Clear()
        ilCaptures.Images.Clear()
        Dim fis() As IO.FileInfo = (New IO.DirectoryInfo("snaps")).GetFiles()
        For Each fi As IO.FileInfo In fis
            Dim bmp As Bitmap = Image.FromFile(fi.FullName)
            Dim thumb As New Bitmap(128, 72)
            Dim g As Graphics = Graphics.FromImage(thumb)
            g.DrawImage(bmp, 0, 0, thumb.Width, thumb.Height)
            bmp.Dispose()
            g.Dispose()
            Dim key As String = fi.Name
            key = key.Substring(0, InStrRev(key, ".") - 1)
            key = CInt(key)
            ilCaptures.Images.Add(key, thumb)
            lvCaptures.Items.Add(vbNullString, key)
        Next
        startIdx = -1
        endIdx = -1
    End Sub

    Private Sub btnRecord_Click(sender As System.Object, e As System.EventArgs) Handles btnRecord.Click
        If tmrRecord.Enabled Then
            tmrRecord.Enabled = False
            loadImages()
            btnRecord.Text = "="
            btnRecord.ForeColor = Color.Red
            btnPlay.Enabled = True
        Else
            If tmrPreview.Enabled Then btnPlay_Click(Nothing, Nothing)
            btnPlay.Enabled = False
            If Not IO.Directory.Exists("snaps") Then IO.Directory.CreateDirectory("snaps")
            Dim fi() As IO.FileInfo = (New IO.DirectoryInfo("snaps")).GetFiles()
            If fi.Length > 0 Then
                If MsgBox("Delete existing snapshots?", vbYesNo) = vbYes Then
                    For i = 0 To fi.Length - 1
                        fi(i).Delete()
                    Next
                Else
                    Exit Sub
                End If
            End If
            snapIdx = 0
            btnRecord.Text = "<"
            btnRecord.ForeColor = Color.Black
            tmrRecord.Enabled = True
        End If
    End Sub

    Private Sub tmrPreview_Tick(sender As System.Object, e As System.EventArgs) Handles tmrPreview.Tick
        Dim oldImage As Image = pbMain.Image
        pbMain.Image = snap.capture
        If Not oldImage Is Nothing Then oldImage.Dispose()
        pbMain.Refresh()
    End Sub

    Private Sub tmrRecord_Tick(sender As System.Object, e As System.EventArgs) Handles tmrRecord.Tick
        Dim bmp As Bitmap = snap.capture
        Dim oldImage As Image = pbMain.Image
        pbMain.Image = bmp
        If Not oldImage Is Nothing Then oldImage.Dispose()
        snapIdx += 1
        Dim s As Date = Now
        bmp.Save("snaps/" & snapIdx.ToString("D6") & ".png", System.Drawing.Imaging.ImageFormat.Png)
        Debug.Print((s - Now).TotalMilliseconds)
        pbMain.Refresh()
    End Sub

    Private Function fileClone(src As String) As Image
        Dim tmp As Image = Image.FromFile(src)
        Dim bmp As New Bitmap(tmp.Width, tmp.Height)
        Dim g As Graphics = Graphics.FromImage(bmp)
        g.DrawImage(tmp, 0, 0)
        g.Dispose()
        tmp.Dispose()
        Return bmp
    End Function

    Private Sub lvCaptures_DoubleClick(sender As Object, e As System.EventArgs) Handles lvCaptures.DoubleClick
        Dim oldImage As Image = pbMain.Image
        pbMain.Image = fileClone("snaps/" & CInt(lvCaptures.SelectedItems(0).ImageKey).ToString("D6") & ".png")
        If Not oldImage Is Nothing Then oldImage.Dispose()
    End Sub

    Private Sub btnFirst_Click(sender As System.Object, e As System.EventArgs) Handles btnFirst.Click
        If lvCaptures.SelectedItems.Count = 0 Then
            MsgBox("You must select a start image.")
            Exit Sub
        End If
        Dim idx As Integer = CInt(lvCaptures.SelectedItems(0).ImageKey)
        If endIdx >= 0 And endIdx < idx Then
            If MsgBox("This frame is after the last frame you selected. Do you want to use this as the new last frame and the current last frame as the new first frame?", vbYesNo) = vbYes Then
                startIdx = endIdx
                endIdx = idx
                Dim oldImage As Image = pbStart.Image
                pbStart.Image = fileClone("snaps/" & startIdx.ToString("D6") & ".png")
                If Not oldImage Is Nothing Then oldImage.Dispose()
                oldImage = pbStop.Image
                pbStop.Image = fileClone("snaps/" & endIdx.ToString("D6") & ".png")
                If Not oldImage Is Nothing Then oldImage.Dispose()
            Else
                Exit Sub
            End If
        Else
            startIdx = idx
            Dim oldImage As Image = pbStart.Image
            pbStart.Image = fileClone("snaps/" & startIdx.ToString("D6") & ".png")
            If Not oldImage Is Nothing Then oldImage.Dispose()
        End If
    End Sub

    Private Sub btnLast_Click(sender As System.Object, e As System.EventArgs) Handles btnLast.Click
        If lvCaptures.SelectedItems.Count = 0 Then
            MsgBox("You must select a start image.")
            Exit Sub
        End If
        Dim idx As Integer = CInt(lvCaptures.SelectedItems(0).ImageKey)
        If startIdx >= 0 And startIdx > idx Then
            If MsgBox("This frame is before the first frame you selected. Do you want to use this as the new first frame and the current first frame as the new last frame?", vbYesNo) = vbYes Then
                endIdx = startIdx
                startIdx = idx
                Dim oldImage As Image = pbStart.Image
                pbStart.Image = fileClone("snaps/" & startIdx.ToString("D6") & ".png")
                If Not oldImage Is Nothing Then oldImage.Dispose()
                oldImage = pbStop.Image
                pbStop.Image = fileClone("snaps/" & endIdx.ToString("D6") & ".png")
                If Not oldImage Is Nothing Then oldImage.Dispose()
            Else
                Exit Sub
            End If
        Else
            endIdx = idx
            Dim oldImage As Image = pbStop.Image
            pbStop.Image = fileClone("snaps/" & endIdx.ToString("D6") & ".png")
            If Not oldImage Is Nothing Then oldImage.Dispose()
        End If

    End Sub

    Private Sub btnScan_Click(sender As System.Object, e As System.EventArgs) Handles btnScan.Click
        If pos = Nothing Then
            MsgBox("You must select a location.")
            Exit Sub
        End If
        If startIdx = -1 Or endIdx = -1 Then
            MsgBox("You must select a range of snapshots to scan.")
            Exit Sub
        End If
        Dim min() As Byte = {255, 255, 255}
        Dim max() As Byte = {0, 0, 0}
        For idx As Integer = startIdx To endIdx
            Dim bmp As Bitmap = Image.FromFile("snaps/" & idx.ToString("D6") & ".png")
            Dim curColor As Color = bmp.GetPixel(pos.X, pos.Y)
            bmp.Dispose()
            min(0) = Math.Min(min(0), curColor.R)
            min(1) = Math.Min(min(1), curColor.G)
            min(2) = Math.Min(min(2), curColor.B)
            max(0) = Math.Max(max(0), curColor.R)
            max(1) = Math.Max(max(1), curColor.G)
            max(2) = Math.Max(max(2), curColor.B)
        Next
        nudRMin.Value = min(0)
        nudGMin.Value = min(1)
        nudBMin.Value = min(2)
        nudRMax.Value = max(0)
        nudGMax.Value = max(1)
        nudBMax.Value = max(2)
        refreshColors()
    End Sub

    Private Sub btnCancel_Click(sender As System.Object, e As System.EventArgs) Handles btnCancel.Click
        Canceled = True
        Me.Close()
    End Sub

    Private Sub refreshcolors()
        'TODO: draw color range
        Dim bmpColors As New Bitmap(40, 120)
        Dim g As Graphics = Graphics.FromImage(bmpColors)
        g.FillRectangle(New SolidBrush(Color.FromArgb((nudRMin.Value + nudRMax.Value) / 2, (nudGMin.Value + nudGMax.Value) / 2, (nudBMin.Value + nudBMax.Value) / 2)), New Rectangle(0, 0, 40, 40))
        g.FillRectangle(New SolidBrush(Color.FromArgb(nudRMin.Value, nudGMin.Value, nudBMin.Value)), New Rectangle(0, 40, 20, 20))
        g.FillRectangle(New SolidBrush(Color.FromArgb(nudRMax.Value, nudGMin.Value, nudBMin.Value)), New Rectangle(20, 40, 20, 20))
        g.FillRectangle(New SolidBrush(Color.FromArgb(nudRMin.Value, nudGMax.Value, nudBMin.Value)), New Rectangle(0, 60, 20, 20))
        g.FillRectangle(New SolidBrush(Color.FromArgb(nudRMax.Value, nudGMax.Value, nudBMin.Value)), New Rectangle(20, 60, 20, 20))
        g.FillRectangle(New SolidBrush(Color.FromArgb(nudRMin.Value, nudGMin.Value, nudBMax.Value)), New Rectangle(0, 80, 20, 20))
        g.FillRectangle(New SolidBrush(Color.FromArgb(nudRMax.Value, nudGMin.Value, nudBMax.Value)), New Rectangle(20, 80, 20, 20))
        g.FillRectangle(New SolidBrush(Color.FromArgb(nudRMin.Value, nudGMax.Value, nudBMax.Value)), New Rectangle(0, 100, 20, 20))
        g.FillRectangle(New SolidBrush(Color.FromArgb(nudRMax.Value, nudGMax.Value, nudBMax.Value)), New Rectangle(20, 100, 20, 20))
        g.Dispose()
        pbColors.Image = bmpColors
    End Sub

    Private Sub refreshLocation()
        txtLocation.Text = pos.X & ", " & pos.Y
        btnUp.Enabled = True
        btnDown.Enabled = True
        btnLeft.Enabled = True
        btnRight.Enabled = True
        btnScan.Enabled = True
        zoomRect = New Rectangle(pos.X - pbZoom.Width \ 8, pos.Y - pbZoom.Height \ 8, pbZoom.Height \ 4, pbZoom.Height \ 4)
        If zoomRect.X < 0 Then zoomRect.X = 0
        If zoomRect.Y < 0 Then zoomRect.Y = 0
        If zoomRect.Right >= pbMain.Image.Width Then zoomRect.X = zoomRect.X - zoomRect.Right + pbMain.Image.Width
        If zoomRect.Bottom >= pbMain.Image.Height + 1 Then zoomRect.Y = zoomRect.Y - zoomRect.Bottom + pbMain.Image.Height + 1
        Dim bmpZoom As New Bitmap(pbZoom.Width, pbZoom.Height)
        Dim g As Graphics = Graphics.FromImage(bmpZoom)
        g.SmoothingMode = Drawing2D.SmoothingMode.None
        g.InterpolationMode = Drawing2D.InterpolationMode.NearestNeighbor
        g.DrawImage(pbMain.Image, New Rectangle(0, 0, pbZoom.Width, pbZoom.Height), zoomRect, GraphicsUnit.Pixel)
        g.DrawRectangle(Pens.Fuchsia, New Rectangle(4 * (pos.X - zoomRect.X) - 1, 4 * (pos.Y - zoomRect.Y) - 1, 6, 6))
        g.Dispose()
        Dim oldImage As Image = pbZoom.Image
        pbZoom.Image = bmpZoom
        If Not oldImage Is Nothing Then oldImage.Dispose()
    End Sub

    Private Sub nud_ValueChanged(sender As System.Object, e As System.EventArgs) Handles nudRMin.ValueChanged, nudRMax.ValueChanged, nudGMin.ValueChanged, nudGMax.ValueChanged, nudBMin.ValueChanged, nudBMax.ValueChanged
        refreshcolors()
    End Sub

    Private Sub btnUp_Click(sender As System.Object, e As System.EventArgs) Handles btnUp.Click
        If pos.Y <= 0 Then Exit Sub
        pos.Y -= 1
        refreshLocation()
    End Sub

    Private Sub btnDown_Click(sender As System.Object, e As System.EventArgs) Handles btnDown.Click
        If pos.Y >= pbMain.Image.Height - 1 Then Exit Sub
        pos.Y += 1
        refreshLocation()
    End Sub

    Private Sub btnLeft_Click(sender As System.Object, e As System.EventArgs) Handles btnLeft.Click
        If pos.X <= 0 Then Exit Sub
        pos.X -= 1
        refreshLocation()
    End Sub

    Private Sub btnRight_Click(sender As System.Object, e As System.EventArgs) Handles btnRight.Click
        If pos.X >= pbMain.Image.Width - 1 Then Exit Sub
        pos.X += 1
        refreshLocation()
    End Sub

    Private Sub pbMain_MouseClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles pbMain.MouseClick
        pos.X = e.X * pbMain.Image.Width / pbMain.Width
        pos.Y = e.Y * pbMain.Image.Height / pbMain.Height
        refreshLocation()
    End Sub

    Private Sub pbZoom_MouseClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles pbZoom.MouseClick
        If pos = Nothing Then Exit Sub
        pos.X = e.X \ 4 + zoomRect.X
        pos.Y = e.Y \ 4 + zoomRect.Y
        refreshLocation()
    End Sub
End Class