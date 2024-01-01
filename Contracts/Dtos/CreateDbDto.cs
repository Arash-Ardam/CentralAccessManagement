using CrudDbAccess.Enums;

namespace CrudDbAccess.Dtos
{
    public class CreateDbDto
    {
        public string Address { get; set; } = string.Empty;
        
        public Zone Zone { get; set; }
        public string? Name { get; set; }
    }
}
