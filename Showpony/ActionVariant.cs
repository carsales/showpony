// Showpony, a lightweight variant testing library
// Copyright (C) Carsales.com Ltd
// See License.md in the project root for license information.

namespace Showpony
{
    public class ActionVariant
    {
        public string Name { get; private set; }

        public int Weighting { get; private set; }

        public string Action { get; private set; }

        public string Controller { get; private set; }

        public object RouteValues { get; private set; }

        public ActionVariant(string name, int weighting, string action, string controller, object routeValues = null)
        {
            Name = name;
            Weighting = weighting;
            Action = action;
            Controller = controller;
            RouteValues = routeValues;
        }
    }
}
