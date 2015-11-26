using System;
using System.IO;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Showpony.Tests
{
    [TestClass]
    public class ExperimentManagerTests
    {
        static readonly Random R = new Random();

        [TestMethod]
        public void GetRandomVariant_VariantIsNotNull()
        {
            for (int i = 0; i < 10000000; i++)
            {
                IList<Variant> variants = new List<Variant>()
                {
                    new Variant("first", R.Next(0, 100000)),
                    new Variant("second", R.Next(0, 100000)),
                    new Variant("third", R.Next(0, 100000)),
                    new Variant("fourth", R.Next(0, 100000))
                };

                Variant variant = ExperimentManager.GetRandomVariant(variants);

                Assert.IsNotNull(variant);
            }
        }

        [TestMethod]
        public void GetRandomVariant_EqualVariants()
        {
            Dictionary<string, int> counts = new Dictionary<string, int>();

            for (int i = 0; i < 10000000; i++)
            {
                IList<Variant> variants = new List<Variant>()
                {
                    new Variant("first", 0),
                    new Variant("second", 1),
                    new Variant("third", 1),
                    new Variant("fourth", 1)
                };

                Variant variant = ExperimentManager.GetRandomVariant(variants);

                Assert.IsNotNull(variant);

                if(!counts.ContainsKey(variant.Name))
                {
                    counts.Add(variant.Name, 0);
                }

                counts[variant.Name]++;
            }

            foreach(var count in counts)
            {
                Console.WriteLine("{0}: {1}", count.Key, count.Value);
            }
        }

        [TestMethod]
        public void GetRandomVariant_Weighting0()
        {
            for (int i = 0; i < 1000; i++)
            {
                IList<Variant> variants = new List<Variant>()
                {
                    new Variant("first", 0),
                    new Variant("second", 1)
                };

                Variant variant = ExperimentManager.GetRandomVariant(variants);

                Assert.AreEqual(variant.Name, "second");
            }
        }

        [TestMethod]
        public void SetSelectedVariant()
        {
            HttpContext.Current = new HttpContext(
                new HttpRequest("", "http://tempuri.org", ""),
                new HttpResponse(new StringWriter())
                );

            ShowponyContext.SetExperimentVariant("Test1", "Variant1");
        }

        [TestMethod]
        public void GetSelectedVariant()
        {
            ShowponyContext.EncryptCookie = false;

            var req = new HttpRequest("", "http://tempuri.org", "");
            req.Cookies.Add(new HttpCookie("ShowponyTest1")
            {
                Value = "Variant1",
                Expires = DateTime.Now.AddYears(1)
            });

            HttpContext.Current = new HttpContext(
                req,
                new HttpResponse(new StringWriter())
                );

            Assert.AreEqual("Variant1", ShowponyContext.GetExperimentVariant("Test1"));
        }
    }
}
