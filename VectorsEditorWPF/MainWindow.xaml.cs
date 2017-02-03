using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using VectorEditorCore;
using VectorEditorCore.Interfaces;
using EventHandler = VectorEditorController.EventHandler;

namespace VectorEditorWPF
{
    public partial class MainWindow : Window, IForm
    {
        readonly EventHandler _eventHandler;

        public bool FillColor { get; set; }
        public bool LineColor { get; set; }
        public bool LineSize { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            IForm form = this;

            _eventHandler = new EventHandler(form, image);
        }

        private void fileOpenButton_Click_1(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.DefaultExt = ".txt";
            openFileDialog.Filter = "Text documents (.txt)|*.txt";

            Nullable<bool> result = openFileDialog.ShowDialog();

            if (result == true)
                _eventHandler.Load(openFileDialog.FileName);
        }

        private void fileSaveButton_Click_1(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
            saveFileDialog.DefaultExt = ".txt";
            saveFileDialog.Filter = "Text documents (.txt)|*.txt";

            Nullable<bool> result = saveFileDialog.ShowDialog();

            if (result == true)
                _eventHandler.Save(saveFileDialog.FileName);
        }

        private void canvas_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            _eventHandler.MouseDown((int)e.GetPosition(image).X, (int)e.GetPosition(image).Y);

            UpdatePropirties();
        }

        private void UpdatePropirties()
        {
            radioButtonFillColor.IsEnabled = FillColor;
            if (!radioButtonFillColor.IsEnabled) radioButtonFillColor.IsChecked = false;
            radioButtonLineColor.IsEnabled = LineColor;
            lineSizeComboBoxItem.IsEnabled = LineSize;
        }

        private void canvas_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            _eventHandler.MouseMove((int)e.GetPosition(image).X, (int)e.GetPosition(image).Y);
        }

        private void canvas_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            _eventHandler.MouseUp((int)e.GetPosition(image).X, (int)e.GetPosition(image).Y);
        }

        private void CreateRect_OnClick(object sender, RoutedEventArgs e)
        {
            _eventHandler.SetFigure(FigureType.RECT);

            _eventHandler.CreateFigure();
        }

        private void CreateLine_OnClick(object sender, RoutedEventArgs e)
        {
            _eventHandler.SetFigure(FigureType.LINE);

            _eventHandler.CreateFigure();
        }

        private void CreateEllipse_OnClick(object sender, RoutedEventArgs e)
        {
            _eventHandler.SetFigure(FigureType.ELLIPSE);

            _eventHandler.CreateFigure();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
                _eventHandler.Delete();

            if (e.Key == Key.LeftShift)
            {
                _eventHandler.SetMultiSelect(true);
            }

            if (e.Key == Key.Escape)
                _eventHandler.Escape();
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftShift)
            {
                _eventHandler.SetMultiSelect(false);
            }
        }

        private void ColorPicker_OnSelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            if (radioButtonFillColor.IsChecked == true)
                _eventHandler.SetFigureFillColor(e.NewValue);

            else if (radioButtonLineColor.IsChecked == true)
                _eventHandler.SetFigureLineColor(e.NewValue);
        }

        private void lineSizeComboBoxItem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lineSizeComboBoxItem.SelectedItem != null) 
                _eventHandler.SetFigureLineSize(lineSizeComboBoxItem.SelectedIndex+1);

            lineSizeComboBoxItem.SelectedItem = null;
        }        
    }
}
