using System;
using System.Collections.Generic;

class Eco
{
    const double GRASS_START = 1;
    const double GRASS_GROW = 1;
    public const double GRASS_LIMIT = 10;
    const double GRASS_BECOME_PARENT = 4;

    const double BUNNY_START = 5;
    const double BUNNY_SPEND = 0.5;
    const double BUNNY_DIVIDED = 10;
    const double BUNNY_EAT = 1.1;

    public int Width { get; init; }
    public int Height { get; init; }
    public Grass[,] grass;
    public List<Bunny> bunnies;
    public List<Wolf> wolves;

    Random rnd = new Random();

    public Eco (int wd, int hg, int gs, int bs, int ws)
    {
        Width = wd;
        Height = hg;

        //создаем поле
        grass = new Grass[wd,hg];
        for (int x=0; x<wd; x++)
            for (int y=0; y<hg; y++)
                grass[x,y] = new Grass();

        //сажаем траву
        for (int i=0; i<gs; i++)
            grass[rnd.Next(wd), rnd.Next(hg)].Value = GRASS_START;

        //раскидываем кроликов
        bunnies = new List<Bunny>();
        for (int i=0; i<bs; i++)
            bunnies.Add(new Bunny() { Value = BUNNY_START, X = rnd.Next(wd), Y = rnd.Next(hg) });
    }

    public void SimulateStep()
    {
        //рост
        for (int x=0; x<Width; x++)
            for (int y=0; y<Height; y++)
                if (grass[x,y].Value > 0 && grass[x,y].Value < GRASS_LIMIT)
                    grass[x,y].Value += GRASS_GROW;

        //распространение травы
        for (int x=0; x<Width; x++)
            for (int y=0; y<Height; y++)
                if (grass[x,y].Value <= 0 && HasParentGrassNeghbour(x,y))
                    grass[x,y].Value = GRASS_START;

        //травоядные
        //питание и голод
        foreach(var b in bunnies)
        {
            if (grass[b.X, b.Y].Value > 0)
            {
                var v = Math.Min(grass[b.X, b.Y].Value, BUNNY_EAT);
                grass[b.X, b.Y].Value -= v;
                b.Value += v;
            }
            b.Value -= BUNNY_SPEND;
        }
        //смерть
        for(int i=bunnies.Count-1; i>=0; i--)
            if (bunnies[i].Value <= 0)
                bunnies.RemoveAt(i);
        //размножение
        for(int i=bunnies.Count-1; i>=0; i--)
            if (bunnies[i].Value >= BUNNY_DIVIDED)
            {
                var d = bunnies[i].Value / 2;
                bunnies[i].Value = d;
                bunnies.Add(new Bunny() { Value = d, X = bunnies[i].X, Y = bunnies[i].Y});
            }
        //движение
        foreach(var b in bunnies)
        {
            b.X += rnd.Next(-1,2);
            b.Y += rnd.Next(-1,2);
            if (b.X <= 0) b.X = 0;
            if (b.Y <= 0) b.Y = 0;
            if (b.X >= Width) b.X = Width-1;
            if (b.Y >= Height) b.Y = Height-1;
        }
    }

    bool HasParentGrassNeghbour(int x, int y)
    {
        return 
            (GetGrass(x-1, y  )?.Value ?? 0) >= GRASS_BECOME_PARENT ||
            (GetGrass(x+1, y  )?.Value ?? 0) >= GRASS_BECOME_PARENT ||
            (GetGrass(x  , y+1)?.Value ?? 0) >= GRASS_BECOME_PARENT ||
            (GetGrass(x  , y-1)?.Value ?? 0) >= GRASS_BECOME_PARENT;
    }

    Grass GetGrass(int x, int y)
    {
        if (x<0 || y <0 || x>= Width || y>=Height)
            return null;
        return grass[x,y];
    }
}