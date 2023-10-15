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
using Bonsai.Vision.Design;
using System.Drawing;
using System.Windows.Forms;
using ZedGraph;
using cv = OpenCV.Net;

[Combinator]
[Description("")]
[WorkflowElementCategory(ElementCategory.Transform)]
public class OverlayPointOnImage
{
    public IObservable<cv.IplImage> Process(IObservable<Tuple<cv.IplImage, double, double>> source)
    {
        return source.Select(input => {

            cv.IplImage image = input.Item1;
            double a0 = input.Item2;
            double a1 = input.Item3;

            int imageWidth = image.Width;
            int imageHeight = image.Height;

            cv.Point a0Pt1 = new cv.Point((int)Math.Round((a0 + 1) * imageWidth/2), (int)Math.Round((a1 - 0.05 + 1) * imageHeight/2));
            cv.Point a0Pt2 = new cv.Point((int)Math.Round((a0 + 1) * imageWidth/2), (int)Math.Round((a1 + 0.05 + 1) * imageHeight/2));
            cv.CV.Line(image, a0Pt1, a0Pt2, new cv.Scalar(255, 255, 255), 1, cv.LineFlags.AntiAliased);

            cv.Point a1Pt1 = new cv.Point((int)Math.Round((a0 + 1 - 0.05) * imageWidth/2), (int)Math.Round((a1 + 1) * imageHeight/2));
            cv.Point a1Pt2 = new cv.Point((int)Math.Round((a0 + 1 + 0.05) * imageWidth/2), (int)Math.Round((a1 + 1) * imageHeight/2));
            cv.CV.Line(image, a1Pt1, a1Pt2, new cv.Scalar(255, 255, 255), 1, cv.LineFlags.AntiAliased);  
                
            return image;
        });
    }
}