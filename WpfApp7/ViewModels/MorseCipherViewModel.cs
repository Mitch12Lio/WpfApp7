using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp7.Models;

namespace WpfApp7.ViewModels
{
    public class MorseCipherViewModel : CipherViewModel
    {
        #region Properties

        Dictionary<char, String>? morseDictionary = null;

        public Dictionary<char, String>? MorseDictionary
        {
            get
            {
                return morseDictionary;
            }
            set
            {

                morseDictionary = value;               
                OnPropertyChanged(nameof(MorseDictionary));

            }
        }


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
                GetAssociatedString(value);
                OnPropertyChanged(nameof(Answer));

            }
        }

        private String cipherType = Utilities.CipherTypes.Morse.ToString();
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

        public MorseCipherViewModel()
        {
            MorseDictionary = Utilities.Algorithms.CreateMorseDictionary();
        }

        #region Methods

      

        private void GetAssociatedString(string answer)
        {
            Hint = String.Empty;
            foreach (char c in answer.ToUpper())
            {
                Hint += GetRelatedString(c) + " ";
            }

            //return answer;
        }

        private string GetRelatedString(char c)
        {
            if ((MorseDictionary != null))
            {
                if (MorseDictionary.TryGetValue(c, out string? value))
                {
                    return value;
                }
                else
                {
                    return c.ToString();
                }
            }
            else { return c.ToString(); }
        }

        #endregion
    }
}
