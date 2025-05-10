using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Media;
using WpfApp7.Models;
using WpfApp7.MVVM;

namespace WpfApp7.ViewModels
{
    public class TroveViewModel : ObservableObject
    {
        public AtbashCipherViewModel AtbashVM { get; private set; }
        public MorseCipherViewModel MorseVM { get; private set; }
        public PolybiusCipherViewModel PolybiusVM { get; private set; }

        #region Properties
        #region Egg Colours Properties

        private System.Collections.ObjectModel.ObservableCollection<EggColour> eggColours = new System.Collections.ObjectModel.ObservableCollection<EggColour>();
        public System.Collections.ObjectModel.ObservableCollection<EggColour> EggColours
        {
            get
            {
                return eggColours;
            }
            set
            {
                eggColours = value;
                OnPropertyChanged(nameof(EggColours));
            }
        }

        private EggColour? selectedEggColour = null;
        public EggColour? SelectedEggColour
        {
            get
            {
                return selectedEggColour;
            }
            set
            {
                selectedEggColour = value;
                OnPropertyChanged(nameof(SelectedEggColour));
            }
        }

        #endregion
        #region Location Properties

        private System.Collections.ObjectModel.ObservableCollection<CipherLocation> cipherLocations = new System.Collections.ObjectModel.ObservableCollection<CipherLocation>();
        public System.Collections.ObjectModel.ObservableCollection<CipherLocation> CipherLocations
        {
            get
            {
                return cipherLocations;
            }
            set
            {
                cipherLocations = value;
                OnPropertyChanged(nameof(CipherLocations));
            }
        }

        private CipherLocation? selectedLocation = null;
        public CipherLocation? SelectedLocation
        {
            get
            {
                return selectedLocation;
            }
            set
            {
                selectedLocation = value;
                OnPropertyChanged(nameof(SelectedLocation));
            }
        }

        #endregion

        #region Tabs

        private bool isAtbashTabSelected = true;
        public bool IsAtbashTabSelected
        {
            get { return isAtbashTabSelected; }
            set
            {
                if (value != isAtbashTabSelected)
                {
                    isAtbashTabSelected = value;
                    OnPropertyChanged(nameof(IsAtbashTabSelected));
                }
            }
        }
        private bool isMorseTabSelected = false;
        public bool IsMorseTabSelected
        {
            get { return isMorseTabSelected; }
            set
            {
                if (value != isMorseTabSelected)
                {
                    isMorseTabSelected = value;
                    OnPropertyChanged(nameof(IsMorseTabSelected));
                }
            }
        }
        private bool isPolybiusTabSelected = false;
        public bool IsPolybiusTabSelected
        {
            get { return isPolybiusTabSelected; }
            set
            {
                if (value != isPolybiusTabSelected)
                {
                    isPolybiusTabSelected = value;
                    OnPropertyChanged(nameof(IsPolybiusTabSelected));
                }
            }
        }

        #endregion

        private int cipherCount = 0;
        public int CipherCount
        {
            get
            {
                return cipherCount;
            }
            set
            {
                if (cipherCount != value)
                {
                    cipherCount = value;
                    OnPropertyChanged(nameof(CipherCount));
                }
            }

        }
        private System.Collections.ObjectModel.ObservableCollection<Cipher> ciphers = new System.Collections.ObjectModel.ObservableCollection<Cipher>();
        public System.Collections.ObjectModel.ObservableCollection<Cipher> Ciphers
        {
            get
            {
                return ciphers;

            }
            set
            {
                ciphers = value;
                OnPropertyChanged(nameof(Ciphers));
            }
        }

        private Cipher? selectedCipher = null;
        public Cipher? SelectedCipher
        {
            get
            {
                return selectedCipher;
            }
            set
            {
                selectedCipher = value;
                TabLogic(selectedCipher);
                OnPropertyChanged(nameof(SelectedCipher));
            }
        }

        #endregion

        public TroveViewModel()
        {
            AtbashVM = new AtbashCipherViewModel();
            MorseVM = new MorseCipherViewModel();
            PolybiusVM = new PolybiusCipherViewModel();
            FillCipherLocations();
            FillEggColours();
        }

        #region Trove

        private void TabLogic(Cipher? selectedCipher)
        {
            CipherViewModel? currentVM = null;
            if (selectedCipher != null)
            {
                if (selectedCipher.GetType() == typeof(AtbashCipher))
                {
                    IsAtbashTabSelected = true;
                    currentVM = AtbashVM;
                }
                else if (selectedCipher.GetType() == typeof(MorseCipher))
                {
                    IsMorseTabSelected = true;
                    currentVM = MorseVM;
                }
                else
                {
                    IsPolybiusTabSelected = true;
                    currentVM = PolybiusVM;
                }
                ViewLogic(selectedCipher, currentVM);
                SelectedLocation = selectedCipher.CipherLocation;
                SelectedEggColour = selectedCipher.EggColour;
            }
            else
            {
                AtbashVM.Answer = string.Empty;
                AtbashVM.Hint = string.Empty;
                AtbashVM.VisibilityAddButton = Utilities.VisibilityTypes.Visible.ToString();
                AtbashVM.VisibilityUpdateButton = Utilities.VisibilityTypes.Hidden.ToString();

                MorseVM.Answer = string.Empty;
                MorseVM.Hint = string.Empty;
                MorseVM.VisibilityAddButton = Utilities.VisibilityTypes.Visible.ToString();
                MorseVM.VisibilityUpdateButton = Utilities.VisibilityTypes.Hidden.ToString();

                PolybiusVM.Answer = string.Empty;
                PolybiusVM.Hint = string.Empty;
                PolybiusVM.VisibilityAddButton = Utilities.VisibilityTypes.Visible.ToString();
                PolybiusVM.VisibilityUpdateButton = Utilities.VisibilityTypes.Hidden.ToString();

                SelectedLocation = null;
                SelectedEggColour = null;
            }

        }

