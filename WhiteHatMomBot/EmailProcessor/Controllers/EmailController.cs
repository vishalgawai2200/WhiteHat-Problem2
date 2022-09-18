using Microsoft.AspNetCore.Mvc;

namespace EmailProcessor.Controllers
{
    [ApiController]
    [Route("/[controller]/[action]")]
    public class EmailController : ControllerBase
    {
        private readonly ILogger<EmailController> _logger;
        private readonly IEmailSender _emailSender;

        public EmailController(ILogger<EmailController> logger, IEmailSender emailSender)
        {
            _logger = logger;
            _emailSender = emailSender;
        }

        [HttpPost(Name ="SendMail")]
        [ActionName("SendMail")]
        public ActionResult<bool> SendMail(Minute mom )
        {
            try
            {
                return Ok(_emailSender.SendMail(mom));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, null);
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost(Name = "SendTestMail")]
        [ActionName("SendTestMail")]
        public ActionResult<bool> SendTestMail(string subject )
        {
            try
            {
                return Ok(_emailSender.SendTestMail(subject));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, null);
                return Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }
    }
}