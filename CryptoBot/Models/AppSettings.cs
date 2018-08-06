namespace CryptoBot.Models
{
    public static class AppSettings
    {
        public static string Url { get; set; }  = "https://serviceurl/{0}";

        public static string Name { get; set; } = "appName";

        public static string Key { get; set; } = "appKey";
    }
}