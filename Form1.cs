using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Generic;
using Microsoft.Win32;
using System.IO;
using Newtonsoft.Json;
using static System.Windows.Forms.DataFormats;


namespace WinFormsApp1
{
    public partial class WinPlayer : Form
    {

        public WinPlayer()
        {
            InitializeComponent();

            // �arj durumu de�i�ikli�i olay�na i�leyici ekle
            SystemEvents.PowerModeChanged += SystemEvents_PowerModeChanged;

        }

        // Json dosyas�n�n yolu
        private static readonly string _filePath = Path.Combine(AppContext.BaseDirectory, "applist.json");

        // Uygulamalar�n listesi
        private static List<string> appList = new List<string>();

        ////////////////////////////////////

        private void Form1_Load(object sender, EventArgs e)
        {
            // Json dosyas�n�n varl���n� kontrol etme
            if (File.Exists(_filePath))
            {

                // MessageBox.Show("Json dosyas� mevcut.");
                // Json dosyas�ndan veri okuma ve liste kutusuna ekleme i�lemlerini burada yapabilirsiniz.
            }
            else
            {
                //MessageBox.Show("Json dosyas� mevcut de�il.");
                // Json dosyas�n� olu�turma
                using FileStream createStream = File.Create(_filePath);
                //MessageBox.Show("Json dosyas� olu�turuldu.");

            }



            // Form y�klendi�inde �al��an metod
            // Json dosyas�ndan veri okuma
            ReadJson();

            // Uygulamalar� liste kutusuna ekleme
            foreach (var app in appList)
            {
                lstUygulamalar.Items.Add(app);
            }
            // �arj durumu olay�n� �a��r
            CheckPowerStatus();
        }

        private void SystemEvents_PowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            // �arj durumunu kontrol et
            CheckPowerStatus();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Form kapat�ld���nda �al��an metod
            // Json dosyas�na veri yazma
            WriteJson();
            this.Close();
        }
        private void LstUygulamalar_DrawItem(object sender, DrawItemEventArgs e)
        {
            // Liste kutusunun ��elerini �izen metod
            // ��e dizinini al
            int index = e.Index;
            // ��e metnini al
            string text = lstUygulamalar.Items[index].ToString();
            // ��e simgesini al
            Icon icon = Icon.ExtractAssociatedIcon(text);
            // ��e arka plan�n� �iz
            e.DrawBackground();
            // ��e simgesini �iz
            e.Graphics.DrawIcon(icon, e.Bounds.X, e.Bounds.Y);
            string appName = Path.GetFileNameWithoutExtension(@text);
            // ��e metnini �iz
            e.Graphics.DrawString(appName, e.Font, Brushes.White, e.Bounds.X + 50, e.Bounds.Y + 5);
        }


        //�zel �arj drumu fonksiyonu
        private void CheckPowerStatus()
        {
            // �arj bilgilerini al
            PowerStatus ps = SystemInformation.PowerStatus;

            // �arja tak�l� m�?
            bool sarjda = ps.PowerLineStatus == PowerLineStatus.Online;

            // �arj y�zdesi
            int yuzde = (int)(ps.BatteryLifePercent * 100);
            // �arj durumu de�i�ikli�inde metinleri de�i�tir
            label2.Text = $"{(sarjda ? "�arj Oluyor" : "�arj Olmuyor")}";
            label1.Text = $"{yuzde}%";
            //MessageBox.Show($"�arj durumu: {(sarjda ? "Tak�l�" : "Tak�l� de�il")}\n�arj y�zdesi: {yuzde}%\n�arj durumu de�i�ikli�i modu: {mode}", "Sarj Durumu");,

            // Uygulamay� ba�lat veya kapat
            foreach (string path in lstUygulamalar.Items)
            {
                if (sarjda)
                {
                    // �arja tak�l�ysa uygulamalar� ba�lat
                    Process.Start(path);
                    label2.Text = $"{(sarjda ? "�arj Oluyor" : "�arj Olmuyor")}";
                    label1.Text = $"{yuzde}%";
                    // �arja tak�l�ysa uygulamay� ba�lat
                    // Process.Start(@"C:\Program Files\Notepad++\notepad++.exe");
                    //MessageBox.Show("�arja oluyor");
                }
                else
                {
                    // �arja tak�l� de�ilse uygulamalar� kapat
                    label2.Text = $"{(sarjda ? "�arj Oluyor" : "�arj Olmuyor")}";
                    label1.Text = $"{yuzde}%";
                    //MessageBox.Show("De�arj oluyor");
                    /*
                    * // �arja tak�l� de�ilse uygulamay� kapat
                    Process[] processes = Process.GetProcessesByName("notepad++");
                    foreach (var process in processes)
                    {
                        process.Kill();
                        this.Close();
                    }*/
                    Process[] processes = Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(path));
                    foreach (var process in processes)
                    {
                        process.Kill();
                    }
                }
            }
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            // Ekleme d��mesine t�kland���nda �al��an metod
            // Dosya se�me penceresi olu�tur
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Uygulama Dosyalar� (*.exe)|*.exe";
            ofd.Title = "Uygulama Se�in";
            // Dosya se�me penceresini g�ster
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                // Se�ilen dosyan�n yolu al
                string path = ofd.FileName;
                // Dosyay� listeye ekle
                lstUygulamalar.Items.Add(path);
                // MessageBox.Show("Uygulama ba�ar�yla eklendi.", "Bilgi");
                AddApp(path);
            }



        }


        private void btnSil_Click(object sender, EventArgs e)
        {
            // Silme d��mesine t�kland���nda �al��an metod
            // Se�ili ��enin dizinini al
            int index = lstUygulamalar.SelectedIndex;
            // Se�ili ��e varsa
            if (index != -1)
            {
                // Se�ili ��enin yolunu al
                string path = lstUygulamalar.Items[index].ToString();
                // Se�ili ��eyi listeden sil
                lstUygulamalar.Items.RemoveAt(index);
                WriteJson();
                //MessageBox.Show("Uygulama ba�ar�yla silindi.", "Bilgi");
                // Se�ili ��eyi uygulama listesinden sil
                RemoveApp(path);
            }
        }




        // Json dosyas�na veri yazma metodu
        public static void WriteJson()
        {
            try
            {
                dynamic jsonObj = new { Apps = appList };
                string output = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);
                File.WriteAllText(_filePath, output);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Hata");
            }
        }


        // Json dosyas�ndan veri okuma metodu
        public static void ReadJson()
        {
            try
            {
                string json = File.ReadAllText(_filePath);
                dynamic jsonObj = JsonConvert.DeserializeObject(json);
                appList = jsonObj["Apps"].ToObject<List<string>>();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Hata");
            }
        }
        // Uygulama ekleme metodu
        public static void AddApp(string app)
        {
            if (!appList.Contains(app))
            {
                appList.Add(app);
                MessageBox.Show(app + " eklendi.");
            }
            else
            {
                MessageBox.Show(app + " zaten listede var.");
            }
        }

        // Uygulama silme metodu
        public static void RemoveApp(string app)
        {
            if (appList.Contains(app))
            {
                appList.Remove(app);
                MessageBox.Show(app + " silindi.");
            }
            else
            {
                MessageBox.Show(app + " listede bulunamad�.");
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Form hakk�nda = new FormAbout();
            // hakk�nda.ShowDialog();

        }
    }
}