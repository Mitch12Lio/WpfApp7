using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using WpfApp7.Models;

namespace WpfApp7.ViewModels
{
    public class AtbashCipherViewModel : CipherViewModel
    {

        #region Properties

        Dictionary<char, char>? atbashDictionary = null;

        public Dictionary<char, char>? AtbashDictionary
        {
            get
            {
                return atbashDictionary;
            }
            set
            {
                atbashDictionary = value;                
                OnPropertyChanged(nameof(AtbashDictionary));
            }
        }

        private String answer = String.Empty;

        [Required(ErrorMessage = "Answer is Required")]
        public override String Answer
        {
            get
            {
                return answer;
            }
            set
            {
                answer = value;
                
                GetAssociatedLetter(value);
                Validate(nameof(Answer), value);
                //OnPropertyChanged(nameof(Answer));
               
            }
        }

        private String cipherType = Utilities.CipherTypes.Atbash.ToString();
        public override string CipherType {
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

        //private string visibilityAddButton = Utilities.VisibilityTypes.Visible.ToString();
        //public string VisibilityAddButton
        //{
        //    get
        //    {
        //        return visibilityAddButton;
        //    }
        //    set
        //    {
        //        visibilityAddButton = value;
        //        OnPropertyChanged(nameof(VisibilityAddButton));
        //    }
        //}

        //private string visibilityUpdateButton = Utilities.VisibilityTypes.Hidden.ToString();
        //public string VisibilityUpdateButton
        //{
        //    get
        //    {
        //        return visibilityUpdateButton;
        //    }
        //    set
        //    {
        //        visibilityUpdateButton = value;
        //        OnPropertyChanged(nameof(VisibilityUpdateButton));
        //    }
        //}

        #endregion

        public AtbashCipherViewModel()
        {
            AtbashDictionary = Utilities.Algorithms.CreateAtbashDictionary();
            //VisibilityAddButton = Utilities.VisibilityTypes.Visible.ToString();
            //VisibilityUpdateButton = Utilities.VisibilityTypes.Hidden.ToString();
            int u = 0;
        }

        #region Methods
        private void GetAssociatedLetter(string answer)
        {
            Hint = String.Empty;
            foreach (char c in answer)
            {
                Hint += GetRelatedCharacter(c);
            }
        }

        private char GetRelatedCharacter(char c)
        {
            if (AtbashDictionary != null)
            {
                if (AtbashDictionary.TryGetValue(c, out char value))
                {
                    return value;
                }
                else
                {
                    return c;
                }
            }
            else
            {
                return c;
            }
        }

        #endregion
    }
}
