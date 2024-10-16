using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TravelRecordApp.Helpers;
using TravelRecordApp.Model;
using System.Linq;
using System.Collections.Specialized;
using System.ComponentModel;
namespace TravelRecordApp.ViewModel
{
    public class ProfileVM:INotifyPropertyChanged
    {
        public ObservableCollection<CategoryCount> Categories{ get; set; }

        private int postCount;
        public int PostCount { 
            
            
            get { 
            
            
                return postCount;
            
            
            }
            
            
            
            set
            {
                postCount = value;
                OnPropertyChanged("PostCount");
            }
                
                
                
         }

        public event PropertyChangedEventHandler PropertyChanged;






        public ProfileVM()
        {

            Categories = new ObservableCollection<CategoryCount>();
        }

     

        public async void GetPosts()
        {
            Categories.Clear();
            var posts = await Firestore.Read();

            PostCount= posts.Count();
            var categories = (from p in posts
                              orderby p.Categoryid
                              select p.Categoryname).Distinct().ToList();

            foreach (var category in categories)
            {

                var count = (from post in posts
                             where post.Categoryname == category
                             select post).ToList().Count();

                //Fluent Syntax



                Categories.Add(new CategoryCount
                {
                    Name=category,
                    Count=count

                });

            }





        }
       private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
       public class CategoryCount
        {
            public string Name { get; set; }
            public int Count { get; set; }
        }
    }
    
}
