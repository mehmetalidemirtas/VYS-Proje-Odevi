using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YurtDatabase
{
    public partial class Yurt : Form
    {
        public Yurt()
        {
            InitializeComponent();
        }
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localHost; port=5432; Database=dbYurt; user ID=postgres; password=Alidem8118");
        private void buttonListele_Click(object sender, EventArgs e)
        {
            string sorgu = "select * from kalanogrenciler";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0]; 
        }

        private void buttonEkle_Click(object sender, EventArgs e) //ogrenci_ekle fonksiyonu ile eklendi
        {
            baglanti.Open();
            NpgsqlCommand komut1 = new NpgsqlCommand("ogrenci_ekle", baglanti);
            komut1.CommandType = CommandType.StoredProcedure;
            komut1.Parameters.AddWithValue("ogrenciid", int.Parse(textId.Text));
            komut1.Parameters.AddWithValue("adi", textAd.Text);
            komut1.Parameters.AddWithValue("soyadi", textSoyad.Text);
            komut1.Parameters.AddWithValue("universiteid", comboBoxUni.SelectedValue);
            komut1.Parameters.AddWithValue("bolumid", comboBoxBolum.SelectedValue);
            komut1.Parameters.AddWithValue("oda", int.Parse(textOda.Text));
            komut1.Parameters.AddWithValue("yatak", int.Parse(textYatak.Text));
            komut1.Parameters.AddWithValue("yurt", comboBoxYurt.SelectedValue);
            komut1.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Ögrenci kaydı başarılı!");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlDataAdapter da_uni = new NpgsqlDataAdapter("select * from universiteler", baglanti);
            NpgsqlDataAdapter da_bolum = new NpgsqlDataAdapter("select * from bolum", baglanti);
            NpgsqlDataAdapter da_yurt = new NpgsqlDataAdapter("select * from yurt", baglanti);
            DataTable dt = new DataTable();
            DataTable dt_bolum = new DataTable();
            DataTable dt_yurt = new DataTable();
            da_uni.Fill(dt);
            da_bolum.Fill(dt_bolum);
            da_yurt.Fill(dt_yurt);

            comboBoxUni.DisplayMember = "universitead";
            comboBoxUni.ValueMember = "uniid";
            comboBoxUni.DataSource = dt;

            comboBoxBolum.DisplayMember = "bolumad";
            comboBoxBolum.ValueMember = "bolumid";
            comboBoxBolum.DataSource = dt_bolum;

            comboBoxYurt.DisplayMember = "yurtad";
            comboBoxYurt.ValueMember = "yurtid";
            comboBoxYurt.DataSource = dt_yurt;

            baglanti.Close();
        }

        private void buttonTemizle_Click(object sender, EventArgs e)
        {
            foreach (Control item in this.Controls)
            {
                if (item is TextBox)
                {
                    TextBox tbx = (TextBox)item;
                    tbx.Clear();
                }
            }
        }

        private void buttonSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut2 = new NpgsqlCommand("ogrenci_sil", baglanti);
            komut2.CommandType = CommandType.StoredProcedure;
            komut2.Parameters.AddWithValue("ogrenciid", int.Parse(textId.Text));
            komut2.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Öğrenci Silme Başarılı!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }

        private void buttonGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut3 = new NpgsqlCommand("ogrenci_guncelle", baglanti);
            komut3.CommandType = CommandType.StoredProcedure;
            komut3.Parameters.AddWithValue("adi", textAd.Text);
            komut3.Parameters.AddWithValue("soyadi", textSoyad.Text);
            komut3.Parameters.AddWithValue("universiteid", comboBoxUni.SelectedValue);
            komut3.Parameters.AddWithValue("bolumid", comboBoxBolum.SelectedValue);
            komut3.Parameters.AddWithValue("oda", int.Parse(textOda.Text));
            komut3.Parameters.AddWithValue("yatak", int.Parse(textYatak.Text));
            komut3.Parameters.AddWithValue("yurt", comboBoxYurt.SelectedValue);
            komut3.Parameters.AddWithValue("ogrenciid", int.Parse(textId.Text));
            komut3.ExecuteNonQuery();
            MessageBox.Show("Öğrenci Güncelleme Başarılı!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            baglanti.Close();
        }

        private void buttonView_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut4 = new NpgsqlCommand("Select * From kalanlar", baglanti);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(komut4);
            DataSet dt = new DataSet();
            da.Fill(dt);
            dataGridView1.DataSource = dt.Tables[0];
            baglanti.Close();
        }

        private void buttonAra_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            DataTable dt = new DataTable();
            NpgsqlDataAdapter ara = new NpgsqlDataAdapter("Select * from kalanogrenciler " +
                "where ad like '%" + textAd.Text + "%' ", baglanti);
            ara.Fill(dt);
            baglanti.Close();
            dataGridView1.DataSource = dt;
        }      
    }
}
