using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using FluentValidation;

namespace Assignment1.Controllers
{
    [ApiController]
    [Route("/assignment1/api/[controller]")]
    public class PersonController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreatePerson(Person person)
        {
            var validator = new PersonValidator();
            var validationResult = validator.Validate(person);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(errors);
            }
            return Ok("Person created successfully.");
        }
    }
    public class Person
    {
        public string? Name { get; set; }
        public string? Lastname { get; set; }
        public string? Phone { get; set; }
        public int AccessLevel { get; set; }
        public decimal Salary { get; set; }
    }
   
    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .WithMessage("Person name is required.")
                .Length(5, 100)
                .WithMessage("Person name must be between 5 and 100 characters.");

            RuleFor(p => p.Lastname)
                .NotEmpty()
                .WithMessage("Person lastname is required.")
                .Length(5, 100)
                .WithMessage("Person lastname must be between 5 and 100 characters.");

            RuleFor(p => p.Phone)
                .NotEmpty()
                .WithMessage("Person phone number is required.")
                .MaximumLength(9)
                .WithMessage("Please enter a valid phone number.");

            RuleFor(p => p.AccessLevel)
                .NotEmpty()
                .WithMessage("Staff person access level is required.")
                .InclusiveBetween(1, 5)
                .WithMessage("Staff person access level must be between 1 and 5.");

            RuleFor(p => p.Salary)
                .NotEmpty()
                .WithMessage("Person salary is required.")
                .InclusiveBetween(5000, 50000)
                .WithMessage("Person salary must be between 5000 and 50000.");
        }
    }
}
