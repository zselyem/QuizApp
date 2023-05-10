using System;
using System.Collections.Generic;

namespace QuizApp.Models
{
    public partial class Test
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Restaurant { get; set; } = null!;
        public int CorrectAnswers { get; set; }
        public DateTime Date { get; set; }
        public string QuizId { get; set; } = null!;
        public bool? Passed { get; set; }
        public string Category { get; set; } = null!;
    }
}
