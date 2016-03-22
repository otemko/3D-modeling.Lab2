using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace WindowsFormsApplication1
{
    class Draw
    {
        private List<PointOfFigure> listPoints;

        internal List<PointOfFigure> ListPoints
        {
            get
            {
                return listPoints;
            }

            set
            {
                listPoints = value;
            }
        }

        public Draw()
        {
            ListPoints = new List<PointOfFigure>();
        }

        public void DrawPoint(Color colorPoint, Graphics formGraphics, PointOfFigure point)
        {
            int sizePoint = 10;
            var brush = new SolidBrush(colorPoint);
            var font = new Font("Times New Roman", 18);

            formGraphics.FillEllipse(brush, point.Coordinates.X - sizePoint / 2, point.Coordinates.Y - sizePoint / 2, sizePoint, sizePoint);
            formGraphics.DrawString(point.Number.ToString(), font, brush, point.Coordinates.X - sizePoint * 2, point.Coordinates.Y - sizePoint * 2);
        }

        public void AddPointInList(PointOfFigure point)
        {
            listPoints.Add(point);
        }

        public void DrawLines(Color colorPoint, Graphics formGraphics, List<PointOfFigure> listPoints)
        {
            var pen = new Pen(colorPoint, 5);

            formGraphics.DrawLine(pen, listPoints[0].Coordinates, listPoints[listPoints.Count - 1].Coordinates);

            for (int i = 0; i < listPoints.Count - 1; i++)
            {
                formGraphics.DrawLine(pen, listPoints[i].Coordinates, listPoints[i + 1].Coordinates);
            }
            
        }

        public List<PointOfFigure> MoveMentAllPoints(double dx, double dy, List<PointOfFigure> list)
        {
            List<PointOfFigure> listNewPoints = new List<PointOfFigure>();

            for (int i = 0; i < list.Count; i++)
            {
                Vector<double> vectorCoorinate = DenseVector.OfArray(new double[] { list[i].Coordinates.X, list[i].Coordinates.Y, 1 });
                Matrix<double> T = DenseMatrix.OfArray(new double[,] {
                                                                  { 1, 0, 0},
                                                                  { 0, 1, 0},
                                                                  { dx, -dy, 1},
                                                                 });
                var resultVector = vectorCoorinate * T;
                var resultArray = resultVector.ToArray();

                listNewPoints.Add(new PointOfFigure(new Point((int)resultArray[0], (int)resultArray[1]), i + 1));
            }

            return listNewPoints;
        }

        public List<PointOfFigure> ScaleAllPoints(double sx, double sy, int point)
        {
            if (!CheckPoint(point))
                return listPoints;

            var currentPoint =
                (from pointlinq in listPoints
                 where pointlinq.Number == point
                 select pointlinq).First();

            var listMovement = MoveMentAllPoints(0-currentPoint.Coordinates.X, currentPoint.Coordinates.Y, listPoints);

            List<PointOfFigure> listScalePoints = new List<PointOfFigure>();

            for (int i = 0; i < listMovement.Count; i++)
            {
                if (listMovement[i].Number != currentPoint.Number)
                {
                    Vector<double> vectorCoorinate = DenseVector.OfArray(new double[] { listMovement[i].Coordinates.X, listMovement[i].Coordinates.Y, 1 });
                    Matrix<double> T = DenseMatrix.OfArray(new double[,] {
                                                                  { sx, 0, 0},
                                                                  { 0, sy, 0},
                                                                  { 0, 0, 1},
                                                                 });
                    var resultVector = vectorCoorinate * T;
                    var resultArray = resultVector.ToArray();

                    listScalePoints.Add(new PointOfFigure(new Point((int)resultArray[0], (int)resultArray[1]), i + 1));
                }
                else
                {
                    listScalePoints.Add(new PointOfFigure(listMovement[i].Coordinates, listMovement[i].Number));
                }
            }

            var listMovementAfterScale = MoveMentAllPoints(currentPoint.Coordinates.X, 0-currentPoint.Coordinates.Y, listScalePoints);

            return listMovementAfterScale;
        }

        public List<PointOfFigure> RotationAllPoints(double alpha, int point)
        {
            if (!CheckPoint(point))
                return listPoints;

            var currentPoint =
                 (from pointlinq in listPoints
                  where pointlinq.Number == point
                  select pointlinq).First();

            var listMovement = MoveMentAllPoints(-currentPoint.Coordinates.X, currentPoint.Coordinates.Y, listPoints);

            List<PointOfFigure> listRotatePoints = new List<PointOfFigure>();

            for (int i = 0; i < listMovement.Count; i++)
            {
                if (listMovement[i].Number != currentPoint.Number)
                {
                    Vector<double> vectorCoorinate = DenseVector.OfArray(new double[] { listMovement[i].Coordinates.X, listMovement[i].Coordinates.Y, 1 });
                    Matrix<double> T = DenseMatrix.OfArray(new double[,] {
                                                                  { Math.Cos(-alpha*Math.PI/180), Math.Sin(-alpha*Math.PI/180), 0},
                                                                  { -Math.Sin(-alpha*Math.PI/180), Math.Cos(-alpha*Math.PI/180), 0},
                                                                  { 0, 0, 1},
                                                                 });
                    var resultVector = vectorCoorinate * T;
                    var resultArray = resultVector.ToArray();

                    listRotatePoints.Add(new PointOfFigure(new Point((int)resultArray[0], (int)resultArray[1]), i + 1));
                }
                else
                {
                    listRotatePoints.Add(new PointOfFigure(listMovement[i].Coordinates, listMovement[i].Number));
                }
            }

            var listMovementAfterRotate = MoveMentAllPoints(currentPoint.Coordinates.X, -currentPoint.Coordinates.Y, listRotatePoints);

            return listMovementAfterRotate;
        }

        public void DrawListElements(Color color, List<PointOfFigure> list, Graphics formGraphics)
        {

            foreach (var item in list)
            {
                DrawPoint(color, formGraphics, item);
            }

            DrawLines(color, formGraphics, list);

        }

        private bool CheckPoint(int point)
        {
            foreach (var item in listPoints)
            {
                if (item.Number == point)
                    return true;
            }
            return false;
        }

    }
}

