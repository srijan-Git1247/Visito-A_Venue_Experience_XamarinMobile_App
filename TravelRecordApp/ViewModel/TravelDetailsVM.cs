using System;
using System.Collections.Generic;
using System.Text;
using TravelRecordApp.Helpers;
using TravelRecordApp.Model;
using Xamarin.Forms;

namespace TravelRecordApp.ViewModel
{
    public class TravelDetailsVM
    {
        public Command UpdateCommand { get; set; }
        public Command DeleteCommand { get; set; }

        public Post SelectedPost { get; set; }  
        public TravelDetailsVM()
        {
            UpdateCommand = new Command<string>(Update, CanUpdate);
            DeleteCommand = new Command(Delete);    
        }
        private bool CanUpdate(string newExperience)
        {
            if(string.IsNullOrEmpty(newExperience)) 
                return false;

            return true;
        }







        private async void Update(string newExperience)
        {
            SelectedPost.Experience = newExperience;
            bool result = await Firestore.Update(SelectedPost);
            if (result)
            {
                await App.Current.MainPage.Navigation.PopAsync();

            }
        }
        private async void Delete()
        {
            bool result = await Firestore.Delete(SelectedPost);


            if (result)
            {
                await App.Current.MainPage.Navigation.PopAsync();
            }
        }
    }
}
