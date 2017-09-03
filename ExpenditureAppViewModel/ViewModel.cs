﻿using System;
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
        public Action<string, string> messageForUser;
        public Func<string, string, bool> decisionForUser;
        private string inputDay;
        private string inputMonth;
        private string inputYear;
        private string selectedDominantTag;
        private string selectedAssociatedTag;
        private string selectedPerson;
        private ObservableCollection<string> allDominantTags = new ObservableCollection<string>() { "hello", "my", "name" };
        private ObservableCollection<string> allAssociatedTags = new ObservableCollection<string>() { "hello", "what", "are" };
        private ObservableCollection<string> allPeople = new ObservableCollection<string>() { "Benedict", "Beth", "Paul" };
        private string dominantTagForAdding;
        private ObservableCollection<string> associatedTagsForAdding = new ObservableCollection<string>();
        private ObservableCollection<string> peopleForAdding = new ObservableCollection<string>();


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

        public ObservableCollection<string> AllDominantTags
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

        public ObservableCollection<string> AllAssociatedTags
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

        public ObservableCollection<string> AllPeople
        {
            get
            {
                return allPeople;
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

        public ViewModel(Action<string, string> messageForUser, Func<string, string, bool> decisionForUser)
        {
            this.messageForUser = messageForUser;
            this.decisionForUser = decisionForUser;
        }

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public ICommand InputExpenditureCommand
        {
            get { return new RelayCommand(new Action(this.OnInputExpenditure)); }
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

        private void OnInputExpenditure()
        {
            InputDay = null;
            InputMonth = null;
            InputYear = null;
        }

        private void OnAddDominantTag()
        {
            if (DominantTagForAdding == null)
            {
                DominantTagForAdding = SelectedDominantTag;
                SelectedDominantTag = null;
            }
            else if (DominantTagForAdding == SelectedDominantTag)
            {
                messageForUser("Dominant tag already added", "Warning");
            }
            else if (DominantTagForAdding != SelectedDominantTag)
            {
                bool replaceTag = decisionForUser("Dominant tag already added. Replace with selected tag", "Replace tag?");
                if (replaceTag)
                {
                    DominantTagForAdding = SelectedDominantTag;
                }
            }
            SelectedDominantTag = null;
        }

        private void OnAddAssociatedTag()
        {
            if (!AssociatedTagsForAdding.Contains(SelectedAssociatedTag))
            {
                AssociatedTagsForAdding.Add(SelectedAssociatedTag);
            }
            else
            {
                messageForUser("Associated tag already added", "Warning");
            }
            SelectedAssociatedTag = null;
        }

        private void OnAddPerson()
        {
            if (!PeopleForAdding.Contains(SelectedPerson))
            {
                PeopleForAdding.Add(SelectedPerson);
            }
            else
            {
                messageForUser("Person already added", "Warning");
            }
            SelectedPerson = null;
        }
    }
}
