using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace ZadanieCRUD
{
    public interface IFrmMainView
    {
         void AddListener(IControllerMain Controller);
         void SetDataGridView1(DataTable DataSetGridView);
        void SetDataGridView2(DataTable DataSetGridView);

        void NewTransactionListLineViewON(int NewID);
        void NewTransactionListLineViewOF();

        void EditTransactionListLineViewON(CTransactionListLine EditListLine);


        int ReadTransactionID();
        string ReadTransactionName();
        int ReadTransactionCustNr();
        double ReadTransactionPriceNet();
        double ReadTransactionPriceBrt();
        DateTime ReadTransactionDate();

        int ReadPositionID();
        int ReadPosTrID();
        string ReadArtNumber();
        int ReadPositionQuantity();
        double ReadPositionPriceNet();
        double ReadPositionPriceBrt();

        void NewPositionLineViewON(CTransactionListLine EditListLine);
        void EditPositionListLineViewON(int RowLine);

    }

    

    public partial class FrmMainView : Form, IFrmMainView
    {

        string connectionString = @"Server=localhost;Database=rekrutacjadb;Uid=root;Pwd=<KI*(OL>,ki89ol.;";
        private MySqlConnection connection;
        private MySqlDataAdapter mySqlDataAdapter;

        IControllerMain Controller;

        public FrmMainView()
        {
            InitializeComponent();
        }

        public void AddListener(IControllerMain Controller_)
        {
            this.Controller = Controller_;
        }

        public void SetDataGridView1(DataTable DataSetGridView)
        {
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.DataSource = DataSetGridView;
            dataGridView1.Refresh();
        }

        public void SetDataGridView2(DataTable DataSetGridView)
        {
            dataGridView2.DataSource = null;
            dataGridView2.Rows.Clear();
            dataGridView2.DataSource = DataSetGridView;
            dataGridView2.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Controller.NewTransactionListLineViewON();
                     
        }

        public void NewTransactionListLineViewON(int NewID)
        {
            groupBox3.Visible = true;
            groupBox2.Visible = false;
            groupBox4.Visible = false;
            button5.Visible = true;
            button4.Visible = false;
            textBox1.Text = NewID.ToString();

        }


       public void EditTransactionListLineViewON(CTransactionListLine EditListLine)
        {
            groupBox3.Visible = true;
            groupBox2.Visible = true;
            button5.Visible = false;
            button4.Visible = true;
            groupBox4.Visible = false;
            textBox1.Text = EditListLine.ID.ToString();
            textBox2.Text = EditListLine.CustNumber.ToString();
            textBox4.Text = EditListLine.Name.ToString();
            textBox5.Text = EditListLine.PriceNet.ToString();
            textBox6.Text = EditListLine.PriceBrt.ToString();
            if (EditListLine.Datin > DateTime.MinValue)
            { dateTimePicker1.Value = EditListLine.Datin; }           
        }

        public void NewTransactionListLineViewOF()
        {
            groupBox3.Visible = false;
            groupBox2.Visible = false;
            textBox1.Text = "";
        }


        private void Form1_Shown(object sender, EventArgs e)
        {
            

        }


        private void button2_Click(object sender, EventArgs e)
        {
           Controller.EditTransactionListLineViewON(dataGridView1.CurrentCell.RowIndex);
        }

        private void FrmMainView_Load(object sender, EventArgs e)
        {
            Controller.ReadAllTransactionLine();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Controller.NewTransactionListLine();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Controller.NewTransactionListLineViewOF();
        }



       public int ReadTransactionID(){
            int n;
            bool isNumeric = int.TryParse(textBox1.Text, out n);
            if (isNumeric)
            {
                return Convert.ToInt32(textBox1.Text);
            }
            else
            {
                return 0;
            }
        }
        public string ReadTransactionName()
        {
            return textBox4.Text;
        }

        public int ReadTransactionCustNr()
        {
            int n;
            bool isNumeric = int.TryParse(textBox2.Text, out n);
            if (isNumeric)
            {
                return Convert.ToInt32(textBox2.Text);
            }
            else
            {
                return 0;
            }
        }

        public double ReadTransactionPriceNet()
        {
            double n;
            bool isDouble = double.TryParse(textBox5.Text.Replace(",", "."), out n);
            if (isDouble)
            {
                return Convert.ToDouble(textBox5.Text.Replace(",", "."));
                
            }
            else
            {
                return 0;
            }
        }
        public double ReadTransactionPriceBrt()
        {
            double n;
            bool isDouble = double.TryParse(textBox6.Text.Replace(",", "."), out n);
            //MessageBox.Show(isDouble.ToString());
            if (isDouble)
            {
                
                return Convert.ToDouble(textBox6.Text.Replace(",", "."));
            }
            else
            {
                return 0;
            }
        }
        public DateTime ReadTransactionDate()
        {
            return Convert.ToDateTime(dateTimePicker1.Value);

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Controller.EditTransactionListLine();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {        
                Controller.DeleteTransactionListLineViewON(dataGridView1.CurrentCell.RowIndex);
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            Controller.EditPositionListLine();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Controller.NewPositionLineViewON(dataGridView1.CurrentCell.RowIndex);
        }


        public void NewPositionLineViewON(CTransactionListLine EditListLine)
        {
            groupBox4.Visible = true;
            button10.Visible = true;
            button11.Visible = false;

            textBox11.Text = EditListLine.ID.ToString();
            if (EditListLine.TransactionPosLine.Count ==0)
            {
                textBox10.Text = "1";
            }
            else
            {
                textBox10.Text = (EditListLine.TransactionPosLine[EditListLine.TransactionPosLine.Count - 1].MaxID+1).ToString();
            }

        }

        private void button10_Click(object sender, EventArgs e)
        {
            Controller.NewPositionListLine();
        }

        public int ReadPositionID() {
            int n;
            bool isNumeric = int.TryParse(textBox10.Text, out n);
            if (isNumeric)
            {
                return Convert.ToInt32(textBox10.Text);
            }
            else
            {
                return 0;
            }
        }
        public int ReadPosTrID() {
            int n;
            bool isNumeric = int.TryParse(textBox11.Text, out n);
            if (isNumeric)
            {
                return Convert.ToInt32(textBox11.Text);
            }
            else
            {
                return 0;
            }
        }
        public string ReadArtNumber() {
            return textBox8.Text;
        }
        public int ReadPositionQuantity()
        {
            int n;
            bool isNumeric = int.TryParse(textBox9.Text, out n);
            if (isNumeric)
            {
                return Convert.ToInt32(textBox9.Text);
            }
            else
            {
                return 0;
            }
        }
        public double ReadPositionPriceNet()
        {
            double n;
            bool isDouble = double.TryParse(textBox7.Text.Replace(",", "."), out n);
            //MessageBox.Show(isDouble.ToString());
            if (isDouble)
            {

                return Convert.ToDouble(textBox7.Text.Replace(",", "."));
            }
            else
            {
                return 0;
            }
        }
        public double ReadPositionPriceBrt()
        {
            double n;
            bool isDouble = double.TryParse(textBox3.Text.Replace(",", "."), out n);
            //MessageBox.Show(isDouble.ToString());
            if (isDouble)
            {

                return Convert.ToDouble(textBox3.Text.Replace(",", "."));
            }
            else
            {
                return 0;
            }
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {           
             Controller.DeletePositionListLineViewON(Convert.ToInt32(dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells["dl_id"].Value));
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Controller.EditPositionListLineViewON(Convert.ToInt32(dataGridView2.CurrentCell.RowIndex));
        }

       public void EditPositionListLineViewON (int RowLine)
        {
            groupBox4.Visible = true;
            button11.Visible = true;
            button10.Visible = false;
            textBox10.Text =  Convert.ToInt32(dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells["dl_id"].Value).ToString();
            textBox11.Text = Convert.ToInt32(dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells["dl_dhid"].Value).ToString();
            textBox9.Text = Convert.ToInt32(dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells["dl_qua"].Value).ToString();
            textBox8.Text = dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells["dl_artname"].Value.ToString();
            textBox7.Text = Convert.ToDouble(dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells["dl_pricenet"].Value).ToString();
            textBox3.Text = Convert.ToDouble(dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells["dl_pricebrt"].Value).ToString();
        }
    }
}
