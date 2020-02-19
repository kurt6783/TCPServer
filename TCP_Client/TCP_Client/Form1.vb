Imports System.Net.Sockets

Public Class Form1

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim ClientThread As Threading.Thread = New Threading.Thread(AddressOf Client)
        ClientThread.IsBackground = False
        ClientThread.Start()
    End Sub
    Sub Client()
        Dim TCPClient As TcpClient = New TcpClient(TextBox1.Text, TextBox2.Text)
        Dim NetworkStream As NetworkStream = TCPClient.GetStream
        Dim Receive(0) As Byte
        Do
            If NetworkStream.DataAvailable = True Then
                NetworkStream.Read(Receive, 0, 1)
                TextBox5.Text = TextBox5.Text & System.Text.Encoding.Default.GetString(Receive)
            End If
        Loop
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CheckForIllegalCrossThreadCalls = False
    End Sub
End Class
