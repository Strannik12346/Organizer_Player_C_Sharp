using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Media;
using System.IO;

namespace Player
{
    public partial class Form1 : Form
    {
        string location; //Путь, который выбирает пользователь
        string name;
        string[] directories;
        string[] names;
        int ActiveTrack = -1;
        bool paused = false;
        MediaPlayer MyPlayer = new MediaPlayer();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BtnClose.FlatAppearance.BorderSize = 0;
            BtnMinimize.FlatAppearance.BorderSize = 0;
            button1.FlatAppearance.BorderSize = 0;
            button2.FlatAppearance.BorderSize = 0;
            button3.FlatAppearance.BorderSize = 0;
            button4.FlatAppearance.BorderSize = 0;
            listBox.SelectedIndex = -1;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void BtnOpen_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.ShowDialog();
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (folderBrowserDialog.SelectedPath != "")
            {
                listBox.Items.Clear();
                location = folderBrowserDialog.SelectedPath;
                timer.Stop();
                directories = Directory.GetFiles(location);
                names = directories;
                string line; // обрабатываемая переменная
                for (int i = 0; i <= directories.Length - 1; i++)
                {
                    line = directories[i];
                    while (line.IndexOf('\\') != -1)
                    {
                        line = line.Remove(0, line.IndexOf('\\') + 1);
                    }
                    names[i] = line;
                    string cutline = line.Remove(line.LastIndexOf('.'), line.Length - line.LastIndexOf('.'));
                    listBox.Items.Add(cutline);
                }
            }
        }

        private void listBox_DoubleClick(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex != -1)
            {
                ActiveTrack = listBox.SelectedIndex;
                Uri MyUri = new Uri(location + "\\" + directories[ActiveTrack]);
                writename();
                MyPlayer.Stop();
                MyPlayer.Open(MyUri);
                MyPlayer.Play();
                MyPlayer.MediaEnded += new EventHandler(MyPlayer_MediaEnded);
            }
            listBox.SelectedIndex = -1;
        }

        private void MyPlayer_MediaEnded(object sender, EventArgs e)
        {
            if (ActiveTrack != names.Length - 1)
            {
                ActiveTrack++;
            }
            else
            {
                ActiveTrack = 0;
            }
            Uri MyUri = new Uri(location + "\\" + directories[ActiveTrack]);
            writename();
            MyPlayer.Stop();
            MyPlayer.Open(MyUri);
            MyPlayer.Play();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ActiveTrack != 0)
            {
                ActiveTrack--;
            }
            else
            {
                ActiveTrack = names.Length - 1;
            }
            Uri MyUri = new Uri(location + "\\" + directories[ActiveTrack]);
            writename();
            MyPlayer.Stop();
            MyPlayer.Open(MyUri);
            MyPlayer.Play();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (paused == false)
            {
                MyPlayer.Pause();
                paused = true;
            }
            else
            {
                MyPlayer.Play();
                paused = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MyPlayer.Stop();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (ActiveTrack != directories.Length - 1)
            {
                ActiveTrack++;
            }
            else
            {
                ActiveTrack = 0;
            }
            Uri MyUri = new Uri(location + "\\" + directories[ActiveTrack]);
            writename();
            MyPlayer.Stop();
            MyPlayer.Open(MyUri);
            MyPlayer.Play();
        }

        private void writename()
        {
            name = names[ActiveTrack];
            if (name.IndexOf('-') > 0)
            {
                name = name.Remove(0, name.IndexOf('-') + 2);
            }
            name = name.Remove(name.LastIndexOf('.'), name.Length - name.LastIndexOf('.'));
            label.Text = name;
        }

        private void listBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                ActiveTrack = listBox.SelectedIndex;
                Uri MyUri = new Uri(location + "\\" + directories[ActiveTrack]);
                writename();
                MyPlayer.Stop();
                MyPlayer.Open(MyUri);
                MyPlayer.Play();
                MyPlayer.MediaEnded += new EventHandler(MyPlayer_MediaEnded);
            }
        }
    }
}
