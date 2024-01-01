using CrudDbAccess.Enums;

namespace CrudDbAccess.Dtos
{
    public class GetAccessDto
    {
        public Guid Id { get; set; }

        public GetDbDto From { get; set; }

        public int Port { get; set; }

        public GetDbDto To { get; set; }

        public DatabaseDirection Direction { get; set; }
    }
}

