using MegaBuy.Calls;
using MegaBuy.Money.Rules;
using MegaBuy.Time;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;

namespace MegaBuy.Money
{
    public sealed class MegaBuyAccounting
    {
        private readonly IAccount _playerAccount;

        private IPerCallRate _currentRate;
        private DayPayment _dayPayment;

        public MegaBuyAccounting(IAccount playerAccount)
        {
            _playerAccount = playerAccount;
            _currentRate = new Day1PerCallRate();
            World.Subscribe(new EventSubscription<DayStarted>(DayStarted, this));
            World.Subscribe(new EventSubscription<DayEnded>(DayEnded, this));
            World.Subscribe(new EventSubscription<CallSucceeded>(CallSucceeded, this));
            World.Subscribe(new EventSubscription<TechnicalMistakeOccurred>(TechnicalMistakeOccurred, this));
        }

        private void CallSucceeded(CallSucceeded call)
        {
            _dayPayment.Add(new CallPayment(_currentRate, call.Rating));
        }

        private void DayStarted(DayStarted day)
        {
            _dayPayment = new DayPayment();
        }

        private void DayEnded(DayEnded day)
        {
            _playerAccount.Add(_dayPayment);
            _dayPayment = null;
        }

        private void TechnicalMistakeOccurred(TechnicalMistakeOccurred mistake)
        {
            _dayPayment.Remove(mistake.PayPenalty);
        }
    }
}
