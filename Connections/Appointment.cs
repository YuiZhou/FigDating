using System.Threading.Tasks;
using System.Net.Http;
using System.Diagnostics;
using System;

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

        public bool addNew(string id, string start, string end, string content, string hint)
        {

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