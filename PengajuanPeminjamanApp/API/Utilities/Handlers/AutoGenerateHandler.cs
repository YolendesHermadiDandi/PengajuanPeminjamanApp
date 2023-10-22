namespace API.Utilities.Handlers
{
    public class AutoGenerateHandler
    {
        public static string GenerateNik(string? nik = null)
        {
            if (nik is null)
            {
                return "111111"; // First employee
            }

            var generateNik = Convert.ToInt32(nik) + 1;

            return generateNik.ToString();
        }
    }
}
