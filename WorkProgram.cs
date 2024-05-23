using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSummaryDesign
{
    internal class WorkProgram
    {
        private Graphics _graphics = null;
        private Pen _pen = new Pen(Color.Black,1);
        private Point _startPoint = new Point();
        private Point _endPoint = new Point();
        private Point _centralPoint = new Point();
        private int _width;
        private int _height;
        private int _radius;
        private SolidBrush _solidBrush;

        private enum GraphType
        {
            Oval = 0,
            Line,
            Rec,
        }
        private int _graphType;

        public WorkProgram() { }

        public WorkProgram(Graphics graphics) { _graphics = graphics; }

        // 深复制构造函数  
        public WorkProgram(WorkProgram other)
        {
            Width=other.Width;
            Height=other.Height;
            _radius=other.Radius;
            _solidBrush=other.SolidBrush;
            StartPoint=other.StartPoint;
            EndPoint=other.EndPoint;
            _centralPoint=other.CentralPoint;
            GraphType1 = other.GraphType1;
            Pen = other.Pen;
            Graphics=other.Graphics;
        }


        public Graphics Graphics { get => _graphics; set => _graphics = value; }
        public Point StartPoint { get => _startPoint; set => _startPoint = value; }
        public int Width { get => _width; set => _width = value; }
        public int Radius { get => _radius; set => _radius = value; }
        public int Height { get => _height; set => _height = value; }
        public Pen Pen { get => _pen; set => _pen = value; }
        public int GraphType1 { get => _graphType; set => _graphType = value; }
        public SolidBrush SolidBrush { get => _solidBrush; set => _solidBrush = value; }
        public Point EndPoint { get => _endPoint; set => _endPoint = value; }
        public Point CentralPoint { get => _centralPoint; set => _centralPoint = value; }

        #region 绘图部分
        public void Clear()
        {
            _graphics.Clear(Color.White);
        }

        public void SetBackImage(Image image)
        {
            if(image == null)
            {
                return;
            }
            _graphics.DrawImage(image,new Point(0,0));
        }

        public void DrawRectangle(Point p)
        {
            Clear();
            _width = p.X - _startPoint.X;
            _height = p.Y - _startPoint.Y;
            if(_width-5 < 0 || _height-5 < 0)
            {
                _startPoint = p;
                _width = _endPoint.X-_startPoint.X;
                _height=_endPoint.Y-_startPoint.Y;
            }
            else
            {
                _endPoint = p;
            }
            _centralPoint=new Point(_startPoint.X+_width>>1, _startPoint.Y+_height>>1);
            Rectangle rectangle = new Rectangle(_startPoint, new Size((int)_width, (int)_height));
            _graphics.DrawRectangle(_pen,rectangle);
        }

        public void DrawOval(Point p)
        {
            Clear();
            _width = p.X - _startPoint.X;
            _height = p.Y - _startPoint.Y;
            if (_width - 5 < 0 || _height - 5 < 0)
            {
                _startPoint = p;
                _width = _endPoint.X - _startPoint.X;
                _height = _endPoint.Y - _startPoint.Y;
            }
            else
            {
                _endPoint = p;
            }
            _centralPoint = new Point(_startPoint.X + _width / 2, _startPoint.Y + _height / 2);
            Rectangle rectangle = new Rectangle(_startPoint, new Size((int)_width, (int)_height));
            _graphics.DrawEllipse(_pen, rectangle);
        }

        public void DrawLine(Point p)
        {
            Clear();
            _endPoint = p;
            _centralPoint = new Point((_endPoint.X+_startPoint.X)>>1, (_endPoint.Y+_startPoint.Y)>>1);
            _graphics.DrawLine(_pen,_startPoint, p);
        }

        /// <summary>
        /// 将子画布投射到主画布
        /// </summary>
        /// <param name="work"></param>
        /// <param name="type"></param>
        public void DrawCopy(WorkProgram work,int type)
        {
            switch (type)
            {
                case (int)GraphType.Rec:
                    {
                        Rectangle rectangle = new Rectangle(work._startPoint, new Size(work._width,work._height));
                        if (work.SolidBrush != null)
                            _graphics.FillRectangle(work.SolidBrush, rectangle);
                        _graphics.DrawRectangle(_pen, rectangle);
                        break;
                    }
                case (int)GraphType.Oval:
                    {
                        Rectangle rectangle = new Rectangle(work._startPoint, new Size(work._width, work._height));
                        if (work.SolidBrush != null)
                            _graphics.FillEllipse(work.SolidBrush, rectangle);
                        _graphics.DrawEllipse(_pen, rectangle);
                        break;
                    }
                case (int)GraphType.Line:
                    {
                        if (work.SolidBrush != null)
                            _pen.Color=work.SolidBrush.Color;
                        _graphics.DrawLine(_pen, work._startPoint, work._endPoint);
                        break;
                    }
            }
        }
        #endregion

        /// <summary>
        /// 用背景色擦除图像
        /// </summary>
        /// <param name="work">特定图像</param>
        /// <param name="color">背景色</param>
        public void EraseToBackColor(WorkProgram work, Color color)
        {
            switch (work._graphType)
            {
                case (int)GraphType.Rec:
                    {
                        Rectangle rectangle = new Rectangle(work._startPoint, new Size(work._width, work._height));
                        Pen pen = new Pen(color,work.Pen.Width);
                        _graphics.DrawRectangle(pen, rectangle);
                        break;
                    }
                case (int)GraphType.Oval:
                    {
                        Rectangle rectangle = new Rectangle(work._startPoint, new Size(work._width, work._height));
                        Pen pen = new Pen(color, work.Pen.Width);
                        _graphics.DrawEllipse(pen, rectangle);
                        break;
                    }
                case (int)GraphType.Line:
                    {
                        Pen pen = new Pen(color, work.Pen.Width);
                        _graphics.DrawLine(pen, work.StartPoint,work._endPoint);
                        break;
                    }
            }
        }

        /// <summary>
        /// 识别某个点是否在此图像内
        /// </summary>
        /// <param name="p">识别点</param>
        /// <returns></returns>
        public bool IdentifyContain(Point p)
        {
            bool is_true= false;
            switch (_graphType)
            {
                case (int)GraphType.Rec:
                    {
                        if((p.X>=_startPoint.X && p.X<=(_startPoint.X+_width))
                            && (p.Y>=_startPoint.Y && p.Y <= (_startPoint.Y + _height)))
                        {
                            is_true = true;
                        }
                        break;
                    }
                case (int)GraphType.Oval:
                    {
                        if(Math.Pow(p.X-_centralPoint.X,2)/Math.Pow(_width>>1,2)
                            + Math.Pow(p.Y - _centralPoint.Y, 2) / Math.Pow(_height >> 1, 2)
                            <= 1)
                        {
                            is_true=true;
                        }
                        break;
                    }
                case (int)GraphType.Line:
                    {
                        if (p.X > Math.Max(_startPoint.X, _endPoint.X) ||
                        p.X < Math.Min(_startPoint.X, _endPoint.X) ||
                        p.Y > Math.Max(_startPoint.Y, _endPoint.Y) ||
                        p.Y < Math.Min(_startPoint.Y, _endPoint.Y))  // 超过线段的界限
                        {
                            return false;
                        }
                        double dis = PointToLineDistance(p, _startPoint, _endPoint); // 线段判断距离
                        if (dis < 5)
                        {
                            return true;
                        }
                        return false;
                        break;
                    }
                default:
                    return false;
            }
            return is_true;
        }

        public static double PointToLineDistance(Point p, Point p1, Point p2)
        {
            double length = Math.Abs(
                    (p2.Y - p1.Y) * p.X + (p1.X - p2.X) * p.Y + (p2.X - p1.X) * p1.Y + (p1.Y - p2.Y) * p1.X) /
                Math.Sqrt((p2.Y - p1.Y) * (p2.Y - p1.Y) + (p1.X - p2.X) * (p1.X - p2.X));
            return length;
        }

        /// <summary>
        /// 绘制选中框
        /// </summary>
        /// <param name="work"></param>
        public void DrawSelected(WorkProgram work)
        {
            if (work.GraphType1 == (int)GraphType.Line)
            {
                return;
            }
            Pen pen = new Pen(Color.FromArgb(0, 120, 215), 1);
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            Rectangle rectangle = new Rectangle(work._startPoint, new Size((int)work._width, (int)work._height));
            _graphics.DrawRectangle(pen, rectangle);
        }

        
        public void DrawSelectedDot(WorkProgram work)
        {
            SolidBrush pen = new SolidBrush(Color.FromArgb(0, 120, 215));
            int weight = 10;
            Rectangle rectangle = new Rectangle(Point.Subtract(work._startPoint,new Size(weight>>1,weight>>1)), new Size(weight, weight));
            _graphics.FillEllipse(pen, rectangle);
            rectangle = new Rectangle(Point.Subtract(work._endPoint, new Size(weight >> 1, weight >> 1)), new Size(weight, weight));
            _graphics.FillEllipse(pen, rectangle);
            if(work.GraphType1 != (int)GraphType.Line)
            {
                rectangle = new Rectangle(work._startPoint.X+work.Width-weight/2,work._startPoint.Y-weight/2, weight, weight);
                _graphics.FillEllipse(pen, rectangle);
                rectangle = new Rectangle(work._startPoint.X - weight/2, work._startPoint.Y+work.Height - weight/2, weight, weight);
                _graphics.FillEllipse(pen, rectangle);
            }
        }


        public static explicit operator WorkProgram(int v)
        {
            throw new NotImplementedException();
        }

        public void RendColor(Color color)
        {
            // 创建一个Brush对象来定义填充样式  
            _solidBrush = new SolidBrush(color);

            switch (_graphType)
            {
                case (int)GraphType.Rec:
                    {
                        Rectangle rec = new Rectangle(_startPoint, new Size(_width, _height));
                        _graphics.FillRectangle(_solidBrush, rec);
                        break;
                    }
                case (int)GraphType.Oval:
                    {
                        Rectangle rec = new Rectangle(_startPoint, new Size(_width, _height));
                        _graphics.FillEllipse(_solidBrush, rec);
                        break;
                    }
                case (int)GraphType.Line:
                    {
                        Pen pen = new Pen(color, _pen.Width);
                        _graphics.DrawLine(pen, _startPoint, _endPoint);
                        break;
                    }
            }
        }
    }
}
