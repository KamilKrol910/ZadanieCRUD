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
    public partial class Form2 : Form
    {
        public Form2(string ID)
        {
            InitializeComponent();
            textBox1.Text = ID;
            textBox3.Text = DateTime.Now.ToString();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

            try
            {
                //This is my connection string i have assigned the database file address path  
                string MyConnection2 = "datasource=localhost;database=RekturacjadB;username=root;password=<KI*(OL>,ki89ol.";
                //This is my insert query in which i am taking input from the user through windows forms  
                string Query = "update configdb set cdb_var='" + this.textBox1.Text  + "' where cdb_table='dh_mdokh'";
                //This is  MySqlConnection here i have created the object and pass my connection string.  
                MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                //This is command class which will handle the query and connection object.  
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                MySqlDataReader MyReader2;
                MyConn2.Open();
                MyReader2 = MyCommand2.ExecuteReader();     // Here our query will be executed and data saved into the database.  
                MessageBox.Show("Save Data");
                while (MyReader2.Read())
                {
                }
                MyConn2.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 Dialog = new Form3();
            this.TopMost = false;
            Dialog.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MySql.Data.MySqlClient.MySqlConnection dbConn = new MySql.Data.MySqlClient.MySqlConnection("Persist Security Info=False;server=localhost;database=RekturacjadB;uid=root;password=" + "<KI*(OL>,ki89ol.");

            MySqlCommand cmd = dbConn.CreateCommand();
            cmd.CommandText = "SELECT * from dh_mdokh ";

            try
            {
                dbConn.Open();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro" + erro);
                this.Close();
            }

            MySqlDataReader reader = cmd.ExecuteReader();

            string idnumber="";

            while (reader.Read())
            {
                idnumber = reader.ToString();
            }
            MessageBox.Show(idnumber);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
