using Microsoft.AspNetCore.SignalR;
using System.ComponentModel.DataAnnotations;

namespace MeetingNotesProcessor.Model
{
    public class Note
    {
        public Note(int id, string message)
        {
            Id = id;
            Message = message;
        }

        [Key]
        public int Id { get; set; }
        public string Message { get; set; }     
    }
}
