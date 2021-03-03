using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjeCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjeCore.Controllers
{
    public class PersonelimController : Controller
    {
        Context c = new Context();

        public IActionResult Index()
        {
            //var degerler = c.Personels.Include(x => x.Birim).ToList();

            //Personel p;
            //var per = c.Personels.Include(p => p.IsDeleted = true).ToList();
            

            List<Personel> _personel = c.Personels
                                  .Include(r => r.Birim)
                                  .Where(r => r.IsDeleted == false)
                                  .ToList();

            return View(_personel);

            //return View(degerler);
        }

        [HttpGet]
        public IActionResult YeniPersonel()
        {
            List<SelectListItem> degerler = (from x in c.Birims.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = x.BirimAd,
                                                 Value = x.BirimID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;
            return View();
        }

        [HttpPost]
        public IActionResult YeniPersonel(Personel p)
        {
            var per = c.Birims.Where(x => x.BirimID == p.Birim.BirimID).FirstOrDefault();
            p.Birim = per;

            c.Personels.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult PersonelSil(int id)
        {
            var per = c.Personels.Find(id);
            per.IsDeleted = true;
            //c.Personels.Remove(per);
            c.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult PersonelGetir(int id)
        {
            var per = c.Personels.Find(id);

            List<SelectListItem> degerler = (from x in c.Birims.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = x.BirimAd,
                                                 Value = x.BirimID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;

            return View("PersonelGetir", per);
        }

        public IActionResult PersonelGuncelle(Personel p)
        {
            var per = c.Personels.Find(p.PersonelID);
            per.Ad = p.Ad;
            per.Soyad = p.Soyad;
            per.Sehir = p.Sehir;
            per.BirimID = p.Birim.BirimID;
            c.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
