using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp7.Models;
using WpfApp7.MVVM;

namespace WpfApp7.ViewModels
{
    public class MasterViewModel:ObservableObject
    {
        public TroveViewModel TroveVM { get; private set; }

        #region Global Properties       

        private string globalLogLocation = Properties.MasterVM.Default.GlobalLogLocation;
        public string GlobalLogLocation
        {
            get
            {
                return globalLogLocation;
            }
            set
            {
                if (globalLogLocation != value)
                {
                    globalLogLocation = value;

                    Properties.MasterVM.Default.GlobalLogLocation = value;
                    Properties.MasterVM.Default.Save();
                    OnPropertyChanged(nameof(GlobalLogLocation));
                }
            }
        }

        private string statusMessage = "Ready!";
        public string StatusMessage
        {
            get
            {
                return statusMessage;
            }
            set
            {
                if (statusMessage != value)
                {
                    statusMessage = value;
                    OnPropertyChanged(nameof(StatusMessage));
                }
            }
        }
        #endregion

        public MasterViewModel()
        {
            TroveVM = new TroveViewModel();

            SetSaveCommand = new RelayCommand(SetSave, param => true);
            SetFileCommand = new RelayCommand(SetFile, param => true);
            SetFolderCommand = new RelayCommand(SetFolder, param => true);
            OpenWindowsExplorerCommand = new RelayCommand(OpenWindowsExplorer, param => true);

        }

        #region Throve

        private ICommand? removeCipherCommand;
        public ICommand RemoveCipherCommand
        {
            get
            {
                if (removeCipherCommand == null)
                {
                    removeCipherCommand = new RelayCommand(param => RemoveCipher());
                }
                return removeCipherCommand;
            }
        }
        public void RemoveCipher()
        {
            TroveVM.Remove();
        }

        private ICommand? printCipherListCommand;
        public ICommand PrintCipherListCommand
        {
            get
            {
                if (printCipherListCommand == null)
                {
                    printCipherListCommand = new RelayCommand(param => PrintCipherList());
                }
                return printCipherListCommand;
            }
        }
        public void PrintCipherList()
        {
            try
            {
                string dateGuid = DateTime.Now.ToString("yyyy.MM.dd.HH.mm.ss.ffff");
                TroveVM.PrintCipherList(GlobalLogLocation + Path.DirectorySeparatorChar + "EasterHunt_" + dateGuid + ".txt");
                StatusMessage = "Print Successfull";
            }
            catch (Exception ex)
            {
                StatusMessage = ex.Message;
            }
            
        }

        private ICommand? clearCiphersCommand;
        public ICommand ClearCiphersCommand
        {
            get
            {
                if (clearCiphersCommand == null)
                {
                    clearCiphersCommand = new RelayCommand(param => ClearCiphers());
                }
                return clearCiphersCommand;
            }
        }
        public void ClearCiphers()
        {
            TroveVM.Clear();
        }

        #endregion

        #region Atbash       

        private ICommand? addAtbashCommand;
        public ICommand AddAtbashCommand
        {
            get
            {
                if (addAtbashCommand == null)
                {
                    addAtbashCommand = new RelayCommand(param => AddAtbash());
                }
                return addAtbashCommand;
            }
        }
        public void AddAtbash()
        {
            TroveVM.AddAtbash();
        }

        private ICommand? updateAtbashCommand;
        public ICommand UpdateAtbashCommand
        {
            get
            {
                if (updateAtbashCommand == null)
                {
                    updateAtbashCommand = new RelayCommand(param => UpdateAtbash());
                }
                return updateAtbashCommand;
            }
        }

        public void UpdateAtbash()
        {
            TroveVM.UpdateAtbash();
        }

        private ICommand? clearAtbashCommand;
        public ICommand ClearAtbashCommand
        {
            get
            {
                if (clearAtbashCommand == null)
                {
                    clearAtbashCommand = new RelayCommand(param => ClearAtbash());
                }
                return clearAtbashCommand;
            }
        }

        public void ClearAtbash()
        {
            TroveVM.ClearAtbashFields();
        }

