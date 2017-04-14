using System.Collections.Generic;
using System.Linq;
using MonoDragons.Core.Common;

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

        private static readonly Dictionary<ProductCategory, Problem[]> Operations = new Dictionary<ProductCategory, Problem[]>
        {
            { ProductCategory.Machine, new[] {Problem.DoesntRun, Problem.DoesntTurnOn, Problem.IsDefective } },
            { ProductCategory.Weapon, new[] {Problem.Jams, Problem.IsDefective} },
            { ProductCategory.Entertainment, new[] {Problem.TerribleExperience} },
            { ProductCategory.Software, new[] {Problem.IsDefective, Problem.IsBuggy, Problem.HasSpyware, Problem.RunsSlowly, Problem.Crashes} }
        };

        public static List<Problem> GetProblems(string product)
        {
            return Prods[product]
                .SelectMany(x => Operations[x])
                .Distinct()
                .ToList();
        }

        public static string Random => Prods.Random().Key;

        public static Problem GetProblemFor(string product)
        {
            return GetProblems(product).Random();
        }
    }
}
