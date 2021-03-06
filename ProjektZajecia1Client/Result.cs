using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjektZajecia1Client {
    public partial class Result : UserControl {
        public Result() {
            InitializeComponent();
        }

        private void Result_VisibleChanged(object sender, EventArgs e) {

            if (this.Visible == true) {
                var parent = this.Parent as Form1;
                Controls.Clear();
                switch (parent.data[0]) {
                    case 1:
                        string someString = Encoding.ASCII.GetString(parent.data.Skip(1).ToArray());
                        Label temp = new Label();
                        temp.Text = someString;
                        temp.Dock = DockStyle.Fill;
                        Controls.Add(temp);
                        break;
                    case 2:
                        int x = parent.data[1];
                        Label temp2 = new Label();
                        temp2.Text = "Wynik dzialania to " + x;
                        temp2.Dock = DockStyle.Fill;
                        Controls.Add(temp2);
                        break;
                    case 3:
                        PictureBox temp3 = new PictureBox();
                        Image img;
                        byte[] tempdata = parent.data.Skip(1).ToArray();
                        using (var ms = new MemoryStream(tempdata)) {
                            img =  Image.FromStream(ms);
                        }
                        temp3.Image = img;
                        temp3.Dock = DockStyle.Fill;
                        Controls.Add(temp3);
                        break;
                    default:

                        break;
                }
            }
            
        }
    }
}
