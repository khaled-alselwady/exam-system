﻿using System.Collections.Generic;

namespace ExamSystem.Entities
{
    public class Question : BaseEntity
    {
        public string Text { get; set; }

        public byte SubjectId { get; set; }
        public Subject Subject { get; set; }

        // One-to-Many relationship with Options
        public ICollection<Option> Options { get; set; } = new List<Option>();
    }
}
