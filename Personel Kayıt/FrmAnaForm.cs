using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;//sql ile bağlantı kurmak için gerekli olan kütüphane


namespace Personel_Kayıt
{
    public partial class FrmAnaForm : Form
    {
        public FrmAnaForm()
        {
            InitializeComponent();
        }

        //global alanda sql ile bağlantı kurup o kütüphanedeki özellikleri kullanabilmek için bir nesneye ihtiyacım var 
        //bu nesne ihtiyacını da sqlconnection sınıfından nesne türeterek yapıyoruz 
        SqlConnection baglantı = new SqlConnection("Data Source=DESKTOP-7C53G4M\\SQLEXPRESS;Initial Catalog=PersonelVeriTabani;Integrated Security=True");


        //Temizleme işlemini metotla çağırmak istediğimiz için global alanda metot tanımı yapıyoruz
        void temizle()
        {
            Txtid.Text = "";
            Txtad.Text = "";
            Txtsoyad.Text = "";
            Txtmeslek.Text = "";
            Mskmaas.Text = "";
            Cmbsehir.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            Txtad.Focus();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            //bu kodu program bağlantı kurunca kednisi getirdi
            // TODO: This line of code loads data into the 'personelVeriTabaniDataSet.Tbl_Personel' table. You can move, or remove it, as needed.
            this.tbl_PersonelTableAdapter.Fill(this.personelVeriTabaniDataSet.Tbl_Personel);

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void BtnListele_Click(object sender, EventArgs e)
        {
            this.tbl_PersonelTableAdapter.Fill(this.personelVeriTabaniDataSet.Tbl_Personel);

        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            SqlCommand komut = new SqlCommand("insert into Tbl_Personel (PerAd,PerSoyad,PerSehir,PerMaaş,PerMeslek,PerDurum) values (@p1,@p2,@p3,@p4,@p5,@p6)",baglantı);
            komut.Parameters.AddWithValue("@p1", Txtad.Text);
            komut.Parameters.AddWithValue("@p2", Txtsoyad.Text);
            komut.Parameters.AddWithValue("@p3", Cmbsehir.Text);
            komut.Parameters.AddWithValue("@p4", Mskmaas.Text);
            komut.Parameters.AddWithValue("@p5", Txtmeslek.Text);
            komut.Parameters.AddWithValue("@p6", label8.Text);
            komut.ExecuteNonQuery();//sorguyu çalıştırmak iin kullanılan bir komut kod parçasıdır
            //eğer tablo sonucunda bir değişikli olacaksa (yani ekleme, silme, güncelleme işlemlerinde) bu kodu mutlaka yazmalıyız
            baglantı.Close();
            MessageBox.Show("Personel Eklendi");
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked==true)
            {
                label8.Text = "True";
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                label8.Text = "False";
            }

        }

        private void BtnTemızle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;

            Txtid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            Txtad.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            Txtsoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            Cmbsehir.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            Mskmaas.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            label8.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            Txtmeslek.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();
        }

        private void label8_TextChanged(object sender, EventArgs e)
        {
            if (label8.Text=="True")
            {
                radioButton1.Checked = true;
            }
            if (label8.Text=="False")
            {
                radioButton2.Checked = true;
            }
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            SqlCommand komutsil = new SqlCommand("Delete From Tbl_Personel Where Perid=@k7", baglantı);
            komutsil.Parameters.AddWithValue("@k1", Txtid.Text);
            komutsil.ExecuteNonQuery();
            baglantı.Close();
            MessageBox.Show("Kayıt Silindi");
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            baglantı.Open();
            SqlCommand komutguncelle = new SqlCommand("Update Tbl_Personel Set PerAd=@a1,PerSoyad=@a2,PerSehir=@a3,PerMaaş=@a4,PerDurum=@a5,PerMeslek=@a6 where Perid=@a7", baglantı);
            komutguncelle.Parameters.AddWithValue("@a1", Txtad.Text);
            komutguncelle.Parameters.AddWithValue("@a2", Txtsoyad.Text);
            komutguncelle.Parameters.AddWithValue("@a3", Cmbsehir.Text);
            komutguncelle.Parameters.AddWithValue("@a4", Mskmaas.Text);
            komutguncelle.Parameters.AddWithValue("@a5", label8.Text);
            komutguncelle.Parameters.AddWithValue("@a6", Txtmeslek.Text);
            komutguncelle.Parameters.AddWithValue("@a7", Txtid.Text);
            komutguncelle.ExecuteNonQuery();
            baglantı.Close();
            MessageBox.Show("Personel Bilgileri Güncellendi");
        }

        private void BtnIstatıstıkler_Click(object sender, EventArgs e)
        {
            Frmİstatistik fr = new Frmİstatistik();
            fr.Show();
            this.Hide();
        }

        private void BtnGrafikler_Click(object sender, EventArgs e)
        {
            FrmGrafikler frg = new FrmGrafikler();
            frg.Show();
            this.Hide();
        }
    }
}
