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
                HttpResponseMessage response = await httpClient.PostAsync(Domain.getDomain() + "user/" + Domain.getId() + "/" , queryString);
                httpClient.Dispose();
                response.EnsureSuccessStatusCode();
                //string responseBody = await response.Content.ReadAsStringAsync();
                //Debug.WriteLine("###\n"+responseBody+"\n###");
                //if (responseBody.Equals("1"))
                //{
                //    return false;
                //}
            }
            catch
            {
            }        
        }
    }
}