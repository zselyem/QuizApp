using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizApp.Models;

namespace QuizApp.Controllers
{
    [Authorize]
    public class ResultController : Controller
    {
        private readonly ILogger<ResultController> _logger;
        private readonly sigmunds_QuizAppContext _context;

        public ResultController(
            ILogger<ResultController> logger,
            sigmunds_QuizAppContext context)
        {
            _logger = logger;
            _context = context;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var results = await _context.Tests.ToListAsync();
            return View(results);
        }
    }
}
