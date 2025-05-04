using Microsoft.AspNetCore.Mvc;

namespace VirtualMachinesCore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        /// <summary>
        /// Handles the result of an operation and returns the appropriate HTTP response
        /// </summary>
        /// <typeparam name="T">The type of the result</typeparam>
        /// <param name="result">The result to handle</param>
        /// <returns>An IActionResult based on the result</returns>
        protected IActionResult HandleResult<T>(T result)
        {
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Handles the result of an operation and returns the appropriate HTTP response
        /// </summary>
        /// <typeparam name="T">The type of the result</typeparam>
        /// <param name="result">The result to handle</param>
        /// <returns>An IActionResult based on the result</returns>
        protected IActionResult HandleResult<T>(IEnumerable<T> result)
        {
            if (result == null || !result.Any())
                return NotFound();
            return Ok(result);
        }
    }
} 