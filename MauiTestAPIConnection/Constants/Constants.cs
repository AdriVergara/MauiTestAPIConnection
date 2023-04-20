namespace MauiTestAPIConnection
{
    public static class Constants
    {
        // URL of REST service
        //public static string RestUrl = "https://dotnetmauitodorest.azurewebsites.net/api/todoitems/{0}";

        // URL of REST service (Android does not use localhost)
        // Use http cleartext for local deployment. Change to https for production

        //tested on android using ngrok
        public static string LocalhostUrl = DeviceInfo.Platform == DevicePlatform.Android ? "80cd-95-19-173-73.ngrok-free.app" : "localhost";
        public static string Scheme = DeviceInfo.Platform == DevicePlatform.Android ? "http" : "https"; // or http
        public static string Port = DeviceInfo.Platform == DevicePlatform.Android ? "10.0.0.2" : "7240";
        public static string RestUrl = DeviceInfo.Platform == DevicePlatform.Android ? $"{Scheme}://{LocalhostUrl}/{{0}}" : $"{Scheme}://{LocalhostUrl}:{Port}/{{0}}";
    }
}
