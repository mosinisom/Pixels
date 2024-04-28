using System;

public class ColorAndName(int r, int g, int b, string name)
{
    public int R { get; set; } = r;
    public int G { get; set; } = g;
    public int B { get; set; } = b;
    public string Name { get; set; } = name;

    public override string ToString()
    {
        return $"{Name} {Math.Abs(R)} {Math.Abs(G)} {Math.Abs(B)}";
    }
}