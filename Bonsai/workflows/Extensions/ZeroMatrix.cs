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
public class ZeroMatrix
{
    public int Rows { get; set; }
    public int Columns {get;set;}
    public IObservable<Matrix<double>> Process()
    {
        return Observable.Return(
            Matrix<double>.Build.Dense(Rows, Columns)
        );
    }
}
