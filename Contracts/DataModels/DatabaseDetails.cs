using CrudDbAccess.Enums;

namespace CrudDbAccess.Data
{
    public class DatabaseDetails
    {
        public Guid Id { get; set; }
        public string Address { get; set; } = string.Empty;
        public string? Name { get; set; }
        public Zone Zone { get; set; }
    }
}
