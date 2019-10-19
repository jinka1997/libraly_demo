using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace CommonLibrary.Models
{
    public class ApiResponse : CommonField
    {
        [Key]
        public int ApiResponseId { set; get; }
        public int RequestId { set; get; }
        public int RequestApiSeq { set; get; }
        public string ApiName { set; get; }
        public DateTime ResponseDateTime { set; get; }
        public string ResponseText { set; get; }

        public ICollection<BookInfo> BookInfos { set; get; }
    }
}
