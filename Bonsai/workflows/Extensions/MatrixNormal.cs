using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reflection;
using MathNet.Numerics.LinearAlgebra;
using distributions = MathNet.Numerics.Distributions;

[Combinator]
[Description("")]
[WorkflowElementCategory(ElementCategory.Transform)]
public class MatrixNormal
{
    public IObservable<distributions.MatrixNormal> Process(IObservable<Tuple<Matrix<double>, Matrix<double>>> source)
    {
        return source.Select(input => {

            Matrix<double> M = input.Item1;
            Matrix<double> V = input.Item2;

            // Validate the input
            if (M.RowCount != V.RowCount || M.RowCount != V.ColumnCount)
                throw new ArgumentException("M matrix and V matrix dimensions do not match.");

            Matrix<double> K = Matrix<double>.Build.Dense(M.ColumnCount, M.ColumnCount, 1);

            distributions.MatrixNormal matrixNormal = new distributions.MatrixNormal(M, V, K);
            return matrixNormal;
        });
    }
}
