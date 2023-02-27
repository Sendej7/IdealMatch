namespace webapi.Settings
{
    public class MongoDB
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Connection { get; set; }
        public string DatabaseName { get; set; }
        public string ConnectionString
        {
            get
            {
                return $"mongodb://{Host}:{Port}";
            }
        }
    }
}
