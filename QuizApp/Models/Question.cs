using System;
using System.Collections.Generic;

namespace QuizApp.Models
{
    public partial class Question
    {
        public int Id { get; set; }
        public string Question1 { get; set; } = null!;
        public string FirstAnswer { get; set; } = null!;
        public string SecondAnswer { get; set; } = null!;
        public string ThirdAnswer { get; set; } = null!;
        public string FourthAnswer { get; set; } = null!;
        public string CorrectAnswer { get; set; } = null!;
        public bool IsBarterBeginnerQuestion { get; set; }
        public bool IsBarterAdvancedQuestion { get; set; }
        public bool IsServerBeginnerQuestion { get; set; }
        public bool IsServerAdvancedQuestion { get; set; }
        public bool IsCashierQuestion { get; set; }
    }
}
