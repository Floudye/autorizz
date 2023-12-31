﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security;
using System.Security.Cryptography;
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
        private int id;
        private IUserRepository userEditor;
        private IUserRepository userDelete;
        private string _username;
        private string _password;
        private string _errorMessage;
        private bool _isViewVisible = true;
        private LoginView loginView = LoginView.loginview;
        public UserRepository dbL = new UserRepository();
        private ObservableCollection<UserModel> users;
        public static LoginViewModel loginViewModel;
        private string firstpassword;
        private string lastpassword;
        private Visibility firstStackPanelVisibility = Visibility.Visible;
        private Visibility secondStackPanelVisibility = Visibility.Hidden;
        private IUserRepository userRepository;
        private UserModel CurrentUser;

        //Properties

        public ObservableCollection<UserModel> Users
        {
            get => users;
            set
            {
                users = value;
                OnPropertyChanged(nameof(Users));
            }
        }
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
                OnPropertyChanged(nameof(Firstpassword));
            }
        }
        private string name;
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        private string access;
        public string AccessLvl
        {
            get
            {
                return access;
            }

            set
            {
                access = value;
                OnPropertyChanged(nameof(AccessLvl));
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
                OnPropertyChanged(nameof(Lastpassword));
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
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
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
        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand EditCommand { get; }


        //Constructor
        public LoginViewModel()
        {
            AddCommand = new ViewModelCommand(Add, CanAdd);
            userDelete = new UserRepository();
            userEditor = new UserRepository();
            Users = new ObservableCollection<UserModel>(dbL.GetAllUsers());
            loginViewModel = this;
            userRepository = new UserRepository();
            LoginCommand = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
            RegistrationCommand = new ViewModelCommand(ExecuteRegisterCommand, CanExecuteRegisterCommand);
            RecoverPasswordCommand = new ViewModelCommand(p => ExecuteRecoverPassCommand("", ""));
            SwapVisabilityCommand = new ViewModelCommand(SwapVisibility);
            DeleteCommand = new ViewModelCommand(DeleteUser);
            EditCommand = new ViewModelCommand(Edit);
        }

        private void Add(object parametr)
        {
            userRepository.Add(Username, Password, Name, AccessLvl);
            string message = "Admin добавил данные пользователя.";
            MessageBox.Show(message);
        }

        private bool CanAdd(object parametr)
        {
            return true;
        }
        private void DeleteUser(object parameter)
        {
            userDelete.DeleteUsername(Id);
            string message = "Пользователь был удален";
            MessageBox.Show(message);
            Users = new ObservableCollection<UserModel>(dbL.GetAllUsers());
        }
        private void Edit(object parameter)
        {
            userEditor.EditUser(Username,Password,Name, AccessLvl);
            string message = "Пользователь был отредактирован.";
            MessageBox.Show(message);
            Users = new ObservableCollection<UserModel>(dbL.GetAllUsers());
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
                userRepository.CreateUser(Username, Firstpassword, "user");
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
                CurrentUser = dbL.GetByUsername(Username);
                Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(Username), null);

                // Открывайте окно в зависимости от роли
                OpenWindowBasedOnRole(CurrentUser.AccessLvl);
            }
            else
            {
                ErrorMessage = "неверный пароль или логин";
            }
                        
        }
        private void OpenWindowBasedOnRole(string role)
        {
            if (role == "admin")
            {
                MainView adminWindow = new MainView();
                Application.Current.MainWindow.Close();
                Application.Current.MainWindow = adminWindow;
                adminWindow.Show();
            }
            else if (role == "user")
            {
                UserWindows userWindow = new UserWindows();
                Application.Current.MainWindow.Close();
                Application.Current.MainWindow = userWindow;
                userWindow.Show();
            }
        }

        private void ExecuteRecoverPassCommand(string username, string email)
        {
            throw new NotImplementedException();
        }
    }
}
