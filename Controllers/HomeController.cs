using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Models;
using StoreApp.Services;

namespace StoreApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmailSender EmailSender;
        public HomeController(IEmailSender emailSender) => EmailSender = emailSender;
        public IActionResult Index() => View();
        [Route("businesses")]
        public IActionResult Businesses() => View();
        [Route("packages")]
        public IActionResult Packages() => View();
        [Route("pricing")]
        public IActionResult Pricing() => View();
        [Route("downloads")]
        public IActionResult Downloads() => View();
        [Route("blog")]
        public IActionResult Blog() => View();
        [Route("contact")]
        public IActionResult Contact() => View();
        [Route("sign-up")]
        public IActionResult Signup() => View();
        [HttpPost]
        [Route("sign-up")]
        public async Task<IActionResult> Signup(ProspectRequest model)
        {
            bool recordSaved = false;
            string exmsg = string.Empty;

            try
            {
                model.date = DateTime.Now;
                model.logEntry = $"{model.name} visited the website, and opted to try out StoreApp for their business, please contact them as soon as possible.";

                try
                {

                    var resp =
                    await Clear.Tools.ApiClient.PostAsync<ProspectRequest, ProspectResponse>(
                        "https://storeapp-api.azurewebsites.net/prospects", model, "", Tools.GetAPIKeyHeader());

                }
                catch
                {

                    var resp =
                    await Clear.Tools.ApiClient.PostAsync<ProspectRequest, ProspectResponse>(
                        "http://api.storeapp.biz/prospects", model, "", Tools.GetAPIKeyHeader());

                }

                recordSaved = true;
            }
            catch (Exception ex)
            {
                exmsg = ex.Message;
            }

            try
            {
                await EmailSender.SendProspectMessage(model, recordSaved, exmsg);
                return RedirectToAction("thankyou");
            }
            catch
            {
                TempData["error"] = "We were unable to your request, don't give up on us, please try again or send an email to storeapp@clearwox.com. Thanks.";
                return View(model);
            }
        }
        [Route("thankyou")]
        public IActionResult Thankyou() => View();

        [Route("privacy")]
        public IActionResult Privacy() => View();

        [Route("error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
