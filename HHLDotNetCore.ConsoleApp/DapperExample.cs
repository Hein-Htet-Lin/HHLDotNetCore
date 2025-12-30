using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Dapper;
using HHLDotNetCore.ConsoleApp.Models;
using MySqlConnector;

namespace HHLDotNetCore.ConsoleApp
{
    public class DapperExample
    {
        private readonly string _connectionString = "Data Source=localhost;Initial Catalog=dotNetBatch5;User ID=root;Password=admin;";

        public void Read()
        {
            using IDbConnection db = new MySqlConnection(_connectionString);

            string query = "SELECT * FROM Tbl_Blog WHERE deleleFlag = 0";
            var lst = db.Query<BlogDataModels>(query).ToList();

            foreach (var item in lst)
            {
                Console.WriteLine(item.blogId);
                Console.WriteLine(item.blogTitle);
                Console.WriteLine(item.blogAuthor);
                Console.WriteLine(item.blogContent);
            }

        }


        public void Create(string title,string author,string content)
        {
            using IDbConnection db = new MySqlConnection(_connectionString);
            string query = "INSERT INTO Tbl_Blog (blogTitle,blogAuthor,blogContent,DeleleFlag) VALUES (@blogTitle,@blogAuthor,@blogContent,0)";
            int result = db.Execute(query, new BlogDataModels
            {
               blogTitle = title,
               blogAuthor = author,
               blogContent = content

            });

            Console.WriteLine(result == 1 ? "Saving Successful" : "Saving Failed");
        }

        public void Edit(int id)
        {
            using IDbConnection db = new MySqlConnection(_connectionString);
            string query = "SELECT * FROM Tbl_Blog WHERE deleleFlag = 0 and blogId = @blogId";
            var item = db.Query<BlogDataModels>(query,new BlogDataModels
            {
                blogId = id
            }).FirstOrDefault();

            if(item is null)
            {
                Console.WriteLine("No Data Found");
                return;
            }
            
            Console.WriteLine(item.blogId);
            Console.WriteLine(item.blogTitle);
            Console.WriteLine(item.blogAuthor);
            Console.WriteLine(item.blogContent);

        }
    }
}