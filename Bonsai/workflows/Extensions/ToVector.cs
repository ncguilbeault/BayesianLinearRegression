using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using MathNet.Numerics.LinearAlgebra;

[Combinator]
[Description("")]
[WorkflowElementCategory(ElementCategory.Transform)]
public class ToVector
{
    private List<double> buffer = new List<double>();
    public int? Count {get; set;}
    public IObservable<Vector<double>> Process(IObservable<double> source)
    {
        buffer = new List<double>();
        return source.Select(input => {
            if (Count.HasValue) {
                if (buffer.Count == Count.Value)
                {
                    buffer.RemoveAt(0);
                }
            }
            buffer.Add(input);
            return Vector<double>.Build.DenseOfArray(buffer.ToArray());
        });
    }
}
