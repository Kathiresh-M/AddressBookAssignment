using AutoMapper;
using Contract;
using Entities.RequestDto;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace AddressBookAssignment.Controllers
{
    [ApiController]
    [Authorize]
    public class AddressBookController : ControllerBase
    {
        private readonly IAddressBookService _addressBookService;
        private readonly IMapper _mapper;
        private readonly ILogger<AddressBookController> _logger;
        private readonly ILog _log;

        public AddressBookController(IAddressBookService addressBookService, IMapper mapper, ILogger<AddressBookController> logger)
        {
            _addressBookService = addressBookService;
            _mapper = mapper;
            _logger = logger;
            _log = LogManager.GetLogger(typeof(AddressBookController));
        }

        /// <summary>
        /// Method to create an address book
        /// </summary>
        /// <param name="addressBookData">address book data to be created</param>
        /// <returns>Id of the address book created</returns>
        [HttpPost]
        [Route("api/account")]
        public IActionResult CreateAddressBook([FromBody] AddressBookCreateDto addressBookData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("The addressBookData field is required");
            }

            Guid tokenUserId;
            var isValidToken = Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out tokenUserId);

           
                var response = _addressBookService.CreateAddressBook(addressBookData, tokenUserId);

            try { 

                if (!response.IsSuccess && response.Message.Contains("already exists"))
                {
                    return Conflict(response.Message);
                }
                _log.Info("Address Book was started to create");
                return Ok($"Address book created with ID: {response.AddressBook.Id}");
                _log.Info("Address Book was created" + response.AddressBook.Id);
                }
                catch (Exception ex)
                {
                _log.Debug("Address was not found");
                    return NotFound("Not found exception please check your code" + ex);
                }
        }

        /// <summary>
        /// Method to get a particular address book
        /// </summary>
        /// <param name="addressBookId">Address Book Id</param>
        /// <returns>an address book</returns>
        [HttpGet]
        [Route("api/account/{addressBookId}")]
        public ActionResult GetAnAddressBook(Guid addressBookId)
        {
            Guid tokenUserId = Guid.Parse("f457ae93-3b54-4fc3-8b57-12fe966f1e94");
            //Guid tokenUserId;
            //var isValidToken = Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out tokenUserId);

                if (addressBookId == null || addressBookId == Guid.Empty)
                {
                    _log.Error("Trying to access address book with not a valid address book id by user: " + tokenUserId);
                    return BadRequest("Not a valid address book ID.");
                }
                try
                {
                    var response = _addressBookService.GetAddressBook(addressBookId, tokenUserId);

                    return Ok(response.addressBook);
                }
                catch (Exception ex)
                {
                    _log.Debug("Address was not found");
                    return NotFound("Not found exception please check your code" + ex);
                }
        }

        /// <summary>
        /// Method to get a list of address book
        /// </summary>
        /// <param name="resourceParameter">query paramters to get pagination and sorting data</param>
        /// <returns>list of address books</returns>
        [HttpGet]
        [Route("api/account")]
        public IActionResult GetAddressBooks([FromQuery] AddressBookResource resourceParameter)
        {

            Guid tokenUserId;
            var isValidToken = Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out tokenUserId);

            var addressBooksToReturn = _addressBookService.GetAddressBooks(tokenUserId, resourceParameter);

            var previousPageLink = addressBooksToReturn.HasPrevious ? CreateUri(resourceParameter, UriType.PreviousPage) : null;
            var nextPageLink = addressBooksToReturn.HasNext ? CreateUri(resourceParameter, UriType.NextPage) : null;

            var metaData = new
            {
                totalCount = addressBooksToReturn.TotalCount,
                pageSize = addressBooksToReturn.PageSize,
                currentPage = addressBooksToReturn.CurrentPage,
                totalPages = addressBooksToReturn.TotalPages,
                previousPageLink = previousPageLink,
                nextPageLink = nextPageLink
            };

            Response.Headers.Add("Pagination", JsonSerializer.Serialize(metaData));

            return Ok(addressBooksToReturn);

        }

        /// <summary>
        /// Method to update an address book
        /// </summary>
        /// <param name="addressBookId">Id of the address book in Database</param>
        /// <param name="addressBook">address book data to be updated</param>
        /// <returns>Id of the address book created</returns>
        [HttpPut]
        [Route("api/account/{Id}")]
        public IActionResult UpdateAddressBook(Guid addressBookId, [FromBody] AddressBookUpdateDto addressBookData)
        {
            if (!ModelState.IsValid)
            {
                _log.Error("Invalid addressbook details used.");
                return BadRequest("Enter valid addressbook data");
            }

            Guid tokenUserId;
            var isValidToken = Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out tokenUserId);

                var response = _addressBookService.UpdateAddressBook(addressBookData, addressBookId, tokenUserId);

                if (!response.IsSuccess && response.Message.Contains("Additional") || response.Message.Contains("duplication") || response.Message.Contains("not valid"))
                {
                    return Conflict(response.Message);
                }

            if (!response.IsSuccess && response.Message.Contains("not found"))
            {
                _log.Debug("Address was not found");
                return NotFound(response.Message);
            }
            _log.Info("Address was updated successfully");
            return Ok("Address book updated successfully.");
        }

        /// <summary>
        /// Method to get number of address books
        /// </summary>
        /// <returns>Count of the address books</returns>
        [HttpGet]
        [Route("api/account/count")]
        public IActionResult GetAddressBookCount()
        {
            Guid tokenUserId;
            var isValidToken = Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out tokenUserId);

            var response = _addressBookService.GetCount(tokenUserId);
            if (!response.IsSuccess && response.Message.Contains("User"))
            {
                _log.Error($"User with Id:{tokenUserId}");
                return NotFound("user not found");
            }
            return Ok(response.Count);
        }

        /// <summary>
        /// Method to delete an address book
        /// </summary>
        /// <param name="addressBookId">Id of the address book</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/account/{Id}")]
        public IActionResult DeleteAddressBook(Guid addressBookId)
        {
            Guid tokenUserId = Guid.Parse("f457ae93-3b54-4fc3-8b57-12fe966f1e94");
            //Guid tokenUserId;
            //var isValidToken = Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out tokenUserId);

            var addressBookResponseData = _addressBookService.GetAddressBook(addressBookId, tokenUserId);

            if (!addressBookResponseData.IsSuccess || addressBookResponseData.Message.Contains("not"))
            {
                _log.Info($"Address book with Id: {addressBookId}, does not exists");
                return NotFound("Address book not found.");
            }

            if (!addressBookResponseData.IsSuccess || addressBookResponseData.Message.Contains("User"))
            {
                _log.Info($"Address book with Id: {addressBookId}, was tried to delete by user Id:{tokenUserId}.");
                return NotFound("Address book not found.");
            }

            var deleteResponse = _addressBookService.DeleteAddressBook(addressBookId, tokenUserId);

            if (!deleteResponse.IsSuccess)
                return NotFound("Address Book not found");

            return Ok(addressBookResponseData.addressBook);
            _log.Info("Address was deleted successfully");
        }

        /// <summary>
        /// Method to create link
        /// </summary>
        /// <param name="resourceParameter">Pagiantiona and sorting data</param>
        /// <param name="uriType">type of the uri</param>
        /// <returns>uri or null</returns>
        private string CreateUri(AddressBookResource resourceParameter, UriType uriType)
        {
            switch (uriType)
            {
                case UriType.PreviousPage:
                    return Url.Link("GetAddressBooks", new
                    {
                        pageNumber = resourceParameter.PageNumber - 1,
                        pageSize = resourceParameter.PageSize,
                        sortBy = resourceParameter.SortBy,
                        sortOrder = resourceParameter.SortOrder,
                    });
                case UriType.NextPage:
                    return Url.Link("GetAddressBooks", new
                    {
                        pageNumber = resourceParameter.PageNumber + 1,
                        pageSize = resourceParameter.PageSize,
                        sortBy = resourceParameter.SortBy,
                        sortOrder = resourceParameter.SortOrder,
                    });
                default:
                    return Url.Link("GetAddressBooks", new
                    {
                        pageNumber = resourceParameter.PageNumber,
                        pageSize = resourceParameter.PageSize,
                        sortBy = resourceParameter.SortBy,
                        sortOrder = resourceParameter.SortOrder,
                    });
            }
        }
    }
}
