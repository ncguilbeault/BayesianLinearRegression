using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive;
using MathNet.Numerics.LinearAlgebra;
using Bonsai.Design;
using Bonsai.Design.Visualizers;
using System.Drawing;
using System.Windows.Forms;
using ZedGraph;

[Combinator]
[Description("")]
[WorkflowElementCategory(ElementCategory.Sink)]
[TypeVisualizer(typeof(PlotRandomSamplesVisualizer))]

public class PlotRandomSamples
{
    public IObservable<Tuple<Vector<double>, Matrix<double>, Vector<double>, Vector<double>>> Process(IObservable<Tuple<Vector<double>, Matrix<double>, Vector<double>, Vector<double>>> source)
    {
        return source.Select(input => {
            Vector<double> X = input.Item1;
            Matrix<double> Ys = input.Item2;
            Vector<double> x = input.Item3;
            Vector<double> t = input.Item4;
            if (X.Count == 0) {
                new ArgumentException("X Vector must be of length greater than 0.");
            }
            if (Ys.RowCount == 0 || Ys.ColumnCount == 0) {
                new ArgumentException("Ys Matrix must have both rows and columns of length greater than 0.");
            }
            if (X.Count != Ys.ColumnCount) {
                new ArgumentException("Length of X Vector and column count of Ys Matrix must be equal.");
            }            
            return input;
        });
    }

    public IObservable<Tuple<Vector<double>, Matrix<double>, Vector<double>, Vector<double>>> Process(IObservable<Tuple<Vector<double>, Matrix<double>>> source)
    {
        return source.Select(input => {
            Vector<double> X = input.Item1;
            Matrix<double> Ys = input.Item2;
            Vector<double> x = Vector<double>.Build.Dense(0);
            Vector<double> t = Vector<double>.Build.Dense(0);
            if (X.Count == 0) {
                new ArgumentException("X Vector must be of length greater than 0.");
            }
            if (Ys.RowCount == 0 || Ys.ColumnCount == 0) {
                new ArgumentException("Ys Matrix must have both rows and columns of length greater than 0.");
            }
            if (X.Count != Ys.ColumnCount) {
                new ArgumentException("Length of X Vector and column count of Ys Matrix must be equal.");
            }            
            return Tuple.Create(X, Ys, X, t);
        });
    }
}


public class PlotRandomSamplesVisualizer : DialogTypeVisualizer
{
    GraphControl graph;
    PointPairList[] xys;
    LineItem[] curves;
    bool initialized = false;

    public override void Load(IServiceProvider provider)
    {
        graph = new GraphControl();
        graph.Dock = DockStyle.Fill;

        graph.GraphPane.YAxis.Scale.MaxAuto = false;
        graph.GraphPane.YAxis.Scale.MinAuto = false;

        graph.GraphPane.XAxis.Scale.Min = -1;
        graph.GraphPane.XAxis.Scale.Max = 1;

        graph.GraphPane.YAxis.Scale.Min = -1;
        graph.GraphPane.YAxis.Scale.Max = 1;

        var visualizerService = (IDialogTypeVisualizerService)provider.GetService(typeof(IDialogTypeVisualizerService));
        if (visualizerService != null)
        {
            visualizerService.AddControl(graph);
        }
    }

    public override void Show(object value)
    {
        Tuple<Vector<double>, Matrix<double>, Vector<double>, Vector<double>> input = (Tuple<Vector<double>, Matrix<double>, Vector<double>, Vector<double>>)value;
        double[] X = input.Item1.ToArray();
        double[][] Ys = input.Item2.ToRowArrays();
        double[] x = input.Item3.ToArray();
        double[] t = input.Item4.ToArray();

        for (int i = 0; i < Ys.Length; i++)
        {
            LineItem curve = new LineItem("", X, Ys[i], Color.Red, SymbolType.None);
            curve.Line.IsAntiAlias = true;
            curve.Line.IsOptimizedDraw = true;
            curve.Label.IsVisible = false;
            curve.Line.Width = 2;
            // curve.Symbol.Fill.Type = FillType.None;
            // curve.Symbol.IsAntiAlias = true;
            curve.Symbol.IsVisible = false;

            LineItem points = new LineItem("", x, t, Color.Blue, SymbolType.Circle);
            points.Label.IsVisible = false;
            points.Line.IsVisible = false;
            points.Symbol.Fill.Type = FillType.None;
            points.Symbol.Size = 12;
            points.Symbol.Border.Width = 2;
            points.Symbol.IsAntiAlias = true;

            graph.GraphPane.XAxis.Title.Text = "X";
            graph.GraphPane.XAxis.Title.IsVisible = true;
            graph.GraphPane.YAxis.Title.Text = "Y";
            graph.GraphPane.YAxis.Title.IsVisible = true;

            graph.GraphPane.CurveList.Add(points);
            graph.GraphPane.CurveList.Add(curve);
        }
        graph.Invalidate();
    }

    public override void Unload()
    {
        graph.Dispose();
    }
}