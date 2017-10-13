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
using ExpenditureAppViewModel;
using System;
using GeneralUseClasses.EventArgs;

namespace ExpenditureAppWPF
{
    /// <summary>
    /// Interaction logic for InputUserControl.xaml
    /// </summary>
    public partial class InputUserControl : UserControl
    {
        internal InputUserControlViewModel viewModel;
        public InputUserControl()
        {
            InitializeComponent();

            AssociatedTagsListView.SelectionChanged += (s, e) => OnAssociatedTagsListViewSelectionChanged(s, e);
            PeopleListView.SelectionChanged += (s, e) => OnPeopleListViewSelectionChanged(s, e);

            AddNewDominantTagBtn.Click += (s, e) => OnAddNewDominantTagBtnClick();
            AddNewAssociatedTagBtn.Click += (s, e) => OnAddNewAssociatedTagBtnClick();
            AddNewPersonBtn.Click += (s, e) => OnAddNewPersonBtnClick();

            InputDayTextBox.KeyUp += (s, e) => OnInputDateTextBoxKeyUp(s);
            InputMonthTextBox.KeyUp += (s, e) => OnInputDateTextBoxKeyUp(s);
            InputYearTextBox.KeyUp += (s, e) => OnInputDateTextBoxKeyUp(s);
            InputExpenditureTextBox.KeyUp += (s, e) => OnInputExpenditureTextBoxKeyUp(e);
        }

        internal void SetViewModel(InputUserControlViewModel viewModel)
        {
            this.viewModel = viewModel;
            this.viewModel.exceptionEventHandler += (s, e) => OnViewModelException(e.exception);
            DataContext = this.viewModel;
        }

        private void OnAssociatedTagsListViewSelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            foreach (var item in args.AddedItems)
            {
                if (item.GetType() == typeof(string))
                {
                    if (!viewModel.AssociatedTagsToRemove.Contains((string)item))
                    {
                        // Convert to ObservableCollection so can remove this code?
                        viewModel.AssociatedTagsToRemove.Add((string)item);
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
                    if (!viewModel.PeopleToRemove.Contains((string)item))
                    {
                        // Convert to ObservableCollection so can remove this code?
                        viewModel.PeopleToRemove.Add((string)item);
                    }
                }
            }
        }

        private void OnAddNewDominantTagBtnClick()
        {
            string title = "Input New Dominant Tag";
            string tagType = "dominant tag";
            ICommand addCommand = viewModel.AddNewDominantTagCommand;
            OpenNewTextPopup(title, tagType, addCommand);
        }

        private void OnAddNewAssociatedTagBtnClick()
        {
            string title = "Input New Associated Tag";
            string tagType = "associated tag";
            ICommand addCommand = viewModel.AddNewAssociatedTagCommand;
            OpenNewTextPopup(title, tagType, addCommand);
        }

        private void OnAddNewPersonBtnClick()
        {
            string title = "Input New Person";
            string tagType = "person";
            ICommand addCommand = viewModel.AddNewPersonCommand;
            OpenNewTextPopup(title, tagType, addCommand);
        }

        private void OpenNewTextPopup(string title, string instruction, ICommand addCommand)
        {
            PopupTextInput popup = new PopupTextInput(viewModel, title, instruction, addCommand);
            popup.ShowDialog();
        }

        private void OnInputExpenditureTextBoxKeyUp(KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var peer = UIElementAutomationPeer.CreatePeerForElement(InputExpenditureBtn);
                IInvokeProvider invoker = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                invoker.Invoke();
            }
        }

        private void OnInputDateTextBoxKeyUp(object sender)
        {
            TextBox dateTextbox = (TextBox)sender;
            if (!(dateTextbox.Text.Length < 2))
            {
                if (dateTextbox == InputDayTextBox)
                {
                    FocusManager.SetFocusedElement(this, InputMonthTextBox);
                }
                if (dateTextbox == InputMonthTextBox)
                {
                    FocusManager.SetFocusedElement(this, InputYearTextBox);
                }
                if (dateTextbox == InputYearTextBox)
                {
                    FocusManager.SetFocusedElement(this, InputExpenditureTextBox);
                }
            }
        }

        private void OnViewModelException(Exception e)
        {
            MessageBox.Show(e.Message, "Warning!");
        }
    }
}
