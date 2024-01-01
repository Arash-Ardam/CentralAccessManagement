using CrudDbAccess.Data;
using CrudDbAccess.Dtos;
using CrudDbAccess.Enums;

namespace OverviewAccess.Models
{
    public class DbViewModel
    {
        public string? Address { get; set; }
        public Zone? Zone{ get; set; }
        public IEnumerable<DatabaseDetails>? Accesses { get; set; }
        public CreateDbDto CreateDbDto { get; set; } 
    }
}
