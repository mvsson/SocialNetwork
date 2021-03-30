using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Domain.Entities
{
    public abstract class EntityBase
    {
        protected EntityBase()
        {
            DateAdded = DateTime.UtcNow;
            Id = Guid.NewGuid().ToString();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual string Id { get; set; } 

        [Display(Name = "Name")]
        public virtual string Name { get; set; }

        [DataType(DataType.DateTime)]
        public virtual DateTime DateAdded { get; set; }
    }
}
