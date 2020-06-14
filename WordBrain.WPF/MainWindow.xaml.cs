using System.Windows;
using System.Windows.Controls;

namespace WordBrain.WPF
{
    public partial class MainWindow
    {
        public MainWindow()
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
            var contentPresenter = (ContentPresenter)Solution.ItemContainerGenerator.ContainerFromIndex(0);
            var element = (UIElement)contentPresenter.ContentTemplate.FindName("Word", contentPresenter);
            SolutionLabel.Target = element;
        }
    }
}
