using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Asset
    {
        [Required]
        [Key]
        [Column(name:"id")]
        public Guid Id { get; set; }

        [Required]
        [StringLength(maximumLength: 25, MinimumLength = 1)]
        [Column(name: "file_name")]
        public string FileName { get; set; }

        [Required]
        [Url]
        [Column(name: "download_url")]
        public string DownloadUrl { get; set; }

        [Required]
        [Column(name: "file_type")]
        public string FileType { get; set; }

        [Required]
        [Column(name: "size")]
        public decimal Size { get; set; }

        [Required]
        [Column(name: "content")]
        public string Content { get; set; }

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
