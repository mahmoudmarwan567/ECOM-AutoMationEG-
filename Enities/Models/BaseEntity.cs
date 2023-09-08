using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enities.Models
{
    [NotMapped]
    public class BaseEntity
    {
        
        public DateTime CreatedDate
        {
            get;
            set;
        }
        public DateTime ModifiedDate
        {
            get;
            set;
        }
        public bool IsActive{get;set;}
        public BaseEntity()
        {
            // Set default values
            CreatedDate = DateTime.UtcNow;
            ModifiedDate = DateTime.UtcNow;
            IsActive = true;
        }
    }
}
