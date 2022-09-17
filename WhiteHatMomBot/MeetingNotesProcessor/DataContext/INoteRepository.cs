using MeetingNotesProcessor.Model;

namespace MeetingNotesProcessor.DataContext
{
    public interface INoteRepository
    {
        bool Init(long sessionId, string subject, List<Participant> participants);
        bool AddNote(long sessionId, string minute);
        bool DeleteNote(long sessionId, int index);        
        Minutes? GetNote(long sessionId);
        bool EmailNote(long sessionId);
        IEnumerable<Minutes> GetNotes();
        bool ClearSessions();
    }
}