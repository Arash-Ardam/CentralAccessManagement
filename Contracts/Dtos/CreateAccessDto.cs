using CrudDbAccess.Data;
using CrudDbAccess.Enums;

namespace CrudDbAccess.Dtos
{
    public class CreateAccessDto
    {
        public CreateDbDto From { get; set; }

        public int Port { get; set; }

        public CreateDbDto To { get; set; }

        public DatabaseDirection Direction { get; set; }
    }

    
}
