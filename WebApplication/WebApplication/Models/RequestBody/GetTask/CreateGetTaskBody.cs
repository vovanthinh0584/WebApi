namespace WebApplication.Models.RequestBody.GetTask
{
    public class CreateGetTaskBody
    {
        public string WorkNo { get; set; }
        public string Status { get; set; }
        public string VerifierInformation { get; set; }
        public string Reason { get; set; }
        public string ErrorMessage { get; set; }
        public string UserId { get; set; }
        public string BUID { get; set; }
        public string LANG { get; set; }
    }
}
