﻿using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Media;
using StakeholderAnalysis.Data;

namespace StakeholderAnalysis.Visualization.ViewModels.PropertiesTree
{
    public class FontFamilyPropertyTreeNodeViewModel<TContent> : PropertyTreeNodeViewModelBase,
        IFontFamilyPropertyTreeNodeViewModel where TContent : INotifyPropertyChangedImplementation
    {
        private readonly PropertyInfo propertyInfo;
        private TContent content;

        public FontFamilyPropertyTreeNodeViewModel(TContent content, string propertyName, string displayName) : base(
            displayName)
        {
            Content = content;

            propertyInfo = typeof(TContent).GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
            if (propertyInfo == null || propertyInfo.PropertyType != typeof(FontFamily)) throw new ArgumentException();

            if (Content != null) Content.PropertyChanged += ContentPropertyChanged;
        }

        public TContent Content
        {
            get => content;
            set
            {
                if (content != null) content.PropertyChanged -= ContentPropertyChanged;
                content = value;
                if (content != null) content.PropertyChanged += ContentPropertyChanged;
                OnPropertyChanged(nameof(SelectedValue));
            }
        }

        public FontFamily SelectedValue
        {
            get => Content != null ? (FontFamily)propertyInfo.GetValue(Content) : default;
            set
            {
                if (propertyInfo.CanWrite && Content != null)
                {
                    propertyInfo.SetValue(Content, value, null);
                    Content.OnPropertyChanged(propertyInfo.Name);
                }
            }
        }

        public override bool IsViewModelFor(object o)
        {
            return ReferenceEquals(o, Content);
        }

        private void ContentPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == propertyInfo.Name) OnPropertyChanged(nameof(SelectedValue));
        }
    }
}