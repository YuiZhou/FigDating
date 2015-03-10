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
                if (responseBody.Equals("{}")) {
                    return false;
                }
                ////////////////////////////////
                User.setProfile(responseBody);
            }
            catch { }
            // 需要重新刷新个人信息
            
            return true;
        }

        
    }
}