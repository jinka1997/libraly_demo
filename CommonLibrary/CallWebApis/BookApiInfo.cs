using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.CallWebApis
{
    public class BookApiInfo
    {
        public string ApiName { set; get; }
        public string ResponseText { set; get; }
        public DateTime ResponseDatetime { set; get; }
        public int ItemCount { set; get; }
        public List<Item> BookItems { set; get; }

        public BookApiInfo()
        {
            BookItems = new List<Item>();
        }

        public class Item
        {
            public string Title { set; get; }
            public string Subtitle { set; get; }
            public string Isbn13 { set; get; }
            public string Isbn10 { set; get; }
            public string Authors { set; get; }
            public string Publisher { set; get; }
            public string PublishedDate { set; get; }
            public string Description { set; get; }
            public string PageCount { set; get; }
            public string Language { set; get; }
            public string InfoLink { set; get; }
            public string PrintType { set; get; }
            public string Categories { set; get; }

        }
    }
}
