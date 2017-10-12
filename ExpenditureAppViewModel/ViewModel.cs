using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Input;
using System.Collections.ObjectModel;
using GeneralUseClasses.Services;
using GeneralUseClasses.Contracts;
using ExpenditureAppViewModel.EventArgs;
using GeneralUseClasses.Exceptions;
using GeneralUseClasses;

namespace ExpenditureAppViewModel
{
    public class ViewModel : INotifyPropertyChanged
    {
        IRecordExpenditureData recorder;
        IProvideExpenditureData dataProvider;
        public event PropertyChangedEventHandler PropertyChanged;
        public Action<string, string> messageForUser;
        public Func<string, string, bool> decisionForUser;
        public Func<string, string, string> requestForTagFromUser;
        public EventHandler<ExceptionForUserEventArgs> exceptionEventHandler;
        private string inputDay;
        private string inputMonth;
        private string inputYear;
        private string inputExpenditure;
        private string selectedDominantTagToAdd;
        private string selectedAssociatedTagToAdd;
        private List<string> selectedPeopleToRemove = new List<string>();
        private List<string> selectedAssociatedTagsToRemove = new List<string>();
        private string selectedPersonToAdd;
        private ObservableCollection<string> allDominantTags;
        private ObservableCollection<string> allAssociatedTags;
        private ObservableCollection<string> allPeople;
        private string dominantTagForAdding;
        private ObservableCollection<string> associatedTagsForAdding = new ObservableCollection<string>();
        private ObservableCollection<string> peopleForAdding = new ObservableCollection<string>();
        private string userTextInput;


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

        public string InputExpenditure
        {
            get
            {
                return inputExpenditure;
            }
            set
            {
                if (inputExpenditure != value)
                {
                    inputExpenditure = value;
                    RaisePropertyChanged("InputExpenditure");
                }
            }
        }

        public string SelectedDominantTagToAdd
        {
            get
            {
                return selectedDominantTagToAdd;
            }
            set
            {
                if (selectedDominantTagToAdd != value)
                {
                    selectedDominantTagToAdd = value;
                    RaisePropertyChanged("SelectedDominantTagToAdd");
                }
            }
        }

        public ObservableCollection<string> AllDominantTags
        {
            get
            {
                return allDominantTags;
            }
            set
            {
                if (allDominantTags != value)
                {
                    allDominantTags = value;
                    RaisePropertyChanged("AllDominantTags");
                }
            }
        }

        public string SelectedAssociatedTagToAdd
        {
            get
            {
                return selectedAssociatedTagToAdd;
            }
            set
            {
                if (selectedAssociatedTagToAdd != value)
                {
                    selectedAssociatedTagToAdd = value;
                    RaisePropertyChanged("SelectedAssociatedTagToAdd");
                }
            }
        }

        public ObservableCollection<string> AllAssociatedTags
        {
            get
            {
                return allAssociatedTags;
            }
            set
            {
                if (allAssociatedTags != value)
                {
                    allAssociatedTags = value;
                    RaisePropertyChanged("AllAssociatedTags");
                }
            }
        }

        public string SelectedPersonToAdd
        {
            get
            {
                return selectedPersonToAdd;
            }
            set
            {
                if (selectedPersonToAdd != value)
                {
                    selectedPersonToAdd = value;
                    RaisePropertyChanged("SelectedPersonToAdd");
                }
            }
        }

        public ObservableCollection<string> AllPeople
        {
            get
            {
                return allPeople;
            }
            set
            {
                if (allPeople != value)
                {
                    allPeople = value;
                    RaisePropertyChanged("AllPeople");
                }
            }
        }

        public string DominantTagForAdding
        {
            get
            {
                return dominantTagForAdding;
            }
            set
            {
                if (dominantTagForAdding != value)
                {
                    dominantTagForAdding = value;
                    RaisePropertyChanged("DominantTagForAdding");
                }
            }
        }

        public ObservableCollection<string> AssociatedTagsForAdding
        {
            get
            {
                return associatedTagsForAdding;
            }
            set
            {
                if (associatedTagsForAdding != value)
                {
                    associatedTagsForAdding = value;
                    RaisePropertyChanged("AssociatedTagsForAdding");
                }
            }
        }

