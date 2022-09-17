using Microsoft.AspNetCore.SignalR;
using System.ComponentModel.DataAnnotations;

namespace MeetingNotesProcessor.Model
{
    public class Participant
    {
        public Participant(int id, string name)
        {
            Id = id;
            Name = name;
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }     
    }
}
