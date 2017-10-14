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
        Dictionary<string, double> totalPieData = new Dictionary<string, double>();
        Dictionary<string, double> miscPieData = new Dictionary<string, double>();
        IProvideExpenditureData dataProvider;
        double sum = 0;
        double miscellaneousSum = 0;

        public PieChartViewModel(IProvideExpenditureDataProvider provider)
        {
            dataProvider = provider.GetExpenditureDataProvider();
            PopulatePieData();
            totalPieData = totalPieData.OrderByDescending((s) => s.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        public Dictionary<string, double> TotalPieData
        {
            get
            {
                return totalPieData;
            }
            set
            {
                if (totalPieData != value)
                {
                    totalPieData = value;
                    RaisePropertyChanged("TotalPieData");
                }
            }
        }

        public Dictionary<string, double> MiscPieData
        {
            get
            {
                return miscPieData;
            }
            set
            {
                if (miscPieData != value)
                {
                    miscPieData = value;
                    RaisePropertyChanged("MiscPieData");
                }
            }
        }

        private void PopulatePieData()
        {
            var entries = dataProvider.GetAllEntries();
            foreach (var entry in entries)
            {
                sum += entry.expenditure;
            }

            foreach (var entry in entries)
            {
                if (!totalPieData.ContainsKey(entry.dominantTag))
                {
                    totalPieData.Add(entry.dominantTag, entry.expenditure);
                }
                else
                {
                    totalPieData[entry.dominantTag] += entry.expenditure;
                }
            }

            foreach (var expenditure in totalPieData)
            {
                if (expenditure.Value < sum / 20)
                {
                    miscellaneousSum += expenditure.Value;
                    if (!miscPieData.ContainsKey(expenditure.Key))
                    {
                        miscPieData.Add(expenditure.Key, expenditure.Value);
                    }
                    else
                    {
                        miscPieData[expenditure.Key] += expenditure.Value;
                    }
                }
            }

            totalPieData.Add("Miscellaneous", miscellaneousSum);
        }
    }
}
