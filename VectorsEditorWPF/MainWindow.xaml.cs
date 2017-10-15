using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using VectorEditorController;
using VectorEditorController.Interfaces;
using VectorEditorController.States;
using VectorEditorCore;
using VectorEditorCore.Interfaces;
using VectorEditorCore.IOData;
using VectorsEditorCore.Interfaces;

namespace VectorEditorWPF
{
    public partial class MainWindow : Window, IForm
    {
        private Store _store;
        private ISavable _saver;
        private ILoadable _loader;
        private ICommand1 _command;
        private IFigureManager _figureManager;
        private IStateMachine _stateMachine;

        public bool FillColor { get; set; }
        public bool LineColor { get; set; }
        public bool LineSize { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            IForm form = this;
            _store = new Store();
            _saver = new Saver(_store);
            _loader = new Loader(_store);
            _figureManager = new FigureManager(form, image, _store);
            _stateMachine = new StateMachine(_figureManager);
            _command = new Command1(_figureManager, _stateMachine, _saver, _loader);
        }

        private void fileOpenButton_Click_1(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.DefaultExt = ".txt";
            openFileDialog.Filter = "Text documents (.txt)|*.txt";

            var result = openFileDialog.ShowDialog();

            if (result == true)
                _loader.Load(openFileDialog.FileName);
            //TODO: костыль, убрать
            _figureManager.ReDraw();
        }

        private void fileSaveButton_Click_1(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
            saveFileDialog.DefaultExt = ".txt";
            saveFileDialog.Filter = "Text documents (.txt)|*.txt";

            var result = saveFileDialog.ShowDialog();

            if (result == true)
                _saver.Save(saveFileDialog.FileName);
        }

        private void canvas_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var state = _stateMachine.GetCurrentState();
            state.MouseDown((int)e.GetPosition(image).X, (int)e.GetPosition(image).Y);

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
            var state = _stateMachine.GetCurrentState();
            state.MouseMove((int)e.GetPosition(image).X, (int)e.GetPosition(image).Y);
        }

        private void canvas_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            var state = _stateMachine.GetCurrentState();
            state.MouseUp((int)e.GetPosition(image).X, (int)e.GetPosition(image).Y);
        }

        private void CreateRect_OnClick(object sender, RoutedEventArgs e)
        {
            _command.SetFigure(FigureType.RECT);

            _command.CreateFigure();
        }

        private void CreateLine_OnClick(object sender, RoutedEventArgs e)
        {
            _command.SetFigure(FigureType.LINE);

            _command.CreateFigure();
        }

        private void CreateEllipse_OnClick(object sender, RoutedEventArgs e)
        {
            _command.SetFigure(FigureType.ELLIPSE);

            _command.CreateFigure();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            var state = _stateMachine.GetCurrentState();

            if (e.Key == Key.Delete)
                state.Delete();

            if (e.Key == Key.LeftShift)
            {
                _command.SetMultiSelect(true);
            }

            if (e.Key == Key.Escape)
                state.Escape();
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftShift)
            {
                _command.SetMultiSelect(false);
            }
        }

        private void ColorPicker_OnSelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            if (radioButtonFillColor.IsChecked == true)
                _command.SetFigureFillColor(e.NewValue);

            else if (radioButtonLineColor.IsChecked == true)
                _command.SetFigureLineColor(e.NewValue);
        }

        private void lineSizeComboBoxItem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lineSizeComboBoxItem.SelectedItem != null)
                _command.SetFigureLineSize(lineSizeComboBoxItem.SelectedIndex+1);

            lineSizeComboBoxItem.SelectedItem = null;
        }        
    }
}
