namespace WebApplication.Models.RequestBody.InputRequest
{
    public class CreateRequestInputBody
    {
        public string WorkshopId { get; set; }
        public string LocationId { get; set; }
        public string WorkerName { get; set; }
        public string RequestedContent { get; set; }
        public string Reason { get; set; }
    }
}
