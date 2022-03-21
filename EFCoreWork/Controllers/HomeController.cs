using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EFCoreWork.Interface;
using DataDB;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EFCoreWork.Controllers
{
    [Route("home")]
    public class HomeController : Controller
    {
        private readonly IProvider _provider;

        public HomeController(IProvider provider)
        {
            _provider = provider;
        }
        [HttpGet]
        public ActionResult Home()
        {
            var Uow = _provider.CreateUnitOfWork();
            var users = Uow.GetRepository<Users>();
            return Ok(users.GetAll());
        }
    }
}
