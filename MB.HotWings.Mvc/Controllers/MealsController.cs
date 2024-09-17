using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MB.HotWings.Entities.Meals;
using MB.HotWings.Mvc.Data;
using AutoMapper;
using MB.HotWings.Mvc.Models.Meals;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Dynamic;
using MB.HotWings.Entities.Ingredients;
using Microsoft.Extensions.Options;
using MB.HotWings.Mvc.AppSettings;

namespace MB.HotWings.Mvc.Controllers
{
    public class MealsController : Controller
    {
        #region Data and Const

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IOptions<HotWingsSettings> _appSettings;

        public MealsController(ApplicationDbContext context, IMapper mapper, IOptions<HotWingsSettings> appSettings)
        {
            _context = context;
            _mapper = mapper;
            _appSettings = appSettings;
        }

        #endregion

        #region Actions

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var meals = await _context
                                .Meals
                                .ToListAsync();

            var mealVMs = _mapper.Map<List<Meal>, List<MealViewModel>>(meals);

            return View(mealVMs);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meal = await _context
                                .Meals
                                .Include(meal => meal.Ingredients)
                                .Where(meal => meal.Id == id)
                                .SingleOrDefaultAsync();

            if (meal == null)
            {
                return NotFound();
            }

            var mealDetailsVM = _mapper.Map<MealDetailsViewModel>(meal);

            return View(mealDetailsVM);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var createUpdateMealVM = new CreateUpdateMealViewModel();
            createUpdateMealVM.IngredientLookup = new MultiSelectList(_context.Ingredients, "Id", "Name");

            return View(createUpdateMealVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUpdateMealViewModel createUpdateMealVM)
        {
            if (ModelState.IsValid)
            {
                var meal = _mapper.Map<Meal>(createUpdateMealVM);

                // UpdateMealIngredients
                await UpdateMealIngredients(meal, createUpdateMealVM.IngredientIds);


                // Set Meal Price
                meal.Price = GetMealPrice(meal.Ingredients);

                _context.Add(meal);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            createUpdateMealVM.IngredientLookup = new MultiSelectList(_context.Ingredients, "Id", "Name");
            return View(createUpdateMealVM);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meal = await _context
                                .Meals
                                .Include(meal => meal.Ingredients)
                                .Where(meal => meal.Id == id)
                                .SingleOrDefaultAsync();

            if (meal == null)
            {
                return NotFound();
            }

            var createUpdateMealVM = _mapper.Map<CreateUpdateMealViewModel>(meal);
            createUpdateMealVM.IngredientLookup = new MultiSelectList(_context.Ingredients, "Id", "Name");

            return View(createUpdateMealVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateUpdateMealViewModel createUpdateMealVM)
        {
            if (id != createUpdateMealVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Get meal including the ingredients from the DB
                var meal = await _context
                                    .Meals
                                    .Include(meal => meal.Ingredients)
                                    .Where(meal => meal.Id == id)
                                    .SingleOrDefaultAsync();

                // Nullability check
                if(meal == null)
                {
                    return NotFound();
                }

                // Patch the createUpdateMealVM into the meal
                _mapper.Map(createUpdateMealVM, meal);

                // Update Meal Ingredients
                await UpdateMealIngredients(meal, createUpdateMealVM.IngredientIds);

                // Set Meal Price
                meal.Price = GetMealPrice(meal.Ingredients);

                // Add to _context and save
                _context.Update(meal);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            createUpdateMealVM.IngredientLookup = new MultiSelectList(_context.Ingredients, "Id", "Name");
            return View(createUpdateMealVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var meal = await _context.Meals.FindAsync(id);
            if (meal != null)
            {
                _context.Meals.Remove(meal);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Private Methods

        private bool MealExists(int id)
        {
            return _context.Meals.Any(e => e.Id == id);
        }

        private async Task UpdateMealIngredients(Meal meal, List<int> ingredientIds)
        {
            // Clear Meal Ingredients
            meal.Ingredients.Clear();

            // Get Ingredients from the DB 
            var ingredients = await _context
                                        .Ingredients
                                        .Where(ingredient => ingredientIds.Contains(ingredient.Id))
                                        .ToListAsync();

            // Add Ingredients to the Meal
            meal.Ingredients.AddRange(ingredients);
        }

        private decimal GetMealPrice(List<Ingredient> ingredients)
        {
            var subPrice = ingredients.Sum(ingredient => ingredient.Price);
            var totalPrice = subPrice * _appSettings.Value.ProfitMargin;

            return totalPrice;
        }

        #endregion
    }
}
