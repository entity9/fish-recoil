using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Specialized;
using System.Net;

namespace Fatal
{
	public class dWebHook : IDisposable
	{
		private readonly WebClient dWebClient;
		private static NameValueCollection discordValues = new NameValueCollection();
		public string WebHook { get; set; }
		public string UserName { get; set; }
		public string ProfilePicture { get; set; }

		public dWebHook()
		{
			dWebClient = new WebClient();
		}


		public void SendMessage(string msgSend)
		{
			discordValues.Set("username", UserName);
			discordValues.Set("avatar_url", ProfilePicture);
			discordValues.Set("content", msgSend);

			dWebClient.UploadValues(WebHook, discordValues);
		}

		public void Dispose()
		{
			dWebClient.Dispose();
		}
	}
}
