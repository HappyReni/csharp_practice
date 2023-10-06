using ShiftLoggerUI.Models;
using ShiftLoggerUI.Data;

namespace ShiftLoggerUI
{
    public class Manager
    {
        private UI Ui { get; set; }
        private SELECTOR Selector { get; set; }
        public Manager()
        {
            Ui = new UI();
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
                    CreateShift();
                    break;
                case SELECTOR.READ:
                    ReadShift();
                    break;
                case SELECTOR.UPDATE:
                    UpdateShift();
                    break;
                case SELECTOR.DELETE:
                    DeleteShift();
                    break;
                case SELECTOR.VIEWALL:
                    ViewAllShifts();
                    break;
                case SELECTOR.EXIT:
                    Environment.Exit(0);
                    break;
                default:
                    UI.Write("Invalid Input");
                    break;
            }
            Selector = Ui.GoToMainMenu("Type any keys to continue.");
        }

        private void CreateShift()
        {
            throw new NotImplementedException();
        }

        private void ReadShift()
        {
            throw new NotImplementedException();
        }

        private void UpdateShift()
        {
            throw new NotImplementedException();
        }

        private void DeleteShift()
        {
            throw new NotImplementedException();
        }

        private void ViewAllShifts()
        {
            var shifts = ShiftController.GetShifts().Result;
            var tableList = new List<List<Shift>>
            {
                shifts
            };
            UI.MakeTable(shifts, "Shifts");
        }
    }
}
