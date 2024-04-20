using System;
using System.Globalization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Threading;

namespace AvaloniaCurves;

public class TickControl : Control
{
    Eco eco;
    private DispatcherTimer timer;

    static TickControl()
    {
        AffectsRender<TickControl>(AngleProperty);
    }

    readonly int fieldSize = 10;
    readonly int grassStart = 5;
    readonly int bunnyStart = 1;
    readonly int wolfStart = 0;

    public TickControl()
    {

        eco = new Eco(fieldSize, fieldSize, grassStart, bunnyStart, wolfStart);

        timer = new DispatcherTimer();
        timer.Interval = TimeSpan.FromSeconds(1 / 30.0);
        timer.Tick += (sender, e) => { Angle += Math.PI / 360; eco.SimulateStep(); };

        // timer.Start();
    }

    public void StartTimer()
    {
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
        double grassCounter = Math.Round(eco.GrassSumValue, 2);
        int bunnyCounter = eco.bunnies.Count;
        int wolfCounter = eco.wolves.Count;

        double dx = 700.0 / eco.Width;
        double dy = 700.0 / eco.Height;

        //рисуем траву
        for (int x = 0; x < eco.Width; x++)
            for (int y = 0; y < eco.Width; y++)
            {
                var value = eco.grass[x, y].Value;
                byte r, g, b;
                r = g = b = 255;
                if (value > 0)
                {
                    r = b = 0;
                    g = (byte)(200 - value / Eco.GRASS_LIMIT * 100);
                }

                var brush = new SolidColorBrush(Color.FromRgb(r, g, b), 1);

                ctx.DrawRectangle(brush, null, new Rect(dx * x, dy * y, dx, dy));
            }

        //рисуем кроликов
        foreach (var bunny in eco.bunnies)
        {
            var brush = Brushes.Yellow;
            ctx.DrawEllipse(brush, null, new Rect(dx * bunny.X + 2, dy * bunny.Y + 2, dx - 3, dy - 3));
        }

        // рисуем волков
        foreach (var wolf in eco.wolves)
        {
            var brush = Brushes.Red;
            ctx.DrawEllipse(brush, null, new Rect(dx * wolf.X + 1, dy * wolf.Y + 1, dx - 2, dy - 2));
        }

        var text = $"Grass: {grassCounter}; Bunnies: {bunnyCounter}; Wolves: {wolfCounter}";
        var formattedText = new FormattedText(
            text,
            CultureInfo.CurrentCulture,
            FlowDirection.LeftToRight,
            new Typeface("Arial"),
            16,
            Brushes.White
        );
        ctx.DrawText(formattedText, new Point(260, 700));

        var text2 = $"1 sub = 1 bunny \n1000 subs = 1 wolf";
        var formattedText2 = new FormattedText(
            text2,
            CultureInfo.CurrentCulture,
            FlowDirection.LeftToRight,
            new Typeface("Arial"),
            20,
            Brushes.White
        );
        ctx.DrawText(formattedText2, new Point(70, 710));

    }
}
