using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PSAZDemo1.NetCoreWebApplication.Model;

namespace PSAZDemo1.NetCoreWebApplication.Pages
{
    public class EmployeeModel : PageModel
    {
        private readonly ILogger<EmployeeModel> _logger;
        private Model.DataContext _dbContext;

        public EmployeeModel(DataContext ctx)
        {
            _dbContext = ctx;
        }

        [BindProperty]
        public List<Employee> Employees { get; set; }

        public IActionResult OnGet()
        {
            var employees = from e in _dbContext.Employee
                            select e;

            Employees = employees.ToList();
            return Page();
        }

        public async Task<IActionResult> OnPostSubmit(Employee model)
        {
            if (ModelState.IsValid)
            {
                //save new employee to the database
                _dbContext.Employee.Add(model);
                await _dbContext.SaveChangesAsync();

            }
            return RedirectToPage("Employee");

        }
    }
}