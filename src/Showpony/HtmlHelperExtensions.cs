// Showpony, a lightweight variant testing library
// Copyright (C) Carsales.com Ltd
// See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace Showpony
{
    public static class HtmlHelperExtensions
    {
        public static void RenderExperiment(
            this HtmlHelper htmlHelper, string experiment, IList<ActionVariant> variants)
        {
            if (String.IsNullOrEmpty(experiment))
            {
                throw new ArgumentNullException("experiment");
            }

            if (variants == null ||
                variants.Count == 0)
            {
                throw new ArgumentException("variants cannot be null or empty");
            }

            var variantName = ShowponyContext.RunExperiment(experiment, variants.Select(x => x.Name).ToList());

            var variant = variants.First(x => x.Name == variantName);

            // add showponyExperiment and showponyVariant to all routevalues collections
            var expandedRouteValues = variant.RouteValues == null
                ? new RouteValueDictionary()
                : new RouteValueDictionary(variant.RouteValues);
            expandedRouteValues.Add("showponyExperiment", experiment);
            expandedRouteValues.Add("showponyVariant", variantName);

            htmlHelper.RenderAction(variant.Action, variant.Controller, expandedRouteValues);
        }
    }
}
