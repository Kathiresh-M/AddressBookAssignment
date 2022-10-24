using Contract.Response;
using Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract
{
    public interface IAssetService
    {
        AssetResponse AddAsset(Guid addressBookId, Guid tokenUserId, Asset assetData, IFormFile file);
        AssetResponse GetAsset(Guid assetId, Guid tokenUserId);
    }
}
