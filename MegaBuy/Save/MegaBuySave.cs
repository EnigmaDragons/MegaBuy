using MonoDragons.Core.IO;

namespace MegaBuy.Save
{
    public sealed class MegaBuySave
    {
        private readonly AppDataJsonIo _io;

        public MegaBuySave()
        {
            _io = new AppDataJsonIo("MegaBuy");
        }

        public void Save(string saveName)
        {
            _io.Save(saveName, GameState.GetSaveData());
        }

        public void Load(string saveName)
        {
        }
    }
}
