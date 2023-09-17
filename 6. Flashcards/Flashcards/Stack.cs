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
        public Stack(int id, string name)
        {
            Id = id;
            Name = name;
            Flashcards = new List<Flashcard>();
            CardCount = Flashcards.Count;
            Count++;
        }

        public static int Count { get; private set; } = 1;
        public int Id { get; private set; }
        public string Name { get; set; }
        private List<Flashcard> Flashcards { get; set; }
        private int CardCount { get; set; }
        public List<object> GetField() => new List<object> { Id, Name };
        private bool CreateTable() { throw new Exception(); }
        private void DeleteTable() { throw new Exception(); }
        public void InsertFlashCard(Flashcard card) { Flashcards.Add(card); }
        public bool DeleteFlashCard() { throw new Exception(); }
        public List<FlashcardDTO> ShowStack() { throw new Exception(); }
    }
}
