using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VideoPay.Entities
{
    public class Order
    {
        public int Id { get; set; }

        [StringLength(128)]
        public string OrderNo { get; set; }

        public int OrderAmt { get; set; }

        [StringLength(128)]
        public string PayType { get; set; }

        public bool HasPaid { get; set; }
        
        [StringLength(128)]
        public string SN { get; set; }

        // auto generate on add
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreateTime { get; set; }

        // auto generate on add or update
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime LastUpdateTime { get; set; }

        [StringLength(128)]
        public string ItemName { get; set; }

        public string Remark { get; set; }

    }
}