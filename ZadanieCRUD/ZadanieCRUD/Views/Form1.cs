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

        void NewPositionListLineViewOF();
    }

    

    public partial class FrmMainView : Form, IFrmMainView
    {

        IControllerMain Controller;

        public FrmMainView()
        {
            InitializeComponent();
        }

        private void FrmMainView_Load(object sender, EventArgs e)
        {
            Controller.ReadAllTransactionLine();
        }

        public void AddListener(IControllerMain Controller_)
        {
            this.Controller = Controller_;
        }


        //---------------------------------------------------------------------PRZYCISKI ZMIANY WIDOKU    

        // do wprowadzenia nowej linii transakcji
        private void button1_Click(object sender, EventArgs e)
        {
            Controller.NewTransactionListLineViewON();
        }

        //Do edycji linii transakcji
        private void button2_Click(object sender, EventArgs e)
        {
            Controller.EditTransactionListLineViewON(dataGridView1.CurrentCell.RowIndex);
        }

        //Do usunięcia linii transakcji
        private void button3_Click(object sender, EventArgs e)
        {
            Controller.DeleteTransactionListLineViewON(dataGridView1.CurrentCell.RowIndex);
        }

        //powrót do startowego widoku
        private void button9_Click(object sender, EventArgs e)
        {
            Controller.NewTransactionListLineViewOF();
        }

        //nowa pozycja transakcji
        private void button8_Click(object sender, EventArgs e)
        {
            Controller.NewPositionLineViewON(dataGridView1.CurrentCell.RowIndex);
        }

        //edycja pozycji transakcji
        private void button7_Click(object sender, EventArgs e)
        {
            Controller.EditPositionListLineViewON(Convert.ToInt32(dataGridView2.CurrentCell.RowIndex));
        }

        //usunięcie linii transakcji
        private void button6_Click(object sender, EventArgs e)
        {
            Controller.DeletePositionListLineViewON(Convert.ToInt32(dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells["dl_id"].Value));
        }

        //powrót do standardowego widoku
        private void button12_Click(object sender, EventArgs e)
        {
            Controller.NewPositionListLineViewOF();
        }

        //---------------------------------------------------------------------PRZYCISKI WYŚWIETLANIA DANYCH
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



        //---------------------------------------------------------------------PRZYCISKI ZMIANY MODELU

        //Nowa transakcja
        private void button5_Click(object sender, EventArgs e)
        {
            Controller.NewTransactionListLine();
        }

        //edycja transakcji
        private void button4_Click(object sender, EventArgs e)
        {
            Controller.EditTransactionListLine();
        }

        //nowa transakcja
        private void button10_Click(object sender, EventArgs e)
        {
            Controller.NewPositionListLine();
        }

        //edycja pozycji transakcji
        private void button11_Click(object sender, EventArgs e)
        {
            Controller.EditPositionListLine();
        }



        //--------------------------------------------------------------------------------ZMIANY WIDOKU NA BAZIE MODELU

        // Przygotowanie pól do wprowadzenia nowego wpisu transakcji
        public void NewTransactionListLineViewON(int NewID)
        {
            groupBox3.Visible = true;
            groupBox2.Visible = false;
            groupBox4.Visible = false;
            button5.Visible = true;
            button4.Visible = false;
            textBox1.Text = NewID.ToString();
        }

        // Przygotowanie pól do wprowadzenia nowego wpisu pozycji transakcji
        public void NewPositionLineViewON(CTransactionListLine EditListLine)
        {
            groupBox4.Visible = true;
            button10.Visible = true;
            button11.Visible = false;

            textBox11.Text = EditListLine.ID.ToString();
            if (EditListLine.TransactionPosLine.Count == 0)
            {
                textBox10.Text = "1";
            }
            else
            {
                textBox10.Text = (EditListLine.TransactionPosLine[EditListLine.TransactionPosLine.Count - 1].MaxID + 1).ToString();
            }
        }

        //Uzupełnie pól edycji transakcji na bazie zaznaczonego rekordu
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
            textBox5.Text = Math.Round(EditListLine.PriceNet,2).ToString();
            textBox6.Text = Math.Round(EditListLine.PriceBrt,2).ToString();
            if (EditListLine.Datin > DateTime.MinValue)
            { dateTimePicker1.Value = EditListLine.Datin; }           
        }


        //Uzupełnie pól edycji pozycji transakcji na bazie zaznaczonego rekordu
        public void EditPositionListLineViewON(int RowLine)
        {
            groupBox4.Visible = true;
            button11.Visible = true;
            button10.Visible = false;
            textBox10.Text = Convert.ToInt32(dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells["dl_id"].Value).ToString();
            textBox11.Text = Convert.ToInt32(dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells["dl_dhid"].Value).ToString();
            textBox9.Text = Convert.ToInt32(dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells["dl_qua"].Value).ToString();
            textBox8.Text = dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells["dl_artname"].Value.ToString();
            textBox7.Text = Convert.ToDouble(dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells["dl_pricenet"].Value).ToString();
            textBox3.Text = Convert.ToDouble(dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells["dl_pricebrt"].Value).ToString();
        }

        //powrót do standardowego widoku pozycji transakcji
        public void NewPositionListLineViewOF()
        {
            groupBox4.Visible = false;
        }

        //powrót do standardowego widoku transakcji
        public void NewTransactionListLineViewOF()
        {
            groupBox3.Visible = false;
            groupBox2.Visible = false;
            textBox1.Text = "";
        }



        //------------------------------------------------------------------------METODY SŁUŻACE DO ODCZYTU DANYCH Z TEXTBOX
        public int ReadTransactionID()
        {
       return SCFunction.ConvertStringToInt(textBox1.Text);
        }
        public string ReadTransactionName()
        {
            return textBox4.Text;
        }

        public int ReadTransactionCustNr()
        {
            return SCFunction.ConvertStringToInt(textBox2.Text);
        }

        public double ReadTransactionPriceNet()
        {
            return SCFunction.ConvertStringToDouble(textBox5.Text);
        }
        public double ReadTransactionPriceBrt()
        {
            return SCFunction.ConvertStringToDouble(textBox6.Text);
        }
        public DateTime ReadTransactionDate()
        {
            return Convert.ToDateTime(dateTimePicker1.Value);
        }
        public int ReadPositionID()
        {
            return SCFunction.ConvertStringToInt(textBox10.Text);
        }
        public int ReadPosTrID()
        {
            return SCFunction.ConvertStringToInt(textBox11.Text);
        }
        public string ReadArtNumber()
        {
            return textBox8.Text;
        }
        public int ReadPositionQuantity()
        {
            return SCFunction.ConvertStringToInt(textBox9.Text);
        }
        public double ReadPositionPriceNet()
        {
            return SCFunction.ConvertStringToDouble(textBox7.Text);
        }
        public double ReadPositionPriceBrt()
        {
            return SCFunction.ConvertStringToDouble(textBox3.Text);
        }

    }

}
