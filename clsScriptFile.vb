Imports System.Xml
Module scriptfile
    Public Const mainGroup As String = "[Main]"
End Module

Public Class clsScriptFile
    Public game As String
    Public title As String
    Public description As String
    Public version As Integer
    Public groups As New Dictionary(Of String, clsActionGroup)
    Public path As String

    Private Shared Function loadScriptLegacy(path As String) As clsScriptFile
        Dim lines() As String = IO.File.ReadAllLines(path)
        Dim header As New List(Of String)
        Dim start As Integer = 0
        While lines(start).StartsWith("#")
            header.Add(lines(start).Substring(1))
            start = start + 1
        End While

        Dim sf As New clsScriptFile

        If header.Count > 0 Then
            sf.game = header(0)
            header.RemoveAt(0)
        End If

        If header.Count > 0 Then
            sf.title = header(0)
            header.RemoveAt(0)
        End If
        sf.version = 0
        sf.path = path

        If header.Count > 0 Then sf.description = Join(header.ToArray(), vbCrLf)

        Dim group As clsActionGroup = New clsActionGroup(mainGroup)
        sf.groups.Add(group.name, group)

        For i = start To lines.Length - 1
            Dim action As clsAction = clsAction.deSerialize(lines(i), group)
            action.index = group.actions.Count
            group.actions.Add(action)
        Next

        Return sf
    End Function

    Public Shared Function loadScriptXML(path As String) As clsScriptFile
        Dim doc As New XmlDocument
        Try
            doc.Load(path)
        Catch ex As XmlException
            Return loadScriptLegacy(path)
        End Try
        Dim sf As New clsScriptFile
        sf.version = 1

        Dim node As Xml.XmlNode = doc.SelectSingleNode("/XBScript/Information/Game")
        If Not node Is Nothing Then sf.game = node.InnerText
        node = doc.SelectSingleNode("/XBScript/Information/Title")
        If Not node Is Nothing Then sf.title = node.InnerText
        node = doc.SelectSingleNode("/XBScript/Information/Description")
        If Not node Is Nothing Then sf.description = node.InnerText
        node = doc.SelectSingleNode("/XBScript/Information/Version")
        If Not node Is Nothing Then sf.version = CInt(node.InnerText)
        Dim actionGroups As Xml.XmlNodeList = doc.SelectNodes("/XBScript/ActionGroups/ActionGroup")
        For Each agNode As Xml.XmlNode In actionGroups
            Dim ag As New clsActionGroup(agNode.SelectSingleNode("Name").InnerText)
            For Each actNode As Xml.XmlNode In agNode.ChildNodes
                Select Case actNode.Name
                    Case "Name", "#comment"
                    Case Else
                        Dim action As clsAction = clsAction.fromXML(actNode, sf.version, ag)
                        action.index = ag.actions.Count
                        ag.actions.Add(action)
                End Select
            Next
            sf.groups.Add(ag.name, ag)
        Next
        sf.path = path
        Return sf
    End Function

    Public Sub saveScriptXML(Optional newPath As String = vbNullString)
        If newPath = vbNullString Then newPath = path
        If newPath = vbNullString Then
            MsgBox("No save location provided.")
            Exit Sub
        End If
        Dim doc As New XmlDocument
        Dim root As XmlElement = doc.CreateElement("XBScript")
        doc.AppendChild(root)
        Dim desc As XmlElement = doc.CreateElement("Information")
        root.AppendChild(desc)
        desc.AppendChild(doc.CreateElement("Game")).InnerText = game
        desc.AppendChild(doc.CreateElement("Title")).InnerText = title
        desc.AppendChild(doc.CreateElement("Description")).InnerText = description
        desc.AppendChild(doc.CreateElement("Version")).InnerText = 2
        Dim agsNode As XmlElement = doc.CreateElement("ActionGroups")
        root.AppendChild(agsNode)
        For Each group As clsActionGroup In groups.Values
            Dim agNode As XmlElement = doc.CreateElement("ActionGroup")
            agsNode.AppendChild(agNode)
            agNode.AppendChild(doc.CreateElement("Name")).InnerText = group.name
            For Each action As clsAction In group.actions
                agNode.AppendChild(action.toXML(doc))
            Next
        Next
        Dim ws As New Xml.XmlWriterSettings()
        ws.Indent = True
        Dim w As Xml.XmlWriter = Xml.XmlWriter.Create(newPath, ws)
        doc.WriteTo(w)
        w.Close()
        w.Dispose()
        path = newPath
    End Sub
End Class
