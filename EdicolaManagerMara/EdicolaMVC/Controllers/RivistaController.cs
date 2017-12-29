using EdicolaMVC.Models;
using System;
using System.Web.Mvc;

namespace EdicolaMVC.Controllers
{
    public class RivistaController : Controller
    {
        // GET: Rivista
        [HttpGet]
        public ActionResult Index()
        {
            var a = new RivistaModel()
            {
                DataDiConsegna = DateTime.Now.AddDays(-2),
                DataDiReso = DateTime.Now.AddDays(5),
                IdMagazine = 1,
                IdPeriodico = 1,
                IdTipologia = 1,
                ISSN = "12312312",
                NomePeriodico = "periodico di prova",
                NomeRivista = "Rivista di prova",
                Numero = 123,
                NumeroCopieRese = 0,
                NumeroCopieTotale = 100,
                NumeroCopieVendute = 30,
                Prezzo = 10

            };
            return View(a);
        }

        // GET: Rivista
        [HttpPost]
        public ActionResult Index(RivistaModel model)
        {
            return View(model);
        }
    }
}