        public ObservableCollection<string> PeopleForAdding
        {
            get
            {
                return peopleForAdding;
            }
            set
            {
                if (peopleForAdding != value)
                {
                    peopleForAdding = value;
                    RaisePropertyChanged("PeopleForAdding");
                }
            }
        }

        public List<string> AssociatedTagsToRemove
        {
            get
            {
                return selectedAssociatedTagsToRemove;
            }
            set
            {
                if (selectedAssociatedTagsToRemove != value)
                {
                    selectedAssociatedTagsToRemove = value;
                }
            }
        }

        public List<string> PeopleToRemove
        {
            get
            {
                return selectedPeopleToRemove;
            }
            set
            {
                if (selectedPeopleToRemove != value)
                {
                    selectedPeopleToRemove = value;
                }
            }
        }

        public string UserTextInput
        {
            get
            {
                return userTextInput;
            }
            set
            {
                if (userTextInput != value)
                {
                    userTextInput = value;
                }
            }
        }

        public ViewModel(Action<string, string> messageForUser, Func<string, string, bool> decisionForUser, IProvideExpenditureDataRecorder recorderFactory, IProvideExpenditureDataProvider dataProviderFactory)
        {
            recorder = recorderFactory.GetExpenditureDataRecorder();
            dataProvider = dataProviderFactory.GetExpenditureDataProvider();
            allDominantTags = new ObservableCollection<string>(dataProvider.GetDominantTags());

            allAssociatedTags = new ObservableCollection<string>(dataProvider.GetAssociatedTags());
            allPeople = new ObservableCollection<string>(dataProvider.GetPeople());
            this.messageForUser = messageForUser;
            this.decisionForUser = decisionForUser;
        }

        public ICommand RecordExpenditureCommand
        {
            get { return new RelayCommand(new Action(this.OnRecordExpenditure)); }
        }

        public ICommand AddDominantTagCommand
        {
            get { return new RelayCommand(new Action(this.OnAddDominantTag)); }
        }

        public ICommand AddAssociatedTagCommand
        {
            get { return new RelayCommand(new Action(this.OnAddAssociatedTag)); }
        }

        public ICommand AddPersonCommand
        {
            get { return new RelayCommand(new Action(this.OnAddPerson)); }
        }

        public ICommand RemoveDominantTagCommand
        {
            get { return new RelayCommand(new Action(this.OnRemoveDominantTag)); }
        }

        public ICommand RemoveAssociatedTagCommand
        {
            get { return new RelayCommand(new Action(this.OnRemoveAssociatedTag)); }
        }

        public ICommand RemovePersonCommand
        {
            get { return new RelayCommand(new Action(this.OnRemovePerson)); }
        }

        public ICommand AddNewDominantTagCommand
        {
            get { return new RelayCommand(new Action(this.OnAddNewDominantTag)); }
        }

        public ICommand AddNewAssociatedTagCommand
        {
            get { return new RelayCommand(new Action(this.OnAddNewAssociatedTag)); }
        }

        public ICommand AddNewPersonCommand
        {
            get { return new RelayCommand(new Action(this.OnAddNewPerson)); }
        }

        public ICommand ResetPropertiesCommand
        {
            get { return new RelayCommand(new Action(ResetProperties)); }
        } 

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void OnRecordExpenditure()
        {
            try
            {
                ExpenditureDate date = ConvertDate();
                double expenditure = ConvertExpenditure();
                CheckForTags();
                ExpenditureEntry entry = new ExpenditureEntry(dominantTagForAdding, associatedTagsForAdding.ToList(), peopleForAdding.ToList(), expenditure, date);
                recorder.RecordExpenditureData(entry);
                ResetProperties();
            }
            catch (ExceptionForUser e)
            {
                ExceptionForUserEventArgs args = new ExceptionForUserEventArgs(e);
                exceptionEventHandler.Invoke(this, args);
            }
        }

        private ExpenditureDate ConvertDate()
        {
            int day;
            int month;
            int year;
            if (string.IsNullOrWhiteSpace(inputDay) || string.IsNullOrWhiteSpace(inputMonth) || string.IsNullOrWhiteSpace(inputYear))
            {
                throw new ExceptionForUser("Missing date");
            }
            bool dayCorrectFormat = int.TryParse(inputDay, out day);
            bool monthCorrectFormat = int.TryParse(inputMonth, out month);
            bool yearCorrectFormat = int.TryParse(inputYear, out year);

            if (!(dayCorrectFormat && monthCorrectFormat && yearCorrectFormat))
            {
                throw new ExceptionForUser("Date is not in integer format");
            }

            return new ExpenditureDate(day, month, year);
        }

