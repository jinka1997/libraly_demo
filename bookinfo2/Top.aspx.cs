﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommonLibrary.CallWebApis;
using System.Data;

namespace bookinfo2
{
    public partial class Top : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TextBox1.Focus();

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            var isbn = TextBox1.Text;

            var api = new GoogleBooksApi();
            var bookInfo = api.ExecuteByIsbn(isbn);

            var api2 = new NationalDietLibraryApi();
            var ndlInfo = api2.ExecuteByIsbn(isbn);

            var ar = new BookApiInfo[] { bookInfo, ndlInfo };


            DataTable dt = new DataTable();
            dt.Columns.Add("Seq");
            dt.Columns.Add("API");
            dt.Columns.Add("タイトル");
            dt.Columns.Add("サブタイトル");
            dt.Columns.Add("ISBN_13");
            dt.Columns.Add("ISBN_10");
            dt.Columns.Add("著者");
            dt.Columns.Add("出版社");
            dt.Columns.Add("発行日");
            //dt.Columns.Add("説明");
            //dt.Columns.Add("ページ数");
            //dt.Columns.Add("言語");
            //dt.Columns.Add("リンク");
            //dt.Columns.Add("印刷種類");
            //dt.Columns.Add("カテゴリ");

            //GridView1.AutoGenerateColumns = false;

            int seq = 0;

            foreach (var gi in bookInfo.BookItems)
            {
                seq++;
                dt.Rows.Add(seq, "Google", gi.Title, gi.Subtitle, gi.Isbn13, gi.Isbn10, gi.Authors, gi.Publisher, gi.PublishedDate
                    //,
                    //gi.Description, gi.PageCount, gi.Language, gi.InfoLink, gi.PrintType, gi.Categories
                    );
            }

            foreach (var ndlItem in ndlInfo.BookItems)
            {
                seq++;
                dt.Rows.Add(seq, "国立図書館", ndlItem.Title, ndlItem.Subtitle, ndlItem.Isbn13, ndlItem.Isbn10,
                    ndlItem.Authors, ndlItem.Publisher, ndlItem.PublishedDate
                    //, ndlItem.Description, 
                    //ndlItem.PageCount, ndlItem.Language, ndlItem.InfoLink, ndlItem.PrintType, ndlItem.Categories
                    );
            }

            GridView1.DataSource = dt;
            GridView1.DataBind();
            TextBox1.Text = "";
            TextBox1.Focus();

        }

    }
}