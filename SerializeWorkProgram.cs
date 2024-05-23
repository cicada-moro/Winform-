using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CourseSummaryDesign
{
    internal class SerializeWorkProgram
    {
        private Point _startPoint = new Point();
        private Point _endPoint = new Point();
        private Point _centralPoint = new Point();
        private int _width;
        private int _height;
        private int _radius;
        private int _penColor;
        private int _penWeight;
        private int _blushColor;
        private int _grapicsType;

        public SerializeWorkProgram()
        {

        }

        public SerializeWorkProgram(Point startPoint, Point endPoint, Point centralPoint, int width, int height, int radius, int penColor, int penWeight, int blushColor,int graghicType)
        {
            _startPoint = startPoint;
            _endPoint = endPoint;
            _centralPoint = centralPoint;
            _width = width;
            _height = height;
            _radius = radius;
            _penColor = penColor;
            _penWeight = penWeight;
            _blushColor = blushColor;
            _grapicsType = graghicType;
        }

        public SerializeWorkProgram(WorkProgram work)
        {
            _startPoint = work.StartPoint;
            _endPoint = work.EndPoint;
            _centralPoint = work.CentralPoint;
            _width = work.Width;
            _height = work.Height;
            _radius = work.Radius;
            if(work.Pen != null)
            {
                _penColor = work.Pen.Color.ToArgb();
                _penWeight = (int)work.Pen.Width;
            }
            if (work.SolidBrush != null)
            {
                _blushColor = work.SolidBrush.Color.ToArgb();
            }
            else
            {
                _blushColor = 0;
            }
            _grapicsType = work.GraphType1;
        }

        public Point StartPoint { get => _startPoint; set => _startPoint = value; }
        public Point EndPoint { get => _endPoint; set => _endPoint = value; }
        public Point CentralPoint { get => _centralPoint; set => _centralPoint = value; }
        public int Width { get => _width; set => _width = value; }
        public int Height { get => _height; set => _height = value; }
        public int Radius { get => _radius; set => _radius = value; }
        public int PenColor { get => _penColor; set => _penColor = value; }
        public int PenWeight { get => _penWeight; set => _penWeight = value; }
        public int BlushColor { get => _blushColor; set => _blushColor = value; }
        public int GrapicsType { get => _grapicsType; set => _grapicsType = value; }
    }
}
