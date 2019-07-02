﻿using System;
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

        int ReadLastIndexTransaction();

        bool CreateListLine(int ID, DateTime DATIN, string Name, int CustNR, double PriceNET, double PriceBRT);

        bool EditListLine(int ID, DateTime DATIN, string Name, int CustNR, double PriceNET, double PriceBRT);

        CTransactionListLine GetTransactionList(int RowLine);

        bool DeleteListLine(int RowLine);

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
            //if (TransactionListLine != null)
           // { TransactionListLine.Clear(); }
            

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

      


      


        public bool CreateListLine(int ID, DateTime DATIN, string Name, int CustNR, double PriceNET, double PriceBRT) {
            if (ID ==0 )
            {
                return false;
            }
            else
            {
                string Request = "insert into dh_mdokh(dh_id,dh_datin, dh_name, dh_custnr , dh_pricenet, dh_pricebrt) values(" + ID + ",'" + DATIN + "','"+ Name + "',"+ CustNR +" ,"+ PriceNET + "," + PriceBRT + ");";
                CConnectSQL ConnectSQL = new CConnectSQL();
                return ConnectSQL.InsertData(Request);
            }

        }
        public bool EditListLine (int ID, DateTime DATIN, string Name, int CustNR, double PriceNET, double PriceBRT) {
            if (ID == 0)
            {
                return false;
            }
            else
            {               
                string Request = "Update dh_mdokh SET dh_id = "+ ID + " , dh_datin = '" + DATIN + "', dh_name = '"+ Name + "', dh_custnr = " + CustNR + " , dh_pricenet = " + PriceNET + ", dh_pricebrt = " + PriceBRT + " where dh_id = " + ID + " ;";
                CConnectSQL ConnectSQL = new CConnectSQL();
                return ConnectSQL.InsertData(Request);
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
                return ConnectSQL.InsertData(Request);
            }



        }


         
    }
}
