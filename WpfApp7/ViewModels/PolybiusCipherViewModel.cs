using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp7.ViewModels
{
    public class PolybiusCipherViewModel : CipherViewModel
    {
        #region Fields

        private DataTable? polybiusDT = null;
        public DataTable? PolybiusDT
        {
            get
            {
                return polybiusDT;
            }
            set
            {

                polybiusDT = value;
                OnPropertyChanged(nameof(PolybiusDT));

            }
        }

        #endregion

        #region Properties

        private String answer = String.Empty;
        public override String Answer
        {
            get
            {
                return answer;
            }
            set
            {
                answer = value;
                DivulgeAnswer();
                OnPropertyChanged(nameof(Answer));
            }
        }

        private String cipherType = Utilities.CipherTypes.Polybius.ToString();
        public override string CipherType
        {
            get
            {
                return cipherType;
            }
            set
            {
                cipherType = value;
                OnPropertyChanged(nameof(CipherType));
            }
        }

        #endregion

        public PolybiusCipherViewModel()
        {
            PolybiusDT = Utilities.Algorithms.CreatePolybiusDataTable();
        }

        private void DivulgeAnswer()
        {
            if ((PolybiusDT != null))
            {
                Hint = string.Empty;

                foreach (char character in Answer)
                {
                    if ((character >= 'A' && character < 'Y') || (character >= 'a' && character < 'y'))
                    {
                        bool characterFound = false;
                        try
                        {
                            foreach (System.Data.DataRow dr in PolybiusDT.Rows)
                            {
                                for (int y = 1; y < PolybiusDT.Columns.Count; y++)
                                {
                                    Char currentDTChar = Convert.ToChar(dr[y]);
                                    if (currentDTChar == Char.ToUpper(character))
                                    {
                                        Hint += PolybiusDT.Columns[y].ColumnName + ":" + (PolybiusDT.Rows.IndexOf(dr) + 1).ToString() + " ";
                                        characterFound = true;
                                        break;
                                    }
                                }
                                if (characterFound)
                                {
                                    break;
                                }
                            }
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                    else
                    {
                        if (character == 'z')
                        {
                            Hint += "Z ";
                        }
                        else
                        {
                            Hint += character.ToString() + " ";
                        }
                    }
                }
            }
        }
    }
}
