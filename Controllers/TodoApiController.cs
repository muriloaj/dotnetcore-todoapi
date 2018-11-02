using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TodoApi.Models;

namespace TodoApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoController(TodoContext context)
        {
            _context = context;
        }

        #region CRUD Controllers

        #region Retrieve - List All

        /// <summary>
        /// List All item saved
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/Todo/list
        ///
        ///
        /// </remarks>
        /// <param name="item"></param>
        /// <returns>List of TodoItems in Database(memory)</returns>
        /// <response code="200">List of TodoItems</response>
        [ProducesResponseType(200)]
        [HttpGet("list", Name = "ListTodo")]
        public ActionResult<List<TodoItem>> GetAll()
        {
            return _context.TodoItems.ToList();
        }

        #endregion

        #region Retrieve - Get by {id}

        /// <summary>
        /// Get a TodoItem by Id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/Todo/get/{id}
        ///
        /// </remarks>
        /// <param name="id">TodoItem id</param>
        /// <returns>The TodoItem's requested</returns>
        /// <response code="200">Returns the TodoItem requested using the its ID</response>
        /// <response code="404">If the item could not be find on Database(memory)</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpGet("get/{id}", Name = "GetTodo")]
        [ProducesResponseType(200)]
        public ActionResult<TodoItem> GetById(long id)
        {
            var item = _context.TodoItems.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        #endregion

        #region Create or Update - save or update a TodoItem

        /// <summary>
        /// Create or Update a TodoItem.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/Todo/save
        ///     {
        ///       "id": 0,
        ///       "name": "string",
        ///       "isComplete": true
        ///     }
        ///
        /// </remarks>
        /// <param name="item"> see TodoItem's model</param>
        /// <returns>Returns the save proccess status</returns>
        /// <response code="200">Returns the save proccess worked properly</response>
        [ProducesResponseType(200)]
        [HttpPost("save", Name = "SaveTodo")]
        public ActionResult Save(TodoItem item)
        {
            TodoItem lookup=_context.TodoItems.Find(item.Id);

            if ( lookup != null)
            {
              _context.TodoItems.Remove(lookup);
            }
            _context.TodoItems.Add(item);
            _context.SaveChanges();

            return Ok();
        }

        #endregion

        #region Delete - Delete item by {id}

        /// <summary>
        /// Remove a TodoItem by Id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/Todo/remove/{id}
        ///
        /// </remarks>
        /// <param name="id">TodoItem's id</param>
        /// <returns>The TodoItem deletion status</returns>
        /// <response code="200">Returns the TodoItem was successfully removed</response>
        /// <response code="404">The TodoItem could not be found on Database(memory)</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]

        [HttpDelete("remove/{id}", Name = "RemoveTodo")]
        public ActionResult RemoveById(long id)
        {
            var item = _context.TodoItems.Find(id);

            if (item == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(item);
            _context.SaveChanges();

            return Ok();

        }

        #endregion
        
        #endregion

    }

}
