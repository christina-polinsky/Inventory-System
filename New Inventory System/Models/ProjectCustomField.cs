using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace New_Inventory_System.Models
{
    public class ProjectCustomField
    {
        public int CustomFieldID { get; set; }
        public int ProjectID { get; set; }
        public string FieldName { get; set; }
        public string FieldValue { get; set; }
        public int SortOrder { get; set; }
    }
}
