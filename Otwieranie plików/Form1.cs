using System;
using System.IO;
using System.Windows.Forms;
using System.Globalization;

namespace Otwieranie_plików
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog wyborPliku = new OpenFileDialog
            {
                InitialDirectory = @"c:\desktop",
                Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*",
            };
            
            wyborPliku.ShowDialog();
            string plik = wyborPliku.FileName;

            //string plik = @"C:\Users\Bartosz\Desktop\1.txt";

            chart1.Series["Series1"].Points.Clear();
            chart1.Series["linia"].Points.Clear();
            textBox1.Text = " ";
            textBox2.Text = " ";
            textBox3.Text = " ";

            string[] daneDoWykresu = new string[2];
            float max = 0;
            float xMax = 0;
            float halfMax = 0;
            float x1 = 0;
            float x2 = 0;
            float y1 = 0;
            float y2 = 0;
            float a = 0;
            float b = 0;
            float x1Hmax = 0;
            float x2Hmax = 0;
            float FWHM;

            foreach (string wiersz in File.ReadLines(plik))
            {
                string[] podzielone = wiersz.Split(' ');

                daneDoWykresu[0] = podzielone[0].Replace('.', ',');
                daneDoWykresu[1] = podzielone[1].Replace('.', ',');

                if (Math.Round(float.Parse(daneDoWykresu[1]), 5) > max)
                {
                    max = float.Parse(daneDoWykresu[1]);
                    xMax = float.Parse(daneDoWykresu[0]);
                }

                halfMax = max / 2;

                if (Math.Round(float.Parse(daneDoWykresu[1]), 5) < max & (Math.Round(float.Parse(daneDoWykresu[1]), 5) > halfMax) & (Math.Round(float.Parse(daneDoWykresu[0]), 5)) > xMax)
                {
                    y2 = float.Parse(daneDoWykresu[1]);
                    x2 = float.Parse(daneDoWykresu[0]);
                }

                if (Math.Round(float.Parse(daneDoWykresu[1]), 5) > 20000  & (Math.Round(float.Parse(daneDoWykresu[1]), 5) < halfMax) & (Math.Round(float.Parse(daneDoWykresu[0]), 5)) > xMax)
                {
                    y1 = float.Parse(daneDoWykresu[1]);
                    x1 = float.Parse(daneDoWykresu[0]);
                }

                chart1.Series["Series1"].Points.AddXY(Math.Round(float.Parse(daneDoWykresu[0]),5), Math.Round(float.Parse(daneDoWykresu[1]),5));
            }

            foreach (string wiersz in File.ReadLines(plik))
            {
                string[] podzielone = wiersz.Split(' ');

                daneDoWykresu[0] = podzielone[0].Replace('.', ',');
                daneDoWykresu[1] = podzielone[1].Replace('.', ',');

                chart1.Series["linia"].Points.AddXY(Math.Round(float.Parse(daneDoWykresu[0]), 5), halfMax);
            }

            a = (y2 - y1) / (x2 - x1);
            b = y1 - a * x1;
            x2Hmax = (halfMax - b) / a;

            x1Hmax = xMax - (x2Hmax - xMax);

            FWHM = (x2Hmax - xMax) * 2;

            textBox1.Text = "FWHM = " + FWHM.ToString();
            textBox2.Text = "x1 = " + x1Hmax.ToString();
            textBox3.Text = "x2 = " + x2Hmax.ToString();  
        }

        private void button2_Click(object sender, EventArgs e)
        {
            chart1.Series["Series1"].Points.Clear();
            chart1.Series["linia"].Points.Clear();
            textBox1.Text = " ";
            textBox2.Text = " ";
            textBox3.Text = " ";
        }
    }
}