using CrudDbAccess.Data;
using CrudDbAccess.Dtos;
using CrudDbAccess.Enums;

namespace OverviewAccess.Models
{
    public class AccessViewModel
    {
        public Zone? Zone { get; set; }
        public string? Address { get; set; }
        public IEnumerable<BaseAccessData>? Accesses { get; set; }

        public CreateAccessDto? CreateAccessDto { get; set; }
    }
}
