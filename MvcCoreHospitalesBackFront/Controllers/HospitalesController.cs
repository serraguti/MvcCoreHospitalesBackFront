using Microsoft.AspNetCore.Mvc;
using MvcCoreHospitalesBackFront.Models;
using MvcCoreHospitalesBackFront.Services;

namespace MvcCoreHospitalesBackFront.Controllers
{
    public class HospitalesController : Controller
    {
        private ServiceApiHospital service;

        public HospitalesController(ServiceApiHospital service)
        {
            this.service = service;
        }

        public async Task<IActionResult> HospitalesBack()
        {
            List<Hospital> hospitales = await this.service.GetHospitalesAsync();
            return View(hospitales);
        }

        //EN INDEX DIBUJAREMOS UN MENU PARA IR AL BACK
        //O AL FRONT
        public IActionResult Index()
        {
            return View();
        }

        //TENDREMOS UNA VISTA PARA EL FRONT
        public IActionResult HospitalesFront()
        {
            return View();
        }
    }
}
