using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace SimpleEchoBot
{
    public class Minute
    {
        public Minute(string sessionId)
        {
            SessionId = sessionId;
            Subject = string.Empty;
            ParticipantsColSeparated = string.Empty;
            Notes = new List<string>();
            Created = DateTime.Now;
        }

        [Key]
        public string SessionId { get; set; }
        public string Subject { get; set; }
        public string ParticipantsColSeparated { get; set; }
        public List<string> Notes { get; set; }
        public DateTime Created { get; set; }
    }
}
