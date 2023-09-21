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
        private Dictionary<string,Stack> Stacks { get; set; } = new();
        private List<Session> Sessions { get; set; } = new();

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
            foreach(var stack in Stacks.Values)
            {
                var cards = db.GetFlashcardsInStack(stack.Id,"Load");
                stack.SetFlashcards(cards);
            }
            Sessions = db.GetSessions();
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
                    string name = ui.GetInput("Choose a name of stack.").str;
                    ManageStack(name);
                    break;
                case SELECTOR.STUDY:
                    ViewAllSessions();
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
            var id = db.Insert(name);
            if (id != -1)
            {
                ui.Write($"{name} is created.");
                var stack = new Stack(id,name);
                Stacks[name] = stack;
            }
            else
            {
                ui.Write($"failed to create.");
            }
        }

        private void ManageStack(string name)
        {
            int action = ui.ManageStack(name);
            var _name = name;
            switch (action)
            {
                case 1:
                    ViewAllFlashcards(_name);
                    break;
                case 2:
                    CreateFlashcard(_name);
                    break;
                case 3:
                    EditFlashcard(_name);
                    break;
                case 4:
                    DeleteFlashcard(_name);
                    break;
                case 5:
                    Study(_name);
                    return;
                case 6:
                    _name = ChangeStack();
                    ui.Write("Successfully changed.");
                    break;
                case 7:
                    if (DeleteStack(_name)) return;
                    else break;
                case 0:
                    return;
                default:
                    ui.Write("Invalid Input");
                    break;
            }
            ui.WaitForInput("Press any key to continue..");
            ManageStack(_name);
        }

        private void CreateFlashcard(string name)
        {
            var front = ui.GetInput("Type a front word.").str;
            var back = ui.GetInput("Type a back word.").str;
            var card = new Flashcard(Stacks[name].Id, front, back);
            Stacks[name].InsertFlashCard(card);
            if (db.Insert(card)) ui.Write($"Successfully created.");
            else ui.Write($"failed to create.");
        }

        private void EditFlashcard(string name)
        {
            ViewAllFlashcards(name);
            var front = ui.GetInput("Type a front word.").str;
            var back = ui.GetInput("Type a new back word.").str;
            var index = Stacks[name].FindFlashcard(front);

            Stacks[name].EditFlashcard(index, back);
            Flashcard card = Stacks[name].GetFlashcard(index);
            if (db.Update(card)) ui.Write($"Successfully updated.");
            else ui.Write($"failed to update.");
        }
        private void DeleteFlashcard(string name)
        {
            ViewAllFlashcards(name);
            var front = ui.GetInput("Type a front word to delete.").str;
            var index = Stacks[name].FindFlashcard(front);
            var id = Stacks[name].Flashcards[index].Id;

            if (db.Delete(id)) 
            {
                ui.Write($"Successfully deleted.");
                Stacks[name].DeleteFlashCard(index);
                foreach (var stack in Stacks.Values)
                {
                    stack.UpdateFlashcardID(id);
                }
                Flashcard.DownCount();

            }
            else ui.Write($"failed to delete.");
        }

        private string ChangeStack()
        {
            Console.Clear();
            ViewAllStacks();
            return ui.GetInput("Type an ID of Stack to change.").str;
        }

        private bool DeleteStack(string name)
        {
            Console.Clear();
            var res = ui.GetInput("Are you sure to delete this stack? (Y)").str;
            if (res == "Y")
            {
                if (db.Delete(name))
                {
                    Stacks[name].DeleteFlashCard();
                    Stacks.Remove(name);
                    Stack.DownCount();
                    db.UpdateID();
                    ui.Write("Successfully deleted.");
                    return true;
                }
                else
                {
                    ui.Write("Failed to delete.");
                    return false;
                }
            }
            else
            {
                ui.Write("Failed to delete.");
                return false;
            }
        }

        private void ViewAllStacks()
        {
            List<List<object>> stackList = new();
            var sorted = from item in Stacks
                         orderby item.Value.Id ascending
                         select item.Value;

            foreach (var stack in sorted)
            {
                stackList.Add(stack.GetField());
            }
            ui.MakeTable(stackList, "stack");
        }

        private void ViewAllFlashcards(string name)
        {
            var cards = db.GetFlashcardsInStack(Stacks[name].Id,"View");
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
            ui.MakeTable(cardList, "Flashcards");
        }

        private void Study(string name)
        {
            Console.Clear();
            var cards = Stacks[name].Flashcards;
            var questionCount = cards.Count();
            var score = 0;
            var startTime = DateTime.Now;

            ui.Write("Guess the back words.");

            foreach (var card in cards)
            {
                var front = card.QuestionDTO.Front;
                var answer = ui.GetInput(front).str;

                if (answer == card.Back)
                {
                    score++;
                    ui.WaitForInput("Correct!");
                }
                else 
                {
                    ui.WaitForInput("Wrong answer!");
                }
                Console.Clear();
            }
            var endTime = DateTime.Now;
            Console.Clear() ;
            ui.Write("Study Finished!");
            ui.Write($"Your score is {score} out of {questionCount} questions");
            
            var session = new Session(Stacks[name].Id,startTime, endTime, score, questionCount);
            Sessions.Add(session);

            db.Insert(session);
        }
        private void ViewAllSessions()
        {
            Console.Clear();
            List<List<object>> tableData = new();

            foreach (var session in Sessions)
            {
                var sessionData = session.GetField();
                sessionData[0] = db.SearchStackName((int)sessionData[0], "Stack");
                if (sessionData[0] != null) tableData.Add(sessionData);
            }
            ui.MakeTable(tableData, "Sessions");
        }
    }
}
