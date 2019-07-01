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
        void CreateListLine();
        void EditListLine();
        void DeleteListLine();

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
            ReadListTable = ConnectSQL.ReadTableFromSQL("select * from dh_mdokh where dh_id > 0 order by dh_id");
            SaveTableAsList(ReadListTable);
       
            return ReadListTable;
        }

        private void SaveTableAsList (DataTable ReadTable)
        {
            //if (TransactionListLine != null)
           // { TransactionListLine.Clear(); }
            

            foreach (DataRow dr2 in ReadTable.Rows)
            {
                if (!DBNull.Value.Equals(dr2["dh_id"]))
                {
                    TransactionListLine.Add(new CTransactionListLine());
                    if (!DBNull.Value.Equals(dr2["dh_datin"])) { TransactionListLine[TransactionListLine.Count - 1].Datin = Convert.ToDateTime(dr2["dh_datin"]); }
                    if (!DBNull.Value.Equals(dr2["dh_custnr"])) { TransactionListLine[TransactionListLine.Count - 1].CustNumber = Convert.ToInt32(dr2["dh_custnr"]); }
                    if (!DBNull.Value.Equals(dr2["dh_name"])) { TransactionListLine[TransactionListLine.Count - 1].Name = dr2["dh_name"].ToString(); }
                    if (!DBNull.Value.Equals(dr2["dh_pricenet"])) { TransactionListLine[TransactionListLine.Count - 1].PriceNet = Convert.ToDouble(dr2["dh_pricenet"]); }
                    if (!DBNull.Value.Equals(dr2["dh_pricebrt"])) { TransactionListLine[TransactionListLine.Count - 1].PriceBrt = Convert.ToDouble(dr2["dh_pricebrt"]); ; }
                }
 

                
            }
            // try
            // {
            /*    TransactionListLine = (from DataRow dr in ReadTable.Rows                       
                                       select new CTransactionListLine()
                                       {
                                           ID = CheckInt32Obj(dr["dh_id"]),
                                          // Datin = CheckDateTimeObj(dr["dh_datin"]),
                                           CustNumber = CheckInt32Obj(dr["dh_custnr"]),
                                           Name = CheckStringObj(dr["dh_name"]),
                                           PriceNet = CheckDoubleObj(dr["dh_pricenet"]),
                                           PriceBrt = CheckDoubleObj(dr["dh_pricebrt"])
                                       }).ToList();*/
            /*  }
              catch (Exception e)
              {
                  MessageBox.Show("Błąd podczas konwersji danych z db");
              }*/
           // MessageBox.Show("pobrane");

        }

       private int CheckInt32Obj (Object IntVar)
        {
            MessageBox.Show("IntVAR" + IntVar.ToString());
            if (IntVar==null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(IntVar);
            }
        }

        private double CheckDoubleObj (Object IntVar)
        {
            if (IntVar == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToDouble(IntVar);
            }
        }


        private string CheckStringObj(Object IntVar)
        {
            if (IntVar == null)
            {
                return "";
            }
            else
            {
                return IntVar.ToString();
            }
        }

        private DateTime CheckDateTimeObj(Object IntVar)
        {
            if (IntVar == null)
            {
                return DateTime.MaxValue;
            }
            else
            {
                return Convert.ToDateTime(IntVar);

                
            }
        }

        public void CreateListLine() { }
        public void EditListLine() { }
        public void DeleteListLine() { }


         
    }
}
