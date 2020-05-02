using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FittingALineIntoData
{
    public partial class MainForm : Form
    {

        private class Line
        {
            public float M;
            public float B;
            public Pen Pen;
            public double Cost;
            public StringBuilder Log;
        }

        private readonly HashSet<Point> _points = new HashSet<Point>();
        private readonly List<Line> _lines = new List<Line>();

        public MainForm()
        {
            InitializeComponent();
            // _lines.Add(new Line() { Pen = Pens.OrangeRed, M = 2, B = 0 });

            // _lines.Add(new Line() { Pen = Pens.Red, B = 200, M = (float)Math.Tan(-45 / 180.0 * Math.PI) });
        }

        private void DataPanelPaint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            graphics.Clear(Color.White);
            foreach (Point point in _points)
            {
                //graphics.FillEllipse();
                graphics.FillEllipse(Brushes.Black, point.X - 5, dataPanel.Height - point.Y - 5, 10, 10);
            }

            foreach (Line line in _lines)
            {
                graphics.DrawLine(line.Pen, 0, dataPanel.Height - line.B, dataPanel.Width, dataPanel.Height - (dataPanel.Width * line.M + line.B));
            }

        }
        private void DataPanelClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }
            AddPoint(e.Location);

        }
        private void AddPointBtnClick(object sender, EventArgs e)
        {
            string pointInput = Interaction.InputBox("Enter X and Y separated by comma:");
            if (String.IsNullOrWhiteSpace(pointInput))
            {
                return;
            }

            pointInput = pointInput.Replace(" ", "");
            string[] pointInputSplit = pointInput.Split(',');
            if (pointInputSplit.Length < 2)
            {
                MessageBox.Show("Invalid input");
                return;
            }

            string xString = pointInputSplit[0];
            if (!Int32.TryParse(xString, out int x))
            {
                MessageBox.Show("Invalid X");
                return;
            }

            string yString = pointInputSplit[1];
            if (!Int32.TryParse(yString, out int y))
            {
                MessageBox.Show("Invalid Y");
                return;
            }
            AddPoint(new Point(x, y));
        }
        private void AddPoint(Point point)
        {
            point.Y = dataPanel.Height - point.Y;
            if (_points.Add(point))
            {
                pointsListBox.Items.Add($"{point.X}, {point.Y}");
            }

            dataPanel.Refresh();
        }
        private void RemovePointBtnClick(object sender, EventArgs e)
        {
            string selectedItem = pointsListBox.SelectedItem as string;
            if (selectedItem == null)
            {
                return;
            }

            string[] pointSplit = selectedItem.Replace(" ", "").Split(',');
            int x = Int32.Parse(pointSplit[0]);
            int y = Int32.Parse(pointSplit[1]);
            _points.Remove(new Point(x, y));
            pointsListBox.Items.Remove(selectedItem);
            dataPanel.Refresh();
        }
        private void FitBtnClick(object sender, EventArgs e)
        {
            #region Fit using m
            //const double mLearningRate = 0.0001;
            //const double bLearningRate = 1;
            //// pick random starting points
            //Random randomGen = new Random();
            //double m = randomGen.Next(100);
            //double b = randomGen.Next(100);
            ////double m = 0;
            ////double b = 0;


            //// calculate df/db
            //// calculate df/dm
            //// (f(x)) os the cost function
            //double bDelta = 0.005d;
            //double mDelta = 0.005d;

            //const int maxIterations = 5000;

            //int currentIteration = 0;
            //double bChange;
            //double mChange;
            //double currentCost = CostFunction(m, b);
            //double diffrence;
            //do
            //{
            //    double bSlope = (CostFunction(m, b + bDelta) - currentCost) / bDelta;


            //    double mSlope = (CostFunction(m + mDelta, b) - currentCost) / mDelta;


            //    bChange = -(bSlope * bLearningRate);
            //    mChange = -(mSlope * mLearningRate);

            //    b += bChange;
            //    m += mChange;


            //    currentIteration++;
            //    double newCost = CostFunction(m, b);
            //    diffrence = newCost - currentCost;
            //    Console.WriteLine("d={0}, cost={1}, M={2}, B={3}", diffrence, newCost, m, b);
            //    currentCost = newCost;
            //} while (currentIteration < maxIterations && Math.Abs(diffrence) > 0.005);

            //Console.WriteLine("M={0}, B={1}, i={2}", m, b, currentIteration);
            #endregion



            const double mLearningRate = 0.0000005;
            const double bLearningRate = 1;

            Line bestLine = new Line()
            {
                Cost = Double.PositiveInfinity
            };
            const int linesToGenerate = 50;
            Random randomGen = new Random();




            for (int i = 0; i < linesToGenerate; i++)
            {
                StringBuilder log = new StringBuilder();

                // pick random starting points
                double m = randomGen.NextDouble() * 2 * Math.PI - Math.PI;
                double b = randomGen.Next(dataPanel.Height * -5, dataPanel.Height * 5);
                //double m = Math.PI / 2;
                //double b = 0;


                // calculate df/db
                // calculate df/dm
                // (f(x)) os the cost function
                double bDelta = 0.00005d;
                double mDelta = 0.00005d;

                const int maxIterations = 5000;

                int currentIteration = 0;
                double bChange;
                double mChange;
                double currentCost = CostFunctionWithAngle(m, b);
                double diffrence;
                do
                {
                    double bSlope = (CostFunctionWithAngle(m, b + bDelta) - currentCost) / bDelta;


                    double mSlope = (CostFunctionWithAngle(m + mDelta, b) - currentCost) / mDelta;


                    bChange = -(bSlope * bLearningRate);
                    mChange = -(mSlope * mLearningRate);

                    b += bChange;
                    m += mChange;


                    currentIteration++;
                    double newCost = CostFunctionWithAngle(m, b);
                    diffrence = newCost - currentCost;

                    log.AppendLine($"d={diffrence}, cost={newCost}, M={m}, B={b}");


                    currentCost = newCost;
                } while (currentIteration < maxIterations && Math.Abs(diffrence) > 0.000000000005);

                if (currentCost < bestLine.Cost)
                {
                    bestLine = new Line()
                    {
                        M = (float)Math.Tan(m),
                        B = (float)b,
                        Cost = currentCost,
                        Pen = Pens.Red,
                        Log = log
                    };
                }
                else
                {
                    log.Clear();
                }
                //Console.WriteLine("M={0}, B={1}, i={2}, cost={3}", m, b, currentIteration, currentCost);

            }

            _lines.ForEach(line =>
            {
                if (line.Pen == Pens.Red)
                {
                    line.Pen = Pens.DarkRed;
                }
                else if (line.Pen == Pens.Cyan)
                {
                    line.Pen = Pens.DarkCyan;
                }
            });
            //  Console.WriteLine(bestLine.Log.ToString());
            Console.WriteLine("Best - M={0}, B={1}, cost={2}", bestLine.M, bestLine.B, bestLine.Cost);
            _lines.Add(bestLine);
            dataPanel.Refresh();
        }

        double CostFunction(double m, double b)
        {
            //double[] pointA = { 0d, 0d };
            //double[] pointB = { dataPanel.Width, dataPanel.Width * m + b };
            double cost = 0;
            foreach (Point point in _points)
            {
                //double distance = DistanceBetweenLineAndPoint(m, b, point.X, point.Y);
                double distance = point.Y - (point.X * m + b);
                cost += distance * distance;
            }

            return Math.Sqrt(cost);
        }
        double CostFunctionWithAngle(double mAngle, double b)
        {
            double m = Math.Tan(mAngle);
            return CostFunction(m, b);
        }

        Line GetLine(double[] x, double[] y)
        {

            double pointsNum = x.Length;

            double ySum = 0;
            double xSum = 0;
            double xSquaredSum = 0;
            double ySquaredSum = 0;
            double xMultipliedByYSum = 0;

            for (int i = 0; i < x.Length; i++)
            {
                xSum += x[i];
                ySum += y[i];
                xMultipliedByYSum += x[i] * y[i];
                xSquaredSum += Math.Pow(x[i], 2);
                ySquaredSum += Math.Pow(y[i], 2);
            }

            double xSumSquared = Math.Pow(xSum, 2);
            double ySumSquared = Math.Pow(ySum, 2);

            double m = (pointsNum * xMultipliedByYSum - xSum * ySum) / (pointsNum * xSquaredSum - xSumSquared);
            double b = (ySum * xSquaredSum - xSum * xMultipliedByYSum) / (pointsNum * xSquaredSum - xSumSquared);
            Console.WriteLine("Real - cost={0}, M={1}, B={2}", CostFunction(m, b), m, b);
            return new Line() { B = (float)b, M = (float)m, Pen = Pens.Cyan };
        }

        //private double CalculateM(double xSum, double ySum, double xSquaredSum, double ySquaredSum, double xMultipliedByYSum, double xSumSquared, double ySumSquared, int pointsNum)
        //{
        //    #region old code
        //    //double numerator = 0;
        //    //double denominator = 0;

        //    //double denominatorVar = 2;

        //    //for (int i = 0; i < x.Length; i++)
        //    //{
        //    //    numerator += y[i] * x[i];
        //    //    for (int j = 0; j < x.Length; j++)
        //    //    {
        //    //        if (j == i)
        //    //        {
        //    //            continue;
        //    //        }

        //    //        numerator -= y[i] * x[j];
        //    //    }

        //    //    denominator += Math.Pow(x[i], 2);
        //    //    denominatorVar *= x[i];
        //    //}

        //    //denominator -= denominatorVar;
        //    //return numerator / denominator;
        //    #endregion

        //    double numerator = pointsNum * xMultipliedByYSum - xSum * ySum;
        //    double denominator = pointsNum * xSquaredSum - xSumSquared;
        //    return numerator / denominator;


        //}
        //private double CalculateB(double[] x, double[] y, double m)
        //{
        //    #region old code
        //    //double numerator = 0;
        //    //for (int i = 0; i < x.Length; i++)
        //    //{
        //    //    numerator += y[i];
        //    //    numerator -= x[i] * m;
        //    //}

        //    //return numerator / 2;
        //    #endregion


        //}

        //private double DistanceBetweenLineAndPoint(double m, double b, double x, double y)
        //{
        //    return Math.Sqrt((Math.Pow(x * m + b - y, 2)) / (m * m + 1));
        //}

        ////Compute the dot product AB . BC
        //private double DotProduct(double[] pointA, double[] pointB, double[] pointC)
        //{
        //    double[] AB = new double[2];
        //    double[] BC = new double[2];
        //    AB[0] = pointB[0] - pointA[0];
        //    AB[1] = pointB[1] - pointA[1];
        //    BC[0] = pointC[0] - pointB[0];
        //    BC[1] = pointC[1] - pointB[1];
        //    double dot = AB[0] * BC[0] + AB[1] * BC[1];

        //    return dot;
        //}

        ////Compute the cross product AB x AC
        //private double CrossProduct(double[] pointA, double[] pointB, double[] pointC)
        //{
        //    double[] AB = new double[2];
        //    double[] AC = new double[2];
        //    AB[0] = pointB[0] - pointA[0];
        //    AB[1] = pointB[1] - pointA[1];
        //    AC[0] = pointC[0] - pointA[0];
        //    AC[1] = pointC[1] - pointA[1];
        //    double cross = AB[0] * AC[1] - AB[1] * AC[0];

        //    return cross;
        //}

        ////Compute the distance from A to B
        //double Distance(double[] pointA, double[] pointB)
        //{
        //    double d1 = pointA[0] - pointB[0];
        //    double d2 = pointA[1] - pointB[1];

        //    return Math.Sqrt(d1 * d1 + d2 * d2);
        //}

        ////Compute the distance from AB to C
        //double LineToPointDistance2D(double[] pointA, double[] pointB, double[] pointC)
        //{
        //    double dist = CrossProduct(pointA, pointB, pointC) / Distance(pointA, pointB);
        //    return Math.Abs(dist);
        //}

        //private float EuclidianDistance(float val1, float val2) =>
        //    (float)Math.Sqrt(val1 * val1 + val2 * val2);

        private void ClearAllBtnClick(object sender, EventArgs e)
        {
            _lines.Clear();
            _points.Clear();
            pointsListBox.Items.Clear();
            dataPanel.Refresh();
            Console.Clear();
        }

        private void BestLineBtnClick(object sender, EventArgs e)
        {
            double[] x = _points.Select(point => (double)point.X).ToArray();
            double[] y = _points.Select(point => (double)point.Y).ToArray();
            _lines.Add(GetLine(x, y));
            dataPanel.Refresh();
        }
    }
}
