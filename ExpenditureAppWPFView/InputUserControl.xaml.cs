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
using ExpenditureAppViewModels;
using System;
using GeneralUseClasses.EventArgs;

namespace ExpenditureAppWPF
{
    /// <summary>
    /// Interaction logic for InputUserControl.xaml
    /// </summary>
    public partial class InputUserControl : UserControl
    {
        internal ExpenditureAppViewModels.ExpenditureAppInputViewModel viewModel;
        public InputUserControl()
        {
            InitializeComponent();
        }
    }
}
