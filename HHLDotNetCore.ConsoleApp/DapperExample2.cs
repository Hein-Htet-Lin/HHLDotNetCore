using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HHLDotNetCore.ConsoleApp.Models;
using HHLDotNetCore.Shared;

namespace HHLDotNetCore.ConsoleApp
{
    public class DapperExample2
    {
        private readonly string _connectionString = "Data Source=localhost;Initial Catalog=dotNetBatch5;User ID=root;Password=admin;";
        private readonly  DapperService _dapperService;

        public DapperExample2()
        {
            _dapperService = new DapperService(_connectionString);
        }

        public void Read()
        {
            string query = "SELECT * FROM Tbl_Blog WHERE DeleteFlag = 0";
            var lst = _dapperService.Query<BlogDapperDataModels>(query).ToList();

            foreach (var item in lst)
            {
                Console.WriteLine(item.BlogId);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);
            }

        }


        public void Create(string title, string author, string content)
        {
            string query = "INSERT INTO Tbl_Blog (BlogTitle,BlogAuthor,BlogContent,DeleteFlag) VALUES (@BlogTitle,@BlogAuthor,@BlogContent,0)";
            int result = _dapperService.Execute(query, new BlogDapperDataModels
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content

            });

            Console.WriteLine(result == 1 ? "Saving Successful" : "Saving Failed");
        }

        public void Edit(int id)
        {
            
            string query = "SELECT * FROM Tbl_Blog WHERE DeleteFlag = 0 and BlogId = @BlogId";
            var item = _dapperService.QueryFirstOrDefault<BlogDapperDataModels>(query, new BlogDapperDataModels
            {
                BlogId = id
            });

            if (item is null)
            {
                Console.WriteLine("No Data Found");
                return;
            }

            Console.WriteLine(item.BlogId);
            Console.WriteLine(item.BlogTitle);
            Console.WriteLine(item.BlogAuthor);
            Console.WriteLine(item.BlogContent);

        }

        public void Update(int id, string author, string title, string content)
        {
            string query = "UPDATE Tbl_Blog SET BlogTitle = @BlogTitle ,BlogAuthor = @BlogAuthor ,BlogContent = @BlogContent,DeleteFlag = 0 where BlogId = @BlogId";
            int result = _dapperService.Execute(query, new BlogDapperDataModels
            {
                BlogId = id,
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            });

            Console.WriteLine(result == 1 ? "Update Successful" : "Update Failed");
        }

        public void Delete(int id)
        {
            string query = "UPDATE Tbl_Blog SET DeleteFlag = 1 WHERE BlogId = @BlogId";
            int result = _dapperService.Execute(query,new BlogDapperDataModels
            {
                BlogId = id
            });

            Console.WriteLine(result == 1 ? "Delete Successful" : "Delete Failed");
        }
    }
}