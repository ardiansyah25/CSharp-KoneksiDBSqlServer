using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//3
using System.Data.SqlClient;

namespace KoneksiDBSQLServer
{
    public partial class Form1 : Form
    {
        //4
        private SqlCommand cmd;
        private DataSet ds;
        private SqlDataAdapter da;

        //5
        Koneksi Konn = new Koneksi();
         
        void Bersihkan()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "0";
            textBox4.Text = "0";
            textBox5.Text = "0";
            comboBox1.Text = "";
        }

        void NoOtomatis()
        {
            long hitung;
            string urutan;
            SqlDataReader rd;
            SqlConnection conn = Konn.GetConn();
            conn.Open();
            cmd = new SqlCommand("select KodeBarang from TBL_BARANG where KodeBarang in(select max(KodeBarang) from TBL_BARANG) order by KodeBarang desc", conn);
            rd = cmd.ExecuteReader();
            rd.Read();
            if (rd.HasRows)
            {
                hitung = Convert.ToInt64(rd[0].ToString().Substring(rd["KodeBarang"].ToString().Length - 3, 3)) + 1;
                string kodeUrutan = "00" + hitung;
                urutan = "BRG" + kodeUrutan.Substring(kodeUrutan.Length - 3, 3);
            }
            else
            {
                urutan = "BRG001";
            }
            rd.Close();
            textBox1.Enabled = false;
            textBox1.Text = urutan;
            conn.Close();
        }

        public Form1()
        {
            InitializeComponent();
        }

        void TampilBarang()
        {
            SqlConnection conn = Konn.GetConn();
            try
            {
                conn.Open();
                cmd = new SqlCommand("Select * from TBl_BARANG", conn);
                ds = new DataSet();
                da = new SqlDataAdapter(cmd);
                da.Fill(ds, "TBL_BARANG");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "TBL_BARANG";
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception G)
            {
                MessageBox.Show(G.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        void CariBarang()
        {
            SqlConnection conn = Konn.GetConn();
            try
            {
                conn.Open();
                cmd = new SqlCommand("Select * from TBl_BARANG where KodeBarang like '%"+textBox7.Text+"%' or NamaBarang like '%"+ textBox7.Text +"%'", conn);
                ds = new DataSet();
                da = new SqlDataAdapter(cmd);
                da.Fill(ds, "TBL_BARANG");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "TBL_BARANG";
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception G)
            {
                MessageBox.Show(G.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        void MunculCombo()
        {
            comboBox1.Items.Add("PCS");
            comboBox1.Items.Add("BOX");
            comboBox1.Items.Add("PAK");
            comboBox1.Items.Add("BOTOL");
            comboBox1.Items.Add("UNIT");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TampilBarang();
            Bersihkan();
            MunculCombo();
            NoOtomatis();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if( 
                textBox1.Text.Trim() == "" || 
                textBox2.Text.Trim() == "" || 
                textBox3.Text.Trim() == "" || 
                textBox4.Text.Trim() == "" ||
                textBox5.Text.Trim() == "" ||
                comboBox1.Text.Trim() == ""
                )
            {
                MessageBox.Show("Data Belum Lengkap");
            }
            else
            {
                SqlConnection conn = Konn.GetConn();
                try
                {
                    cmd = new SqlCommand("insert into TBL_BARANG values(" +
                           "'"+ textBox1.Text+ "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + comboBox1.Text + "')", conn);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Insert Data Berhasil !");
                    TampilBarang();
                    Bersihkan();
                    NoOtomatis();
                }
                catch (Exception x)
                {
                    MessageBox.Show(x.ToString());
                }
               
            }
           
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                textBox1.Text = row.Cells["KodeBarang"].Value.ToString();
                textBox2.Text = row.Cells["NamaBarang"].Value.ToString();
                textBox3.Text = row.Cells["HargaBeli"].Value.ToString();
                textBox4.Text = row.Cells["HargaJual"].Value.ToString();
                textBox5.Text = row.Cells["JumlahBarang"].Value.ToString();
                comboBox1.Text = row.Cells["SatuanBarang"].Value.ToString();
            }
            catch(Exception x)
            {
                MessageBox.Show(x.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (
               textBox1.Text.Trim() == "" ||
               textBox2.Text.Trim() == "" ||
               textBox3.Text.Trim() == "" ||
               textBox4.Text.Trim() == "" ||
               textBox5.Text.Trim() == "" ||
               comboBox1.Text.Trim() == ""
               )
            {
                MessageBox.Show("Data Belum Lengkap");
            }
            else
            {
                SqlConnection conn = Konn.GetConn();
                try
                {
                    cmd = new SqlCommand("update TBL_BARANG Set NamaBarang = '"+ textBox2.Text + "', HargaBeli = '" + textBox3.Text + "', HargaJual = '" + textBox4.Text + "', JumlahBarang = '" + textBox5.Text + "', SatuanBarang = '" + comboBox1.Text + "' where KodeBarang = '"+textBox1.Text +"'", conn);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Update Data Berhasil !");
                    TampilBarang();
                    Bersihkan();
                }
                catch (Exception x)
                {
                    MessageBox.Show(x.ToString());
                }

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Yakin akan menghapus data barang : "+ textBox2.Text + " ?","Konfirmasi",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SqlConnection conn = Konn.GetConn();
                {
                    cmd = new SqlCommand("delete TBL_BARANG where KodeBarang = '" + textBox1.Text + "'", conn);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Hapus Data Berhasil !");
                    TampilBarang();
                    Bersihkan();
                }
              
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Bersihkan();
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            CariBarang();
        }
    }
}
