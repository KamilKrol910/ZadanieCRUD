using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ZadanieCRUD.Models
{
    class CConnectSQL
    {

        /// <summary>
        /// 
        /// </summary>
        private string connectionString = @"Server=localhost;Database=rekrutacjadb;Uid=root;Pwd=<KI*(OL>,ki89ol.;";

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
