using EFCoreInMemoryDbDemo;
using MeetingNotesProcessor.DataContext;
using MeetingNotesProcessor.Model;
using Microsoft.EntityFrameworkCore;

namespace EFCoreInMemoryDbDemo
{
    public class NoteRepository : INoteRepository
    {
        public NoteRepository()
        {
            //using (var context = new ApiContext())
            //{
            //    var notes = new List<Note>
            //    {
            //        new Note
            //        {
            //            Id= 1,
            //            Participants = "Saurabh, Vishal" ,
            //            Subject = "Imp thing to consider to participate in Hackathon",
            //            //Participants = new string[] { "Saurabh" , "Vishal" },
            //            //MinuteOfMeeting = new string[] { "Step 1 ", "Step 2", "Step 3" }
            //            MinuteOfMeeting = "Step 1, Step 2, Step 3"
            //        }
            //    };
            //    context.Notes.AddRange(notes);
            //    context.SaveChanges();
            //}
        }

        public bool Init(long sessionId, string subject, List<Participant> participants)
        {
            using var context = new ApiContext();
            //long maxId = context.Notes.Max(x => x.SessionId);            
            context.Minutes?.Add(new Minutes(sessionId));
            context.SaveChanges();

            return true;
        }

        public bool AddNote(long sessionId, string minute)
        {
            using var context = new ApiContext();
            var note = context.Minutes?.FirstOrDefault(x => x.SessionId == sessionId);
            note?.MinuteOfMeeting.Add(new Note( 1, minute ));
            //context.Notes.Add(new Note { SessionId = maxId + 1, Subject = subject, MinuteOfMeeting = minutes, Created = DateTime.Now, Participants = "Saurabh" });
            context.SaveChanges();
            
            return true;
        }

        public Minutes? GetNote(long sessionId)
        {
            using var context = new ApiContext();
            var note = context.Minutes?.FirstOrDefault(x => x.SessionId == sessionId);
            return note;
        }

        public bool EmailNote(long sessionId)
        {
            using var context = new ApiContext();
            var note = context.Minutes?.FirstOrDefault(x => x.SessionId == sessionId);
            // Call Email Service

            return true;
        }

        public bool DeleteNote(long sessionId, int index)
        {
            using var context = new ApiContext();
            var note = context.Minutes?.FirstOrDefault(x => x.SessionId == sessionId);
            note?.MinuteOfMeeting.RemoveAt(index);

            return true;
        }

        public bool ClearSessions()
        {
            using var context = new ApiContext();
            //context.Notes.

            return true;
        }
        public IEnumerable<Minutes> GetNotes()
        {
            using var context = new ApiContext();
            var list = context.Minutes
                .ToList();
            return list;
        }
    }
}