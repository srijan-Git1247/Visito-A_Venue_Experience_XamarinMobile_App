using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.SymbolStore;
using System.Text;
using TravelRecordApp.Helpers;
using Xamarin.Forms;

namespace TravelRecordApp.ViewModel
{
    public class MainVM: INotifyPropertyChanged
    {
        public Command LoginCommand
        {
            get; set;
        }
        private string email;
        public String Email
        {
            get
            {

                return email;
            }
            set
            {
                email = value;
                OnPropertyChanged("EntriesHaveText");
            }
        }
        private string password;
        public String Password
        {
            get
            {

                return password;
            }
            set
            {
                password = value;
                OnPropertyChanged("EntriesHaveText");
            }
        }

        private bool entriesHaveText;
        public bool EntriesHaveText
        {
            get
            {
                return !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password);
            }
            
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public MainVM()
        {
            LoginCommand=new Command<bool>(Login, CanLogin);
        }

       

        private async void Login(bool parameter)
        {
            


                //Authenticate
                bool result = await Auth.LoginUser(Email,Password);

                if ((result))
                {
                    await App.Current.MainPage.Navigation.PushAsync(new HomePage());//Compute Bound Operation runs on different thread.
                                                               //PushAsync has returns a task.run that makes it run on a different thread.
                }


            
        }

        private bool CanLogin(bool parameter)
        {
            return EntriesHaveText;





        }
        private void OnPropertyChanged(string propertyName) 
        { 
        
        
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        
        }

    }
}
