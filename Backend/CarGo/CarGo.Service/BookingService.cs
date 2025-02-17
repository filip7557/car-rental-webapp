using CarGo.Common;
using CarGo.Model;
using CarGo.Repository.Common;
using CarGo.Service.Common;

namespace CarGo.Service
{
    public class BookingService : IBookService
    {
        private IBookRepository _repository;
        private readonly ITokenService _tokenService;
        private readonly INotificationService _notificationService;
        private readonly ICompanyVehicleService _companyVehicleService;
        private readonly IVehicleModelService _vehicleModelService;
        private readonly IVehicleMakeService _vehicleMakeService;
        private readonly ICompanyService _companyService;
        private readonly IUserService _userService;
        

        public BookingService(IBookRepository repository, ITokenService tokenService, INotificationService notificationService, ICompanyVehicleService companyVehicleService, IVehicleModelService vehicleModelService, IVehicleMakeService vehicleMakeService, ICompanyService companyService, IUserService userService)
        {
            _repository = repository;
            _tokenService = tokenService;
            _notificationService = notificationService;
            _companyVehicleService = companyVehicleService;
            _vehicleModelService = vehicleModelService;
            _vehicleMakeService = vehicleMakeService;
            _companyService = companyService;
            _userService = userService;
   
        }

        public async Task<List<Booking>> GetAllBookingsAsync(BookingSorting sorting, BookingPaging paging, BookingFilter filter)
        {
            var userId = _tokenService.GetCurrentUserId();
            var userRole = _tokenService.GetCurrentUserRoleName();


            filter.UserId = userId;
            filter.UserRole = userRole;


            if (userRole == "Administrator")
            {
                
                filter.IsActive = null;
            }
            else if (userRole == "User")
            {
       
                filter.IsActive = true;
            }

            if (userRole == "Administrator")
            {
                filter.UserId = null;  
            }

            return await _repository.GetAllBookingsAsync(sorting, paging, filter);
        }
        public async Task<Booking> GetBookingByIdAsync(Guid id)
        {
            return await _repository.GetBookingByIdAsync(id);
        }

        public async Task AddBookingAsync(Booking booking)
        {
            var userId = _tokenService.GetCurrentUserId();
            await _repository.AddBookingAsync(booking, userId);
            var companyVehicle = await _companyVehicleService.GetCompanyVehicleByIdAsync(booking.CompanyVehicleId);
            var vehicleModel = await _vehicleModelService.GetByIdAsync(companyVehicle.VehicleModelId);
            var user = await _userService.GetUserDTOByIdAsync(booking.UserId);
            var company = await _companyService.GetCompanyAsync(companyVehicle.CompanyId);
            var notification = new Notification
            {
                Title = "CarGo - Booking Created",
                Text = $"Hello!" +
                $"\n\n{user!.FullName}, you have successfully created a booking." +
                $"\nStart date: {booking.StartDate} | End date: {booking.EndDate}" +
                $"\nVehicle: {await _vehicleMakeService.GetByIdAsync(vehicleModel!.MakeId)} {vehicleModel.Name} Power: {vehicleModel.EnginePower} HP" +
                $"\nCompany: {company!.Name}" +
                $"\nTotal price: {booking.TotalPrice} €" +
                $"\n\nBest regards," +
                $"\nCarGo Administration team.",
                To = user.Email,
            };
            await _notificationService.SendNotificationAsync(notification);
        }

        public async Task UpdateBookingAsync(Guid id, Booking updatedBooking)
        {
            var userId = _tokenService.GetCurrentUserId();
            await _repository.UpdateBookingAsync(id, updatedBooking, userId);
        }

        public async Task UpdateBookingStatusAsync(Guid id, BookingStatus status)
        {
            var userId = _tokenService.GetCurrentUserId();
            await _repository.UpdateBookingStatusAsync(id, status, userId);
        }

        public async Task SoftDeleteBookingAsync(Guid id)
        {
            await _repository.SoftDeleteBookingAsync(id);
        }
    }
}