using System.Drawing;

namespace Flashcards
{
    internal class Stack
    {
        public Stack(string name)
        {
            Count++;

            Id = Count;
            Name = name;
            Flashcards = new List<Flashcard>();
            CardCount = Flashcards.Count;
        }
        public Stack(int id, string name)
        {
            Count++;

            Id = id;
            Name = name;
            Flashcards = new List<Flashcard>();
            CardCount = Flashcards.Count;
        }

        public static int Count { get; private set; } = 0;
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Flashcard> Flashcards { get; private set; }
        private int CardCount { get; set; }
        public static void DownCount() => Count--;
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
        public void DeleteFlashCard()
        {
            Flashcards = new();
        }
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
