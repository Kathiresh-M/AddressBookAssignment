using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace Contract.Response
{
    public class AssetResponse : MessageResponse
    {
        public Asset Asset { get; protected set; }

        public AssetResponse(bool isSuccess, string message, Asset assetData) : base(isSuccess, message)
        {
            Asset = assetData;
        }
    }
}
