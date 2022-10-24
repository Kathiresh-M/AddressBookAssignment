using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class RefSetTerm
    {
        [Required]
        [Key]
        [Column(name: "id")]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey("RefSetId")]
        [Column(name: "ref_set_id")]
        public Guid RefSetId { get; set; }
        public RefSet RefSet { get; set; }

        [Required]
        [ForeignKey("RefTermId")]
        [Column(name: "ref_term_id")]
        public Guid RefTermId { get; set; }
        public RefTerm RefTerm { get; set; }
    }
}
