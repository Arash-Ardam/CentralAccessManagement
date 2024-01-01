using CrudDbAccess.Enums;

namespace CrudDbAccess.Dtos
{
    public class UpdateAccessDto
    {
        public Guid Id { get; set; }

        public UpdateDbDto From { get; set; }

        public int Port { get; set; }

        public UpdateDbDto To { get; set; }

        public DatabaseDirection Direction { get; set; }
    }
}
