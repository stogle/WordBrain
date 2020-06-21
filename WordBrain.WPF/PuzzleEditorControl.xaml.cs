﻿using System.Windows;
using System.Windows.Controls;

namespace WordBrain.WPF
{
    public partial class PuzzleEditorControl
    {
        public PuzzleEditorControl()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            var contentPresenter = (ContentPresenter)Grid.ItemContainerGenerator.ContainerFromIndex(0);
            var element = (UIElement)contentPresenter.ContentTemplate.FindName("Line", contentPresenter);
            GridLabel.Target = element;
            element.Focus();
        }

        private void Solution_Loaded(object sender, RoutedEventArgs e)
        {
            var contentPresenter = (ContentPresenter)Lengths.ItemContainerGenerator.ContainerFromIndex(0);
            var element = (UIElement)contentPresenter.ContentTemplate.FindName("Word", contentPresenter);
            LengthsLabel.Target = element;
        }
    }
}
