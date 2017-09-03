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

namespace ExpenditureAppWPF
{
    /// <summary>
    /// Interaction logic for ExpenditureApp.xaml
    /// </summary>
    public partial class ExpenditureApp : Window
    {
        private ExpenditureAppViewModel.ViewModel viewModel;
        public ExpenditureApp()
        {
            InitializeComponent();
            Action<string, string> messageForUser = ((message, caption) => System.Windows.MessageBox.Show(message, caption));
            Func<string, string, bool> decisionForUser = (message, caption) => System.Windows.MessageBox.Show(message, caption, MessageBoxButton.YesNo) == MessageBoxResult.Yes;
            viewModel = new ExpenditureAppViewModel.ViewModel(messageForUser, decisionForUser);
            DataContext = viewModel;

            AssociatedTagsListView.SelectionChanged += (s,e) => OnAssociatedTagsListViewSelectionChanged(s,e);
            PeopleListView.SelectionChanged += (s, e) => OnPeopleListViewSelectionChanged(s, e);

            AddNewDominantTagBtn.Click += (s, e) => OnAddNewDominantTagBtnClick();
            AddNewAssociatedTagBtn.Click += (s, e) => OnAddNewAssociatedTagBtnClick();
            AddNewPersonBtn.Click += (s, e) => OnAddNewPersonBtnClick();
        }

        private void OnAssociatedTagsListViewSelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            foreach (var item in args.AddedItems)
            {
                if (item.GetType() == typeof(string))
                {
                    if (!viewModel.SelectedAssociatedTagsToRemove.Contains((string)item))
                    {
                        viewModel.SelectedAssociatedTagsToRemove.Add((string)item);
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
                    if (!viewModel.SelectedPeopleToRemove.Contains((string)item))
                    {
                        viewModel.SelectedPeopleToRemove.Add((string)item);
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
    }
}
