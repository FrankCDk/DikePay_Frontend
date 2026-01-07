using CommunityToolkit.Mvvm.Messaging.Messages;

namespace DikePay.Services.Implementations
{
    public class QrLoginMessage : ValueChangedMessage<string>
    {
        public QrLoginMessage(string value) : base(value)
        {
        }
    }
}
