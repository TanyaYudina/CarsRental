using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using kw2.ViewModels;

namespace kw2.Models
{
    public class Model
    {
        [Key]
        public int ModelId { get; set; }
        public String Name { get; set; }
        public String Specification { get; set; }
        public String Description { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}
