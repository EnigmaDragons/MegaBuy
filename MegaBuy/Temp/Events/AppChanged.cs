using MegaBuy.Apps;

namespace MegaBuy.Temp.Events
{
    public class AppChanged
    {
        public App App { get; private set; }

        public AppChanged(App app)
        {
            App = app;
        }
    }
}
