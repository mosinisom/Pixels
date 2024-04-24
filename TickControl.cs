using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Threading;

namespace AvaloniaCurves;

public class TickControl : Control
{
    Pixels pixels;
    private DispatcherTimer timer;

    static TickControl()
    {
        AffectsRender<TickControl>(AngleProperty);
    }

    readonly int fieldSize = 100;

    public TickControl()
    {
        List<string> names = new List<string> { "Grass", "Bunny", "Wolf", "Fox", "Bear", "Deer", "Moose", "Rabbit", "Squirrel", "Hare", "Hedgehog", "Raccoon", "Beaver", "Otter", "Mink", "Weasel", "Badger", "Skunk", "Opossum", "Porcupine", "Armadillo", "Anteater", "Sloth", "Aardvark", "Elephant", "Rhino", "Hippopotamus", "Giraffe", "Okapi", "Zebra", "Horse", "Donkey", "Mule", "Tapir", "Pig", "Warthog", "Hog", "Boar", "Peccary", "Camel", "Llama", "Alpaca", "Guanaco", "Vicuna", "Whale", "Dolphin", "Porpoise", "Orca", "Manatee", "Dugong", "Seal", "Sea Lion", "Walrus", "Polar Bear", "Panda", "Sloth Bear", "Spectacled Bear", "Sun Bear", "Brown Bear", "Black Bear", "Grizzly", "Kodiak", "Polar Bear", "Panda", "Sloth Bear", "Spectacled Bear", "Sun Bear", "Brown Bear", "Black Bear", "Grizzly", "Kodiak", "Polar Bear", "Panda", "Sloth Bear", "Spectacled Bear", "Sun Bear", "Brown Bear", "Black Bear", "Grizzly", "Kodiak", "Polar Bear", "Panda", "Sloth Bear", "Spectacled Bear", "Sun Bear", "Brown Bear", "Black Bear", "Grizzly", "Kodiak", "Polar Bear", "Panda", "Sloth Bear", "Spectacled Bear", "Sun Bear", "Brown Bear", "Black Bear", "Grizzly", "Kodiak", "Polar Bear", "Panda", "Sloth Bear", "Spectacled Bear", "Sun Bear", "Brown Bear", "Black Bear", "Grizzly", "Kodiak", "Polar Bear", "Panda", "Sloth Bear", "Spectacled Bear", "Sun Bear", "Brown Bear", "Black Bear", "Grizzly", "Kodiak", "Polar Bear", "Panda", "Sloth Bear", "Spectacled Bear", "Sun Bear", "Brown Bear", "Black Bear", "Grizzly", "Kodiak", "Polar Bear", "Panda", "Sloth Bear", "Spectacled Bear", "Sun Bear", "Brown Bear", "Black Bear", "Grizzly", "Kodiak", "Polar Bear", "Panda", "Sloth Bear", "Spectacled Bear", "Sun Bear", "Brown Bear", "Black Bear", "Grizzly", "Kodiak", "Polar Bear", "Panda", "Sloth Bear", "Spectacled Bear", "Sun Bear", "Brown Bear", "Black Bear", "Grizzly", "Kodiak", "Polar Bear", "Panda", "Sloth Bear", "Spectacled Bear", "Sun Bear", "Brown Bear", "Black Bear", "Grizzly", "Kodiak", "Polar Bear", "Panda", "Sloth Bear", "Spectacled Bear", "Sun Bear", "Brown Bear", "Black Bear", "Grizzly", "Kodiak", "Polar Bear", "Panda", "Sloth Bear", "Spectacled Bear", "Sun Bear", "Brown Bear", "Black Bear", "Grizzly", "Kodiak", "Polar Bear", "Panda", "Sloth Bear", "Spectacled Bear", "Sun Bear", "Brown Bear", "Black Bear", "Grizzly", "Kodiak", "Polar Bear", "Panda", "Sloth Bear", "Spectacled Bear", "Sun Bear", "Brown Bear", "Black Bear", "Grizzly", "Kodiak", "Polar Bear", "Panda", "Sloth Bear", "Spectacled Bear", "Sun Bear", "Brown Bear", "Black Bear", "Grizzly", "Kodiak", "Polar Bear", "Panda", "Sloth Bear", "Spectacled Bear", "Sun Bear", "Brown Bear", "Black Bear", "Grizzly", "Kodiak", "Polar Bear", "Panda", "Sloth Bear", "Spectacled Bear", "Sun Bear", "Brown" };

        pixels = new Pixels(fieldSize, fieldSize, names);

        timer = new DispatcherTimer();
        timer.Interval = TimeSpan.FromSeconds(1 / 10.0);
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

        //рисуем пиксели
        for (int x = 0; x < pixels.Width; x++)
            for (int y = 0; y < pixels.Width; y++)
            {
                ctx.DrawRectangle(Brushes.Black, null, new Rect(260, 700, 500, 30));
                r = (byte)pixels[x, y].R;
                g = (byte)pixels[x, y].G;
                b = (byte)pixels[x, y].B;
                text = pixels[x, y].ToString();

                // text = pixels[x, y].Name + " - " + r + " " + g + " " + b;
                var brush = new SolidColorBrush(Color.FromRgb(r, g, b), 1);


                var formattedText = new FormattedText(
                    text,
                    CultureInfo.CurrentCulture,
                    FlowDirection.LeftToRight,
                    new Typeface("Arial"),
                    16,
                    Brushes.White
                );
                ctx.DrawRectangle(brush, null, new Rect(dx * x, dy * y, dx, dy));
                ctx.DrawText(formattedText, new Point(260, 700));
            }
    }
}
