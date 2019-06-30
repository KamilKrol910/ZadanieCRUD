﻿using System;
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
    public partial class Form1 : Form
    {

        string connectionString = @"Server=localhost;Database=rekrutacjadb;Uid=root;Pwd=<KI*(OL>,ki89ol.;";
        private MySqlConnection connection;
        private MySqlDataAdapter mySqlDataAdapter;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ID = "";

            string server = "localhost";
            string database = "rekrutacjadb";
            string uid = "root";
            string password = "<KI*(OL>,ki89ol.";
            string connectionString;
            connectionString = "SERVER=" + server + ";database=RekturacjadB;uid=root;password=" + "<KI*(OL>,ki89ol.";

            connection = new MySqlConnection(connectionString);

            if (this.OpenConnection() == true)
            {
                mySqlDataAdapter = new MySqlDataAdapter("select cdb_var from configdb where cdb_table = 'dh_mdokh'", connection);
                DataSet DS = new DataSet();
                mySqlDataAdapter.Fill(DS);
                //MessageBox.Show(DS.Tables[0].Rows[0]["cdb_var"].ToString());

                ID = DS.Tables[0].Rows[0]["cdb_var"].ToString();
                //close connection
                this.CloseConnection();
            }





            Form2 Test;
            Test = new Form2(ID);
            //Test.MdiParent = this;

            Test.Show();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            
         string server = "localhost";
            string database = "rekrutacjadb";
            string uid = "root";
            string password = "<KI*(OL>,ki89ol.";
            string connectionString;
            connectionString = "SERVER=" + server + ";database=RekturacjadB;uid=root;password=" + "<KI*(OL>,ki89ol.";

            connection = new MySqlConnection(connectionString);

            if (this.OpenConnection() == true)
            {
                mySqlDataAdapter = new MySqlDataAdapter("select * from configdb", connection);
                DataSet DS = new DataSet();
                mySqlDataAdapter.Fill(DS);
                //MessageBox.Show(DS.Tables[0].Rows[0]["cdb_var"].ToString());
                dataGridView1.DataSource = DS.Tables[0];

                //close connection
                this.CloseConnection();
            }
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


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(dataGridView1.CurrentCell.RowIndex.ToString());
        }
    }
}
