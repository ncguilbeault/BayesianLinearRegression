using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using distributions = MathNet.Numerics.Distributions;

[Combinator]
[Description("")]
[WorkflowElementCategory(ElementCategory.Transform)]
public class LikelihoodGrid
{

    public double Beta { get; set; }

    public IObservable<Matrix<double>> Process(IObservable<Tuple<Vector<double>, Vector<double>, Matrix<double>, double, double>> source)
    {

        return source.Select(input => {
            Vector<double> w0 = input.Item1;
            Vector<double> w1 = input.Item2;
            Matrix<double> meshGrid = input.Item3;
            double x = input.Item4;
            double t = input.Item5;

            Matrix<double> likelihoodGrid = Matrix<double>.Build.Dense(w0.Count, w1.Count, (i, j) => { 
                return CalculateLikelihood(i, j, w1.Count, meshGrid, x, t); 
                });

            return likelihoodGrid;
            
        });
    }

    private double CalculateLikelihood (int i, int j, int count, Matrix<double> meshGrid, double x, double t)
    {
        Vector<double> A = Vector<double>.Build.DenseOfArray(
            new double[] { 1, x }
            );
        Vector<double> W = Vector<double>.Build.DenseOfArray(
                new double[] { meshGrid[i,j], meshGrid[i,j+count] }
            );

        double prediction = A.DotProduct(W);
        double error = t - prediction;
        double likelihood = distributions.Normal.PDF(error, 1 / Math.Sqrt(Beta), 0);

        return likelihood;
    }
}