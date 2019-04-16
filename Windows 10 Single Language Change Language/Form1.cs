using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using Microsoft.Win32;
using System.Windows.Forms;

namespace Windows_10_Single_Language_Change_Language
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.Filter = "Dolap Paleti|*.cab";
            openfile.Title = "Dil Paketinizi Seçiniz...";
            if (openfile.ShowDialog() == DialogResult.OK)
            {
                paketyoluTextBox.Text = openfile.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(paketyoluTextBox.Text == "Dil paketinizi seçiniz.")
            {
                MessageBox.Show("Lütfen bir dil paketi seçiniz.");
            }
            else
            {
                Process process = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                startInfo.CreateNoWindow = true;
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardOutput = true;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = "DISM /Online /Add-Package /PackagePath:" + paketyoluTextBox.Text;
                process.StartInfo = startInfo;
                process.Start();
                button2.Text = "Bu işlem uzun sürebilir lütfen bekleyiniz.";
                console.Text = console.Text + process.StandardOutput.ReadToEnd();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "bcdedit /set {current} locale " + localTextBox.Text;
            process.StartInfo = startInfo;
            process.Start();
            console.Text = console.Text + process.StandardOutput.ReadToEnd();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "bcdboot %WinDir% /l " + localTextBox.Text;
            process.StartInfo = startInfo;
            process.Start();
            console.Text = console.Text + process.StandardOutput.ReadToEnd();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.FileName = "regedit.bat";
            process.StartInfo = startInfo;
            process.Start();
            console.Text = console.Text + process.StandardOutput.ReadToEnd();
            MessageBox.Show("CMD Penceresini kapatabilirsiniz.","Bilgilendirme");
            MessageBox.Show("Açılan Regedit Ekranından HKEY_LOCAL_MACHINE yi açın.","Uyarı");
            MessageBox.Show("Ardından Sırasıyla Bu Yolu Takip Edin. SYSTEM\\CurrentControlSet\\Control\\MUI\\UILanguages", "Önemli Uyarı");
            MessageBox.Show("2 Adet klasör olacak birisi sizin sisteminizin diğeri yüklediğiniz dil." + localTextBox.Text  + "Bu klasörü değil diğer klasörü siliniz.", "Bilgilendirme");
            MessageBox.Show("Bilgisayarınızı yeniden başlatınız.","Bilgilendirme");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            RegistryKey key;
            key = Registry.LocalMachine.OpenSubKey("\\SYSTEM\\CurrentControlSet\\Control\\MUI\\UILanguages\\");
            key.DeleteSubKeyTree("deneme");
            key.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                localTextBox.Clear();
                localTextBox.Text = "tr-TR";
            }
            else
            {
                localTextBox.Text = "Dil paketinizin Local adını giriniz. Örneğin Türkiye için tr-TR ENG için en-US / en-GB gibi.";
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox2.Checked == true)
            {
                localTextBox.Clear();
                localTextBox.Text = "en-US";
            }
            else
            {
                localTextBox.Text = "Dil paketinizin Local adını giriniz. Örneğin Türkiye için tr-TR ENG için en-US / en-GB gibi.";
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Oluşacak hasarlardan veya veri kayıplarından kullanıcı sorumludur.","Uyarı");
        }

        private void button6_Click_1(object sender, EventArgs e)
        {

        }
    }
}
