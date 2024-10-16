using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TravelRecordApp.Helpers
{
    public interface IAuth
    {
        Task<bool> RegisterUser(string email, string password);
        Task<bool> LoginUser(string email, string password);
        bool IsAuthenticated();

        string GetCurrentUserId();
    }
   public class Auth
    {
        private static IAuth auth = DependencyService.Get<IAuth>();
        public static async Task<bool> RegisterUser(string email,string password)
        {
            //Needs a connection to the internet
            try
            {
                return await auth.RegisterUser(email, password);
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                return false;
            }
        }
        public static async Task<bool> LoginUser(string email, string password)
        {
            //if user does not exist register
            //Needs a connection to the internet

            try
            {
                return await auth.LoginUser(email, password);
            }
            catch(Exception ex)
            {
               await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                string registerMessage = "There is no user corresponding to the identifier";
                if(ex.Message.Contains(registerMessage)) 
                { 
                    return await RegisterUser(email, password);
                }
                return false;
            }
           
        }
        public static bool IsAuthenticated()
        {
            return auth.IsAuthenticated();
        }
        public static string GetCurrentUserId()
        {
            return auth.GetCurrentUserId();
        }
    }
}
