﻿using System;
using System.ComponentModel;
using System.Reflection;
using StakeholderAnalysis.Data;

namespace StakeholderAnalysis.Visualization.ViewModels.PropertiesTree
{
    public class BooleanPropertyTreeNodeViewModel<TContent> : PropertyTreeNodeViewModelBaseBase, IBooleanPropertyTreeNodeViewModel where TContent : INotifyPropertyChangedImplementation
    {
        private readonly PropertyInfo propertyInfo;
        private TContent content;

        public BooleanPropertyTreeNodeViewModel(TContent content, string propertyName, string displayName) : base(displayName)
        {
            this.content = content;
            propertyInfo = typeof(TContent).GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
            if (propertyInfo == null || propertyInfo.PropertyType != typeof(bool))
            {
                throw new ArgumentException();
            }

            if (this.Content != null)
            {
                this.Content.PropertyChanged += ContentPropertyChanged;
            }
        }

        public TContent Content
        {
            get => content;
            set
            {
                if (content != null)
                {
                    content.PropertyChanged -= ContentPropertyChanged;
                }
                content = value;
                if (content != null)
                {
                    content.PropertyChanged += ContentPropertyChanged;
                }
                OnPropertyChanged(nameof(BooleanValue));
            }
        }

        public bool BooleanValue
        {
            get => (bool)propertyInfo.GetValue(content);
            set
            {
                if (propertyInfo.CanWrite)
                {
                    propertyInfo.SetValue(content, value, null);
                    content.OnPropertyChanged(propertyInfo.Name);
                }
            }
        }

        private void ContentPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == propertyInfo.Name)
            {
                OnPropertyChanged(nameof(BooleanValue));
            }
        }

        public override bool IsViewModelFor(object o)
        {
            return ReferenceEquals(o, content);
        }
    }
}
