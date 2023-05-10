using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizApp.Models;

namespace QuizApp.Controllers
{
    [Authorize]
    public class QuestionsController : Controller
    {
        private readonly ILogger<QuestionsController> _logger;
        private readonly sigmunds_QuizAppContext _context;

        public QuestionsController(
            ILogger<QuestionsController> logger,
            sigmunds_QuizAppContext context)
        {
            _logger = logger;
            _context = context;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var questions = await _context.Questions.ToListAsync();

            return View(questions);
        }

        public async Task<IActionResult> Create()
        {
            return View( new Question() );
        }

        [HttpPost]
        public async Task<IActionResult> Create(Question question)
        {
            if(question == null)
            {
                return NotFound();
            }
            if(ModelState.IsValid)
            {
                _context.Questions.Add(question);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(question);
        }

        
        public async Task<IActionResult> Edit(int id)
        {
            var question = await _context.Questions.Where(_ => _.Id == id).FirstOrDefaultAsync();
            return View(question);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Question question)
        {
            
            var originalQuestion = _context.Questions.Find(id);

            originalQuestion.Question1 = question.Question1;
            originalQuestion.FirstAnswer = question.FirstAnswer;
            originalQuestion.SecondAnswer = question.SecondAnswer;
            originalQuestion.ThirdAnswer = question.ThirdAnswer;
            originalQuestion.FourthAnswer= question.FourthAnswer;
            originalQuestion.IsBarterBeginnerQuestion = question.IsBarterBeginnerQuestion;
            originalQuestion.IsBarterAdvancedQuestion = question.IsBarterAdvancedQuestion;
            originalQuestion.IsServerBeginnerQuestion = question.IsServerBeginnerQuestion;
            originalQuestion.IsServerAdvancedQuestion = question.IsServerAdvancedQuestion;
            originalQuestion.IsCashierQuestion = question.IsCashierQuestion;

            await _context.SaveChangesAsync();

            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            return View(question);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = new Question();
            var questionToDelete = _context.Questions.Find(id);

            _context.Questions.Remove(questionToDelete);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
