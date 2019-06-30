using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZadanieCRUD.Models
{
    /// <summary>
    /// Klasa odpowiadająca za linie w tabeli pozycji w liniach transakcji - DL_MDOKL
    /// </summary>
    class CTransactionPosLine
    {
        
        /// <summary>
        /// ID wpisu w tabeli
        /// </summary>
        public int  ID { get; set; }

        /// <summary>
        /// Numer artykułu
        /// </summary>
        public string ArtName { get; set; }

        /// <summary>
        /// Ilość sztuk
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Cena netto
        /// </summary>
        public float PriceNet { get; set; }

        /// <summary>
        /// Cena brutto
        /// </summary>
        public float ProceBrt { get; set; }

    }
}
