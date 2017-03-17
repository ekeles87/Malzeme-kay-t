using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Status;

namespace WindowsFormsApplication2
{
    public partial class DepoKayıtFrm : Form
    {
        public Malzeme yenikayit { get; set; }

        private DataSet ds = new DataSet();

        public static int Tiklandi = 0;

        private void DepoKayıtFrm_Load(object sender, EventArgs e)
        {
          if(File.Exists(Application.StartupPath + "//" + "veri.xml"))
            { 
            ds.Tables.Clear();
            ds.ReadXml(Application.StartupPath + "//" + "veri.xml", XmlReadMode.ReadSchema);
            if (ds.Tables.Count > 0)
             {
                this.dataGridView1.DataSource = ds.Tables[0];
                this.kayitsayisi();
             }
            }
          else
            {
                DataTable dt = new DataTable("veri");
                DataColumn malzeme = new DataColumn("Malzeme");
                DataColumn tip = new DataColumn("Tip/Özellik");
                DataColumn miktar = new DataColumn("Miktar");
                DataColumn teslimalan = new DataColumn("Teslim Alan");
                DataColumn teslimeden = new DataColumn("Teslim Eden");

                dt.Columns.Add(malzeme);
                dt.Columns.Add(tip);
                dt.Columns.Add(miktar);
                dt.Columns.Add(teslimalan);
                dt.Columns.Add(teslimeden);

                ds.Tables.Add(dt);
                this.dataGridView1.DataSource = dt;
            }




            //else
            //{
            //    //DataTable dt1 = new DataTable();
            //    //dt1.Columns.Add("Malzeme", typeof(string));
            //    //dt1.Columns.Add("Tipi/Özelliği", typeof(string));
            //    //dt1.Columns.Add("Miktar", typeof(int));
            //    //dt1.Columns.Add("Teslim Alan", typeof(string));
            //    //dt1.Columns.Add("Teslim Eden", typeof(string));
            //    //this.dataGridView1.DataSource = dt1;

            //    //dt.WriteXml(Application.StartupPath + "//" + "veri.xml", XmlWriteMode.WriteSchema);
            //    //}
           
        }

       
        public DepoKayıtFrm()
        {
            InitializeComponent();
        }

        private void DüzenleBtn_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow !=null)
             {
                

                DüzenleFrm frm = new DüzenleFrm();

                frm.duzen = new Malzeme();
                frm.duzen.adi = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                frm.duzen.ozellik = this.dataGridView1.CurrentRow.Cells[1].Value.ToString();



                frm.duzen.miktar = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells[2].Value);


                frm.duzen.teslimAlan = this.dataGridView1.CurrentRow.Cells[3].Value.ToString();
                frm.duzen.teslimEden = this.dataGridView1.CurrentRow.Cells[4].Value.ToString();


                DialogResult secim = frm.ShowDialog();
                if (secim == System.Windows.Forms.DialogResult.OK)
                {
                    DataRow dr = (this.dataGridView1.CurrentRow.DataBoundItem as DataRowView).Row;
                    dr[0] = frm.duzen.adi;
                    dr[1] = frm.duzen.ozellik;
                    dr[2] = frm.duzen.miktar;
                    dr[3] = frm.duzen.teslimAlan;
                    dr[4] = frm.duzen.teslimEden;


                }



            }
            else
            {
                MessageBox.Show("Düzenlenecek Satırı Seçin");
            }
        }

        private void KaydetBtn_Click(object sender, EventArgs e)
        {
            DepoKayıtFrm dfrm = this;



            DataTable dt = this.dataGridView1.DataSource as DataTable;

          foreach(Control tb in this.Controls)
            {
                if(tb is TextBox && tb.Text != string.Empty)
                { 
                Malzeme yenikayit = new Malzeme();
                yenikayit.adi = dfrm.MalzemeTxt.Text;

                int result = 0;
                if (int.TryParse(dfrm.MiktarTxt.Text, out result))

                {

                    yenikayit.miktar = result;
                    result.ToString();
                }

                yenikayit.ozellik = dfrm.TipTxt.Text;
                yenikayit.teslimAlan = dfrm.TeslimAlanTxt.Text;
                yenikayit.teslimEden = dfrm.TeslimEdenTxt.Text;



                DataRow dr = dt.NewRow();
                dr[0] = yenikayit.adi;
                dr[1] = yenikayit.ozellik;
                dr[2] = yenikayit.miktar;
                dr[3] = yenikayit.teslimAlan;
                dr[4] = yenikayit.teslimEden;

                dt.Rows.Add(dr);

                MalzemeTxt.Text = "";
                MiktarTxt.Text = "";
                TipTxt.Text = "";
                TeslimAlanTxt.Text = "";
                TeslimEdenTxt.Text = "";

                ds.WriteXml(Application.StartupPath + "//" + "veri.xml", XmlWriteMode.WriteSchema);
                this.kayitsayisi();
                }
                else /*if (this.Tiklandi = 1)*/
                {
                    ds.WriteXml(Application.StartupPath + "//" + "veri.xml", XmlWriteMode.WriteSchema);
                }
            }


        }
        private void kayitsayisi()
        {
            this.toolStripStatusLabel1.Text = "Kayıt Sayısı :" + this.dataGridView1.RowCount.ToString();

        }

        private void silbtn_Click(object sender, EventArgs e)
        {
            if(dataGridView1.CurrentRow != null)
            {
                DialogResult sonuc = MessageBox.Show("Silmak İstediğinize Emin misiniz?", "Silme İşlemi", MessageBoxButtons.YesNo);
                if(sonuc==System.Windows.Forms.DialogResult.Yes)
                {
                    DataRowView drv = this.dataGridView1.CurrentRow.DataBoundItem as DataRowView;
                    DataRow dr = drv.Row;

                    dr.Delete();
                    ds.WriteXml(Application.StartupPath + "//" + "veri.xml", XmlWriteMode.WriteSchema);
                }
            
            }
        }
    }
}
