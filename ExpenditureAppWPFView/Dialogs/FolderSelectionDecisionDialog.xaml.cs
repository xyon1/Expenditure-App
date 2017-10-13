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

namespace ExpenditureAppWPF.Dialogs
{
    /// <summary>
    /// Interaction logic for FolderSelectionDecisionDialog.xaml
    /// </summary>
    public partial class FolderSelectionDecisionDialog : Window
    {
        private bool chooseFolder = false;
        private bool useDefaultLocation = false;

        public FolderSelectionDecisionDialog()
        {
            InitializeComponent();
            chooseFolderBtn.Click += (s, e) => OnChooseFolderClicked();
            useDefaultBtn.Click += (s, e) => OnUseDefaultClicked();
            closeApplicationBtn.Click += (s, e) => OnCloseApplicationClicked();
        }

        public FolderSelectionDecisionDialogResult Show()
        {
            ShowDialog();
            
            if (chooseFolder)
            {
                return FolderSelectionDecisionDialogResult.ChooseFolder;
            }
            else if (useDefaultLocation)
            {
                return FolderSelectionDecisionDialogResult.UseDefaultLocation;
            }
            else
            {
                return FolderSelectionDecisionDialogResult.CloseApplication;
            }
        }

        private void OnChooseFolderClicked()
        {
            chooseFolder = true;
            Close();
        }

        private void OnUseDefaultClicked()
        {
            useDefaultLocation = true;
            Close();
        }

        private void OnCloseApplicationClicked()
        {
            Close();
        }
    }
}
