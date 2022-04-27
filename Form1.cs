using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Örnek1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        bool değişti;
        string dosyaadı = "Adsız";
        DialogResult cevap;
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = dosyaadı;
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            değişti = true;
        }
        private void yeniToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!değişti)
            {
                textBox1.Clear();
                dosyaadı = "Adsız";
                this.Text = dosyaadı;
                değişti = false;
            }
            else
            {
                cevap = MessageBox.Show("Değişiklikler kaydedilsin mi?", "Not Defteri", MessageBoxButtons.YesNoCancel);
                if (cevap == DialogResult.Yes)
                {
                    farklıKaydetToolStripMenuItem_Click(sender, e);
                    textBox1.Clear();
                    dosyaadı = "Adsız";
                    this.Text = dosyaadı;
                    değişti = false;
                }
                else if (cevap == DialogResult.No)
                {
                    textBox1.Clear();
                    dosyaadı = "Adsız";
                    this.Text = dosyaadı;
                    değişti = false;
                }
            }
        }

        private void açToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cevap = openFileDialog1.ShowDialog();
            if (cevap == DialogResult.OK)
            {
                dosyaadı = openFileDialog1.FileName;
                StreamReader Aç = new StreamReader(dosyaadı);
                textBox1.Text = Aç.ReadLine();
                Aç.Close();
                this.Text = dosyaadı;
            }
        }

        private void kaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dosyaadı == "Adsız")
            {
                cevap = saveFileDialog1.ShowDialog();
                if (cevap == DialogResult.OK)
                {
                    dosyaadı = saveFileDialog1.FileName;
                    StreamWriter Kaydet = new StreamWriter(dosyaadı);
                    Kaydet.WriteLine(textBox1.Text);
                    Kaydet.Close();
                    this.Text = dosyaadı;
                    değişti = false;
                }
            }
            else
            {
                StreamWriter Kaydet = new StreamWriter(dosyaadı);
                Kaydet.WriteLine(textBox1.Text);
                Kaydet.Close();
            }
        }

        private void farklıKaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cevap = saveFileDialog1.ShowDialog();
            if (cevap == DialogResult.OK)
            {
                dosyaadı = saveFileDialog1.FileName;
                StreamWriter Kaydet = new StreamWriter(dosyaadı);
                Kaydet.WriteLine(textBox1.Text);
                Kaydet.Close();
                this.Text = dosyaadı;
            }
        }
        private void sayfaYapısıToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cevap = pageSetupDialog1.ShowDialog();
            if (cevap == DialogResult.OK)
            {
                printDialog1.PrinterSettings = pageSetupDialog1.PrinterSettings;
            }
        }

        private void yazdırToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cevap = printDialog1.ShowDialog();
            if (cevap == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int üst, sol;
            üst = pageSetupDialog1.PageSettings.Margins.Top;
            sol = pageSetupDialog1.PageSettings.Margins.Left;

            e.Graphics.DrawString(textBox1.Text, textBox1.Font, Brushes.Black, new Point(üst, sol));
        }

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (değişti)
            {
                cevap = MessageBox.Show("Değişiklikler kaydedilsin mi?", "Not Defteri", MessageBoxButtons.YesNoCancel);
                if (cevap == DialogResult.Yes)
                {
                    cevap = saveFileDialog1.ShowDialog();
                    if (cevap == DialogResult.OK)
                    {
                        dosyaadı = saveFileDialog1.FileName;
                        StreamWriter Kaydet = new StreamWriter(dosyaadı);
                        Kaydet.WriteLine(textBox1.Text);
                        Kaydet.Close();
                        this.Close();
                    }
                }
                else if (cevap == DialogResult.No) Close();
            }
            else Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int satır, sütun;

            satır = textBox1.GetLineFromCharIndex(textBox1.SelectionStart);

            sütun = textBox1.SelectionStart - textBox1.GetFirstCharIndexFromLine(satır);

            toolStripStatusLabel1.Text = "Satır : " + Convert.ToString(satır + 1) + "   " + "Sütun : " + Convert.ToString(sütun + 1);
        }

        private void geriAlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Undo();
        }

        private void kesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Cut();
        }

        private void kopyalaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Copy();
        }

        private void yapıştırToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Paste();
        }

        private void silToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int x, y;
            x = textBox1.SelectionStart;
            y = textBox1.SelectionLength;
            textBox1.Text = textBox1.Text.Remove(x, y);
            textBox1.SelectionStart = x;
        }

        private void tümünüSeçToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.SelectAll();
        }

        private void tarihToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int x = textBox1.SelectionStart;
            string tarih = DateTime.Now.ToString();
            textBox1.Text = textBox1.Text.Insert(x, tarih);
            textBox1.SelectionStart = x + tarih.Length;
        }

        private void sözcükKaydırToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sözcükKaydırToolStripMenuItem.Checked) textBox1.WordWrap = true;
            else textBox1.WordWrap = false;
        }

        private void yazıTipiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult cevap;
            cevap = fontDialog1.ShowDialog();
            if (cevap == DialogResult.OK) textBox1.Font = fontDialog1.Font;
        }

        private void durumÇubuğuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (durumÇubuğuToolStripMenuItem.Checked) statusStrip1.Visible = true;
            else statusStrip1.Visible = false;
        }

        private void yardımıGörüntüleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string key = @"http\shell\open\command";
            RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey(key, false);

            string varsayılanTarayıcıYolu = ((string)registryKey.GetValue(null, null)).Split('"')[1];

            Process.Start(varsayılanTarayıcıYolu, "https://www.bing.com/search?q=windows+10%E2%80%99da+not+defteriyle+ilgili+yard%C4%B1m+alma");

        }

        private void notDefteriHakkındaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not Defteri\nVersiyon 1.0", "Not Defteri Hakkında");
        }
    }
}
