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
    
    public partial class Potrebnost
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Potrebnost()
        {
            this.Sdelka = new HashSet<Sdelka>();
        }
    
        public int id { get; set; }
        public int id_client { get; set; }
        public int id_rieltor { get; set; }
        public int id_type { get; set; }
        public Nullable<int> id_address { get; set; }
        public Nullable<int> min_price { get; set; }
        public Nullable<int> max_price { get; set; }
        public Nullable<double> min_area { get; set; }
        public Nullable<double> max_area { get; set; }
        public Nullable<int> min_rooms { get; set; }
        public Nullable<int> max_rooms { get; set; }
        public Nullable<int> min_floor { get; set; }
        public Nullable<int> max_floor { get; set; }
    
        public virtual Address Address { get; set; }
        public virtual Client Client { get; set; }
        public virtual Rieltor Rieltor { get; set; }
        public virtual Type_Object Type_Object { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sdelka> Sdelka { get; set; }
    }
}
