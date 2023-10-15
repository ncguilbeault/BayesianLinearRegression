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
public class MapToJet
{
    public double MinVal { get; set; }

    public double MaxVal { get; set; }

    private struct RGB
    {
        public double R;
        public double G;
        public double B;

        public RGB(double r, double g, double b)
        {
            R = r;
            G = g;
            B = b;
        }
    }

    private static void SetR(double newR, ref RGB Rgb)
    {
        Rgb = new RGB(newR, Rgb.G, Rgb.B);
    }

    private static void SetG(double newG, ref RGB Rgb)
    {
        Rgb = new RGB(Rgb.R, newG, Rgb.B);
    }

    private static void SetB(double newB, ref RGB Rgb)
    {
        Rgb = new RGB(Rgb.R, Rgb.G, newB);
    }

    private static void MapValToRgb(double val, double minVal, double maxVal, ref RGB Rgb)
    {
        // Normalize value
        double normalizedVal = (val - minVal) / (maxVal - minVal);
        
        // Define color levels
        int aR = 0, aG = 0, aB = 128;
        int bR = 0, bG = 0, bB = 255;
        int cR = 0, cG = 255, cB = 255;
        int dR = 255, dG = 255, dB = 0;
        int eR = 255, eG = 0, eB = 0;

        // Apply colormap
        if (normalizedVal < 0.25)
        {
            SetR(MapVal(normalizedVal, 0.0, 0.25, aR, bR), ref Rgb);
            SetG(MapVal(normalizedVal, 0.0, 0.25, aG, bG), ref Rgb);
            SetB(MapVal(normalizedVal, 0.0, 0.25, aB, bB), ref Rgb);
        }
        else if (normalizedVal < 0.5)
        {
            SetR(MapVal(normalizedVal, 0.25, 0.5, bR, cR), ref Rgb);
            SetG(MapVal(normalizedVal, 0.25, 0.5, bG, cG), ref Rgb);
            SetB(MapVal(normalizedVal, 0.25, 0.5, bB, cB), ref Rgb);
        }
        else if (normalizedVal < 0.75)
        {
            SetR(MapVal(normalizedVal, 0.5, 0.75, cR, dR), ref Rgb);
            SetG(MapVal(normalizedVal, 0.5, 0.75, cG, dG), ref Rgb);
            SetB(MapVal(normalizedVal, 0.5, 0.75, cB, dB), ref Rgb);
        }
        else
        {
            SetR(MapVal(normalizedVal, 0.75, 1.0, dR, eR), ref Rgb);
            SetG(MapVal(normalizedVal, 0.75, 1.0, dG, eG), ref Rgb);
            SetB(MapVal(normalizedVal, 0.75, 1.0, dB, eB), ref Rgb);
        }
    }

    private static double MapVal(double val, double inMin, double inMax, double outMin, double outMax)
    {
        return (val - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }

    public IObservable<IplImage> Process(IObservable<IplImage> source)
    {
        return source.Select(input => {

            IplImage channel1 = new IplImage(input.Size, input.Depth, 1);
            IplImage channel2 = new IplImage(input.Size, input.Depth, 1);
            IplImage channel3 = new IplImage(input.Size, input.Depth, 1);

            for (int i = 0; i < input.Height; i++)
            {
                for (int j = 0; j < input.Width; j++)
                {
                    double val = input.GetReal(i, j);
                    RGB Rgb = new RGB(0, 0, 0);
                    MapValToRgb(val, MinVal, MaxVal, ref Rgb);
                    channel1.SetReal(i, j, Rgb.B);
                    channel2.SetReal(i, j, Rgb.G);
                    channel3.SetReal(i, j, Rgb.R);
                }
            }

            IplImage output = new IplImage(input.Size, input.Depth, 3);
            CV.Merge(channel1, channel2, channel3, null, output);

            return output;
        });
    }
}