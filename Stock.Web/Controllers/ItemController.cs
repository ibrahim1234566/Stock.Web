using Microsoft.AspNetCore.Mvc;
using Stock.Data.Models;
using Stock.Service.Interfaces;

namespace Stock.Web.Controllers
{
    public class ItemController : Controller
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _itemService.GetAllItemsAsync();
            return View(items);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> Create(Item item)
        {
            if (ModelState.IsValid)
            {
                await _itemService.AddItemAsync(item);
                return RedirectToAction("Index");
            }
            return View(item);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _itemService.GetItemByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Item model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _itemService.UpdateItemAsync(model);
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int id)
        {
            await _itemService.DeleteItemAsync(id);
            return RedirectToAction("Index");
        }
    }
}
