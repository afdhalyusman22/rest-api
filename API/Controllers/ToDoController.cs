using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Repository;
using API.Models;
using API.Models.Todo;

namespace API.Controllers
{
    [Route("api/ToDo")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private ITodoRepository _todoRepository { get; set; }
        public ToDoController(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }
        /// <summary>
        /// Get All Todo 
        /// </summary>
        /// <returns></returns>
        [Route("GetAll"), HttpGet]
        public async Task<IActionResult> GetAll()
        {
            CommonResponse response = await _todoRepository.GetAll();
            return Ok(response);
        }
        /// <summary>
        /// Get Todo List By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("GetById"), HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            CommonResponse response = await _todoRepository.GetById(id);
            return Ok(response);
        }
        /// <summary>
        /// Get Incoming Todo
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("GetIncomingTodo"), HttpPost]
        public async Task<IActionResult> GetIncomingTodo(RequestIncomingTodo request)
        {
            CommonResponse response = await _todoRepository.GetIncomingTodo(request);
            return Ok(response);
        }

        /// <summary>
        /// Add Todo 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("AddToDo"), HttpPost]
        public async Task<IActionResult> AddToDo(RequestAddTodo request)
        {
            CommonResponse response = await _todoRepository.AddTodo(request);
            return Ok(response);
        }
        /// <summary>
        /// Update Todo 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("UpdateToDo"), HttpPost]
        public async Task<IActionResult> UpdateToDo(RequestUpdateToDo request)
        {
            CommonResponse response = await _todoRepository.UpdateTodo(request);
            return Ok(response);
        }
        
        /// <summary>
        /// Delete Todo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("DeleteTodo"), HttpGet]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            CommonResponse response = await _todoRepository.DeleteTodo(id);
            return Ok(response);
        }

        /// <summary>
        /// Update Complete Todo
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("UpdateComplete"), HttpPost]
        public async Task<IActionResult> UpdateComplete(RequestCompleteToDo request)
        {
            CommonResponse response = await _todoRepository.UpdateComplete(request);
            return Ok(response);
        }

        /// <summary>
        /// Update todo Done
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("UpdateDone"), HttpPost]
        public async Task<IActionResult> UpdateDone(RequestDoneToDo request)
        {
            CommonResponse response = await _todoRepository.UpdateDoneTodo(request);
            return Ok(response);
        }
    }
}