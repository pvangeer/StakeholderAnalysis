using System.Windows;
using System.Windows.Controls;
using StakeholderAnalysis.Visualization.Commands;

namespace StakeholderAnalysis.Visualization
{
    public static class SaveToImageBehaviours
    {
        public static readonly DependencyProperty SaveToImageProperty =
            DependencyProperty.RegisterAttached("SaveToImage", typeof(bool), typeof(SaveToImageBehaviours),
                new UIPropertyMetadata(false, OnSaveToImage));

        public static void SetSaveToImage(DependencyObject obj, bool value)
        {
            obj.SetValue(SaveToImageProperty, value);
        }

        public static bool GetSaveToImage(DependencyObject obj)
        {
            return (bool) obj.GetValue(SaveToImageProperty);
        }

        private static void OnSaveToImage(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if ((bool) e.NewValue)
            {
                if (!(obj is ContentPresenter contentPresenter)) return;

                contentPresenter.SaveToFile();
            }
        }
    }
}