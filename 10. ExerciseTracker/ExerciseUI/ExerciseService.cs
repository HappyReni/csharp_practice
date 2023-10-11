namespace ExerciseUI
{
    internal class ExerciseService
    {
        public static async void ViewAllExercises()
        {
            try
            {
                var exercises = await ExerciseController.GetExercises();
                UserInterface.MakeTable(exercises,"All Exercises");
            }
            catch (Exception ex)
            {
                UserInterface.Write(ex.Message);
            }
        }
    }
}
