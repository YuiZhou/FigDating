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

        public bool addNew(string id, string start, string end, string content, string hint) {
            
            return true;
        }
    }
}