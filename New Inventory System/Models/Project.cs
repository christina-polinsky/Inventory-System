using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace New_Inventory_System.Models
{
    public class Project
    {
        public int ProjectID { get; set; }

        public string ProjectType { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public short? Year { get; set; }
        public string Condition { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime? DateStarted { get; set; }
        public DateTime? DateCompleted { get; set; }
        public int ProgressPercent { get; set; }
        public string RepoURL { get; set; }
        public string LanguageStack { get; set; }
        public string VersionNumber { get; set; }
        public string LicenseType { get; set; }
    }
}
