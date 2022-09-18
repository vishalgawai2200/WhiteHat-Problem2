using MeetingNotesProcessor.DataContext;
using MeetingNotesProcessor.Model;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Runtime.CompilerServices;

namespace MeetingNotesProcessor.Controllers
{
    [ApiController]
    [Route("/[controller]/[action]")]
    public class NotesController : ControllerBase
    {
        private readonly ILogger<NotesController> _logger;

        private readonly INoteRepository _noteRepository;

        private readonly IEmailSender _emailSender;

        public NotesController(INoteRepository noteRepository, IEmailSender emailSender, ILogger<NotesController> logger)
        {
            _logger = logger;
            _noteRepository = noteRepository;
            _emailSender = emailSender;
        }

        [HttpPost]
        [ActionName("AddNote")]
        public ActionResult AddNote(string sessionId, string note)
        {
            try
            {
                return Ok(_noteRepository.AddNote(sessionId, note));
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        [ActionName("DeleteNote")]
        public ActionResult<bool> DeleteNote(string sessionId, int index)
        {
            try
            {
                return Ok(_noteRepository.DeleteNote(sessionId, index));
            }
            catch (Exception ex)
            {

                return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [ActionName("GetMoM")]
        public ActionResult<Minute> GetMoM(string sessionId)
        {
            try
            {
                return Ok(_noteRepository.GetMoM(sessionId));
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }          
        }

        [HttpPost]
        [ActionName("EmailMOM")]
        public ActionResult<bool> EmailMoM(string sessionId)
        {
            try
            {
                var mom = _noteRepository.GetMoM(sessionId);
                mom.Subject = $"WhiteHatBot-MOM-{sessionId}-{DateTime.Now}";
                return Ok(_emailSender.SendMail(mom));
            }
            catch (Exception ex)
            {
                return Problem(detail:ex.Message, statusCode: StatusCodes.Status500InternalServerError); 
            }
        }

        [HttpPost]
        [ActionName("AddParticipants")]
        public ActionResult<bool> AddParticipants(string sessionId, string participants)
        {
            try
            {
                if (string.IsNullOrEmpty(participants))
                    throw new ArgumentNullException(nameof(participants));

                var mom = _noteRepository.GetMoM(sessionId);
                if (mom == null)
                    throw new Exception($"Unable to find minutes for session id{sessionId}");

                mom.ParticipantsColSeparated=participants;
              _noteRepository.UpdateMom(mom);
                return true;
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }


        //[HttpGet]
        //[ActionName("GetNotes")]
        //public ActionResult<IEnumerable<Minute>> GetNotes()
        //{
        //    return Ok(_noteRepository.GetNotes());
        //}

        [HttpPost]
        [ActionName("SendTestMail")]
        public void SendTestMail(string subject)
        {
            _emailSender.SendTestMail(subject);

        }



    }
}