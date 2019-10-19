using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CommonLibrary.Models
{
    public class BookInfo : CommonField
    {
        [Key]
        public int BookInfoId { set; get; }
        public int ItemIndex { set; get; }
        public int ApiResponseId { set; get; }
        public string Title { set; get; }
        public string Subtitle { set; get; }
        public string Isbn10 { set; get; }
        public string Isbn13 { set; get; }
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
