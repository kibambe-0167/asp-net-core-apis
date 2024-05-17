using assessment.Data;
using assessment.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace assessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly TasksDbContext _db;

        public TasksController(TasksDbContext db)
        {
            _db = db;
        }

        // get all tasks
        [HttpGet]
        public async Task<IEnumerable<Tasks>> GetTasks()
        {
            return await _db.Tasks.ToListAsync();
        }

        // get task by ID
        [HttpGet("gettask/id")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var task = await _db.Tasks.FindAsync(id);
            return task == null ? NotFound() : Ok(task);
        }

        // add a tasks
        [HttpPost]
        public async Task<IActionResult> AddTask(Tasks task)
        {
            await _db.Tasks.AddAsync(task);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTaskById), new { id = task.ID }, task);
        }

        // update a tasks
        [HttpPut("updatetask/{id}")]
        public async Task<IActionResult> UpdateTask(int id, Tasks task)
        {
            if (id != task.ID) return BadRequest();

            _db.Entry(task).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return NoContent();
        }

        // delete a task
        [HttpDelete("deletetask/{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var taskToDelete = await _db.Tasks.FindAsync(id);

            if (taskToDelete == null) return NotFound();

            _db.Tasks.Remove(taskToDelete);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        // get all expired tasks
        [HttpGet("expiredtassk")]
        public async Task<IActionResult> AllExpiredTasks()
        {
            DateTime current_date = DateTime.Now;

            // check if all data in _db context that have expired as compared to the current data
            // and return them
            var expiredTask = await _db.Tasks.Where( task => task.DueDate < current_date).ToListAsync();

            return Ok(expiredTask);
        }

        // get task that have not expired
        [HttpGet("activetasks")]
        public async Task<IActionResult> ActiveTask()
        {
            DateTime current_date = DateTime.Now;
            var activeTask = await _db.Tasks.Where( task => task.DueDate >= current_date ).ToListAsync();
            return Ok(activeTask);
        }

        // get all tasks from a certain date
        [HttpGet("searchtasks/{date}")]
        public async Task<IActionResult> CertainDateTask(DateTime date)
        {
            var taskCertainDate = await _db.Tasks.Where(task => task.DueDate == date).ToListAsync();
            return Ok(taskCertainDate);
        }
    }
}
