using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonLibrary.Models
{
    public class RequestInfo : CommonField
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RequestId { set; get; }
        public string SearchKey { set; get; }

        public List<ApiResponse> ApiResponses { set; get; }
    }
}
