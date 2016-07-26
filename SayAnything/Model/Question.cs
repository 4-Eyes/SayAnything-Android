using System.Collections.Generic;

namespace SayAnything.Model
{
    public class Question
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int CreatorId { get; set; }

        public string Text { get; private set; }
        public IEnumerable<Answer> Answers { get; set; }

        public Question(User creator, string questionText)
        {
            CreatorId = creator.Id;
            Text = questionText;
        }
    }
}