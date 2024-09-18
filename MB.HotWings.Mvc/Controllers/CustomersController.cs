using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MB.HotWings.Entities.Customers;
using MB.HotWings.Mvc.Data;
using AutoMapper;
using MB.HotWings.Mvc.Models.Customers;

namespace MB.HotWings.Mvc.Controllers
{
    public class CustomersController : Controller
    {
        #region Data and Const

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CustomersController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #endregion

        #region Actions

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var customers = await _context
                                    .Customers
                                    .ToListAsync();

            var customerVMs = _mapper.Map<List<CustomerViewModel>>(customers);

            return View(customerVMs);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUpdateCustomerViewModel createUpdateCustomerVM)
        {
            if (ModelState.IsValid)
            {
                var customer = _mapper.Map<Customer>(createUpdateCustomerVM);

                _context.Add(customer);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(createUpdateCustomerVM);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context
                                    .Customers
                                    .FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            var customerVM = _mapper.Map<CreateUpdateCustomerViewModel>(customer);

            return View(customerVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateUpdateCustomerViewModel createUpdateCustomerVM)
        {
            if (id != createUpdateCustomerVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Get customer from the DB
                var customer = await _context
                                        .Customers
                                        .FindAsync(id);

                if(customer == null)
                {
                    return NotFound();
                }

                // Patch the createUpdateCustomerVM into the customer
                _mapper.Map(createUpdateCustomerVM, customer);

                // Add to _context and save
                _context.Update(customer);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(createUpdateCustomerVM);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Private Methods

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        } 

        #endregion
    }
}
