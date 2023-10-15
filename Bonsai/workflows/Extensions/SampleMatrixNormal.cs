using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using MathNet.Numerics.LinearAlgebra;
using distributions = MathNet.Numerics.Distributions;

[Combinator]
[Description("")]
[WorkflowElementCategory(ElementCategory.Transform)]
public class SampleMatrixNormal
{
    public Random Seed { get; set; }
    public int NumSamples { get; set; }

    public IObservable<Matrix<double>> Process(IObservable<distributions.MatrixNormal> source)
    {
        return source.Select(input => {

            input.RandomSource = Seed;
            Matrix<double> matrixNormalSamples = input.Sample();

            for (int i = 0; i < NumSamples - 1; i++)
            {
                matrixNormalSamples = matrixNormalSamples.Append(input.Sample());
            }

            return matrixNormalSamples;
        });
    }
}
