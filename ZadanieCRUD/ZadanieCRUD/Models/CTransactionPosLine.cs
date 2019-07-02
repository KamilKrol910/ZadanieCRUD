using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZadanieCRUD
{
    /// <summary>
    /// Klasa odpowiadająca za linie w tabeli pozycji w liniach transakcji - DL_MDOKL
    /// </summary>
    public class CTransactionPosLine
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
        public double PriceNet { get; set; }

        /// <summary>
        /// Cena brutto
        /// </summary>
        public double PriceBrt { get; set; }

        /// <summary>
        /// maksymalne id w tabeli
        /// </summary>
        public int MaxID { get; set; }

    }
}
