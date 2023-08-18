using BlazorApp1.Shared.Models;

namespace BlazorApp1.Services
{
    public class NotificationService
    {
        public event Action<NotificationOption> ShowNotificationTrigger;
        public void Show(NotificationOption options)
        {
            ShowNotificationTrigger.Invoke(options);
        }

        public void ShowSuccess()
        {
           ShowSuccess("عملیات با موفقیت انجام شد");
        }

        public void ShowSuccess(string content)
        {
            ShowNotificationTrigger.Invoke(new NotificationOption
            {
                Title = "موفق",
                Content = content,
                Type = NotificationType.Success
            });
        }


        public void ShowError()
        {
            ShowError("عملیات با خطا مواجه شد");
        }

        public void ShowError(string content)
        {
            ShowNotificationTrigger.Invoke(new NotificationOption
            {
                Title = "خطا",
                Content = content,
                Type = NotificationType.Error
            });
        }
    }
}
