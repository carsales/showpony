// Showpony, a lightweight variant testing library
// Copyright (C) Carsales.com Ltd
// See License.md in the project root for license information.

namespace Showpony
{
    public class ActionVariant
    {
        public string Name { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public object RouteValues { get; set; }

        public ActionVariant() { }

        public ActionVariant(string name, string action, string controller, object routeValues)
        {
            Name = name;
            Action = action;
            Controller = controller;
            RouteValues = routeValues;
        }
    }
}
