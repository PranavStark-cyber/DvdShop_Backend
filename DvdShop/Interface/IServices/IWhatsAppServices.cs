namespace DvdShop.Interface.IServices
{
    public interface IWhatsAppServices
    {
        Task SendWhatsAppNotification(string customerPhoneNumber, string message);
    }
}
