using CarGo.Model;
using CarGo.Repository.Common;
using CarGo.Service.Common;
using Microsoft.AspNetCore.Mvc;

namespace CarGo.Service
{
    public class CompanyRequestService : ICompanyRequestService
    {
        private readonly ICompanyRequestRepository _companyRequestRepository;

        private readonly ITokenService _tokenService;

        private readonly INotificationService _notificationService;

        private readonly IManagerService _managerService;

        private readonly IUserService _userService;

        public CompanyRequestService(ICompanyRequestRepository companyRequestRepository, ITokenService tokenService, INotificationService notificationService, IManagerService managerService, IUserService userService)
        {
            _tokenService = tokenService;
            _companyRequestRepository = companyRequestRepository;
            _notificationService = notificationService;
            _managerService = managerService;
            _userService = userService;
        }

        public async Task<bool> UpdateCompanyRequestAsync(CompanyRequest companyRequest)
        {
            var userId = _tokenService.GetCurrentUserId();
            companyRequest.UpdatedByUserId = userId;
            var result = await _companyRequestRepository.UpdateCompanyRequestAsync(companyRequest);
            if (result)
            {
                if (!companyRequest.IsApproved)
                {
                    var notification = new Notification
                    {
                        Title = "CarGo - Company Request Rejected",
                        Text = "Hello!" +
                        $"\n\nYour request to create company {companyRequest.Name} has been rejected." +
                        $"\n\nBest regards," +
                        $"\nCarGo Administration Team.",
                        To = companyRequest.Email,
                    };
                    result = result & await _notificationService.SendNotificationAsync(notification);
                }
            }
            return result;
        }

        public async Task<bool> CreateCompanyAsync(Company company)
        {
            var userId = _tokenService.GetCurrentUserId();
            company.Id = Guid.NewGuid();
            company.CreatedByUserId = userId;
            company.UpdatedByUserId = userId;
            var result = await _companyRequestRepository.CreateCompanyAsync(company);
            if (result)
            {
                var notification = new Notification
                {
                    Title = "CarGo - Company Request Approved",
                    Text = "Hello!" +
                    $"\n\nYour request to create company {company.Name} has been approved." +
                    $"\nYou can now log in and manage your company." +
                    $"\n\nBest regards," +
                    $"\nCarGo Administration Team.",
                    To = company.Email,
                };
                var user = await _userService.GetUserByIdAsync(userId);
                await _managerService.AddManagerToCompanyAsync(company.Id, user!);
                return await _notificationService.SendNotificationAsync(notification);
            }
            return false;
        }

        public async Task<bool> NewCompanyRequest(CompanyRequest newCompanyRequest)
        {
            var userId = _tokenService.GetCurrentUserId();
            newCompanyRequest.UserId = userId;
            var result = await _companyRequestRepository.NewCompanyRequest(newCompanyRequest);
            if (result)
            {
                var notification = new Notification
                {
                    Title = "CarGo - Company Request Created",
                    Text = "Hello!" +
                    $"\n\nYour request to create company {newCompanyRequest.Name} has been created." +
                    $"\nWe will notify you once an administrator reviews it." +
                    $"\n\nBest regards," +
                    $"\nCarGo Administration Team.",
                    To = newCompanyRequest.Email,
                };
                return await _notificationService.SendNotificationAsync(notification);
            }
            return false;
        }

        public async Task<bool> ManageCompanyRequest(Guid userId, bool isAccepted)
        {
            try
            {
                var companyRequest = await _companyRequestRepository.GetCompanyRequestByIdAsync(userId);

                if (companyRequest == null)
                {
                    return false;
                }

                bool result = false;

                if (isAccepted)
                {
                    var newCompany = new Company
                    {
                        Id = Guid.NewGuid(),
                        Name = companyRequest.Name,
                        Email = companyRequest.Email,
                        IsActive = true,
                        CreatedByUserId = userId,
                        DateCreated = DateTime.UtcNow,
                        UpdatedByUserId = userId,
                    };

                    var resultOfCreatingCompany = await CreateCompanyAsync(newCompany);

                    if (resultOfCreatingCompany)
                    {
                        companyRequest.IsApproved = true;
                        companyRequest.IsActive = false;
                        result = await UpdateCompanyRequestAsync(companyRequest);
                    }
                }
                else
                {
                    companyRequest.IsApproved = false;
                    companyRequest.IsActive = false;
                    result = await UpdateCompanyRequestAsync(companyRequest);
                }

                if (result)
                {
                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error occurred: {e.Message}");
                return false;
            }
        }

        public async Task<List<CompanyRequest>> GetCompanyRequestAsync()
        {
            return await _companyRequestRepository.GetCompanyRequestsAsync();
        }
    }
}