using Fine.Api.Business.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fine.Api.DataAccess.Contracts.Entities
{
    public class ReceivedSms
    {
        public int Id { get; set; }

        public string? Sender { get; set; }

        public DateTimeOffset? ReceivedDate { get; set; }

        public string? Text { get; set; }

        public string? CarNumber { get; set; }

        public string? Article { get; set; }

        public string? Street { get; set; }

        public DateTimeOffset? DateOfFine { get; set; }

        public string? ReceiptNumber { get; set; }

        public int? Amount { get; set; }

        public int? Term { get; set; }

        public DateTimeOffset? LastDateOfPayment { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public bool Parsed { get; set; }

        public SmsStatus SmsStatus { get; set; }

        public FineStatus FineStatus { get; set; }

        public bool Deleted { get; set; }
    }
}
