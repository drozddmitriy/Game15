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

namespace Image_game
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        void Init()
        {
            string[] files_bgr = Directory.GetFiles("../../bgr");
            string[] files = Directory.GetFiles("../../img");
            int image_index = 0;
            int count = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Button btn = new Button();
                    btn.BackgroundImageLayout = ImageLayout.Stretch;
                    btn.BackgroundImage = Image.FromFile(files_bgr[0]);
                    btn.Tag = files[image_index];
                    count++;
                    if (count % 2 == 0)
                    {
                        image_index++;
                    }
                    btn.Size = new Size(this.ClientSize.Width / 4, this.ClientSize.Height / 4);
                    btn.Location = new Point((this.ClientSize.Width / 4) * i, (this.ClientSize.Height / 4) * j);
                    btn.Click += Btn_Click;
                    panel1.Controls.Add(btn);

                }
            }
            Random_();
            this.Resize += Form1_Resize;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            int x = 0, y = 0;
            for (int j = 1; j < 17; j++)
            {
                panel1.Controls[j - 1].Size = new Size(this.ClientSize.Width / 4, this.ClientSize.Height / 4);
                panel1.Controls[j - 1].Location = new Point((this.ClientSize.Width / 4) * x, (this.ClientSize.Height / 4) * y);
                x++;
                if (j % 4 == 0)
                {
                    y++;
                    x = 0;
                }
            }

        }

        Button btn1, btn2 = null;
        private void Btn_Click(object sender, EventArgs e)
        {

            if (btn1 == null)
            {
                btn1 = ((Button)sender);
                btn1.BackgroundImage = Image.FromFile(((Button)sender).Tag.ToString());
            }
            else
            {
                btn2 = ((Button)sender);
                btn2.BackgroundImage = Image.FromFile(((Button)sender).Tag.ToString());
                panel1.Enabled = false;
                Timer t = new Timer();
                t.Interval = 1000;
                t.Tick += T_Tick;
                t.Start();
            }
        }

        private void T_Tick(object sender, EventArgs e)
        {

            if (btn1.Tag != btn2.Tag)
            {
                string[] files_bgr = Directory.GetFiles("../../bgr");
                btn1.BackgroundImage = Image.FromFile(files_bgr[0]);
                btn2.BackgroundImage = Image.FromFile(files_bgr[0]);
            }
            else
            {
                btn1.Visible = false;
                btn2.Visible = false;
            }
            btn1 = btn2 = null;
            panel1.Enabled = true;
            ((Timer)sender).Stop();
            ((Timer)sender).Dispose();
            Win();
        }

        void Random_()
        {
            Random r = new Random();
            for (int i = 0; i < 100; i++)
            {
                int a = r.Next(0, 15);
                int b = r.Next(0, 15);
                string str = panel1.Controls[a].Tag.ToString();
                panel1.Controls[a].Tag = panel1.Controls[b].Tag;
                panel1.Controls[b].Tag = str;
            }
        }

        public void Win()
        {
            bool win = true;
            foreach (Control v in panel1.Controls)
            {
                if (v.Visible == true)
                {
                    win = false;
                    break;
                }
            }
            if (win)
            {
                MessageBox.Show("YOU WIN !");
                Init();
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Init();
        }
    }
}
