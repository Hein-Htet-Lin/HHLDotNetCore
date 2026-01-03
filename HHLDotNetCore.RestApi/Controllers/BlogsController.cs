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
            return Ok(item);
        }

        [HttpPut]

        public IActionResult UpdateBlogs()
        {
            return Ok();
        }

        [HttpPatch]

        public IActionResult PatchBlogs()
        {
            return Ok();
        }

        [HttpDelete]

        public IActionResult DeleteBlogs()
        {
            return Ok();
        }
    }
}