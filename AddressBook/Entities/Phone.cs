using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Phone
    {
        [Required]
        [Key]
        [Column(name: "id")]
        public Guid Id { get; set; }

        [Required]
        [StringLength(maximumLength: 13, MinimumLength = 4)]
        [Column(name: "phone_number")]
        public string PhoneNumber { get; set; }

        [Required]
        [ForeignKey("RefSetMappingId")]
        [Column(name: "phone_type_id")]
        public Guid PhoneTypeId { get; set; }

        [Required]
        [ForeignKey("AddressBookId")]
        [Column(name: "address_book_id")]
        public Guid AddressBookId { get; set; }
        public AddressBook AddressBook { get; set; }

        [Required]
        [ForeignKey("UserId")]
        [Column(name: "user_id")]
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
