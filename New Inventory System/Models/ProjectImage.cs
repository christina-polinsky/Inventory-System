using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace New_Inventory_System.Models
{
    public class ProjectImage
    {
        public int ImageID { get; set; }
        public int ProjectID { get; set; }
        public string ImagePath { get; set; }
        public byte[] ImageData { get; set; }
        public string Caption { get; set; }
        public System.DateTime DateAdded { get; set; }
    }
}
