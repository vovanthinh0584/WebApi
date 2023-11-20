using System;

namespace WebApplication.Models
{
    public class WorkPermitBody
    {
        
        public string UserId { get; set; }
        public string BUID { get; set; }

        public string Lang { get; set; }

        public string WorkPermitNo { get; set; }

        public string Contractor { get; set; }


        public string DeviceDescription { get; set; }

        public string VendorManager { get; set; }

        public string Area { get; set; }

        public string ProjectManager { get; set; }
        

        public DateTime? StartDate { get; set; }

        public DateTime?EndDate { get; set; }

        public string FileImage { get; set; }

        public string FileName { get; set; }

        public string FileType { get; set; }

        public float FileSize { get; set; }

        public Guid ? RecID { get; set; }

        public string UserApproval { get; set; }

        public string Status { get; set; }

        public string ZoneManager { get; set; }

        public string SafeManager { get; set; }

        public string ErrorMessage { get; set; }


        public string WorkDescription { get; set; }

        public int numofEmployees { get; set; }

        public string OtherSafe { get; set; }

        public string LaborProtection { get; set; }

        public bool Helmet { get; set; }



        public bool Goggles { get; set; }

        public bool Safetyshoes { get; set; }

        public bool Antinoisedevice { get; set; }

        public bool Soldermask { get; set; }

        public bool Facemask { get; set; }

        public bool Cutmask { get; set; }


        public bool Gloves { get; set; }

        public bool Apron { get; set; }

        public bool Chemicalresistantsuit { get; set; }

        public bool
        Gasmasks
        { get; set; }

        public bool Seatbelt { get; set; }

        public string controlmeasures { get; set; }

        public string Hazard { get; set; }

    }
}
