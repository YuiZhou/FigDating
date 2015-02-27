namespace FigDating {
    public class Sign {
        private static Sign sign = null;
        private Sign() { }
        public static Sign getSign(){
            if (sign == null) {
                sign = new Sign();
            }
            return sign;
        }

        public bool signup(string username, string password)
        {
            return true;
        }

        public bool signin(string username, string password)
        {
            // 需要重新刷新个人信息
            return true;
        }
    }
}