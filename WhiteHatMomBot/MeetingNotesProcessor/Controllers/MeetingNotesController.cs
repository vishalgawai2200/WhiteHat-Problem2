using Microsoft.AspNetCore.Mvc;

namespace MeetingNotesProcessor.Controllers
{
    [ApiController]
    [Route("/[controller]/[action]")]
    public class MeetingNotesController : ControllerBase
    {
       private readonly ILogger<MeetingNotesController> _logger;

        private static Dictionary<int, string> _notes;
       

        public MeetingNotesController(ILogger<MeetingNotesController> logger)
        {
            _logger = logger;
            _notes = new Dictionary<int, string>();
        }

        [HttpGet]
        [ActionName("GetNotes")]
        public IEnumerable<MeetingNote> GetNotes()
        {            
            foreach(var note in _notes) 
            {
                yield return new MeetingNote
                {
                    Id = note.Key,
                    Note = note.Value
                };
            }
        }


        [HttpPost]
        [ActionName("AddNotes")]
        public void AddNote(string msg)
        {
            int id = _notes.Count() + 1;
            _notes.Add(id, msg);

            return;
            
        }

    }
}