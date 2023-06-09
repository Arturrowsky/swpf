//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ArturCzarnecki_9807
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Fizjoterapeuta
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Fizjoterapeuta()
        {
            this.Gabinet = new HashSet<Gabinet>();
            this.Godziny = new HashSet<Godziny>();
            this.KodPocztowy1 = new HashSet<KodPocztowy>();
            this.Wizyta = new HashSet<Wizyta>();
        }
    
        public int IdFizjoterapeuty { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Haslo { get; set; }
       
        public string Imie { get; set; }
      
        public string Nazwisko { get; set; }
     
        public string NrFizjoterapeuty { get; set; }
        public string KodPocztowy { get; set; }
        public string Miasto { get; set; }
        public bool CzyGabinet { get; set; }

        public string NrTel { get; set; }
  
        public string Opis { get; set; }
        public int IdRoli { get; set; }
    
        public virtual Rola Rola { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Gabinet> Gabinet { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Godziny> Godziny { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KodPocztowy> KodPocztowy1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Wizyta> Wizyta { get; set; }
    }
}
