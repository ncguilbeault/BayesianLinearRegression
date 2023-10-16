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
public class Posterior
{
    public IObservable<Tuple<Matrix<double>, Matrix<double>>> Process(IObservable<Tuple<Vector<double>, Vector<double>, double, Matrix<double>, Matrix<double>>> source)
    {
        return source.Select(input => {

            Vector<double> x = input.Item1;
            Vector<double> t = input.Item2;
            double beta = input.Item3;
            Matrix<double> covariancePrior = input.Item4;
            Matrix<double> meanPrior = input.Item5;

            Matrix<double> A = Matrix<double>.Build.Dense(x.Count, 1, 1).Transpose();
            A = A.Stack(x.ToRowMatrix());

            Matrix<double> covariance = (covariancePrior + beta * A.Multiply(A.Transpose())).Inverse();

            Matrix<double> mean = covariance.Multiply(
                covariancePrior.Multiply(meanPrior).Add(
                    A.Multiply(t.ToColumnMatrix()).Multiply(beta))
                );

            return Tuple.Create(mean, covariance);
        });
    }
}