using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HHLDotNetCore.RestApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace HHLDotNetCore.RestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogsAdoDotNetController : ControllerBase
    {
        private readonly string _connectionString = "Data Source=localhost;Initial Catalog=dotNetBatch5;User ID=root;Password=admin;";
        
        [HttpGet]

        public IActionResult GetBlogs()
        {
            List<BlogsViewModel>lst = new List<BlogsViewModel>();
            MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();

            string query = @"SELECT BlogId,
                BlogAuthor,
                BlogTitle,
                BlogContent FROM Tbl_Blog WHERE DeleteFlag = 0";


            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lst.Add(new BlogsViewModel
                {
                    Id = Convert.ToInt32(reader["BlogID"]),
                    Title = Convert.ToString(reader["BlogTitle"]),
                    Author = Convert.ToString(reader["BlogAuthor"]),
                    Content = Convert.ToString(reader["BlogContent"]),
                });
            }

            connection.Close();
            return Ok(lst);
            
        }

        [HttpGet("{id}")]

        public IActionResult GetBlog(int id)
        {
            BlogsViewModel item = new BlogsViewModel();
            MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();

            string query = @"
                SELECT blogId,
                BlogAuthor,
                BlogTitle,
                BlogContent 
                FROM Tbl_Blog WHERE BlogId = @BlogID
            ";;


            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogID",id);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                item = new BlogsViewModel
                {
                    Id = Convert.ToInt32(reader["BlogID"]),
                    Title = Convert.ToString(reader["BlogTitle"]),
                    Author = Convert.ToString(reader["BlogAuthor"]),
                    Content = Convert.ToString(reader["BlogContent"]),
                };
            }

            connection.Close();
            return Ok(item);
        }

        [HttpPost]

        public IActionResult CreateBlog([FromBody]BlogsViewModel blog)
        {
            MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();
            string queryInsert = $@"INSERT INTO Tbl_Blog 
                    (BlogAuthor,
                    BlogTitle,
                    BlogContent,
                    DeleteFlag) 
                VALUES 
                    (@BlogAuthor,
                    @BlogTitle,
                    @BlogContent,
                    0)";

            MySqlCommand cmd2 = new MySqlCommand(queryInsert, connection);
            // MySqlDataAdapter adapter2 = new MySqlDataAdapter(cmd2);
            // DataTable dt = new DataTable();
            // adapter2.Fill(dt);
            cmd2.Parameters.AddWithValue("@BlogTitle", blog.Title);
            cmd2.Parameters.AddWithValue("@BlogAuthor", blog.Author);
            cmd2.Parameters.AddWithValue("@BlogContent", blog.Content);

            int result = cmd2.ExecuteNonQuery();
            String message = result == 1 ? "Create Successful" : "Create Failed" ;
            connection.Close();
            return Ok(message);
        }

        [HttpPut("{id}")]

        public IActionResult UpdateBlogs(int id,[FromBody]BlogsViewModel blog)
        {
            MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();
            string query = @"
                    UPDATE 
                    Tbl_Blog SET 
                    BlogTitle = @BlogTitle ,
                    BlogAuthor = @BlogAuthor ,
                    BlogContent = @BlogContent,
                    DeleteFlag = 0 where BlogId = @BlogId
            ";
            MySqlCommand cmd = new MySqlCommand(query,connection);
            cmd.Parameters.AddWithValue("BlogId",id);
            cmd.Parameters.AddWithValue("BlogTitle",blog.Title);
            cmd.Parameters.AddWithValue("BlogAuthor",blog.Author);
            cmd.Parameters.AddWithValue("BlogContent",blog.Content);

            int result = cmd.ExecuteNonQuery();
            connection.Close();
            String Message = result > 0 ? "Updating Successful" : "Updating Failed";
            return Ok(Message);
        }

        [HttpPatch("{id}")]

        public IActionResult PatchBlogs(int id,[FromBody]BlogsViewModel blog)
        {
            String conditions = "";
            if (!String.IsNullOrEmpty(blog.Title))
            {
                conditions += " BlogTitle = @BlogTitle, ";
            }
            if (!String.IsNullOrEmpty(blog.Author))
            {
                conditions += " BlogAuthor = @BlogAuthor, ";
            }
            if (!String.IsNullOrEmpty(blog.Content))
            {
                conditions += " BlogContent = @BlogContent, ";
            }
            if(conditions.Length == 0)
            {
                return BadRequest();
            }

            // conditions = conditions.Substring(0,conditions.Length - 2);

            MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();

            string query = $@"
                    UPDATE 
                    Tbl_Blog SET 
                    {conditions}
                    DeleteFlag = 0 where BlogId = @BlogId
            ";
            MySqlCommand cmd = new MySqlCommand(query,connection);
            cmd.Parameters.AddWithValue("BlogId",id);
            
            if (!String.IsNullOrEmpty(blog.Title))
            {
            cmd.Parameters.AddWithValue("BlogTitle",blog.Title);
            }
            
            if (!String.IsNullOrEmpty(blog.Author))
            {
            cmd.Parameters.AddWithValue("BlogAuthor",blog.Author);
            }
            
            if (!String.IsNullOrEmpty(blog.Content))
            {
            cmd.Parameters.AddWithValue("BlogContent",blog.Content);
            }
            
            

            int result = cmd.ExecuteNonQuery();
            connection.Close();
            String Message = result > 0 ? "Updating Successful" : "Updating Failed";
            return Ok(Message);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlogs(int id)
        {
            MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();
            String query = "UPDATE Tbl_Blog SET DeleteFlag = 1 WHERE BlogId = @BlogId";
            MySqlCommand cmd = new MySqlCommand(query,connection);
            cmd.Parameters.AddWithValue("BlogId",id);
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            String Message = result > 0 ? "Delete Successful" : "Delete Failed";

            return Ok(Message);
        }
        
    }
}