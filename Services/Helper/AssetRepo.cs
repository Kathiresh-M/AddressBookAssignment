using Contract.IHelper;
using Entities;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Helper
{
    public class AssetRepo : IAssetRepo
    {
        private readonly BookRepository _context;

        public AssetRepo(BookRepository context)
        {
            _context = context;
        }

        public void AddAsset(Asset assetData)
        {
            if (assetData == null)
                throw new ArgumentNullException(nameof(assetData) + " was null in AddAsset from AssetRepository");

            _context.Assets.Add(assetData);
        }

        public Asset GetAssetByAssetId(Guid AssetId)
        {
            if (AssetId == null || AssetId == Guid.Empty)
                throw new ArgumentNullException(nameof(AssetId) + " was null in GetAsset from AssetRepository");

            return _context.Assets.SingleOrDefault(asset => asset.Id == AssetId);
        }

        public Asset GetAssetByAddressBookId(Guid AddressBookId)
        {
            if (AddressBookId == null || AddressBookId == Guid.Empty)
                throw new ArgumentNullException(nameof(AddressBookId) + " was null in GetAsset from AssetRepository");

            return _context.Assets.SingleOrDefault(asset => asset.AddressBookId == AddressBookId);
        }

        public void UpdateAsset(Asset assetData)
        {
            if (assetData == null)
                throw new ArgumentNullException(nameof(assetData) + " was null in UpdateAsset from AssetRepository");

            _context.Assets.Update(assetData);
        }

        public void DeleteAsset(Asset assetData)
        {
            if (assetData == null)
                throw new ArgumentNullException(nameof(assetData) + " was null in DeleteAsset from AssetRepository");

            _context.Assets.Remove(assetData);
        }
    }
}
