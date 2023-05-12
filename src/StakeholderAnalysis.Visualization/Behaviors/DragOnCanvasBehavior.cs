using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace StakeholderAnalysis.Visualization.Behaviors
{
    public class DragOnCanvasBehavior
    {
        private static DragOnCanvasBehavior Instance = new DragOnCanvasBehavior();

        public static readonly DependencyProperty DropHandlerProperty =
            DependencyProperty.RegisterAttached(
                "DropHandler",
                typeof(IDropHandler),
                typeof(DragOnCanvasBehavior),
                new PropertyMetadata(OnDropHandlerChanged));

        private Point mouseLastPosition;

        private IDropHandler DropHandler { get; set; }

        public static IDropHandler GetDropHandler(UIElement target)
        {
            return (IDropHandler)target.GetValue(DropHandlerProperty);
        }

        public static void SetDropHandler(UIElement target, IDropHandler value)
        {
            target.SetValue(DropHandlerProperty, value);
        }

        private static void OnDropHandlerChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var element = (UIElement)sender;
            var handler = (IDropHandler)e.NewValue;

            Instance = new DragOnCanvasBehavior { DropHandler = handler };

            if (Instance.DropHandler != null)
            {
                element.MouseLeftButtonDown += Instance.ElementOnMouseLeftButtonDown;
                element.MouseLeftButtonUp += Instance.ElementOnMouseLeftButtonUp;
                element.MouseMove += Instance.ElementOnMouseMove;
            }
            else
            {
                element.MouseLeftButtonDown -= Instance.ElementOnMouseLeftButtonDown;
                element.MouseLeftButtonUp -= Instance.ElementOnMouseLeftButtonUp;
                element.MouseMove -= Instance.ElementOnMouseMove;
            }
        }

        private void ElementOnMouseLeftButtonDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            var canvas = FindParent<Canvas>((UIElement)sender);
            if (canvas == null) throw new ArgumentException();

            mouseLastPosition = GetMousePositionFromCanvas(canvas, mouseButtonEventArgs);
            ((UIElement)sender).CaptureMouse();
        }

        private void ElementOnMouseLeftButtonUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            var element = (UIElement)sender;
            element.ReleaseMouseCapture();
        }

        private void ElementOnMouseMove(object sender, MouseEventArgs mouseEventArgs)
        {
            if (!((UIElement)sender).IsMouseCaptured || DropHandler == null) return;

            var canvas = FindParent<Canvas>((UIElement)sender);
            if (canvas == null) throw new ArgumentException();
            var mouseCurrentPos = GetMousePositionFromCanvas(canvas, mouseEventArgs);
            var movement = mouseCurrentPos - mouseLastPosition;

            if (movement.Length > 0)
            {
                DropHandler.Moved(mouseCurrentPos.X / canvas.ActualWidth, mouseCurrentPos.Y / canvas.ActualHeight);

                mouseLastPosition = mouseCurrentPos;
            }
        }

        private Point GetMousePositionFromCanvas(Canvas canvas, MouseEventArgs mouseEventArgs)
        {
            return mouseEventArgs.GetPosition(canvas);
        }

        private static T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            var parentObject = VisualTreeHelper.GetParent(child);

            if (parentObject == null) return null;

            var parent = parentObject as T;
            return parent ?? FindParent<T>(parentObject);
        }
    }
}