using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZadanieCRUD.Models
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

    }
}
