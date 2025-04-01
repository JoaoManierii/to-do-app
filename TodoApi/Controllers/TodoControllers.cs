using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace TodoApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {
        private static List<TodoItem> _todos = new List<TodoItem>();

        [HttpGet]
        public IActionResult Get() => Ok(_todos);

        [HttpPost]
        public IActionResult Post([FromBody] TodoItem item)
        {
            _todos.Add(item);
            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
        }
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] TodoItem updatedItem)
        {
            var existing = _todos.FirstOrDefault(t => t.Id == id);
            if (existing == null) return NotFound();

            existing.Title = updatedItem.Title;
            existing.IsCompleted = updatedItem.IsCompleted;
            return NoContent();
        }
        public IActionResult Delete(int id)
        {
            var item = _todos.FirstOrDefault(t => t.Id == id);
            if (item == null) return NotFound();

            _todos.Remove(item);
            return NoContent();
        }
        public IActionResult GetById(int id)
        {
            var item = _todos.FirstOrDefault(t => t.Id == id);
            if (item == null) return NotFound();

            return Ok(item);
        }
    }

   public class TodoItem
{
    public long Id { get; set; }       
    public string Title { get; set; }
    public bool IsCompleted { get; set; }
}

}
