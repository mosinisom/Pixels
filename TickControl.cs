using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Documents;
using Avalonia.Media;
using Avalonia.Threading;

namespace AvaloniaCurves;

public class TickControl : Control
{
    Pixels pixels;
    private DispatcherTimer timer;
    private int colorIndex = 0;

    static TickControl()
    {
        AffectsRender<TickControl>(AngleProperty);
    }


    public TickControl()
    {
        List<string> names = new List<string>();
        using (System.IO.StreamReader file = new System.IO.StreamReader("names.txt"))
        {
            string line;
            while ((line = file.ReadLine()) != null)
            {
                names.Add(line);
            }
        }
        int fieldSize = (int)Math.Sqrt(names.Count) + 1;
        pixels = new Pixels(fieldSize, fieldSize, names);

        timer = new DispatcherTimer();
        timer.Interval = TimeSpan.FromSeconds(1 / 3.0);
        timer.Tick += (sender, e) => { Angle += Math.PI / 360; pixels.NextStep(); };

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
        string text;


        double dx = 700.0 / pixels.Width;
        double dy = 700.0 / pixels.Height;
        byte r, g, b;

        ColorAndName currentColor;

        //рисуем пиксели

        for (int x = 0; x < pixels.Width; x++)
        {
            for (int y = 0; y < pixels.Height; y++)
            {
                if (colorIndex >= pixels.allColors.Count)
                    colorIndex = 0;

                currentColor = pixels[x, y];
                ctx.DrawRectangle(Brushes.DarkGray, null, new Rect(0, 700, 700, 50));
                r = (byte)currentColor.R;
                g = (byte)currentColor.G;
                b = (byte)currentColor.B;


                // text = pixels.allColors[colorIndex].Name + "  " + (byte)pixels.allColors[colorIndex].R + " " + (byte)pixels.allColors[colorIndex].G + " " + (byte)pixels.allColors[colorIndex].B;
                var brush = new SolidColorBrush(Color.FromRgb(r, g, b), 1);
                ctx.DrawRectangle(brush, null, new Rect(dx * x, dy * y, dx, dy));



            }
        }
        colorIndex++;

        if (pixels.LastFilledColor != null)
        {
            text = pixels.LastFilledColor.ToString();
            var formattedText = new FormattedText(
                        text,
                        CultureInfo.CurrentCulture,
                        FlowDirection.LeftToRight,
                        new Typeface("Arial"),
                        30,
                        Brushes.White
                    );
            var textColorBrush = new SolidColorBrush(Color.FromRgb((byte)pixels.LastFilledColor.R, (byte)pixels.LastFilledColor.G, (byte)pixels.LastFilledColor.B));
            ctx.DrawRectangle(textColorBrush, null, new Rect(220, 700, formattedText.Width, 30));

            ctx.DrawText(formattedText, new Point(220, 700));
        }
    }
}
