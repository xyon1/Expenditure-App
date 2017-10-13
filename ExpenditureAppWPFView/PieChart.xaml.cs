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
//using System.Windows.Forms.DataVisualization.Charting;

namespace ExpenditureAppWPF
{
    /// <summary>
    /// Interaction logic for PieChart.xaml
    /// </summary>
    public partial class PieChart : Window
    {
        public PieChart()
        {
            InitializeComponent();
            //SetupChart()
        }

        //private void SetupChart(Chart chart)
        //{
        //    Series expenditureSeries = new Series("Expenditure");

        //    expenditureSeries.ChartType = SeriesChartType.Pie;
        //    expenditureSeries.ChartArea = "pieChart";
        //    expenditureSeries.Legend = "Legend1";
        //    chart.Series.Add(expenditureSeries);
        //    chart.Series["Expenditure"].Label = "#VALX #VALY";
        //    chart.Series["Expenditure"].SetCustomProperty("PieStartAngle", "270");
        //}
    }
}
