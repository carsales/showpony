// Showpony, a lightweight variant testing library
// Copyright (C) Carsales.com Ltd
// See License.md in the project root for license information.

using System;
using System.Web.Mvc;

namespace Showpony
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class EndExperimentAttribute : ActionFilterAttribute
    {
        private readonly string _experiment;

        public EndExperimentAttribute(string experiment)
        {
            _experiment = experiment;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            ShowponyContext.EndExperiment(_experiment);
        }
    }
}
