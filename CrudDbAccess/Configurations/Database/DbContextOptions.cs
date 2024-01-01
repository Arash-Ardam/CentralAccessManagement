namespace CrudDbAccess.Configurations.Database
{
    public class DbContextOptions
    {
        public bool isEnabled { get; set; }
        public bool isInMemory { get; set; }
        public string connectionString{ get; set; }
    }
}
