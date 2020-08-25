using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace StoreApp.Core.Controllers
{
    public class FeaturesController : Controller
    {
        public IActionResult index() => View();

        public IActionResult pos() => View();
        public IActionResult products() => View();
        public IActionResult pricing() => View();
        public IActionResult inventory() => View();

        public IActionResult customers() => View();
        public IActionResult staffs() => View();
        public IActionResult payroll() => View();
        public IActionResult accounts() => View();

        public IActionResult reporting() => View();
        public IActionResult outlets() => View();
        public IActionResult ecommerce() => View();

        public IActionResult support() => View();
    }
}
