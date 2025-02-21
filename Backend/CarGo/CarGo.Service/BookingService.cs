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
        private readonly IBookingStatusRepository _bookingStatus;


        public BookingService(IBookRepository repository, ITokenService tokenService,IBookingStatusRepository bookingStatusRepository, INotificationService notificationService, ICompanyVehicleService companyVehicleService, IVehicleModelService vehicleModelService, IVehicleMakeService vehicleMakeService, ICompanyService companyService, IUserService userService)
        {
            _repository = repository;
            _bookingStatus= bookingStatusRepository;
            _tokenService = tokenService;
            _notificationService = notificationService;
            _companyVehicleService = companyVehicleService;
            _vehicleModelService = vehicleModelService;
            _vehicleMakeService = vehicleMakeService;
            _companyService = companyService;
            _userService = userService;
   
        }

        public async Task<List<BookingDto>> GetAllBookingsAsync(BookingSorting sorting, BookingPaging paging, BookingFilter filter)
        {
            var userId = _tokenService.GetCurrentUserId();
            var userRole = _tokenService.GetCurrentUserRoleName();

            filter.UserId = userId;
            filter.UserRole = userRole;

            if (userRole == "Administrator")
            {
                filter.IsActive = null;
                filter.UserId = null;
            }
            else if (userRole == "User")
            {
                filter.IsActive = true;
            }

            var bookings = await _repository.GetAllBookingsAsync(sorting, paging, filter);
            var bookingResponses = new List<BookingDto>();

            foreach (var booking in bookings)
            {

                var companyVehicle = await _companyVehicleService.GetCompanyVehicleByIdAsync(booking.CompanyVehicleId);

                var vehicleModel = companyVehicle != null
                    ? companyVehicle.VehicleModel
                    : null;
                var vehicleMake = vehicleModel != null
                    ? companyVehicle.VehicleMake
                    : null;
                var company = companyVehicle != null
                    ? await _companyService.GetCompanyAsync((Guid)companyVehicle.CompanyId!)
                    : null;
                var status = booking.StatusId != Guid.Empty
                    ? await _bookingStatus.GetByIdAsync(booking.StatusId)
                    : null;

                var response = new BookingDto
                {
                    Id = booking.Id,
                    CompanyId = company.Id,
                    BookingStatus = status?.Name ?? "Unknown",
                    VehicleMake = vehicleMake ?? "Unknown",
                    VehicleModel = vehicleModel ?? "Unknown",
                    CompanyName = company?.Name ?? "Unknown",
                    TotalPrice = booking.TotalPrice,
                    StartDate = booking.StartDate,
                    EndDate = booking.EndDate
                };

                bookingResponses.Add(response);
            }

            return bookingResponses;
        }


        public async Task<Booking> GetBookingByIdAsync(Guid id)
        {
            return await _repository.GetBookingByIdAsync(id);
        }

        public async Task AddBookingAsync(Booking booking)
        {
            var userId = _tokenService.GetCurrentUserId();
            booking.UserId = userId;
            var statuses = await _bookingStatus.GetAllAsync();
            Console.WriteLine(statuses.ToString());
            booking.StatusId = statuses.FirstOrDefault(p => p.Name.Equals("Active"))!.ID;
            Console.WriteLine(booking.StatusId);
            await _repository.AddBookingAsync(booking, userId);
            var companyVehicle = await _companyVehicleService.GetCompanyVehicleByIdAsync(booking.CompanyVehicleId);
            var wholeCompanyVehicle = await _companyVehicleService.GetWholeCompanyVehicleByIdAsync(booking.CompanyVehicleId);
            wholeCompanyVehicle.CurrentLocationId = null;
            await _companyVehicleService.UpdateCompanyVehicleAsync(booking.CompanyVehicleId, wholeCompanyVehicle!);
            var vehicleModel = companyVehicle.VehicleModel;
            var user = await _userService.GetUserDTOByIdAsync(booking.UserId);
            var company = await _companyService.GetCompanyAsync((Guid)companyVehicle.CompanyId!);
            var notification = new Notification
            {
                Title = "CarGo - Booking Created",
                Text = $"Hello!" +
                $"\n\n{user!.FullName}, you have successfully created a booking." +
                $"\nStart date: {booking.StartDate} | End date: {booking.EndDate}" +
                $"\nVehicle: {companyVehicle.VehicleMake} {vehicleModel} Power: {companyVehicle.EnginePower} HP" +
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

        public async Task UpdateBookingStatusAsync(Guid id)
        {
            var userId = _tokenService.GetCurrentUserId();
            await _repository.UpdateBookingStatusAsync(id, userId);
        }

        public async Task SoftDeleteBookingAsync(Guid id)
        {
            await _repository.SoftDeleteBookingAsync(id);
        }
    }
}