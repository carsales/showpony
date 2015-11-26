// Showpony, a lightweight variant testing library
// Copyright (C) Carsales.com Ltd
// See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Showpony
{
    internal static class ExperimentManager
    {
        static readonly Random R = new Random();

        internal static string GetSelectedVariant(HttpContextBase httpContext, string experiment, IEnumerable<Variant> variants)
        {
            var variant = Cookies.GetExperimentVariant(httpContext.Request, experiment);
            if (variant != null)
            {
                return variant;
            }

            return null;
        }

        private static void SetSelectedVariant(HttpContextBase httpContext, string experiment, string variant)
        {
            Cookies.SetExperimentVariant(httpContext.Request, httpContext.Response, experiment, variant);
        }

        internal static string GetRandomVariant(HttpContextBase httpContext, string experiment, IEnumerable<Variant> variants)
        {
            var variant = GetRandomVariant(variants);
            SetSelectedVariant(httpContext, experiment, variant.Name);
            ShowponyContext.OnExperimentStarted(new ExperimentStartedEventArgs()
            {
                Experiment = experiment,
                Variant = variant.Name
            });
            return variant.Name;
        }

        internal static Variant GetRandomVariant(IEnumerable<Variant> variants)
        {
            int randomNumber = R.Next(0, variants.Sum(o => o.Weighting));
            int lowerRange = 0;
            int topRange = 0;

            foreach (var variant in variants)
            {
                topRange += variant.Weighting;

                if (randomNumber >= lowerRange && randomNumber < topRange)
                {
                    return variant;
                }

                lowerRange = topRange;
            }

            return null;
        }
    }
}
