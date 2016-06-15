using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using MyApp3.Models;
using MyApp3.Data;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MyApp3.API
{
    [Route("api/[controller]")]
    public class CommentController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private ApplicationDbContext _db;
        public CommentController(UserManager<ApplicationUser> userManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            var userId = _userManager.GetUserId(this.User);
            var comments = _db.Comments.Where(c => c.UserId == userId).ToList();
            return Ok(comments);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            // goal is to get the user by using the comment id
            var user = _db.Comments.Where(c => c.Id == id).Select(c => c.User).FirstOrDefault();

            return "value";
        }

        // POST api/values
        [HttpPost("{id}")]
        public void Post(int id, [FromBody]Comment comment)
        {
            var userId = _userManager.GetUserId(this.User);
            comment.UserId = userId;

            var blog = _db.Blogs.Where(b => b.Id == id).Include(b => b.Comments).FirstOrDefault();
            blog.Comments.Add(comment);
            _db.SaveChanges();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
