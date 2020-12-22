using E3.AuditTrailManager.Model;
using E3.AuditTrailManager.Model.Enums;
using E3.ReactorManager.Interfaces.DataAbstractionLayer;
using E3.UserManager.Model.Data;
using E3.UserManager.Model.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Controls;
using System.Windows.Input;
using Unity;

namespace USV.ReactorManager.UI.ViewModels
{
    public class LoginViewModel : BindableBase, IRegionMemberLifetime
    {
        IRegionManager regionManager;
        Timer timer;
        TaskScheduler taskScheduler;
        IAuditTrailManager auditTrailManager;
        IDatabaseReader databaseReader;
        IUserManager userManager;

        public LoginViewModel(IUnityContainer containerProvider, IDatabaseReader databaseReader, IRegionManager regionManager, IUserManager userManager)
        {
            this.regionManager = regionManager;
            this.userManager = userManager;
            auditTrailManager = containerProvider.Resolve<IAuditTrailManager>();
            this.databaseReader = databaseReader;
            taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();

            UserNameError = false;
            PasswordError = false;

            DateTimeString = DateTime.Now.ToString();

            Task.Factory.StartNew(new Action(StartTimer));
        }

        #region Live Date Time Updaters In UI

        private void StartTimer()
        {
            timer = new Timer(TimeSpan.FromSeconds(1).TotalMilliseconds);
            timer.Elapsed += UpdateUI;
            timer.Start();
        }

        private void UpdateUI(object sender, ElapsedEventArgs e)
        {
            DateTimeString = DateTime.Now.ToString();
        }

        #endregion

        #region Login
        public void Login(PasswordBox passwordBox)
        {
            /* Update password */
            Password = passwordBox.Password;

            if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password))
            {
                Task.Factory.StartNew(new Func<User>(ValidateUserCredentials))
                    .ContinueWith(new Action<Task<User>>(UpdateUserDetailsAndValidationStatus), taskScheduler);
            }
            else
            {
                UserNameError = true;
                PasswordError = true;
            }
        }

        private void UpdateUserDetailsAndValidationStatus(Task<User> task)
        {
            if (task.IsCompleted)
            {
                User userDetails = task.Result;

                if (userDetails != null && userDetails.UserID != null && CheckIfUserIsActive(userDetails.CurrentStatus))
                {
                    //Add the User Details as NavigationParameters
                    NavigationParameters parameters = new NavigationParameters();
                    parameters.Add("UserDetails", userDetails);

                    //Log Audit Trail as User Logged in
                    auditTrailManager.RecordEventAsync(userDetails.Name + " logged in", userDetails.Name, EventTypeEnum.UserManagement);

                    //Navigate to Dashboard
                    regionManager.RequestNavigate("SelectedViewPane", "Dashboard", parameters);
                }
                else
                {
                    UserNameError = true;
                    PasswordError = true;
                }
            }
        }

        private User ValidateUserCredentials()
        {
            Credential credential = new Credential { Username = this.Username, PasswordHash = this.Password };

            return userManager.AuthenticateCredential(credential);
        }

        /// <summary>
        /// Checks whether the user's login status in Active or Inactive
        /// </summary>
        /// <param name="userCurrentStatus"></param>
        /// <returns></returns>
        public bool CheckIfUserIsActive(UserStatus userCurrentStatus)
        {
            switch (userCurrentStatus)
            {
                case UserStatus.Active:
                    return true;
            }
            return false;
        }
        #endregion

        private ICommand _loginCommand;
        public ICommand LoginCommand
        {
            get => _loginCommand ?? (_loginCommand = new DelegateCommand<PasswordBox>(Login));
            set { _loginCommand = value; }
        }

        #region Properties

        public bool KeepAlive
        {
            get => false;
        }

        /// <summary>
        /// gives real date time to GUI
        /// </summary>
        private string _dateTimeString;
        public string DateTimeString
        {
            get { return _dateTimeString; }
            set
            {
                _dateTimeString = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Describes if there is Username Error  
        /// </summary>
        private bool _userNameError;
        public bool UserNameError
        {
            get { return _userNameError; }
            set
            {
                _userNameError = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Describes if there is Password Error
        /// </summary>
        private bool _passwordError;
        public bool PasswordError
        {
            get { return _passwordError; }
            set
            {
                _passwordError = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Contains the username entered in GUI
        /// </summary>
        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                RaisePropertyChanged();
                ResetErrors();
            }
        }

        /// <summary>
        /// Contains the password entered in GUI
        /// </summary>
        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                RaisePropertyChanged();
                ResetErrors();
            }
        }

        private void ResetErrors()
        {
            UserNameError = false;
            PasswordError = false;
        }
        #endregion
    }
}
