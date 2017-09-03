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
            this.DataContext = viewModel;

            this.AssociatedTagsListView.SelectionChanged += (s,e) => OnAssociatedTagsListViewSelectionChanged(s,e);
            this.PeopleListView.SelectionChanged += (s, e) => OnPeopleListViewSelectionChanged(s, e);
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
    }
}
