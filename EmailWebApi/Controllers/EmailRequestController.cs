using EmailWebApi.Data;
using EmailWebApi.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace EmailWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailRequestController : Controller
    {
        [HttpPost("send")]
        public IActionResult SendEmail(SendEmail sendEmail)
        {
           var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("your-email@gmail.com"));
            email.To.Add(MailboxAddress.Parse(sendEmail.to));
            email.Subject = sendEmail.subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Plain)
            {
                Text = sendEmail.body
            };

            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate("zibabeachtech2@gmail.com", "@Techpeace23");
            smtp.Send(email);
            smtp.Disconnect(true);
            return Ok(new { message = "Email sent successfully" });
        }
    }
}
