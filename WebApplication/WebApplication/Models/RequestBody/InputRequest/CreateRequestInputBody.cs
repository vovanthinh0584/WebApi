using System;

namespace WebApplication.Models.RequestBody.InputRequest
{
    public class CreateRequestInputBody
    {
        
        public string UserId { get; set; }
        public string BUID { get; set; }

        public string Lang { get; set; }
        public string ZoneId { get; set; }
        public string Requester { get; set; }


        public string UserManage { get; set; }
        public DateTime? MTNDeadLineDateTime { get; set; }

        public string MNType { get; set; }

    
        public string Equipment { get; set; }
        public string Descriptionrequest { get; set; }


        public bool Repair { get; set; }

        public bool Projectsupporting { get; set; }
        public bool Housekeeping { get; set; }
        public bool Others { get; set; }

        public string MTNRequestNum { get; set; }

        public string NotApprovalDescription { get; set; }
        


    }
}
