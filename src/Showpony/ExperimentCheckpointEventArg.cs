// Showpony, a lightweight variant testing library
// Copyright (C) Carsales.com Ltd
// See License.md in the project root for license information.

using System;

namespace Showpony
{
    public class ExperimentCheckpointEventArg : EventArgs
    {
        public string Experiment { get; set; }
        public string Variant { get; set; }
    }
}
