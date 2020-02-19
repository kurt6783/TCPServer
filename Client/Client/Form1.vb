Public Class Form1
    Dim TCPListener As Net.Sockets.TcpListener
    Dim Client As Net.Sockets.Socket
    Dim Thread_Client As Threading.Thread
    Dim HT As Hashtable = New Hashtable
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CheckForIllegalCrossThreadCalls = False
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim Thread As Threading.Thread = New Threading.Thread(AddressOf Server)
        Thread.IsBackground = False
        Thread.Start()
        Button1.Enabled = False
    End Sub

    Sub Server()
        Dim EP As Net.IPEndPoint = New Net.IPEndPoint(Net.IPAddress.Parse(TextBox1.Text), TextBox2.Text)
        TCPListener = New Net.Sockets.TcpListener(EP)
        TCPListener.Start(100)
        Do
            Client = TCPListener.AcceptSocket
            Thread_Client = New Threading.Thread(AddressOf Listen)
            Thread_Client.IsBackground = False
            Thread_Client.Start()
        Loop
    End Sub


    Sub Listen()
        Dim Socket As Net.Sockets.Socket = Client
        Dim Thread As Threading.Thread = Thread_Client
        Do
            Dim Data(1023) As Byte
            Dim Length As Integer = Socket.Receive(Data)
            Dim Message As String = System.Text.Encoding.Default.GetString(Data, 0, Length)
            Dim Command As String = Message.Substring(0, 1)
            Dim Str As String = Message.Substring(1)
            Select Case Command
                Case 0
                    HT.Add(Str, Socket)
                    ListBox1.Items.Add(Str)
                Case 1
                    Dim text() As String = Str.Split(",")
                    ListBox2.Items.Add(text(0) & " : " & text(1))
                Case 9
                    HT.Remove(Str)
                    ListBox1.Items.Remove(Str)
                    Thread.Abort()
            End Select
        Loop
    End Sub

    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Application.ExitThread()
    End Sub
End Class
