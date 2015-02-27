# Showpony

**A lightweight variant testing library - brought to you by [Carsales.com Ltd](http://www.carsales.com.au)**

![Showpony!!!](showpony.png)

## License

[Apache License, Version 2.0](LICENSE.md)

## Installation

```
PM> Install-Package Showpony
```

## Requirements

ASP.NET MVC 5+

## Usage

**Running an experiment through an MVC controller action**

```csharp
@Html.RenderExperiment("TheMatrix", new List<ActionVariant> {
	new ActionVariant("TheBluePill", 50, "Blue", "Pills"),
	new ActionVariant("TheRedPill", 50, "Red", "Pills")
});
```

**Running an experiment the hard way**

```csharp
var variant = ShowponyContext.RunExperiment(
	"TheMatrix", new List<Variant>{ 
		new Variant("TheBluePill", 50),
		new Variant("TheRedPill", 50)
	});
	
switch (variant) {
	case "TheBluePill":
		StayInTheBlissfulIgnoranceOfIllusion();
		break;
	case "TheRedPill":
		EmbraceTheSometimesPainfulTruthOfReality();
		break;
}
```

**Ending an experiment**

```csharp
ShowponyContext.EndExperiment("TheMatrix");
```

**Recording experiment results**

```csharp
ShowponyContext.ExperimentStarted += (sender, args) => SaveData("Started", args);
ShowponyContext.ExperimentEnded += (sender, args) => SaveData("Ended", args);
```

## How it works

**TL;DR**

It uses cookies!

**The long version**

When Showpony is asked to execute an experiment it looks at the cookies on the incoming request to see if the user has already been assigned a variant. If yes then it returns that variant. If no then it chooses a variant at random, sets it in the response cookies, fires off the "ExperimentStarted" event and returns that variant.

When Showpony is asked to end an experiment it again looks at the cookies on the incoming request to see if the user is participating in the experiment. If yes then it fires off the "ExperimentEnded" event and removes the cookie from the user's browser.
