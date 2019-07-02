using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ZadanieCRUD
{
    class CConnectSQL
    {



        /// <summary>
        /// 
        /// </summary>
        private MySqlConnection connection;


        /// <summary>
        /// 
        /// </summary>
        private MySqlDataAdapter mySqlDataAdapter;



        /// <summary>
        /// Konstruktor klasy
        /// </summary>
        public CConnectSQL()
        {

        }


        public DataTable ReadTableFromSQL(string request)
        {
            DataTable Result = null;

            string connectionString;
            connectionString = "SERVER=localhost;database=RekturacjadB;uid=root;password=" + "!QAZxsw2";

            connection = new MySqlConnection(connectionString);

            if (this.OpenConnection() == true)
            {
                mySqlDataAdapter = new MySqlDataAdapter(request, connection);
                DataSet DS = new DataSet();
                mySqlDataAdapter.Fill(DS);
                //MessageBox.Show(DS.Tables[0].Rows[0]["cdb_var"].ToString());

                Result = DS.Tables[0];

                //close connection
                this.CloseConnection();

            }




            return Result;
        }



        public bool InsertData(string Query_)
        {
            try
            {
                //This is my connection string i have assigned the database file address path  
                string MyConnection2 = "datasource=localhost;database=RekturacjadB;username=root;password=!QAZxsw2";
                //This is my insert query in which i am taking input from the user through windows forms  
                string Query = Query_;
                //This is  MySqlConnection here i have created the object and pass my connection string.  
                MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                //This is command class which will handle the query and connection object.  
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                MySqlDataReader MyReader2;
                MyConn2.Open();
                MyReader2 = MyCommand2.ExecuteReader();     // Here our query will be executed and data saved into the database.  
                //MessageBox.Show("Save Data");
                while (MyReader2.Read())
                {
                }
                MyConn2.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

/// <summary>
/// Metoda uruchamiająca połączenie SQL raportująca o ewentualnych błędach
/// </summary>
/// <returns></returns>
private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {

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


            /// <summary>
            /// Metoda zamykająca połączenie
            /// </summary>
            /// <returns></returns>
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

    }
}
