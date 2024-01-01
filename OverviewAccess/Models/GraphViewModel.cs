using CrudDbAccess.Data;
using CrudDbAccess.Enums;

namespace OverviewAccess.Models
{
    public class GraphViewModel
    {
        public IEnumerable<BaseAccessData>? Accesses { get; set; }
        public Zone? SelectedZone { get; set; }
        public string? Address { get; set; }
    }
}
