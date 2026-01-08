using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using HHLDotNetCore.Shared;

namespace HHLDotNetCore.ConsoleApp
{
    public class AdoDotNetExample2
    {
        private readonly string _connectionString = "Data Source=localhost;Initial Catalog=dotNetBatch5;User ID=root;Password=admin;";
        private readonly AdoDotNetService _adoDotNetService;
        
        public AdoDotNetExample2()
        {
            _adoDotNetService = new AdoDotNetService(_connectionString);

        }

        public void Read()
        {
            string query = @"SELECT BlogId,
                BlogAuthor,
                BlogTitle,
                BlogContent FROM Tbl_Blog WHERE DeleteFlag = 0";
            
            var dt = _adoDotNetService.Query(query);
            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine(dr["blogId"]);
                Console.WriteLine(dr["blogTitle"]);
                Console.WriteLine(dr["blogAuthor"]);
                Console.WriteLine(dr["blogContent"]);
            }
        }

        public void Edit()
        {

            Console.WriteLine("Enter BlogId: ");
            string blogId = Console.ReadLine();
            string query = @"SELECT BlogId,
                BlogAuthor,
                BlogTitle,
                BlogContent FROM Tbl_Blog WHERE BlogId=@blogId && DeleteFlag = 0";
            
            var dt = _adoDotNetService.Query(query,new ParameterModel("@BlogId",blogId));
            if(dt.Rows.Count == 0)
            {
                Console.WriteLine("Data Not Found");
                return;
            }
            DataRow dr = dt.Rows[0];

            Console.WriteLine("Blog Id:" + dr["blogId"]);
            Console.WriteLine("Blog Title:" + dr["blogTitle"]);
            Console.WriteLine("Blog Author:" + dr["blogAuthor"]);
            Console.WriteLine("Blog Content:" + dr["blogContent"]);
        }

        public void Create()
        {
            // string connectionString2 = "Data Source=localhost;Initial Catalog = dotNetBatch5; User Id= root;Password = admin";

            Console.WriteLine("BlogTitle ");
            string blogTitle = Console.ReadLine();

            Console.WriteLine("BlogAuthor ");
            string blogAuthor = Console.ReadLine();

            Console.WriteLine("BlogContent ");
            string blogContent = Console.ReadLine();


            string query = $@"INSERT INTO Tbl_Blog 
                    (BlogAuthor,
                    BlogTitle,
                    BlogContent,
                    DeleteFlag) 
                VALUES 
                    (@BlogAuthor,
                    @BlogTitle,
                    @BlogContent,
                    0)";

            
            int result = _adoDotNetService.Execute(query,new ParameterModel("@BlogTitle",blogTitle),
            new ParameterModel("@BlogAuthor",blogAuthor),
            new ParameterModel("@BlogContent",blogContent));

            Console.WriteLine(result == 1 ? "Saving Successful" : "saving Failed");
        }

        public void Update()
        {
            Console.WriteLine("Blog Id: ");
            string blogId = Console.ReadLine();

            Console.WriteLine("Blog Title: ");
            string blogTitle = Console.ReadLine();

            Console.WriteLine("Blog Author: ");
            string blogAuthor = Console.ReadLine();

            Console.WriteLine("Blog Content: ");
            string blogContent = Console.ReadLine();

            string query = @"
                    UPDATE 
                    Tbl_Blog SET 
                    BlogTitle = @BlogTitle ,
                    BlogAuthor = @BlogAuthor ,
                    BlogContent = @BlogContent,
                    DeleteFlag = 0 where BlogId = @BlogId
            ";
            
            int result = _adoDotNetService.Execute(query,new ParameterModel("BlogId",blogId),
            new ParameterModel("BlogAuthor",blogAuthor),
            new ParameterModel("BlogTitle",blogTitle),
            new ParameterModel("BlogContent",blogContent));

            Console.WriteLine(result == 1 ? "Update Successful" : "Update Failed");
        }
    
        public void Delete()
        {
            Console.WriteLine("Blog Id: ");
            string blogId = Console.ReadLine();
            string query = @"
                        UPDATE
                        Tbl_Blog
                        SET DeleteFlag = 1 
                        WHERE 
                        BlogId = @BlogId
            ";
            int result = _adoDotNetService.Execute(query,new ParameterModel("@BlogId",blogId));

            Console.WriteLine(result == 1 ? "Delete Successful" : "Delete Failed");
        }


    }
}