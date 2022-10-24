using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.IHelper
{
    public interface IAssetRepo
    {
        void AddAsset(Asset assetData);
        void DeleteAsset(Asset assetData);
        Asset GetAssetByAddressBookId(Guid AddressBookId);
        Asset GetAssetByAssetId(Guid AssetId);
        void UpdateAsset(Asset assetData);
    }
}
