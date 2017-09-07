using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO; // Подключаем пространство имен для работы с файлами

namespace Organizer
{
    public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && richTextBox1.Text != "")
            {
                using (StreamWriter text1 = File.AppendText("events.txt")) // Открываем файл для дописи
                {
                    text1.WriteLine(textBox1.Text);
                    text1.WriteLine(textBox2.Text);
                    text1.WriteLine(richTextBox1.Text);
                    listBox1.Items.Add(textBox1.Text + "   " + textBox2.Text); // Вписываем дату и событие в listBox
                }
            }
            else
            {
                MessageBox.Show("Проверьте поля ввода! Возможно вы ввели не всю информацию!"); // Сообщение об ошибке ввода
            }

            textBox1.Clear();
            textBox2.Clear();
            richTextBox1.Clear();
        }

        private void button2_Click(object sender, EventArgs e) // Очистка формы
        {
            textBox1.Clear();
            textBox2.Clear();
            richTextBox1.Clear();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            FileInfo Events = new FileInfo("events.txt");
            if (!Events.Exists)
            {
                FileStream Stream = Events.Create(); // Создание файла если его не существовало
                Stream.Close(); // Закрытие потока
            }
            else
            {
                using (StreamReader text2 = new StreamReader("events.txt"))
                {
                    string a = "event", b = "date", c = "description";

                    while (a != null) // Вывод сохраненных данных из файла
                    {
                        a = text2.ReadLine();
                        b = text2.ReadLine();
                        c = text2.ReadLine();
                        if (a != "" && a != null)
                        {
                            listBox1.Items.Add(a + "   " + b);
                        }
                    }
                }
            }
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            using (StreamReader text3 = new StreamReader("events.txt"))
            {
                string[] f;
                f = new string[999];
                int x = 1;

                for (x = 1; x <= 99; x++)
                {
                    f[x] = text3.ReadLine();
                }
                label4.Text = f[3 * (listBox1.SelectedIndex + 1)];
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            string[] f;
            f = new string[999];
            int x = 0;

            using (StreamReader text3 = new StreamReader("events.txt"))
            {
                for (x = 0; x <= 99; x++)
                {
                    {
                        f[x] = text3.ReadLine();
                    }
                }
            }

            FileInfo Events1 = new FileInfo("events.txt");
            Events1.Delete();

            FileStream Stream1 = Events1.Create(); // Создание файла
            Stream1.Close(); // Закрытие потока

            using (StreamWriter text1 = File.AppendText("events.txt")) // Открываем файл для дописи
            {
                if (listBox1.SelectedIndex == 0)
                {
                    for (x = 3; x <= 99; x++)
                    {
                        if (f[x] != null && f[x] != "")
                        {
                            text1.WriteLine(f[x]);
                        }
                    }
                }
                else
                {
                    for (x = 0; x <= 99; x++)
                    {
                        if (f[x] != null && f[x] != "")
                        {
                            text1.WriteLine(f[x]);
                        }

                        if (x == 3 * listBox1.SelectedIndex - 1)
                        {
                            x = x + 3;
                        }
                    }
                }
            }

            listBox1.Items.Clear();

            using (StreamReader text2 = new StreamReader("events.txt"))
            {
                string a = "event", b = "date", c = "description";
                
                while (a != null) // Вывод сохраненных данных из файла
                {
                    a = text2.ReadLine();
                    b = text2.ReadLine();
                    c = text2.ReadLine();
                    if (a != "" && a != null)
                    {
                        listBox1.Items.Add(a + "   " + b);
                    }
                }
            }
        }
    }
}
