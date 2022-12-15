using Contract;
using Contract.IHelper;
using Contract.Response;
using Entities;
using Entities.Dto;
using Entities.RequestDto;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Services
{
    public class AddressBookService : IAddressBookService
    {
        private readonly IAddressBookRepo _addressBookRepo;
        private readonly IRefSetRepo _refSetRepo;
        private readonly IRefSetTermRepo _refSetTermRepo;
        private readonly IAssetRepo _assetRepo;
        private readonly IAddressRepo _addressRepo;
        private readonly IRefTermRepo _refTermRepo;
        private readonly IEmailRepo _emailRepo;
        private readonly IPhoneRepo _phoneRepo;
        private readonly IUserRepo _userRepo;
        private readonly ILog _log;

        public AddressBookService(IAddressBookRepo addressBookRepo, IRefSetRepo refSetRepo,
            IRefSetTermRepo refSetTermRepo, IAssetRepo assetRepo, IAddressRepo addressRepo,
            IRefTermRepo refTermRepo, IEmailRepo emailRepo, IPhoneRepo phoneRepo, IUserRepo userRepo 
            )
        {
            _addressBookRepo = addressBookRepo;
            _refSetRepo = refSetRepo;
            _refSetTermRepo = refSetTermRepo;
            _assetRepo = assetRepo;
            _addressRepo = addressRepo;
            _refTermRepo = refTermRepo;
            _emailRepo = emailRepo;
            _phoneRepo = phoneRepo;
            _userRepo = userRepo;
            _log = LogManager.GetLogger(typeof(AddressBookService));
        }
        public AddressBookAddResponse CreateAddressBook(AddressBookCreateDto addressBookData, Guid userId)
        {
            var addressBookCheck = _addressBookRepo.GetAddressBookByName(addressBookData.FirstName, addressBookData.LastName);

            try
            {
                //if (addressBookCheck != null && addressBookCheck.UserId == userId)
                //    return new AddressBookAddResponse(false, "Address book already exists with same first and last name.", null);
                _log.Info("Email start to add");
                var emailsAdded = addEmails(addressBookData.Emails);
                if (!emailsAdded.IsSuccess)
                    return new AddressBookAddResponse(false, emailsAdded.Message, null);

                _log.Info("Email is entered successfully");

                _log.Info("Phone is start to add");
                var phonesAdded = addPhones(addressBookData.Phones);
                if (!phonesAdded.IsSuccess)
                    return new AddressBookAddResponse(false, phonesAdded.Message, null);

                _log.Info("Phone is entered successfully");
                _log.Info("Address start to add");
                var addressAdded = addAddresses(addressBookData.Addresses);
                if (!addressAdded.IsSuccess)
                    return new AddressBookAddResponse(false, addressAdded.Message, null);

                _log.Info("Address is entered successfully");
                var addressBook = new AddressBookDto()
                {
                    Id = Guid.NewGuid(),
                    FirstName = addressBookData.FirstName,
                    LastName = addressBookData.LastName,
                    Emails = emailsAdded.Emails,
                    Phones = phonesAdded.Phones,
                    Addresses = addressAdded.Addresses,
                    UserId = userId,
                };

                foreach (var email in emailsAdded.Emails)
                {
                    email.UserId = userId;
                    email.AddressBookId = addressBook.Id;
                }

                foreach (var phone in phonesAdded.Phones)
                {
                    phone.AddressBookId = addressBook.Id;
                    phone.UserId = userId;
                }

                foreach (var address in addressAdded.Addresses)
                {
                    address.AddressBookId = addressBook.Id;
                    address.UserId = userId;
                }

                _addressBookRepo.CreateAddressBook(addressBook);

                _addressBookRepo.Save();

                return new AddressBookAddResponse(true, "", addressBook);
                _log.Info("Address Book is entered successfully");
            }
            catch
            {
                return new AddressBookAddResponse(false, "Address book already exists with same first and last name.", null);
            }
        }

        //GetAddressBook
        public AddressBookResponse GetAddressBook(Guid addressBookId, Guid tokenUserId)
        {

            var addressBook = _addressBookRepo.GetAddressBookById(addressBookId);

            if (addressBook == null)
            {
                return new AddressBookResponse(false, "Address book not found", null);
            }

            if (addressBook.UserId != tokenUserId)
            {
                return new AddressBookResponse(false, "User has no access to address book", null);
            }

            var emailsListToReturn = getEmails(addressBookId);

            var phonesListToReturn = getPhones(addressBookId);

            var addressListToReturn = getAddresses(addressBookId);

            var asset = _assetRepo.GetAssetByAddressBookId(addressBookId);

            if (asset == null)
                asset = new Asset();

            var addressBookToReturn = new AddressBookReturnDto()
            {
                Id = addressBook.Id,
                FirstName = addressBook.FirstName,
                LastName = addressBook.LastName,
                Emails = emailsListToReturn,
                Phones = phonesListToReturn,
                Addresses = addressListToReturn,
                AssetDTO = new AssetIdDto() { FileId = asset.Id }
            };

            return new AddressBookResponse(true, "", addressBookToReturn);
        }

        //GetAddressBook
        public PagedList<AddressBookReturnDto> GetAddressBooks(Guid userId, AddressBookResource resourceParameter)
        {
            var user = _userRepo.GetUser(userId);

            if (user == null)
                return PagedList<AddressBookReturnDto>.Create(new List<AddressBookReturnDto>(), 0, 1);

            var addressBooksToReturn = new List<AddressBookReturnDto>();

            var addressBooks = _addressBookRepo.GetAddressBooks(userId);

            foreach (var addressBook in addressBooks)
            {
                var asset = _assetRepo.GetAssetByAddressBookId(addressBook.Id);
                if (asset == null)
                    asset = new Asset();
                addressBooksToReturn.Add(new AddressBookReturnDto()
                {
                    Id = addressBook.Id,
                    FirstName = addressBook.FirstName,
                    LastName = addressBook.LastName,
                    Emails = getEmails(addressBook.Id),
                    Phones = getPhones(addressBook.Id),
                    Addresses = getAddresses(addressBook.Id),
                    AssetDTO = new AssetIdDto() { FileId = asset.Id }
                });
            }

            if (resourceParameter.SortBy.ToLower() == "lastname" && resourceParameter.SortOrder.ToLower() == "asc")
            {
                addressBooksToReturn = addressBooksToReturn.OrderBy(addressBook => addressBook.LastName).ToList();
            }

            if (resourceParameter.SortBy.ToLower() == "lastname" && resourceParameter.SortOrder.ToLower() == "desc")
            {
                addressBooksToReturn = addressBooksToReturn.OrderByDescending(addressBook => addressBook.LastName).ToList();
            }

            if (resourceParameter.SortBy.ToLower() == "firstname" && resourceParameter.SortOrder.ToLower() == "asc")
            {
                addressBooksToReturn = addressBooksToReturn.OrderBy(addressBook => addressBook.FirstName).ToList();
            }

            if (resourceParameter.SortBy.ToLower() == "firstname" && resourceParameter.SortOrder.ToLower() == "desc")
            {
                addressBooksToReturn = addressBooksToReturn.OrderByDescending(addressBook => addressBook.FirstName).ToList();
            }

            return PagedList<AddressBookReturnDto>.Create(addressBooksToReturn, resourceParameter.PageNumber, resourceParameter.PageSize);
        }

        //Update AddressBook
        public AddressBookAddResponse UpdateAddressBook(AddressBookUpdateDto addressBookData, Guid addressBookId, Guid userId)
        {
            var addressBookCheck = _addressBookRepo.GetAddressBookById(addressBookId);

            if (addressBookCheck == null && addressBookCheck.UserId == userId)
                return new AddressBookAddResponse(false, "Address book not found", null);

            var emailsUpdated = UpdateEmails(addressBookData.Emails, addressBookId);
            if (!emailsUpdated.IsSuccess)
                return new AddressBookAddResponse(false, emailsUpdated.Message, null);

            var phonesUpdated = UpdatePhones(addressBookData.Phones, addressBookId);
            if (!phonesUpdated.IsSuccess)
                return new AddressBookAddResponse(false, phonesUpdated.Message, null);

            var addressUpdated = UpdateAddresses(addressBookData.Addresses, addressBookId);
            if (!addressUpdated.IsSuccess)
                return new AddressBookAddResponse(false, addressUpdated.Message, null);

            addressBookCheck.FirstName = addressBookData.FirstName;
            addressBookCheck.LastName = addressBookData.LastName;

            _addressBookRepo.UpdateAddressBook(addressBookCheck, emailsUpdated.Emails, addressUpdated.Addresses, phonesUpdated.Phones);

            _addressBookRepo.Save();

            return new AddressBookAddResponse(true, "", null);
        }

        //Delete AddressBook
        public MessageResponse DeleteAddressBook(Guid addressBookId, Guid userId)
        {
            var addressBook = _addressBookRepo.GetAddressBookById(addressBookId);

            if (addressBook == null)
                return new MessageResponse(false, "Address book not found");

            if (addressBook.UserId != userId)
                return new MessageResponse(false, "User not having access");

            _addressBookRepo.DeleteAddressBook(addressBook);
            _addressBookRepo.Save();

            return new MessageResponse(true, null);
        }

        //Get Count
        public CountResponse GetCount(Guid userId)
        {

            var count = _addressBookRepo.GetAddressBookCount(userId);

            return new CountResponse(true, null, new CountDto { Count = count });
        }

        //Add Email,Phone,Address
        private EmailResponse addEmails(IEnumerable<EmailDto> emails)
        {
            var emailSet = _refSetRepo.GetRefSet("EMAIL_TYPE");
            var emailTerms = _refSetTermRepo.GetRefTermsByRefSetId(emailSet.Id);
            var emailRefTermsWithMapping = _refSetTermRepo.GetRefTermMappingId(emailSet.Id);

            IList<Email> emailsList = new List<Email>();

            //email validation
            bool unique = true;
            for (int i = 0; i < emails.Count() - 1; i++)
            {
                if (!Regex.Match(emails.ElementAt(i).EmailAddress, @"/^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/").Success)
                {
                    unique = false;
                    break;
                }

                for (int j = i + 1; j < emails.Count(); j++)
                {
                    if (emails.ElementAt(i).EmailAddress.Equals(emails.ElementAt(i).EmailAddress))
                    {
                        unique = false;
                        break;
                    }
                }
            }

            if (!unique)
                return new EmailResponse(false, "Email data is not valid", null);

            for (int i = 0; i < emails.Count(); i++)
            {
                var refTerm = emailTerms.SingleOrDefault(refTerm => refTerm.Key.ToLower() == emails.ElementAt(i).Type.Key.ToLower());
                if (refTerm == null)
                    return new EmailResponse(false, $"Key {emails.ElementAt(i).Type.Key} was not found.", null);
                var mapping = emailRefTermsWithMapping.SingleOrDefault(mapping => mapping.RefTermId == refTerm.Id);
                if (mapping == null)
                    return new EmailResponse(false, $"Key {emails.ElementAt(i).Type.Key} was not found.", null);

                emailsList.Add(new Email
                {
                    Id = Guid.NewGuid(),
                    EmailAddress = emails.ElementAt(i).EmailAddress,
                    EmailTypeId = mapping.Id
                });
            }

            return new EmailResponse(true, null, emailsList);
        }

        private PhoneResponse addPhones(IEnumerable<PhoneDto> phones)
        {
            var phoneSet = _refSetRepo.GetRefSet("PHONE_TYPE");
            var phoneTerms = _refSetTermRepo.GetRefTermsByRefSetId(phoneSet.Id);
            var phoneRefTermsWithMapping = _refSetTermRepo.GetRefTermMappingId(phoneSet.Id);

            IList<Phone> phonesList = new List<Phone>();

            //phone validation
            bool unique = true;
            for (int i = 0; i < phones.Count() - 1; i++)
            {
                if (phones.ElementAt(i).PhoneNumber.Length == 14)
                {
                    unique = false;
                    break;
                }

                for (int j = i + 1; j < phones.Count(); j++)
                {
                    if (phones.ElementAt(i).PhoneNumber.Equals(phones.ElementAt(i).PhoneNumber))
                    {
                        unique = false;
                        break;
                    }
                }
            }

            if (!unique)
                return new PhoneResponse(false, "Phone data is not valid", null);

            for (int i = 0; i < phones.Count(); i++)
            {
                var refTerm = phoneTerms.SingleOrDefault(refTerm => refTerm.Key.ToLower() == phones.ElementAt(i).Type.Key.ToLower());
                if (refTerm == null)
                    return new PhoneResponse(false, $"Key {phones.ElementAt(i).Type.Key} was not found.", null);
                var mapping = phoneRefTermsWithMapping.SingleOrDefault(mapping => mapping.RefTermId == refTerm.Id);
                if (mapping == null)
                    return new PhoneResponse(false, $"Key {phones.ElementAt(i).Type.Key} was not found.", null);

                phonesList.Add(new Phone
                {
                    Id = Guid.NewGuid(),
                    PhoneNumber = phones.ElementAt(i).PhoneNumber,
                    PhoneTypeId = mapping.Id,
                });
            }

            return new PhoneResponse(true, null, phonesList);
        }

        private AddressResponse addAddresses(IEnumerable<AddressDto> addresses)
        {
            var addressSet = _refSetRepo.GetRefSet("ADDRESS_TYPE");
            var addressTerms = _refSetTermRepo.GetRefTermsByRefSetId(addressSet.Id);
            var addressRefTermsWithMapping = _refSetTermRepo.GetRefTermMappingId(addressSet.Id);

            var countrySet = _refSetRepo.GetRefSet("COUNTRY_TYPE");
            var countryTerms = _refSetTermRepo.GetRefTermsByRefSetId(countrySet.Id);
            var countryRefTermsWithMapping = _refSetTermRepo.GetRefTermMappingId(countrySet.Id);

            IList<Address> addressesList = new List<Address>();

            for (int i = 0; i < addresses.Count(); i++)
            {
                var address = addresses.ElementAt(i);

                var addressRefTerm = addressTerms.SingleOrDefault(refTerm => refTerm.Key.ToLower() == address.Type.Key.ToLower());
                if (addressRefTerm == null)
                    return new AddressResponse(false, $"Key {address.Type.Key} was not found.", null);
                var addressMapping = addressRefTermsWithMapping.SingleOrDefault(mapping => mapping.RefTermId == addressRefTerm.Id);
                if (addressMapping == null)
                    return new AddressResponse(false, $"Key {address.Type.Key} was not found.", null);

                var countryRefTerm = countryTerms.SingleOrDefault(refTerm => refTerm.Key.ToLower() == address.Country.Key.ToLower());
                if (countryRefTerm == null)
                    return new AddressResponse(false, $"Key {address.Country.Key} was not found.", null);
                var countryMapping = countryRefTermsWithMapping.SingleOrDefault(mapping => mapping.RefTermId == countryRefTerm.Id);
                if (countryMapping == null)
                    return new AddressResponse(false, $"Key {address.Country.Key} was not found.", null);

                addressesList.Add(new Address
                {
                    Id = Guid.NewGuid(),
                    Line1 = address.Line1,
                    Line2 = address.Line2,
                    City = address.City,
                    StateName = address.StateName,
                    ZipCode = address.ZipCode,
                    AddressTypeId = addressMapping.Id,
                    CountryTypeId = countryMapping.Id,
                });
            }

            return new AddressResponse(true, null, addressesList);
        }

        //Get address,phone,email
        private IList<EmailToReturnDto> getEmails(Guid addressBookId)
        {
            var emailsList = _emailRepo.GetEmailsByAddressBookId(addressBookId);
            IList<EmailToReturnDto> emailsListToReturn = new List<EmailToReturnDto>();
            foreach (var email in emailsList)
            {
                var refSetMapping = _refSetTermRepo.GetRefSetMapping(email.EmailTypeId);
                var refTerm = _refTermRepo.GetRefTerm(refSetMapping.RefTermId);
                emailsListToReturn.Add(new EmailToReturnDto()
                {
                    Id = email.Id,
                    EmailAddress = email.EmailAddress,
                    Type = new Entities.Dto.Type() { Key = refTerm.Key }
                });
            }

            return emailsListToReturn;
        }

        private IList<PhoneToReturnDto> getPhones(Guid addressBookId)
        {
            var phonesList = _phoneRepo.GetPhonesByAddressBookId(addressBookId);
            IList<PhoneToReturnDto> phonesListToReturn = new List<PhoneToReturnDto>();
            foreach (var phone in phonesList)
            {
                var refSetMapping = _refSetTermRepo.GetRefSetMapping(phone.PhoneTypeId);
                var refTerm = _refTermRepo.GetRefTerm(refSetMapping.RefTermId);
                phonesListToReturn.Add(new PhoneToReturnDto()
                {
                    Id = phone.Id,
                    PhoneNumber = phone.PhoneNumber,
                    Type = new Entities.Dto.Type() { Key = refTerm.Key }
                });
            }

            return phonesListToReturn;
        }

        private IList<AddressToReturnDto> getAddresses(Guid Id)
        {
            var addressList = _addressRepo.GetAddresssByAddressBookId(Id);
            IList<AddressToReturnDto> addressListToReturn = new List<AddressToReturnDto>();
            foreach (var address in addressList)
            {
                var refSetMapping = _refSetTermRepo.GetRefSetMapping(address.AddressTypeId);
                var typeRefTerm = _refTermRepo.GetRefTerm(refSetMapping.RefTermId);
                refSetMapping = _refSetTermRepo.GetRefSetMapping(address.CountryTypeId);
                var countryRefTerm = _refTermRepo.GetRefTerm(refSetMapping.RefTermId);
                addressListToReturn.Add(new AddressToReturnDto()
                {
                    Id = address.Id,
                    Line1 = address.Line1,
                    Line2 = address.Line2,
                    City = address.City,
                    StateName = address.StateName,
                    ZipCode = address.ZipCode,
                    Type = new Entities.Dto.Type() { Key = typeRefTerm.Key },
                    Country = new Entities.Dto.Type() { Key = countryRefTerm.Key },
                });
            }

            return addressListToReturn;
        }

        //Update Address,Phone,Email
        private EmailResponse UpdateEmails(IEnumerable<EmailUpdationDto> emails, Guid addressbookId)
        {
            var emailSet = _refSetRepo.GetRefSet("EMAIL_TYPE");
            var emailTerms = _refSetTermRepo.GetRefTermsByRefSetId(emailSet.Id);
            var emailRefTermsWithMapping = _refSetTermRepo.GetRefTermMappingId(emailSet.Id);

            IList<Email> emailsList = new List<Email>();
            var emailsInDB = _emailRepo.GetEmailsByAddressBookId(addressbookId);

            if (emailsInDB.Count() != emails.Count())
                return new EmailResponse(false, "Additional email address data given", null);

            foreach (var emailFromDB in emailsInDB)
            {
                var DoesExists = emails.Where(email => email.Id == emailFromDB.Id);
                if (DoesExists == null)
                    return new EmailResponse(false, "Given email address data not found in the address book", null);
            }

            //email validation
            bool unique = true;
            for (int i = 0; i < emails.Count() - 1; i++)
            {
                for (int j = i + 1; j < emails.Count(); j++)
                {
                    if (emails.ElementAt(i).EmailAddress.Equals(emails.ElementAt(i).EmailAddress))
                    {
                        unique = false;
                        break;
                    }
                }
            }

            if (!unique)
                return new EmailResponse(false, "Email address duplication found", null);

            for (int i = 0; i < emails.Count() - 1; i++)
            {
                for (int j = i + 1; j < emails.Count(); j++)
                {
                    if (!Regex.Match(emails.ElementAt(i).EmailAddress, @"/^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/").Success)
                    {
                        unique = false;
                        break;
                    }
                }
            }

            if (!unique)
                return new EmailResponse(false, "Email address not valid", null);

            for (int i = 0; i < emails.Count(); i++)
            {
                var refTerm = emailTerms.SingleOrDefault(refTerm => refTerm.Key.ToLower() == emails.ElementAt(i).Type.Key.ToLower());
                if (refTerm == null)
                    return new EmailResponse(false, $"Key {emails.ElementAt(i).Type.Key} was not found.", null);
                var mapping = emailRefTermsWithMapping.SingleOrDefault(mapping => mapping.RefTermId == refTerm.Id);
                if (mapping == null)
                    return new EmailResponse(false, $"Key {emails.ElementAt(i).Type.Key} was not found.", null);

                emailsList.Add(new Email
                {
                    Id = emails.ElementAt(i).Id,
                    EmailAddress = emails.ElementAt(i).EmailAddress,
                    EmailTypeId = mapping.Id
                });
            }

            foreach (var email in emailsList)
            {
                int index = emailsInDB.ToList().FindIndex(emailFromDB => emailFromDB.Id == email.Id);
                var emailFromDB = emailsInDB.ToList().ElementAt(index);
                emailFromDB.EmailAddress = email.EmailAddress;
                emailFromDB.EmailTypeId = email.EmailTypeId;
            }

            return new EmailResponse(true, "", emailsInDB);
        }

        private PhoneResponse UpdatePhones(IEnumerable<PhoneUpdationDto> phones, Guid addressbookId)
        {
            var phoneSet = _refSetRepo.GetRefSet("PHONE_TYPE");
            var phoneTerms = _refSetTermRepo.GetRefTermsByRefSetId(phoneSet.Id);
            var phoneRefTermsWithMapping = _refSetTermRepo.GetRefTermMappingId(phoneSet.Id);

            IList<Phone> phonesList = new List<Phone>();
            var phonesInDB = _phoneRepo.GetPhonesByAddressBookId(addressbookId);

            if (phonesInDB.Count() != phones.Count())
                return new PhoneResponse(false, "Additional phone number data given", null);

            foreach (var phoneFromDB in phonesInDB)
            {
                var DoesExists = phones.Where(phone => phone.Id == phoneFromDB.Id);
                if (DoesExists == null)
                    return new PhoneResponse(false, "Given phone number data not found in the address book", null);
            }

            //phone validation
            bool unique = true;
            for (int i = 0; i < phones.Count() - 1; i++)
            {
                for (int j = i + 1; j < phones.Count(); j++)
                {
                    if (phones.ElementAt(i).PhoneNumber.Length == 13)
                    {
                        unique = false;
                        break;
                    }
                }
            }

            if (!unique)
                return new PhoneResponse(false, "Phone number is not valid", null);

            for (int i = 0; i < phones.Count() - 1; i++)
            {

                for (int j = i + 1; j < phones.Count(); j++)
                {
                    if (phones.ElementAt(i).PhoneNumber.Equals(phones.ElementAt(i).PhoneNumber))
                    {
                        unique = false;
                        break;
                    }
                }
            }

            if (!unique)
                return new PhoneResponse(false, "Duplicate Phone data is found", null);

            for (int i = 0; i < phones.Count(); i++)
            {
                var refTerm = phoneTerms.SingleOrDefault(refTerm => refTerm.Key.ToLower() == phones.ElementAt(i).Type.Key.ToLower());
                if (refTerm == null)
                    return new PhoneResponse(false, $"Key {phones.ElementAt(i).Type.Key} was not found.", null);
                var mapping = phoneRefTermsWithMapping.SingleOrDefault(mapping => mapping.RefTermId == refTerm.Id);
                if (mapping == null)
                    return new PhoneResponse(false, $"Key {phones.ElementAt(i).Type.Key} was not found.", null);

                phonesList.Add(new Phone
                {
                    Id = phones.ElementAt(i).Id,
                    PhoneNumber = phones.ElementAt(i).PhoneNumber,
                    PhoneTypeId = mapping.Id,
                });
            }

            foreach (var phone in phonesList)
            {
                int index = phonesInDB.ToList().FindIndex(phoneFromDB => phoneFromDB.Id == phone.Id);
                var phoneFromDB = phonesInDB.ToList().ElementAt(index);
                phoneFromDB.PhoneNumber = phone.PhoneNumber;
                phoneFromDB.PhoneTypeId = phone.PhoneTypeId;
            }

            return new PhoneResponse(true, null, phonesInDB);
        }

        private AddressResponse UpdateAddresses(IEnumerable<AddressUpdationDto> addresses, Guid addressBookId)
        {
            var addressSet = _refSetRepo.GetRefSet("ADDRESS_TYPE");
            var addressTerms = _refSetTermRepo.GetRefTermsByRefSetId(addressSet.Id);
            var addressRefTermsWithMapping = _refSetTermRepo.GetRefTermMappingId(addressSet.Id);

            var countrySet = _refSetRepo.GetRefSet("COUNTRY_TYPE");
            var countryTerms = _refSetTermRepo.GetRefTermsByRefSetId(countrySet.Id);
            var countryRefTermsWithMapping = _refSetTermRepo.GetRefTermMappingId(countrySet.Id);

            IList<Address> addressesList = new List<Address>();
            var addressesInDB = _addressRepo.GetAddresssByAddressBookId(addressBookId);

            if (addressesInDB.Count() != addresses.Count())
                return new AddressResponse(false, "Additional address data given", null);

            foreach (var addressFromDB in addressesInDB)
            {
                var DoesExists = addresses.Where(address => address.Id == addressFromDB.Id);
                if (DoesExists == null)
                    return new AddressResponse(false, "Given address data not found in the address book", null);
            }

            for (int i = 0; i < addresses.Count(); i++)
            {
                var address = addresses.ElementAt(i);

                var addressRefTerm = addressTerms.SingleOrDefault(refTerm => refTerm.Key.ToLower() == address.Type.Key.ToLower());
                if (addressRefTerm == null)
                    return new AddressResponse(false, $"Key {address.Type.Key} was not found.", null);
                var addressMapping = addressRefTermsWithMapping.SingleOrDefault(mapping => mapping.RefTermId == addressRefTerm.Id);
                if (addressMapping == null)
                    return new AddressResponse(false, $"Key {address.Type.Key} was not found.", null);

                var countryRefTerm = countryTerms.SingleOrDefault(refTerm => refTerm.Key.ToLower() == address.Country.Key.ToLower());
                if (countryRefTerm == null)
                    return new AddressResponse(false, $"Key {address.Country.Key} was not found.", null);
                var countryMapping = countryRefTermsWithMapping.SingleOrDefault(mapping => mapping.RefTermId == countryRefTerm.Id);
                if (countryMapping == null)
                    return new AddressResponse(false, $"Key {address.Country.Key} was not found.", null);

                addressesList.Add(new Address
                {
                    Id = address.Id,
                    Line1 = address.Line1,
                    Line2 = address.Line2,
                    City = address.City,
                    StateName = address.StateName,
                    ZipCode = address.ZipCode,
                    AddressTypeId = addressMapping.Id,
                    CountryTypeId = countryMapping.Id,
                });
            }

            foreach (var address in addressesList)
            {
                int index = addressesInDB.ToList().FindIndex(addressFromDB => addressFromDB.Id == address.Id);
                var addressFromDB = addressesInDB.ToList().ElementAt(index);
                addressFromDB.Line1 = address.Line1;
                addressFromDB.Line2 = address.Line2;
                addressFromDB.City = address.City;
                addressFromDB.StateName = address.StateName;
                addressFromDB.ZipCode = address.ZipCode;
                addressFromDB.AddressTypeId = address.AddressTypeId;
                addressFromDB.CountryTypeId = address.CountryTypeId;
            }

            return new AddressResponse(true, null, addressesInDB);
        }
    }
}