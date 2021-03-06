using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Drawing;

namespace ProjekZajecia1Server {
    class Program {
        /// <summary>
        /// funkcja od wysyłania stringa
        /// </summary>
        /// <param name="s"></param>
        static void sendString(Socket s) { //funkcja od wysyłania stringa
            string test = "String testowy do wyslania sialalala";
            byte[] bytes = Encoding.ASCII.GetBytes(test);
            byte[] byteFinal = new byte[bytes.Length + 1];
            byteFinal[0] = 1;
            for (int i = 0; i < bytes.Length; i++) byteFinal[i + 1] = bytes[i];
            s.Send(byteFinal);
        }
        /// <summary>
        /// Funkcja od wysyłania wyniku działania
        /// </summary>
        /// <param name="s"></param>
        /// <param name="test"></param>
        static void sendNumbers(Socket s, byte[] test) { //funkcja od wysyłania wyniku działania
            byte[] temp = new byte[2];
            temp[0] = 2;
            temp[1] = (byte)(test[1] + test[2]);
            s.Send(temp);
        }
        /// <summary>
        /// Funkcja od wysyłania obrazu
        /// </summary>
        /// <param name="s"></param>
        static void sendPicutre(Socket s) { 
            Bitmap test = new Bitmap("C:\\Users\\Artur\\source\\repos\\ProjekZajecia1\\ProjekZajecia1\\pic.jpg");
            byte[] dataToSend;
            using (var stream = new MemoryStream()) {
                test.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                dataToSend = stream.ToArray();
            }
            byte[] byteFinal = new byte[dataToSend.Length + 1];
            byteFinal[0] = 3;
            for (int i = 0; i < dataToSend.Length; i++) byteFinal[i + 1] = dataToSend[i];
            s.Send(byteFinal);
        }

        static void Main(string[] args) {
            
            //--dane servera
            const int PORT_NO = 8001;
            const string SERVER_IP = "192.168.195.167"; //tu zmień adres na swój dla testów
            IPAddress localAdd = IPAddress.Parse(SERVER_IP);
            TcpListener listener = new TcpListener(localAdd, PORT_NO);

            
            Console.WriteLine("Czekam na polaczenie");
            listener.Start();
            Socket s = listener.AcceptSocket();

            Console.WriteLine("Connection accepted from " + s.RemoteEndPoint);

            while (true) {
                byte[] data = new byte[3];
                Console.WriteLine("Czekam na dane");
                s.Receive(data); 
                
                switch (data[0]) { // w zależności od pierwszego bitu otrzymanych danych wykonuje inną funkcję
                    case 1:
                        sendString(s);
                        break;
                    case 2:
                        sendNumbers(s, data);
                        break;
                    case 3:
                        sendPicutre(s);
                        break;
                    default:
                        break;
                }

                Console.WriteLine("Wyslano wariant " + data[0]);

            }

        }
    }
}
