using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Rendering.SceneGraph;
using Avalonia.Threading;

namespace AvaloniaCurves;

public class CurveControl : Control
{
    Curve curve = new Curve();

    public CurveControl()
    {
        curve.points.Add(new Point(100,150));
        curve.points.Add(new Point(400,180));
        curve.points.Add(new Point(700,250));
        curve.points.Add(new Point(600,350));
        curve.points.Add(new Point(500,450));
        curve.points.Add(new Point(500,500));
    }

    public override void Render(DrawingContext drawingContext)
    {
        var pen = new Pen(Brushes.Green, 3, lineCap: PenLineCap.Square);

        const double step = 0.1;
        for(double t = 0; t<curve.MaxT-step; t+=step)
            drawingContext.DrawLine
            (
                pen,
                curve.GetPoint(t),
                curve.GetPoint(t+step)
            );
    }
}
