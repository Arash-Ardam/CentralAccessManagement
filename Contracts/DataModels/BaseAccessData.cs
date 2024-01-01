using CrudDbAccess.Enums;

namespace CrudDbAccess.Data
{
    public class BaseAccessData
    {
        public Guid Id { get; set; }

        public DatabaseDetails From { get; set; }

        public int Port { get; set; }

        public DatabaseDetails To { get; set; }

        public DatabaseDirection Direction { get; set; }
    }
}
