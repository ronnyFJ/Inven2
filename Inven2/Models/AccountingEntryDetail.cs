using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inven2.Models
{
    public class AccountingEntryDetail
    {
        public decimal amount {get; set;}
        public int legerAccount { get; set; }
        public string movementType { get; set; }
    }
}