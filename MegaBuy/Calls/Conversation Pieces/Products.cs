using System.Collections.Generic;
using System.Linq;

namespace MegaBuy.Calls.Conversation_Pieces
{
    public class Products
    {
        private static readonly Dictionary<string, ProductCategory[]> Prods = new Dictionary<string, ProductCategory[]>
        {
            { "Hopper", new [] { ProductCategory.Machine } },
            { "PAD XMB-9700", new [] { ProductCategory.Machine } },
            { "Electro-Coffee-ator", new [] { ProductCategory.Machine } },
            { "Ghandi Gun", new [] { ProductCategory.Weapon } },
            { "MagMaster 30X", new [] { ProductCategory.Weapon } },
            { "PQK 72", new [] { ProductCategory.Weapon } },
            { "Fillmore the Flying Puppy", new [] { ProductCategory.Entertainment } },
            { "Terminator XVII", new [] { ProductCategory.Entertainment } },
            { "The Clone Uprising", new [] { ProductCategory.Entertainment } },
            { "Matrix Analyzer", new [] { ProductCategory.Software } },
            { "Data Raven", new [] { ProductCategory.Software } },
            { "Ice Ice Firewall", new [] { ProductCategory.Software } },
            { "Mini-Micro-Mega-DataMaster", new [] { ProductCategory.Software } },
        };

        private static readonly Dictionary<ProductCategory, NounOperations[]> Operations = new Dictionary<ProductCategory, NounOperations[]>
        {
            { ProductCategory.Machine, new[] {NounOperations.CanRun, NounOperations.CanTurnOnOrOff, NounOperations.IsDefective } },
            { ProductCategory.Weapon, new[] {NounOperations.Jams, NounOperations.IsDefective} },
            { ProductCategory.Entertainment, new[] {NounOperations.TerribleExperience} },
            { ProductCategory.Software, new[] {NounOperations.IsDefective, NounOperations.IsBuggy, NounOperations.HasSpyware, NounOperations.RunsSlowly} }
        };

        public static List<NounOperations> GetOperations(string product)
        {
            return Prods[product]
                .SelectMany(x => Operations[x])
                .Distinct()
                .ToList();
        }

        public static string Random()
        {
            return Prods.Random().Key;
        }
    }
}
