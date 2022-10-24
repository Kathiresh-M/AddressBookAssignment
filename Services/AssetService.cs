using Contract;
using Contract.IHelper;
using Contract.Response;
using Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AssetService : IAssetService
    {
        private readonly IAssetRepo _assetRepo;
        private readonly IAddressBookRepo _addressBookRepo;

        public AssetService(IAssetRepo assetRepo,
            IAddressBookRepo addressBookRepo)
        {
            _assetRepo = assetRepo;
            _addressBookRepo = addressBookRepo;
        }

        public AssetResponse AddAsset(Guid addressBookId, Guid tokenUserId, Asset assetData, IFormFile file)
        {
            var addressBook = _addressBookRepo.GetAddressBookById(addressBookId);

            if (addressBook == null)
                return new AssetResponse(false, "AddressBook not found", null);

            if (!addressBook.UserId.Equals(tokenUserId))
                return new AssetResponse(false, "AddressBook not found", null);

            var assetExists = _assetRepo.GetAssetByAddressBookId(addressBookId);

            if (assetExists != null && assetExists.UserId.Equals(tokenUserId))
                return new AssetResponse(false, "Asset already exists", null);

            if (assetExists != null && !assetExists.UserId.Equals(tokenUserId))
                return new AssetResponse(false, "AddressBook not found", null);

            assetData.UserId = tokenUserId;
            assetData.AddressBookId = addressBookId;
            assetData.FileName = file.FileName;
            assetData.Size = file.Length;
            assetData.FileType = file.ContentType;

            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                assetData.Content = Convert.ToBase64String(ms.ToArray());
            }

            _assetRepo.AddAsset(assetData);
            _addressBookRepo.Save();

            return new AssetResponse(true, null, assetData);

        }

        public AssetResponse GetAsset(Guid assetId, Guid tokenUserId)
        {
            var asset = _assetRepo.GetAssetByAssetId(assetId);

            if (asset == null)
                return new AssetResponse(false, "Asset not found", null);

            if (!asset.UserId.Equals(tokenUserId))
                return new AssetResponse(false, "Asset not found", null);

            return new AssetResponse(true, null, asset);

        }
    }
}
