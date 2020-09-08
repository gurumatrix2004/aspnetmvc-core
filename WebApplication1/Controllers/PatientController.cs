using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PatientLibrary;
using PatientRepository;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    
    public class PatientController : Controller
    {

       
        public IActionResult Index()
        {  
            return View();
        }

        

        public IActionResult Add()
        {
            return View("PatientAdd");
        }

      



    }
}
