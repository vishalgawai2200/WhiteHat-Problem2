

namespace EmailProcessor
{
    public interface IEmailSender
    {
        bool SendMail(Minute mom);
        bool SendTestMail(string subject);
    }
}