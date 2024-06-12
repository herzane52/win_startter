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

            // Þarj durumu deðiþikliði olayýna iþleyici ekle
            SystemEvents.PowerModeChanged += SystemEvents_PowerModeChanged;

        }

        // Json dosyasýnýn yolu
        private static readonly string _filePath = Path.Combine(AppContext.BaseDirectory, "applist.json");

        // Uygulamalarýn listesi
        private static List<string> appList = new List<string>();

        ////////////////////////////////////

        private void Form1_Load(object sender, EventArgs e)
        {
            // Json dosyasýnýn varlýðýný kontrol etme
            if (File.Exists(_filePath))
            {

                // MessageBox.Show("Json dosyasý mevcut.");
                // Json dosyasýndan veri okuma ve liste kutusuna ekleme iþlemlerini burada yapabilirsiniz.
            }
            else
            {
                //MessageBox.Show("Json dosyasý mevcut deðil.");
                // Json dosyasýný oluþturma
                using FileStream createStream = File.Create(_filePath);
                //MessageBox.Show("Json dosyasý oluþturuldu.");

            }



            // Form yüklendiðinde çalýþan metod
            // Json dosyasýndan veri okuma
            ReadJson();

            // Uygulamalarý liste kutusuna ekleme
            foreach (var app in appList)
            {
                lstUygulamalar.Items.Add(app);
            }
            // Þarj durumu olayýný çaðýr
            CheckPowerStatus();
        }

        private void SystemEvents_PowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            // Þarj durumunu kontrol et
            CheckPowerStatus();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Form kapatýldýðýnda çalýþan metod
            // Json dosyasýna veri yazma
            WriteJson();
            this.Close();
        }
        private void LstUygulamalar_DrawItem(object sender, DrawItemEventArgs e)
        {
            // Liste kutusunun öðelerini çizen metod
            // Öðe dizinini al
            int index = e.Index;
            // Öðe metnini al
            string text = lstUygulamalar.Items[index].ToString();
            // Öðe simgesini al
            Icon icon = Icon.ExtractAssociatedIcon(text);
            // Öðe arka planýný çiz
            e.DrawBackground();
            // Öðe simgesini çiz
            e.Graphics.DrawIcon(icon, e.Bounds.X, e.Bounds.Y);
            string appName = Path.GetFileNameWithoutExtension(@text);
            // Öðe metnini çiz
            e.Graphics.DrawString(appName, e.Font, Brushes.White, e.Bounds.X + 50, e.Bounds.Y + 5);
        }


        //Özel þarj drumu fonksiyonu
        private void CheckPowerStatus()
        {
            // Þarj bilgilerini al
            PowerStatus ps = SystemInformation.PowerStatus;

            // Þarja takýlý mý?
            bool sarjda = ps.PowerLineStatus == PowerLineStatus.Online;

            // Þarj yüzdesi
            int yuzde = (int)(ps.BatteryLifePercent * 100);
            // Þarj durumu deðiþikliðinde metinleri deðiþtir
            label2.Text = $"{(sarjda ? "Þarj Oluyor" : "Þarj Olmuyor")}";
            label1.Text = $"{yuzde}%";
            //MessageBox.Show($"Þarj durumu: {(sarjda ? "Takýlý" : "Takýlý deðil")}\nÞarj yüzdesi: {yuzde}%\nÞarj durumu deðiþikliði modu: {mode}", "Sarj Durumu");,

            // Uygulamayý baþlat veya kapat
            foreach (string path in lstUygulamalar.Items)
            {
                if (sarjda)
                {
                    // Þarja takýlýysa uygulamalarý baþlat
                    Process.Start(path);
                    label2.Text = $"{(sarjda ? "Þarj Oluyor" : "Þarj Olmuyor")}";
                    label1.Text = $"{yuzde}%";
                    // Þarja takýlýysa uygulamayý baþlat
                    // Process.Start(@"C:\Program Files\Notepad++\notepad++.exe");
                    //MessageBox.Show("Þarja oluyor");
                }
                else
                {
                    // Þarja takýlý deðilse uygulamalarý kapat
                    label2.Text = $"{(sarjda ? "Þarj Oluyor" : "Þarj Olmuyor")}";
                    label1.Text = $"{yuzde}%";
                    //MessageBox.Show("Deþarj oluyor");
                    /*
                    * // Þarja takýlý deðilse uygulamayý kapat
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
            // Ekleme düðmesine týklandýðýnda çalýþan metod
            // Dosya seçme penceresi oluþtur
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Uygulama Dosyalarý (*.exe)|*.exe";
            ofd.Title = "Uygulama Seçin";
            // Dosya seçme penceresini göster
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                // Seçilen dosyanýn yolu al
                string path = ofd.FileName;
                // Dosyayý listeye ekle
                lstUygulamalar.Items.Add(path);
                // MessageBox.Show("Uygulama baþarýyla eklendi.", "Bilgi");
                AddApp(path);
            }



        }


        private void btnSil_Click(object sender, EventArgs e)
        {
            // Silme düðmesine týklandýðýnda çalýþan metod
            // Seçili öðenin dizinini al
            int index = lstUygulamalar.SelectedIndex;
            // Seçili öðe varsa
            if (index != -1)
            {
                // Seçili öðenin yolunu al
                string path = lstUygulamalar.Items[index].ToString();
                // Seçili öðeyi listeden sil
                lstUygulamalar.Items.RemoveAt(index);
                WriteJson();
                //MessageBox.Show("Uygulama baþarýyla silindi.", "Bilgi");
                // Seçili öðeyi uygulama listesinden sil
                RemoveApp(path);
            }
        }




        // Json dosyasýna veri yazma metodu
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


        // Json dosyasýndan veri okuma metodu
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
                MessageBox.Show(app + " listede bulunamadý.");
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Form hakkýnda = new FormAbout();
            // hakkýnda.ShowDialog();

        }
    }
}