using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZadanieCRUD
{
    static class Program
    {
        /// <summary>
        /// Główny punkt wejścia dla aplikacji.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            FrmMainView MainView = new FrmMainView();
            CTransactionList TransactionList = new CTransactionList();
            CControllerMain ControllerMain = new CControllerMain(MainView, TransactionList);

            Application.Run(MainView);
        }
    }
}
