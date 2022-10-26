using AutoMapper;
using Contract;
using Entities.RequestDto;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace AddressBook.Controllers
{
    [Route("api/addressbook")]
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
        public IActionResult CreateAddressBook([FromBody] AddressBookCreateDto addressBookData)
        {
            if (!ModelState.IsValid)
            {
                _log.Error("Invalid addressbook details used.");
                return BadRequest("Enter valid addressbook data");
            }

            Guid tokenUserId;
            var isValidToken = Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out tokenUserId);

            /*if (!isValidToken)
            {
                _log.Warn($"User with invalid token, trying to access address book data");
                return Unauthorized();
            }

            if (tokenUserId == null || tokenUserId == Guid.Empty)
            {
                _log.Error("Trying to access address book count with not a valid user id by user: " + tokenUserId);
                return BadRequest("Not a valid user ID.");
            }*/
            try
            {
                var response = _addressBookService.CreateAddressBook(addressBookData, tokenUserId);

                if (!response.IsSuccess && response.Message.Contains("already exists") || response.Message.Contains("not valid"))
                {
                    return Conflict(response.Message);
                }

                /*if (!response.IsSuccess && response.Message.Contains("not found"))
                {
                    return NotFound(response.Message);
                }*/

                return Ok($"Address book created with ID: {response.AddressBook.Id}");
            }
            catch(Exception ex)
            {
                return NotFound("Not found exception please check your code"+ex);
            }
        }

        /// <summary>
        /// Method to get a particular address book
        /// </summary>
        /// <param name="addressBookId">Address Book Id</param>
        /// <returns>an address book</returns>
        [HttpGet]
        [Route("{addressBookId}")]
        public IActionResult GetAnAddressBook(Guid addressBookId)
        {

            Guid tokenUserId;
            var isValidToken = Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out tokenUserId);

            /*if (!isValidToken)
            {
                _log.Warn($"User with invalid token, trying to access user data");
                return Unauthorized();
            }*/
            try
            {
                if (addressBookId == null || addressBookId == Guid.Empty)
                {
                    _log.Error("Trying to access address book with not a valid address book id by user: " + tokenUserId);
                    return BadRequest("Not a valid address book ID.");
                }

                var response = _addressBookService.GetAddressBook(addressBookId, tokenUserId);

                if (!response.IsSuccess && response.Message.Contains("found"))
                {
                    return NotFound(response.Message);
                }

                /*if (!response.IsSuccess && response.Message.Contains("User"))
                {
                    return NotFound("Address book not found");
                }*/

                return Ok(response.addressBook);
            }
            catch(Exception ex)
            {
                return NotFound("Address book not found");
            }
        }

        /// <summary>
        /// Method to get a list of address book
        /// </summary>
        /// <param name="resourceParameter">query paramters to get pagination and sorting data</param>
        /// <returns>list of address books</returns>
        [HttpGet]
        public IActionResult GetAddressBooks([FromQuery] AddressBookResource resourceParameter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Guid tokenUserId;
            var isValidToken = Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out tokenUserId);

            /*if (!isValidToken)
            {
                _log.Warn($"User with invalid token, trying to access address book data");
                return Unauthorized();
            }

            if (tokenUserId == null || tokenUserId == Guid.Empty)
            {
                _log.Error("Trying to access address book count with not a valid user id by user: " + tokenUserId);
                return BadRequest("Not a valid user ID.");
            }*/

            
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
        [Route("{Id}")]
        public IActionResult UpdateAddressBook(Guid addressBookId, [FromBody] AddressBookUpdateDto addressBookData)
        {
            if (!ModelState.IsValid)
            {
                _log.Error("Invalid addressbook details used.");
                return BadRequest("Enter valid addressbook data");
            }

            Guid tokenUserId;
            var isValidToken = Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out tokenUserId);

            /*if (!isValidToken)
            {
                _log.Warn($"User with invalid token, trying to access address book data");
                return Unauthorized();
            }

            if (tokenUserId == null || tokenUserId == Guid.Empty)
            {
                _log.Error("Trying to update address book with not a valid user id by user: " + tokenUserId);
                return BadRequest("Not a valid user ID.");
            }*/
            try
            {
                var response = _addressBookService.UpdateAddressBook(addressBookData, addressBookId, tokenUserId);

                if (!response.IsSuccess && response.Message.Contains("Additional") || response.Message.Contains("duplication") || response.Message.Contains("not valid"))
                {
                    return Conflict(response.Message);
                }

                /*if (!response.IsSuccess && response.Message.Contains("not found"))
                {
                    return NotFound(response.Message);
                }*/

                return Ok("Address book updated successfully.");
            }
            catch (Exception ex)
            {
                return NotFound("Not found exception please check your code" + ex);
            }
        }

        /// <summary>
        /// Method to get number of address books
        /// </summary>
        /// <returns>Count of the address books</returns>
        [HttpGet]
        [Route("count")]
        public IActionResult GetAddressBookCount()
        {
            Guid tokenUserId;
            var isValidToken = Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out tokenUserId);

           /* if (!isValidToken)
            {
                _log.Warn($"User with invalid token, trying to access address book data");
                return Unauthorized();
            }

            if (tokenUserId == null || tokenUserId == Guid.Empty)
            {
                _log.Error("Trying to access address book count with not a valid user id by user: " + tokenUserId);
                return BadRequest("Not a valid user ID.");
            }*/

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
        [Route("{Id}")]
        public IActionResult DeleteAddressBook(Guid addressBookId)
        {
            Guid tokenUserId;
            var isValidToken = Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out tokenUserId);

           /* if (!isValidToken)
            {
                _log.Warn($"User with invalid token, trying to access address book data");
                return Unauthorized();
            }*/

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
