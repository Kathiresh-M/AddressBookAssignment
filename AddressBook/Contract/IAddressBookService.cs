using Contract.Response;
using Entities.Dto;
using Entities.RequestDto;
using System;
using System.Linq;
using System.Text;

namespace Contract
{
    public interface IAddressBookService
    {
        AddressBookAddResponse CreateAddressBook(AddressBookCreateDto addressBookData, Guid userId);
        AddressBookResponse GetAddressBook(Guid addressBookId, Guid tokenUserId);
        AddressBookAddResponse UpdateAddressBook(AddressBookUpdateDto addressBookData, Guid addressBookId, Guid userId);
        CountResponse GetCount(Guid userId);
        MessageResponse DeleteAddressBook(Guid addressBookId, Guid userId);
        PagedList<AddressBookReturnDto> GetAddressBooks(Guid userId, AddressBookResource resourceParameter);
    }
}
