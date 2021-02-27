using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjektZajecia1Client {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();

            

        }

        private void button1_Click(object sender, EventArgs e) {

            TcpClient client = new TcpClient();
            var result = client.BeginConnect("192.168.195.167", 8001, null, null); //Spróbuj połączyć z serverem
            var success = result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(5)); //True jesli się połączy, czeka 5s
            success = client.Connected;
            if (!success) { //jeśli nie połączy
                //client.EndConnect(result);
                label1.Text = "Nie dziala Sadge";

            }
            else label1.Text = "Dziala WidePeepoHappy";
            //client.EndConnect(result);
        }
    }
}
