﻿Public Class RefreshingListBox
    Inherits ListBox
    Public Overloads Sub RefreshItem(index As Integer)
        MyBase.RefreshItem(index)
    End Sub

    Public Overloads Sub RefreshItems()
        MyBase.RefreshItems()
    End Sub
End Class
