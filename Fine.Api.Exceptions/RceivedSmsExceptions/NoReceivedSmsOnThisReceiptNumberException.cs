using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fine.Api.Exceptions
{
    public class NoReceivedSmsOnThisReceiptNumberException:Exception
    {
        public NoReceivedSmsOnThisReceiptNumberException() : base("No Sms Received On This Receipt Number") { }
    }
}
