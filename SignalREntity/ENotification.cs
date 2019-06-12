using BaseEntity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignalREntity
{
    [Table("Notification")]
    public class ENotification : Entity
    {
        private DateTime? _eventDate;

        [Key]
        public int NotificationId { get; set; }
        public DateTime EventDate
        {
            get
            {
                if (_eventDate.HasValue)
                    return _eventDate.Value;
                else
                    return DateTime.UtcNow;
            }
            set
            {
                _eventDate = value;
            }
        }

        [MaxLength(50)]
        public string Sender { get; set; }
        [MaxLength(250)]
        public string Message { get; set; }
    }
}
