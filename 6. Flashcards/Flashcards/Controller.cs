namespace Flashcards
{
    internal class Controller
    {
        private UI ui;
        private SELECTOR selector;

        public Controller()
        {
            ui = new UI();
            selector = ui.MainMenu();
            while (true)
            {
                Action();
            }
        }

        private List<Stack> Stacks { get; set; }
      
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
            var name = ui.CreateTable();
            Stacks.Add(new Stack(name));
            ui.Write($"{name} is created.");
            ui.GoToMainMenu();
        }
        private void ManageStack()
        {
            throw new NotImplementedException();
        }
        private void Study()
        {
            throw new NotImplementedException();
        }
    }
}
