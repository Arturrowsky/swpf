using System;
using System.Collections.Generic;
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
    public class PacjentController : Controller
    {
        private SystemSWPF db = new SystemSWPF();
        public ActionResult Main()
        {
            if (Session["User_ID"] != null)
            {
                var id = Convert.ToInt32(Session["User_ID"]);
                Pacjent pacjent = db.Pacjent.Find(id);

                return View(pacjent);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        #region Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Pacjent user)
        {
            if (ModelState.IsValid)
            {

                using (SystemSWPF db = new SystemSWPF())
                {
                    var obj = db.Pacjent.Where(a => a.Email.Equals(user.Email) && a.Haslo.Equals(user.Haslo)).FirstOrDefault();

                    if (obj != null)
                    {

                        Session["User_ID"] = obj.IdPacjenta.ToString();

                        Session["User_Role"] = obj.IdRoli.ToString();


                        switch (obj.IdRoli.ToString())
                        {
                            case "2":
                                return RedirectToAction("Main");
                        }
                    }
                }
            }
            return View(user);
        }

        #endregion
        #region rejestracja
        public ActionResult Rejestracja()
        {
            return View();
        }
      
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Rejestracja([Bind(Include = "IdPacjenta,Email,Haslo,Imie,Nazwisko,KodPocztowy,Miasto,Ulica,Lokal,NrTelefonu")] Pacjent pacjent)
        {

            if (ModelState.IsValid)
            {
                if (db.Pacjent.Any(x => x.Email == pacjent.Email))
                {
                    ModelState.AddModelError("Email", "Taki adres email już istnieje!");
                    return View(pacjent);
                }
                pacjent.IdRoli = 2;
                db.Pacjent.Add(pacjent);
                db.SaveChanges();
                return RedirectToAction("Login");

            }
            return View(pacjent);

        }

        #endregion


        public ActionResult Wyszukaj( )
        {
            if (Session["User_ID"] != null)
            {

                List<Fizjoterapeuta> dane = new List<Fizjoterapeuta>();
                var id = Convert.ToInt32(Session["User_ID"]);
                Pacjent pacjent = db.Pacjent.Find(id);
                
                foreach (var item in db.KodPocztowy)
                {
                    if(item.KodPocztowy1==pacjent.KodPocztowy)
                    {
                        Fizjoterapeuta fizjoterapeuta = db.Fizjoterapeuta.Find(item.IdFizjoterapeuty);
                        dane.Add( fizjoterapeuta);
                    }

                }
                

                return View(dane);
            }
            else
            {
                return RedirectToAction("Main");
            }
            
        }

        #region Umow
        //public ActionResult Umow()
        //{
        //    return View();
        //}
        //wyswietlic dane wybranego fizjo
        //wyswietlic pole z kalendarzem
        //wyswietlic liste dostępnych godzin
        //wizyta: id wizyty id fizjo id pacjenta data godzina
        //public ActionResult Umow()
        //{ return View(); }
        //List<Godziny> godziny = new List<Godziny>();
      
        public ActionResult Umow(int ?id )
        {
           
            List<Godziny> godziny = new List<Godziny>();
            if (Session["User_ID"] != null)
            {
                foreach (var item in db.Godziny)
                {
                    if (item.IdFizjoterapeuty == id)
                        godziny.Add(item);
                }
                              
                ViewBag.Godzina = new SelectList(godziny, "Godzina", "Godzina");
                return View();
            }
            else
            {
                return RedirectToAction("Main");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Umow(int? id,[Bind(Include = "IdWizyty,IdFizjoterapeuty,IdPacjenta,Godzina,Data,Opis")] Wizyta wizyta)
        {
            if (Session["User_ID"] != null)
            {
                wizyta.IdFizjoterapeuty = (int)id;
                wizyta.IdPacjenta = Convert.ToInt32(Session["User_ID"]);
                if (ModelState.IsValid)
                    {
                    if (wizyta.Godzina==null || wizyta.Data==null)
                    {
                        ModelState.AddModelError("Godzina", "Nie podałeś godziny lub daty wizyty");
                        return View(wizyta);
                    }
                        db.Wizyta.Add(wizyta);
                        db.SaveChanges();
                        return RedirectToAction("Main");
                   
                                          
                    }
                ViewBag.Godzina = new SelectList(db.Godziny, "Godzina", "Godzina", wizyta.Godzina);
                return View(wizyta);
             }
            else
            {
                return RedirectToAction("Main");
            }

        }

#endregion






#region Wizyty
public ActionResult Wizyty()
        {
            if (Session["User_ID"] != null)
            {
                List<Wizyta> wizyty = new List<Wizyta>();
            foreach (var item in db.Wizyta)
            {
                    if(item.IdPacjenta==Convert.ToInt32( Session["User_ID"]))
                    {
                        wizyty.Add(item);
                    }    
            }
                
            return View(wizyty);
            }
            else
            {
                return RedirectToAction("Main");
            }
        }
        #endregion


        public ActionResult Wyloguj()
        {
            Session.Clear();
            return View("Login");
        }

       
    
    }
  
}
