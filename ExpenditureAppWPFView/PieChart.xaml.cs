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
using System.Windows.Controls.DataVisualization;
using ExpenditureAppViewModels;
using GeneralUseClasses.Contracts;

namespace ExpenditureAppWPF
{
    /// <summary>
    /// Interaction logic for PieChart.xaml
    /// </summary>
    public partial class PieChart : Window
    {
        public PieChart(IProvideExpenditureDataProvider provider)
        {
            InitializeComponent();
            DataContext = new PieChartViewModel(provider);
        }
    }
}
