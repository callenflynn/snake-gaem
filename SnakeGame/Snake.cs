using System.Collections.Generic;
using System.Linq;
using System;

public class Snake
{
    public List<Point> Body { get; private set; }
    public int Length { get; private set; }
    public Point Direction { get; set; }

    public Point Head => Body[0];

    public Snake(Point startPosition)
    {
        Body = new List<Point> { startPosition };
        Length = 1;
        Direction = new Point(1, 0); 
    }

    public void Move()
    {
        Point newHead = new Point(Body[0].X + Direction.X, Body[0].Y + Direction.Y);
        Body.Insert(0, newHead);

        if (Body.Count > Length)
        {
            Body.RemoveAt(Body.Count - 1);
        }
    }

    public void Grow()
    {
        Length++;
    }

    public bool CheckCollision(Point point)
    {
        return Body.Any(segment => segment.Equals(point));
    }
}

public struct Point
{
    public int X { get; }
    public int Y { get; }

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public override bool Equals(object obj)
    {
        if (obj is Point other)
        {
            return X == other.X && Y == other.Y;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }
}