        #endregion

        #region Morse

        private ICommand? addMorseCommand;
        public ICommand AddMorseCommand
        {
            get
            {
                if (addMorseCommand == null)
                {
                    addMorseCommand = new RelayCommand(param => AddMorse());
                }
                return addMorseCommand;
            }
        }
        public void AddMorse()
        {
            TroveVM.AddMorse();
        }

        private ICommand? updateMorseCommand;
        public ICommand UpdateMorseCommand
        {
            get
            {
                if (updateMorseCommand == null)
                {
                    updateMorseCommand = new RelayCommand(param => UpdateMorse());
                }
                return updateMorseCommand;
            }
        }

        public void UpdateMorse()
        {
            TroveVM.UpdateMorse();
        }

        private ICommand? clearMorseCommand;
        public ICommand ClearMorseCommand
        {
            get
            {
                if (clearMorseCommand == null)
                {
                    clearMorseCommand = new RelayCommand(param => ClearMorse());
                }
                return clearMorseCommand;
            }
        }

        public void ClearMorse()
        {
            TroveVM.ClearMorseFields();
        }

        #endregion

        #region Polybius

        private ICommand? addPolybiusCommand;
        public ICommand AddPolybiusCommand
        {
            get
            {
                if (addPolybiusCommand == null)
                {
                    addPolybiusCommand = new RelayCommand(param => AddPolybius());
                }
                return addPolybiusCommand;
            }
        }
        public void AddPolybius()
        {
            TroveVM.AddPolybius();
           
        }
        private ICommand? updatePolybiusCommand;

        public ICommand UpdatePolybiusCommand
        {
            get
            {
                if (updatePolybiusCommand == null)
                {
                    updatePolybiusCommand = new RelayCommand(param => UpdatePolybius());
                }
                return updatePolybiusCommand;
            }
        }

        public void UpdatePolybius()
        {
            TroveVM.UpdatePolybius();
        }

        private ICommand? clearPolybiusCommand;
        public ICommand ClearPolybiusCommand
        {
            get
            {
                if (clearPolybiusCommand == null)
                {
                    clearPolybiusCommand = new RelayCommand(param => ClearPolybius());
                }
                return clearPolybiusCommand;
            }
        }

        public void ClearPolybius()
        {
            TroveVM.ClearPolybiusFields();
        }

        #endregion

        #region "IO Methods"

        #region Properties

        private ICommand? setSaveCommand;
        public ICommand? SetSaveCommand
        {
            get
            {
                return setSaveCommand;
            }
            set
            {
                setSaveCommand = value;
            }
        }

        private ICommand? setFileCommand;
        public ICommand? SetFileCommand
        {
            get
            {
                return setFileCommand;
            }
            set
            {
                setFileCommand = value;
            }
        }

        private ICommand? setFolderCommand;
        public ICommand? SetFolderCommand
        {
            get
            {
                return setFolderCommand;
            }
            set
            {
                setFolderCommand = value;
            }
        }

        private ICommand openWindowsExplorerCommand;
        public ICommand OpenWindowsExplorerCommand
        {
            get
            {
                return openWindowsExplorerCommand;
            }
            set
            {
                openWindowsExplorerCommand = value;
            }
        }

        #endregion

        public void SetFile(object obj)
        {
            bool exit = false;
            Microsoft.Win32.OpenFileDialog openFD = new Microsoft.Win32.OpenFileDialog();

            switch (obj.ToString())
            {
                case "IOGC.XMLDataLocation":
                    //openFD.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
                    //if (System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(IOGC.XMLDataLocation)))
                    //{
                    //    openFD.InitialDirectory = System.IO.Path.GetDirectoryName(IOGC.XMLDataLocation);
                    //}
                    break;
                default:
                    StatusMessage = "Save Incomplete.";
                    exit = true;
                    break;
            }
            if (!exit)
            {
                if (openFD.ShowDialog() == true)
                {
                    string pathName = openFD.FileName;
                    switch (obj.ToString())
                    {
                        case "IOGC.XMLDataLocation":
                            //IOGC.XMLDataLocation = pathName;
                            break;
                        default:
                            StatusMessage = "Save Incomplete.";
                            break;
                    }
                }
            }
        }

