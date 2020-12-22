﻿using WpfBaseApp.Base;
using DataBaseAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfBaseApp.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public class MainWindowViewModel : ViewModelBase
    {
        private string _name;


        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }

        public MainWindowViewModel()
        {
            _updateNameCommand =
                new RelayCommand(UpdateName, CanUpdateName);
        }

        private ICommand _updateNameCommand;

        public ICommand UpdateNameCommand
        {
            get { return _updateNameCommand; }
            set { _updateNameCommand = value; }
        }

        private bool CanUpdateName()
        {
            return true;
        }

        private void UpdateName()
        {
            var dataBaseAccessor = new DataBaseAccessor();
            var user = dataBaseAccessor.UpdateName(Name + "Updated");
            Name = user.Name;
        }

    }
}
