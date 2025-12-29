using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using MySqlConnector;

namespace HHLDotNetCore
{
    public class AdoDotNetExample
    {
        private readonly string _connectionString = "Data Source=localhost;Initial Catalog=dotNetBatch5;User ID=root;Password=admin;";

        public void Read()
        {
            // string connectionString = "Data Source=localhost;Initial Catalog=dotNetBatch5;User ID=root;Password=admin;";

            MySqlConnection connection = new MySqlConnection(_connectionString);
            Console.WriteLine("Connetion Opening.....");
            connection.Open();
            Console.WriteLine("Connetion Opened");


            string query = @"SELECT blogId,
                blogAuthor,
                blogTitle,
                blogContent FROM Tbl_Blog WHERE deleleFlag = 0";


            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(reader["blogId"]);
                Console.WriteLine(reader["blogTitle"]);
                Console.WriteLine(reader["blogAuthor"]);
                Console.WriteLine(reader["blogContent"]);
            }
            // MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            // DataTable dt = new DataTable();
            // adapter.Fill(dt);



            // MySqlDataAdapter adapter = new MySqlDataAdapter(cmd2);
            // DataTable dt = new DataTable();
            // adapter.Fill(dt);


            Console.WriteLine("Connection Closing.....");
            connection.Close();
            Console.WriteLine("Connection Closed");

            // foreach (DataRow dr in dt.Rows)
            // {
            //     Console.WriteLine(dr["blogId"]);
            //     Console.WriteLine(dr["blogTitle"]);
            //     Console.WriteLine(dr["blogAuthor"]);
            //     Console.WriteLine(dr["blogContent"]);
            // }
        }

        public void Create()
        {
            // string connectionString2 = "Data Source=localhost;Initial Catalog = dotNetBatch5; User Id= root;Password = admin";
            MySqlConnection connection2 = new MySqlConnection(_connectionString);
            connection2.Open();
            Console.WriteLine("Connection Opened");

            Console.WriteLine("BlogTitle ");
            string blogTitle = Console.ReadLine();

            Console.WriteLine("BlogAuthor ");
            string blogAuthor = Console.ReadLine();

            Console.WriteLine("BlogContent ");
            string blogContent = Console.ReadLine();


            string queryInsert = $@"INSERT INTO Tbl_Blog 
                    (blogAuthor,
                    blogTitle,
                    blogContent,
                    deleleFlag) 
                VALUES 
                    (@blogAuthor,
                    @blogTitle,
                    @blogContent,
                    0)";

            MySqlCommand cmd2 = new MySqlCommand(queryInsert, connection2);
            // MySqlDataAdapter adapter2 = new MySqlDataAdapter(cmd2);
            // DataTable dt = new DataTable();
            // adapter2.Fill(dt);
            cmd2.Parameters.AddWithValue("@blogTitle", blogTitle);
            cmd2.Parameters.AddWithValue("@blogAuthor", blogAuthor);
            cmd2.Parameters.AddWithValue("@blogContent", blogContent);

            int result = cmd2.ExecuteNonQuery();




            connection2.Close();
            Console.WriteLine("Connection Closed....");

            Console.WriteLine(result == 1 ? "Saving Successful" : "saving Failed");
        }

        public void Edit()
        {

            Console.WriteLine("Enter BlogId: ");
            string blogId = Console.ReadLine();

            MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();
            string query = @"
                SELECT blogId,
                blogAuthor,
                blogTitle,
                blogContent 
                FROM Tbl_Blog WHERE blogId = @blogID
            ";
            MySqlCommand cmd = new MySqlCommand(query,connection);
            cmd.Parameters.AddWithValue("@blogId",blogId);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            connection.Close();

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
    
        public void Update()
        {
            Console.WriteLine("Blog Id: ");
            string blogId = Console.ReadLine();

            Console.WriteLine("Blog Title: ");
            string blogTitle = Console.ReadLine();

            Console.WriteLine("Blog Author: ");
            string blogAuthor = Console.ReadLine();

            Console.WriteLine("Blog Content: ");
            string blogcontent = Console.ReadLine();

            MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();
            string query = @"
                    UPDATE 
                    Tbl_Blog SET 
                    blogTitle = @blogTitle ,
                    blogAuthor = @blogAuthor ,
                    blogContent = @blogContent,
                    deleleFlag = 0 where blogId = @blogId
            ";
            MySqlCommand cmd = new MySqlCommand(query,connection);
            cmd.Parameters.AddWithValue("blogId",blogId);
            cmd.Parameters.AddWithValue("blogTitle",blogTitle);
            cmd.Parameters.AddWithValue("blogAuthor",blogAuthor);
            cmd.Parameters.AddWithValue("blogContent",blogcontent);

            int result = cmd.ExecuteNonQuery();

            connection.Close();

            Console.WriteLine(result == 1 ? "Update Successful" : "Update Failed");
        }
    
        public void Delete()
        {
            Console.WriteLine("Blog Id: ");
            string blogId = Console.ReadLine();

            MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();
            string query = @"
                        UPDATE
                        Tbl_Blog
                        SET deleleFlag = 1 
                        WHERE 
                        blogId = @blogId
            ";
            MySqlCommand cmd = new MySqlCommand(query,connection);
            cmd.Parameters.AddWithValue("blogId",blogId);
            int result = cmd.ExecuteNonQuery();
            connection.Close();

            Console.WriteLine(result == 1 ? "Delete Successful" : "Delete Failed");
        }
    
    }
}