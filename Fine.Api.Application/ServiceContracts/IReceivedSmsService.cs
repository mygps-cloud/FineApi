using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fine.Api.Application.ServiceContracts
{
    public interface IReceivedSmsService
    {
        Task UpdateReceivedSms(string receiptNumber, bool paid);
    }
}
