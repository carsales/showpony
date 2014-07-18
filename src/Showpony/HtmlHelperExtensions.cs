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
        public static void RenderExperiment(this HtmlHelper htmlHelper, string experiment, IList<ActionVariant> variants)
        {
            var variantName = ShowponyContext.RunExperiment(experiment, variants.Select(x => new Variant(x.Name, x.Weighting)).ToList());

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
