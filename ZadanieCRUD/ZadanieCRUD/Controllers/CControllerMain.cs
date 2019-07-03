using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZadanieCRUD
{

    /// <summary>
    /// Interfejs kontrolera
    /// </summary>
    public interface IControllerMain
    {
        void NewTransactionListLine();
        void NewTransactionListLineViewON();
        void NewTransactionListLineViewOF();
        void NewPositionListLineViewOF();
        void EditTransactionListLineViewON(int RowLine);

        void DeleteTransactionListLineViewON(int RowLine);
        void DeletePositionListLineViewON(int IDPos);


        void EditTransactionListLine();
        void EditPositionListLine();

        void ReadAllTransactionLine();

        void ReadPositionLines(int RowLine);

        void NewPositionLineViewON(int RowLine);

        void NewPositionListLine();

        void EditPositionListLineViewON(int IDPos);
    }

    /// <summary>
    /// Klasa odpowiadająca za działanie kontrollera, implementująca interfejs IControllerMain
    /// </summary>
    class CControllerMain : IControllerMain
    {
        /// <summary>
        /// Zmienna odpowiadająca za model
        /// </summary>
        ITransactionList TransactionList;

        /// <summary>
        /// Zmienna odpowiadająca za Widok
        /// </summary>
        IFrmMainView FrmMainView;

        /// <summary>
        /// Główny kontruktor kontrolera - tworzący połączenie pomiędzy Main i View 
        /// </summary>
        /// <param name="FrmMainView_"></param>
        /// <param name="TransactionList_"></param>
        public CControllerMain(IFrmMainView FrmMainView_, ITransactionList TransactionList_)
        {
            this.TransactionList = TransactionList_;
            this.FrmMainView = FrmMainView_;
            FrmMainView.AddListener(this);
        }


        //
        //
        //--------------------------------------------METODY ODPOWIADAJĄCE ZA ZAPIS DANYCH I ZMIANĘ MODELU
        //
        //

//------------------------------------------------------------------------------------------------------------------------------NOWY

         /// <summary>
         /// Funkcja odpowiadająca za nową linie transakcji
         /// </summary>
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

        /// <summary>
        /// Funckja odpowiadająca za nową linie pozycji w transakcji
        /// </summary>
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


        //------------------------------------------------------------------------------------------------------------------------------EDYCJA BIEŻĄCYCH
        /// <summary>
        /// Funckja odpowiadająca za edycje linii transakcji
        /// </summary>
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


        /// <summary>
        /// Funckja odpowiadająca za edycje linii pozycji w transakcji
        /// </summary>
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



        //------------------------------------------------------------------------------------------------------------------------------USUNIĘCIE BIEŻĄCYCH

        /// <summary>
        /// Usunięcie linii transakcji
        /// </summary>
        /// <param name="RowLine"></param>
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
        /// <summary>
        /// Usunięcie linii pozycji
        /// </summary>
        /// <param name="RowLine"></param>
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





        /// <summary>
        /// Usunięcie linii transakcji
        /// </summary>
        /// <param name="RowLine"></param>
        public void DeleteTransactionListLineViewON(int RowLine)
        {
            DialogResult dialogResult = MessageBox.Show("Czy chcesz usunąć zaznaczony record?", "Usunięcie wpisu w bazie danych", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                TransactionList.DeletePosLines(RowLine);
                DeleteTransactionListLine(RowLine);

            }
            else if (dialogResult == DialogResult.No)
            {
                FrmMainView.NewTransactionListLineViewOF();
            }

        }

        /// <summary>
        /// Usunięcie linii pozycji
        /// </summary>
        /// <param name="IDPos"></param>
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


        //------------------------------------------------------------------------------------------------------------------------------USTAWIANIE WIDOKÓW
        public void ReadAllTransactionLine()
        {
            FrmMainView.SetDataGridView1(TransactionList.ReadList());
        }

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

       
        public void NewTransactionListLineViewOF()
        {
            FrmMainView.NewTransactionListLineViewOF();

        }


        public void NewPositionListLineViewOF()
        {

            FrmMainView.NewPositionListLineViewOF();
        }

        public void NewPositionLineViewON(int RowLine)
        {
            FrmMainView.NewPositionLineViewON(TransactionList.GetTransactionList(RowLine));
        }


    }
}
