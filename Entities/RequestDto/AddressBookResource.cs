using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.RequestDto
{
    public class AddressBookResource
    {
        const int maxPageSize = 30;
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 5;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > maxPageSize) ? maxPageSize : value;
        }

        public string SortBy { get; set; } = "FirstName";

        public string SortOrder { get; set; } = "ASC";
    }
}
