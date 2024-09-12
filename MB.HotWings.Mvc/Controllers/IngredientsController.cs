using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MB.HotWings.Entities.Ingredients;
using MB.HotWings.Mvc.Data;
using AutoMapper;
using MB.HotWings.Mvc.Models.Ingredients;

namespace MB.HotWings.Mvc.Controllers
{
    public class IngredientsController : Controller
    {
        #region Data and Const

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public IngredientsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #endregion

        #region Actions

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var ingredients = await _context
                                        .Ingredients
                                        .ToListAsync();

            var ingredientVMs = _mapper.Map<List<IngredientViewModel>>(ingredients);


            return View(ingredientVMs);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingredient = await _context
                                        .Ingredients
                                        .FindAsync(id);

            if (ingredient == null)
            {
                return NotFound();
            }

            var ingredientVM = _mapper.Map<IngredientDetailsViewModel>(ingredient);

            return View(ingredientVM);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUpdateIngredientViewModel createUpdateIngredientVM)
        {
            if (ModelState.IsValid)
            {
                var ingredient = _mapper.Map<Ingredient>(createUpdateIngredientVM);

                _context.Add(ingredient);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(createUpdateIngredientVM);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingredient = await _context
                                        .Ingredients
                                        .FindAsync(id);

            if (ingredient == null)
            {
                return NotFound();
            }

            var createUpdateIngredientVM = _mapper.Map<CreateUpdateIngredientViewModel>(ingredient);

            return View(createUpdateIngredientVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateUpdateIngredientViewModel createUpdateIngredientVM)
        {
            if (id != createUpdateIngredientVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Get ingredient from the DB
                var ingredient = await  _context
                                            .Ingredients
                                            .FindAsync(id);

                if(ingredient == null)
                {
                    return NotFound();
                }

                // Patch (Copy) the createUpdateIngredientVM into the ingredient
                _mapper.Map(createUpdateIngredientVM, ingredient);

                // Update in the _context and save
                _context.Update(ingredient);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(createUpdateIngredientVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id) // 1 => Tomato
        {
            var ingredient = await _context
                                    .Ingredients
                                    .FindAsync(id); // Get Tomato

            if (ingredient == null)
            {
                return RedirectToAction(nameof(Index));
            } // If tomato is not in the DB return to Index page
            
            
            _context.Ingredients.Remove(ingredient); // Remove Tomato memory
            await _context.SaveChangesAsync(); // Confirm deleting Tomato from DB

            return RedirectToAction(nameof(Index)); // Return to index page
        }

        #endregion

        #region Private Methods

        #endregion
    }
}
