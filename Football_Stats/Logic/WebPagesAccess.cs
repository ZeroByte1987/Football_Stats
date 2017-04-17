namespace Football_Stats.Logic
{
	using System;
	using System.IO;
	using System.Net;
	using HtmlDocument = HtmlAgilityPack.HtmlDocument;


	public static class WebPagesAccess
	{
		public static string		GetHtmlText(string url)
		{
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.UserAgent = @"Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.8.0.4) Gecko/20060508 Firefox/1.5.0.4";

			try
			{
				var myHttpWebResponse = (HttpWebResponse)request.GetResponse();

				var stream = myHttpWebResponse.GetResponseStream();
				if (stream == null) throw new Exception("Cannot access the requested page: " + url);

				var reader = new StreamReader(stream);
				var html = reader.ReadToEnd();
				myHttpWebResponse.Close();
				return html;
			}
			catch
			{				
				return null;
			}
		}


		public static HtmlDocument	GetHtmlBody(string url)
		{
			var htmlText = GetHtmlText(url);
			if (htmlText == null) return null;

			var htmlDoc = new HtmlDocument { OptionFixNestedTags = true };
			htmlDoc.LoadHtml(htmlText);
			return htmlDoc;
		}
	}
}