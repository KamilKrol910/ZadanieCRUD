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
         void SetDataGridView(DataTable DataSetGridView);


        void NewTransactionListLineViewON(int NewID);
        void NewTransactionListLineViewOF();

        void EditTransactionListLineViewON(CTransactionListLine EditListLine);


        int ReadTransactionID();
        string ReadTransactionName();
        int ReadTransactionCustNr();
        double ReadTransactionPriceNet();
        double ReadTransactionPriceBrt();
        DateTime ReadTransactionDate();



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

        public void SetDataGridView(DataTable DataSetGridView)
        {
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.DataSource = DataSetGridView;
            dataGridView1.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Controller.NewTransactionListLineViewON();
                     
        }

        public void NewTransactionListLineViewON(int NewID)
        {
            groupBox3.Visible = true;
            groupBox2.Visible = false;
            button5.Visible = true;
            button4.Visible = false;
            textBox1.Text = NewID.ToString();

        }


       public void EditTransactionListLineViewON(CTransactionListLine EditListLine)
        {

            groupBox3.Visible = true;
            groupBox2.Visible = false;
            button5.Visible = false;
            button4.Visible = true;
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

        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server. Contact administrator");
                        break;
                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                    default:
                        MessageBox.Show(ex.Message);
                        break;
                }
                return false;
            }
        }


        //Close connection
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
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
    }
}
