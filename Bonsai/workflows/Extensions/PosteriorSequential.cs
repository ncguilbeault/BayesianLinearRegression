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
public class PosteriorSequential
{
    public IObservable<Tuple<Matrix<double>, Matrix<double>>> Process(IObservable<Tuple<double, double, double, Matrix<double>, Matrix<double>>> source)
    {
        return source.Select(input => {

            double x = input.Item1;
            double t = input.Item2;
            double beta = input.Item3;
            Matrix<double> covariancePrior = input.Item4;
            Matrix<double> meanPrior = input.Item5;

            Matrix<double> A = Matrix<double>.Build.DenseOfArray(new double[,] {{1}, {x}});
            Matrix<double> S_phi = covariancePrior.Multiply(A);
            Matrix<double> factor = 1.0 / (1.0 + beta * A.Transpose().Multiply(S_phi));
            Matrix<double> mean = meanPrior + (factor * beta * (t - A.Transpose().Multiply(meanPrior)).Multiply(S_phi.Transpose())).Transpose();
            Matrix<double> covariance = (covariancePrior - beta * factor[0,0] * (S_phi * S_phi.Transpose()));

            return Tuple.Create(mean, covariance);
        });
    }
}