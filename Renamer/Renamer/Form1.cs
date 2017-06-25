using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Globalization;

namespace Renamer
{
    public partial class Form1 : Form
    {
        public string Path = "";
        public bool C = false;
        public bool D = false;
        public bool S = false;
        public bool U = false;

        public string progress = "";
        public string currentFileName = "";
        public int All = 0;
        public int renameTitle = 0;

        ThreadStart A;
        Thread B;


        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            Path = folderBrowserDialog1.SelectedPath;
            textBox1.Text = Path;
            if (Path == "" || Path == null)
                textBox1.Text = "";
        }
        public void Renamer()
        {
            string[] FileList = Directory.GetFiles(Path);
            All = FileList.Length;
            for (int i = 0; i < FileList.Length; i++)
            {
                string Temp = FileList[i].Replace(Path+@"\","");

                if (U)
                {
                    Temp = Temp.Replace('_', '-');
                }
                if (D)
                {
                    Temp = Temp.Replace('-', ' ');
                }
                if (S)
                {
                    Temp = Temp.Replace('_', ' ');
                }

                if (C)
                {
                    Temp = Temp.First().ToString().ToUpper() + Temp.Substring(1);
                    Temp = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Temp.ToLower());
                }

                progress = i + 1 + @"\" + FileList.Length;
                currentFileName = Temp;

                Temp = Path + @"\" + Temp;

                System.IO.File.Move(FileList[i], Temp);
                
            }

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            C = checkBox1.Checked;
            D = checkBox2.Checked;
            S = checkBox3.Checked;
            U = checkBox4.Checked;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Path =="" || Path == null)
            {
                panel1.Location = new Point(19, 12);
                panel1.Visible = true;
            }
            else
            {
                A = new ThreadStart(Renamer);
                B = new Thread(A);
                B.IsBackground = true;
                B.Start();
                timer1.Enabled = true;
                timer2.Enabled = true;
                panel2.Location = new Point(19, 12);
                panel2.Visible = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel1.Location = new Point(585, 173);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (renameTitle)
            {
                case 0:
                    label5.ForeColor = Color.FromArgb(15, 15,15,15);
                    renameTitle++;
                    break;
                case 1:
                    label5.ForeColor = Color.FromArgb(95,95,95,95);
                    renameTitle = 0;
                    break;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            label6.Text = currentFileName;
            label7.Text = progress;
            if (B.IsAlive == false)
            {
                panel2.Visible = false;
                timer1.Enabled = false;

                if (!label2.Text.Contains("Renamed "))
                {
                    label2.Text = "Renamed " + All + " Files!";
                }

                timer3.Enabled = true;
                timer2.Enabled = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (B != null)
             B.Suspend();
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            
                label2.Text = "The Renamer";
                timer3.Enabled = false;

        }
    }
}
