using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class RefTerm
    {
        [Required, Key]
        [Column(name: "id")]
        public Guid Id { get; set; }

        [Required, StringLength(maximumLength: 25, MinimumLength = 5)]
        [Column(name: "key")]
        public string Key { get; set; }

        [Required, StringLength(maximumLength: 50, MinimumLength = 5)]
        [Column(name: "description")]
        public string Description { get; set; }
    }
}
