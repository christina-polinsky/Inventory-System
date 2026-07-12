using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace New_Inventory_System.Models
{
    internal class ProjectBOM
    {
        public int BOMID { get; set; }
        public int ProjectID { get; set; }
        public string ComponentType { get; set; }
        public int ComponentID { get; set; }
        public int QuantityNeeded { get; set; }
        public string Notes { get; set;}
    }
}