        private void ViewLogic(Cipher selectedCipher, CipherViewModel cipherVM)
        {
            cipherVM.Answer = selectedCipher.Answer;
            cipherVM.Hint = selectedCipher.Hint;
            cipherVM.VisibilityAddButton = Utilities.VisibilityTypes.Hidden.ToString();
            cipherVM.VisibilityUpdateButton = Utilities.VisibilityTypes.Visible.ToString();
        }

        public void PrintCipherList(string path)
        {
            string dateGuid = DateTime.Now.ToString("yyyy.MM.dd.HH.mm.ss.ffff");
            using (System.IO.StreamWriter file4 = new System.IO.StreamWriter(path, true))
            {
                foreach (var cipher in Ciphers)
                {
                    file4.WriteLine(Environment.NewLine);
                    file4.WriteLine(cipher.Hint);
                    file4.WriteLine(Environment.NewLine);
                    if (cipher.CipherLocation != null)
                    {
                        file4.WriteLine(cipher.CipherLocation.Location);
                        file4.WriteLine(Environment.NewLine);
                    }
                    file4.WriteLine(cipher.Answer);
                    file4.WriteLine(Environment.NewLine);
                    file4.WriteLine(@"-----------------------");
                }
            }
        }

        public void Remove()
        {
            if (SelectedCipher != null)
            {
                Ciphers.Remove(SelectedCipher);
                CountCiphers();
            }
        }

        public void Clear()
        {
            Ciphers.Clear();
            CountCiphers();
        }

        private void CountCiphers()
        {
            CipherCount = Ciphers.Count();
        }

        #endregion

        private void Add(Cipher newCipher, CipherViewModel viewModel)
        {
            newCipher.Answer = viewModel.Answer;
            newCipher.Hint = viewModel.Hint;
            newCipher.CipherLocation = SelectedLocation;
            newCipher.CipherType = viewModel.CipherType;
            newCipher.EggColour = SelectedEggColour;

            Ciphers.Add(newCipher);
            Clear(viewModel);
            CountCiphers();
        }

        private void Update(Cipher cipher, CipherViewModel viewModel)
        {
            cipher.Answer = viewModel.Answer;
            cipher.Hint = viewModel.Hint;
            cipher.CipherType = viewModel.CipherType;
            cipher.CipherLocation = SelectedLocation;
            cipher.EggColour = SelectedEggColour;

        }

        private void Clear(CipherViewModel viewModel)
        {
            viewModel.Answer = string.Empty;
            viewModel.Hint = string.Empty;
            SelectedLocation = null;
            SelectedEggColour = null;
        }

        #region Atbash

        public void AddAtbash()
        {
            Add(new AtbashCipher(), AtbashVM);
        }

        public void UpdateAtbash()
        {
            if (SelectedCipher != null)
            {
                Update(SelectedCipher, AtbashVM);
            }
        }

        public void ClearAtbashFields()
        {
            Clear(AtbashVM);
        }

        #endregion

        #region Morse
        public void AddMorse()
        {
            Add(new MorseCipher(), MorseVM);
        }

        public void UpdateMorse()
        {
            if (SelectedCipher != null)
            {
                Update(SelectedCipher, MorseVM);
            }
        }

        public void ClearMorseFields()
        {
            Clear(MorseVM);
        }

        #endregion

        #region Polybius

        public void AddPolybius()
        {
            Add(new PolybiusCipher(), PolybiusVM);
        }

        public void UpdatePolybius()
        {
            if (SelectedCipher != null)
            {
                Update(SelectedCipher, PolybiusVM);
            }
        }

        public void ClearPolybiusFields()
        {
            Clear(PolybiusVM);
        }

        #endregion

        #region Setup

        private void FillCipherLocations()
        {
            CipherLocations.Add(new CipherLocation { Id = 0, Location = Utilities.LocationTypes.MainLevel });
            CipherLocations.Add(new CipherLocation { Id = 0, Location = Utilities.LocationTypes.Upstairs });
            CipherLocations.Add(new CipherLocation { Id = 0, Location = Utilities.LocationTypes.Basement });
            CipherLocations.Add(new CipherLocation { Id = 0, Location = Utilities.LocationTypes.Attic });
            CipherLocations.Add(new CipherLocation { Id = 0, Location = Utilities.LocationTypes.Garage });
            CipherLocations.Add(new CipherLocation { Id = 0, Location = Utilities.LocationTypes.Frontyard });
            CipherLocations.Add(new CipherLocation { Id = 0, Location = Utilities.LocationTypes.Backyard });
            CipherLocations.Add(new CipherLocation { Id = 0, Location = Utilities.LocationTypes.Other });
        }

        private void FillEggColours()
        {
            EggColours.Add(new EggColour { Colour = Utilities.EggColours.Blue.ToString() });
            EggColours.Add(new EggColour { Colour = Utilities.EggColours.Yellow.ToString() });
            EggColours.Add(new EggColour { Colour = Utilities.EggColours.Red.ToString() });
            EggColours.Add(new EggColour { Colour = Utilities.EggColours.Green.ToString() });
            EggColours.Add(new EggColour { Colour = Utilities.EggColours.Purple.ToString() });
            EggColours.Add(new EggColour { Colour = Utilities.EggColours.Orange.ToString() });
        }


        #endregion


    }
}
