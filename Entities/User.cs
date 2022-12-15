using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    [Table(name: "user")]
    public class User
    {
        [Required]
        [Key]
        [Column(name: "id")]
        public Guid Id { get; set; }

        [Required]
        [Column(name: "user_name")]
        public string UserName { get; set; }

        [Required]
        [StringLength(maximumLength: 25, MinimumLength = 5)]
        [Column(name: "first_name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name cannot be empty")]
        [StringLength(maximumLength: 25, MinimumLength = 5)]
        [Column(name: "last_name")]
        public string LastName { get; set; }

        [Required]
        [Column(name: "password")]
        public string Password { get; set; }
    }
}
