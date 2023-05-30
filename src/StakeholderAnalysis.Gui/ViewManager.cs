﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using StakeholderAnalysis.Gui.Properties;

namespace StakeholderAnalysis.Gui
{
    public class ViewManager : INotifyPropertyChanged
    {
        private readonly SelectionManager selectionManager;

        public ViewManager(SelectionManager selectionManager)
        {
            Views = new ObservableCollection<ViewInfo>();
            ActiveDocument = null;

            this.selectionManager = selectionManager;
            selectionManager.PropertyChanged += SelectionManagerPropertyChanged;
        }

        public ObservableCollection<ViewInfo> Views { get; }

        public ViewInfo ActiveDocument { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void SelectionManagerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(SelectionManager.Selection):
                    if (selectionManager.Selection != null)
                        foreach (var viewInfo in Views)
                            if (viewInfo.ViewModel is ISelectable selectable)
                                if (viewInfo.ViewModel.IsViewModelFor(selectionManager.Selection))
                                    BringToFront(viewInfo);
                    break;
            }
        }

        public void OpenView(ViewInfo viewInfo)
        {
            if (IsAvailableView(viewInfo))
            {
                BringToFront(viewInfo);
                return;
            }

            viewInfo.CloseViewCommand = new CloseViewCommand(this, viewInfo);
            Views.Add(viewInfo);
        }

        public void CloseView(ViewInfo viewInfo)
        {
            if (IsAvailableView(viewInfo))
            {
                Views.Remove(viewInfo);
                OnPropertyChanged(nameof(ActiveDocument));
            }
        }

        public void BringToFront(ViewInfo viewInfo)
        {
            if (!IsAvailableView(viewInfo)) OpenView(viewInfo);

            ActiveDocument = viewInfo;
            OnPropertyChanged(nameof(ActiveDocument));
        }

        [NotifyPropertyChangedInvocator]
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool IsAvailableView(ViewInfo viewInfo)
        {
            return Views.Any(v => v == viewInfo || v.ViewModel == viewInfo.ViewModel);
        }
    }
}