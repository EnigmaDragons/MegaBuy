using System.Collections.Generic;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;

namespace MegaBuy.Notifications
{
    public sealed class Mailbox
    {
        private readonly HashSet<string> _blocked = new HashSet<string>();

        public List<PlayerNotification> Inbox { get; }
        public int NumMessagesReceived { get; private set; }

        public Mailbox()
            : this(new List<string>(), new List<PlayerNotification>()) { }

        public Mailbox(List<string> blockedSenders, List<PlayerNotification> inbox)
        {
            blockedSenders.ForEach(x => _blocked.Add(x));
            Inbox = inbox;
            World.Subscribe(EventSubscription.Create<PlayerNotification>(Notified, this));
        }

        public void Block(PlayerNotification obj)
        {
            Block(obj.Sender);
        }

        public void Block(string sender)
        {
            _blocked.Add(sender);
        }

        public void Dismiss(PlayerNotification notification)
        {
            Inbox.Remove(notification);
        }

        private void Notified(PlayerNotification obj)
        {
            if (_blocked.Contains(obj.Sender))
                return;

            Inbox.Add(obj);
            NumMessagesReceived++;
        }
    }
}
