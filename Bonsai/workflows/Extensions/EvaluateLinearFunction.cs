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
public class EvaluateLinearFunction
{
    public IObservable<Matrix<double>> Process(IObservable<Tuple<Matrix<double>, Vector<double>>> source)
    {
        return source.Select(input => {

            Matrix<double> A = input.Item1;
            Vector<double> x = input.Item2;

            // Evaluate the linear function a0 + a1*x

            if (A.RowCount != 2)
                throw new ArgumentException("A matrix dimensions are incorrect. Needs to be a m * 2 matrix");

            Vector<double> a0 = Vector<double>.Build.Dense(A.ColumnCount);
            A.Row(0, 0, A.ColumnCount, a0);

            Vector<double> a1 = Vector<double>.Build.Dense(A.ColumnCount);
            A.Row(1, 0, A.ColumnCount, a1);

            Matrix<double> ys = a1.OuterProduct(x) + a0.OuterProduct(Vector<double>.Build.Dense(x.Count, 1.0));

            return ys;
        });
    }
}
