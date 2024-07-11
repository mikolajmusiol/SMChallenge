using BookingTask.Models.DTOs;
using DeskBooking.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace BookingTask.Models.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator(SMCDbContext dbContext)
        {
            RuleFor(x => x.Password).MinimumLength(6);
            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password);
            RuleFor(x => x.Name)
                .Custom((value, context) =>
                {
                    bool nameExists = dbContext.Users.Any(u => u.Name == value);

                    if (nameExists)
                    {
                        context.AddFailure("Name", "User with this name is already registered");
                    }
                });
        }
    }
}
