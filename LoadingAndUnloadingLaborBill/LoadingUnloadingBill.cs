using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRP.BPL.Data.INV.LoadingAndUnloadingLaborBill
{
    public class LoadingUnloadingBill
    {
        public string BillNo { get; set; }

        public DateTime BillDate { get; set; }
        public string ClientCode { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string DeliveryTime { get; set; }
        public string DriverName { get; set; }
        public string Remarks { get; set; }
        public string PaperSupplierCode { get; set; }
        public int TotalChalanQty { get; set; }
        public string SupplierSerial { get; set; }
        public string ReceiverAddress { get; set; }
        public int RollQtyInKG { get; set; }
        public string PaperSize { get; set; }
        public string Paperbrand { get; set; }
        public string PaperCode { get; set; }
        public string PRMemoNo { get; set; }
        public string PaperType { get; set; }
        public string ChalanNo { get; set; }
        public string TrackNo { get; set; }
        public decimal TotalBill { get; set; }
        public decimal OtherBill { get; set; }
        public int RollQty { get; set; }
        public decimal UnitPrice { get; set; }
    }
}