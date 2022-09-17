using Azure;
using Azure.Communication.Email;
using Azure.Communication.Email.Models;
using MeetingNotesProcessor.Controllers;
using System.Net.Mail;

namespace MeetingNotesProcessor
{
    public class EmailSender
    {
        //private readonly ILogger<EmailSender> _logger;

        public static void SendMail( string subject)
        {
            string connectionString = "endpoint=https://bot-framework-communication-resource.communication.azure.com/;accesskey=VOO8D+jwocyGSmYaueFl2JBzZsIqDN8oZMu9hw5P6nFrcClFmpnUvqVtMdC7JUpQwATIZIKXL74gCsN3stNsMg==";
            EmailClient emailClient = new EmailClient(connectionString);


            //Replace with your domain and modify the content, recipient details as required

            EmailContent emailContent = new EmailContent($"{subject}");
            emailContent.PlainText = "Minutes of meeting test mail content";
            List<EmailAddress> emailAddresses = new List<EmailAddress> { new EmailAddress("vishal.gawai@gmail.com") { DisplayName = "Vishal Gawai" } };
            EmailRecipients emailRecipients = new EmailRecipients(emailAddresses);
            EmailMessage emailMessage = new EmailMessage("donotreply@2a497475-2a9d-43f3-9c0c-a8b07706d60e.azurecomm.net", emailContent, emailRecipients);
            var emailResult = emailClient.SendAsync(emailMessage, CancellationToken.None);

            //_logger.Info($"MessageId = {emailResult.MessageId}");


            ////Getting the status of the email

            //Response<SendStatusResult> messageStatus = null;
            //messageStatus = emailClient.GetSendStatus(emailResult.MessageId);
            ////Console.WriteLine($"MessageStatus = {messageStatus.Value.Status}");
            //TimeSpan duration = TimeSpan.FromMinutes(3);
            //long start = DateTime.Now.Ticks;
            //do
            //{
            //    messageStatus = emailClient.GetSendStatus(emailResult.MessageId);
            //    if (messageStatus.Value.Status != SendStatus.Queued)
            //    {
            //        //Console.WriteLine($"MessageStatus = {messageStatus.Value.Status}");
            //        break;
            //    }
            //    Thread.Sleep(10000);
            //    Console.WriteLine($"...");

            //} while (DateTime.Now.Ticks - start < duration.Ticks);


        }
    }
}
