using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing;
using System.Security.Cryptography;

namespace CourseSummaryDesign
{
    public partial class MainWindow : Form
    {
        //背景图
        private Image _orginBackImage;

        //主画布
        private Image _backBmpImage;
        private WorkProgram _backWorkProgram;

        //子画布
        private List<Image> _childrenBmpImages;
        private List<WorkProgram> _childrenWorkPrograms;

        //选中图形
        private WorkProgram _selectWorkProgram;
        private bool _selected = false;
        private int _selectedIndex=-1;

        //蒙布
        private PictureBox _maskPictureBox = new PictureBox();

        //双端队列存储过程
        private List<List<WorkProgram>> _workProgramQueueBack = new List<List<WorkProgram>>();
        private List<List<WorkProgram>> _workProgramQueueFront = new List<List<WorkProgram>>();
        private List<List<Image>> _bmpQueueBack = new List<List<Image>>();
        private List<List<Image>> _bmpQueueFront = new List<List<Image>>();

        //图形类型
        private enum GraphType {
            Oval=0,
            Line,
            Rec,
            Cusor
        }
        private int _graphType = (int)GraphType.Cusor;

        //深复制双端队列存储过程
        private static LinkedList<List<WorkProgram>> DeepCopyWorkProgramQueue(LinkedList<List<WorkProgram>> originalQueue)
        {
            LinkedList<List<WorkProgram>> copiedQueue = new LinkedList<List<WorkProgram>>();

            foreach (var workProgramList in originalQueue)
            {
                // 深复制List<WorkProgram>  
                List<WorkProgram> copiedWorkProgramList = new List<WorkProgram>(workProgramList.Count);
                foreach (var workProgram in workProgramList)
                {
                    copiedWorkProgramList.Add(new WorkProgram(workProgram));
                }
                copiedQueue.AddLast(copiedWorkProgramList);
            }

            return copiedQueue;
        }
        
        //深复制workprogram列表
        private static List<WorkProgram> DeepCopyWorkProgramList(List<WorkProgram> workProgramList)
        {
            List<WorkProgram> copiedWorkProgramList = new List<WorkProgram>(workProgramList.Count);
            foreach (var workProgram in workProgramList)
            {
                copiedWorkProgramList.Add(new WorkProgram(workProgram));
            }
            return copiedWorkProgramList;
        }

        //深复制image列表
        private static List<Image> DeepCopyImageList(List<Image> imageList)
        {
            List<Image> copiedImageList = new List<Image>(imageList.Count);
            foreach (var image in imageList)
            {
                copiedImageList.Add((Image)image.Clone());
            }
            return copiedImageList;
        }


        public MainWindow()
        {
            InitializeComponent();
            _backBmpImage = new Bitmap(this.DrawPanel.Width,this.DrawPanel.Height);
            this.DrawPanel.Image= _backBmpImage;
            Graphics graphics = Graphics.FromImage(_backBmpImage);
            _backWorkProgram = new WorkProgram();
            _backWorkProgram.Graphics = graphics;
            _backWorkProgram.Clear();

            _childrenBmpImages = new List<Image>();
            _childrenBmpImages.Clear();
            _childrenWorkPrograms = new List<WorkProgram>();
            _childrenWorkPrograms.Clear();

            _maskPictureBox.Parent = this.Panels;
            _maskPictureBox.Location = new Point(0, 0);
            _maskPictureBox.Size = new Size(this.DrawPanel.Width, this.DrawPanel.Height);
            
        }

        private void Init()
        {
            this.DrawPanel.Image = null;
            if(_backWorkProgram != null)
                _backWorkProgram.Clear();
            _childrenBmpImages.Clear();
            _childrenWorkPrograms.Clear();
            _workProgramQueueBack.Clear();
            _workProgramQueueFront.Clear();
            _bmpQueueBack.Clear();
            _bmpQueueFront.Clear();
            _backStep = _forwardStep = 0;
            _orginBackImage = null;
            _selected = false;
            _selectedIndex = -1;
            if(_selectWorkProgram != null)
                _selectWorkProgram.Clear();
        }

        private void RePaiting()
        {

            foreach (var item in _childrenWorkPrograms)
            {
                _backWorkProgram.DrawCopy(item, (int)item.GraphType1);
            }
            this.DrawPanel.Image = _backBmpImage;
        }

        #region 菜单栏
        private void NewFileToolTrip_Click(object sender, EventArgs e)
        {
            Init();
            this.DrawPanel.Image = _backBmpImage;

        }

        private void OpenFileToolTrip_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "图像文件 (*.jpg, *.jpeg,*.bmp *.png,*.gif) | *.jpg; *.jpeg; *.bmp; *.png";
            openFileDialog.DefaultExt = ".jpg";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Init();

                string filepath = openFileDialog.FileName;

                _orginBackImage = new Bitmap(this.DrawPanel.Width, this.DrawPanel.Height);
                using (Graphics g = Graphics.FromImage(_orginBackImage))
                {
                    g.DrawImage(Image.FromFile(filepath), 0, 0, this.DrawPanel.Width, this.DrawPanel.Height);

                }
                _backWorkProgram.SetBackImage(_orginBackImage);
                this.DrawPanel.Image=_backBmpImage;
            }
        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //备份...

            Image new_orginBackImage=null;
            Image new_backBmpImage=null;
            WorkProgram new_backWorkProgram=null;
            List<Image> new_childrenBmpImages=new List<Image>();
            List<WorkProgram> new_childrenWorkPrograms=new List<WorkProgram>();

            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.SelectedPath = System.AppDomain.CurrentDomain.BaseDirectory;

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                List<string> supportedExtensions = new List<string> { ".jpg", ".jpeg", ".png", ".bmp" };

