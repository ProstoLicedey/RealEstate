//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RealEstate
{
    using System;
    using System.Collections.Generic;
    
    public partial class Commission
    {
        public int Id { get; set; }
        public Nullable<int> DealId { get; set; }
        public Nullable<int> SellerRealtorId { get; set; }
        public Nullable<int> BuyerRealtorId { get; set; }
        public Nullable<decimal> CompanyCommission { get; set; }
        public Nullable<decimal> SellerCommission { get; set; }
        public Nullable<decimal> BuyerCommission { get; set; }
    
        public virtual Rieltor Rieltor { get; set; }
        public virtual Sdelka Sdelka { get; set; }
        public virtual Rieltor Rieltor1 { get; set; }
    }
}
