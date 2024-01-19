using Microsoft.AspNetCore.Mvc;
using ForumService.Data;
using ForumService.Models;
using Microsoft.AspNetCore.Authorization;
using Audiomind.RabbitMQ;
using MassTransit.Transports;
using MassTransit;
using Audiomind.RabbitMQ.Moddels;

namespace ForumService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ForumController : ControllerBase
    {
        private ISqlForum _sqlForum;
        private readonly ILogger<ForumController> _logger;
        private readonly IMessageProducer _messagePublisher;
        private readonly IPublishEndpoint _publishEndpoint;

        public ForumController(ILogger<ForumController> logger, ISqlForum sqlForum, IMessageProducer messageProducer, IPublishEndpoint publishEndpoint)
        {
            _logger = logger;
            _sqlForum = sqlForum;
            _messagePublisher = messageProducer;
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost("", Name = "Create"), Authorize]
        public IActionResult Create(Forum forum)
        {
            if (forum == null) return BadRequest();
            else if (forum.title == null || forum.title == string.Empty) return BadRequest("Title can't be empty");
            else if (forum.description == null || forum.description == string.Empty) return BadRequest("Description can't be empty.");

            Forum createdForum = _sqlForum.AddForum(forum);
            if (createdForum == null) return BadRequest("Forum already exists!");
            _messagePublisher.SendMessage(new ForumMessage() { userId = createdForum.userId.ToString(), forumId = createdForum.id.ToString() }, _publishEndpoint);
            return Created("Forum database", createdForum);
        }

        [HttpGet("{forumId}", Name = "Read")]
        public IActionResult GetForum(string forumId)
        {
            if (forumId == null || forumId == string.Empty) return BadRequest("Email field can't be empty.");


            Forum foundForum = _sqlForum.GetForum(forumId);
            if (foundForum == null) return NotFound("Forum doesn't exists!");

            return Ok(foundForum);
        }

        [HttpGet("all", Name = "ReadAll")]
        public IActionResult GetAll()
        {
            List<ForumView> foundForum = _sqlForum.GetForums();
            if (foundForum == null || foundForum.Count == 0) return NotFound("No forums exist in the database.");

            return Ok(foundForum);
        }

        [HttpPatch("", Name = "Update"), Authorize]
        public IActionResult Update(Forum forum)
        {
            if (forum.id.ToString() == null || forum.id.ToString() == string.Empty) return BadRequest("Forum field can't be empty.");
            if (User.Claims.Contains(new System.Security.Claims.Claim("forums", forum.id.ToString())) || User.IsInRole("administrator"))
            {
                Forum existing = _sqlForum.GetForum(forum.id.ToString());
                if (existing == null) return NotFound("Couldn't find forum.");

                existing.updateDate = DateTime.Now;
                existing.description = forum.description;
                existing.title = forum.title;
                Forum success = _sqlForum.UpdateForum(existing);

                if (success == null) return Problem("Couldn' t update forum", string.Empty, 500);
                return Ok("Forum updated");
            }
            else return Unauthorized();
        }

        [HttpDelete("", Name = "Delete"), Authorize]
        public IActionResult DeleteForum(string forumId)
        {
            if (forumId == null || forumId == string.Empty) return BadRequest("Forum field can't be empty.");
            bool success = false;
            if (User.Claims.Contains(new System.Security.Claims.Claim("forums", forumId)) || User.IsInRole("administrator"))
                success = _sqlForum.DeleteForum(forumId);
            else return Unauthorized();

            if (!success) return Problem("Couldn't delete forum", string.Empty, 500);
            return Ok("Forum deleted!");
        }
    }
}