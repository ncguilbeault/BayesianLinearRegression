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
public class Density
{
    public IObservable<Matrix<double>> Process(IObservable<Tuple<distributions.MatrixNormal, Matrix<double>>> source)
    {
        return source.Select(input => {
            
            distributions.MatrixNormal matrixNormal = input.Item1;
            Matrix<double> meshGrid = input.Item2;

            int rowCount = meshGrid.RowCount;
            int columnCount = meshGrid.ColumnCount / 2;

            Matrix<double> pdf = Matrix<double>.Build.Dense(rowCount, columnCount);

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    double[,] pos = new double[,] {{meshGrid[i, j]}, {meshGrid[i, j + columnCount]}};
                    Matrix<double> matrixPos = Matrix<double>.Build.DenseOfArray(pos);
                    pdf[i, j] = matrixNormal.Density(matrixPos);
                }
            }
            return pdf;
        });
    }
}