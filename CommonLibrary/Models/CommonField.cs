using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Models
{
    public class CommonField
    {
        public string DeleteFlg { set; get; }
        public DateTime CreateDate { set; get; }
        public string CreateUser { set; get; }
        public DateTime UpdateDate { set; get; }
        public string UpdateUser { set; get; }
        public int VersionNo { set; get; }
    }
}
