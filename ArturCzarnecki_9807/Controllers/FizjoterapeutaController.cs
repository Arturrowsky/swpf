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


namespace ArturCzarnecki_9807.Controllers
{
    public class FizjoterapeutaController : Controller
    {

        private SystemSWPF db = new SystemSWPF();
        #region Login

      
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Fizjoterapeuta user)
        {
            if (ModelState.IsValid)
            {
                using (SystemSWPF db = new SystemSWPF()) 
                {
                    var obj = db.Fizjoterapeuta.Where(a => a.Email.Equals(user.Email) && a.Haslo.Equals(user.Haslo)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["User_ID"] = obj.IdFizjoterapeuty.ToString();
                        Session["User_Role"] = obj.IdRoli.ToString();

                        switch (obj.IdRoli.ToString())
                        {
                            case "1":
                                return RedirectToAction("Main");
                        }
                    }
                }
            }
            return View(user);
        }

        #endregion
        #region Rejestracja
        public ActionResult Rejestracja()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Rejestracja([Bind(Include = "IdFizjoterapeuty,Email,Haslo,Imie,Nazwisko,NrFizjoterapeuty,KodPocztowy,Miasto,CzyGabinet,NrTel,Opis")] Fizjoterapeuta fizjoterapeuta)
        {
            if (ModelState.IsValid)
            {
                if (db.Fizjoterapeuta.Any(x => x.Email == fizjoterapeuta.Email))
                {
                    ModelState.AddModelError("Email", "Taki adres email już istnieje!");
                    return View(fizjoterapeuta);
                }
                if (db.Fizjoterapeuta.Any(x => x.NrFizjoterapeuty == fizjoterapeuta.NrFizjoterapeuty))
                {
                    ModelState.AddModelError("NrFizjoterapeuty", "Taki numer już istnieje!");
                    return View(fizjoterapeuta);
                }
                fizjoterapeuta.IdRoli = 1;
                db.Fizjoterapeuta.Add(fizjoterapeuta);
                db.SaveChanges();
                return RedirectToAction("Login");

            }
            return View(fizjoterapeuta);
        }


        #endregion
        #region Main

        public ActionResult Main()
        {
            if (Session["User_ID"] != null)
            {
                var id = Convert.ToInt32(Session["User_ID"]);
                Fizjoterapeuta pacjent = db.Fizjoterapeuta.Find(id);

                return View(pacjent);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }


        #endregion
        #region Godziny

        public ActionResult Godziny()
        {
            var godziny = db.Godziny.Include(g => g.Fizjoterapeuta);
            return View(godziny.ToList());
        }
        public ActionResult DodajGodziny()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DodajGodziny([Bind(Include = "IdGodziny,Godzina,IdFizjoterapeuty")] Godziny godziny)
        {
            
                if (ModelState.IsValid)
                {
                    //if (db.Godziny.Any(x => x.Godzina == godziny.Godzina))
                    //{
                    //    ModelState.AddModelError("Email", "Taki adres email już istnieje!");

                    //    return View(godziny);
                    //}
                    godziny.IdFizjoterapeuty =Convert.ToInt32( Session["User_ID"]);
                    db.Godziny.Add(godziny);
                    db.SaveChanges();

             
                
                return RedirectToAction("Godziny");
                }
            return View("Login");
            

        }

        public ActionResult UsunGodziny(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Godziny godziny = db.Godziny.Find(id);
            if (godziny == null)
            {
                return HttpNotFound();
            }
            return View(godziny);
        }

        // POST: Godzinies/Delete/5
        [HttpPost, ActionName("UsunGodziny")]
        [ValidateAntiForgeryToken]
        public ActionResult UsunGodziny(int id)
        {
            Godziny godziny = db.Godziny.Find(id);
            db.Godziny.Remove(godziny);
            db.SaveChanges();
            return RedirectToAction("Godziny");
        }

        #endregion
        #region Miejsca

        public ActionResult Miejsca()
        {
            if (Session["User_ID"] != null)
            {
                var miejsca = db.KodPocztowy;
                return View(miejsca.ToList());
            }
            else
            {
                return RedirectToAction("Login");
            }
            }
        public ActionResult DodajMiejsca()
        {
                if (Session["User_ID"] != null)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Login");
                }
                }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DodajMiejsca([Bind(Include = "IdKodPocztowy,KodPocztowy1,IdFizjoterapeuty")] KodPocztowy miejsca)
        {
                    if (Session["User_ID"] != null)
                    {
                        if (ModelState.IsValid)
            {
                //if (db.KodPocztowy.Any(x => x.KodPocztowy1 == miejsca.KodPocztowy1))
                //{
                //    ModelState.AddModelError("Email", "Taki adres email już istnieje!");

                //    return View(miejsca);
                //}
                miejsca.IdFizjoterapeuty = Convert.ToInt32(Session["User_ID"]);
                db.KodPocztowy.Add(miejsca);
                db.SaveChanges();



                return RedirectToAction("Miejsca");
            }
            return View("Login");
            }
            else
            {
                return RedirectToAction("Login");
            }

        }

        public ActionResult UsunMiejsca(int? id)
        {
                        if (Session["User_ID"] != null)
                        {
                            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KodPocztowy miejsca = db.KodPocztowy.Find(id);
            if (miejsca == null)
            {
                return HttpNotFound();
            }
            return View(miejsca);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        // POST: Godzinies/Delete/5
        [HttpPost, ActionName("UsunMiejsca")]
        [ValidateAntiForgeryToken]
        public ActionResult UsunMiejsca(int id)
        {
                            if (Session["User_ID"] != null)
                            {
                                KodPocztowy miejsca = db.KodPocztowy.Find(id);
            db.KodPocztowy.Remove(miejsca);
            db.SaveChanges();
            return RedirectToAction("Miejsca");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        #endregion

        public ActionResult Wizyty()
        {
            if (Session["User_ID"] != null)
            {
                List<Wizyta> wizyty = new List<Wizyta>();
                foreach (var item in db.Wizyta)
                {
                    if (item.IdFizjoterapeuty == Convert.ToInt32(Session["User_ID"]))
                    {
                        wizyty.Add(item);
                    }
                }

                return View(wizyty);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }


        public ActionResult Edytuj(int?id)
        {
            if (Session["User_ID"] != null)
            {
                if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wizyta wizyta = db.Wizyta.Find(id);
            if (wizyta == null)
            {
                return HttpNotFound();
            }
          
            return View(wizyta);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edytuj(int? id, [Bind(Include = "IdWizyty,IdFizjoterapeuty,IdPacjenta,Godzina,Data,Opis")] Wizyta wizyta)
        {
            if (Session["User_ID"] != null)
            {
                

                if (ModelState.IsValid)
            {
                    wizyta.IdFizjoterapeuty = db.Wizyta.Find(id).IdFizjoterapeuty;
                    wizyta.IdPacjenta = db.Wizyta.Find(id).IdPacjenta;
                    wizyta.Godzina = db.Wizyta.Find(id).Godzina;
                    wizyta.Data = db.Wizyta.Find(id).Data;
                    db.Entry(wizyta).State = EntityState.Modified;
                    db.Wizyta.Remove(db.Wizyta.Find(id));
                    db.Wizyta.Add(wizyta);
                    db.SaveChanges();
                   
                return RedirectToAction("Wizyty");
            }
            
            return View(wizyta);
        }
            else
            {
                return RedirectToAction("Login");
    }
}
        public ActionResult Wyloguj()
        {
            Session.Clear();
            return View("Login");
        }

      
    }
}
