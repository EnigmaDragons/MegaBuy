using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Engine;
using MonoDragons.Core.Graphics;
using MonoDragons.Core.PhysicsEngine;

namespace MonoDragons.Core.UserInterface.Graphs
{
    public static class GraphDefaults
    {
        public const int GraphPointRadius = 10;
        public const int GraphLineThickness = 3;
        public const int GraphBackgoundLineThickness = 1;

        public static Size2 GraphPointSize = new Size2(GraphPointRadius* 2, GraphPointRadius* 2);

        public static Texture2D GraphPoint()
        {
            return new CircleTexture(GraphPointRadius, Color.Red).Create();
        }

        public static Texture2D GraphLine()
        {
            var data = new[] { Color.Red };
            var result = new Texture2D(Hack.TheGame.GraphicsDevice, 1, GraphLineThickness, false, SurfaceFormat.Color);
            result.SetData(data, 0, result.Width * result.Height);
            return result;
        }

        public static Texture2D GraphBackgroundLine()
        {
            var data = new[] { Color.Gray };
            var result = new Texture2D(Hack.TheGame.GraphicsDevice, 1, GraphBackgoundLineThickness, false, SurfaceFormat.Color);
            result.SetData(data, 0, result.Width * result.Height);
            return result;
        }
    }
}
