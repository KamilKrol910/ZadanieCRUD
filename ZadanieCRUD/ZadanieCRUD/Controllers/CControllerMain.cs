using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZadanieCRUD
{
    public interface IControllerMain
    {
        void NewTransactionListLine();
        void NewTransactionListLineViewON();
        void NewTransactionListLineViewOF();

        void EditTransactionListLineViewON(int RowLine);

        void DeleteTransactionListLineViewON(int RowLine);
        void DeletePositionListLineViewON(int IDPos);


        void EditTransactionListLine();
        void EditPositionListLine();

        void ReadAllTransactionLine();
        void DeleteTransactionListLine();

        void ReadPositionLines(int RowLine);



        void NewPositionLineViewON(int RowLine);

        void NewPositionListLine();

        void EditPositionListLineViewON(int IDPos);
    }


    class CControllerMain : IControllerMain
    {
        ITransactionList TransactionList;
        IFrmMainView FrmMainView;
        CTransactionListLine NewTransactionLine;

        public CControllerMain(IFrmMainView FrmMainView_, ITransactionList TransactionList_)
        {
            this.TransactionList = TransactionList_;
            this.FrmMainView = FrmMainView_;
            FrmMainView.AddListener(this);
        }

        public void NewTransactionListLine() {

           if ( TransactionList.CreateListLine(FrmMainView.ReadTransactionID(), FrmMainView.ReadTransactionDate(), FrmMainView.ReadTransactionName(), FrmMainView.ReadTransactionCustNr(), FrmMainView.ReadTransactionPriceNet(), FrmMainView.ReadTransactionPriceBrt()) == false)
            {
                MessageBox.Show("Błąd podczas zapisu danych do bazy - błędny numer id");
            }
           else
            {
                MessageBox.Show("Poprawnie wczytano nowy wpis do bazy");
            }
            FrmMainView.NewTransactionListLineViewOF();
            ReadAllTransactionLine();
        }
        public void EditTransactionListLine() {

            if (TransactionList.EditListLine(FrmMainView.ReadTransactionID(), FrmMainView.ReadTransactionDate(), FrmMainView.ReadTransactionName(), FrmMainView.ReadTransactionCustNr(), FrmMainView.ReadTransactionPriceNet(), FrmMainView.ReadTransactionPriceBrt()) == false)
            {
                MessageBox.Show("Błąd podczas zapisu danych do bazy - błędny numer id");
            }
            else
            {
                MessageBox.Show("Poprawnie zedytowano wpis do bazy");
            }
            FrmMainView.NewTransactionListLineViewOF();
            ReadAllTransactionLine();
        }

        public void EditPositionListLine()
        {

            if (TransactionList.EditPosLine(FrmMainView.ReadPositionID(), FrmMainView.ReadPosTrID(), FrmMainView.ReadArtNumber(), FrmMainView.ReadPositionQuantity(), FrmMainView.ReadPositionPriceNet(), FrmMainView.ReadPositionPriceBrt()) == false)
            {
                MessageBox.Show("Błąd podczas zapisu danych do bazy - błędny numer id");
            }
            else
            {
                MessageBox.Show("Poprawnie zedytowano wpis do bazy");
            }
            FrmMainView.NewTransactionListLineViewOF();
            ReadAllTransactionLine();
        }
        

        public void ReadAllTransactionLine()
        {
            FrmMainView.SetDataGridView1(TransactionList.ReadList());
        }


        /// <summary>
        /// Uzupełnienie pozycji w linii transakcji
        /// </summary>
        public void ReadPositionLines(int RowLine)
        {
            FrmMainView.SetDataGridView2(TransactionList.ReadPosList(RowLine));
        }
            


        public void NewTransactionListLineViewON()
        {
            FrmMainView.NewTransactionListLineViewON(TransactionList.ReadLastIndexTransaction()+1);
        }

        public void EditTransactionListLineViewON(int RowLine)
        {
            FrmMainView.EditTransactionListLineViewON(TransactionList.GetTransactionList(RowLine));
            ReadPositionLines(RowLine);
        }

        public void EditPositionListLineViewON(int RowLine)
        {
            FrmMainView.EditPositionListLineViewON(RowLine);
        }

        

        public void DeleteTransactionListLineViewON(int RowLine)
        {
            DialogResult dialogResult = MessageBox.Show("Czy chcesz usunąć zaznaczony record?", "Usunięcie wpisu w bazie danych", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                DeleteTransactionListLine(RowLine);
            }
            else if (dialogResult == DialogResult.No)
            {
                FrmMainView.NewTransactionListLineViewOF();
            }

        }

        public void DeletePositionListLineViewON(int IDPos)
        {
            DialogResult dialogResult = MessageBox.Show("Czy chcesz usunąć zaznaczoną pozycję?", "Usunięcie wpisu w bazie danych", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                DeletePositionListLine(IDPos);
            }
            else if (dialogResult == DialogResult.No)
            {
                FrmMainView.NewTransactionListLineViewOF();
            }

        }



        private void DeleteTransactionListLine(int RowLine)
        {
            if (TransactionList.DeleteListLine(RowLine) == false)
            {
                MessageBox.Show("Błąd podczas usunięcia danych do bazy - błędny numer id");
            }
            else
            {
                MessageBox.Show("Poprawnie zedytowano wpis do bazy");
            }
            FrmMainView.NewTransactionListLineViewOF();
            ReadAllTransactionLine();

        }

        private void DeletePositionListLine(int RowLine)
        {
         if (TransactionList.DeletePosLine(RowLine) == false)
            {
                MessageBox.Show("Błąd podczas usunięcia danych do bazy - błędny numer id");
            }
            else
            {
                MessageBox.Show("Poprawnie zedytowano wpis do bazy");
            }
            FrmMainView.NewTransactionListLineViewOF();
            ReadAllTransactionLine();
            
        }

        

        public void NewTransactionListLineViewOF()
        {
            FrmMainView.NewTransactionListLineViewOF();

        }

        public void DeleteTransactionListLine() { }

        public void NewPositionLineViewON(int RowLine)
        {
            FrmMainView.NewPositionLineViewON(TransactionList.GetTransactionList(RowLine));
        }


        public void NewPositionListLine()
        {

            if (TransactionList.CreatePosLine(FrmMainView.ReadPositionID(), FrmMainView.ReadPosTrID(), FrmMainView.ReadArtNumber(), FrmMainView.ReadPositionQuantity(), FrmMainView.ReadPositionPriceNet(), FrmMainView.ReadPositionPriceBrt()) == false)
            {
                MessageBox.Show("Błąd podczas zapisu danych do bazy - błędny numer id");
            }
            else
            {
                MessageBox.Show("Poprawnie wczytano nowy wpis pozycji do bazy");
            }
            FrmMainView.NewTransactionListLineViewOF();
            ReadAllTransactionLine();
        }

    }
}
