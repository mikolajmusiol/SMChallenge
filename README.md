ASP.NET Core 6 Web API designed to automate desk bookings in offices

## Technologies 
* ASP.NET Core 6
* Entity Framework Core 6
* Microsoft SQL Server
* JWT
* AutoMapper
* FluentValidation
* NLog
* Swagger

## Endpoints
### Account
* Register new user
* Login user

### Location
* (*admin*) Get all locations
* (*admin*) Add location
* (*admin*) Delete location

### Desk
* (*employee*) Get all desks
* (*admin*) Add desk
* (*admin*) Delete desk
* (*admin*) Make unavailable

* ### Booking
* Get all bookings
  *Administration sees the details, employees sees which desks are unavailable*
* Book desk
  *Reservation available for no more than 7 days*
* Change desk in booking
  *Changing the desk in the reservation is available no later than 24 hours before the booking*
