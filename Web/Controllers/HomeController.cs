using Repo.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;
using Web.Services;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMailService _mail;
        private readonly IMessagesRepository _repo;

        public HomeController(IMailService mail, IMessagesRepository repo)
        {
            _mail = mail;
            _repo = repo;
        }

        public ActionResult Index()
        {
            var results = _repo.GetTopics();
            return View(results);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Contact(ContactModel model)
        {

            var isSendMail = _mail.SendMail(model.Name, model.Email, model.Message);
            if (isSendMail)
                ViewBag.MailSent = true;

            return View();
        }
    }
}