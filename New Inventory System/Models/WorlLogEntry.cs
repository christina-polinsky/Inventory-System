using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace New_Inventory_System.Models
{
    public class WorkLogEntry
    {
        public int LogID { get; set; }
        public int ProjectID { get; set; }
        public DateTime LogDate { get; set; }
        public string Entry { get; set; }
        public string Stage { get; set; }
    }
}
