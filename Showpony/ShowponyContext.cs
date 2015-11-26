// Showpony, a lightweight variant testing library
// Copyright (C) Carsales.com Ltd
// See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Web;
using System.Linq;

namespace Showpony
{
    public static class ShowponyContext
    {
        public static string CookiePrefix { get; set; }
        public static bool EncryptCookie { get; set; }
        public static string CookieEncryptionPassword { get; set; }
        public static event EventHandler<ExperimentEndedEventArgs> ExperimentEnded;
        public static event EventHandler<ExperimentStartedEventArgs> ExperimentStarted;
        public static event EventHandler<ExperimentCheckpointEventArg> ExperimentCheckpoint;

        static ShowponyContext()
        {
            CookiePrefix = "Showpony";
            EncryptCookie = true;
            CookieEncryptionPassword = "Showpony";
        }

        public static string RunExperiment(string experiment, IList<Variant> variants)
        {
            if (String.IsNullOrEmpty(experiment))
            {
                throw new ArgumentNullException("experiment");
            }

            if (variants == null || variants.Count == 0)
            {
                throw new ArgumentException("variants cannot be null or empty");
            }

            if (variants.Any(o => o.Weighting <= 0))
            {
                throw new ArgumentException("variant weighting cannot be zero or less");
            }

            if (variants.Any(o => String.IsNullOrWhiteSpace(o.Name)))
            {
                throw new ArgumentException("variant must have a name");
            }

            var variant =
                ExperimentManager.GetSelectedVariant(new HttpContextWrapper(HttpContext.Current), experiment, variants) ??
                ExperimentManager.GetRandomVariant(new HttpContextWrapper(HttpContext.Current), experiment, variants);

            return variant;
        }

        public static void EndExperiment(string experiment)
        {
            var variant = Cookies.GetExperimentVariant(new HttpRequestWrapper(HttpContext.Current.Request), experiment);
            if (variant == null) return;

            Cookies.DeleteExperimentVariant(new HttpResponseWrapper(HttpContext.Current.Response), experiment);

            OnExperimentEnded(new ExperimentEndedEventArgs()
            {
                Experiment = experiment,
                Variant = variant
            });
        }

        public static void ExperimentCheckpointRecord(string experiment)
        {
            var variant = Cookies.GetExperimentVariant(new HttpRequestWrapper(HttpContext.Current.Request), experiment);
            if (variant == null) return;

            OnExperimentCheckpoint(new ExperimentCheckpointEventArg()
            {
                Experiment = experiment,
                Variant = variant
            });
        }

        public static string GetExperimentVariant(string experiment)
        {
            return Cookies.GetExperimentVariant(new HttpRequestWrapper(HttpContext.Current.Request), experiment);
        }

        public static void SetExperimentVariant(string experiment, string variant)
        {
            Cookies.SetExperimentVariant(
                new HttpRequestWrapper(HttpContext.Current.Request),
                new HttpResponseWrapper(HttpContext.Current.Response),
                experiment, variant);
        }

        internal static void OnExperimentStarted(ExperimentStartedEventArgs e)
        {
            var handler = ExperimentStarted;
            if (handler != null)
            {
                handler(null, e);
            }
        }

        internal static void OnExperimentEnded(ExperimentEndedEventArgs e)
        {
            var handler = ExperimentEnded;
            if (handler != null)
            {
                handler(null, e);
            }
        }

        internal static void OnExperimentCheckpoint(ExperimentCheckpointEventArg e)
        {
            var handler = ExperimentCheckpoint;
            if (handler != null)
            {
                handler(null, e);
            }
        }
    }
}
