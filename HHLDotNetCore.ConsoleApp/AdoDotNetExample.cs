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


            string query = @"SELECT BlogId,
                BlogAuthor,
                BlogTitle,
                BlogContent FROM Tbl_Blog WHERE DeleteFlag = 0";


            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(reader["BlogId"]);
                Console.WriteLine(reader["BlogTitle"]);
                Console.WriteLine(reader["BlogAuthor"]);
                Console.WriteLine(reader["BlogContent"]);
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
                    (BlogAuthor,
                    BlogTitle,
                    BlogContent,
                    DeleteFlag) 
                VALUES 
                    (@BlogAuthor,
                    @BlogTitle,
                    @BlogContent,
                    0)";

            MySqlCommand cmd2 = new MySqlCommand(queryInsert, connection2);
            // MySqlDataAdapter adapter2 = new MySqlDataAdapter(cmd2);
            // DataTable dt = new DataTable();
            // adapter2.Fill(dt);
            cmd2.Parameters.AddWithValue("@BlogTitle", blogTitle);
            cmd2.Parameters.AddWithValue("@BlogAuthor", blogAuthor);
            cmd2.Parameters.AddWithValue("@BlogContent", blogContent);

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
                BlogAuthor,
                BlogTitle,
                BlogContent 
                FROM Tbl_Blog WHERE BlogId = @BlogID
            ";
            MySqlCommand cmd = new MySqlCommand(query,connection);
            cmd.Parameters.AddWithValue("@BlogId",blogId);
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
                    BlogTitle = @BlogTitle ,
                    BlogAuthor = @BlogAuthor ,
                    BlogContent = @BlogContent,
                    DeleteFlag = 0 where BlogId = @BlogId
            ";
            MySqlCommand cmd = new MySqlCommand(query,connection);
            cmd.Parameters.AddWithValue("BlogId",blogId);
            cmd.Parameters.AddWithValue("BlogTitle",blogTitle);
            cmd.Parameters.AddWithValue("BlogAuthor",blogAuthor);
            cmd.Parameters.AddWithValue("BlogContent",blogcontent);

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
                        SET DeleteFlag = 1 
                        WHERE 
                        BlogId = @BlogId
            ";
            MySqlCommand cmd = new MySqlCommand(query,connection);
            cmd.Parameters.AddWithValue("BlogId",blogId);
            int result = cmd.ExecuteNonQuery();
            connection.Close();

            Console.WriteLine(result == 1 ? "Delete Successful" : "Delete Failed");
        }
    
    }
}