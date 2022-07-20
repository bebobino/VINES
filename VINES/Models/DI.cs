namespace VINES.Models
{
    public class AppSettings
    {
        public static ConnectionStrings ConnectionStrings { get; set; }
        public static Smtp Smtp { get; set; }
        public static Site Site { get; set; }
    }

    public class ConnectionStrings
    {
        public string DefaultConnection { get; set; }
    }
    public class Smtp
    {
        public string Server { get; set; }
        public int Port { get; set; }
        public string FromAddress { get; set; }
        public string Password { get; set; }
    }

    public class Site
    {
        public string Url { get; set; }
    }

    
}
