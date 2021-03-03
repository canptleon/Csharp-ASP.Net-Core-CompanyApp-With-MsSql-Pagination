using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using ProjeCore.Models;

namespace ProjeCore.Controllers
{
    public class DefaultController : Controller
    {
        Context c = new Context();

        public IActionResult Index()
        {
            //var degerler = c.Birims.ToList();

            List<Birim> _birim = c.Birims
                                  .Where(r => !r.IsDeleted)
                                  .ToList();

            return View(_birim);

            //return View(degerler);
        }

        [HttpGet]
        public IActionResult YeniBirim()
        {
            return View();
        }

        [HttpPost]
        public IActionResult YeniBirim(Birim b)
        {
            c.Birims.Add(b);
            c.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult BirimSil(int id)
        {
            var bir = c.Birims.Find(id);
            bir.IsDeleted = true;
            //c.Birims.Remove(bir);
            c.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult BirimGetir(int id)
        {
            var birim = c.Birims.Find(id);

            return View("BirimGetir", birim);
        }

        public IActionResult BirimGuncelle(Birim b)
        {
            var bir = c.Birims.Find(b.BirimID);
            bir.BirimAd = b.BirimAd;
            c.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult BirimDetay(int id)
        {
            var degerler = c.Personels.Where(x => x.BirimID == id).ToList();
            var birimad = c.Birims.Where(x => x.BirimID == id).Select(y => y.BirimAd).FirstOrDefault();
            ViewBag.brm = birimad;
            return View(degerler);
        }
    }
}
