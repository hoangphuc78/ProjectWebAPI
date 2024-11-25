using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectWebAPI.Models;

namespace ProjectWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private static List<Article> articles = new List<Article>(); 

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAll()
        {
            return Ok(articles);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Get(int id)
        {
            var article = articles.FirstOrDefault(a => a.Id == id);
            if (article == null)
            {
                return NotFound();
            }
            return Ok(article);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Editor")]
        public IActionResult Create([FromBody] Article newArticle)
        {
            articles.Add(newArticle);
            return CreatedAtAction(nameof(Get), new { id = newArticle.Id }, newArticle);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Editor")]
        public IActionResult Update(int id, [FromBody] Article updatedArticle)
        {
            var article = articles.FirstOrDefault(a => a.Id == id);
            if (article == null)
            {
                return NotFound();
            }
            article.Title = updatedArticle.Title;
            article.Content = updatedArticle.Content;
            article.CategoryId = updatedArticle.CategoryId;
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var article = articles.FirstOrDefault(a => a.Id == id);
            if (article == null)
            {
                return NotFound();
            }
            articles.Remove(article);
            return NoContent();
        }
    }
}
