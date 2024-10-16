using Foundation;
using Intents;
using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.Helpers;
using TravelRecordApp.Model;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(TravelRecordApp.iOS.Dependencies.Firestore))]

namespace TravelRecordApp.iOS.Dependencies
{
    internal class Firestore : IFirestore
    {
        //Last 30-09-2024

        public async Task<bool> Delete(Post post)
        {
            try { 
           var collection = Firebase.CloudFirestore.Firestore.SharedInstance.GetCollection("posts");
           await collection.GetDocument(post.ID).DeleteDocumentAsync();
           return true;
            }

            catch(Exception ex) 
            {

                return false;
            }
        }

        public bool Insert(Post post)
        {
            try
            {
                var keys = new[]
                {
                    new NSString("experience"),
                    new NSString("venueName"),
                    new NSString("categoryid"),
                    new NSString("categoryname"),
                    new NSString("address"),
                    new NSString("formattedAddress"),
                    new NSString("postcode"),
                    new NSString("userID")
                    


                };
                var values = new NSObject[]
                {
                    new NSString(post.Experience),
                    new NSString(post.VenueName),
                    new NSString(post.Categoryid),
                    new NSString(post.Categoryname),
                    new NSString(post.Address),
                    new NSString(post.FormattedAddress),
                    new NSString(post.Postcode),
                    new NSString(Firebase.Auth.Auth.DefaultInstance.CurrentUser.Uid)
                };


                var document = new NSDictionary<NSString, NSObject>(keys,values);

                var collection = Firebase.CloudFirestore.Firestore.SharedInstance.GetCollection("posts");
                collection.AddDocument(document);
                return true;
            }

            catch (Exception ex)
            {

                return false;
            }
        }

        public async Task<List<Post>> Read()
        {
            try
            { 
            var collection = Firebase.CloudFirestore.Firestore.SharedInstance.GetCollection("posts");
            var query = collection.WhereEqualsTo("userID", Firebase.Auth.Auth.DefaultInstance.CurrentUser.Uid);
            var documents = await query.GetDocumentsAsync();

            List<Post> posts = new List<Post>();

            foreach (var doc in documents.Documents)
            {
                var dictionary = doc.Data;

                var newPost = new Post()
                {
                    Experience = dictionary.ValueForKey(new NSString("experience")) as NSString,
                    VenueName = dictionary.ValueForKey(new NSString("venueName")) as NSString,
                    Categoryid = dictionary.ValueForKey(new NSString("categoryid")) as NSString,
                    Categoryname = dictionary.ValueForKey(new NSString("categoryname")) as NSString,
                    Address = dictionary.ValueForKey(new NSString("address")) as NSString,
                    FormattedAddress = dictionary.ValueForKey(new NSString("formattedAddress")) as NSString,
                    Postcode = dictionary.ValueForKey(new NSString("postcode")) as NSString,
                    UserID = dictionary.ValueForKey(new NSString("userID")) as NSString,
                    ID = doc.Id


                };

                posts.Add(newPost);
            }
            return posts;

        }
            catch(Exception ex)
            {
                return new List<Post>();
            }




        }

        public async Task<bool> Update(Post post)
        {
            try
            {
                var keys = new[]
                {
                    new NSString("experience"),
                    new NSString("venueName"),
                    new NSString("categoryid"),
                    new NSString("categoryname"),
                    new NSString("address"),
                    new NSString("formattedAddress"),
                    new NSString("postcode"),
                    new NSString("userID")



                };
                var values = new NSObject[]
                {
                    new NSString(post.Experience),
                    new NSString(post.VenueName),
                    new NSString(post.Categoryid),
                    new NSString(post.Categoryname),
                    new NSString(post.Address),
                    new NSString(post.FormattedAddress),
                    new NSString(post.Postcode),
                    new NSString(Firebase.Auth.Auth.DefaultInstance.CurrentUser.Uid)
                };


                var document = new NSDictionary<NSObject, NSObject>(keys, values);

                var collection = Firebase.CloudFirestore.Firestore.SharedInstance.GetCollection("posts");
                await collection.GetDocument(post.ID).UpdateDataAsync(document);
                return true;
            }

            catch (Exception ex)
            {

                return false;
            }
        }
    }
}