                string folderPath = folderBrowserDialog.SelectedPath;
                string[] fileEntries = Directory.GetFiles(folderPath);
                try
                {
                    for (int i = 0; i < fileEntries.Length; i++)
                    {
                        string fileName = fileEntries[i];
                        string extension = Path.GetExtension(fileName).ToLower();

                        WorkProgram workProgram = new WorkProgram();

                        if (supportedExtensions.Contains(extension))
                        {

                            Image image = Image.FromFile(fileName);
                            if (i == 0)
                            {
                                new_orginBackImage = (Image)image.Clone();
                                new_backBmpImage = new Bitmap(this.DrawPanel.Width, this.DrawPanel.Height);
                                Graphics graphics = Graphics.FromImage(new_backBmpImage);
                                graphics.DrawImage(image,0,0);
                                new_backWorkProgram = new WorkProgram();
                                new_backWorkProgram.Graphics = graphics;
                                new_backWorkProgram.Clear();
                                new_backWorkProgram.SetBackImage(new_orginBackImage);
                                this.DrawPanel.Image = new_backBmpImage;
                            }
                            else
                            {
                                Image child_image = (Image)image.Clone();
                                new_childrenBmpImages.Add(child_image);
                                using (Graphics g = Graphics.FromImage(child_image))
                                {
                                    workProgram.Graphics = g;
                                }
                            }
                        }
                        else if(extension == ".json")
                        {
                            SerializeWorkProgram program=JsonUtils.DeserializeFromFile<SerializeWorkProgram>(fileName);
                            workProgram.Width = program.Width;
                            workProgram.Height = program.Height;
                            workProgram.Radius = program.Radius;
                            workProgram.StartPoint = program.StartPoint;
                            workProgram.EndPoint = program.EndPoint;
                            workProgram.CentralPoint = program.CentralPoint;
                            Pen p = new Pen(Color.FromArgb(program.PenColor), program.PenWeight);
                            workProgram.Pen = p;
                            if (program.BlushColor != 0)
                            {
                                SolidBrush brush = new SolidBrush(Color.FromArgb(program.BlushColor));
                                workProgram.SolidBrush = brush;
                            }
                            else
                            {
                                workProgram.SolidBrush = null;
                            }
                            workProgram.GraphType1 = program.GrapicsType;

                            new_childrenWorkPrograms.Add(workProgram);
                        }
                    }
                    Init();
                    _backBmpImage = new_backBmpImage;
                    _backWorkProgram = new_backWorkProgram;
                    _orginBackImage = (Image)new_orginBackImage.Clone();
                    _childrenBmpImages = DeepCopyImageList(new_childrenBmpImages);
                    _childrenWorkPrograms = DeepCopyWorkProgramList(new_childrenWorkPrograms);
                }
                catch (Exception ex)
                {

                    MessageBox.Show("加载失败!", "Error");
                    return;
                }
                RePaiting();
            }
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowNewFolderButton = true;
            folderBrowserDialog.SelectedPath = System.AppDomain.CurrentDomain.BaseDirectory;  
  
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)  
            {  
                string parentFolderPath = folderBrowserDialog.SelectedPath;

                // 构建新文件夹的完整路径  
                Random rd = new Random();
                int rand_num = rd.Next();
                string newFolderPath = Path.Combine(parentFolderPath, $"image{rand_num}");

                // 设置保存文件名  
                while (Directory.Exists(newFolderPath))
                {
                    rand_num = rd.Next();
                    newFolderPath = Path.Combine(parentFolderPath, $"image{rand_num}");
                }
                Directory.CreateDirectory(newFolderPath);

                //保存失败抛出异常
                try
                {
                    ImageFormat format = ImageFormat.Jpeg;
                    string fileName = Path.Combine(newFolderPath, $"image{0}.{"jpg".ToLower()}");
                    Image image = (Image)_backBmpImage.Clone();
                    using(Graphics g= Graphics.FromImage(image))
                    {
                        g.Clear(this.DrawPanel.BackColor);

                        if (_orginBackImage != null)
                        {
                            g.DrawImage(_orginBackImage, new Point(0, 0));
                        }
                    }
                    image.Save(fileName, format);


                    // 遍历图像列表  
                    for (int i = 0; i < _childrenBmpImages.Count; i++)
                    {
                        // 构建图片名（默认jpg格式）  
                        fileName = Path.Combine(newFolderPath, $"image{i + 1}.{"jpg".ToLower()}");
                        _childrenBmpImages[i].Save(fileName, format);
                        //Json序列化图片参数
                        fileName = Path.Combine(newFolderPath, $"image{i + 1}.{"json".ToLower()}");
                        SerializeWorkProgram serializeWork = new SerializeWorkProgram(_childrenWorkPrograms[i]);
                        JsonUtils.SerializeToFile(serializeWork, fileName);
                    }


                    MessageBox.Show("保存成功!", "Success");
                }
                catch (Exception)
                {
                    Directory.Delete(newFolderPath, true);
                    MessageBox.Show("保存失败!", "Error");
                }
            }  
            
        }


        private void SaveFileToolTrip_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "图像文件 (*.jpg, *.jpeg,*.bmp *.png,*.gif) | *.jpg; *.jpeg; *.bmp; *.png";
            saveFileDialog.DefaultExt = ".jpg";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filepath = saveFileDialog.FileName;
                ImageFormat image_format;
                switch (Path.GetExtension(filepath).ToLower())
                {
                    case ".jpg":
                    case ".jpeg":
                        image_format = ImageFormat.Jpeg;
                        break;
                    case ".png":
                        image_format = ImageFormat.Png;
                        break;
                    case ".bmp":
                        image_format = ImageFormat.Bmp;
                        break;
                    case ".gif":
                        image_format = ImageFormat.Gif;
                        break;
                    default:
                        // 如果不是已知的格式，则使用默认的JPEG格式  
                        image_format = ImageFormat.Jpeg;
                        break;
                }
                _backWorkProgram.Clear();
                _backWorkProgram.SetBackImage(_orginBackImage);
                RePaiting();
                _backBmpImage.Save(filepath, image_format);

            }
        }

        #endregion

        private void Oval_Click(object sender, EventArgs e)
        {
            _graphType = (int)GraphType.Oval;
            this.Oval.BackColor=Color.WhiteSmoke;
            this.Rectangle.BackColor = Color.Gainsboro;
            this.Line.BackColor = Color.Gainsboro;
            this.CommonCusor.BackColor = Color.Gainsboro;
        }

        private void Line_Click(object sender, EventArgs e)
        {
            _graphType = (int)GraphType.Line;
            this.Oval.BackColor = Color.Gainsboro;
            this.Rectangle.BackColor = Color.Gainsboro;
            this.Line.BackColor = Color.WhiteSmoke;
            this.CommonCusor.BackColor = Color.Gainsboro;

        }

        private void Rectangle_Click(object sender, EventArgs e)
        {
            _graphType = (int)GraphType.Rec;
            this.Oval.BackColor = Color.Gainsboro;
            this.Rectangle.BackColor = Color.WhiteSmoke;
            this.Line.BackColor = Color.Gainsboro;
            this.CommonCusor.BackColor = Color.Gainsboro;

        }

        private void CommonCusor_Click(object sender, EventArgs e)
        {
            _graphType = (int)GraphType.Cusor;
            this.Oval.BackColor = Color.Gainsboro;
            this.Rectangle.BackColor = Color.Gainsboro;
            this.Line.BackColor = Color.Gainsboro;
            this.CommonCusor.BackColor = Color.WhiteSmoke;
        }

        #region 重写Panel鼠标事件

        private bool mouse_isdowm = false;
        private bool _isDrawing = false;
        private Point _startPoint;
        //缩放类型
        private enum ResizeDragType
        {
            LeftTop=0,
            RightTop,
            LeftBottom,
            RightBottom
        }
        private ResizeDragType _resizeDragType;

        private void DrawPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouse_isdowm = true;
                _startPoint= e.Location;
                if(_graphType != (int)GraphType.Cusor)
                {
                    _backWorkProgram.Clear();
                    _backWorkProgram.SetBackImage(_orginBackImage);

                    RePaiting();

                    _selected = false;

                    this.Cursor = Cursors.Cross;

                    Image image = new Bitmap(this.DrawPanel.Width, this.DrawPanel.Height);
                    WorkProgram workProgram = new WorkProgram();
                    Graphics g = Graphics.FromImage(image);

                    /*PictureBox pictureBox = new PictureBox();
                    pictureBox.Image = image;
                    pictureBox.Location=PointToClient(pictureBox.Location);
                    pictureBox.Size = new Size(this.DrawPanel.Width, this.DrawPanel.Height);
                    this.Panels.Controls.Add(pictureBox);
                    pictureBox.BringToFront();*/

                    workProgram.Graphics = g;
                    workProgram.StartPoint = e.Location;
                    workProgram.EndPoint = e.Location;
                    workProgram.GraphType1 = _graphType;


                    _childrenBmpImages.Add(image);
                    _childrenWorkPrograms.Add(workProgram);

                }
                else
                {
                    //选中图像...
                    for(int i = _childrenWorkPrograms.Count-1; i >=0; i--)
                    {
                        if (_childrenWorkPrograms[i].IdentifyContain(e.Location))
                        {
                            _selectWorkProgram = _childrenWorkPrograms[i];
                            _selected = true;
                            _selectedIndex = i;

                            this.Cursor = Cursors.SizeAll;

                            _backWorkProgram.Clear();
                            _backWorkProgram.SetBackImage(_orginBackImage);

                            RePaiting();

                            _backWorkProgram.DrawSelected(_selectWorkProgram);
                            _backWorkProgram.DrawSelectedDot(_selectWorkProgram);

                            this.DrawPanel.Image = _backBmpImage;



                            this.Properites.Text = "位置:" + _selectWorkProgram.StartPoint.ToString()
                                + "\n形状:";
                            switch (_selectWorkProgram.GraphType1)
                            {
                                case (int)GraphType.Rec:
                                    this.Properites.Text += "矩形";
                                    break;
                                case (int)GraphType.Oval:
                                    this.Properites.Text += "椭圆";
                                    break;
                                case (int)GraphType.Line:
                                    this.Properites.Text += "直线";
                                    break;
                            }

                            break;
                        }
                        if(i == 0)
                        {
                            _backWorkProgram.Clear();
                            _backWorkProgram.SetBackImage(_orginBackImage);
                            RePaiting();
                            if (this.Cursor != Cursors.SizeAll
                                && this.Cursor != Cursors.Default)
                            {
                                _backWorkProgram.DrawSelected(_selectWorkProgram);
                                _backWorkProgram.DrawSelectedDot(_selectWorkProgram);
                                _selected = true;
                            }
                            else
                                _selected = false;
                        }
                    }
   
                }
            }
        }

        private void DrawPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (mouse_isdowm)
                {
                    switch (_graphType)
                    {
                        case (int)GraphType.Rec:
                            {
                                if (_childrenWorkPrograms.Count > 0)
                                {
                                    _isDrawing = true;

/*                                    _backWorkProgram.EraseToBackColor(_childrenWorkPrograms[_childrenWorkPrograms.Count - 1], this.DrawPanel.BackColor);
*/                                    _childrenWorkPrograms[_childrenWorkPrograms.Count-1].DrawRectangle(e.Location);
                                    _backWorkProgram.Clear();
                                    _backWorkProgram.SetBackImage(_orginBackImage);
                                    this.DrawPanel.Image = _backBmpImage;

                                    _backWorkProgram.DrawCopy(_childrenWorkPrograms[_childrenWorkPrograms.Count - 1],
                                        _childrenWorkPrograms[_childrenWorkPrograms.Count - 1].GraphType1);
                                    foreach (var item in _childrenWorkPrograms)
                                    {
                                        if (item != _childrenWorkPrograms[_childrenWorkPrograms.Count - 1])
                                            _backWorkProgram.DrawCopy(item, (int)item.GraphType1);
                                    }
                                    this.DrawPanel.Image = _backBmpImage;


/*                                    _maskPictureBox.BringToFront();
                                    Rectangle rec = new Rectangle(_childrenWorkPrograms[_childrenWorkPrograms.Count - 1].StartPoint,
                                        new Size(_childrenWorkPrograms[_childrenWorkPrograms.Count - 1].Width,
                                        _childrenWorkPrograms[_childrenWorkPrograms.Count - 1].Height));
                                    _maskPictureBox.Location = rec.Location;
                                    _maskPictureBox.Size = rec.Size;

                                    Image crop_image = CropImage(_childrenBmpImages[_childrenBmpImages.Count - 1], rec);
                                    _maskPictureBox.Image = crop_image;*/

                                }
                                break;
                            }
                        case (int)GraphType.Oval:
                            {
                                if (_childrenWorkPrograms.Count > 0)
                                {
                                    _isDrawing = true;

                                    _backWorkProgram.EraseToBackColor(_childrenWorkPrograms[_childrenWorkPrograms.Count - 1], this.DrawPanel.BackColor);
                                    _childrenWorkPrograms[_childrenWorkPrograms.Count - 1].DrawOval(e.Location);
                                    _backWorkProgram.DrawCopy(_childrenWorkPrograms[_childrenWorkPrograms.Count - 1],
                                        _childrenWorkPrograms[_childrenWorkPrograms.Count - 1].GraphType1);
                                    foreach (var item in _childrenWorkPrograms)
                                    {
                                        if(item != _childrenWorkPrograms[_childrenWorkPrograms.Count-1])
                                            _backWorkProgram.DrawCopy(item, (int)item.GraphType1);
                                    }
                                    this.DrawPanel.Image = _backBmpImage;

                                }
                                break;
                            }
                        case (int)GraphType.Line:
                            {
                                if (_childrenWorkPrograms.Count > 0)
                                {
                                    _isDrawing = true;

                                    _backWorkProgram.EraseToBackColor(_childrenWorkPrograms[_childrenWorkPrograms.Count - 1], this.DrawPanel.BackColor);
                                    _childrenWorkPrograms[_childrenWorkPrograms.Count - 1].DrawLine(e.Location);
                                    _backWorkProgram.DrawCopy(_childrenWorkPrograms[_childrenWorkPrograms.Count - 1],
                                        _childrenWorkPrograms[_childrenWorkPrograms.Count - 1].GraphType1);
                                    foreach (var item in _childrenWorkPrograms)
                                    {
                                        if (item != _childrenWorkPrograms[_childrenWorkPrograms.Count - 1])
                                            _backWorkProgram.DrawCopy(item, (int)item.GraphType1);
                                    }
                                    this.DrawPanel.Image = _backBmpImage;

                                }
                                break;
                            }

                        case (int)GraphType.Cusor:
                            {
                                if (_selected)
                                {
                                    _isDrawing = true;

                                    _backWorkProgram.Clear();
                                    _backWorkProgram.SetBackImage(_orginBackImage);
                                    if (this.Cursor == Cursors.SizeAll)
                                    {

                                        Point diff = Point.Subtract(e.Location, new Size(_startPoint));
                                        _childrenWorkPrograms[_selectedIndex].StartPoint = Point.Add(_childrenWorkPrograms[_selectedIndex].StartPoint, new Size(diff));
                                        _childrenWorkPrograms[_selectedIndex].EndPoint = Point.Add(_childrenWorkPrograms[_selectedIndex].EndPoint, new Size(diff));
                                        _childrenWorkPrograms[_selectedIndex].CentralPoint = Point.Add(_childrenWorkPrograms[_selectedIndex].CentralPoint, new Size(diff));

                                    }
                                    else
                                    {
                                        int boundary = 6;
                                        Point diff = Point.Subtract(e.Location, new Size(_startPoint));

                                        if (this.Cursor == Cursors.SizeNWSE)
                                        {
                                            if (_resizeDragType == ResizeDragType.LeftTop)
                                            {
                                                if(_selectWorkProgram.GraphType1 != (int)GraphType.Line)
                                                {
                                                    //超出边界
                                                    if (_childrenWorkPrograms[_selectedIndex].Width - diff.X <= boundary
                                                        || _childrenWorkPrograms[_selectedIndex].Height - diff.Y <= boundary)
                                                    {
                                                        return;
                                                    }
                                                    _childrenWorkPrograms[_selectedIndex].StartPoint = Point.Add(_childrenWorkPrograms[_selectedIndex].StartPoint, new Size(diff));

                                                    _childrenWorkPrograms[_selectedIndex].Width -= diff.X;
                                                    _childrenWorkPrograms[_selectedIndex].Height -= diff.Y;

                                                    _childrenWorkPrograms[_selectedIndex].CentralPoint = Point.Add(_childrenWorkPrograms[_selectedIndex].StartPoint,
                                                        new Size(_childrenWorkPrograms[_selectedIndex].Width >> 1, _childrenWorkPrograms[_selectedIndex].Height >> 1));
                                                }
                                                else
                                                {
                                                    _childrenWorkPrograms[_selectedIndex].StartPoint = Point.Add(_childrenWorkPrograms[_selectedIndex].StartPoint, new Size(diff));


                                                    _childrenWorkPrograms[_selectedIndex].CentralPoint = new Point((_childrenWorkPrograms[_selectedIndex].EndPoint.X+ _childrenWorkPrograms[_selectedIndex].StartPoint.X)/2,
                                                        (_childrenWorkPrograms[_selectedIndex].EndPoint.Y + _childrenWorkPrograms[_selectedIndex].StartPoint.Y) / 2);
                                                }

                                            }
                                            else if(_resizeDragType == ResizeDragType.RightBottom)
                                            {
                                                if (_selectWorkProgram.GraphType1 != (int)GraphType.Line)
                                                {
                                                    //超出边界
                                                    if (_childrenWorkPrograms[_selectedIndex].Width + diff.X <= boundary
                                                        || _childrenWorkPrograms[_selectedIndex].Height + diff.Y <= boundary)
                                                    {
                                                        return;
                                                    }
                                                    _childrenWorkPrograms[_selectedIndex].EndPoint = Point.Add(_childrenWorkPrograms[_selectedIndex].EndPoint, new Size(diff));

                                                    _childrenWorkPrograms[_selectedIndex].Width += diff.X;
                                                    _childrenWorkPrograms[_selectedIndex].Height += diff.Y;

                                                    _childrenWorkPrograms[_selectedIndex].CentralPoint = Point.Add(_childrenWorkPrograms[_selectedIndex].StartPoint,
                                                        new Size(_childrenWorkPrograms[_selectedIndex].Width >> 1, _childrenWorkPrograms[_selectedIndex].Height >> 1));
                                                }
                                                else
                                                {
                                                    _childrenWorkPrograms[_selectedIndex].EndPoint = Point.Add(_childrenWorkPrograms[_selectedIndex].EndPoint, new Size(diff));
                                                    _childrenWorkPrograms[_selectedIndex].CentralPoint = new Point((_childrenWorkPrograms[_selectedIndex].EndPoint.X + _childrenWorkPrograms[_selectedIndex].StartPoint.X) / 2,
                                                        (_childrenWorkPrograms[_selectedIndex].EndPoint.Y + _childrenWorkPrograms[_selectedIndex].StartPoint.Y) / 2);

                                                }
                                            }
                                        }
                                        if(this.Cursor == Cursors.SizeNESW)
                                        {
                                            if(_selectWorkProgram.GraphType1 == (int)GraphType.Line)
                                            {
                                                return;
                                            }
                                            else
                                            {
                                                if (_resizeDragType == ResizeDragType.RightTop)
                                                {
                                                    //超出边界
                                                    if (_childrenWorkPrograms[_selectedIndex].Width + diff.X <= boundary
                                                        || _childrenWorkPrograms[_selectedIndex].Height - diff.Y <= boundary)
                                                    {
                                                        return;
                                                    }
                                                    _childrenWorkPrograms[_selectedIndex].StartPoint = new Point(_childrenWorkPrograms[_selectedIndex].StartPoint.X,
                                                        _childrenWorkPrograms[_selectedIndex].StartPoint.Y + diff.Y);
                                                    _childrenWorkPrograms[_selectedIndex].EndPoint = new Point(_childrenWorkPrograms[_selectedIndex].EndPoint.X + diff.X,
                                                        _childrenWorkPrograms[_selectedIndex].EndPoint.Y);

                                                    _childrenWorkPrograms[_selectedIndex].Width += diff.X;
                                                    _childrenWorkPrograms[_selectedIndex].Height -= diff.Y;

                                                    _childrenWorkPrograms[_selectedIndex].CentralPoint = Point.Add(_childrenWorkPrograms[_selectedIndex].StartPoint,
                                                        new Size(_childrenWorkPrograms[_selectedIndex].Width >> 1, _childrenWorkPrograms[_selectedIndex].Height >> 1));
                                                }
                                                else if(_resizeDragType == ResizeDragType.LeftBottom)
                                                {
                                                    //超出边界
                                                    if (_childrenWorkPrograms[_selectedIndex].Width - diff.X <= boundary
                                                        || _childrenWorkPrograms[_selectedIndex].Height + diff.Y <= boundary)
                                                    {
                                                        return;
                                                    }
                                                    _childrenWorkPrograms[_selectedIndex].StartPoint = new Point(_childrenWorkPrograms[_selectedIndex].StartPoint.X + diff.X,
                                                        _childrenWorkPrograms[_selectedIndex].StartPoint.Y);
                                                    _childrenWorkPrograms[_selectedIndex].EndPoint = new Point(_childrenWorkPrograms[_selectedIndex].EndPoint.X,
                                                        _childrenWorkPrograms[_selectedIndex].EndPoint.Y + diff.Y);

                                                    _childrenWorkPrograms[_selectedIndex].Width -= diff.X;
                                                    _childrenWorkPrograms[_selectedIndex].Height += diff.Y;

                                                    _childrenWorkPrograms[_selectedIndex].CentralPoint = Point.Add(_childrenWorkPrograms[_selectedIndex].StartPoint,
                                                        new Size(_childrenWorkPrograms[_selectedIndex].Width >> 1, _childrenWorkPrograms[_selectedIndex].Height >> 1));
                                                }
                                            }
                                        }

                                    }
                                    _selectWorkProgram = _childrenWorkPrograms[_selectedIndex];
                                    _startPoint = new Point(e.Location.X, e.Location.Y);

                                    RePaiting();
                                    _backWorkProgram.DrawSelected(_selectWorkProgram);
                                    _backWorkProgram.DrawSelectedDot(_selectWorkProgram);
                                    this.DrawPanel.Image = _backBmpImage;
                                }
                                break;
                            }
                    }
                }
            }
            else
            {
                if (!mouse_isdowm && _selected)
                {
                    int boundary = 6; // 边界

                    if (Math.Abs(e.X - _selectWorkProgram.StartPoint.X) < boundary
                        && Math.Abs(e.Y - _selectWorkProgram.StartPoint.Y) < boundary)//左上
                    {
                        this.Cursor = Cursors.SizeNWSE;
                        _resizeDragType = ResizeDragType.LeftTop;
                        return;
                    }
                    else if (Math.Abs(e.X - _selectWorkProgram.EndPoint.X) < boundary
                        && Math.Abs(e.Y - _selectWorkProgram.EndPoint.Y) < boundary)//右下
                    {
                        this.Cursor = Cursors.SizeNWSE;
                        _resizeDragType = ResizeDragType.RightBottom;
                        return;
                    }
                    if (_selectWorkProgram.GraphType1 != (int)GraphType.Line)
                    {
                        if(Math.Abs(e.X - _selectWorkProgram.StartPoint.X) < boundary
                            && Math.Abs(e.Y - _selectWorkProgram.StartPoint.Y - _selectWorkProgram.Height) < boundary)//左下
                        {
                            this.Cursor = Cursors.SizeNESW;
                            _resizeDragType = ResizeDragType.LeftBottom;
                            return;
                        }
                        else if(Math.Abs(e.X - _selectWorkProgram.StartPoint.X - _selectWorkProgram.Width) < boundary
                            && Math.Abs(e.Y - _selectWorkProgram.StartPoint.Y) < boundary)//右上
                        {
                            this.Cursor = Cursors.SizeNESW;
                            _resizeDragType = ResizeDragType.RightTop;
                            return;
                        }
                    }
                    this.Cursor = Cursors.Default;
                }
            }
        }

        private void DrawPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (mouse_isdowm)
            {
                _maskPictureBox.SendToBack();
                if (_isDrawing)
                {
                    RecodingProgress(); 
                    _isDrawing = false;
                }

/*                _backWorkProgram.SetBackImage(_orginBackImage);
                RePaiting();*/
                mouse_isdowm = false;
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        public Bitmap CropImage(Image img, Rectangle cropArea)
        {
            // 创建一个新的 Bitmap 对象，大小和裁剪区域一样  
            if(cropArea.Width<=0 || cropArea.Height <= 0)
            {
                cropArea.Width += 1;
                cropArea.Height += 1;
            }
            Bitmap bmp = new Bitmap(cropArea.Width, cropArea.Height);

            // 创建一个 Graphics 对象，该对象可以在 bmp 上绘图  
            Graphics g = Graphics.FromImage(bmp);

            // 绘制原始图像的裁剪区域到新的 Bitmap 上  
            g.DrawImage(img, new Rectangle(0, 0, bmp.Width, bmp.Height),
                        cropArea,
                        GraphicsUnit.Pixel);

            // 释放 Graphics 对象  
            g.Dispose();

            // 返回裁剪后的图像  
            return bmp;
        }


        #region 颜色渲染
        private void RedButton_Click(object sender, EventArgs e)
        {
            if (_selected)
            {
                _childrenWorkPrograms[_selectedIndex].RendColor(Color.Red);
                _backWorkProgram.DrawCopy(_childrenWorkPrograms[_selectedIndex]
                    , (int)_childrenWorkPrograms[_selectedIndex].GraphType1);
                this.DrawPanel.Image = _backBmpImage;

                RecodingProgress();
            }
        }

        private void BlueButton_Click(object sender, EventArgs e)
        {
            if (_selected)
            {
                _childrenWorkPrograms[_selectedIndex].RendColor(Color.Blue);
                _backWorkProgram.DrawCopy(_childrenWorkPrograms[_selectedIndex]
                    , (int)_childrenWorkPrograms[_selectedIndex].GraphType1);
                this.DrawPanel.Image = _backBmpImage;

                RecodingProgress();
            }
        }

        private void GreenButton_Click(object sender, EventArgs e)
        {
            if (_selected)
            {
                _childrenWorkPrograms[_selectedIndex].RendColor(Color.Green);
                _backWorkProgram.DrawCopy(_childrenWorkPrograms[_selectedIndex]
                    , (int)_childrenWorkPrograms[_selectedIndex].GraphType1);
                this.DrawPanel.Image = _backBmpImage;

                RecodingProgress();
            }
        }

        private void MoreColorButton_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                Color color = colorDialog1.Color;
                if (_selected)
                {
                    _childrenWorkPrograms[_selectedIndex].RendColor(color);
                    _backWorkProgram.DrawCopy(_childrenWorkPrograms[_selectedIndex]
                        , (int)_childrenWorkPrograms[_selectedIndex].GraphType1);
                    this.DrawPanel.Image = _backBmpImage;

                    RecodingProgress();
                }

            }

        }

        #endregion


        private int _forwardStep=0;
        private int _backStep=0;
        #region 回退和前进和删除
        private void RecodingProgress()
        {
            if(_backStep != 0)
            {
                _backStep = 0;
                _workProgramQueueBack.Clear();
                _bmpQueueBack.Clear();
                _forwardStep = _childrenWorkPrograms.Count-1;
            }
            _workProgramQueueFront.Add(DeepCopyWorkProgramList(_childrenWorkPrograms));
            _bmpQueueFront.Add(DeepCopyImageList(_childrenBmpImages));
            _forwardStep++;
        }

        private void BackBotton_Click(object sender, EventArgs e)
        {
            if (_forwardStep == _backStep)
                return;
            if ((_forwardStep - _backStep) > 1)
            {
                List<WorkProgram> pop_list = _workProgramQueueFront.Last();
                _workProgramQueueBack.Add(DeepCopyWorkProgramList(pop_list));
                _workProgramQueueFront.RemoveAt(_workProgramQueueFront.Count - 1);

                List<Image> pop_list1 = _bmpQueueFront.Last();
                _bmpQueueBack.Add(DeepCopyImageList(pop_list1));
                _bmpQueueFront.RemoveAt(_bmpQueueFront.Count - 1);

                _childrenWorkPrograms = DeepCopyWorkProgramList(_workProgramQueueFront.Last());
                _childrenBmpImages = DeepCopyImageList(_bmpQueueFront.Last());
                _backStep++;

                _backWorkProgram.Clear();
                _backWorkProgram.SetBackImage(_orginBackImage);

                RePaiting();    
            }
            else
            {
                _workProgramQueueBack.Add(DeepCopyWorkProgramList(_childrenWorkPrograms));
                _bmpQueueBack.Add(DeepCopyImageList(_childrenBmpImages));
                _backStep++;

                _backWorkProgram.Clear();
                _backWorkProgram.SetBackImage(_orginBackImage);

                if (_childrenWorkPrograms.Count > 1)
                {
                    _childrenWorkPrograms.RemoveAt(_childrenWorkPrograms.Count - 1);
                    _childrenBmpImages.RemoveAt(_childrenBmpImages.Count - 1);

                    RePaiting();
                }

                this.DrawPanel.Image = _backBmpImage;
            }
        }

        private void ForwardButton_Click(object sender, EventArgs e)
        {
            if(_backStep > 0 &&_bmpQueueBack.Count>0)
            {
                _childrenWorkPrograms = DeepCopyWorkProgramList(_workProgramQueueBack.Last());
                _childrenBmpImages = DeepCopyImageList(_bmpQueueBack.Last());

                List<WorkProgram> pop_list = _workProgramQueueBack.Last();
                _workProgramQueueFront.Add(DeepCopyWorkProgramList(pop_list));
                _workProgramQueueBack.RemoveAt(_workProgramQueueBack.Count - 1);

                List<Image> pop_list1 = _bmpQueueBack.Last();
                _bmpQueueFront.Add(DeepCopyImageList(pop_list1));
                _bmpQueueBack.RemoveAt(_bmpQueueBack.Count - 1);

                _backStep--;
                _backWorkProgram.Clear();
                _backWorkProgram.SetBackImage(_orginBackImage);

                RePaiting();
            }
        }

        private void Eraser_Click(object sender, EventArgs e)
        {
            if (_selected)
            {
                _childrenBmpImages.RemoveAt(_selectedIndex);
                _childrenWorkPrograms.RemoveAt(_selectedIndex);
                _selectWorkProgram = null;
                _selectedIndex= -1;
                _selected = false;

                RecodingProgress();

                _backWorkProgram.Clear();
                _backWorkProgram.SetBackImage(_orginBackImage);
                RePaiting();
            }
        }
        #endregion

        #region 图层移动
        /// <summary>
        /// 右键菜单打开时打开特定功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_selected)
            {
                if(_selectedIndex != _childrenWorkPrograms.Count-1)
                {
                    上移ToolStripMenuItem.Enabled = true;
                    移到最上层ToolStripMenuItem.Enabled = true;
                }
                else
                {
                    上移ToolStripMenuItem.Enabled = false;
                    移到最上层ToolStripMenuItem.Enabled = false;
                }
                if(_selectedIndex != 0)
                {
                    下移ToolStripMenuItem.Enabled = true;
                    移到最底层ToolStripMenuItem.Enabled = true;
                }
                else
                {
                    下移ToolStripMenuItem.Enabled = false;
                    移到最底层ToolStripMenuItem.Enabled = false;
                }

            }
            else
            {
                上移ToolStripMenuItem.Enabled = false;
                下移ToolStripMenuItem.Enabled = false;
                移到最底层ToolStripMenuItem.Enabled = false;
                移到最上层ToolStripMenuItem.Enabled = false;
            }
        }

        private void 上移ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WorkProgram move_program = _childrenWorkPrograms[_selectedIndex];
            _childrenWorkPrograms[_selectedIndex] = _childrenWorkPrograms[_selectedIndex + 1];
            _childrenWorkPrograms[_selectedIndex + 1] = move_program;
            RecodingProgress();


            Image move_image = _childrenBmpImages[_selectedIndex];
            _childrenBmpImages[_selectedIndex] = _childrenBmpImages[_selectedIndex + 1];
            _childrenBmpImages[_selectedIndex + 1] = move_image;

            _selectedIndex += 1;

            _backWorkProgram.Clear();
            _backWorkProgram.SetBackImage(_orginBackImage);
            RePaiting();
        }

        private void 下移ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WorkProgram move_program = _childrenWorkPrograms[_selectedIndex];
            _childrenWorkPrograms.RemoveAt(_selectedIndex);
            _childrenWorkPrograms.Insert(_selectedIndex - 1, move_program);
            RecodingProgress();

            Image move_image = _childrenBmpImages[_selectedIndex];
            _childrenBmpImages.RemoveAt(_selectedIndex);
            _childrenBmpImages.Insert(_selectedIndex - 1, move_image);

            _selectedIndex -= 1;

            _backWorkProgram.Clear();
            _backWorkProgram.SetBackImage(_orginBackImage);
            RePaiting();
        }

        private void 移到最底层ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WorkProgram move_program = _childrenWorkPrograms[_selectedIndex];
            _childrenWorkPrograms.RemoveAt(_selectedIndex);
            _childrenWorkPrograms.Insert(0, move_program);
            RecodingProgress();


            Image move_image = _childrenBmpImages[_selectedIndex];
            _childrenBmpImages.RemoveAt(_selectedIndex);
            _childrenBmpImages.Insert(0, move_image);

            _selectedIndex =0;

            _backWorkProgram.Clear();
            _backWorkProgram.SetBackImage(_orginBackImage);
            RePaiting();
        }

        private void 移到最上层ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WorkProgram move_program = _childrenWorkPrograms[_selectedIndex];
            _childrenWorkPrograms.RemoveAt(_selectedIndex);
            _childrenWorkPrograms.Add(move_program);
            RecodingProgress();

            Image move_image = _childrenBmpImages[_selectedIndex];
            _childrenBmpImages.RemoveAt(_selectedIndex);
            _childrenBmpImages.Add(move_image);

            _selectedIndex = _childrenWorkPrograms.Count-1;

            _backWorkProgram.Clear();
            _backWorkProgram.SetBackImage(_orginBackImage);
            RePaiting();
        }
        #endregion


        private void DrawPanel_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                return;
            }

            //定义缩放因子
            int x_multi = this.DrawPanel.Width/_backBmpImage.Width;
            int y_multi = this.DrawPanel.Height/_backBmpImage.Height;

            Image image = new Bitmap(this.DrawPanel.Width,this.DrawPanel.Height);
            Graphics g = Graphics.FromImage(image);

            g.DrawImage(_backBmpImage, 0, 0);
            _backBmpImage = (Image)image.Clone();

            g.Clear(Color.White);
            if (_orginBackImage != null)
            {
                g.DrawImage(_orginBackImage, 0, 0);
                _orginBackImage = (Image)image.Clone();
            }

            Graphics back_g = Graphics.FromImage(_backBmpImage);
            _backWorkProgram.Graphics = back_g;


            for (int i = 0; i < _childrenWorkPrograms.Count; i++)
            {
                g.Clear(Color.White);
                g.DrawImage(_childrenBmpImages[i], 0, 0);
                _childrenBmpImages[i] = (Image)image.Clone();

                Graphics child_g = Graphics.FromImage(_childrenBmpImages[i]);
                _childrenWorkPrograms[i].Graphics = child_g;
               /* _childrenWorkPrograms[i].Width *= x_multi;
                _childrenWorkPrograms[i].Height *= y_multi;
                _childrenWorkPrograms[i].Radius *= x_multi;
                Point point = _childrenWorkPrograms[i].StartPoint;
                _childrenWorkPrograms[i].StartPoint = new Point(point.X *= x_multi, point.Y *= y_multi);
                point = _childrenWorkPrograms[i].EndPoint;
                _childrenWorkPrograms[i].EndPoint = new Point(point.X *= x_multi, point.Y *= y_multi);
                point = _childrenWorkPrograms[i].CentralPoint;
                _childrenWorkPrograms[i].CentralPoint = new Point(point.X *= x_multi, point.Y *= y_multi);*/
            }



            for (int i=0;i< _workProgramQueueBack.Count;i++)
            {
                // 深复制List<WorkProgram>  
                List<WorkProgram> work_list = _workProgramQueueBack[i];
                List<Image> image_list = _bmpQueueBack[i];
                for (int j=0;j<work_list.Count;j++)
                {
                    g.Clear(Color.White);
                    g.DrawImage(image_list[j], 0, 0);
                    image_list[j] = (Image)image.Clone();

                    Graphics child_g = Graphics.FromImage(image_list[j]);
                    work_list[j].Graphics = child_g;
                }
            }

            for (int i = 0; i < _workProgramQueueFront.Count; i++)
            {
                // 深复制List<WorkProgram>  
                List<WorkProgram> work_list = _workProgramQueueFront[i];
                List<Image> image_list = _bmpQueueFront[i];
                for (int j = 0; j < work_list.Count; j++)
                {
                    g.Clear(Color.White);
                    g.DrawImage(image_list[j], 0, 0);
                    image_list[j] = (Image)image.Clone();

                    Graphics child_g = Graphics.FromImage(image_list[j]);
                    work_list[j].Graphics = child_g;
                }
            }




            _backWorkProgram.Clear();
            _backWorkProgram.SetBackImage(_orginBackImage);
            RePaiting();
        }


    }
} 