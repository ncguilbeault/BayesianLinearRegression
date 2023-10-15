using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

[Combinator]
[Description("")]
[WorkflowElementCategory(ElementCategory.Source)]
public class Linspace
{
    public double Start { get; set; }

    public double End { get; set; }

    public int NumSteps { get; set; }

    public IObservable<Vector<double>> Process()
    {
        double step = (End - Start) / (NumSteps - 1);
        
        Vector<double> linspace = Vector<double>.Build.Dense(NumSteps, i => {
            return Start + i * step;
        });

        return Observable.Return(linspace);
    }
}