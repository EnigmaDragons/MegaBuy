using System;
using MegaBuy.Time;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;

namespace MegaBuy.Save
{
    public sealed class AutoSave
    {
        private readonly MegaBuySave _save;

        public AutoSave()
            : this(new MegaBuySave()) { }

        public AutoSave(MegaBuySave save)
        {
            _save = save;
            World.Subscribe(EventSubscription.Create<HourChanged>(SaveGame, this));
        }

        public void SaveGame(HourChanged hourChanged)
        {
            if (hourChanged.Hour.Equals(20))
                _save.Save($"autosave-{DateTime.Now:yyMMddHHmmss}");
        }
    }
}
