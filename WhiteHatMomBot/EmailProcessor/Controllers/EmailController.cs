using Microsoft.AspNetCore.Mvc;

namespace EmailProcessor.Controllers
{
    [ApiController]
    [Route("/[controller]/[action]")]
    public class EmailController : ControllerBase
    {
        private readonly ILogger<EmailController> _logger;

        public EmailController(ILogger<EmailController> logger)
        {
            _logger = logger;
        }

        [HttpPost(Name ="SendMail")]
        [ActionName("SendMail")]
        public void SendMail(EmailDetails emailDetails )
        {
            return;
        }
    }
}