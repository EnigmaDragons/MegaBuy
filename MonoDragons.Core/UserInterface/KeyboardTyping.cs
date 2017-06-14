using System;
using System.Linq;
using Microsoft.Xna.Framework.Input;
using MonoDragons.Core.Engine;

namespace MonoDragons.Core.UserInterface
{
    public sealed class KeyboardTyping : IAutomaton
    {
        public string Result { get; private set; } = "";

        Keys[] keys;
        bool[] IskeyUp;
        private bool _backspaceIsDown;

        public KeyboardTyping()
        {
            InitValidKeys();
        }

        private void InitValidKeys()
        {
            keys = new Keys[38];
            var tempkeys = Enum.GetValues(typeof(Keys)).Cast<Keys>().ToArray();
            var j = 0;
            for (var i = 0; i < tempkeys.Length; i++)
            {
                if (i == 1 || i == 11 || (i > 26 && i < 63)) //get the keys listed above as well as A-Z
                {
                    keys[j] = tempkeys[i]; //fill our key array
                    j++;
                }
            }
            IskeyUp = new bool[keys.Length];
            for (int i = 0; i < keys.Length; i++)
                IskeyUp[i] = true;
        }

        public void Update(TimeSpan delta)
        {
            var state = Keyboard.GetState();
            var i = 0;
            foreach (Keys key in keys)
            {
                if (state.IsKeyDown(key))
                {
                    if (IskeyUp[i])
                    {
                        if (key == Keys.Back && Result != "") Result = Result.Remove(Result.Length - 1);
                        if (key == Keys.Space) Result += " ";
                        if (i > 1 && i < 12)
                        {
                            if (state.IsKeyDown(Keys.RightShift) || state.IsKeyDown(Keys.LeftShift))
                                Result += key.ToString()[1];
                        }
                        if (i > 11 && i < 38)
                        {
                            if (state.IsKeyDown(Keys.RightShift) || state.IsKeyDown(Keys.LeftShift))
                                Result += key.ToString();
                            else Result += key.ToString().ToLower(); //return the lowercase char is shift is up.
                        }
                    }
                    IskeyUp[i] = false; //make sure we know the key is pressed
                }
                else if (state.IsKeyUp(key)) IskeyUp[i] = true;
                i++;
            }
            UpdateBackspace(state);
        }

        private void UpdateBackspace(KeyboardState state)
        {
            if (!_backspaceIsDown && state.IsKeyDown(Keys.Back))
            {
                _backspaceIsDown = true;
                Result = Result.Substring(0, Math.Max(0, Result.Length));
            }
            if (_backspaceIsDown && !state.IsKeyDown(Keys.Back))
                _backspaceIsDown = false;
        }
    }
}
