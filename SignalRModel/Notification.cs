using System;

namespace SignalRModel
{
    public class Notification
    {
        private DateTime? _eventDate;
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
        public string Sender { get; set; }
        public string Message { get; set; }
    }
}
