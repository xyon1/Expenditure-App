using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using GeneralUseClasses.Services;
using ServiceProvider;
using Microsoft.WindowsAPICodePack.Dialogs;
using ExpenditureAppWPF.Dialogs;

namespace ExpenditureAppWPF
{
    /// <summary>
    /// Interaction logic for ExpenditureApp.xaml
    /// </summary>
    public partial class ExpenditureApp : Window
    {
        private ExpenditureAppViewModel.InputUserControlViewModel viewModel;
        public ExpenditureApp()
        {
            InitializeComponent();

            Action<string, string> messageForUser = ((message, caption) => System.Windows.MessageBox.Show(message, caption));
            Func<string, string, bool> decisionForUser = (message, caption) => System.Windows.MessageBox.Show(message, caption, MessageBoxButton.YesNo) == MessageBoxResult.Yes;
            Func<string> selectFileLocation = () => SelectFileLocation();
            var dataRecorderFactory = new ExpenditureDataRecorderFactory(selectFileLocation, messageForUser);
            var dataProviderFactory = new ExpenditureDataProviderFactory(selectFileLocation, messageForUser);
            viewModel = new ExpenditureAppViewModel.InputUserControlViewModel(messageForUser, decisionForUser, dataRecorderFactory, dataProviderFactory);
            inputUserControl.SetViewModel(viewModel);

            //viewModel = new ExpenditureAppViewModel.InputPageViewModel(messageForUser, decisionForUser, dataRecorderFactory, dataProviderFactory);
            //DataContext = viewModel;
        }

        private string SelectFileLocation()
        {
            FolderSelectionDecisionDialog fsdDialog = new FolderSelectionDecisionDialog();
            var decision = fsdDialog.Show();
            string result = "";
            if (decision == FolderSelectionDecisionDialogResult.ChooseFolder)
            {
                result= OpenFolderSelecterDialog(true);
            }
            else if (decision == FolderSelectionDecisionDialogResult.UseDefaultLocation)
            {
                result = System.AppDomain.CurrentDomain.BaseDirectory;
            }
            else
            {
                Environment.Exit(0);
            }

            return result;
        }

        private string OpenFolderSelecterDialog(bool failureIsTerminal)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog("Please select a folder to store your expenditure in");
            dialog.InitialDirectory = "C:";
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                if (string.IsNullOrEmpty(dialog.FileName))
                {
                    DialogFail(failureIsTerminal);
                }
            }
            else
            {
                DialogFail(failureIsTerminal);
            }

            return dialog.FileName;
        }

        private void DialogFail(bool failureIsTerminal)
        {
            if (failureIsTerminal)
            {
                MessageBox.Show("You have not selected a path! Application will now close.", "Warning!");
                Environment.Exit(0);
            }
            else
            {
                MessageBox.Show("You have not selected a path!", "Warning!");
            }
        }
    }
}
