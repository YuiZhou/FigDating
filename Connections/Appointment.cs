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
        private static int i = 0;
        private Appointment() { }
        public static Appointment getAppointment()
        {
            if (appointment == null)
            {
                appointment = new Appointment();
            }
            return appointment;
        }
        
        public async Task<bool> addNew(string start, string end, string content, string hint)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();

            data.Add("user_id", Domain.getId());
            data.Add("begin", start.Substring(0, start.Length - 1));
            data.Add("end", end.Substring(0, end.Length - 1));
            data.Add("content", content);
            data.Add("comment", hint);

            var httpClient = new HttpClient();
            try
            {
                var queryString = new FormUrlEncodedContent(data);
                HttpResponseMessage response = await httpClient.PostAsync(Domain.getDomain() + "appointments/", queryString);
                httpClient.Dispose();
                response.EnsureSuccessStatusCode();
                //string responseBody = await response.Content.ReadAsStringAsync();
                //Debug.WriteLine("###\n"+responseBody+"\n###");
                //if (responseBody.Equals("1"))
                //{
                //    return false;
                //}
            }
            catch {
                return false;            
            }

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

        public async Task<string> getComment(string appointmentId)
        {
            HttpClient client = new HttpClient();
            try
            {
                HttpResponseMessage responseHttp = await client.GetAsync(Domain.getDomain() + "appointments/"+appointmentId+"/comment/?"+ (i++));
                client.Dispose();
                Task<string> message = responseHttp.Content.ReadAsStringAsync();
                return await message;
            }
            catch (Exception)
            {
                return "";
            }
        }


         public async Task<bool> addComment(string content, string appointid)
         {
             Dictionary<string, string> data = new Dictionary<string, string>();

             data.Add("user_id", Domain.getId());
             data.Add("app_id", appointid);
             data.Add("comment", content);

             var httpClient = new HttpClient();
             try
             {
                 var queryString = new FormUrlEncodedContent(data);
                 HttpResponseMessage response = await httpClient.PostAsync(Domain.getDomain() + "appointments/comment/", queryString);
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
                 return false;
             }

             return true;
         }
    }


}