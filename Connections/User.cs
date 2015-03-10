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
        private static int i = 0;
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
                fileContent.Add(new Windows.Web.Http.HttpStringContent(getId().ToString()), "user_id");
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
                HttpResponseMessage responseHttp = await client.GetAsync(Domain.getDomain() + "user/" + User.getId() + "/notifications/?"+(i++));
                client.Dispose();
                Task<string> message = responseHttp.Content.ReadAsStringAsync();
                return await message;

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

        public static void setProfile(string json)
        {
            // username = "1";
            JsonValue response = JsonValue.Parse(json);
            JsonObject item = response.GetObject();


            item = item["user"].GetObject();

            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            Windows.Storage.ApplicationDataCompositeValue composite = new Windows.Storage.ApplicationDataCompositeValue();
            composite["username"] = item["user_id"].GetNumber();
            composite["name"] = item["username"].GetString();
            composite["grade"] = item["grade"].GetString();
            composite["gender"] = item["gender"].GetString();
            composite["chance"] = item["chance"].GetNumber();
            composite["college"] = item["college"].GetString();
            composite["birth_year"] = item["birth_year"].GetString();
            composite["id"] = item["stu_id"].GetString();
            composite["image"] = item["path"].GetString();

            localSettings.Values["profile"] = composite;
        }

        public async void loadProfile() {
            HttpClient client = new HttpClient();
            HttpResponseMessage responseHttp = await client.GetAsync(Domain.getDomain() + "user/" + User.getId() + "/?" + (i++));
            client.Dispose();
            Task<string> message = responseHttp.Content.ReadAsStringAsync();
            setProfile(await message);
        }
    }
}