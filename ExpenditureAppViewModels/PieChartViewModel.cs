using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneralUseClasses.Contracts;
using GeneralUseClasses.Services;

namespace ExpenditureAppViewModels
{
    public class PieChartViewModel : ViewModel
    {
        Dictionary<string, double> totalData = new Dictionary<string, double>();
        Dictionary<string, double> miscData = new Dictionary<string, double>();
        IProvideExpenditureData dataProvider;
        double sum = 0;
        double miscellaneousSum = 0;

        public PieChartViewModel(IProvideExpenditureDataProvider provider)
        {
            dataProvider = provider.GetExpenditureDataProvider();
            PopulatePieData();
            totalData = totalData.OrderByDescending((s) => s.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        public Dictionary<string, double> TotalPieData
        {
            get
            {
                return totalData;
            }
            set
            {
                if (totalData != value)
                {
                    totalData = value;
                    RaisePropertyChanged("TotalPieData");
                }
            }
        }

        public Dictionary<string, double> MiscPieData
        {
            get
            {
                return miscData;
            }
            set
            {
                if (miscData != value)
                {
                    miscData = value;
                    RaisePropertyChanged("MiscPieData");
                }
            }
        }

        public string TotalSum
        {
            get
            {
                return "Total Expenditure was £" + string.Format("{0:0.00}", sum);
            }
        }

        public string MiscSum
        {
            get
            {
                return "Miscellaneous Expenditure was £" + string.Format("{0:0.00}", miscellaneousSum);
            }
        }

        private void PopulatePieData()
        {
            var entries = dataProvider.GetAllEntries();
            foreach (var entry in entries)
            {
                sum += entry.expenditure;
            }

            Dictionary<string, double> intialTotalData = new Dictionary<string, double>();
            foreach (var entry in entries)
            {
                if (!intialTotalData.ContainsKey(entry.dominantTag))
                {
                    intialTotalData.Add(entry.dominantTag, entry.expenditure);
                }
                else
                {
                    intialTotalData[entry.dominantTag] += entry.expenditure;
                }
            }

            foreach (var expenditure in intialTotalData)
            {
                totalData.Add(expenditure.Key + " - " + expenditure.Value.ToString(), expenditure.Value);

                if (expenditure.Value < sum / 20)
                {
                    miscellaneousSum += expenditure.Value;
                    if (!miscData.ContainsKey(expenditure.Key + " - " + expenditure.Value.ToString()))
                    {
                        miscData.Add(expenditure.Key + " - " + expenditure.Value.ToString(), expenditure.Value);
                    }
                    else
                    {
                        miscData[expenditure.Key + " - " + expenditure.Value.ToString()] += expenditure.Value;
                    }
                }
            }

            totalData.Add("Miscellaneous", miscellaneousSum);
        }
    }
}
