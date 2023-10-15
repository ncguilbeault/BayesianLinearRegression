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
[WorkflowElementCategory(ElementCategory.Transform)]
public class Meshgrid
{
    public IObservable<Matrix<double>> Process(IObservable<Tuple<Vector<double>, Vector<double>>> source)
    {
        return source.Select(input => {
            Vector<double> x = input.Item1;
            Vector<double> y = input.Item2;

            Matrix<double> X = x.OuterProduct(Vector<double>.Build.Dense(y.Count, 1.0));
            Matrix<double> Y = y.OuterProduct(Vector<double>.Build.Dense(x.Count, 1.0)).Transpose();

            Matrix<double> meshgrid = X.Append(Y);
            return meshgrid;
        });
    }
}