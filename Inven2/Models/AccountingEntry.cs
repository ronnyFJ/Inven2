using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inven2.Models
{
    public class AccountingEntry
    {
        public string period { get; set; }
        public string currency { get; set; }
        public IEnumerable<AccountingEntryDetail> detail { get; set; }
    }
}