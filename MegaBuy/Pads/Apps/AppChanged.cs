namespace MegaBuy.Pads.Apps
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
