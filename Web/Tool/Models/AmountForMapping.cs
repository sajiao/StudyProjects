using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tool.Models
{
    public class AmountForMapping
    {
        public int Id { get; set; }

        public string CashInDate { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal ByBatchAmount { get; set; }

        public string SapCo { get; set; }

        public string Term { get; set; }

        public string WBS { get; set; }

        public string FileName { get; set; }

    }
}
