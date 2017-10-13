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
using GeneralUseClasses;
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
        private ExpenditureAppViewModels.ExpenditureAppInputViewModel inputViewModel;
        public ExpenditureApp()
        {
            InitializeComponent();

            Action<string, string> messageForUser = ((message, caption) => System.Windows.MessageBox.Show(message, caption));
            Func<string, string, bool> decisionForUser = (message, caption) => System.Windows.MessageBox.Show(message, caption, MessageBoxButton.YesNo) == MessageBoxResult.Yes;
            Func<string> selectFileLocation = () => SelectFileLocation();
            var dataRecorderFactory = new ExpenditureDataRecorderFactory(selectFileLocation, messageForUser);
            var dataProviderFactory = new ExpenditureDataProviderFactory(selectFileLocation, messageForUser);
            inputViewModel = new ExpenditureAppViewModels.ExpenditureAppInputViewModel(messageForUser, decisionForUser, dataRecorderFactory, dataProviderFactory);
            inputViewModel = new ExpenditureAppViewModels.ExpenditureAppInputViewModel(messageForUser, decisionForUser, dataRecorderFactory, dataProviderFactory);
            inputUserControl.DataContext = inputViewModel;
            //DataContext = viewModel;

            SetupInputUserControlDelegates();

            outputModeCombo.MouseEnter += (s, e) => OnOutputModeChanged();

            inputViewModel.exceptionEventHandler += (s, e) => OnViewModelException(e.exception);
        }

        private void OnOutputModeChanged()
        {
            outputControl.Content = null;
            outputControl.Content = new OutputUserControlPie();
        }

        private void SetupInputUserControlDelegates()
        {
            inputUserControl.AssociatedTagsListView.SelectionChanged += (s, e) => OnAssociatedTagsListViewSelectionChanged(s, e);
            inputUserControl.PeopleListView.SelectionChanged += (s, e) => OnPeopleListViewSelectionChanged(s, e);

            inputUserControl.AddNewDominantTagBtn.Click += (s, e) => OnAddNewDominantTagBtnClick();
            inputUserControl.AddNewAssociatedTagBtn.Click += (s, e) => OnAddNewAssociatedTagBtnClick();
            inputUserControl.AddNewPersonBtn.Click += (s, e) => OnAddNewPersonBtnClick();

            inputUserControl.InputDayTextBox.KeyUp += (s, e) => OnInputDateTextBoxKeyUp(s);
            inputUserControl.InputMonthTextBox.KeyUp += (s, e) => OnInputDateTextBoxKeyUp(s);
            inputUserControl.InputYearTextBox.KeyUp += (s, e) => OnInputDateTextBoxKeyUp(s);
            inputUserControl.InputExpenditureTextBox.KeyUp += (s, e) => OnInputExpenditureTextBoxKeyUp(e);
        }

        private void OnAssociatedTagsListViewSelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            foreach (var item in args.AddedItems)
            {
                if (item.GetType() == typeof(string))
                {
                    if (!inputViewModel.AssociatedTagsToRemove.Contains((string)item))
                    {
                        // Convert to ObservableCollection so can remove this code?
                        inputViewModel.AssociatedTagsToRemove.Add((string)item);
                    }
                }
            }
        }

        private void OnPeopleListViewSelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            foreach (var item in args.AddedItems)
            {
                if (item.GetType() == typeof(string))
                {
                    if (!inputViewModel.PeopleToRemove.Contains((string)item))
                    {
                        // Convert to ObservableCollection so can remove this code?
                        inputViewModel.PeopleToRemove.Add((string)item);
                    }
                }
            }
        }

        private void OnAddNewDominantTagBtnClick()
        {
            string title = "Input New Dominant Tag";
            string tagType = "dominant tag";
            ICommand addCommand = inputViewModel.AddNewDominantTagCommand;
            OpenNewTextPopup(title, tagType, addCommand);
        }

        private void OnAddNewAssociatedTagBtnClick()
        {
            string title = "Input New Associated Tag";
            string tagType = "associated tag";
            ICommand addCommand = inputViewModel.AddNewAssociatedTagCommand;
            OpenNewTextPopup(title, tagType, addCommand);
        }

        private void OnAddNewPersonBtnClick()
        {
            string title = "Input New Person";
            string tagType = "person";
            ICommand addCommand = inputViewModel.AddNewPersonCommand;
            OpenNewTextPopup(title, tagType, addCommand);
        }

        private void OpenNewTextPopup(string title, string instruction, ICommand addCommand)
        {
            PopupTextInput popup = new PopupTextInput(inputViewModel, title, instruction, addCommand);
            popup.ShowDialog();
        }

        private void OnInputExpenditureTextBoxKeyUp(KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var peer = UIElementAutomationPeer.CreatePeerForElement(inputUserControl.InputExpenditureBtn);
                IInvokeProvider invoker = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                invoker.Invoke();
            }
        }

        private void OnInputDateTextBoxKeyUp(object sender)
        {
            TextBox dateTextbox = (TextBox)sender;
            if (!(dateTextbox.Text.Length < 2))
            {
                if (dateTextbox == inputUserControl.InputDayTextBox)
                {
                    FocusManager.SetFocusedElement(this, inputUserControl.InputMonthTextBox);
                }
                if (dateTextbox == inputUserControl.InputMonthTextBox)
                {
                    FocusManager.SetFocusedElement(this, inputUserControl.InputYearTextBox);
                }
                if (dateTextbox == inputUserControl.InputYearTextBox)
                {
                    FocusManager.SetFocusedElement(this, inputUserControl.InputExpenditureTextBox);
                }
            }
        }

        private void OnViewModelException(Exception e)
        {
            MessageBox.Show(e.Message, "Warning!");
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
                return dialog.FileName;
            }
            else
            {
                DialogFail(failureIsTerminal);
                return DataStorage.Default.xmlFileDirectory;
            }
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
                MessageBox.Show("You have not selected a path! The path will not be changed.", "Warning!");
            }
        }

        private void OnDataFilePathOptionBtnClick(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("You are currently storing data in " + DataStorage.Default.xmlFileDirectory + ". Would you like to change this?", "Data Directory", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                DataStorage.Default.xmlFileDirectory = OpenFolderSelecterDialog(false);
            }
        }
    }
}
