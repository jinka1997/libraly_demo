using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using CommonLibrary.Web;

namespace CommonLibrary.CallWebApis
{
    public class GoogleBooksApi
    {
        public GoogleBooksApi()
        {

        }
        public BookApiInfo ExecuteByIsbn(string isbn)
        {
            var url = string.Format("https://www.googleapis.com/books/v1/volumes?q=isbn:{0}", isbn);
            var json = WebApiUtil.GetResponseText(url);

            BookApiInfo info = new BookApiInfo
            {
                ApiName = "GoogleBooksApi",
                ResponseText = json,
                ResponseDatetime = DateTime.Now,
            };
            var jres = JObject.Parse(json);
            
            info.ItemCount = jres["totalItems"].Value<int>();
            if (info.ItemCount > 0)
            {
                foreach (var item in jres["items"])
                {
                    BookApiInfo.Item bookItem = new BookApiInfo.Item();

                    var volumeItems = (JObject)item["volumeInfo"];
                    foreach (var kvp in volumeItems)
                    {
                        switch (kvp.Key)
                        {
                            case "title":
                                bookItem.Title = kvp.Value.Value<string>();
                                break;
                            case "subtitle":
                                bookItem.Subtitle = kvp.Value.Value<string>();
                                break;
                            case "publisher":
                                bookItem.Publisher = kvp.Value.Value<string>();
                                break;
                            case "authors":
                                bookItem.Authors = string.Join(" ,", kvp.Value.Values<string>());
                                break;
                            case "publishedDate":
                                bookItem.PublishedDate = kvp.Value.Value<string>();
                                break;
                            case "description":
                                bookItem.Description = kvp.Value.Value<string>();
                                break;
                            case "industryIdentifiers":
                                var industryIdentifiers = (JArray)kvp.Value;
                                foreach (var ind in industryIdentifiers)
                                {
                                    var val = ind["identifier"].Value<string>();
                                    switch (ind["type"].Value<string>())
                                    {
                                        case "ISBN_13":
                                            bookItem.Isbn13 = val;
                                            break;
                                        case "ISBN_10":
                                            bookItem.Isbn10 = val;
                                            break;
                                    }
                                }
                                break;
                            case "pageCount":
                                bookItem.PageCount = kvp.Value.Value<string>();
                                break;
                            case "language":
                                bookItem.Language = kvp.Value.Value<string>();
                                break;
                            case "infoLink":
                                bookItem.InfoLink = kvp.Value.Value<string>();
                                break;
                            case "printType":
                                bookItem.PrintType = kvp.Value.Value<string>();
                                break;
                            case "categories":
                                bookItem.Categories = string.Join(", ", kvp.Value.Values<string>());
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
