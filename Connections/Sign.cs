using System.Collections.Generic;
using System.Net.Http;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace FigDating {
    public class Domain{
        public static string getDomain(){
            return "http://198.52.103.223/";
        }    
    }
    public class Sign {
        private static Sign sign = null;
        private Sign() { }
        public static Sign getSign(){
            if (sign == null) {
                sign = new Sign();
            }
            return sign;
        }

        public async Task<bool> signup(string username, string password)
        {
            Dictionary<string,string> data = new Dictionary<string,string>();
            var httpClient = new HttpClient();
            try {
                var queryString = new FormUrlEncodedContent(data);
                HttpResponseMessage response = await httpClient.PostAsync(Domain.getDomain() + "registration/", queryString); 
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
            }catch{}
            return true;
        }

        public bool signin(string username, string password)
        {
            // 需要重新刷新个人信息
            loadProfile(username);
            return true;
        }

        private async void loadProfile(string username) {
            username = "1";

            HttpClient client = new HttpClient();
            HttpResponseMessage responseHttp = await client.GetAsync(Domain.getDomain() +"user/"+ username +"/profile/");
            client.Dispose();

            Task<string> message = responseHttp.Content.ReadAsStringAsync();
            string json = await message;
            JsonValue response = JsonValue.Parse(json);
            JsonObject item = response.GetObject();

            
            item = item["user"].GetObject();

            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            Windows.Storage.ApplicationDataCompositeValue composite = new Windows.Storage.ApplicationDataCompositeValue();
            composite["username"] = username;
            composite["name"] = item["username"].GetString();
            composite["grade"] = item["grade"].GetString();
            composite["gender"] = item["gender"].GetString();
            composite["chance"] = item["chance"].GetNumber();
            composite["college"] = item["college"].GetString();
            composite["birth_year"] = item["birth_year"].GetString();
            composite["id"] = item["stu_id"].GetString() ;
 
            localSettings.Values["profile"] = composite;
        }
    }
}