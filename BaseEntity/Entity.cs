using System;

namespace BaseEntity
{
    //This should be in a separate NuGet Package
    public class Entity
    {
        private DateTime? _createdDateUtc;

        public DateTime CreatedDateUtc
        {
            get
            {
                if (_createdDateUtc.HasValue)
                    return _createdDateUtc.Value;
                else
                    return DateTime.Now;
            }
            set
            {
                _createdDateUtc = value;
            }
        }
        public DateTime? UpdatedDateUtc { get; set; }

        public int CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
