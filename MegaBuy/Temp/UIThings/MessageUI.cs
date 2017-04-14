using System.Text;
using MegaBuy.CustomUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Engine;
using MonoDragons.Core.Memory;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MegaBuy.Temp.UIThings
{
    public class MessageUI : IVisual
    {
        private readonly Label _label;
        public int Height { get; private set; }

        public MessageUI(string message, int maxWidth, bool isPlayer)
        {
            var font = Resources.Load<SpriteFont>("Fonts/arial");
            var newMessage = WrapText(font, message, maxWidth - 10);
            var size = font.MeasureString(newMessage);
            var position = isPlayer ? new Vector2(maxWidth - size.X - 10, 0) : new Vector2(0, 0);
            Height = (int)size.Y + 10;
            _label = new Label
            {
                BackgroundColor = isPlayer ? Colors.Secondary : Colors.Primary,
                TextColor = Color.Black,
                Font = "Fonts/arial",
                Text = newMessage,
                Transform = new Transform2(position, new Size2((int)size.X + 10, (int)size.Y + 10)),
            };
        }

        public void Draw(Transform2 parentTransform)
        {
            _label.Draw(parentTransform);
        }

        private string WrapText(SpriteFont font, string text, int maxWidth)
        {
            var words = text.Split(' ');
            var sb = new StringBuilder();
            var lineWidth = 0f;
            var spaceWidth = font.MeasureString(" ").X;
            foreach (var word in words)
            {
                var size = font.MeasureString(word);
                if (lineWidth + size.X < maxWidth)
                {
                    sb.Append(word + " ");
                    lineWidth += size.X + spaceWidth;
                }
                else
                {
                    sb.Append("\n" + word + " ");
                    lineWidth = size.X + spaceWidth;
                }
            }
            return sb.ToString();
        }
    }
}
