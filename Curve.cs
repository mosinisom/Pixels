using Avalonia;
using System;
using System.Collections.Generic;

namespace AvaloniaCurves;

public class Curve
{
    //массив точек
    public List<Point> points = new List<Point>();

    //максимальнео значение параметра T (используется в методе GetPoint)
    public double MaxT => points.Count - 1 <= 0 ? 0 : points.Count - 1; 

    /// <summary>
    /// Получить точку на кривой в зависимости от параметра T
    /// </summary>
    /// <param name="t">параметр, который меняется в интервале от 0 до MaxT</param>
    /// <returns></returns>
    public Point GetPoint(double t)
    {
        //защита от некорректных значений
        if (points == null || points.Count <= 1) return new Point(0,0);

        //обеспечим попадание параметра t интервал от 0 до MaxT
        t = Math.Abs(t) % MaxT;

        int index1 = (int)t; //отбрасываем дробную часть
        int index2 = index1+1;

        return GetPointOnLine(points[index1], points[index2], t % 1);
    }

    Point GetPointOnLine(Point p1, Point p2, double t)
    {
        return new Point(p1.X + t * (p2.X-p1.X), p1.Y + t * (p2.Y-p1.Y));
    }

}