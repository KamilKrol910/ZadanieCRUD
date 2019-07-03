using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZadanieCRUD
{
    static class SCFunction
    {
        /// <summary>
        /// Konwersja string na double
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static double ConvertStringToDouble(string Value)
        {
            if (Value == null) //odrzucenie pustego recordu
            {
                return 0;
            }
            else
            {
                double OutVal;
                double.TryParse(Value, out OutVal);

                if (double.IsNaN(OutVal) || double.IsInfinity(OutVal)) //sprawdzenie, czy konwersja się udała
                {
                    return 0;
                }
                return OutVal;
            }
        }


        public static int ConvertStringToInt(string Value)
        {
            int n;
            bool isNumeric = int.TryParse(Value, out n);
            if (isNumeric)
            {
                return n;
            }
            else
            {
                return 0;
            }

        }



    }
}
