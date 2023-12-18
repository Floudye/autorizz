using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPF_LoginForm.Models;
using WPF_LoginForm.Repositories;
using WPF_LoginForm.Views;

namespace WPF_LoginForm.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        //Fields
        private string _username;
        private string _password;
        private string _errorMessage;
        private bool _isViewVisible = true;
        private LoginView loginView = LoginView.loginview;
        public static LoginViewModel loginViewModel;
        private string firstpassword;
        private string lastpassword;
        private Visibility firstStackPanelVisibility = Visibility.Visible;
        private Visibility secondStackPanelVisibility = Visibility.Hidden;
        private IUserRepository userRepository;

        //Properties
        public string Username
        {
            get
            {
                return _username;
            }

            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
       
        public string Firstpassword
        {
            get
            {
                return firstpassword;
            }

            set
            {
                firstpassword = value;
                OnPropertyChanged(nameof(firstpassword));
            }
        }
        public string Lastpassword
        {
            get
            {
                return lastpassword;
            }

            set
            {
                lastpassword = value;
                OnPropertyChanged(nameof(lastpassword));
            }
        }
        public string Password
        {
            get
            {
                return _password;
            }

            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }

            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public Visibility FirstStackPanelVisibility
        {
            get => firstStackPanelVisibility;
            set
            {
                firstStackPanelVisibility = value;
                OnPropertyChanged(nameof(FirstStackPanelVisibility));
            }
        }

        public Visibility SecondStackPanelVisibility
        {
            get => secondStackPanelVisibility;
            set
            {
                secondStackPanelVisibility = value;
                OnPropertyChanged(nameof(SecondStackPanelVisibility));
            }
        }

        public bool IsViewVisible
        {
            get
            {
                return _isViewVisible;
            }

            set
            {
                _isViewVisible = value;
                OnPropertyChanged(nameof(IsViewVisible));
            }
        }

        //-> Commands
        public ICommand LoginCommand { get; }
        public ICommand RecoverPasswordCommand { get; }
        public ICommand ShowPasswordCommand { get; }
        public ICommand RememberPasswordCommand { get; }
        public ICommand RegistrationCommand { get; }
        public ICommand SwapVisabilityCommand { get; }
        

        //Constructor
        public LoginViewModel()
        {
            loginViewModel = this;
            userRepository = new UserRepository();
            LoginCommand = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
            RegistrationCommand = new ViewModelCommand(ExecuteRegisterCommand, CanExecuteRegisterCommand);
            RecoverPasswordCommand = new ViewModelCommand(p => ExecuteRecoverPassCommand("", ""));
            SwapVisabilityCommand = new ViewModelCommand(SwapVisibility);
        }
        private void ExecuteRegisterCommand(object parameter)
        {
            if (userRepository.logcheck(Username))
            {
                ErrorMessage = "Такой пользователь уже существует";
            }
            else if (Firstpassword != Lastpassword)
            {
                ErrorMessage = "Первый пароль не совпадает с вторым";
            }
            else
            {
                userRepository.CreateUser(Username, Firstpassword);
                SwapVisibility(Username);
            }
        }
        private bool CanExecuteRegisterCommand(object obj)
        {
            return true;
        }
        private bool CanExecuteLoginCommand(object obj)
        {
            bool validData;
            if (string.IsNullOrWhiteSpace(Username) || Username.Length < 3 ||
                Password == null || Password.Length < 3)
                validData = false;
            else
                validData = true;
            return validData;
        }

        public void SwapVisibility(object obj)
        {
            FirstStackPanelVisibility = FirstStackPanelVisibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;
            SecondStackPanelVisibility = SecondStackPanelVisibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;
        }
        private void ExecuteLoginCommand(object obj)
        {
            var isValidUser = userRepository.AuthenticateUser(new NetworkCredential(Username, Password));
            if (isValidUser)
            {
                MainView window = new MainView();
                window.Show();
                loginView.Close();
            }
            else
            {
                ErrorMessage = "неверный пароль или логин";
            }
        }


        private void ExecuteRecoverPassCommand(string username, string email)
        {
            throw new NotImplementedException();
        }
    }
}
