using Avalonia.Controls;

namespace AvaloniaCurves;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        this.WindowState = WindowState.Maximized;

        // Создание кнопки
        var startButton = new Button { Content = "Start Game" };

        // Создание TickControl
        var tickControl = new TickControl();

        // Привязка события Click к методу, который будет запускать таймер
        startButton.Click += (sender, e) => tickControl.StartTimer();

        // Добавление кнопки и TickControl на экран
        var panel = new StackPanel();
        panel.Children.Add(startButton);
        panel.Children.Add(tickControl);
        this.Content = panel;
    }
}