namespace Flashcards
{
    internal class Controller
    {
        private SELECTOR Selector { get; set; }
        private UI UI { get; set; }
        public Controller()
        {
            UI = new UI();
            Selector = UI.MainMenu();
            while (true)
            {
                Action();
            }
        }
        private void Action()
        {
            switch (Selector)
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
                    UI.Write("Invalid Input");
                    break;
            }
            Selector = UI.GoToMainMenu("Type any keys to continue.");
        }

        private void Study()
        {
            throw new NotImplementedException();
        }

        private void ManageStack()
        {
            throw new NotImplementedException();
        }

        private void CreateStack()
        {
            throw new NotImplementedException();
        }
    }
}
