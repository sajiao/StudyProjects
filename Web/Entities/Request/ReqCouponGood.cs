using Entities.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Request
{
    public class ReqCouponGood : ReqBase
    {
        public Guid Id { get; set; }

        public string GoodName { get; set; }

        public string CouponValue { get; set; }

        public DateTime Endime { get; set; }

        public CouponGood ConvertData()
        {
            CouponGood data = new CouponGood()
            {
                Id = this.Id,
                GoodName = this.GoodName,
                EndTime = this.Endime
            };

            return data;
        }
    }
}
