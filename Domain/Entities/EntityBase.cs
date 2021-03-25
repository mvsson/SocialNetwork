using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        }

        [Required]
        public virtual Guid Id { get; set; }

        [Display(Name = "Name")]
        public virtual string Name { get; set; }

        [Display(Name = "Description")]
        public virtual string Description { get; set; }

        [DataType(DataType.Time)]
        public virtual DateTime DateAdded { get; set; }

        [Display(Name = "SEO Title")]
        public virtual string MetaTitle { get; set; }
    }
}
