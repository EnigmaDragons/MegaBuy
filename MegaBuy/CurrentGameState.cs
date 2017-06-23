using MegaBuy.Player;

namespace MegaBuy
{
    public static class CurrentGameState
    {
        private static string _charName = "nobody";
        private static CharacterSex _charSex = CharacterSex.Male;

        public static GameState State { get; private set; }

        public static GameState StartNewGame()
        {
            State?.Dispose();
            State = new GameState(_charName, _charSex);
            return State;
        }

        public static void SetupCharacter(string characterName, CharacterSex sex)
        {
            _charName = characterName;
            _charSex = sex;
        }
    }
}
