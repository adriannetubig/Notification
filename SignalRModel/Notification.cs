using System;

namespace SignalRModel
{
    public class Notification
    {
        public DateTime EventDate => DateTime.Now;
        public string Sender { get; set; }
        public string Message { get; set; }
    }
}
