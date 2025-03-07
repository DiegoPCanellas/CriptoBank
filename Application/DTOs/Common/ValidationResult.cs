namespace Application.DTOs.Common
{
    public class Validation
    {
        public List<string> ValidationErrors { get; set; }
        public bool Valid => ValidationErrors.Count == 0;

        public Validation(string message) 
        {
            ValidationErrors = new List<string>();

            AddError(message);
        }

        public Validation() 
        { 
            ValidationErrors = new List<string>();
        }

        public void AddError(string message)
        {
            ValidationErrors.Add(message);
        }
    }
}
