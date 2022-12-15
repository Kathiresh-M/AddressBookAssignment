using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Response
{
    public class EmailResponse : MessageResponse
    {
        public IEnumerable<Email> Emails { get; protected set; }

        public EmailResponse(bool isSuccess, string message, IEnumerable<Email> emails) : base(isSuccess, message)
        {
            Emails = emails;
        }
    }
}
