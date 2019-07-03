using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZadanieCRUD
{

    /// <summary>
    /// Klasa odpowiadająca za linie w tabeli transakcji - DH_MDOKH
    /// </summary>
    public class CTransactionListLine
    {
        /// <summary>
        /// Lista wszystkich pozycji w transakcji
        /// </summary>
        public List<CTransactionPosLine> TransactionPosLine = new List<CTransactionPosLine>();

        public CTransactionListLine()
        {
            ID = 0;
            CustNumber = 0;
            Name = "";
            PriceNet = 0;
            PriceBrt = 0;
        }
        /// <summary>
        /// ID wpisu w tabeli
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Numer artykułu
        /// </summary>
        public DateTime Datin { get; set; }

        /// <summary>
        /// Ilość sztuk
        /// </summary>
        public int CustNumber { get; set; }

        /// <summary>
        /// Ilość sztuk
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Cena netto
        /// </summary>
        public double PriceNet { get; set; }

        /// <summary>
        /// Cena brutto
        /// </summary>
        public double PriceBrt { get; set; }





        public DataTable ReadPosList()
        {

            DataTable ReadListTable = null;

            CConnectSQL ConnectSQL = new CConnectSQL();
            ReadListTable = ConnectSQL.SQLReadData("select dl_id, dl_artname, dl_qua, dl_pricenet, dl_pricebrt, dl_dhid, MAX(dl_id) as maxID from dl_mdokl where dl_dhid = " + ID + " GROUP BY dl_id order by dl_id ");
            SaveTableAsList(ReadListTable); 
            return ReadListTable;
        }


        private void SaveTableAsList(DataTable ReadTable)
        {

            foreach (DataRow dr2 in ReadTable.Rows)
            {
                if (!DBNull.Value.Equals(dr2["dl_id"]))
                {
                    TransactionPosLine.Add(new CTransactionPosLine() { ID = Convert.ToInt32(dr2["dl_id"]) });
                    if (!DBNull.Value.Equals(dr2["dl_qua"])) { TransactionPosLine[TransactionPosLine.Count - 1].Quantity = Convert.ToInt32(dr2["dl_qua"]); }
                    if (!DBNull.Value.Equals(dr2["maxID"])) { TransactionPosLine[TransactionPosLine.Count - 1].MaxID = Convert.ToInt32(dr2["maxID"]); }
                    if (!DBNull.Value.Equals(dr2["dl_artname"])) { TransactionPosLine[TransactionPosLine.Count - 1].ArtName = dr2["dl_artname"].ToString(); }
                    if (!DBNull.Value.Equals(dr2["dl_pricenet"])) { TransactionPosLine[TransactionPosLine.Count - 1].PriceNet = Convert.ToDouble(dr2["dl_pricenet"]); }
                    if (!DBNull.Value.Equals(dr2["dl_pricebrt"])) { TransactionPosLine[TransactionPosLine.Count - 1].PriceBrt = Convert.ToDouble(dr2["dl_pricebrt"]); ; }
                }

            }


        }
    }
}


