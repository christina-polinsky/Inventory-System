using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace New_Inventory_System.Models
{
    public class ProjectLink
    {
        public int LinkID { get; set; }
        public int ProjectID { get; set; }
        public string LinkType { get; set; }
        public string URL { get; set; }
        public string FilePath { get; set; }
        public string Description { get; set; }
    }
}