using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;
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

    public abstract class CipherViewModel : ObservableObject,INotifyDataErrorInfo
    {
       
        protected CipherViewModel()
        {
            int x = 0;
           
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


        public virtual String Answer { get; set; }

        //[Required(ErrorMessage = "Answer is Required")]
        //public virtual String Answer
        //{
        //    get
        //    {
        //        return answer;
        //    }
        //    set
        //    {

        //        answer = value;
        //        Validate(nameof(Answer), value);
        //        //OnPropertyChanged(nameof(Answer));

        //    }
        //}


        
        private String hint = String.Empty;

        [Required(ErrorMessage = "Cipher is Required")]
        public String Hint
        {
            get
            {
                return hint;
            }
            set
            {

                hint = value;
                //Validate(nameof(Hint), value);
                OnPropertyChanged(nameof(Hint));

            }
        }

        public abstract String CipherType { get; set; }

        public virtual CipherLocation Location { get; set; }

        #endregion

        #region Validation

        Dictionary<string, List<string>> Errors = new Dictionary<string, List<string>>();
        public bool HasErrors => Errors.Count > 0;

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        public IEnumerable GetErrors(string? propertyName)
        {
            if (Errors.ContainsKey(propertyName))
            {
                return Errors[propertyName];
            }
            else { return Enumerable.Empty<string>(); }
        }

        public void Validate(string propertyName, object propertyValue)
        {
            var results = new List<ValidationResult>();
            Validator.TryValidateProperty(propertyValue, new ValidationContext(this) { MemberName = propertyName }, results);
            if (results.Any())
            {
                Errors.Add(propertyName, results.Select(r => r.ErrorMessage).ToList());
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            }
            else
            {
                Errors.Remove(propertyName);
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            }

            //SubmitCommand.RaiseCanExecuteChanged();
        }

        #endregion


    }
}
