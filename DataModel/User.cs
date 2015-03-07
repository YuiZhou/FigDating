namespace FigDating { 
    public class User{
        private string id { get; set; }
        private string name { get; set; }
        private string gender { get; set; }
        private string group { get; set; }
        private int age { get; set; }
        private string grade { get; set; }
        public static User user {get; set; }
        private User(string id) { 
            // 根据特有的ID可以锁定一个user
        }

        public static void setUser(string ids, string names, string genders, string groups, string grades, 
            string ages) {
                //id = ids;


        }


    }
}