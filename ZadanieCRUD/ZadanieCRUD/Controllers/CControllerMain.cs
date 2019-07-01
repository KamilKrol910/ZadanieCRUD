using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZadanieCRUD
{
    public interface IControllerMain
    {
        void NewTransactionListLine();
        void EditTransactionListLine();
        void ReadAllTransactionLine();
        void DeleteTransactionListLine();

    }


    class CControllerMain : IControllerMain
    {
        ITransactionList TransactionList;
        IFrmMainView FrmMainView;

        public CControllerMain(IFrmMainView FrmMainView_, ITransactionList TransactionList_)
        {
            this.TransactionList = TransactionList_;
            this.FrmMainView = FrmMainView_;
            FrmMainView.AddListener(this);
        }

        public void NewTransactionListLine() { }
        public void EditTransactionListLine() { }

        public void ReadAllTransactionLine()
        {
            FrmMainView.SetDataGridView(TransactionList.ReadList());

        }
        public void DeleteTransactionListLine() { }
    }
}
