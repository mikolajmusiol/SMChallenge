using BookingTask.Models.DTOs;
using DeskBooking.Entities;
using FluentValidation;

namespace BookingTask.Models.Validators
{
    public class AddLocationDtoValidator : AbstractValidator<AddLocationDto>
    {
        public AddLocationDtoValidator(SMCDbContext dbContext)
        {
            RuleFor(x => x)
                .Custom((value, context) =>
                {
                    var location = dbContext.Locations.FirstOrDefault(x => 
                        x.Country == value.Country && 
                        x.City == value.City && 
                        x.Street == value.Street);


                    if (location is not null)
                    {
                        context.AddFailure("Location already exists in the database");
                    }
                });
        }
    }
}
