namespace Flashcards
{
    internal class Stack
    {
        public Stack(string name)
        {
            Id = Count++;
            Name = name;
            Flashcards = new List<Flashcard>();
            CardCount = Flashcards.Count;
        }

        public static int Count { get; private set; } = 1;
        public int Id { get; private set; }
        public string Name { get; set; }
        private List<Flashcard> Flashcards { get; set; }
        private int CardCount { get; set; }
        private bool CreateTable() { throw new Exception(); }
        private void DeleteTable() { throw new Exception(); }
        public bool InsertFlashCard() { throw new Exception(); }
        public bool DeleteFlashCard() { throw new Exception(); }
        public List<FlashcardDTO> ShowStack() { throw new Exception(); }
    }
}
