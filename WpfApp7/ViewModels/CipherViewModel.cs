using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp7.Models;
using WpfApp7.MVVM;

namespace WpfApp7.ViewModels
{
    //public interface ICipherCryptograph
    //{
    //    public void Encrypt();
    //}
    //public class AtbashCryptograph : ICipherCryptograph
    //{
    //    public void Encrypt()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    //public class MorseCryptograph : ICipherCryptograph
    //{
    //    public void Encrypt()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    public abstract class CipherViewModel : ObservableObject
    {
        TroveViewModel troveVM;
        protected CipherViewModel(TroveViewModel troveVM)
        {
            int x = 0;
            this.troveVM = troveVM;
        }
        protected CipherViewModel()
        {
            
        }

        #region Properties

        private string visibilityAddButton = Utilities.VisibilityTypes.Visible.ToString();
        public string VisibilityAddButton
        {
            get
            {
                return visibilityAddButton;
            }
            set
            {
                visibilityAddButton = value;
                OnPropertyChanged(nameof(VisibilityAddButton));
            }
        }

        private string visibilityUpdateButton = Utilities.VisibilityTypes.Hidden.ToString();
        public string VisibilityUpdateButton
        {
            get
            {
                return visibilityUpdateButton;
            }
            set
            {
                visibilityUpdateButton = value;
                OnPropertyChanged(nameof(VisibilityUpdateButton));
            }
        }

        [Required]
        public virtual String Answer { get; set; }

        [Required]
        private String hint = String.Empty;
        public String Hint
        {
            get
            {
                return hint;
            }
            set
            {

                hint = value;
                //GetAssociatedLetter(value);
                OnPropertyChanged(nameof(Hint));

            }
        }

        public abstract String CipherType { get; set; }

        public virtual CipherLocation Location { get; set; }

        //private String answer = String.Empty;
        //public String Answer
        //{
        //    get
        //    {
        //        return answer;
        //    }
        //    set
        //    {

        //        answer = value;
        //        //GetAssociatedLetter(value);
        //        OnPropertyChanged(nameof(Answer));

        //    }
        //}

        //private String cipherType = "Atbash";
        //public String CipherType
        //{
        //    get
        //    {
        //        return cipherType;
        //    }
        //    set
        //    {
        //        cipherType = value;
        //        OnPropertyChanged(nameof(CipherType));
        //    }
        //}

        #endregion


        //ICipherCryptograph? iCipherCryptograph;

        //public void performCrypto() 
        //{
        //    iCipherCryptograph?.Encrypt();
        //}

    }
}
