using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZadanieCRUD
{

    /// <summary>
    /// Klasa odpowiadająca za linie w tabeli transakcji - DH_MDOKH
    /// </summary>
    class CTransactionListLine
    {
        /// <summary>
        /// Lista wszystkich pozycji w transakcji
        /// </summary>
        List<CTransactionPosLine> TransactionPosLine;

        public CTransactionListLine()
        { }
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

    }
}
