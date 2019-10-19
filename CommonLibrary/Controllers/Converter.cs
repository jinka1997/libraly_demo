using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace CommonLibrary.Controllers
{
    public class Converter
    {
        public Converter()
        {
        }

        public void Regist(SqlConnection cnc, string searchKey, IEnumerable<CallWebApis.BookApiInfo> infos)
        {
            cnc.Open();
            using (SqlTransaction tran = cnc.BeginTransaction())
            {
                var ri = new Models.RequestInfo
                {
                    SearchKey = searchKey,
                    CreateUser = Environment.UserName
                };

                StringBuilder sb = new StringBuilder();
                sb.Append(" INSERT INTO t_request_info(search_key, create_user) ");
                sb.Append(" VALUES ");
                sb.Append(" (@1, @2); ");
                sb.Append(" SELECT SCOPE_IDENTITY();");
                SqlCommand cmd1 = new SqlCommand(sb.ToString(), cnc, tran);
                cmd1.Parameters.AddWithValue("@1", ri.SearchKey);
                cmd1.Parameters.AddWithValue("@2", ri.CreateUser);
                object id1 = cmd1.ExecuteScalar();

                int requestId = int.Parse(id1.ToString());

                foreach (var info in infos)
                {
                    var ar = new Models.ApiResponse
                    {
                        RequestId = requestId,
                        ApiName = info.ApiName,
                        ResponseDateTime = info.ResponseDatetime,
                        ResponseText = info.ResponseText,
                        CreateUser = Environment.UserName
                    };

                    StringBuilder sb1 = new StringBuilder();
                    sb1.Append(" INSERT INTO t_api_response ");
                    sb1.Append(" (request_id,api_name,response_datetime,response_text,create_user) ");
                    sb1.Append(" VALUES (@1,@2,@3,@4,@5); ");
                    sb1.Append(" SELECT SCOPE_IDENTITY(); ");
                    SqlCommand cmd2 = new SqlCommand(sb1.ToString(), cnc, tran);
                    cmd2.Parameters.AddWithValue("@1", ar.RequestId);
                    cmd2.Parameters.AddWithValue("@2", ar.ApiName);
                    cmd2.Parameters.AddWithValue("@3", ar.ResponseDateTime);
                    cmd2.Parameters.AddWithValue("@4", ar.ResponseText);
                    cmd2.Parameters.AddWithValue("@5", ar.CreateUser);
                    object id2 = cmd2.ExecuteScalar();

                    int apiResponseId = int.Parse(id2.ToString());
                    int itemIndex = 0;

                    foreach (var item in info.BookItems)
                    {
                        itemIndex++;
                        var bi = new Models.BookInfo
                        {
                            ApiResponseId = apiResponseId,
                            ItemIndex = itemIndex,
                            Title = item.Title,
                            Subtitle = item.Subtitle,
                            Isbn10 = item.Isbn10,
                            Isbn13 = item.Isbn13,
                            Authors = item.Authors,
                            Publisher = item.Publisher,
                            PublishedDate = item.PublishedDate,
                            Description = item.Description,
                            PageCount = item.PageCount,
                            Language = item.Language,
                            InfoLink = item.InfoLink,
                            PrintType = item.PrintType,
                            Categories = item.Categories,
                            CreateUser = Environment.UserName
                        };

                        StringBuilder sb2 = new StringBuilder();
                        sb2.Append(" INSERT INTO t_book_info ");
                        sb2.Append(" ([api_response_id],[item_index],[title],[sub_title] ");
                        sb2.Append(" ,[isbn10],[isbn13],[authors],[publisher],[publishedDate] ");
                        sb2.Append(" ,[description],[page_count],[language],[info_link],[print_type] ");
                        sb2.Append(" ,[categories],[create_user]) ");
                        sb2.Append(" VALUES ");
                        sb2.Append(" (@1,@2,@3,@4 ");
                        sb2.Append(" ,@5,@6,@7,@8,@9 ");
                        sb2.Append(" ,@10,@11,@12,@13,@14 ");
                        sb2.Append(" ,@15,@16) ");

                        SqlCommand cmd3 = new SqlCommand(sb2.ToString(), cnc, tran);
                        cmd3.Parameters.AddWithValue("@1", bi.ApiResponseId);
                        cmd3.Parameters.AddWithValue("@2", bi.ItemIndex);
                        cmd3.Parameters.AddWithValue("@3", bi.Title ?? "");
                        cmd3.Parameters.AddWithValue("@4", bi.Subtitle ?? "");
                        cmd3.Parameters.AddWithValue("@5", bi.Isbn10 ?? "");
                        cmd3.Parameters.AddWithValue("@6", bi.Isbn13 ?? "");
                        cmd3.Parameters.AddWithValue("@7", bi.Authors ?? "");
                        cmd3.Parameters.AddWithValue("@8", bi.Publisher ?? "");
                        cmd3.Parameters.AddWithValue("@9", bi.PublishedDate ?? "");
                        cmd3.Parameters.AddWithValue("@10", bi.Description ?? "");
                        cmd3.Parameters.AddWithValue("@11", bi.PageCount ?? "");
                        cmd3.Parameters.AddWithValue("@12", bi.Language ?? "");
                        cmd3.Parameters.AddWithValue("@13", bi.InfoLink ?? "");
                        cmd3.Parameters.AddWithValue("@14", bi.PrintType ?? "");
                        cmd3.Parameters.AddWithValue("@15", bi.Categories ?? "");
                        cmd3.Parameters.AddWithValue("@16", bi.CreateUser);

                        cmd3.ExecuteNonQuery();
                    }
                }
                tran.Commit();
            }
        }
    }
}
