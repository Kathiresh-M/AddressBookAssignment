using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Address
    {
        [Required, Key]
        [Column(name: "id")]
        public Guid Id { get; set; }

        [Required, StringLength(maximumLength: 25, MinimumLength = 5)]
        [Column(name: "line_1")]
        public string Line1 { get; set; }

        [Required, StringLength(maximumLength: 25, MinimumLength = 5)]
        [Column(name: "line_2")]
        public string Line2 { get; set; }

        [Required, StringLength(maximumLength: 25, MinimumLength = 5)]
        [Column(name: "city")]
        public string? City { get; set; }

        [Required, StringLength(maximumLength: 25, MinimumLength = 5)]
        [Column(name: "state_name")]
        public string StateName { get; set; }

        [Required, RegularExpression(@"^(\w{6})$")]
        [Column(name: "zip_code")]
        public string ZipCode { get; set; }

        [Required, ForeignKey("RefSetMappingId")]
        [Column(name: "address_type_id")]
        public Guid AddressTypeId { get; set; }

        [Required, ForeignKey("RefSetMappingId")]
        [Column(name: "country_type_id")]
        public Guid CountryTypeId { get; set; }

        [Required, ForeignKey("AddressBookId")]
        [Column(name: "address_book_id")]
        public Guid AddressBookId { get; set; }
        public AddressBook AddressBook { get; set; }

        [Required, ForeignKey("UserId")]
        [Column(name: "user_id")]
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
