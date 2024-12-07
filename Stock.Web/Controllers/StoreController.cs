using Microsoft.AspNetCore.Mvc;
using Stock.Data.Models;
using Stock.Service.Interfaces;

namespace Stock.Web.Controllers
{
    public class StoreController : Controller
    {
        private readonly IStoreService _storeService;

        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        public async Task<IActionResult> Index()
        {
            var stores = await _storeService.GetAllStoresAsync();
            return View(stores);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> Create(Store store)
        {
            if (ModelState.IsValid)
            {
                await _storeService.AddStoreAsync(store);
                return RedirectToAction("Index");
            }
            return View(store);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var store = await _storeService.GetStoreByIdAsync(id);
            if (store == null)
            {
                return NotFound();
            }
            return View(store); 
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Store model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Console.WriteLine("ModelState is invalid. Errors:");
                    foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                    {
                        Console.WriteLine($"- {error.ErrorMessage}");
                    }
                    return View(model);
                }

                Console.WriteLine($"Updating Store: {model.StoreId} - {model.Name}");
                await _storeService.UpdateStoreAsync(model);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return View(model); 
            }
        }


        public async Task<IActionResult> Delete(int id)
        {
            await _storeService.DeleteStoreAsync(id);
            return RedirectToAction("Index");
        }
    }
}
