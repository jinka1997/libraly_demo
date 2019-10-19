using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using CommonLibrary.Web;

namespace CommonLibrary.CallWebApis
{
    public class NationalDietLibraryApi
    {
        public BookApiInfo ExecuteByIsbn(string isbn)
        {
            var url = string.Format("http://iss.ndl.go.jp/api/opensearch?isbn={0}", isbn);
            var xmlText = WebApiUtil.GetResponseText(url);

            BookApiInfo info = new BookApiInfo
            {
                ApiName = "NationalDietLibraryApi",
                ResponseText = xmlText,
                ResponseDatetime = DateTime.Now
            };

            if (!string.IsNullOrEmpty(xmlText))
            {
                var doc = new XmlDocument();
                doc.LoadXml(xmlText);

                var items = doc.GetElementsByTagName("item");

                foreach (XmlNode item in items)
                {
                    var bookItem = new BookApiInfo.Item();
                    foreach (XmlNode node in item.ChildNodes)
                    {
                        switch (node.Name)
                        {
                            case "dc:title":
                                bookItem.Title = node.InnerText;
                                break;
                            case "dcndl:volume":
                                bookItem.Subtitle = node.InnerText;
                                break;
                            case "dc:publisher":
                                bookItem.Publisher = node.InnerText;
                                break;
                            case "dc:identifier":
                                foreach (XmlAttribute attr in node.Attributes)
                                {
                                    if (attr.InnerText == "dcndl:ISBN")
                                    {
                                        bookItem.Isbn13 = node.InnerText;
                                    }
                                }
                                break;
                            case "dc:creator":
                                bookItem.Authors = node.InnerText;
                                break;
                            case "pubDate":
                                bookItem.PublishedDate = node.InnerText;
                                break;
                            case "dc:subject":
                                if (node.Attributes.Count == 0)
                                {
                                    bookItem.Categories = node.InnerText;
                                }
                                break;
                        }
                    }
                    info.BookItems.Add(bookItem);
                }
            }
            return info;
        }
    }
}
