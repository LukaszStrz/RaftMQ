using System;

namespace RaftMQ
{
    public class Message
    {
        public int Term { get; set; }

        public string SenderId { get; set; }

        public DateTime Timestamp { get; set; }

        public Message(string senderId, int term, DateTime timestamp)
        {
            SenderId = senderId;
            Term = term;
            Timestamp = timestamp;
        }
    }
}
