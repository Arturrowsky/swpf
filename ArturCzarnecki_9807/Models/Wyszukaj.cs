using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ArturCzarnecki_9807;
namespace ArturCzarnecki_9807.Models
{
    public class Wyszukaj
    {
        public Fizjoterapeuta Fizjoterapeuta { get; set; }
        public Godziny Godziny { get; set; }
        public Wizyta Wizyta { get; set; }
        public virtual ICollection<KodPocztowy> KodPocztowy { get; set; } = new List<KodPocztowy>();
    }
}