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

        public DataTable SQLReadData(string request)
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

                Result = DS.Tables[0];
                this.CloseConnection();
            }
            return Result;
        }



        public bool SQLQueryExc(string Query_)
        {
            try
            {
                string MyConnection2 = "datasource=localhost;database=RekturacjadB;username=root;password=!QAZxsw2";
                string Query = Query_;
                MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                MySqlDataReader MyReader2;
                MyConn2.Open();
                MyReader2 = MyCommand2.ExecuteReader();    
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

                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Nie można połączyć się z bazą danych SQL. Skontaktuj się z administratorem.");
                        break;
                    case 1045:
                        MessageBox.Show("Niepoprawny numer użytkownika/hasło");
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
