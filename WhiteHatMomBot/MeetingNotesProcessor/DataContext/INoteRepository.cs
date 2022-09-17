using MeetingNotesProcessor.Model;

namespace MeetingNotesProcessor.DataContext
{
    public interface INoteRepository
    {
        bool AddNote(long sessionId, string note);
        bool DeleteNote(long sessionId, int noteIndex);        
        Minute? GetNote(long sessionId);
        bool EmailNote(long sessionId);
        IEnumerable<Minute> GetNotes();
        bool ClearSessions();// Test
    }
}