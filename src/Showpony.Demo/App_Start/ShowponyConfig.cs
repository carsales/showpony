using System;
using Showpony.Demo.Repositories;

namespace Showpony.Demo
{
    public class ShowponyConfig
    {
        public static void Register()
        {
            ShowponyContext.ExperimentStarted += ShowponyContext_ExperimentStarted;
            ShowponyContext.ExperimentEnded += ShowponyContext_ExperimentEnded;
        }

        static void ShowponyContext_ExperimentStarted(object sender, ExperimentStartedEventArgs e)
        {
            var repo = new ShowponyRepository();
            repo.RecordShowponyStarted(e.Experiment, e.Variant);
        }

        static void ShowponyContext_ExperimentEnded(object sender, ExperimentEndedEventArgs e)
        {
            var repo = new ShowponyRepository();
            repo.RecordShowponyEnded(e.Experiment, e.Variant);
        }
    }
}