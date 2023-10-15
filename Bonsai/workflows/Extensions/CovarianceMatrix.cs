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
public class CovarianceMatrix
{
    public double Alpha { get; set; }

    public IObservable<Matrix<double>> Process()
    {
        Matrix<double> eye = Matrix<double>.Build.DenseIdentity(2);

        Matrix<double> covariance = (Alpha * eye).Inverse();

        return Observable.Return(
            covariance
        );
    }
}