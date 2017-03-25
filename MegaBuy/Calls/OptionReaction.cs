using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaBuy.Calls
{
    public sealed class OptionReaction
    {
        public ICallOption Option;
        private Func<List<ICallOption>, bool> _isCorrect;
        private Func<List<ICallOption>, bool> _isAllowed;
        private string CorrectMessage;
        private string IncorrectMessage;
        public string ResultMessage { get; private set; }

        public OptionReaction(ICallOption option, Func<List<ICallOption>, bool> isAllowed, Func<List<ICallOption>, bool> isCorrect,
            string correctMessage = "", string incorrectMessage = "")
        {
            Option = option;
            _isCorrect = isCorrect;
            _isAllowed = isAllowed;
            CorrectMessage = correctMessage;
            IncorrectMessage = incorrectMessage;
        }

        public bool IsAllowed(List<ICallOption> optionsCompleted)
        {
            return _isAllowed(optionsCompleted);
        }

        public void ChooseOption(List<ICallOption> optionsCompleted)
        {
            if (!_isAllowed(optionsCompleted))
                throw new Exception("Not Allowed");
            Option.Go(_isCorrect(optionsCompleted));
            ResultMessage = _isCorrect(optionsCompleted) ? CorrectMessage : IncorrectMessage;
        }
    }
}
