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
using System.Windows.Shapes;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using ExpenditureAppWPF;

namespace ExpenditureAppWPF.Dialogs
{
    /// <summary>
    /// Interaction logic for PopupTextInput.xaml
    /// </summary>
    public partial class PopupTextInput : Window
    {
        string instruction;
        ExpenditureAppViewModel.ExpenditureAppInputViewModel viewModel;
        public PopupTextInput(ExpenditureAppViewModel.ExpenditureAppInputViewModel viewModel, string title, string tagType, ICommand addCommand)
        {
            InitializeComponent();
            this.viewModel = viewModel;
            DataContext = viewModel;
            Title = title;
            instruction = "Please write your new " + tagType + " and click add or press enter";
            label.Content = instruction;         
            AddBtn.Command = addCommand;

            CancelBtn.Click += (s, e) => Close();
            AddBtn.Click += (s, e) => CheckIfClose();
            TextBox.KeyUp += (s, e) => CheckIfEnter(e);
        }

        private void CheckIfEnter(KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var peer = UIElementAutomationPeer.CreatePeerForElement(AddBtn);
                IInvokeProvider invoker = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                invoker.Invoke();
            }
        }

        private void CheckIfClose()
        {
            if (TextBox.Text.Length > 0)
            {
                Close();
            }
            else
            {
                MessageBox.Show(instruction, Title);
            }
        }
    }
}
