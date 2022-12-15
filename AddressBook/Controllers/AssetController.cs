using AutoMapper;
using Contract;
using Entities;
using Entities.Dto;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AddressBook.Controllers
{
    [ApiController]
    [Authorize]
    public class AssetController : ControllerBase
    {
        private readonly IAssetService _assetService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ILogger<AssetController> _logger;
        private readonly ILog _log;

        public AssetController(IAssetService assetService, 
            IUserService userService, ILogger<AssetController> logger, IMapper mapper)
        {
            _assetService = assetService;
            _userService = userService;
            _logger = logger;
            _log = LogManager.GetLogger(typeof(AssetController));
            _mapper = mapper;
        }

        /// <summary>
        /// Method to upload an asset
        /// </summary>
        /// <param name="addressBookId">Address Book Id</param>
        /// <param name="file">asset file</param>
        /// <returns>asset meta data</returns>
        [HttpPost]
        [Route("api/asset/{addressBookId}")]
        public IActionResult UploadAsset(Guid addressBookId,[FromForm] IFormFile file)
        {
            Guid tokenUserId;
            var isValidToken = Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out tokenUserId);

            if (addressBookId == null || addressBookId == Guid.Empty)
            {
                _log.Error("Trying to update address book data with not a valid addressbook Id by user: " + tokenUserId);
                return BadRequest("Not a valid address book ID.");
            }
                var asset = new Asset();
                asset.Id = Guid.NewGuid();
                asset.DownloadUrl = GenerateDownloadUrl(addressBookId);
                var response = _assetService.AddAsset(addressBookId, tokenUserId, asset, file);

            if (!response.IsSuccess && response.Message.Contains("not found"))
            {
                return NotFound(response.Message);
            }

            if (!response.IsSuccess && response.Message.Contains("exists"))
                {
                    return Conflict(response.Message);
                }

                var assetToReturn = _mapper.Map<AssetReturnDto>(response.Asset);

                return Ok(assetToReturn);
        }

        /// <summary>
        /// Method to upload an asset
        /// </summary>
        /// <param name="Id">Id of the Asset</param>
        /// <returns>asset file</returns>
        [HttpGet]
        [Route("api/asset/{Id}")]
        public IActionResult DownloadAsset(Guid Id)
        {
            Guid tokenUserId;
            var isValidToken = Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out tokenUserId);

            if (Id == null || Id == Guid.Empty)
            {
                _log.Error("Trying to access asset data with not a valid user id by user: " + tokenUserId);
                return BadRequest("Not a valid user ID.");
            }

            var response = _assetService.GetAsset(Id, tokenUserId);

            if (!response.IsSuccess)
            {
                return NotFound(response.Message);
            }

            byte[] bytes = Convert.FromBase64String(response.Asset.Content);

            return File(bytes, response.Asset.FileType, response.Asset.FileName);
        }

        private string GenerateDownloadUrl(Guid addressBookId)
        {
            return ("https://localhost:7258/api/asset/"+ addressBookId);
        }
    }

}
