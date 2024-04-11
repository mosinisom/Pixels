using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Threading;

namespace AvaloniaCurves;

public class TickControl : Control
{
    Eco eco;

    static TickControl()
    {
        AffectsRender<TickControl>(AngleProperty);
    }

    public TickControl()
    {
        eco = new Eco(100, 100, 200, 125, 0);

        var timer = new DispatcherTimer();
        timer.Interval = TimeSpan.FromSeconds(1 / 60.0);
        timer.Tick += (sender, e) => { Angle += Math.PI / 360; eco.SimulateStep(); };
        timer.Start();
    }

    public static readonly StyledProperty<double> AngleProperty =
        AvaloniaProperty.Register<TickControl, double>(nameof(Angle));        

    public double Angle
    {
        get => GetValue(AngleProperty);
        set => SetValue(AngleProperty, value);
    }

    public override void Render(DrawingContext ctx)
    {
        /*var lineLength = Math.Sqrt((100 * 100) + (100 * 100));

        var p1 = new Point(200, 200);
        var p2 = new Point(p1.X + Math.Cos(Angle)*lineLength, p1.Y + Math.Sin(Angle)*lineLength);

        var pen = new Pen(Brushes.Green, 20, lineCap: PenLineCap.Square);

        ctx.DrawLine(pen, p1, p2);*/

        double dx = 800.0 / eco.Width;
        double dy = 800.0 / eco.Height;

        //рисуем траву
        for (int x=0; x<eco.Width; x++)
            for (int y=0; y<eco.Width; y++)
                {
                    var value = eco.grass[x,y].Value;
                    byte r,g,b;
                    r=g=b=255;
                    if (value > 0)
                    {
                        r=b=0;
                        g = (byte)(200 - value/Eco.GRASS_LIMIT * 100);
                    }

                    var brush = new SolidColorBrush(Color.FromRgb(r,g,b), 1);

                    ctx.DrawRectangle(brush, null, new Rect(dx*x, dy*y, dx, dy));
                }

        //рисуем кроликов
        foreach(var bunny in eco.bunnies)
        {
            var brush = Brushes.Yellow;
            ctx.DrawEllipse(brush, null, new Rect(dx*bunny.X+1, dy*bunny.Y+1, dx-2, dy-2));
        }
    }
}