        private double ConvertExpenditure()
        {
            double expenditure;
            if (inputExpenditure == null)
            {
                throw new ExceptionForUser("Missing expenditure");
            }

            if (!double.TryParse(inputExpenditure, out expenditure))
            {
                throw new ExceptionForUser("Expenditure is not  valid number");
            }

            return expenditure;
        }

        private void CheckForTags()
        {
            if (string.IsNullOrWhiteSpace(dominantTagForAdding))
            {
                throw new ExceptionForUser("Missing dominant tag");
            }
        }

        private void OnAddDominantTag()
        {
            if (SelectedDominantTagToAdd == null)
            {
                messageForUser("Please select a dominant tag and try again", "No tag selected");
            }
            else if (DominantTagForAdding == null)
            {
                DominantTagForAdding = SelectedDominantTagToAdd;
                SelectedDominantTagToAdd = null;
            }
            else if (DominantTagForAdding == SelectedDominantTagToAdd)
            {
                messageForUser("Dominant tag already added", "Warning");
            }
            else if (DominantTagForAdding != SelectedDominantTagToAdd)
            {
                bool replaceTag = decisionForUser("Dominant tag already added. Replace with selected tag", "Replace tag?");
                if (replaceTag)
                {
                    DominantTagForAdding = SelectedDominantTagToAdd;
                }
            }
            SelectedDominantTagToAdd = null;
        }

        private void OnAddAssociatedTag()
        {
            if (SelectedAssociatedTagToAdd == null)
            {
                messageForUser("Please select an associated tag and try again", "No tag selected");
            }
            else if (!AssociatedTagsForAdding.Contains(SelectedAssociatedTagToAdd))
            {
                AssociatedTagsForAdding.Add(SelectedAssociatedTagToAdd);
            }
            else
            {
                messageForUser("Associated tag already added", "Warning");
            }
            SelectedAssociatedTagToAdd = null;
        }

        private void OnAddPerson()
        {
            if (SelectedPersonToAdd == null)
            {
                messageForUser("Please select a person and try again", "No person selected");
            }
            else if (!PeopleForAdding.Contains(SelectedPersonToAdd))
            {
                PeopleForAdding.Add(SelectedPersonToAdd);
            }
            else
            {
                messageForUser("Person already added", "Warning");
            }
            SelectedPersonToAdd = null;
        }

        private void OnRemoveDominantTag()
        {
            DominantTagForAdding = null;
        }

        private void OnRemoveAssociatedTag()
        {
            foreach (string selectedAssociatedTagToRemove in AssociatedTagsToRemove)
            {
                if (AssociatedTagsForAdding.Contains(selectedAssociatedTagToRemove))
                {
                    AssociatedTagsForAdding.Remove(selectedAssociatedTagToRemove);
                }
            }
        }

        private void OnRemovePerson()
        {
            foreach (string selectedPersonToRemove in PeopleToRemove)
            {
                if (PeopleForAdding.Contains(selectedPersonToRemove))
                {
                    PeopleForAdding.Remove(selectedPersonToRemove);
                }
            }
        }

        private void OnAddNewDominantTag()
        {
            AllDominantTags.Add(UserTextInput);
            UserTextInput = null;
        }

        private void OnAddNewAssociatedTag()
        {
            AllAssociatedTags.Add(UserTextInput);
            UserTextInput = null;
        }

        private void OnAddNewPerson()
        {
            AllPeople.Add(UserTextInput);
            UserTextInput = null;
        }

        private void ResetProperties()
        {
            InputDay = null;
            InputMonth = null;
            InputYear = null;
            InputExpenditure = null;
            SelectedDominantTagToAdd = null;
            SelectedAssociatedTagToAdd = null;
            SelectedPersonToAdd = null;
            DominantTagForAdding = null;
            AssociatedTagsForAdding = new ObservableCollection<string>();
            PeopleForAdding = new ObservableCollection<string>();
        }
    }
}
