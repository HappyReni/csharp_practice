namespace Flashcards
{
    internal class Flashcard
    {
        public Flashcard(int stackId, string front, string back)
        {
            Id = Count++;
            StackId = stackId;
            Front = front;
            Back = back;
        }

        public int Id { get; set; }
        public int StackId { get; set; }
        public string Front { get; set; }
        public string Back { get; set; }
        public static int Count { get; private set; } = 1;

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
