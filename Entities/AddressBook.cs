using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class AddressBook
    {
        [Required, Key]
        [Column(name: "id")]
        public Guid Id { get; set; }

        [Required, StringLength(maximumLength: 25, MinimumLength = 5)]
        [Column(name: "first_name")]
        public string FirstName { get; set; }

        [Required, StringLength(maximumLength: 25, MinimumLength = 5)]
        [Column(name: "last_name")]
        public string LastName { get; set; }

        [Required, ForeignKey("UserId")]
        [Column(name: "user_id")]
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
