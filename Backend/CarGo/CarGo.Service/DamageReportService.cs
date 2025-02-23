using CarGo.Common;
using CarGo.Model;
using CarGo.Repository.Common;
using CarGo.Service.Common;
using Microsoft.IdentityModel.Tokens;

namespace CarGo.Service
{
    public class DamageReportService : IDamageReportService
    {
        private readonly IDamageReportRepository _damageReportRepository;
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;
        private readonly IImageService _imageService;

        public DamageReportService(IDamageReportRepository damageReportRepository, ITokenService tokenService, IUserService userService, IImageService imageService)
        {
            _damageReportRepository = damageReportRepository;
            _tokenService = tokenService;
            _userService = userService;
            _imageService = imageService;
        }
        
        public async Task<Guid> CreateDamageReportAsync(DamageReport damageReport)
        {
            if (damageReport == null)
                return Guid.Empty;

            var userId = _tokenService.GetCurrentUserId();
            damageReport.Id = Guid.NewGuid();

            var result =  await _damageReportRepository.CreateDamageReportAsync(damageReport, userId);

            if (result)
                return damageReport.Id;
            else
                return Guid.Empty;
        }

        public async Task<List<DamageReportDTO>> GetDamageReportByCompanyVehicleIdAsync(Guid companyVehicleId)
        {
            var roleName = _tokenService.GetCurrentUserRoleName();
            var isAdmin = roleName.Equals(RoleName.Administrator);
            var damageReports = await _damageReportRepository.GetDamageReportByCompanyVehicleIdAsync(companyVehicleId, isAdmin);
            var damageReportDTOs = new List<DamageReportDTO>();
            if (damageReports.IsNullOrEmpty())
                return damageReportDTOs;

            foreach (var damageReport in damageReports)
            {
                var user = await _userService.GetUserByIdAsync(damageReport.UserId);
                var imageIds = await _imageService.GetImageIdsByDamageReportAsync(damageReport.Id);
                var images = new List<Image>();
                foreach (var imageId in imageIds)
                {
                    var image = await _imageService.GetImageByIdAsync(imageId);
                    images.Add(image!);
                }
                var damageReportDTO = new DamageReportDTO
                {
                    Id = damageReport.Id,
                    Title = damageReport.Title,
                    Description = damageReport.Description,
                    Driver = user!.FullName,
                    Images = images,
                    DateCreated = damageReport.DateCreated
                };
                damageReportDTOs.Add(damageReportDTO);
            }

            return damageReportDTOs;
        }

        public async Task<bool> DeleteDamageReportAsync(Guid damageReportId)
        {
            if (damageReportId == Guid.Empty)
                return false;

            return await _damageReportRepository.DeleteDamageReportAsync(damageReportId);
        }
    }
}
