using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Services
{
	public class MailService : IMailService
	{
		public bool SendMail(string from, string to, string body)
		{
			return true;
		}
	}
}