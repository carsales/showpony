// Showpony, a lightweight variant testing library
// Copyright (C) Carsales.com Ltd
// See License.md in the project root for license information.

using System;
using System.Web.Mvc;

namespace Showpony
{
    /// <summary>
    /// Allows a test to have 1 or more checkpoints before the experiment is ended.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ExperimentCheckpointAttribute : ActionFilterAttribute
    {
        private readonly string _experiment;

        public ExperimentCheckpointAttribute(string experiment)
        {
            _experiment = experiment;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            ShowponyContext.ExperimentCheckpointRecord(_experiment);
        }
    }
}
