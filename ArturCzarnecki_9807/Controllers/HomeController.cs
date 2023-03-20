using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ArturCzarnecki_9807;
using ArturCzarnecki_9807.Models;

namespace ArturCzarnecki_9807.Controllers
{
    public class HomeController : Controller
    {
        private SystemSWPF db = new SystemSWPF();
        

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Wyszukaj()
        {
            var wyszukaj = db.Fizjoterapeuta;

            return View(wyszukaj.ToList());
        }
        public ActionResult Zaloguj()
        {
            return View();
        }
        public ActionResult Rejestracja()
        {
            return View();
        }
        public ActionResult Informacja()
        {
            return View();
        }
        //public ActionResult Umow()
        //{

        //    return View();
        //}
        public ActionResult Umow(int ? id, Wyszukaj wyszukaj )
        {
           
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            Fizjoterapeuta fizjoterapeuta= db.Fizjoterapeuta.Find(id);
            wyszukaj.Fizjoterapeuta = fizjoterapeuta;
            
            foreach (var item in db.Godziny)
            {
                if (item.IdFizjoterapeuty == id)
                {
                    wyszukaj.Godziny= item;
                   
                }
            }
            foreach (var item in db.KodPocztowy)
            {
                if (item.IdFizjoterapeuty == id)
                {
                    wyszukaj.KodPocztowy.Add(item);
                    
                }
            }
            if (wyszukaj == null)
            {
                return HttpNotFound();
            }
            foreach (var item in wyszukaj.KodPocztowy)
            {
                ViewBag.KodPocztowy = item.KodPocztowy1;
            }
            return View(wyszukaj);
        }

        //    public ActionResult Umow(int? id)
        //    {
        //        if (id == null)
        //        {
        //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //        }
        //        KodPocztowy godziny = db.KodPocztowy.Find(id);


        //        if (godziny == null)
        //        {
        //            return HttpNotFound();
        //        }
        //        return View(godziny);

        //    }
        //    [HttpPost, ActionName("Umow")]
        //    [ValidateAntiForgeryToken]
        //    public ActionResult Umow(int id)
        //    {
        //        Fizjoterapeuta godziny = db.Fizjoterapeuta.Find(id);

        //        return View();
        //    }
        //    //[HttpPost]
        //    //[ValidateAntiForgeryToken]
        //    //public ActionResult Umow([Bind(Include = "IdWizyty,IdFizjoterapeuty,IdPacjenta,Godzina,Data,Opis")] Wizyta wizyta, int? id)
        //    //{
        //    //    if (ModelState.IsValid)
        //    //    {
        //    //        db.Wizyta.Add(wizyta);
        //    //        db.SaveChanges();
        //    //        return RedirectToAction("Index");
        //    //    }

        //    //    ViewBag.IdFizjoterapeuty = new SelectList(db.Fizjoterapeuta, "IdFizjoterapeuty", "Email", wizyta.IdFizjoterapeuty);
        //    //    ViewBag.IdPacjenta = new SelectList(db.Pacjent, "IdPacjenta", "Email", wizyta.IdPacjenta);
        //    //    return View(wizyta);
        //    //}
        //}
    }
}