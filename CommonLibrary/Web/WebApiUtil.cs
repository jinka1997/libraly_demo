using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace CommonLibrary.Web
{
    public static class WebApiUtil
    {
        public static string GetResponseText(string url)
        {
            var result = "";
            var req = (HttpWebRequest)WebRequest.Create(url);     //using System.Net;

            // 指定したURLに対してReqestを投げてResponseを取得します。
            try
            {
                using (var res = (HttpWebResponse)req.GetResponse())
                using (var resSt = res.GetResponseStream())
                // 取得した文字列をUTF8でエンコードします。
                using (var sr = new StreamReader(resSt, Encoding.UTF8))     //using System.IO;
                {
                    result = sr.ReadToEnd();
                }
            }
            catch (Exception)
            {
                //握りつぶす
            }
            return result;
        }
    }
}
