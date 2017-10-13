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
using ExpenditureAppViewModels;

namespace ExpenditureAppWPF
{
    /// <summary>
    /// Interaction logic for OutputUserControlPie.xaml
    /// </summary>
    public partial class OutputUserControlPie : UserControl
    {
        ExpenditureAppOutputPieViewModel viewModel;
        public OutputUserControlPie()
        {
            InitializeComponent();
            viewModel = new ExpenditureAppOutputPieViewModel();
            DataContext = viewModel;
        }
    }
}
