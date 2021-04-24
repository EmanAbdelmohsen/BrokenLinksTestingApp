using BrokenLinksTesting.WPF.Common;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace BrokenLinksTesting.WPF
{
    /// <summary>
    /// Service to check links nested in a web page content
    /// </summary>
    public static class WebPageLinkChecker
    {
        /// <summary>
        /// Get all links nested in a web page after being tested
        /// </summary>
        /// <param name="url">web page URL</param>
        /// <returns></returns>
        public static List<Link> GetAllLinksFromPageUrl(string url)
        {
            List<Link> links = new List<Link>();

            //1- get html document from given url
            HtmlWeb hw = new HtmlWeb();
            HtmlDocument doc = hw.Load(new Uri(url));

            //2- fetch all links from html
            var urls = ExtractLinksFromHtmlDoc(doc);

            //3- iterate on links to get the status
            foreach (string u in urls)
            {
                int status = GetStatusCodeFromHttpResponse(u);

                var link = new Link
                {
                    Id = RandomNumberGenerator.GenerateRandomNumber(),
                    RequestMethod = "GET",
                    ResponseCode = status.ToString(),
                    URL = u,
                    RequestDate = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString(),
                };

                links.Add(link);
            }

            return links;
        }
        /// <summary>
        /// Gets all links from a given web page html source
        /// </summary>
        /// <param name="doc">html source document</param>
        /// <returns>list of links</returns>
        static List<string> ExtractLinksFromHtmlDoc(HtmlDocument doc)
        {
            List<string> links = new List<string>();

            //fetch links nodes from the html document
            try
            {
                links = doc.DocumentNode
                       .SelectNodes("//a[@href]")
                       .Select(node => node.Attributes["href"]?.Value)
                       .ToList();
            }
            catch (ArgumentNullException)
            { }

            return links;
        }

        /// <summary>
        /// Gets status code from an http response for a given url
        /// </summary>
        /// <param name="url">string url to make an http request for</param>
        /// <returns>status code</returns>
        static int GetStatusCodeFromHttpResponse(string url)
        {
            //create http request out from the given url
            HttpWebRequest request;
            try
            {
                request = (HttpWebRequest)WebRequest.Create(url);
            }
            catch
            {
                return 404;
            }

            HttpStatusCode statusCode;

            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException we)
            {
                response = (HttpWebResponse)we.Response;
            }

            statusCode = response.StatusCode;

            return (int)statusCode;
        }

    }
}
