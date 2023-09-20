using System;
using System.Xml;
using System.Xml.Linq;

namespace Flashcards
{
    internal class Controller
    {
        private readonly UI ui;
        private readonly Database db;
        private SELECTOR selector;
        private List<Stack> Stacks { get; set; } = new();

        public Controller()
        {
            ui = new UI();
            db = new Database();

            if(!db.isConnected)
            {
                ui.Write("Can't connect to DB. Check your connection again.");
                ui.WaitForInput();
                Environment.Exit(0);
            }
            LoadData();
            selector = ui.MainMenu();
            while (true)
            {
                Action();
            }
        }
        private void LoadData()
        {
            Stacks = db.GetStacksFromDatabase();
            foreach(var stack in Stacks)
            {
                var cards = db.GetFlashcardsInStack(stack.Id,"Load");
                stack.SetFlashcards(cards);
            }
        }
        private void Action()
        {
            switch (selector)
            {
                case SELECTOR.CREATE:
                    CreateStack();
                    break;
                case SELECTOR.MANAGE:
                    ViewAllStacks();
                    int stackId = ui.GetInput("Choose an id of stack.").val;
                    ManageStack(stackId);
                    break;
                case SELECTOR.STUDY:
                    Study();
                    break;
                case SELECTOR.EXIT:
                    Environment.Exit(0);
                    break;
                default:
                    ui.Write("Invalid Input");
                    break;
            }
            selector = ui.GoToMainMenu("Type any keys to continue.");
        }

        private void CreateStack()
        {
            var name = ui.CreateStack();
            var stack = new Stack(name);
            Stacks.Add(stack);
            if(db.Insert(stack)) ui.Write($"{name} is created.");
            else ui.Write($"failed to create.");
        }

        private void ManageStack(int stackId)
        {
            int action = ui.ManageStack(Stacks[stackId - 1].Name);
            var _stackId = stackId;
            switch (action)
            {
                case 1:
                    ViewAllFlashcards(_stackId);
                    break;
                case 2:
                    CreateFlashcard(_stackId);
                    break;
                case 3:
                    EditFlashcard(_stackId);
                    break;
                case 4:
                    DeleteFlashcard(_stackId);
                    break;
                case 5:
                    _stackId = ChangeStack();
                    ui.Write("Successfully changed.");
                    break;
                case 0:
                    return;
                default:
                    ui.Write("Invalid Input");
                    break;
            }
            ui.WaitForInput("Press any key to continue..");
            ManageStack(_stackId);
        }


        private void CreateFlashcard(int stackId)
        {
            var front = ui.GetInput("Type a front word.").str;
            var back = ui.GetInput("Type a back word.").str;
            var card = new Flashcard(stackId, front, back);
            Stacks[stackId - 1].InsertFlashCard(card);
            if (db.Insert(card)) ui.Write($"Successfully created.");
            else ui.Write($"failed to create.");
        }

        private void EditFlashcard(int stackId)
        {
            ViewAllFlashcards(stackId);
            var front = ui.GetInput("Type a front word.").str;
            var back = ui.GetInput("Type a new back word.").str;
            var idx = Stacks[stackId - 1].FindFlashcard(front);
            Stacks[stackId - 1].EditFlashcard(idx,back);
            Flashcard card = Stacks[stackId - 1].GetFlashcard(idx);
            if (db.Update(card)) ui.Write($"Successfully updated.");
            else ui.Write($"failed to update.");
        }
        private void DeleteFlashcard(int stackId)
        {
            ViewAllFlashcards(stackId);
            var front = ui.GetInput("Type a front word to delete.").str;
            var idx = Stacks[stackId - 1].FindFlashcard(front);
            Stacks[stackId - 1].DeleteFlashCard(idx);
            Flashcard.DownCount();
            if (db.Delete(idx+1)) ui.Write($"Successfully deleted.");
            else ui.Write($"failed to delete.");
        }

        private int ChangeStack()
        {
            Console.Clear();
            return ui.GetInput("Type an ID of Stack to change.").val;
        }

        private void ViewAllStacks()
        {
            List<List<object>> stackList = new();
            foreach (var stack in Stacks)
            {
                stackList.Add(stack.GetField());
            }
            ui.MakeTable(stackList, "stack");
        }

        private void ViewAllFlashcards(int stackID)
        {
            var cards = db.GetFlashcardsInStack(stackID,"View");
            List<List<object>> cardList = new();

            if (cards == null) 
            { 
                ui.Write("The stack is empty.");
                return;
            }

            foreach (var card in cards)
            {
                cardList.Add(new List<object> { card.DTO.Front, card.DTO.Back });
            }
            ui.MakeTable(cardList, "Flashcard");

        }

        private void Study()
        {
            throw new NotImplementedException();
        }
    }
}
