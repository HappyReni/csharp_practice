namespace Flashcards
{
    internal class Flashcard
    {
        public Flashcard(int id, string front, string back)
        {
            Id = id;
            Front = front;
            Back = back;
        }

        public int Id { get; set; }
        public string Front { get; set; }
        public string Back { get; set; }
    }

    internal class FlashcardDTO
    {
        public string Front { get; set; }
        public string Back { get; set; }
    }

    internal class FlashcardQuestionDTO
    {
        public string Front { get; set; }
    }
}
