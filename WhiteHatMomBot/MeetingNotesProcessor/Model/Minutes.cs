using Microsoft.AspNetCore.SignalR;
using System.ComponentModel.DataAnnotations;

namespace MeetingNotesProcessor.Model
{
    public class Minutes
    {
        public Minutes(long sessionId)
        {
            SessionId = sessionId;
            //Subject = subject;
            //Participants = participants;
            //MinuteOfMeeting = minuteOfMeeting;
            Subject = string.Empty;
            Created = DateTime.Now;
            Participants = String.Empty;
            MinuteOfMeeting = String.Empty;
            //Participants = new List<Participant>();
            //MinuteOfMeeting = new List<Note>();
        }

        [Key]
        public long SessionId { get; set; }
        public string Subject { get; set; }

        public string Participants { get; set; }

        public string MinuteOfMeeting { get; set; }

        //public List<Participant> Participants { get; set; }
        //public List<Note> MinuteOfMeeting { get; set; }
        public DateTime Created { get; set; }

        //- Subject/Topic
        //- Participant List
        //- Mom discussion
        //- Tasks(Action Items)
        //   - Sub Items
        //- Task with user Mapping
    }
}