        public void SetFolder(object obj)
        {
            bool exit = false;
            System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            switch (obj.ToString())
            {
                case "GlobalLogLocation":
                    if (GlobalLogLocation != string.Empty)
                    {
                        folderBrowserDialog.SelectedPath = GlobalLogLocation;
                    }
                    break;
                default:
                    StatusMessage = "Save Incomplete.";
                    exit = true;
                    break;
            }
            if (!exit)
            {
                if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string pathName = folderBrowserDialog.SelectedPath;
                    switch (obj.ToString())
                    {
                        case "GlobalLogLocation":
                            GlobalLogLocation = pathName;
                            break;
                        default:
                            StatusMessage = "Save Incomplete.";
                            break;
                    }
                }
            }
        }

        public void OpenWindowsExplorer(object obj)
        {
            switch (obj.ToString())
            {
                case "GlobalLogLocation":
                    OpenWE(GlobalLogLocation);
                    break;
                default:
                    StatusMessage = "Open Unsuccessful.";
                    break;
            }

        }

        private void OpenWE(string location)
        {
            if (location == string.Empty)
            {
                StatusMessage = "Location does not exists.";
            }
            else
            {
                FileAttributes attr = System.IO.File.GetAttributes(location);

                if (!attr.HasFlag(FileAttributes.Directory))
                {
                    location = Path.GetDirectoryName(location);
                }

                if (System.IO.Directory.Exists(location))
                {
                    System.Diagnostics.Process.Start("explorer.exe", location);
                }
                else
                {
                    StatusMessage = location + " does not exists.";
                }
            }
        }

        private bool CheckExistance(string path)
        {
            bool okay2Go = false;
            FileAttributes attr = System.IO.File.GetAttributes(path);

            if (attr.HasFlag(FileAttributes.Directory))
            {
                if (Directory.Exists(path))
                {
                    okay2Go = true;
                }
            }
            else //File
            {
                if (System.IO.File.Exists(path))
                {
                    string directoryName = Path.GetDirectoryName(path);
                    if (directoryName != null)
                    {
                        if (directoryName != string.Empty)
                        {
                            okay2Go = true;
                        }
                    }
                }
            }

            return okay2Go;
        }

        public void SetSave(object obj)
        {
            bool exit = false;
            Microsoft.Win32.SaveFileDialog saveFD = new Microsoft.Win32.SaveFileDialog();
            saveFD.CheckPathExists = true;

            switch (obj.ToString())
            {
                case "ExportTemplatesLocation":
                    //saveFD.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
                    //saveFD.FileName = "ConnectionTemplates_" + DateTime.Now.ToString("yyyy.MM.dd.HH.mm.ss.ffff") + ".json";
                    //if (CheckExistance(GCDocsDB.ExportTemplatesLocation))
                    //{
                    //    saveFD.InitialDirectory = System.IO.Path.GetDirectoryName(GCDocsDB.ExportTemplatesLocation);
                    //}
                    break;
                default:
                    StatusMessage = "Save Incomplete.";
                    exit = true;
                    break;
            }
            if (!exit)
            {
                if (saveFD.ShowDialog() == true)
                {
                    switch (obj.ToString())
                    {
                        case "ExportTemplatesLocation":
                            //if (saveFD.FileName != null)
                            //{
                            //    GCDocsDB.ExportTemplatesLocation = System.IO.Path.GetDirectoryName(saveFD.FileName);
                            //    GCDocsDB.ExportTemplatesFileName = System.IO.Path.GetFileName(saveFD.FileName);
                            //    GCDocsDB.ExportGCDocsConnectionTemplate();
                            //}
                            //else { StatusMessage = "Save Incomplete.";  }
                            break;
                        default:
                            StatusMessage = "Save Incomplete.";
                            break;
                    }
                }


            }
        }


        #endregion
    }
}
