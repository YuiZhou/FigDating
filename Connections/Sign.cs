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

        public static string getId()
        {
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            //Windows.Storage.StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

            Windows.Storage.ApplicationDataCompositeValue UsrPwd =
   (Windows.Storage.ApplicationDataCompositeValue)localSettings.Values["profile"];
            var id = UsrPwd["username"];
            return id.ToString();
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
            data.Add("stu_id", username);
            data.Add("password",password);
            var httpClient = new HttpClient();
            try {
                var queryString = new FormUrlEncodedContent(data);
                HttpResponseMessage response = await httpClient.PostAsync(Domain.getDomain() + "registration/", queryString);
                httpClient.Dispose();
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
            }catch{}
            return true;
        }

        public async Task<bool> signin(string username, string password)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("stu_id", username);
            data.Add("password", password);
            var httpClient = new HttpClient();
            try
            {
                var queryString = new FormUrlEncodedContent(data);
                HttpResponseMessage response = await httpClient.PostAsync(Domain.getDomain() + "login/", queryString);
                httpClient.Dispose();
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                Debug.WriteLine("$$$\n"+responseBody+"\n$$$");
                if (responseBody.Equals("1")) {
                    return false;
                }
                ////////////////////////////////
                loadProfile(responseBody);
            }
            catch { }
            // 需要重新刷新个人信息
            
            return true;
        }

        private void loadProfile(string json) {
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
            composite["id"] = item["stu_id"].GetString() ;
            composite["image"] = item["path"].GetString();
 
            localSettings.Values["profile"] = composite;
        }
    }
}