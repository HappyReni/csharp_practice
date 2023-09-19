using System.Drawing;

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
        public void SetFlashcards(List<Flashcard> flashcards) => Flashcards = flashcards;
        public int FindFlashcard(string front) 
        {
            for (int i = 0; i < Flashcards.Count; i++)
            {
                if (Flashcards[i].Front == front)
                {
                    return i;
                }
            }
            return -1;
        }
        public void InsertFlashCard(Flashcard card) { Flashcards.Add(card); }
        public bool DeleteFlashCard(int idx) 
        {
            try
            {
                Flashcards.RemoveAt(idx);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool EditFlashcard(int idx, string back)
        {
            try
            {
                Flashcards[idx].Back = back;
                return true;
            }
            catch
            {
                return false;
            }
        }
        public Flashcard? GetFlashcard(int idx)
        {
            try
            {
                return Flashcards[idx];
            }
            catch
            {
                return null;
            }
        }
        public List<FlashcardDTO> ShowStack() { throw new Exception(); }


    }
}
