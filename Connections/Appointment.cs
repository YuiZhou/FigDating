using System.Threading.Tasks;
using System.Net.Http;
using System.Diagnostics;
using System;
using System.Collections.Generic;


namespace FigDating
{
    public class Appointment
    {
        private static Appointment appointment = null;
        private Appointment() { }
        public static Appointment getAppointment()
        {
            if (appointment == null)
            {
                appointment = new Appointment();
            }
            return appointment;
        }

        public async Task<bool> addNew(string id, string start, string end, string content, string hint)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();

            data.Add("user_id", id);
            data.Add("begin", start.Substring(0, start.Length - 1));
            data.Add("end", end.Substring(0, end.Length - 1));
            data.Add("content", content);
            data.Add("comment", hint);

            Debug.WriteLine("###\n" + id + start.Substring(0, start.Length - 1) + end.Substring(0, end.Length - 1) + content + hint + "\n###");
            var httpClient = new HttpClient();
            try
            {
                var queryString = new FormUrlEncodedContent(data);
                HttpResponseMessage response = await httpClient.PostAsync(Domain.getDomain() + "appointments/", queryString);
                httpClient.Dispose();
                //response.EnsureSuccessStatusCode();
                //string responseBody = await response.Content.ReadAsStringAsync();
                //Debug.WriteLine("###\n"+responseBody+"\n###");
                //if (responseBody.Equals("1"))
                //{
                //    return false;
                //}
            }
            catch { }
            // 需要重新刷新个人信息

            return true;
        }

        public async Task<string> getNews()
        {
            HttpClient client = new HttpClient();
            try
            {
                HttpResponseMessage responseHttp = await client.GetAsync(Domain.getDomain() + "appointments/latest/");
                client.Dispose();
                Task<string> message = responseHttp.Content.ReadAsStringAsync();
                return await message;
            }
            catch (Exception)
            {
                return "";
            }
        }
    }


}