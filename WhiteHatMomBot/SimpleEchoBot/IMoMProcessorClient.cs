using System.Collections.Generic;

namespace SimpleEchoBot
{
    public interface IMoMProcessorClient
    {
        List<string> GetMinutesOfMeeting();
        bool AddNote(string note);
        bool DeleteNode(int index);
        bool AddParticipants(string participants);
        bool SendMail();
    }
}
