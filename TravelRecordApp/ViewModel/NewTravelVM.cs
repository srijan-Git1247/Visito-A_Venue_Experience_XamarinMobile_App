using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using TravelRecordApp.Helpers;
using TravelRecordApp.Model;
using Xamarin.Forms;

namespace TravelRecordApp.ViewModel
{
    public class NewTravelVM: INotifyPropertyChanged
    {
        public ObservableCollection<Result> Venues { get; set; }// Can be named to Result

        public Command SaveCommand { get; set; }



        private string experience;
        public string Experience
        {
            get
            {
                return experience;
            }
            set
            {
                experience = value;
                OnPropertyChanged("Experience");
                OnPropertyChanged("PostIsReady");
            }
        }

        private Result selectedVenue;
        public Result SelectedVenue
        {
            get
            {
                return selectedVenue;
            }
            set
            {
                selectedVenue = value;
                OnPropertyChanged("PostIsReady");
            }
           
        }

        private bool postIsReady;
        public bool PostIsReady
        {
            get
            {
                return !string.IsNullOrEmpty(Experience) && SelectedVenue!=null;
            }
           
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public NewTravelVM()
        {
            Venues=new ObservableCollection<Result>();

            SaveCommand = new Command<bool>(Save, CanSave);
        }

      
        private void Save(bool parameter)
        {
            try
            {
                
                var firstCategory = SelectedVenue.categories.FirstOrDefault();
                Post post = new Post()
                {
                    Experience = Experience,
                    Categoryid = firstCategory.id.ToString(),
                    Categoryname = firstCategory.name,
                    Address = SelectedVenue.location.address,
                    Postcode = SelectedVenue.location.postcode,
                    FormattedAddress = SelectedVenue.location.formatted_address,

                    VenueName = SelectedVenue.name


                };
                

                bool result = Firestore.Insert(post);
                if (result)
                {
                    Experience = string.Empty;
                    App.Current.MainPage.DisplayAlert("Success", "Experience Successfully Inserted", "OK");
                }
                else
                {
                    App.Current.MainPage.DisplayAlert("Failure", "Experience Failed to be Inserted", "OK");
                }





            }
            catch (NullReferenceException nre)
            {

            }
            catch (Exception ex)
            {

            }

        }
        private bool CanSave(bool parameter)
        {
            return parameter;
        }
        public async void GetVenues(double lat, double lng)
        {
            var venues = await Result.GetVenues(lat, lng);

            Venues.Clear();
            foreach (var result in venues)
            {
                Venues.Add(result); 
                
            }
        }
        private void OnPropertyChanged(string propertyName)
        {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }




    }







}
