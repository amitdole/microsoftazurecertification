using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PSAZDemo1.NetCoreWebApplication.Model
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        [BindProperty]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [BindProperty]

        public DateTime JoiningDate { get; set; }
    }
}
