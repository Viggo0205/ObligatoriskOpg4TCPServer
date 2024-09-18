
using System;
using System.Diagnostics.Metrics;
using System.IO;
using System.Net;
using System.Net.Sockets;
//using System.Threading.Tasks;

Console.WriteLine("TCP server 1");

TcpListener listener = new TcpListener(IPAddress.Any, 7);
listener.Start();
while (true)
{
    TcpClient socket = listener.AcceptTcpClient();
    Task.Run(() => HandleClient(socket));
}

void HandleClient(TcpClient socket)
{

    NetworkStream ns = socket.GetStream();
    StreamReader reader = new StreamReader(ns);
    StreamWriter writer = new StreamWriter(ns);
    int count = 0;
    while (socket.Connected)
    {

        string message = reader.ReadLine().ToLower();
        count++;
        Console.WriteLine(message);
        if (message == "stop")
        {
            writer.WriteLine("Goodbye world");
            writer.Flush();
            socket.Close();
        } 
        else if (message.Contains("random"))
        {
            writer.WriteLine("indtast tal");
            writer.Flush();
            string talR = reader.ReadLine();
            string[] messageA = talR.Split(" ");
            int x = Int32.Parse(messageA[0]);
            int y = Int32.Parse(messageA[1]);
            Console.WriteLine(x);
            Console.WriteLine(y);

            Random random = new Random();
            int n = random.Next(x, y);
            string sn = n.ToString();
            writer.WriteLine(sn);
            writer.Flush();

        }
        else if (message.Contains("add"))
        {
            writer.WriteLine("indtast tal");
            writer.Flush();
            string talA = reader.ReadLine();
            string[] messageB = talA.Split(" ");
            int x2 = Int32.Parse(messageB[0]);
            int y2 = Int32.Parse(messageB[1]);
            writer.WriteLine(x2 + y2);
            writer.Flush();
        }
        else if (message.Contains("subtract"))
        {
            writer.WriteLine("indtast tal(tal 1 - tal 2)");
            writer.Flush();
            string talS = reader.ReadLine();
            string[] messageB = talS.Split(" ");
            int x2 = Int32.Parse(messageB[0]);
            int y2 = Int32.Parse(messageB[1]);
            writer.WriteLine(x2 - y2);
            writer.Flush();
        }
    }
}


//listener.Stop();

