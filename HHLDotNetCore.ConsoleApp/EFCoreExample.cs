using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HHLDotNetCore.ConsoleApp.Models;

namespace HHLDotNetCore.ConsoleApp
{
    public class EFCoreExample
    {
        public void Read()
        {
            AppDbContext db = new AppDbContext();
            var lst = db.Blogs.Where(x=> x.deleleFlag == false ).ToList();

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
            BlogDataModels blog = new BlogDataModels
            {
              blogTitle = title,
              blogAuthor = author,
              blogContent = content
            };
            AppDbContext db = new AppDbContext();
            db.Blogs.Add(blog);

            var result = db.SaveChanges();

            System.Console.WriteLine(result == 1 ? "Saving Successful" : "Saving Failed");
        }
    }
}