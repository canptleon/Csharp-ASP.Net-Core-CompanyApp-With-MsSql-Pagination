using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjeCore.Models;
using X.PagedList;

namespace ProjeCore.Controllers
{
    public class PaginationController : Controller
    {
        Context c = new Context();

        public IActionResult Index(int page = 1)
        {
            var _birim = c.Birims.Where(r => !r.IsDeleted);

            int pageSize = 5;
            var onePageOfBirims = _birim.ToPagedList(page, pageSize);
            return View(onePageOfBirims);
        }
    }
}
