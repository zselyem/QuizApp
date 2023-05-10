using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizApp.Models;

namespace QuizApp.Controllers
{

    [Authorize]
    public class QuizController : Controller
    {
        private readonly ILogger<QuizController> _logger;
        private readonly sigmunds_QuizAppContext _context;

        public QuizController(
            ILogger<QuizController> logger,
            sigmunds_QuizAppContext context)
        {
            _logger = logger;
            _context = context;
        }

        [Authorize]
        public IActionResult Index()
        {
            var categories = new Dictionary<string,string>()
            {
                {"Felszolgáló alapteszt", "Felszolgalo alapteszt"},
                {"Felszolgáló haladó teszt", "Felszolgalo halado teszt"},
                {"Pultos alapteszt", "Pultos alapteszt"},
                {"Pultos haladó teszt", "Pultos halado teszt"},
                {"Kasszás teszt", "Kasszas teszt"},
            };

            return View(categories);
        }
        [Authorize]
        public async Task<IActionResult> Quiz(string category)
        {
            var questions = new List<Question>();
            switch (category)
            {
                case "Felszolgalo alapteszt":
                    questions = await _context.Questions.Where(_ => _.IsServerBeginnerQuestion == true).ToListAsync();
                    break;
                case "Felszolgalo halado teszt":
                    questions = await _context.Questions.Where(_ => _.IsServerAdvancedQuestion == true).ToListAsync();
                    break;
                case "Pultos alapteszt":
                    questions = await _context.Questions.Where(_ => _.IsBarterBeginnerQuestion == true).ToListAsync();
                    break;
                case "Pultos halado teszt":
                    questions = await _context.Questions.Where(_ => _.IsBarterAdvancedQuestion == true).ToListAsync();
                    break;
                case "Kasszas teszt":
                    questions = await _context.Questions.Where(_ => _.IsCashierQuestion == true).ToListAsync();
                    break;
                default:
                    break;
            }
            var rnd = new Random();
            var randomQuestions = questions.OrderBy(item => rnd.Next());

            //itt kell majd az első x darab kérdést venni


            ViewBag.Category = category;

            return View(randomQuestions);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Send(string name, string restaurant, int correctAnswers, string strDate, string category)
        {
            var test = new Test()
            {
                Name= name,
                Restaurant= restaurant,
                Category = category,
                CorrectAnswers=correctAnswers,
                Date= DateTime.Now,
                QuizId = name + restaurant + strDate,
                Passed=null,
            };
            _context.Tests.Add(test);

            await _context.SaveChangesAsync();

            return RedirectToAction("Quiz");
        }
    }
}
