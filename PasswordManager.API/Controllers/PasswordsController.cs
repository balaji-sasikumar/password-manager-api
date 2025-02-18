using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PasswordManager.Service.Interface;
using PasswordManager.Service.ViewModels;

namespace PasswordManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasswordsController : ControllerBase
    {
        private readonly ILogger<PasswordsController> _logger;
        private readonly IPasswordManagerService _passwordService;
        public PasswordsController(ILogger<PasswordsController> logger, IPasswordManagerService passwordService)
        {
            _logger = logger;
            _passwordService = passwordService;

        }
        [HttpGet]
        public async Task<ActionResult<List<EncryptedPasswordViewModel>>> GetAll()
        {
            var result = await _passwordService.GetAllAsync();
            if (result == null)
            {
                return NoContent();
            }
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _passwordService.GetByIdAsync(id);
            if (result == null)
            {
                return NoContent();
            }
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create(DecryptedPasswordViewModel externalDatabaseConfig)
        {
            var result = await _passwordService.CreateAsync(externalDatabaseConfig);
            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, DecryptedPasswordViewModel externalDatabaseConfig)
        {
            var result = await _passwordService.UpdateAsync(id, externalDatabaseConfig);
            if (!result)
            {
                return BadRequest();
            }
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _passwordService.DeleteAsync(id);
            return Ok();
        }
        [HttpGet("{id}/decrypted")]
        public async Task<IActionResult> GetDecryptedPasswordByIdAsync(int id)
        {
            var result = await _passwordService.GetDecryptedPasswordByIdAsync(id);
            if (result == null)
            {
                return NoContent();
            }
            return Ok(result);
        }
    }
}
