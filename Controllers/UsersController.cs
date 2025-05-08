using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Prueba.DTOs;
using Prueba.Models;
using Prueba.Repositories.Interfaces;
using Prueba.Services;

namespace Prueba.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowAll")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly UserService _userService;
        private readonly IValidator<UserDTO> _validator;

        public UsersController(IUserRepository userRepository, UserService userService, IValidator<UserDTO> validator)
        {
            _userRepository = userRepository;
            _userService = userService;
            _validator = validator;
        }

        // GET: api/users
        [HttpGet]
        public ActionResult<IEnumerable<UserResponseDTO>> GetAllUsers()
        {
            var users = _userRepository.GetAllUsers();

            var result = users.Select(u => new UserResponseDTO
            {
                Id = u.ID,
                Name = u.Name,
                LastName = u.LastName,
                Address = u.Address,
                TellNumber = u.TellNumber,
                BirthDay = u.BirthDay,
                DocumentID = u.DocumentID
            });

            return Ok(result);
        }

        // POST: api/users
        [HttpPost]
        public ActionResult CreateUser([FromBody] UserDTO dto)
        {
            var validationResult = _validator.Validate(dto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var user = new User
            {
                Name = dto.Name,
                LastName = dto.LastName,
                Address = dto.Address,
                TellNumber = dto.TellNumber,
                BirthDay = dto.BirthDay,
                DocumentID = dto.DocumentID
            };

            _userRepository.CreateUser(user);
            return Ok(new { message = "User created successfully." });
        }

        // PUT: api/users/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateUser(int id, [FromBody] UserDTO dto)
        {
            var validationResult = _validator.Validate(dto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var existing = _userService.GetUserById(id);
            if (existing == null)
                return NotFound($"User with id {id} not found.");

            var beforeUpdate = new UserResponseDTO
            {
                Id = existing.ID,
                Name = existing.Name,
                LastName = existing.LastName,
                Address = existing.Address,
                TellNumber = existing.TellNumber,
                BirthDay = existing.BirthDay,
                DocumentID = existing.DocumentID
            };

            var user = new User
            {
                ID = id,
                Name = dto.Name,
                LastName = dto.LastName,
                Address = dto.Address,
                TellNumber = dto.TellNumber,
                BirthDay = dto.BirthDay,
                DocumentID = dto.DocumentID
            };

            _userRepository.UpdateUser(id, user);

            var afterUpdate = _userService.GetUserById(id);
            var afterUpdateDTO = new UserResponseDTO
            {
                Id = afterUpdate.ID,
                Name = afterUpdate.Name,
                LastName = afterUpdate.LastName,
                Address = afterUpdate.Address,
                TellNumber = afterUpdate.TellNumber,
                BirthDay = afterUpdate.BirthDay,
                DocumentID = afterUpdate.DocumentID
            };

            if (beforeUpdate.Name == afterUpdateDTO.Name && beforeUpdate.LastName == afterUpdateDTO.LastName)
                return BadRequest("No changes were made to the user.");

            return Ok(new
            {
                message = "User updated successfully.",
                before = beforeUpdate,
                after = afterUpdateDTO
            });
        }

        // DELETE: api/users/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
        {
            try
            {
                var existing = _userService.GetUserById(id);
                if (existing == null)
                    return NotFound(new { message = "User not found." });

                _userRepository.DeleteUser(id);
                return Ok(new { message = "User deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error deleting user.", error = ex.Message });
            }
        }
    }
}