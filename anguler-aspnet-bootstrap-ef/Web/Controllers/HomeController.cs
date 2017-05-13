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

		public HomeController(IMailService mail)
		{
			_mail = mail;
		}

		public ActionResult Index()
		{
			return View();
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

			if(_mail.SendMail(model.Name, model.Email, "TestMessage"))
			{
				ViewBag.MailSent = true;
			}
			return View();
		}
	}
}