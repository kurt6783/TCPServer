

Public Class Form1

    Dim Socket As Net.Sockets.Socket
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Button1.Enabled = True
        Button3.Enabled = False
        Button2.Enabled = False
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim EP As Net.IPEndPoint = New Net.IPEndPoint(Net.IPAddress.Parse(TextBox1.Text), TextBox2.Text)
        Socket = New Net.Sockets.Socket(Net.Sockets.AddressFamily.InterNetwork, Net.Sockets.SocketType.Stream, Net.Sockets.ProtocolType.Tcp)
        Socket.Connect(EP)
        Dim SendData() As Byte
        SendData = System.Text.Encoding.Default.GetBytes("0" & TextBox3.Text)
        Socket.Send(SendData, 0, SendData.Length, Net.Sockets.SocketFlags.None)
        TextBox3.Enabled = False
        Button1.Enabled = False
        Button3.Enabled = True
        Button2.Enabled = True
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim SendData() As Byte
        SendData = System.Text.Encoding.Default.GetBytes("9" & TextBox3.Text)
        Socket.Send(SendData, 0, SendData.Length, Net.Sockets.SocketFlags.None)
        Button1.Enabled = True
        Button3.Enabled = False
        Button2.Enabled = False
    End Sub


    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim SendData() As Byte
        SendData = System.Text.Encoding.Default.GetBytes("1" & TextBox3.Text & "," & TextBox4.Text)
        Socket.Send(SendData, 0, SendData.Length, Net.Sockets.SocketFlags.None)
    End Sub
End Class
