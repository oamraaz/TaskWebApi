using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyFirstApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    { 
        private static List<Task> tasks = new List<Task>
            {
                new Task {
                    Id = 1,
                    Name = "Add CRUD",
                    Description = "Create, Read, Update, Delete",
                }
            };

        private readonly DataContext _context;

        public TaskController(DataContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Task>>> Get()
        {
            return Ok(await _context.tasks.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Task>> GetById(int id)
        {
            var task = await _context.tasks.FindAsync(id);
            if (task == null)
                return BadRequest("Task is not found");
            return Ok(task);
        }

        [HttpPost]
        public async Task<ActionResult<List<Task>>> AddTask(Task task)
        {
            _context.tasks.Add(task);
            await _context.SaveChangesAsync();
            return Ok(await _context.tasks.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Task>>> UpdateTask(Task request)
        {
            var task = await _context.tasks.FindAsync(request.Id);
            if (task == null)
                return BadRequest("Task is not found");

            task.Name = request.Name;
            task.Description = request.Description;

            await _context.SaveChangesAsync();

            return Ok(await _context.tasks.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Task>> DeleteById(int id)
        {
            var task = await _context.tasks.FindAsync(id);
            if (task == null)
                return BadRequest("Task is not found");

            _context.tasks.Remove(task);
            await _context.SaveChangesAsync();

            return Ok(await _context.tasks.ToListAsync());
        }
    }
}
