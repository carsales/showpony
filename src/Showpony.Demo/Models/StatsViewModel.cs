using Showpony.Demo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Showpony.Demo.Models
{
    public class ExperimentResultsViewModel
    {
        public string Experiment { get; private set; }

        public IEnumerable<VariantResultViewModel> VariantResults { get; private set; }

        public ExperimentResultsViewModel(string experiment, IEnumerable<ShowponyStats> stats)
        {
            Experiment = experiment;
            VariantResults = stats.Select(o => new VariantResultViewModel(o));
        }
        
        public class VariantResultViewModel
        {
            public string Name { get; private set; }

            public int Started { get; private set; }

            public int Ended { get; private set; }

            public VariantResultViewModel(ShowponyStats stats)
            {
                Name = stats.Variant;
                Started = stats.Started;
                Ended = stats.Ended;
            }
        }
    }

}