﻿using CarGo.Common;
using CarGo.Model;
using CarGo.Repository.Common;
using CarGo.Service.Common;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace CarGo.Service
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly EmailSettings _emailSettings;

        public NotificationService(INotificationRepository notificationRepository,
            IOptions<EmailSettings> emailSettings)
        {
            _notificationRepository = notificationRepository;
            _emailSettings = emailSettings.Value;
        }

        public async Task<bool> SendNotificationAsync(Notification notification)
        {
            var email = new MimeMessage();

            email.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));
            email.To.Add(MailboxAddress.Parse(notification.To));
            email.Subject = notification.Title;

            var bodyBuilder = new BodyBuilder { TextBody = notification.Text };
            email.Body = bodyBuilder.ToMessageBody();

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.SmtpPort, _emailSettings.UseSsl);

            await smtp.AuthenticateAsync(_emailSettings.SenderEmail, _emailSettings.Password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);

            notification.From = _emailSettings.SenderEmail;

            return await SaveNotificationAsync(notification);
        }

        public async Task<PagedResponse<Notification>> GetAllNotificationsAsync(Paging paging)
        {
            var notifications = await _notificationRepository.GetAllNotificationsAsync(paging);
            var result = new PagedResponse<Notification>
            {
                PageNumber = paging.PageNumber,
                PageSize = paging.Rpp,
                TotalRecords = await _notificationRepository.CountAsync(),
                Data = notifications
            };
            return result;
        }

        private async Task<bool> SaveNotificationAsync(Notification notification)
        {
            return await _notificationRepository.SaveNotificationAsync(notification);
        }
    }
}