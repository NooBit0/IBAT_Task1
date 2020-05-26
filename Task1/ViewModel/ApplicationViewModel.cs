using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Task1.Model;
using Task1.FileExtensions;
using Task1.DbContexts;
using System.Data.Entity;
using Task1.View.DialogService;
using System.Windows;

namespace Task1.ViewModel
{
    public class ApplicationViewModel
    {
        private DateTime date;

        private UserDbContext userDataBase;

        public ApplicationViewModel()
        {
            userDataBase = new UserDbContext();
            userDataBase.User.Load();
            Users = userDataBase.User.Local;
        }

        public ObservableCollection<User> Users { get; set; }

        public string FirstNameSelection { get; set; }

        public string LastNameSelection { get; set; }

        public string SurNameSelection { get; set; }

        public string CitySelection { get; set; }

        public string CountrySelection { get; set; }

        public string DateSelection
        {
            get
            {
                if(date != DateTime.MinValue)
                    return date.ToShortDateString();
                return string.Empty;
            }
            set
            {
                try
                {
                    date = DateTime.Parse(value);
                }
                catch (Exception)
                {
                    MessageBox.Show("Invalid date format!");
                }
            }
        }

        public RelayCommand SaveToDataBaseCommand => new RelayCommand(obj =>
        {
            userDataBase.SaveChanges();
            MessageBox.Show("Users saved to database!");
        });

        public RelayCommand GetFromFileCommand => new RelayCommand(obj => GetFromFileDialog());

        public RelayCommand SaveReportToFileCommand => new RelayCommand(obj => SaveReportToFile());

        private List<User> UsersSelectionByParameters()
        {
            List<User> selectedUsers = Users.ToList();
            if (FirstNameSelection != string.Empty && FirstNameSelection != null)
            {
                selectedUsers = selectedUsers.Where(item => item.FirstName == FirstNameSelection).Select(item => item).ToList();
            }
            if (LastNameSelection != string.Empty && LastNameSelection != null)
            {
                selectedUsers = selectedUsers.Where(item => item.LastName == LastNameSelection).Select(item => item).ToList();
            }
            if (SurNameSelection != string.Empty && SurNameSelection != null)
            {
                selectedUsers = selectedUsers.Where(item => item.SurName == SurNameSelection).Select(item => item).ToList();
            }
            if (CitySelection != string.Empty && CitySelection != null)
            {
                selectedUsers = selectedUsers.Where(item => item.City == CitySelection).Select(item => item).ToList();
            }
            if (CountrySelection != string.Empty && CountrySelection != null)
            {
                selectedUsers = selectedUsers.Where(item => item.Country == CountrySelection).Select(item => item).ToList();
            }
            if (date != DateTime.MinValue)
            {
                selectedUsers = selectedUsers.Where(item => item.Date == date.ToShortDateString()).Select(item => item).ToList();
            }

            return selectedUsers;
        }

        private void SaveReportToFile()
        {
            DefaultDialogService dialogService = new DefaultDialogService();
            if (dialogService.SaveFileDialog())
            {
                if (dialogService.FileFormat == ".csv")
                    FileExtension.SaveToCsvFile(dialogService.FilePath, UsersSelectionByParameters());
                else if (dialogService.FileFormat == ".xml")
                    FileExtension.SaveToXmlFile(dialogService.FilePath, UsersSelectionByParameters());
                else
                    dialogService.ShowMessage("Error saving report!");
            }
            else
            {
                dialogService.ShowMessage("Error saving report!");
            }
        }

        private void GetFromFileDialog()
        {
            DefaultDialogService dialogService = new DefaultDialogService();
            if (dialogService.OpenFileDialog())
            {
                dialogService.ShowMessage("File is open!");
                GateFromFile(dialogService.FilePath);
            }
            else
            {
                dialogService.ShowMessage("Error opening file!");
            }
        }

        private void GateFromFile(string path)
        {
            List<User> a = FileExtension.GetFromFile(path)?.ToList();
            if(a != null)
                foreach (var item in a)
                {
                    Users.Add(item);
                }
        }
    }
}