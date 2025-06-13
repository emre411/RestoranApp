using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace restorant
{
    public partial class MenuForm : Form
    {
        private decimal toplamFiyat = 0;
        private int masaNo;
        private FormMasalar parentForm;
        private int siparisNo = 1; 

        public MenuForm(int masaNo, FormMasalar parentForm)
        {
            InitializeComponent();
            this.masaNo = masaNo;
            this.parentForm = parentForm;
            this.Text = $"Menü - Masa {masaNo}";
            this.Load += MenuForm_Load;
        }

        private void MenuForm_Load(object sender, EventArgs e)
        {
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is Panel panel)
                {
                    foreach (Control altCtrl in panel.Controls)
                    {
                        if (altCtrl is CheckBox cb)
                        {
                            cb.CheckedChanged += chkEkle_CheckedChanged;
                        }
                    }
                }
            }
        }

        private void chkEkle_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = sender as CheckBox;
            if (chk == null || !chk.Checked) return;

            Panel panel = chk.Parent as Panel;
            if (panel == null) return;

            string urunAdi = "";
            decimal fiyat = 0;
            int adet = 0;

            foreach (Control ctrl in panel.Controls)
            {
                if (ctrl is Label lbl)
                {
                    string text = lbl.Text.ToLower().Trim();

                    if (text.Contains("fiyat"))
                    {
                       
                        string fiyatMetin = lbl.Text
                            .ToLower()
                            .Replace("fiyat", "")
                            .Replace(":", "")
                            .Replace("tl", "")
                            .Replace("₺", "")
                            .Trim()
                            .Replace(",", ".");

                        if (!decimal.TryParse(fiyatMetin, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out fiyat))
                        {
                            MessageBox.Show($"Fiyat alınamadı! Label değeri: '{lbl.Text}'");
                            chk.Checked = false;
                            return;
                        }
                    }
                    else if (!text.Contains("adet") && !text.Contains("ekle") && !text.Contains("fiyat"))
                    {
                        urunAdi = lbl.Text.Trim();
                    }
                }

                if (ctrl is NumericUpDown nud)
                {
                    adet = (int)nud.Value;
                }
            }

            if (string.IsNullOrWhiteSpace(urunAdi))
            {
                MessageBox.Show("Ürün adı alınamadı.");
                chk.Checked = false;
                return;
            }

            if (adet <= 0)
            {
                MessageBox.Show("Lütfen adet seçiniz.");
                chk.Checked = false;
                return;
            }

            decimal satirTutar = fiyat * adet;
            toplamFiyat += satirTutar;

            string satir = $"{siparisNo}. {urunAdi.ToUpper()} x{adet} = {satirTutar:C2}";
            SiparisListesi.Items.Add(satir);
            txtToplamFiyat.Text = $"{toplamFiyat:C2}";

            siparisNo++;
            chk.Checked = false;
        }

        private void btnFisYazdir_Click(object sender, EventArgs e)
        {
            if (SiparisListesi.Items.Count == 0)
            {
                MessageBox.Show("Sipariş listesi boş.");
                return;
            }

            string fis = "----- FİŞ -----\n";

            foreach (var item in SiparisListesi.Items)
            {
                fis += item.ToString() + "\n";
            }

            fis += $"Toplam: {txtToplamFiyat.Text}";

            MessageBox.Show(fis, "Fiş");

            parentForm.MasaKapat(masaNo);
            this.Close();
        }

      
        private void checkBox1_CheckedChanged(object sender, EventArgs e) { }
        private void pictureBox1_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void pictureBox2_Click(object sender, EventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e) { }
        private void label62_Click(object sender, EventArgs e) { }
        private void textBox6_TextChanged(object sender, EventArgs e) { }
        private void textBox7_TextChanged(object sender, EventArgs e) { }
        private void label34_Click(object sender, EventArgs e) { }
        private void numericUpDown6_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label55_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           parentForm.Show();
            this.Close();
        }
    }
}