using System.Threading.Tasks;
using System.Net.Http;
using Windows.Storage;
using Windows.Storage.Streams;
using System;
using System.Collections.Generic;
using Windows.Data.Json;

namespace FigDating
{
    public class User
    {
        private static User user = null;
        private User() { }
        public static User getUser()
        {
            if (user == null)
                user = new User();
            return user;
        }

        public async void uploadPhoto(StorageFile file)
        {
            Windows.Web.Http.HttpClient httpClient = new Windows.Web.Http.HttpClient();
            try
            {
                string posturi = Domain.getDomain() + "upload/";
                IRandomAccessStreamWithContentType stream1 = await file.OpenReadAsync();
                Windows.Web.Http.HttpStreamContent streamContent1 = new Windows.Web.Http.HttpStreamContent(stream1);
                Windows.Web.Http.HttpMultipartFormDataContent fileContent = new Windows.Web.Http.HttpMultipartFormDataContent();
                fileContent.Add(streamContent1, "file", "file.jpg");
                Windows.Web.Http.HttpResponseMessage response = await httpClient.PostAsync(new Uri(posturi), fileContent);
                string responString = await response.Content.ReadAsStringAsync();
                JsonObject jo = JsonObject.Parse(responString);
                changeProfile("path", Domain.getDomain() +"static/"+jo["filename"].GetString());
            }
            catch {}
        }

        public async void changeProfile(string key, string value)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();

            data.Add(key, value);

            var httpClient = new HttpClient();
            try
            {
                var queryString = new FormUrlEncodedContent(data);
                HttpResponseMessage response = await httpClient.PostAsync(Domain.getDomain() + "user/" + User.getId() + "/" , queryString);
                httpClient.Dispose();
                response.EnsureSuccessStatusCode();
            }
            catch
            {
            }        
        }

        public async Task<string> getNotification() {
            HttpClient client = new HttpClient();
            try
            {
                HttpResponseMessage responseHttp = await client.GetAsync(Domain.getDomain() + "user/" + User.getId() + "/notifications/");
                client.Dispose();
                Task<string> message = responseHttp.Content.ReadAsStringAsync();
                return "{\"notifications\": [{\"notification_id\": 20, \"status\": \"read\", \"message\": \"[\\u5468\\u4e88\\u7ef4]\\u5bf9\\u4f60\\u8fdb\\u884c\\u4e86\\u8bc4\\u8bba\", \"user_id\": 1, \"time\": \"2015-03-08 12:31:40.269855+00:00\"}]}";//return await message;

            }
            catch (Exception)
            {
                return "";
            }
        }

        public int useChance() {
            int newChance = GetChance() - 1;
            if (newChance < 0)
                return newChance;

            changeProfile("chance", newChance.ToString());
            // 写回chance
            setChance(newChance);
            return newChance;
        }

        public static string getId()
        {
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            Windows.Storage.ApplicationDataCompositeValue UsrPwd =
   (Windows.Storage.ApplicationDataCompositeValue)localSettings.Values["profile"];
            var id = UsrPwd["username"];
            return id.ToString();
        }

        public static  int GetChance() {
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            Windows.Storage.ApplicationDataCompositeValue UsrPwd =
   (Windows.Storage.ApplicationDataCompositeValue)localSettings.Values["profile"];
            var id = UsrPwd["chance"];
            return int.Parse(id.ToString());
        }

        public static void setChance(int newChance) {
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            Windows.Storage.ApplicationDataCompositeValue Usr =
   (Windows.Storage.ApplicationDataCompositeValue)localSettings.Values["profile"];
            Usr["chance"] = newChance;
        }

        public static string getUsername() {
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            Windows.Storage.ApplicationDataCompositeValue UsrPwd =
   (Windows.Storage.ApplicationDataCompositeValue)localSettings.Values["profile"];
            var id = UsrPwd["name"];
            return id.ToString();
        }
    }
}