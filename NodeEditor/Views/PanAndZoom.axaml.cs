using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using System;

namespace NodeEditor.Views;

public partial class PanAndZoom : Canvas
{
    private Point _previousPoint;
    private bool _isPanning;

    public PanAndZoom()
    {
        InitializeComponent();

        TransformGroup transformGroup = new TransformGroup();
        transformGroup.Children.Add(new TranslateTransform());
        transformGroup.Children.Add(new ScaleTransform());

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
            TransformGroup transformGroup = (TransformGroup)RenderTransform;
            TranslateTransform translateTransform = (TranslateTransform)transformGroup.Children[0];
            ScaleTransform scaleTransform = (ScaleTransform)transformGroup.Children[1];

            Point currentPoint = e.GetPosition(Parent);

            Point delta = currentPoint - _previousPoint;

            translateTransform.X += delta.X / scaleTransform.ScaleX;
            translateTransform.Y += delta.Y / scaleTransform.ScaleY;

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

        TransformGroup transformGroup = (TransformGroup)RenderTransform;
        TranslateTransform translateTransform = (TranslateTransform)transformGroup.Children[0];
        ScaleTransform scaleTransform = (ScaleTransform)transformGroup.Children[1];

        double scaleStep = 0.2 * scaleTransform.ScaleX;

        double scale = Math.Clamp(scaleTransform.ScaleX + e.Delta.Y * scaleStep, 0.25, 5);

        Point oldMousePoint = e.GetPosition(this);

        scaleTransform.ScaleX = scale;
        scaleTransform.ScaleY = scale;

        Point mouseDelta = e.GetPosition(this) - oldMousePoint;

        double xOffset = mouseDelta.X;
        double yOffset = mouseDelta.Y;

        translateTransform.X += xOffset;
        translateTransform.Y += yOffset;

        ClampPosition();
    }

    void ClampPosition()
    {
        TransformGroup transformGroup = (TransformGroup)RenderTransform;
        TranslateTransform translateTransform = (TranslateTransform)transformGroup.Children[0];
        ScaleTransform scaleTransform = (ScaleTransform)transformGroup.Children[1];

        double maxX = (Bounds.Width - Parent.Bounds.Width / scaleTransform.ScaleY) * 0.5;
        double maxY = (Bounds.Height - Parent.Bounds.Height / scaleTransform.ScaleY) * 0.5;

        translateTransform.X = Math.Clamp(translateTransform.X, -maxX, maxX);
        translateTransform.Y = Math.Clamp(translateTransform.Y, -maxY, maxY);
    }
}