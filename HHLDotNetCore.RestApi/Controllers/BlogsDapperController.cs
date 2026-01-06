using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using HHLDotNetCore.RestApi.DataModels;
using HHLDotNetCore.RestApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace HHLDotNetCore.RestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogsDapperController : ControllerBase
    {
        private readonly string _connectionString = "Data Source=localhost;Initial Catalog=dotNetBatch5;User ID=root;Password=admin;";
        [HttpGet]

        public IActionResult GetBlogs()
        {
            List<BlogsViewModel>viewLst = new List<BlogsViewModel>();
            using IDbConnection db = new MySqlConnection(_connectionString);
            String query = "SELECT * FROM Tbl_Blog WHERE DeleteFlag = 0";
            var lst = db.Query<BlogsDataModel>(query).ToList();
            
            foreach (var item in lst)
            {
                viewLst.Add(new BlogsViewModel
                {
                    Id = item.BlogId,
                    Title =item.BlogTitle,
                    Author = item.BlogAuthor,
                    Content = item.BlogContent
                });
            }
            return Ok(viewLst);
        }

        [HttpGet("{id}")]

        public IActionResult GetBlog(int id)
        {
            using IDbConnection db = new MySqlConnection(_connectionString);
            String query = "SELECT * FROM Tbl_Blog WHERE DeleteFlag = 0 and BlogId = @BlogId";
            var item = db.Query<BlogsDataModel>(query, new BlogsDataModel
            {
                BlogId = id
            }).FirstOrDefault();

            if (item is null)
            {
                return NotFound();
            }

            var viewLst = new BlogsViewModel
            {
                Id = item.BlogId,
                Title = item.BlogTitle,
                Author = item.BlogAuthor,
                Content = item.BlogContent,
            };
            
            return Ok(viewLst);
        }

        [HttpPost]

        public IActionResult CreateBlog(BlogsViewModel blog)
        {
            IDbConnection db = new MySqlConnection(_connectionString);
            String query = "INSERT INTO Tbl_Blog (BlogTitle,BlogAuthor,BlogContent,DeleteFlag) VALUES (@BlogTitle,@BlogAuthor,@BlogContent,0)";
            int result = db.Execute(query,new BlogsDataModel
            {
                BlogId = blog.Id,
                BlogTitle = blog.Title,
                BlogAuthor = blog.Author,
                BlogContent = blog.Content
            });

            String Message = result > 0 ? "Create Successful" : "Create failed";
            return Ok(Message);
        }

        [HttpPut("{id}")]

        public IActionResult UpdateBlogs(int id,BlogsViewModel blog)
        {
            IDbConnection db = new MySqlConnection(_connectionString);
            String Query = "UPDATE Tbl_Blog SET BlogTitle = @BlogTitle ,BlogAuthor = @BlogAuthor ,BlogContent = @BlogContent,DeleteFlag = 0 where BlogId = @BlogId";
            int result = db.Execute(Query,new BlogsDataModel
            {
                BlogId = id,
                BlogTitle = blog.Title,
                BlogAuthor = blog.Author,
                BlogContent = blog.Content

            });

            String Message = result > 1 ? "Update Successful" : "Update Failed";
            return Ok(Message);
        }

        [HttpPatch("{id}")]

        public IActionResult PatchBlogs(int id,BlogsViewModel blog)
        {
            IDbConnection db = new MySqlConnection(_connectionString);
            String conditions = "";
            if (String.IsNullOrEmpty(blog.Title))
            {
                conditions += " BlogTitle = @BlogTitle, ";
            }
            if (String.IsNullOrEmpty(blog.Author))
            {
                conditions += " BlogAuthor = @BlogAuthor, ";
            }
            if (String.IsNullOrEmpty(blog.Title))
            {
                conditions += " BlogContent = @BlogContent, ";
            }
            String Query = $"UPDATE Tbl_Blog SET {conditions} DeleteFlag = 0 where BlogId = @BlogId";
            int result = db.Execute(Query,new BlogsDataModel
            {
                BlogId = id,
                BlogTitle = blog.Title,
                BlogAuthor = blog.Author,
                BlogContent = blog.Content

            });

            String Message = result > 1 ? "Update Successful" : "Update Failed";
            return Ok(Message);
        }

        [HttpDelete("{id}")]

        public IActionResult DeleteBlogs(int id)
        {
            return Ok();
        }
    }
}