using MonoDragons.Core.Engine;
using System;
using System.Collections.Generic;
using MonoDragons.Core.PhysicsEngine;
using MegaBuy.UIStuff;
using MonoDragons.Core.UserInterface;
using Microsoft.Xna.Framework;

namespace MegaBuy.Calls
{
    public sealed class CallerScript : IVisualAutomaton, IDisposable
    {
        public Caller Caller;
        private List<ICallOption> _optionsCompleted = new List<ICallOption>();
        public List<OptionReaction> Options;
        public ChatBox ChatBox;
        private List<OptionReaction> _availableOptions;
        private ClickUI _clickUI;
        private bool _areOptionsDisplayed = false;
        private List<SimpleClickable> _clickables = new List<SimpleClickable>();
        private bool _disposed;

        public CallerScript(int milliPerPatience, string startingText, ClickUI clickUI, List<OptionReaction> options)
        {
            Caller = new Caller(milliPerPatience);
            Options = options;
            ChatBox = new ChatBox(startingText, 1500, DefaultFont.Font);
            _clickUI = clickUI;
            _availableOptions = new List<OptionReaction>();
            foreach (var option in options)
                if (option.IsAllowed(_optionsCompleted))
                    _availableOptions.Add(option);
            _clickables.Add(new SimpleClickable(0, new Rectangle(0, 0, 1920, 1080), () => this.CompletelyDisplayMessage()));
            _clickUI.Add(_clickables[0]);
        }

        public void CompletelyDisplayMessage()
        {
            ChatBox.CompletelyDisplayMessage();
            if (!_areOptionsDisplayed)
                DisplayOptions();
        }

        private void DisplayOptions()
        {
            _clickUI.Remove(_clickables[0]);
            _clickables.RemoveAt(0);
            for (var i = 0; i < _availableOptions.Count; i++)
            {
                var currentIndex = i;
                _clickables.Add(new SimpleClickable(0, new Rectangle(0, i * 100, 1920, 100), () => SelectOption(_availableOptions[currentIndex])));
                _clickUI.Add(_clickables[i]);
            }
            _areOptionsDisplayed = true;
        }

        public void SelectOption(OptionReaction action)
        {
            action.ChooseOption(_optionsCompleted);
            if (_disposed)
                return;
            ChatBox.ShowMessage(action.ResultMessage);
            _optionsCompleted.Add(action.Option);
            Options.Remove(action);
            while (_clickables.Count != 0)
            {
                _clickUI.Remove(_clickables[_clickables.Count -1]);
                _clickables.RemoveAt(_clickables.Count - 1);
            }
            _clickables.Add(new SimpleClickable(0, new Rectangle(0, 0, 1920, 1080), () => this.CompletelyDisplayMessage()));
            _clickUI.Add(_clickables[0]);
            _availableOptions = new List<OptionReaction>();
            foreach (var option in Options)
                if (option.IsAllowed(_optionsCompleted))
                    _availableOptions.Add(option);
            _areOptionsDisplayed = false;
        }

        public void Update(TimeSpan delta)
        {
            ChatBox.Update(delta);
            if (!_areOptionsDisplayed && ChatBox.IsMessageCompletelyDisplayed())
                DisplayOptions();
        }

        public void Draw(Transform2 parentTransform)
        {
            ChatBox.Draw(new Transform2(new Vector2(50, 725), new Size2(1500, 150)));
            if (_areOptionsDisplayed)
                for (var i = 0; i < _availableOptions.Count; i++)
                    UI.DrawText(_availableOptions[i].Option.message, new Vector2(0, i * 100), Color.Yellow);
        }

        public void Dispose()
        {
            while(_clickables.Count > 0)
            {
                _clickUI.Remove(_clickables[0]);
                _clickables.RemoveAt(0);
            }
            _disposed = true;
        }
    }
}
