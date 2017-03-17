using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class DüzenleFrm : Form
   {
        private DataSet ds = new DataSet();

        public Malzeme duzen { get; set; }

        public DüzenleFrm()
        {
            InitializeComponent();
        }

        private void DüzenleFrm_Load(object sender, EventArgs e)
        {
         if(this.duzen != null)
            {
                DüzenleFrm dfrm = this;
                this.MalzemeTxt.Text = this.duzen.adi;
                this.TipTxt.Text = this.duzen.ozellik;

                int result = 0;
                if (int.TryParse(dfrm.MiktarTxt.Text, out result))

                {

                    this.duzen.miktar = this.duzen.miktar;
                    result.ToString();
                }


            this.TeslimAlanTxt.Text = this.duzen.teslimAlan;
                this.TeslimEdenTxt.Text = this.duzen.teslimEden;

            }
         
        }

        private void KaydetBtn_Click(object sender, EventArgs e)
        {
            
            
                    DüzenleFrm dfrm = this;

                    this.duzen.adi = MalzemeTxt.Text;
                    this.duzen.ozellik = TipTxt.Text;

                    int result = 0;
                    if (int.TryParse(dfrm.MiktarTxt.Text, out result))

                    {

                        this.duzen.miktar = result;
                        //result.ToString();
                    }

                    this.duzen.teslimAlan = TeslimAlanTxt.Text;
                    this.duzen.teslimEden = TeslimEdenTxt.Text;

                   

                    this.DialogResult = System.Windows.Forms.DialogResult.OK;

            //ds.WriteXml(Application.StartupPath + "//" + "veri.xml", XmlWriteMode.WriteSchema);

            //this.Tiklandi = 1;


        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}
