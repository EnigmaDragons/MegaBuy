using System;
using System.Collections.Generic;
using System.Linq;
using MegaBuy.Calls.Conversation_Pieces;
using MonoDragons.Core.Common;

namespace MegaBuy.PurchaseHistories.Data
{
    public static class Products
    {
        private static readonly List<Product> Prods = new List<Product>
        {
            Make("Hopper", 275693.45m, ProductCategory.Machine),
            Make("PAD XMB-9700", 1654.14m, ProductCategory.Machine),
            Make("Electro-Coffee-ator", 151.15m, ProductCategory.Machine),
            Make("Ghandi Gun", 2123.72m, ProductCategory.Weapon),
            Make("MagMaster 30X", 3045.61m, ProductCategory.Weapon),
            Make("PQK 72", 2898.14m, ProductCategory.Weapon),
            Make("Fillmore the Flying Puppy", 63.84m, ProductCategory.Entertainment),
            Make("Terminator XVII", 74.44m, ProductCategory.Entertainment),
            Make("The Clone Uprising", 86.18m, ProductCategory.Entertainment),
            Make("Matrix Analyzer", 882.37m, ProductCategory.Software),
            Make("Data Raven", 195.72m, ProductCategory.Software),
            Make("Ice Ice Firewall", 416.25m, ProductCategory.Software),
            Make("Mini-Micro-Mega-DataMaster", 373.68m, ProductCategory.Software),
        };

        private static Product Make(string name, decimal price, ProductCategory category)
        {
            return new Product(Guid.NewGuid().ToString(), name, price, category);
        }

        private static readonly Dictionary<ProductCategory, Problem[]> Operations = new Dictionary<ProductCategory, Problem[]>
        {
            { ProductCategory.Machine, new[] {Problem.DoesntRun, Problem.DoesntTurnOn, Problem.IsDefective } },
            { ProductCategory.Weapon, new[] {Problem.Jams, Problem.IsDefective} },
            { ProductCategory.Entertainment, new[] {Problem.TerribleExperience} },
            { ProductCategory.Software, new[] {Problem.IsDefective, Problem.IsBuggy, Problem.HasSpyware, Problem.RunsSlowly, Problem.Crashes} }
        };

        public static List<Problem> GetProblems(string product)
        {
            return Prods.Where(x => x.Name.Equals(product))
                .SelectMany(x => Operations[x.Category])
                .Distinct()
                .ToList();
        }

        public static Product Random => Prods.Random();

        public static Problem GetProblemFor(string product)
        {
            return GetProblems(product).Random();
        }
    }
}
