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
    
    public partial class Predlojenie
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Predlojenie()
        {
            this.Sdelka = new HashSet<Sdelka>();
        }
    
        public int id { get; set; }
        public int id_client { get; set; }
        public int id_rieltor { get; set; }
        public int id_object { get; set; }
        public int price { get; set; }
    
        public virtual Client Client { get; set; }
        public virtual Object_Nedv Object_Nedv { get; set; }
        public virtual Rieltor Rieltor { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sdelka> Sdelka { get; set; }
    }
}