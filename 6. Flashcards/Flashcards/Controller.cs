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

            CheckDatabaseConnection();
            GetStacksFromDatabase();
            SetFlashcardsInStack();
            GetSessionsFromDatabase();

            selector = ui.MainMenu();
            while (true)
            {
                Action();
            }
        }

        private void CheckDatabaseConnection()
        {
            if (!db.isConnected)
            {
                ui.Write("Can't connect to DB. Check your connection again.");
                ui.WaitForInput();
                Environment.Exit(0);
            }
        }

        private void GetStacksFromDatabase()
        {
            Stacks = db.GetStacksFromDatabase();
            if (Stacks == null) Stacks = new();
        }

        private void SetFlashcardsInStack()
        {
            foreach(var stack in Stacks.Values)
            {
                var cards = db.SetFlashcardsInStack(stack.Id,"Load");
                stack.SetFlashcards(cards);
            }
        }

        private void GetSessionsFromDatabase()
        {
            Sessions = db.GetSessions();
            if (Sessions == null) Sessions = new();
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
            try
            {
                if (!Validation.IsUniqueStackName(name, Stacks.Keys.ToList<string>()))
                {
                    var id = db.Insert(name);
                    var stack = new Stack(id, name);
                    Stacks[name] = stack;
                    ui.Write($"{name} is created.");
                }
            }
            catch(Exception e)
            {
                var message = "Not an unique name.";
                if (e.Message == message) ui.Write($"{message} Try other names.");
                else ui.Write($"failed to create.");
            }
        }

        private void ManageStack(string name)
        {
            try
            {
                if (Validation.IsValidStackName(name, Stacks))
                {
                    int action = ui.ManageStack(name);
                    var _name = name;
                    switch ((MANAGE_SELECTOR)action)
                    {
                        case MANAGE_SELECTOR.BACK:
                            return;
                        case MANAGE_SELECTOR.VIEW:
                            ViewAllFlashcards(_name);
                            break;
                        case MANAGE_SELECTOR.CREATE:
                            CreateFlashcard(_name);
                            break;
                        case MANAGE_SELECTOR.EDIT:
                            EditFlashcard(_name);
                            break;
                        case MANAGE_SELECTOR.DELETE:
                            DeleteFlashcard(_name);
                            break;
                        case MANAGE_SELECTOR.STUDY:
                            Study(_name);
                            return;
                        case MANAGE_SELECTOR.CHANGE:
                            _name = ChangeStack();
                            ui.Write("Successfully changed.");
                            break;
                        case MANAGE_SELECTOR.DELETESTACK:
                            if (DeleteStack(_name)) return;
                            else break;
                        default:
                            ui.Write("Invalid Input");
                            break;
                    }
                    ui.WaitForInput("Press any key to continue..");
                    ManageStack(_name);
                }
            }
            catch(Exception e)
            {
                ui.Write(e.Message);
            }
        }

        private void CreateFlashcard(string name)
        {
            var front = ui.GetInput("Type a front word.").str;
            var back = ui.GetInput("Type a back word.").str;
            var card = new Flashcard(Stacks[name].Id, front, back);

            if (db.Insert(card))
            {
                Stacks[name].InsertFlashCard(card);
                ui.Write($"Successfully created.");
            }
            else ui.Write($"failed to create.");
        }

        private void EditFlashcard(string name)
        {
            ViewAllFlashcards(name);
            try
            {
                var front = ui.GetInput("Type a front word.").str;
                if (Validation.IsValidFlashcard(front, Stacks[name].Flashcards))
                {
                    var back = ui.GetInput("Type a new back word.").str;
                    var index = Stacks[name].FindFlashcard(front);
                    Flashcard card = Stacks[name].GetFlashcard(index);
                    if (db.Update(card))
                    {
                        Stacks[name].EditFlashcard(index, back);
                        ui.Write($"Successfully updated.");
                    }
                }
            }
            catch(Exception e)
            {
                ui.Write(e.Message);
            }
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
            var cards = db.SetFlashcardsInStack(Stacks[name].Id,"View");
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
            var format = "yyyy-MM-dd HH:mm:ss";
            var session = new Session(Stacks[name].Id,startTime.ToString(format), endTime.ToString(format), score, questionCount);
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
