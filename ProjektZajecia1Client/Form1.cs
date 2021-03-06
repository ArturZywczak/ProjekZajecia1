using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjektZajecia1Client {
    public partial class Form1 : Form {
        Stream stm; //Tutaj jest przechowywane połącznie
        public byte[] data; //dane odbierane
        public Form1() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {

            TcpClient client = new TcpClient();
            var result = client.BeginConnect("192.168.195.167", 8001, null, null); //Spróbuj połączyć z serverem, tutaj wpisz adres swojej karty do testów
            var success = result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(5)); //True jesli się połączy, czeka 5s
            success = client.Connected;
            if (!success) { //jeśli nie połączy
                label1.Text = "Nie dziala Sadge";
            }
            else {
                stm = client.GetStream();
                polaczono(); 
            }
            //client.EndConnect(result);
        }
        /// <summary>
        /// Połaczono, odpala przyciski
        /// </summary>
        private void polaczono() {

            result1.Visible = false;
            label1.Text = "Dziala WidePeepoHappy";
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button1.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e) { //Przycisk z tekstem

            result1.Visible = false;
            byte[] command = new byte[1];
            command[0] = 1;

            stm.Write(command, 0, 1);
            waitForData();
        }

        private void button3_Click(object sender, EventArgs e) { //Przycisk z działaniem
            result1.Visible = false;
            byte command = 2;
            byte[] dataToSend = new byte[3];
            dataToSend[0] = command;
            dataToSend[1] = (byte)Int32.Parse(textBox1.Text);
            dataToSend[2] = (byte)Int32.Parse(textBox2.Text);
            stm.Write(dataToSend,0,dataToSend.Length);
            waitForData();

        }

        private void button4_Click(object sender, EventArgs e) { //przycisk z obrazkiem

            result1.Visible = false;
            byte[] command = new byte[1];
            command[0] = 3;

            stm.Write(command, 0, 1);
            waitForData();
        }

        private void waitForData() {

            stm.Flush();

            byte[] bytesToRead = new byte[1000000];
            int bytesRead = stm.Read(bytesToRead, 0, bytesToRead.Length);
            data = bytesToRead;
            result1.Visible = true; //fuszera tylko po to aby pokazać jak to działa, kontrolka sie odpala i w zalezności od pierwszego bitu tworzy konkretną wartość;
        }

    }
}
