namespace WebApplication.Models.RequestBody.InputRequest
{
    public class CreateRequestInputBody
    {
        public string Equipment { get; set; }
        public string Descriptionrequest { get; set; }

        public string UserId { get; set; }
        public string BUID { get; set; }

        public string Lang { get; set; }
        public string ZoneId { get; set; }
        public string Requester { get; set; }
        public string ReceiveName { get; set; }

        public bool Repair { get; set; }

        public bool Projectsupporting { get; set; }
        public bool Housekeeping { get; set; }
        public bool Others { get; set; }


    }
}
