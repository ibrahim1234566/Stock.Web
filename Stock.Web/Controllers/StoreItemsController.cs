using Microsoft.AspNetCore.Mvc;
using Stock.Data.Models;
using Stock.Service.Interfaces;

namespace Stock.Web.Controllers
{
    public class StoreItemsController : Controller
    {
        private readonly IStoreService _storeService;
        private readonly IItemService _itemService;
        private readonly IStoreItemService _storeItemService;

        public StoreItemsController(IStoreService storeService, IItemService itemService, IStoreItemService storeItemService)
        {
            _storeService = storeService;
            _itemService = itemService;
            _storeItemService = storeItemService;
        }

        public async Task<IActionResult> AddItem(int? storeId, int? itemId)
        {
            ViewBag.Stores = await _storeService.GetAllStoresAsync();
            ViewBag.Items = await _itemService.GetAllItemsAsync();
        

            if (storeId.HasValue && itemId.HasValue)
            {
                int quantity = await _storeItemService.GetItemQuantityAsync(storeId.Value, itemId.Value);
                ViewBag.SelectedStoreId = storeId.Value;
                ViewBag.SelectedItemId = itemId.Value;
                ViewBag.Quantity = quantity;
            }
            else
            {
                ViewBag.SelectedStoreId = null;
                ViewBag.SelectedItemId = null;
                ViewBag.Quantity = null;
            }

            return View();
        }


       [HttpPost]
public async Task<IActionResult> AddItem(int storeId, int itemId, int quantity)
{
    if (quantity > 0)
    {
        int currentQuantity = await _storeItemService.GetItemQuantityAsync(storeId, itemId);
        await _storeItemService.AddItemToStoreAsync(storeId, itemId, quantity);

        return RedirectToAction("AddItem", "StoreItems");
    }

    ModelState.AddModelError("", "Quantity must be greater than 0.");
    ViewBag.Stores = await _storeService.GetAllStoresAsync();
    ViewBag.Items = await _itemService.GetAllItemsAsync();
    ViewBag.SelectedStoreId = storeId;
    ViewBag.SelectedItemId = itemId;
    ViewBag.Quantity = await _storeItemService.GetItemQuantityAsync(storeId, itemId);

    return View();
}

        [HttpGet]
        public async Task<IActionResult> GetQuantity(int storeId, int itemId)
        {
            int quantity = await _storeItemService.GetItemQuantityAsync(storeId, itemId);
            return Json(new { quantity });
        }

    }

}

