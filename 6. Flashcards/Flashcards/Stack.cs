namespace Flashcards
{
    internal class Stack
    {
        private int Id { get; set; }

        public string Name { get; set; }
        private List<Flashcard> Flashcards { get; set; }
        private int CardCount { get; set; }
        private bool CreateTable() { throw new Exception(); }
        public bool InsertFlashCard() { throw new Exception(); }
        public bool DeleteFlashCard() { throw new Exception(); }
        public List<FlashcardDTO> ShowStack() { throw new Exception(); }
    }
}
