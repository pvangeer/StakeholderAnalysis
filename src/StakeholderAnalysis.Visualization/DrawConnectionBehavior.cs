using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Visualization.ViewModels;
using StakeholderAnalysis.Visualization.ViewModels.OnionDiagramView;

namespace StakeholderAnalysis.Visualization
{
    public class DrawConnectionBehavior
    {
        private static DrawConnectionBehavior Instance = new DrawConnectionBehavior();

        private Point mouseLastPosition;

        public static readonly DependencyProperty DrawConnectionHandlerProperty =
            DependencyProperty.RegisterAttached(
                "DrawConnectionHandler",
                typeof(IDrawConnectionHandler),
                typeof(DrawConnectionBehavior),
                new PropertyMetadata(OnDrawConnectionHandlerChanged));

        private IDrawConnectionHandler DrawConnectionHandler { get; set; }

        public static IDrawConnectionHandler GetDrawConnectionHandler(UIElement target)
        {
            return (IDrawConnectionHandler)target.GetValue(DrawConnectionHandlerProperty);
        }

        public static void SetDrawConnectionHandler(UIElement target, IDrawConnectionHandler value)
        {
            target.SetValue(DrawConnectionHandlerProperty, value);
        }

        private static void OnDrawConnectionHandlerChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            UIElement element = (UIElement)sender;
            IDrawConnectionHandler handler = (IDrawConnectionHandler)(e.NewValue);

            Instance = new DrawConnectionBehavior { DrawConnectionHandler = handler};

            if (Instance.DrawConnectionHandler != null)
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
            if (canvas == null)
            {
                throw new ArgumentException();
            }

            mouseLastPosition = GetMousePositionFromCanvas(canvas, mouseButtonEventArgs);
            var firstViewModel = FindViewModelUnderMouse(canvas, mouseLastPosition);

            if (firstViewModel == null)
            {
                return;
            }
            ((UIElement)sender).CaptureMouse();
            DrawConnectionHandler.InitializeConnection(firstViewModel);
        }

        private void ElementOnMouseLeftButtonUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            // Find StakeholderTo under mouse button (use hittest) and make connection
            UIElement element = (UIElement)sender;
            element.ReleaseMouseCapture();

            DrawConnectionHandler.FinishConnecting();
        }

        private void ElementOnMouseMove(object sender, MouseEventArgs mouseEventArgs)
        {
            // Update drawing
            if (!((UIElement)sender).IsMouseCaptured || this.DrawConnectionHandler == null)
            {
                return;
            }

            var canvas = FindParent<Canvas>((UIElement)sender);
            if (canvas == null)
            {
                throw new ArgumentException();
            }
            Point mouseCurrentPos = GetMousePositionFromCanvas(canvas, mouseEventArgs);
            Vector movement = mouseCurrentPos - mouseLastPosition;
            
            if (movement.Length > 0)
            {
                DrawConnectionHandler.PositionMoved(mouseCurrentPos.X / canvas.ActualWidth, mouseCurrentPos.Y / canvas.ActualHeight);
                mouseLastPosition = mouseCurrentPos;

                DrawConnectionHandler.ChangeTarget(FindViewModelUnderMouse(canvas, mouseCurrentPos));
            }
        }

        private OnionDiagramStakeholderViewModel FindViewModelUnderMouse(Canvas canvas, Point mouseCurrentPos)
        {
            // Perform the hit test against a given portion of the visual object tree.
            HitTestResult result = VisualTreeHelper.HitTest(canvas, mouseCurrentPos);

            if (result != null)
            {
                var contentPresenter = FindParent<ContentPresenter>(result.VisualHit);
                
                if (contentPresenter != null && contentPresenter.Content is OnionDiagramStakeholderViewModel viewModel)
                {
                    return viewModel;
                }
            }

            return null;
        }

        private Point GetMousePositionFromCanvas(Canvas canvas, MouseEventArgs mouseEventArgs)
        {
            return mouseEventArgs.GetPosition(canvas);
        }

        private static T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            if (parentObject == null)
            {
                return null;
            }

            T parent = parentObject as T;
            return parent ?? FindParent<T>(parentObject);
        }
    }

    public interface IDrawConnectionHandler
    {
        void PositionMoved(double relativeLeft, double relativeTop);

        void ChangeTarget(OnionDiagramStakeholderViewModel viewModel);

        void InitializeConnection(OnionDiagramStakeholderViewModel stakeholderViewModel);

        void FinishConnecting();

        bool IsConnectionTarget(Stakeholder stakeholder);
    }
}