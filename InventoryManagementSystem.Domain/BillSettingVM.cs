using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Domain
{
    public class BillSettingVM
    {
        [Key]
        public int rowid { get; set; }

        public int? company_id { get; set; }

        [StringLength(50)]
        public string bill_sales { get; set; }

        [StringLength(50)]
        public string bill_purchase { get; set; }

        [StringLength(50)]
        public string bill_cash_sales { get; set; }

        [StringLength(50)]
        public string voucher_number { get; set; }

        [StringLength(50)]
        public string journal_voucher { get; set; }

        [StringLength(50)]
        public string sales_voucher { get; set; }

        [StringLength(50)]
        public string payment_voucher { get; set; }

        [StringLength(50)]
        public string receipt_voucher { get; set; }

        [StringLength(50)]
        public string purchase_voucher { get; set; }

        [StringLength(50)]
        public string contra_voucher { get; set; }

        [StringLength(50)]
        public string TRAN_VOUCHER { get; set; }

        [StringLength(50)]
        public string TRADING_VOUCHER { get; set; }

        [StringLength(50)]
        public string EXCHANGE_VOUCHER { get; set; }

        [StringLength(50)]
        public string TRANSIT_VOUCHER { get; set; }

        [StringLength(50)]
        public string depreciation_voucher { get; set; }

        public int? disposal_voucher { get; set; }
    }
}
