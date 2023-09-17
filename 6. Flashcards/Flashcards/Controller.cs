using System.Xml;
using System.Xml.Linq;

namespace Flashcards
{
    internal class Controller
    {
        private readonly UI ui;
        private readonly Database db;
        private SELECTOR selector;

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
            selector = ui.MainMenu();
            LoadData();
            while (true)
            {
                Action();
            }
        }
        private List<Stack> Stacks { get; set; } = new();
        private void LoadData()
        {
            Stacks = db.GetStacksFromDatabase();
        }
        private void Action()
        {
            switch (selector)
            {
                case SELECTOR.CREATE:
                    CreateStack();
                    break;
                case SELECTOR.MANAGE:
                    ManageStack();
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
        private void ManageStack()
        {
            ViewAllStacks();
            int stackID = ui.GetInput("Choose an id of stack.").val;
            int action = ui.ManageStack(Stacks[stackID-1].Name);

            switch (action)
            {
                case 1:
                    break;
                case 2:
                    CreateFlashcard(stackID);
                    break;
                default:
                    ui.Write("Invalid Input");
                    break;
            }
        }

        private void CreateFlashcard(int id)
        {
            var front = ui.GetInput("Type a front word.").str;
            var back = ui.GetInput("Type a back word.").str;
            var card = new Flashcard(id, front, back);
            if(db.Insert(card)) ui.Write($"Successfully created.");
            else ui.Write($"failed to create.");
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
        private void Study()
        {
            throw new NotImplementedException();
        }
    }
}
