using System;
using System.Net;
using System.Net.Sockets;

namespace ProjekZajecia1Server {
    class Program {
        static void Main(string[] args) {
            
            //--dane servera
            const int PORT_NO = 8001;
            const string SERVER_IP = "192.168.195.167";
            IPAddress localAdd = IPAddress.Parse(SERVER_IP);
            TcpListener listener = new TcpListener(localAdd, PORT_NO);

            
            Console.WriteLine("Czekam na polaczenie");
            listener.Start();
            Socket s = listener.AcceptSocket();

            Console.WriteLine("Connection accepted from " + s.RemoteEndPoint);


        }
    }
}
