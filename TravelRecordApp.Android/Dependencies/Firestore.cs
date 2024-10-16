using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.Helpers;
using TravelRecordApp.Model;
using Java.Util;
using Android.Gms.Tasks;
using Java.Interop;
using Firebase.Firestore;

[assembly: Dependency(typeof(TravelRecordApp.Droid.Dependencies.Firestore))]
namespace TravelRecordApp.Droid.Dependencies
{
   public class Firestore: Java.Lang.Object, IFirestore , IOnCompleteListener


    {

        List<Post> posts;
        bool hasReadPosts = false;
        public Firestore()
        {
            posts=new List<Post>();
        }

        

        public async Task<bool> Delete(Post post)
        {
            try
            {
                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("posts");
                collection.Document(post.ID).Delete();
                return true;
            }

            catch (Exception ex)
            {

                return false;
            }
        }

        
        public bool Insert(Post post)
        {
            try
            {
                var postDocument = new Dictionary<string, Java.Lang.Object>
            {
                { "experience", post.Experience },
                { "venueName", post.VenueName },
                { "categoryid", post.Categoryid },
                { "categoryname", post.Categoryname },
                { "address", post.Address },
                { "formattedAddress", post.FormattedAddress},
                { "postcode", post.Postcode},
                { "userID", Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Uid},

            };
                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("posts");
                collection.Add(new HashMap(postDocument));

                return true;
            } 
            catch (Exception ex) 
            {
                return false;
            
            
            }
        }

        public void OnComplete(Android.Gms.Tasks.Task task)
        {
            if(task.IsSuccessful)
            {
                var documents= (QuerySnapshot) task.Result;
                posts.Clear();
                foreach(var doc in documents.Documents)
                {
                    Post newPost = new Post()
                    {
                        Experience = doc.Get("experience").ToString(),
                        VenueName = doc.Get("venueName").ToString(),
                        Categoryid = doc.Get("categoryid").ToString(),
                        Categoryname = doc.Get("categoryname").ToString(),
                        Address = doc.Get("address").ToString(),
                        FormattedAddress = doc.Get("formattedAddress").ToString(),
                        Postcode = doc.Get("postcode").ToString(),
                        UserID = doc.Get("userID").ToString(),
                        ID = doc.Id



                    };
                    posts.Add(newPost);
                }
            }
            else
            {

                posts.Clear();
            }
            hasReadPosts = true;
        }

        public async Task<List<Post>> Read()
        {
            try
            {
                hasReadPosts = false;
                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("posts");
                var query = collection.WhereEqualTo("userID", Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Uid);
                query.Get().AddOnCompleteListener(this);


                for (int i = 0; i < 50; i++)
                {
                    await System.Threading.Tasks.Task.Delay(100);
                    if (hasReadPosts)
                        break;
                }
                return posts;
            }
            catch (Exception ex) 
            {
                return posts;
            
            }
        }

       
        public async Task<bool> Update(Post post)
        {
            try
            {
                var postDocument = new Dictionary<string, Java.Lang.Object>
            {
                { "experience", post.Experience },
                { "venueName", post.VenueName },
                { "categoryid", post.Categoryid },
                { "categoryname", post.Categoryname },
                { "address", post.Address },
                { "formattedAddress", post.FormattedAddress},
                { "postcode", post.Postcode},
                { "userID", Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Uid},

            };
                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("posts");
                collection.Document(post.ID).Update(postDocument);

                return true;
            }
            catch (Exception ex)
            {
                return false;


            }
        }
    }
}