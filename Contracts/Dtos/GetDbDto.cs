using CrudDbAccess.Enums;

namespace CrudDbAccess.Dtos
{
    public class GetDbDto
    {
        public Guid Id { get; set; }
        public string Address { get; set; } = string.Empty;
        public Zone Zone { get; set; }
        public string Name { get; set; }
    }
}
