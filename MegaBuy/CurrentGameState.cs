using MegaBuy.Player;

namespace MegaBuy
{
    public static class CurrentGameState
    {
        public static GameState State = new GameState("nobody", CharacterSex.Male);

        public static void StartNewGame(string characterName, CharacterSex sex)
        {
            State = new GameState(characterName, sex);
        }
    }
}
