using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Dto;

namespace Contract.Response
{
    public class CountResponse : MessageResponse
    {
        public CountDto Count { get; protected set; }

        public CountResponse(bool isSuccess, string message, CountDto count) : base(isSuccess, message)
        {
            Count = count;
        }
    }
}
