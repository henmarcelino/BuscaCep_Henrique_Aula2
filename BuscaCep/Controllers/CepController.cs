/*
 * Marcos Vinicius da Cruz Rubin
 * Jean Lucas Trizotti
 * Henrique Marcelino da Luz
 */ 

using BuscaCep.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BuscaCep.Controllers
{
    public class CepController : Controller
    {
        private readonly Context _context;

        public CepController(Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Enderecos.ToList());
        }

        public IActionResult BuscaCep()
        {
            var cep = HttpContext.Request.Query["cep"].ToString();
            WebClient client = new WebClient();
            string json = client.DownloadString("http://viacep.com.br/ws/" + cep + "/json/");
            Endereco endereco = JsonConvert.DeserializeObject<Endereco>(json);
            _context.Enderecos.Add(endereco);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
