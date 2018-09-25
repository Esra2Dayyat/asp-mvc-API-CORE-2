using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DontCoreApiAuthantaication.Data;
using System.Text;
using System.Net.Mail;
using Microsoft.Extensions.Hosting;

namespace DontCoreApiAuthantaication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddUserController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
     
        private readonly UserContext _context;
        public AddUserController(UserContext context ,IHostingEnvironment env)
        {
            _hostingEnvironment = env;
            _context = context;
        }


        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {  
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }
          _context.Users.Add(user);
            await _context.SaveChangesAsync();
            BuildEmail(user.ID);
            return
                Ok("Riegister successfuly");
        }

        public  void BuildEmail(int iD)
        {
          
            string contentRootPath = _hostingEnvironment.ContentRootPath;

            string body = System.IO.File.ReadAllText(contentRootPath+("/BuildEmail/") + "Text" + ".cshtml"); //path  View
            User RegInfo = _context.Users.Where(x => x.ID == iD).FirstOrDefault(); // get  user  from DataBase

            var url = "https://localhost:44303" + "/Confirm/Confirm/" + iD;
            body = body.Replace("@ViewBag.ConfirmationLink", url);
            body = body.ToString();
            BuildEmail("Your Account Is Successfully Created", body, RegInfo.Email);
        }
        public static void BuildEmail(string subjectText, string bodyText, string sendTo)
        {

            string from, to, bcc, cc, subject, body;
            from = "esraa.dayyat@gmail.com";
            to = sendTo.Trim();
            bcc = "";
            cc = "";
            subject = subjectText;
            StringBuilder sb = new StringBuilder();
            sb.Append(bodyText);
            body = sb.ToString();
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(from);
            mail.To.Add(new MailAddress(to));
            if (!string.IsNullOrEmpty(bcc))
            {
                mail.Bcc.Add(new MailAddress(bcc));
            }
            if (!string.IsNullOrEmpty(cc))
            {
                mail.CC.Add(new MailAddress(cc));
            }
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            SendEmail(mail);

        }

       
  
        public static void SendEmail(MailMessage mail)
        {
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new System.Net.NetworkCredential("esraa.dayyat@gmail.com", "311091esraa");
            try
            {
                client.Send(mail);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

  


        [HttpGet]
        public IEnumerable<User> Getuser()
        {
            return _context.Users;
        }

        [HttpGet("{id}")]
        public  async Task<IActionResult> Getuser( [FromRoute] int id) {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            User user = await _context.Users.FindAsync( id);

            if (user == null)
            {
                return NotFound();
              }
            return Ok(user);

        }


    

    }
}