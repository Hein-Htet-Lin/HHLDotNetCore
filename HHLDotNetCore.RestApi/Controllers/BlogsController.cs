using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HHLDotNetCore.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HHLDotNetCore.RestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogsController : ControllerBase
    {
        private readonly AppDbContext _db = new AppDbContext(); 

        [HttpGet]

        public IActionResult GetBlogs()
        {
            var lst = _db.TblBlogs.AsNoTracking().ToList();
            return Ok(lst);
        }

        [HttpGet("{id}")]

        public IActionResult GetBlog(int id)
        {
            var item = _db.TblBlogs.Where(x=>x.BlogId == id && x.DeleteFlag == 0).AsNoTracking().FirstOrDefault();
            if(item is null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]

        public IActionResult CreateBlog(TblBlog blog)
        {
            var item = _db.TblBlogs.Add(blog);
            _db.SaveChanges();
            return Ok(blog);
        }

        [HttpPut("{id}")]

        public IActionResult UpdateBlogs(int id,TblBlog blog)
        {
            var item = _db.TblBlogs.Where(x=>x.BlogId == id && x.DeleteFlag == 0).AsNoTracking().FirstOrDefault();

            if(item is null)
            {
                return NotFound();
            }

            item.BlogAuthor=blog.BlogAuthor;
            item.BlogTitle=blog.BlogTitle;
            item.BlogContent=blog.BlogContent;

            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();

            return Ok(blog);
        }

        [HttpPatch("{id}")]

        public IActionResult PatchBlogs(int id,TblBlog blog)
        {
            var item = _db.TblBlogs.Where(x=>x.BlogId == id && x.DeleteFlag == 0).AsNoTracking().FirstOrDefault();

            if(item is null)
            {
                return NotFound();
            }
            if (!String.IsNullOrEmpty(blog.BlogAuthor))
            {
                item.BlogAuthor=blog.BlogAuthor;
                
            }
            if (!String.IsNullOrEmpty(blog.BlogTitle))
            {
                item.BlogTitle=blog.BlogTitle;   
            }

            if (!String.IsNullOrEmpty(blog.BlogContent))
            {
                item.BlogContent=blog.BlogContent;
                
            }
            

            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();

            return Ok(blog);
        }

        [HttpDelete("{id}")]

        public IActionResult DeleteBlogs(int id)
        {
            var item = _db.TblBlogs.Where(x=>x.BlogId == id && x.DeleteFlag == 0).AsNoTracking().FirstOrDefault();

            if(item is null)
            {
                return NotFound();
            }
            
            item.DeleteFlag = 1;

            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            return Ok(item);
        }
    }
}