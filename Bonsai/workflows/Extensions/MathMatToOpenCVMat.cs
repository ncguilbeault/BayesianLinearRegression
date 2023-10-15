using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reflection;
using MathNet.Numerics.LinearAlgebra;
using OpenCV.Net;

[Combinator]
[Description("")]
[WorkflowElementCategory(ElementCategory.Transform)]
public class MathMatToOpenCVMat
{
    private void CopyData(Matrix<double> input, ref Mat output, int rowCount, int columnCount) 
    {
        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < columnCount; j++)
            {
                output[i,j] = new Scalar(input[j,i]);
            }
        }
    }

    public IObservable<Mat> Process(IObservable<Matrix<double>> source)
    {
        return source.Select(input => {
            int rowCount = input.RowCount;
            int columnCount = input.ColumnCount;
            Mat output = new Mat(new Size(columnCount, rowCount), Depth.F64, 1);
            CopyData(input, ref output, rowCount, columnCount);
            return output;
        });
    }
}
