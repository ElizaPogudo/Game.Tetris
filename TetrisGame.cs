﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Game.Tetris
{
    public class TetrisGame
    {
        private const int CellSize = 23;
        private const int GameFieldWidth = 230;
        private const int GameFieldHeight = 460;
        private const int GameFieldWidthInCells = 10;
        private const int GameFieldHeightInCells = 20;

        private readonly Random random = new Random();
        private Direction moveDirection;
        private Figure activeFigure;
        private List<Figure> fallenFigures = new List<Figure>();
        private bool isPause;
        public event Action Defeat = delegate { };

        private class Figure
        {
            public int KeyPoint { get; set; }
            public List<Point> Points { get; set; }

            public Figure(List<Point> pointsList, int keyPoint = 0)
            {
                KeyPoint = keyPoint;
                Points = pointsList;
            }

            public Figure(Point point) :this(new List<Point> { point })
            {}
        }

        public void Restart()
        {
            moveDirection = Direction.None;
            fallenFigures.Clear();
            activeFigure = GetRandomFigure();
            isPause = false;
        }
        public void Draw(Graphics graphics)
        {
            for (int i = 0; i < GameFieldWidthInCells; i++)
            {
                graphics.DrawLine(Pens.Gray, i * CellSize, 0, i * CellSize, GameFieldHeight);
            }

            for (int i = 0; i < GameFieldHeightInCells; i++)
            {
                graphics.DrawLine(Pens.Gray, 0, i * CellSize, GameFieldWidth, i * CellSize);
            }

            foreach (Figure figure in fallenFigures)
            {
                foreach (Point point in figure.Points)
                {
                    graphics.FillRectangle(Brushes.Blue, point.X * CellSize, point.Y * CellSize, CellSize, CellSize);
                }
            }
            foreach (Point point in activeFigure.Points)
            {
                graphics.FillRectangle(Brushes.Blue, point.X * CellSize, point.Y * CellSize, CellSize, CellSize);
            }

        }

        public void Move()
        {
            if (isPause)
                return;

            if (!IsReachedBorder(activeFigure))
            {
                for (int i = 0; i < activeFigure.Points.Count; i++)
                {
                    activeFigure.Points[i] = activeFigure.Points[i].Displace(0, 1);
                }
            }
            else 
            { 
                activeFigure = GetRandomFigure();
                if (IsDefeat(activeFigure))
                {
                    isPause = true;
                    Defeat();
                    return;
                }
            }
        }

        private bool IsReachedBorder(Figure figure)
        {
            if (fallenFigures.Any(p => p.Points.Any(n => figure.Points.Contains(n.Displace(0, -1)))) 
                || figure.Points.Any(p => p.Displace(0, 1).Y == GameFieldHeightInCells))
            {
                fallenFigures.Add(figure);
                return true;
            }
            return false;
        }

        private bool IsDefeat(Figure figure)
        {
            return fallenFigures.Any(p => p.Points.Any(n => figure.Points.Contains(n)));
        }

        private Figure GetRandomFigure()
        {
            Point FigureTopCell = new Point(4, 0);

            FigureType figureType = (FigureType)random.Next(0, 7);
            switch (figureType)
            {
                case FigureType.O: return new Figure(new List<Point>() 
                { 
                    FigureTopCell, 
                    new Point(FigureTopCell.X + 1, FigureTopCell.Y), 
                    new Point(FigureTopCell.X, FigureTopCell.Y + 1), 
                    new Point(FigureTopCell.X + 1, FigureTopCell.Y + 1) 
                });

                case FigureType.I: return new Figure(new List<Point>()
                { 
                    FigureTopCell, 
                    new Point(FigureTopCell.X, FigureTopCell.Y + 1), 
                    new Point(FigureTopCell.X, FigureTopCell.Y + 2), 
                    new Point(FigureTopCell.X, FigureTopCell.Y + 3) 
                }, 2);
                
                case FigureType.J: return new Figure(new List<Point>()
                { 
                   FigureTopCell, 
                   new Point(FigureTopCell.X, FigureTopCell.Y + 1), 
                   new Point(FigureTopCell.X, FigureTopCell.Y + 2), 
                   new Point(FigureTopCell.X - 1, FigureTopCell.Y + 2) 
                }, 2);

                case FigureType.L: return new Figure(new List<Point>()
                {
                   FigureTopCell,
                   new Point(FigureTopCell.X, FigureTopCell.Y + 1),
                   new Point(FigureTopCell.X, FigureTopCell.Y + 2),
                   new Point(FigureTopCell.X + 1, FigureTopCell.Y + 2)
                }, 2);

                case FigureType.S: return new Figure(new List<Point>()
                {
                   FigureTopCell,
                   new Point(FigureTopCell.X + 1, FigureTopCell.Y),
                   new Point(FigureTopCell.X, FigureTopCell.Y + 1),
                   new Point(FigureTopCell.X - 1, FigureTopCell.Y + 1)
                }, 3);

                case FigureType.T: return new Figure(new List<Point>()
                {
                   FigureTopCell,
                   new Point(FigureTopCell.X - 1, FigureTopCell.Y),
                   new Point(FigureTopCell.X + 1, FigureTopCell.Y),
                   new Point(FigureTopCell.X, FigureTopCell.Y + 1)
                }, 1);

                case FigureType.Z: return new Figure(new List<Point>()
                {
                   FigureTopCell,
                   new Point(FigureTopCell.X - 1, FigureTopCell.Y),
                   new Point(FigureTopCell.X, FigureTopCell.Y + 1),
                   new Point(FigureTopCell.X + 1, FigureTopCell.Y + 1)
                }, 3);

                case FigureType.P: return new Figure(FigureTopCell);
                default: throw new ArgumentOutOfRangeException(nameof(figureType), figureType, null);
            }
        }

    }
}
