using Avalonia;
using Avalonia.Media;
using Avalonia.Controls;

namespace NodeEditor.Views;

public partial class GridLines : Canvas
{
    public static readonly StyledProperty<int> LineCountProperty = 
        AvaloniaProperty.Register<GridLines, int>(nameof(LineCount));

    public static readonly StyledProperty<int> PrimaryLineStepProperty = 
        AvaloniaProperty.Register<GridLines, int>(nameof(PrimaryLineStep));

    public static readonly StyledProperty<SolidColorBrush> PrimaryLineBrushProperty =
        AvaloniaProperty.Register<GridLines, SolidColorBrush>(nameof(PrimaryLineBrush));

    public static readonly StyledProperty<SolidColorBrush> SecondaryLineBrushProperty =
        AvaloniaProperty.Register<GridLines, SolidColorBrush>(nameof(SecondaryLineBrush));


    public int LineCount
    {
        get { return GetValue(LineCountProperty); }
        set { SetValue(LineCountProperty, value); }
    }

    public int PrimaryLineStep
    {
        get { return GetValue(PrimaryLineStepProperty); }
        set { SetValue(PrimaryLineStepProperty, value); }
    }

    public SolidColorBrush PrimaryLineBrush
    {
        get { return GetValue(PrimaryLineBrushProperty); }
        set { SetValue(PrimaryLineBrushProperty, value); }
    }

    public SolidColorBrush SecondaryLineBrush
    {
        get { return GetValue(SecondaryLineBrushProperty); }
        set { SetValue(SecondaryLineBrushProperty, value); }
    }


    public GridLines()
    {
        InitializeComponent();
    }

    public override void Render(DrawingContext drawingContext)
    {
        base.Render(drawingContext);

        Pen primaryPen = new(PrimaryLineBrush, 1, lineCap: PenLineCap.Square);
        Pen secondaryPen = new(SecondaryLineBrush, 1, lineCap: PenLineCap.Square);

        GeometryGroup primaryGeometryGroup = new GeometryGroup();
        GeometryGroup secondaryGeometryGroup = new GeometryGroup();

        double xStep = Bounds.Width / LineCount;
        double yStep = Bounds.Height / LineCount;

        for (int x = 0; x < LineCount; x++)
        {
            double xPosition = xStep * x;

            Point point1 = new Point(xPosition, 0);
            Point point2 = new Point(xPosition, Bounds.Height);

            GeometryGroup group = x % PrimaryLineStep == 0 ? primaryGeometryGroup : secondaryGeometryGroup;

            group.Children.Add(new LineGeometry(point1, point2));
        }

        for (int y = 0; y < LineCount; y++)
        {
            double yPosition = yStep * y;

            Point point1 = new Point(0, yPosition);
            Point point2 = new Point(Bounds.Width, yPosition);

            GeometryGroup group = y % PrimaryLineStep == 0 ? primaryGeometryGroup : secondaryGeometryGroup;

            group.Children.Add(new LineGeometry(point1, point2));
        }

        drawingContext.DrawGeometry(SecondaryLineBrush, secondaryPen, secondaryGeometryGroup);
        drawingContext.DrawGeometry(PrimaryLineBrush, primaryPen, primaryGeometryGroup);
    }
}