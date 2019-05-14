using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace LSScraper.Api
{
    public class LSApi
    {

        public static List<Article> ScrapeLS()
        {
            var html = GetLSHtml();
            return GetPosts(html);
        }

        private static string GetLSHtml()
        {            
            using (var client = new HttpClient())
            {                
                var url = $"https://www.thelakewoodscoop.com/";
                var html = client.GetStringAsync(url).Result;
                return html;
            }
        }

        private static List<Article> GetPosts(string html)
        {
            var parser = new HtmlParser();
            IHtmlDocument document = parser.ParseDocument(html);
            var postDivs = document.QuerySelectorAll(".post");
            List<Article> articles = new List<Article>();
            foreach (var post in postDivs)
            {
                Article article = new Article();
                var href = post.QuerySelectorAll("a").First();
                article.Title = href.TextContent.Trim();
                article.Url = href.Attributes["href"].Value;

                var p = post.QuerySelectorAll("p").First();
                var image = p.QuerySelector("a");               
                article.ImageUrl = image.Attributes["href"].Value;

                var date = post.QuerySelectorAll(".postmetadata-top").First();
                if(date != null)
                {
                    article.DatePosted = date.TextContent;
                }

                articles.Add(article);
            }

            return articles;
        }
    }
}
