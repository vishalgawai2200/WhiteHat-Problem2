using MeetingNotesProcessor.Model;

namespace MeetingNotesProcessor
{
    public interface IEmailSender
    {
        bool SendMail(Minute mom);
        bool SendTestMail(string subject);
    }
}