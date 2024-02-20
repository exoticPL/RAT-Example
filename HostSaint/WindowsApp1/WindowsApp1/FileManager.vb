Imports System.Text

Public Class FileManager
    Public sock As Integer
    Private Sub FileManager_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As MouseEventArgs) Handles ListView1.MouseDoubleClick
        Form1.Socket(sock).Send(Encoding.Default.GetBytes("GetFilesFolders" & Form1.Separador & Me.ListView1.SelectedItems(0).Text))
    End Sub
End Class