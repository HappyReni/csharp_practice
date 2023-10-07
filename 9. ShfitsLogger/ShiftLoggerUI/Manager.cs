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
            UI.Clear();
            var name = UI.GetInput("Type a worker's name.").str;
            var startTime = DateTime.Parse(UI.GetInput("Type a start time of work. (YYYY-MM-dd HH:mm:ss)").str);
            var endTime = DateTime.Parse(UI.GetInput("Type a end time of work. (YYYY-MM-dd HH:mm:ss)").str);

            ShiftController.AddShift(new Shift() { Id = 0, Name = name, StartTime = startTime, EndTime = endTime });
        }

        private void ReadShift()
        {
            ViewAllShifts();
            var id = UI.GetInput("Type an ID to read.").val;
            UI.MakeTable(new List<Shift>() { ShiftController.GetShift(id).Result }, "Shift");
        }

        private void UpdateShift()
        {
            throw new NotImplementedException();
        }

        private void DeleteShift()
        {
            ViewAllShifts();
            var id = UI.GetInput("Type an ID to delete.").val;
            ShiftController.DeleteShift(id);
        }

        private void ViewAllShifts()
        {
            var shifts = ShiftController.GetShifts().Result;
            UI.MakeTable(shifts, "Shifts");
        }
    }
}
