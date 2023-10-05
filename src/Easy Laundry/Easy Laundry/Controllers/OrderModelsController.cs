using Easy_Laundry.Data;
using Easy_Laundry.Models;
using Easy_Laundry.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Easy_Laundry.Controllers
{
    public class OrderModelsController : Controller
    {
        private readonly Easy_LaundryContext _context;

        public OrderModelsController(Easy_LaundryContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index1()
        {
            return _context.OrderModel != null ?
                        View(await _context.OrderModel.ToListAsync()) :
                        Problem("Entity set 'Easy_LaundryContext.OrderModel'  is null.");
        }
        // GET: OrderModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.OrderModel == null)
            {
                return NotFound();
            }

            var orderModel = await _context.OrderModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderModel == null)
            {
                return NotFound();
            }

            return View(orderModel);
        }
        public async Task<IActionResult> ConfirmOrder(int id)
        {
            TempData["Message"] = "Order confirmed successfully. " +
                "Thank You For Choosing Us";

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> CancelOrder(int id)
        {
            var orderModel = await _context.OrderModel.FindAsync(id);
            if (orderModel != null)
            {
                _context.OrderModel.Remove(orderModel);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Create");
        }

        // GET: OrderModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OrderModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ContactNumber,Address,Quantity,ClothType,LaundryType")] OrderModel orderModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = orderModel.Id });
            }
            return View(orderModel);
        }

        // POST: OrderModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        // GET: OrderModels/Details2
        public async Task<IActionResult> Track(int? id)
        {
            if (id == null)
            {
                return View();
            }

            OrderModel order = await FetchOrderById(id.Value);

            if (order != null)
            {
                return View(order);
            }
            else
            {
                return View();
            }
        }

        private async Task<OrderModel> FetchOrderById(int id)
        {
            var orderModel = await _context.OrderModel.FindAsync(id);
            return orderModel;
        }

        public async Task<IActionResult> Details2(int? id)
        {
            if (id == null || _context.OrderModel == null)
            {
                return NotFound();
            }

            var orderModel = await _context.OrderModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderModel == null)
            {
                return NotFound();
            }

            return View(orderModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id == null || _context.OrderModel == null)
            {
                return NotFound();
            }

            var orderModel = await _context.OrderModel.FindAsync(id);
            if (orderModel == null)
            {
                return NotFound();
            }

            OrderViewModel orderViewModel = new OrderViewModel
            {
                Id = orderModel.Id,
                Status = orderModel.Status,
                CompletionDate = orderModel.CompletionDate
            };

            return View(orderViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, OrderViewModel updatedOrderViewModel)
        {
            if (id != updatedOrderViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingOrderModel = await _context.OrderModel.FindAsync(id);

                    if (existingOrderModel == null)
                    {
                        return NotFound();
                    }

                    existingOrderModel.Status = updatedOrderViewModel.Status;
                    existingOrderModel.CompletionDate = updatedOrderViewModel.CompletionDate;

                    _context.Update(existingOrderModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderModelExists(updatedOrderViewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index1));
            }
            return View(updatedOrderViewModel);
        }

        // GET: OrderModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderModel = await _context.OrderModel
                .FirstOrDefaultAsync(m => m.Id == id);

            if (orderModel == null)
            {
                return NotFound();
            }

            return View(orderModel);
        }

        // POST: OrderModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderModel = await _context.OrderModel.FindAsync(id);

            if (orderModel == null)
            {
                return NotFound();
            }

            _context.OrderModel.Remove(orderModel);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index1));
        }


        private bool OrderModelExists(int id)
        {
            return (_context.OrderModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

