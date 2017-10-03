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
        //IT WILL TURN THE DEV MODE ON IF ITS NOT EMPTY
        public string Path = "";


        public string progress = "";
        public string currentFileName = "";
        public int All = 0;
        public int renameTitle = 0;

        ThreadStart A;
        Thread B;


        public Form1()
        {
            InitializeComponent();
            this.Size = new Size(612, 233);
        }

        List<String> list = new List<String>();
        public void DirSearch(string sDir)
        {
            try
            {
                string[] TempInners = Directory.GetFiles(sDir);

                for (int p = 0; p < TempInners.Length; p++)
                {
                    list.Add(TempInners[p]);
                }

                foreach (string d in Directory.GetDirectories(sDir))
                {
                    foreach (string f in Directory.GetFiles(d))
                    {
                        string xzc = f.Replace("\\", @"\");
                        list.Add(xzc);
                    }
                    DirSearch(d);
                }
            }
            catch (System.Exception excpt)
            {
                MessageBox.Show(excpt.Message);
            }

        }




        private void button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            Path = folderBrowserDialog1.SelectedPath;
            textBox1.Text = Path;
            if (Path == "" || Path == null)
                textBox1.Text = "";
        }


        private void button3_Click(object sender, EventArgs e)
        {
            BrowseItDude.Visible = false;
            BrowseItDude.Location = new Point(585, 173);
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
                Renaming.Visible = false;
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

        private void allCapsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            button1.PerformClick();
        }

        private void menuItem5_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        string ReplaceThis;
        string ReplaceThat;
        private void button5_Click(object sender, EventArgs e)
        {
            ReplaceThis = textBox2.Text;
            ReplaceThat = textBox3.Text;
            A = new ThreadStart(do_Replace);
            B = new Thread(A);
            B.IsBackground = true;
            B.Start();
            timer1.Enabled = true;
            timer2.Enabled = true;
            label6.Left = (this.ClientSize.Width - label6.Width) / 2;
            label7.Left = (this.ClientSize.Width - label7.Width) / 2;
            label5.Left = (this.ClientSize.Width - label5.Width) / 2;
            button4.Left = (this.ClientSize.Width - button4.Width) / 2;
            Renaming.Dock = DockStyle.Fill;
            Renaming.Visible = true;
            Replacer.Visible = false;
        }

        private void menuItem14_Click(object sender, EventArgs e)
        {
            if (Path == "" || Path == null)
            {
                label4.Left = (this.ClientSize.Width - label4.Width) / 2;
                button3.Left = (this.ClientSize.Width - button3.Width) / 2;
                //panel1.Location = new Point(0,0);
                BrowseItDude.Dock = DockStyle.Fill;
                BrowseItDude.Visible = true;
            }
            else
            {
                filePath.Visible = true;
                filePath.Dock = DockStyle.Fill;
                if (DigDirs == true)
                {
                    DirSearch(Path);
                    richTextBox1.Text = string.Join("\r\n", list.ToArray());
                }
                else
                richTextBox1.Text = string.Join("\r\n", Directory.GetFiles(Path));
            }
        }

        private void menuItem7_Click(object sender, EventArgs e)
        {
            if (Path == "" || Path == null)
            {
                label4.Left = (this.ClientSize.Width - label4.Width) / 2;
                button3.Left = (this.ClientSize.Width - button3.Width) / 2;
                //panel1.Location = new Point(0,0);
                BrowseItDude.Dock = DockStyle.Fill;
                BrowseItDude.Visible = true;
            }
            else
            {
                Replacer.Visible = true;
                Replacer.BringToFront();
                Replacer.Dock = DockStyle.Fill;
            }
        }

        private void label22_MouseEnter(object sender, EventArgs e)
        {
            ExitButton.BackColor = Color.FromKnownColor(KnownColor.GradientActiveCaption);
            label26.BackColor = Color.FromKnownColor(KnownColor.GradientActiveCaption);
            label23.BackColor = Color.FromKnownColor(KnownColor.GradientActiveCaption);
            label25.BackColor = Color.FromKnownColor(KnownColor.GradientActiveCaption);
            label22.BackColor = Color.FromKnownColor(KnownColor.GradientActiveCaption);
        }

        private void label22_MouseLeave(object sender, EventArgs e)
        {
            ExitButton.BackColor = Color.FromArgb(224, 224, 224);
            label26.BackColor = Color.FromArgb(224, 224, 224);
            label23.BackColor = Color.FromArgb(224, 224, 224);
            label25.BackColor = Color.FromArgb(224, 224, 224);
            label22.BackColor = Color.FromArgb(224, 224, 224);
        }

        private void label22_Click(object sender, EventArgs e)
        {
            Replacer.Visible = false;
            Adder.Visible = false;
            Remover.Visible = false;
            filePath.Visible = false;
            About.Visible = false;
        }

        private void menuItem9_Click(object sender, EventArgs e)
        {
            if (Path == "" || Path == null)
            {
                label4.Left = (this.ClientSize.Width - label4.Width) / 2;
                button3.Left = (this.ClientSize.Width - button3.Width) / 2;
                //panel1.Location = new Point(0,0);
                BrowseItDude.Dock = DockStyle.Fill;
                BrowseItDude.Visible = true;
            }
            else
            {
                Adder.Visible = true;
                Adder.BringToFront();
                Adder.Dock = DockStyle.Fill;
            }
        }

        private void menuItem8_Click(object sender, EventArgs e)
        {
            if (Path == "" || Path == null)
            {
                label4.Left = (this.ClientSize.Width - label4.Width) / 2;
                button3.Left = (this.ClientSize.Width - button3.Width) / 2;
                //panel1.Location = new Point(0,0);
                BrowseItDude.Dock = DockStyle.Fill;
                BrowseItDude.Visible = true;
            }
            else
            {
                Remover.Visible = true;
                Remover.BringToFront();
                Remover.Dock = DockStyle.Fill;
            }
        }

        private void filePath_Paint(object sender, PaintEventArgs e)
        {

        }
        string RemoveString;
        private void button7_Click(object sender, EventArgs e)
        {
            RemoveString = textBox4.Text;
            A = new ThreadStart(do_Remove);
            B = new Thread(A);
            B.IsBackground = true;
            B.Start();
            timer1.Enabled = true;
            timer2.Enabled = true;
            label6.Left = (this.ClientSize.Width - label6.Width) / 2;
            label7.Left = (this.ClientSize.Width - label7.Width) / 2;
            label5.Left = (this.ClientSize.Width - label5.Width) / 2;
            button4.Left = (this.ClientSize.Width - button4.Width) / 2;
            Renaming.Dock = DockStyle.Fill;
            Renaming.Visible = true;
            Remover.Visible = false;

        }
        string AddString;
        bool AddBool;
        private void button6_Click(object sender, EventArgs e)
        {
            AddString = textBox5.Text;

            if (comboBox1.SelectedItem == "Beginning")
                AddBool = true;
            else
                AddBool = false;

            A = new ThreadStart(do_Add);
            B = new Thread(A);
            B.IsBackground = true;
            B.Start();
            timer1.Enabled = true;
            timer2.Enabled = true;
            label6.Left = (this.ClientSize.Width - label6.Width) / 2;
            label7.Left = (this.ClientSize.Width - label7.Width) / 2;
            label5.Left = (this.ClientSize.Width - label5.Width) / 2;
            button4.Left = (this.ClientSize.Width - button4.Width) / 2;
            Renaming.Dock = DockStyle.Fill;
            Renaming.Visible = true;
            Adder.Visible = false;
        }

        public void do_Replace()
        {
            string[] FileList;

            if (DigDirs == true)
            {
                DirSearch(Path);
                FileList = list.ToArray();
            }
            else
            {
                FileList = Directory.GetFiles(Path);
            }
            All = FileList.Length;
            for (int i = 0; i < FileList.Length; i++)
            {
                string Temp = FileList[i].Replace(Path + @"\", "");

                Temp = Temp.Replace(ReplaceThis, ReplaceThat);

                progress = i + 1 + @"\" + FileList.Length;
                currentFileName = Temp;

                Temp = Path + @"\" + Temp;
                try
                {
                    System.IO.File.Move(FileList[i], Temp);
                }
                catch (System.Exception excpt)
                {
                    MessageBox.Show(excpt.Message);
                }
            }


            string[] TempDirs = Directory.GetDirectories(Path);
            for (int w = 0; w < TempDirs.Length; w++)
            {
                string Temp = TempDirs[w].Replace(Path + @"\", "");



                Temp = Temp.Replace(ReplaceThis, ReplaceThat);



                currentFileName = Temp;
                Temp = Path + @"\" + Temp;
                try
                {
                    System.IO.Directory.Move(TempDirs[w], Temp);
                }
                catch (System.Exception excpt)
                {
                    MessageBox.Show(excpt.Message);
                }
            }




        }


        public void do_Remove()
        {

            string[] FileList;

            if (DigDirs == true)
            {
                DirSearch(Path);
                FileList = list.ToArray();
            }
            else
            {
                FileList = Directory.GetFiles(Path);
            }

            All = FileList.Length;
            string[] enterSepped = RemoveString.Split(new[] { "\r\n" }, StringSplitOptions.None);

            for (int i = 0; i < FileList.Length; i++)
            {
                string Temp = FileList[i].Replace(Path + @"\", "");

                for (int x = 0; x < enterSepped.Length; x++)
                {
                    Temp = Temp.Replace(enterSepped[x], "");
                }


                progress = i + 1 + @"\" + FileList.Length;
                currentFileName = Temp;

                Temp = Path + @"\" + Temp;

                try
                {
                    System.IO.File.Move(FileList[i], Temp);
                }
                catch (System.Exception excpt)
                {
                    MessageBox.Show(excpt.Message);
                }
            }

            string[] TempDirs = Directory.GetDirectories(Path);
            for (int w = 0; w < TempDirs.Length; w++)
            {
                string Temp = TempDirs[w].Replace(Path + @"\", "");



                for (int x = 0; x < enterSepped.Length; x++)
                {
                    Temp = Temp.Replace(enterSepped[x], "");
                }



                currentFileName = Temp;
                Temp = Path + @"\" + Temp;
                try
                {
                    System.IO.Directory.Move(TempDirs[w], Temp);
                }
                catch (System.Exception excpt)
                {
                    MessageBox.Show(excpt.Message);
                }
            }




        }


        public void do_Add()
        {
            string[] FileList;

            if (DigDirs == true)
            {
                DirSearch(Path);
                FileList = list.ToArray();
            }
            else
            {
                FileList = Directory.GetFiles(Path);
            }

            All = FileList.Length;
            for (int i = 0; i < FileList.Length; i++)
            {
                string Temp = FileList[i].Replace(Path + @"\", "");
                if(AddBool)
                    Temp = AddString + Temp;
                else
                    Temp = Temp + AddString;

                progress = i + 1 + @"\" + FileList.Length;
                currentFileName = Temp;

                Temp = Path + @"\" + Temp;

                try
                {
                    System.IO.File.Move(FileList[i], Temp);
                }
                catch (System.Exception excpt)
                {
                    MessageBox.Show(excpt.Message);
                }
            }

            string[] TempDirs = Directory.GetDirectories(Path);
            for (int w = 0; w < TempDirs.Length; w++)
            {
                string Temp = TempDirs[w].Replace(Path + @"\", "");



                if (AddBool)
                    Temp = AddString + Temp;
                else
                    Temp = Temp + AddString;



                currentFileName = Temp;
                Temp = Path + @"\" + Temp;
                try
                {
                    System.IO.Directory.Move(TempDirs[w], Temp);
                }
                catch (System.Exception excpt)
                {
                    MessageBox.Show(excpt.Message);
                }
            }



        }



        public void do_AllCaps()
        {
            string[] FileList;

            if (DigDirs == true)
            {
                DirSearch(Path);
                FileList = list.ToArray();
            }
            else
            {
                FileList = Directory.GetFiles(Path);
            }
            All = FileList.Length;
            for (int i = 0; i < FileList.Length; i++)
            {
                string Temp = FileList[i].Replace(Path + @"\", "");

                Temp = Temp.ToUpper();

                progress = i + 1 + @"\" + FileList.Length;
                currentFileName = Temp;

                Temp = Path + @"\" + Temp;

                try
                {
                    System.IO.File.Move(FileList[i], Temp);
                }
                catch (System.Exception excpt)
                {
                    MessageBox.Show(excpt.Message);
                }
            }


            string[] TempDirs = Directory.GetDirectories(Path);
            for (int w = 0; w < TempDirs.Length; w++)
            {
                string Temp = TempDirs[w].Replace(Path + @"\", "");



                Temp = Temp.ToUpper();



                currentFileName = Temp;
                Temp = Path + @"\" + Temp;
                try
                {
                    System.IO.Directory.Move(TempDirs[w], "Temp");
                    System.IO.Directory.Move("Temp", Temp);
                }
                catch (System.Exception excpt)
                {
                    MessageBox.Show(excpt.Message);
                }
            }


        }


        public void do_AllLower()
        {
            string[] FileList;

            if (DigDirs == true)
            {
                DirSearch(Path);
                FileList = list.ToArray();
            }
            else
            {
                FileList = Directory.GetFiles(Path);
            }
            All = FileList.Length;
            for (int i = 0; i < FileList.Length; i++)
            {
                string Temp = FileList[i].Replace(Path + @"\", "");

                Temp = Temp.ToLower();

                progress = i + 1 + @"\" + FileList.Length;
                currentFileName = Temp;

                Temp = Path + @"\" + Temp;

                try
                {
                    System.IO.File.Move(FileList[i], Temp);
                }
                catch (System.Exception excpt)
                {
                    MessageBox.Show(excpt.Message);
                }
            }

            string[] TempDirs = Directory.GetDirectories(Path);
            for (int w = 0; w < TempDirs.Length; w++)
            {
                string Temp = TempDirs[w].Replace(Path + @"\", "");



                Temp = Temp.ToLower();



                currentFileName = Temp;
                Temp = Path + @"\" + Temp;
                try
                {
                    System.IO.Directory.Move(TempDirs[w], "Temp");
                    System.IO.Directory.Move("Temp", Temp);
                }
                catch (System.Exception excpt)
                {
                    MessageBox.Show(excpt.Message);
                }
            }


        }



        public void do_CapIt()
        {
            string[] FileList;

            if (DigDirs == true)
            {
                DirSearch(Path);
                FileList = list.ToArray();
            }
            else
            {
                FileList = Directory.GetFiles(Path);
            }

            All = FileList.Length;
            for (int i = 0; i < FileList.Length; i++)
            {
                string Temp = FileList[i].Replace(Path + @"\", "");

                Temp = Temp.First().ToString().ToUpper() + Temp.Substring(1);
                Temp = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Temp.ToLower());

                progress = i + 1 + @"\" + FileList.Length;
                currentFileName = Temp;

                Temp = Path + @"\" + Temp;

                try
                {
                    System.IO.File.Move(FileList[i], Temp);
                }
                catch (System.Exception excpt)
                {
                    MessageBox.Show(excpt.Message);
                }
            }



            string[] TempDirs = Directory.GetDirectories(Path);
            for (int w = 0; w < TempDirs.Length; w++)
            {
                string Temp = TempDirs[w].Replace(Path + @"\", "");
                Temp = Temp.First().ToString().ToUpper() + Temp.Substring(1);
                Temp = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Temp.ToLower());




                currentFileName = Temp;
                Temp = Path + @"\" + Temp;
                try
                {
                    System.IO.Directory.Move(TempDirs[w], "Temp");
                    System.IO.Directory.Move("Temp", Temp);
                }
                catch (System.Exception excpt)
                {
                    MessageBox.Show(excpt.Message);
                }
            }



        }



        private void menuItem15_Click(object sender, EventArgs e)
        {

            if (Path == "" || Path == null)
            {
                label4.Left = (this.ClientSize.Width - label4.Width) / 2;
                button3.Left = (this.ClientSize.Width - button3.Width) / 2;
                //panel1.Location = new Point(0,0);
                BrowseItDude.Dock = DockStyle.Fill;
                BrowseItDude.Visible = true;
            }
            else
            {
                RemoveString = textBox4.Text;
                A = new ThreadStart(do_CapIt);
                B = new Thread(A);
                B.IsBackground = true;
                B.Start();
                timer1.Enabled = true;
                timer2.Enabled = true;
                label6.Left = (this.ClientSize.Width - label6.Width) / 2;
                label7.Left = (this.ClientSize.Width - label7.Width) / 2;
                label5.Left = (this.ClientSize.Width - label5.Width) / 2;
                button4.Left = (this.ClientSize.Width - button4.Width) / 2;
                Renaming.Dock = DockStyle.Fill;
                Renaming.Visible = true;
            }
        }

        private void menuItem11_Click(object sender, EventArgs e)
        {
            if (Path == "" || Path == null)
            {
                label4.Left = (this.ClientSize.Width - label4.Width) / 2;
                button3.Left = (this.ClientSize.Width - button3.Width) / 2;
                //panel1.Location = new Point(0,0);
                BrowseItDude.Dock = DockStyle.Fill;
                BrowseItDude.Visible = true;
            }
            else
            {
                RemoveString = textBox4.Text;
                A = new ThreadStart(do_AllCaps);
                B = new Thread(A);
                B.IsBackground = true;
                B.Start();
                timer1.Enabled = true;
                timer2.Enabled = true;
                label6.Left = (this.ClientSize.Width - label6.Width) / 2;
                label7.Left = (this.ClientSize.Width - label7.Width) / 2;
                label5.Left = (this.ClientSize.Width - label5.Width) / 2;
                button4.Left = (this.ClientSize.Width - button4.Width) / 2;
                Renaming.Dock = DockStyle.Fill;
                Renaming.Visible = true;
            }
        }

        private void menuItem12_Click(object sender, EventArgs e)
        {
            if (Path == "" || Path == null)
            {
                label4.Left = (this.ClientSize.Width - label4.Width) / 2;
                button3.Left = (this.ClientSize.Width - button3.Width) / 2;
                //panel1.Location = new Point(0,0);
                BrowseItDude.Dock = DockStyle.Fill;
                BrowseItDude.Visible = true;
            }
            else
            {
                RemoveString = textBox4.Text;
                A = new ThreadStart(do_AllLower);
                B = new Thread(A);
                B.IsBackground = true;
                B.Start();
                timer1.Enabled = true;
                timer2.Enabled = true;
                label6.Left = (this.ClientSize.Width - label6.Width) / 2;
                label7.Left = (this.ClientSize.Width - label7.Width) / 2;
                label5.Left = (this.ClientSize.Width - label5.Width) / 2;
                button4.Left = (this.ClientSize.Width - button4.Width) / 2;
                Renaming.Dock = DockStyle.Fill;
                Renaming.Visible = true;
            }
        }

        public bool DigDirs = false;
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            DigDirs = checkBox1.Checked;
            File.WriteAllText("Renamer.conf", RenameDirs + "," + DigDirs);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Path = textBox1.Text;
        }

        bool RenameDirs = false;
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            RenameDirs = checkBox2.Checked;
            File.WriteAllText("Renamer.conf", RenameDirs + "," + DigDirs);
        }

        private void menuItem16_Click(object sender, EventArgs e)
        {
            Settings.Visible = true;
            Settings.BringToFront();
            Settings.Dock = DockStyle.Fill;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Settings.Visible = false;
        }

        private void menuItem17_Click(object sender, EventArgs e)
        {
            if (Path == "" || Path == null)
            {
                label4.Left = (this.ClientSize.Width - label4.Width) / 2;
                button3.Left = (this.ClientSize.Width - button3.Width) / 2;
                //panel1.Location = new Point(0,0);
                BrowseItDude.Dock = DockStyle.Fill;
                BrowseItDude.Visible = true;
            }
            else
            {
                ThreadStart h = new ThreadStart(EFI);
                Thread g = new Thread(h);
                g.IsBackground = true;
                g.Start();
            }
        }
        public void EFI()
        {
            if (DigDirs == true)
            {
                DirSearch(Path);
                string dixs = "Directory_Index-[WithSubs][" + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + "].txt";
                StreamWriter sw = new StreamWriter(dixs);
                string[] arr = list.ToArray();
                for (int i = 0; i < arr.Length; i++)
                {
                    sw.WriteLine(arr[i]);
                }
                sw.Close();
                System.Diagnostics.Process.Start(dixs);
            }
            else
            {
                string dix = "Directory_Index-[" + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + "].txt";
                StreamWriter sw = new StreamWriter(dix);
                string[] arr = Directory.GetFiles(Path);
                for (int i = 0; i < arr.Length; i++)
                {
                    sw.WriteLine(arr[i]);
                }
                sw.Close();
                System.Diagnostics.Process.Start(dix);
            
            
            }
        }

        private void menuItem3_Click(object sender, EventArgs e)
        {
            About.Visible = true;
            About.BringToFront();
            About.Dock = DockStyle.Fill;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                string[] conf = File.ReadAllText("Renamer.conf").Split(',');
                RenameDirs = conf[0] == "True";
                DigDirs = conf[1] == "True";
                checkBox1.Checked = DigDirs;
                checkBox2.Checked = RenameDirs;
            }
            catch { /*swallow*/ } 
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }


    }
}
