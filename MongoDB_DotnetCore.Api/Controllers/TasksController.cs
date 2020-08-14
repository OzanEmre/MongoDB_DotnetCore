using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MongoDB_DotnetCore.Repository;
using MongoDB_DotnetCore.Model;
using Newtonsoft;

namespace MongoDB_DotnetCore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        public MongoRepository _mongoRepository {get; set;}
        public TasksController(MongoRepository mongoRepository){
            _mongoRepository = mongoRepository;
        }
        // GET api/tasks
        [HttpGet]
        public ActionResult Get()
        {
            var list = _mongoRepository.GetTasks();
            var result = new JsonResultModel(list, "", true);
            return Ok(result);
        }

        // GET api/tasks/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var item = _mongoRepository.GetTask(id);
            var result = new JsonResultModel(item, "", true);
            return Ok(result);   
        }

        // POST api/tasks/add
        [HttpPost("add")]
        public ActionResult Add([FromBody] TaskModel model)
        {
            var result = new { Success = false, Message = "", Result = "" };
            try{
                _mongoRepository.AddTask(model);
                result = new { Success = true, Message = $"Insert success, {model.ID} add", Result = "" };
            }
            catch(Exception ex){
                result = new { Success = false, Message = "Insert fail", Result = ex.ToString() };
            }            
            return Ok(result);   
        }

        // POST api/tasks/update
        [HttpPut("update")]
        public ActionResult Update([FromBody] TaskModel model)
        {            
            var result = new { Success = false, Message = "Update fail", Result = "" };
            try{
                _mongoRepository.UpdateTask(model);
                result = new { Success = true, Message = $"Update success, {model.ID} updated", Result = "" };
            }
            catch{
            }
            return Ok(result);   
        }

        // DELETE api/tasks/delete
        [HttpDelete("delete")]
        public ActionResult Delete(int id)
        {
            var result = new { Success = true, Message = "Delete fail", Result = "" };
            try{
                _mongoRepository.DeleteTask(id);
                result = new { Success = true, Message = $"Delete success, {id} updated", Result = "" };
            }
            catch{
            }
            return Ok(result);   
        }
    }
}
