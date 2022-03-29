using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAplicationProduct.Models;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;
namespace WebAplicationProduct.Controllers
{
    public class ProdutoController : Controller
    {
        public Context context;
        public ProdutoController(Context ctx)
        {
            context = ctx;
        }
        public IActionResult Index(int pagina = 1)
        {
            return View(context.Produtos.Include(f => f.Fabricante).ToPagedList(pagina, 3));
            
            
        }
        public IActionResult Create()
        {
            ViewBag.FabricanteID = new SelectList(context.Fabricantes.OrderBy(f => f.Nome), "FabricanteID", "Nome");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Produto produto)
        {
            context.Add(produto);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Details(int id)
        {
            var produto = context.Produtos
                .Include(f => f.Fabricante)
                .FirstOrDefault(p => p.ProdutoID == id);
            return View(produto);
        }
        public IActionResult Edit(int id)
        {
            var produto = context.Produtos.Find(id);
            ViewBag.FabricanteID = new SelectList(context.Fabricantes.OrderBy(f => f.Nome), "FabricanteID", "Nome");
            return View(produto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Produto produto)
        {
            context.Entry(produto).State = EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var produto = context.Produtos
                .Include(f => f.Fabricante)
                .FirstOrDefault(p => p.ProdutoID == id);
            return View(produto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Produto produto)
        {
            context.Remove(produto);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
