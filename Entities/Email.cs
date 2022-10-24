using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    [Table(name: "email")]
    public class Email
    {
        [Required]
        [Key]
        [Column(name: "id")]
        public Guid Id { get; set; }

        [Required]
        [EmailAddress]
        [Column(name: "email_address")]
        public string EmailAddress { get; set; }

        [Required]
        [ForeignKey("RefSetMappingId")]
        [Column(name: "email_type_id")]
        public Guid EmailTypeId { get; set; }

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
