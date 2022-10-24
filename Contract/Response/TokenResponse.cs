using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Response
{
    public class TokenResponse : MessageResponse
    {
        public string AccessToken { get; set; }

        public string TokenType { get; set; }

        public TokenResponse(bool isSuccess, string message, string token, string tokenType) : base(isSuccess, message)
        {
            AccessToken = token;
            TokenType = tokenType;
        }
    }
}
