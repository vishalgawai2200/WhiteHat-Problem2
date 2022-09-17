using MeetingNotesProcessor.DataContext;
using MeetingNotesProcessor.Model;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace MeetingNotesProcessor.Controllers
{
    [ApiController]
    [Route("/[controller]/[action]")]
    public class NotesController : ControllerBase
    {
        private readonly ILogger<NotesController> _logger;

        private readonly INoteRepository _noteRepository;


        public NotesController(INoteRepository noteRepository, ILogger<NotesController> logger)
        {
            _logger = logger;
            _noteRepository = noteRepository;
        }

        [HttpPost]
        [ActionName("AddNote")]
        public ActionResult AddNote(long sessionId, string note)
        {            
            return Ok(_noteRepository.AddNote(sessionId, note));
        }

        [HttpDelete]
        [ActionName("DeleteNote")]
        public ActionResult<bool> DeleteNote(long sessionId, int index)
        {            
            return Ok(_noteRepository.DeleteNote(sessionId, index));
        }

        [HttpGet]
        [ActionName("GetNote")]
        public ActionResult<Minute> GetNote(long sessionId)
        {
            return Ok(_noteRepository.GetNote(sessionId));            
        }

        [HttpGet]
        [ActionName("EmailNote")]
        public ActionResult<bool> EmailNote(long sessionId)
        {            
            return Ok(_noteRepository.EmailNote(sessionId));            
        }

        [HttpGet]
        [ActionName("GetNotes")]
        public ActionResult<IEnumerable<Minute>> GetNotes()
        {
            return Ok(_noteRepository.GetNotes());
        }

        [HttpPost]
        [ActionName("EmailMinutesOfMeeting")]
        public void EmailMinutesOfMeeting(string subject)
        {
          EmailSender.SendMail(subject);

        }



    }
}