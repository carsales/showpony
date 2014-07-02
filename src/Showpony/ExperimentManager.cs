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

        internal static string GetSelectedVariant(HttpContextBase httpContext, string experiment, IEnumerable<string> variants)
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
            Cookies.SetExperimentVariant(httpContext.Response, experiment, variant);
        }

        internal static string GetRandomVariant(HttpContextBase httpContext, string experiment, IEnumerable<string> variants)
        {
            var variant = variants.OrderBy(x => R.Next()).First();
            SetSelectedVariant(httpContext, experiment, variant);
            ShowponyContext.OnExperimentStarted(new ExperimentStartedEventArgs()
            {
                Experiment = experiment,
                Variant = variant
            });
            return variant;
        }
    }
}
