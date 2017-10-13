using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Input;
using System.Collections.ObjectModel;
using GeneralUseClasses.Services;
using GeneralUseClasses.Contracts;
using GeneralUseClasses.EventArgs;
using GeneralUseClasses.Exceptions;
using GeneralUseClasses;

namespace ExpenditureAppViewModels
{
    public class ExpenditureAppOutputPieViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<string> dateOptions = new ObservableCollection<string>() { "Current Week", "Current Month", "Current Year" };
        private string dateOption;
        private string startDay;
        private string startMonth;
        private string startYear;
        private string endDay;
        private string endMonth;
        private string endYear;

        public ObservableCollection<string> DateOptions
        {
            get
            {
                return dateOptions;
            }
        }

        public string DateOption
        {
            get
            {
                return dateOption;
            }
            set
            {
                if (dateOption != value)
                {
                    dateOption = value;
                    UpdateDates(value);
                    RaisePropertyChanged("DateOption");
                }
            }
        }

        public string StartDay
        {
            get
            {
                return startDay;
            }
            set
            {
                if (startDay != value)
                {
                    startDay = value;
                    RaisePropertyChanged("StartDay");
                }
            }
        }

        public string StartMonth
        {
            get
            {
                return startMonth;
            }
            set
            {
                if (startMonth != value)
                {
                    startMonth = value;
                    RaisePropertyChanged("StartMonth");
                }
            }
        }

        public string StartYear
        {
            get
            {
                return startYear;
            }
            set
            {
                if (startYear != value)
                {
                    startYear = value;
                    RaisePropertyChanged("StartYear");
                }
            }
        }

        public string EndDay
        {
            get
            {
                return endDay;
            }
            set
            {
                if (endDay != value)
                {
                    endDay = value;
                    RaisePropertyChanged("EndDay");
                }
            }
        }

        public string EndMonth
        {
            get
            {
                return endMonth;
            }
            set
            {
                if (endMonth != value)
                {
                    endMonth = value;
                    RaisePropertyChanged("EndMonth");
                }
            }
        }

        public string EndYear
        {
            get
            {
                return endYear;
            }
            set
            {
                if (endYear != value)
                {
                    endYear = value;
                    RaisePropertyChanged("EndYear");
                }
            }
        }

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void UpdateDates(string mode)
        {
            Dictionary<int, int[]> valid = SetupValidMonthsAndDays();
            switch (mode)
            {
                case "Current Week":
                    int day = DateTime.Now.Day - (int)DateTime.Now.DayOfWeek;
                    if (day <= 0)
                    {
                        int[] daysInPreviousMonth = valid[DateTime.Now.Month - 1];
                        int lastDay = daysInPreviousMonth[daysInPreviousMonth.Length - 1];
                        day = lastDay + (day + 1);
                        StartDay = day.ToString("D2");
                        StartMonth = (DateTime.Now.Month - 1).ToString("D2");
                        StartYear = (DateTime.Now.Year).ToString();
                    }
                    else
                    {
                        StartDay = day.ToString("D2");
                        StartMonth = DateTime.Now.Month.ToString("D2");
                        StartYear = DateTime.Now.Year.ToString();
                    }

                    day = DateTime.Now.Day + (7 - (int)DateTime.Now.DayOfWeek);
                    if (!(valid[DateTime.Now.Month].Contains(day)))
                    {

                        int[] daysInMonth = valid[DateTime.Now.Month];
                        int lastDay = daysInMonth[daysInMonth.Length - 1];
                        day = (7 - (int)DateTime.Now.DayOfWeek) - (lastDay - DateTime.Now.Day);
                        EndDay = day.ToString("D2");
                        EndMonth = (DateTime.Now.Month + 1).ToString("D2");
                        EndYear = DateTime.Now.Year.ToString();
                    }
                    else
                    {
                        EndDay = day.ToString("D2");
                        EndMonth = DateTime.Now.Month.ToString("D2");
                        EndYear = DateTime.Now.Year.ToString();
                    }
                    break;
                case "Current Month":
                    StartDay = valid[DateTime.Now.Month].Last().ToString("D2");
                    StartMonth = DateTime.Now.Month.ToString("D2");
                    StartYear = DateTime.Now.Year.ToString();

                    EndDay = valid[DateTime.Now.Month].First().ToString("D2");
                    EndMonth = DateTime.Now.Month.ToString("D2");
                    EndYear = DateTime.Now.Year.ToString();
                    break;
                case "Current Year":
                    StartDay = 1.ToString("D2");
                    StartMonth = 1.ToString("D2");
                    StartYear = DateTime.Now.Year.ToString();

                    EndDay = 31.ToString("D2");
                    EndMonth = 12.ToString("D2");
                    EndYear = DateTime.Now.Year.ToString();
                    break;
                default:
                    break;
            }
        }

        private Dictionary<int, int[]> SetupValidMonthsAndDays()
        {
            Dictionary<int, int[]> validMonthsAndDays = new Dictionary<int, int[]>();
            for (int i = 1; i <= 12; i++)
            {
                if (i == 9 || i == 4 || i == 6 || i == 11)
                {
                    int[] validDays = new int[30];
                    for (int j = 0; j < 30; j++)
                    {
                        validDays[j] = j + 1;
                    }
                    validMonthsAndDays.Add(i, validDays);
                }
                else if (i == 2)
                {
                    int[] validDays = new int[29];
                    for (int j = 0; j < 29; j++)
                    {
                        validDays[j] = j + 1;
                    }
                    validMonthsAndDays.Add(i, validDays);
                }
                else
                {
                    int[] validDays = new int[31];
                    for (int j = 0; j < 31; j++)
                    {
                        validDays[j] = j + 1;
                    }
                    validMonthsAndDays.Add(i, validDays);
                }
            }
            return validMonthsAndDays;
        }
    }
}
