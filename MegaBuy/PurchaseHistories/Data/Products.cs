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
            Make("Suction Cup MicroCam", 668.39m, ProductCategory.Machine),
            Make("Synthsteel Fruit Press", 1253.84m, ProductCategory.Machine),
            Make("MaxBass Auds", 216.74m, ProductCategory.Machine),
            Make("GC310 840W Garment Steamer", 487.52m, ProductCategory.Machine),
            Make("Thermo-Detecto-Metric 3", 513.74m, ProductCategory.Machine),

            Make("Ghandi Gun", 2123.72m, ProductCategory.Weapon),
            Make("MagMaster 30X", 3045.61m, ProductCategory.Weapon),
            Make("PQK 72", 2898.14m, ProductCategory.Weapon),
            Make("Gerber Ghoststrike Blade Knife", 964.12m, ProductCategory.Weapon),
            Make("The Vindicator 900X", 1973.82m, ProductCategory.Weapon),

            Make("Fillmore the Flying Puppy", 63.84m, ProductCategory.Entertainment),
            Make("Terminator XVII", 74.44m, ProductCategory.Entertainment),
            Make("The Clone Uprising", 86.18m, ProductCategory.Entertainment),
            Make("The Wonder Society", 55.15m, ProductCategory.Entertainment),
            Make("Billionaire Alpha Romance", 61.97m, ProductCategory.Entertainment),
            Make("Hitonili Plays Nihongai", 162.67m, ProductCategory.Entertainment),
            Make("Colours", 74.12m, ProductCategory.Entertainment),
            Make("Psychokinesis and Mental States", 101.46m, ProductCategory.Entertainment),
            Make("Tropical Arcology Infrastructure", 131.79m, ProductCategory.Entertainment),

            Make("Matrix Analyzer", 882.37m, ProductCategory.Software),
            Make("Data Raven", 195.72m, ProductCategory.Software),
            Make("Ice Ice Firewall", 416.25m, ProductCategory.Software),
            Make("Mini-Micro-Mega-DataMaster", 373.68m, ProductCategory.Software),
            Make("Business Appointment Scheduler", 89.13m, ProductCategory.Software),

            Make("Sailboat Reflections Painting", 2615.19m, ProductCategory.Decor),
            Make("NetCorp Beverage Tumbler", 241.85m, ProductCategory.Decor),
            Make("Liberated Minds Hopper Sticker", 65.89m, ProductCategory.Decor),
            Make("Vintage Orange Sheer Window Scarf", 312.83m, ProductCategory.Decor),

            Make("Oil-Tanned Calfskin Leather Wristband", 263.17m, ProductCategory.Apparel),
            Make("Vibram #1276 NeoKicks", 787.42m, ProductCategory.Apparel),
            Make("Black Shoulder Bag", 313.37m, ProductCategory.Apparel),
            Make("Kissy Kissy Buckaroo Bodysuit", 1262.83m, ProductCategory.Apparel),
            Make("Kiplin Sabiens Sports Series Shirt", 215.38m, ProductCategory.Apparel),
            Make("Lucky Gemstone Necklace", 184.13m, ProductCategory.Apparel),
            Make("Lightup Snowflake Princess Sunglaesses", 70.14m, ProductCategory.Apparel),

            Make("Hillshire Cove Fruit Basket", 374.42m, ProductCategory.Food),
            Make("Chocolate Coconut Voodoo Bar", 22.17m, ProductCategory.Food),
            Make("5 lbs. Gummy Palm Trees", 53.14m, ProductCategory.Food),
            Make("Premium Grated Parmesan", 120.54m, ProductCategory.Food),
            Make("Honeydew-Tomato Marinade Seasoning", 28.03m, ProductCategory.Food),
            // @todo #1 Add 10 more products
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
            { ProductCategory.Software, new[] {Problem.IsDefective, Problem.IsBuggy, Problem.HasSpyware, Problem.RunsSlowly, Problem.Crashes} },
            { ProductCategory.Apparel, new[] {Problem.IsDefective, Problem.DoesNotFit, Problem.WrongStyle} },
            { ProductCategory.Decor, new[] {Problem.IsDefective, Problem.WrongStyle} },
            { ProductCategory.Food, new[] {Problem.IsSpoiled, Problem.DoesNotTasteGood} },
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

        public static Product RandomExcept(string productName)
        {
            return Prods.Where(x => x.Name != productName).ToList().Random();
        }
    }
}
