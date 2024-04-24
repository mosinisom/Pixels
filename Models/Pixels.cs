using System;
using System.Collections.Generic;
using System.Xml;
using Avalonia.Media;

class Pixels
{
    ColorAndName white = new ColorAndName(255, 255, 255, "white");
    public int Width { get; init; }
    public int Height { get; init; }
    public List<string> Names { get; init; }
    public ColorAndName[,] colorAndNames;
    public ColorAndName this[int x, int y]
    {
        get => colorAndNames[x, y];
        set => colorAndNames[x, y] = value;
    }
    public List<ColorAndName> allColors;
    public Random random = new Random();


    public Pixels(int wd, int hg, List<string> names)
    {
        wd = wd < 1 ? 1 : wd;
        hg = hg < 1 ? 1 : hg;

        Width = wd;
        Height = hg;
        Names = names;
        colorAndNames = new ColorAndName[Width, Height];
        for (int x = 0; x < Width; x++)
            for (int y = 0; y < Height; y++)
                colorAndNames[x, y] = white;

        allColors = GetColors();
    }

    public List<ColorAndName> GetColors()
    {
        List<ColorAndName> colors = new List<ColorAndName>();
        foreach (var name in Names)
        {
            var hash = name.GetHashCode();
            int r = hash % 256;
            int g = hash / 256 % 256;
            int b = hash / 256 / 256 % 256;
            colors.Add(new ColorAndName(r, g, b, name));
        }
        return colors;
    }

    int index2 = 0;
    public void NextStep()
    {
        int index = 0;

        for (int x = 0; x < Width; x++)
            for (int y = 0; y < Height; y++)
            {
                if (index >= index2)
                    colorAndNames[x, y] = white;
                else
                    colorAndNames[x, y] = allColors[index++];
            }
        if (index2 < allColors.Count) index2++;
    }










}