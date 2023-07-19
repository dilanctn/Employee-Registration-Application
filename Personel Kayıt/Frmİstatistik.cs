using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;//yeni ekeldik

namespace Personel_Kayıt
{
    public partial class Frmİstatistik : Form
    {
        public Frmİstatistik()
        {
            InitializeComponent();
        }

        SqlConnection baglantı = new SqlConnection("Data Source=DESKTOP-7C53G4M\\SQLEXPRESS;Initial Catalog=PersonelVeriTabani;Integrated Security=True");


        private void Frmİstatistik_Load(object sender, EventArgs e)
        {
            //Toplam Personel sayısı için   
            baglantı.Open();
            SqlCommand komut1 = new SqlCommand("Select Count(*) From Tbl_Personel", baglantı);
            SqlDataReader dr1 = komut1.ExecuteReader();
            while (dr1.Read())
            {
                lbltoplampersonel.Text = dr1[0].ToString();
            }
            baglantı.Close();

            //Evli Olan Personellerin Sayısı
            baglantı.Open();
            SqlCommand komut2 = new SqlCommand("Select Count (*) From Tbl_Personel Where PerDurum=1 ",baglantı);
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                LblEvliPer.Text = dr2[0].ToString();
            }
            baglantı.Close();

            //Bekar Personel Sayısı
            baglantı.Open();
            SqlCommand komut3 = new SqlCommand("Select Count (*) From Tbl_Personel Where PerDurum=0", baglantı);
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                LblBekarper.Text = dr3[0].ToString();
            }
            baglantı.Close();

            //Kaç Farklı Şehir olduğunu gösteren istatistik için 
            //Burada sorguyu yapmak için farklı olan şehirleri getirmek
            //istiyoruz yani aynı olan şehirlerin tekrar etmesini istemiyoruz
            //bunu sağlayan sorgu kelimesi ise "distinct" anahtar kelimesidir

            baglantı.Open();
            SqlCommand komut4 = new SqlCommand("Select Count (distinct(PerSehir)) From Tbl_Personel", baglantı);
            SqlDataReader dr4 = komut4.ExecuteReader();
            while (dr4.Read())
            {
                LblSehir.Text = dr4[0].ToString();
            }
            baglantı.Close();

            //Ortalama Maaş İstatistiği için 
            baglantı.Open();
            SqlCommand komut5 = new SqlCommand("Select Sum(PerMaaş) From Tbl_Personel",baglantı);
            SqlDataReader dr5 = komut5.ExecuteReader();
            while (dr5.Read())
            {
                LblTopMaas.Text = dr5[0].ToString();
            }
            baglantı.Close();

            //Ortalama Maaş İstatistiği İçin
            //toplam maas/ kişi sayısı yapmanın kısa yolu avg komutu
            baglantı.Open();
            SqlCommand komut6 = new SqlCommand("Select Avg(PerMaaş) From Tbl_Personel", baglantı);
            SqlDataReader dr6 = komut6.ExecuteReader();
            while (dr6.Read())
            {
                LblortMaas.Text = dr6[0].ToString();
            }
            baglantı.Close();
            
        }
    }
}
