using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace ExpenditureAppViewModel
{
    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string inputDay;
        private string inputMonth;
        private string inputYear;
        private string selectedDominantTag;
        private string selectedAssociatedTag;
        private string selectedPerson;
        private List<string> allDominantTags = new List<string>() { "hello", "my", "name" };
        private List<string> allAssociatedTags = new List<string>() { "hello", "what", "are" };
        private List<string> allPeople = new List<string>() { "Benedict", "Beth", "Paul" };


        public string InputDay
        {
            get
            {
                return inputDay;
            }
            set
            {
                if (inputDay != value)
                {
                    inputDay = value;
                    RaisePropertyChanged("InputDay");
                }
            }
        }

        public string InputMonth
        {
            get
            {
                return inputMonth;
            }
            set
            {
                if (inputMonth != value)
                {
                    inputMonth = value;
                    RaisePropertyChanged("InputMonth");
                }
            }
        }

        public string InputYear
        {
            get
            {
                return inputYear;
            }
            set
            {
                if (inputYear != value)
                {
                    inputYear = value;
                    RaisePropertyChanged("InputYear");
                }
            }
        }

        public string SelectedDominantTag
        {
            get
            {
                return selectedDominantTag;
            }
            set
            {
                if (selectedDominantTag != value)
                {
                    selectedDominantTag = value;
                    RaisePropertyChanged("SelectedDominantTag");
                }
            }
        }

        public List<string> AllDominantTags
        {
            get
            {
                return allDominantTags;
            }
        }

        public string SelectedAssociatedTag
        {
            get
            {
                return selectedAssociatedTag;
            }
            set
            {
                if (selectedAssociatedTag != value)
                {
                    selectedAssociatedTag = value;
                    RaisePropertyChanged("SelectedAssociatedTag");
                }
            }
        }

        public List<string> AllAssociatedTags
        {
            get
            {
                return allAssociatedTags;
            }
        }

        public string SelectedPerson
        {
            get
            {
                return selectedPerson;
            }
            set
            {
                if (selectedPerson != value)
                {
                    selectedPerson = value;
                    RaisePropertyChanged("SelectedPerson");
                }
            }
        }

        public List<string> AllPeople
        {
            get
            {
                return allPeople;
            }
        }

        public ViewModel()
        {
            selectedDominantTag = allDominantTags[0];
            selectedAssociatedTag = allAssociatedTags[0];
            selectedPerson = allPeople[0];
        }

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public ICommand InputButtonClickedCommand
        {
            get { return new RelayCommand(new Action(this.OnInputButtonClicked)); }
        }

        private void OnInputButtonClicked()
        {
            InputDay = null;
            InputMonth = null;
            InputYear = null;
        }
    }
}
