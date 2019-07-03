using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZadanieCRUD
{
    public interface ITransactionList
    {
        DataTable ReadList();
        DataTable ReadPosList(int RowLine);

        int ReadLastIndexTransaction();

        bool CreateListLine(int ID, DateTime DATIN, string Name, int CustNR, double PriceNET, double PriceBRT);
        bool CreatePosLine(int ID, int MAINID, string ArtName, int Quantity, double PriceNet, double PriceBrt);



        bool EditListLine(int ID, DateTime DATIN, string Name, int CustNR, double PriceNET, double PriceBRT);
        bool EditPosLine(int ID, int MAINID, string ArtName, int Quantity, double PriceNet, double PriceBrt);
        CTransactionListLine GetTransactionList(int RowLine);

        bool DeleteListLine(int RowLine);
        bool DeletePosLine(int RowLine);
        

    }

    /// <summary>
    /// Główny model programu - odpowiada za przechowanie listy wszystkich wpisów
    /// </summary>
    class CTransactionList: ITransactionList
    {
        /// <summary>
        /// Lista wszystkich linii w tabeli
        /// </summary>
        public List<CTransactionListLine> TransactionListLine = new List<CTransactionListLine>();


        public DataTable ReadList() {
            DataTable ReadListTable = null;

            CConnectSQL ConnectSQL = new CConnectSQL();
            ReadListTable = ConnectSQL.SQLReadData("select * from dh_mdokh where dh_id > 0 order by dh_id");
            SaveTableAsList(ReadListTable);
       
            return ReadListTable;
        }

        public DataTable ReadPosList(int RowLine)
        {
            DataTable ReadListTable = null;

            ReadListTable = TransactionListLine[RowLine].ReadPosList();

            return ReadListTable;
        }

            public CTransactionListLine GetTransactionList(int RowLine)
        {
            return TransactionListLine[RowLine];

        }

        public int ReadLastIndexTransaction()
        {
            if (TransactionListLine.Count == 0)
            { return 0; }
            else
            { return TransactionListLine[TransactionListLine.Count - 1].ID; }

        }

        private void SaveTableAsList (DataTable ReadTable)
        {
            TransactionListLine.Clear();
            foreach (DataRow dr2 in ReadTable.Rows)
            {
                if (!DBNull.Value.Equals(dr2["dh_id"]))
                {
                    TransactionListLine.Add(new CTransactionListLine() { ID = Convert.ToInt32(dr2["dh_id"])});
                    if (!DBNull.Value.Equals(dr2["dh_datin"])) { TransactionListLine[TransactionListLine.Count - 1].Datin = Convert.ToDateTime(dr2["dh_datin"]); }
                    if (!DBNull.Value.Equals(dr2["dh_custnr"])) { TransactionListLine[TransactionListLine.Count - 1].CustNumber = Convert.ToInt32(dr2["dh_custnr"]); }
                    if (!DBNull.Value.Equals(dr2["dh_name"])) { TransactionListLine[TransactionListLine.Count - 1].Name = dr2["dh_name"].ToString(); }
                    if (!DBNull.Value.Equals(dr2["dh_pricenet"])) { TransactionListLine[TransactionListLine.Count - 1].PriceNet = Convert.ToDouble(dr2["dh_pricenet"]); }
                    if (!DBNull.Value.Equals(dr2["dh_pricebrt"])) { TransactionListLine[TransactionListLine.Count - 1].PriceBrt = Convert.ToDouble(dr2["dh_pricebrt"]); ; }
                    ReadPosList(TransactionListLine.Count - 1);
                }
                
            }

        }

        public bool CreateListLine(int ID, DateTime DATIN, string Name, int CustNR, double PriceNET, double PriceBRT) {
            if (ID ==0 )
            {
                return false;
            }
            else
            {
                string Request = "insert into dh_mdokh(dh_id,dh_datin, dh_name, dh_custnr , dh_pricenet, dh_pricebrt) values(" + ID + ",'" + DATIN + "','"+ Name + "',"+ CustNR +" ,"+ PriceNET.ToString().Replace(",", ".") + "," + PriceBRT.ToString().Replace(",",".") + ");";
                MessageBox.Show(Request);
                CConnectSQL ConnectSQL = new CConnectSQL();
                return ConnectSQL.SQLQueryExc(Request);
            }
        }


        public bool CreatePosLine(int ID, int MAINID, string ArtName, int Quantity, double PriceNet, double PriceBrt) {
            if (ID == 0)
            {
                return false;
            }
            else
            {
                string Request = "insert into dl_mdokl(dl_id, dl_artname, dl_qua, dl_dhid , dl_pricenet, dl_pricebrt) values(" + ID + ",'" + ArtName + "'," + Quantity + "," + MAINID + " ," + PriceNet.ToString().Replace(",", ".") + "," + PriceBrt.ToString().Replace(",", ".") + ");";
                CConnectSQL ConnectSQL = new CConnectSQL();
                return ConnectSQL.SQLQueryExc(Request);
            }

        }



        public bool EditListLine (int ID, DateTime DATIN, string Name, int CustNR, double PriceNET, double PriceBRT) {
            if (ID == 0)
            {
                return false;
            }
            else
            {               
                string Request = "Update dh_mdokh SET dh_id = "+ ID + " , dh_datin = '" + DATIN + "', dh_name = '"+ Name + "', dh_custnr = " + CustNR + " , dh_pricenet = " + PriceNET.ToString().Replace(",", ".") + ", dh_pricebrt = " + PriceBRT.ToString().Replace(",", ".") + " where dh_id = " + ID + " ;";
                CConnectSQL ConnectSQL = new CConnectSQL();
                return ConnectSQL.SQLQueryExc(Request);
            }
            
        }

        public bool EditPosLine(int ID, int MAINID, string ArtName, int Quantity, double PriceNet, double PriceBrt)
        {
            if (ID == 0)
            {
                return false;
            }
            else
            {
                string Request = "Update dl_mdokl SET dl_id = " + ID + " , dl_dhid = " + MAINID + ", dl_artname = '" + ArtName + "', dl_qua = " + Quantity + " , dl_pricenet = " + PriceNet.ToString().Replace(",", ".") + ", dl_pricebrt = " + PriceBrt.ToString().Replace(",", ".") + " where dl_id = " + ID + " ;";
                CConnectSQL ConnectSQL = new CConnectSQL();
                return ConnectSQL.SQLQueryExc(Request);
            }
        }


        public bool DeleteListLine(int RowLine) {
            int DeletedID = TransactionListLine[RowLine].ID;

            if (DeletedID == 0 )
            {
                return false;
            }
            else
            {
                string Request = "DELETE FROM dh_mdokh WHERE dh_id = " + DeletedID + ";";
                CConnectSQL ConnectSQL = new CConnectSQL();
                return ConnectSQL.SQLQueryExc(Request);
            }



        }

        


                    public bool DeletePosLine(int RowLine)
        {
            int DeletedID = RowLine;

            if (DeletedID == 0)
            {
                return false;
            }
            else
            {
                string Request = "DELETE FROM dl_mdokl WHERE dl_id = " + DeletedID + ";";
                CConnectSQL ConnectSQL = new CConnectSQL();
                return ConnectSQL.SQLQueryExc(Request);
            }



        }



    }
}
