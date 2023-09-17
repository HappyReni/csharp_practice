﻿namespace Flashcards
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
            db.Insert(stack);
            Stacks.Add(stack);
            ui.Write($"{name} is created.");
        }
        private void ManageStack()
        {
            foreach (var stack in Stacks)
            {
                ui.Write($"{stack.Id} : {stack.Name}");
            }
        }
        private void Study()
        {
            throw new NotImplementedException();
        }
    }
}
