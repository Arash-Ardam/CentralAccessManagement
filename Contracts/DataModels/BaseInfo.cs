namespace CrudDbAccess.DataModels
{
    public class BaseInfo
    {
        public int id { get; set; }
        public string?  from { get; set; } = string.Empty;

        public string? to { get; set; } = string.Empty;

        public int port { get; set; }
    }
}
