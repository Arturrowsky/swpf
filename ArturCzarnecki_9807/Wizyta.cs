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

    public partial class Wizyta
    {
        public int IdWizyty { get; set; }
        public int IdFizjoterapeuty { get; set; }
        public int IdPacjenta { get; set; }
        [Required]
        public string Godzina { get; set; }
        [Required]
        public System.DateTime Data { get; set; }
        public string Opis { get; set; }
    
        public virtual Fizjoterapeuta Fizjoterapeuta { get; set; }
        public virtual Pacjent Pacjent { get; set; }
    }
}