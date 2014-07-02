# Showpony - a lightweight variant testing library - brought to you by Carsales.com Ltd

All your variant are belong to us.

## License

[Apache License, Version 2.0](LICENSE.md)

## Installation

`PM> Install-Package Showpony

## Usage

**Running an experiment through an MVC controller action

`	@Html.RenderExperiment("TheMatrix", new List<ActionVariant> {
`			new ActionVariant {} {Name = "TheBluePill", Action = "Blue", Controller = "Pills"},
`			new ActionVariant {} {Name = "TheRedPill", Action = "Red", Controller = "Pills"}
`		});

**Running an experiment the hard way**

`	var variant = ShowponyContext.RunExperiment("TheMatrix", new List<string>{ "TheBluePill", "TheRedPill" });
`	switch (variant) {
`		case "TheBluePill":
`			StayInTheBlissfulIgnoranceOfIllusion();
`			break;
`		case "TheRedPill":
`			EmbraceTheSometimesPainfulTruthOfReality();
`			break;
`	}

**Ending an experiment**

`	ShowponyContext.EndExperiment("TheMatrix");

**Recording experiment results**

`	ShowponyContext.ExperimentStarted += (sender, args) => SaveData("Started", args);
`	ShowponyContext.ExperimentEnded += (sender, args) => SaveData("Ended", args);
