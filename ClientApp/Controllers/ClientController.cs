#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClientApp.Data;
using ClientApp.Models;
using ClientApp.Services.Ibge;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClientApp.Controllers
{
    public class ClientController : Controller
    {
        private readonly ClientAppContext _context;
        private readonly IIbgeService _ibgeService;

        public ClientController(ClientAppContext context, IIbgeService ibgeService)
        {
            _context = context;
            _ibgeService = ibgeService;
        }

        // GET: Client
        public async Task<IActionResult> Index()
        {
            return View(await _context.Clients.ToListAsync());
        }

        // GET: Client/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Client/Create
        public async Task<IActionResult> Create()
        {           
            ViewData["States"] = (await _ibgeService.GetEstadosAsync())
                .Select(es => new SelectListItem(es.Nome, es.Sigla));

            return View();
        }

        // POST: Client/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Age,Gender,State,City")] Client client)
        {
            if (ModelState.IsValid)
            {
                _context.Add(client);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        // GET: Client/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            ViewData["States"] = (await _ibgeService.GetEstadosAsync())
                .Select(es => new SelectListItem(es.Nome, es.Sigla));

            ViewData["Cities"] = (await _ibgeService.GetMunicipiosPorEstadoAsync(client.State))
                .Select(mc => new SelectListItem(mc.Nome, mc.Nome));

            return View(client);
        }

        // POST: Client/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Age,Gender,State,City")] Client client)
        {
            if (id != client.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        // GET: Client/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Client/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Client/GetCitiesByUf
        public async Task<IEnumerable<Municipio>> GetCitiesByUf(string uf)
        {
            return await _ibgeService.GetMunicipiosPorEstadoAsync(uf);
        }

        private bool ClientExists(int id)
        {
            return _context.Clients.Any(e => e.Id == id);
        }
    }
}
