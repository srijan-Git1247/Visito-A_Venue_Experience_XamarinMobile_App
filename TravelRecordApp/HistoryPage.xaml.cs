using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using TravelRecordApp.Helpers;
using TravelRecordApp.Model;
using TravelRecordApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryPage : ContentPage
    {
        private HistoryVM vm;

        public HistoryPage()
        {
            InitializeComponent();
            vm = Resources["vm"] as HistoryVM;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();


            vm.GetPosts();
           

        }

        
    }
}