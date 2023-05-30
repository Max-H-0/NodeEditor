using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using System;

namespace NodeEditor.Views;

public partial class PanAndZoom : Canvas
{
    private readonly TranslateTransform _translateTransform;
    private readonly ScaleTransform _scaleTransform;

    private Point _previousPoint;
    private bool _isPanning;

    public PanAndZoom()
    {
        InitializeComponent();

        TransformGroup transformGroup = new();
        transformGroup.Children.Add(_translateTransform = new TranslateTransform());
        transformGroup.Children.Add(_scaleTransform = new ScaleTransform());

        RenderTransform = transformGroup;

        Initialized += (s, e) =>
        {
            Parent.SizeChanged += (s, e) =>
            {
                ClampPosition();
            };
        };
    }

    protected override void OnPointerPressed(PointerPressedEventArgs e)
    {
        base.OnPointerPressed(e);

        _isPanning = e.GetCurrentPoint(Parent).Properties.IsMiddleButtonPressed;

        _previousPoint = e.GetPosition(Parent);
    }

    protected override void OnPointerMoved(PointerEventArgs e)
    {
        base.OnPointerMoved(e);

        if (_isPanning)
        {
            Point currentPoint = e.GetPosition(Parent);

            Point delta = currentPoint - _previousPoint;

            _translateTransform.X += delta.X / _scaleTransform.ScaleX;
            _translateTransform.Y += delta.Y / _scaleTransform.ScaleY;

            ClampPosition();

            _previousPoint = currentPoint;
        }
    }

    protected override void OnPointerReleased(PointerReleasedEventArgs e)
    {
        base.OnPointerReleased(e);

        _isPanning = e.GetCurrentPoint(Parent).Properties.IsMiddleButtonPressed;
    }

    protected override void OnPointerWheelChanged(PointerWheelEventArgs e)
    {
        base.OnPointerWheelChanged(e);

        double scaleStep = 0.2 * _scaleTransform.ScaleX;

        double scale = Math.Clamp(_scaleTransform.ScaleX + e.Delta.Y * scaleStep, 0.25, 5);

        Point oldMousePoint = e.GetPosition(this);

        _scaleTransform.ScaleX = scale;
        _scaleTransform.ScaleY = scale;

        Point mouseDelta = e.GetPosition(this) - oldMousePoint;

        double xOffset = mouseDelta.X;
        double yOffset = mouseDelta.Y;

        _translateTransform.X += xOffset;
        _translateTransform.Y += yOffset;

        ClampPosition();
    }

    void ClampPosition()
    {
        double maxX = (Bounds.Width - Parent.Bounds.Width / _scaleTransform.ScaleY) * 0.5;
        double maxY = (Bounds.Height - Parent.Bounds.Height / _scaleTransform.ScaleY) * 0.5;

        _translateTransform.X = Math.Clamp(_translateTransform.X, -maxX, maxX);
        _translateTransform.Y = Math.Clamp(_translateTransform.Y, -maxY, maxY);
    }
}