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
    
    public partial class KodPocztowy
    {
        public int IdKodPocztowy { get; set; }
        public string KodPocztowy1 { get; set; }
        public int IdFizjoterapeuty { get; set; }
    
        public virtual Fizjoterapeuta Fizjoterapeuta { get; set; }
    }
}