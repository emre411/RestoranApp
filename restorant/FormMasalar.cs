using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Windows.Forms;

namespace restorant
{
    public partial class FormMasalar : Form
    {
        private int masaSayisi = 0;
        private int butonGenislik = 100;
        private int butonYukseklik = 50;
        private int butonAralik = 10;
        private int butonlarYanYana = 4;

        
        private Dictionary<int, Button> aktifMasalar = new Dictionary<int, Button>();

        public FormMasalar()
        {
            InitializeComponent();
        }

        private void FormMasalar_Load(object sender, EventArgs e)
        {
            

            
        }

        private void btnMasaAc_Click(object sender, EventArgs e)
        {
            masaSayisi++;

            if (aktifMasalar.ContainsKey(masaSayisi))
            {
                MessageBox.Show($"Masa {masaSayisi} zaten var.");
                return;
            }

            Button yeniMasaBtn = new Button();
            yeniMasaBtn.Text = "Masa " + masaSayisi;
            yeniMasaBtn.Name = "btnMasa" + masaSayisi;
            yeniMasaBtn.Width = butonGenislik;
            yeniMasaBtn.Height = butonYukseklik;

            
            int satir = (masaSayisi - 1) / butonlarYanYana;
            int sutun = (masaSayisi - 1) % butonlarYanYana;

            yeniMasaBtn.Left = 20 + sutun * (butonGenislik + butonAralik);
            yeniMasaBtn.Top = 60 + satir * (butonYukseklik + butonAralik);

            yeniMasaBtn.Click += MasaButon_Click;
            if (this.Controls.ContainsKey("panelMasalar"))
            {
                Control panel = this.Controls["panelMasalar"];
                panel.Controls.Add(yeniMasaBtn);
            }
            else
            {
                this.Controls.Add(yeniMasaBtn);
            }

            aktifMasalar.Add(masaSayisi, yeniMasaBtn);
        }

        private void MasaButon_Click(object sender, EventArgs e)
        {
            Button tiklananBtn = sender as Button;
            string masaText = tiklananBtn.Text;  
            int masaNo = int.Parse(masaText.Split(' ')[1]); 

            MenuForm menuForm = new MenuForm(masaNo, this); 
            menuForm.Show();

            this.Hide(); 
        }

        
        public void MasaKapat(int masaNo)
        {
            if (aktifMasalar.ContainsKey(masaNo))
            {
                Button btn = aktifMasalar[masaNo];
                this.Controls.Remove(btn);     
                aktifMasalar.Remove(masaNo);   
            }

            this.Show(); 
        }
    }
}