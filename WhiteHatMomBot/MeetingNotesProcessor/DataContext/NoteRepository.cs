using EFCoreInMemoryDbDemo;
using MeetingNotesProcessor.DataContext;
using MeetingNotesProcessor.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Reflection.Emit;
using System.Runtime.Intrinsics.Arm;
using static Azure.Core.HttpHeader;

namespace EFCoreInMemoryDbDemo
{
    public class NoteRepository : INoteRepository
    {
        public bool AddNote(long sessionId, string note)
        {
            if (sessionId <= 0)
                return false;

            try
            {
                using var context = new ApiContext();
                var minute = context.Minutes?.FirstOrDefault(x => x.SessionId == sessionId);

                //Create SessionId if doesn't exists
                if(minute == null)
                {
                    minute = new Minute(sessionId);
                    context.Minutes?.Add(minute);
                    context.SaveChanges();
                }

                if(!string.IsNullOrEmpty(note))
                {
                    minute.Notes.Add(note);
                    context.Minutes?.Update(minute);
                    context.SaveChanges();
                }

                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public bool DeleteNote(long sessionId, int noteIndex)
        {
            using var context = new ApiContext();
            var minute = context.Minutes?.FirstOrDefault(x => x.SessionId == sessionId);
            
            if (minute == null || minute.Notes.Count < noteIndex) 
                return false;
            
            minute.Notes.RemoveAt(noteIndex);
            context.Minutes?.Update(minute);
            context.SaveChanges();           

            return true;
        }

        public Minute? GetNote(long sessionId)
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

        public IEnumerable<Minute> GetNotes()
        {
            using var context = new ApiContext();
            var list = context.Minutes
                .ToList();
            return list;
        }

        public bool ClearSessions()
        {
            using var context = new ApiContext();
            //context.Notes.

            return true;
        }
    }
}