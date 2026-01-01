using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HHLDotNetCore.ConsoleApp.Models;
using Microsoft.EntityFrameworkCore;

namespace HHLDotNetCore.ConsoleApp
{
    public class EFCoreExample
    {
        public void Read()
        {
            AppDbContext db = new AppDbContext();
            var lst = db.Blogs.Where(x=> x.DeleteFlag == false ).ToList();

            foreach (var item in lst)
            {
                Console.WriteLine(item.BlogId);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);
            }

        }

        public void Create(string title,string author,string content)
        {
            BlogDataModels blog = new BlogDataModels
            {
              BlogTitle = title,
              BlogAuthor = author,
              BlogContent = content
            };
            AppDbContext db = new AppDbContext();
            db.Blogs.Add(blog);

            var result = db.SaveChanges();

            System.Console.WriteLine(result == 1 ? "Saving Successful" : "Saving Failed");
        }

        public void Edit(int id)
        {
            AppDbContext db = new AppDbContext();
            var item = db.Blogs.Where(x=>x.BlogId == id && x.DeleteFlag == false).FirstOrDefault();

            if(item is null)
            {
                System.Console.WriteLine("No Data Found");
                return;
            }
            Console.WriteLine(item.BlogId);
            Console.WriteLine(item.BlogTitle);
            Console.WriteLine(item.BlogAuthor);
            Console.WriteLine(item.BlogContent);

        }

        public void Update(int id,string title,string author,string content)
        {
            AppDbContext db = new AppDbContext();
            var item = db.Blogs.AsNoTracking().Where(x=>x.BlogId == id && x.DeleteFlag == false).FirstOrDefault();

            if(item is null)
            {
                System.Console.WriteLine("No Data Found");
                return;
            }

            if (!string.IsNullOrEmpty(title))
            {
                item.BlogTitle = title;
            }

            if (!string.IsNullOrEmpty(author))
            {
                item.BlogAuthor = author;
            }

            if (!string.IsNullOrEmpty(content))
            {
                item.BlogContent = content;
            }
            
            db.Entry(item).State = EntityState.Modified;
            var result = db.SaveChanges();

            System.Console.WriteLine(result == 1 ? "Update Successful" : "Update Failed");
        }

        public void Delete(int id)
        {
            AppDbContext db = new AppDbContext();
            var item = db.Blogs.AsNoTracking().Where(x=>x.BlogId == id && x.DeleteFlag == false).FirstOrDefault();

            if(item is null)
            {
                System.Console.WriteLine("No Data Found");
                return;
            }

            item.DeleteFlag = true;

            db.Entry(item).State = EntityState.Modified;
            var result = db.SaveChanges();
            System.Console.WriteLine(result == 1 ? "Delete successful":"Delete Failed");
        }
    }
}