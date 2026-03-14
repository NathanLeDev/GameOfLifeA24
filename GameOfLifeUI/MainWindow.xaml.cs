using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using DataLayerGameOfLife.Models;
using DataLayerGameOfLife.Repositories;
using GameOfLifeA24;
using GameOfLifeA24.Rules;

namespace GameOfLifeUI;

public partial class MainWindow : Window
{
    private static readonly int CellSize = 20;
    private static readonly int Rows = 25;
    private static readonly int Cols = 35;

    private GameOfLife? game;
    private readonly DispatcherTimer timer;
    private readonly IInitialStateRepository repo;

    private List<(int X, int Y)> initialAliveCells = new();
    private IRule selectedRule = new StandardRule();
    private bool running;

    public MainWindow()
    {
        InitializeComponent();

        repo = RepositoryFactory.Instance.GetInitialStateRepository();
        LoadData();

        timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(250) };
        timer.Tick += Timer_Tick;

        DrawGrid();

        if (InitialStatesCombo.Items.Count > 0)
            InitialStatesCombo.SelectedIndex = 0;
    }

    private void LoadData()
    {
        //COmplete to get the list of all states
        InitialStatesCombo.ItemsSource = states;
        InitialStatesCombo.DisplayMemberPath = nameof(InitialState.Name);
    }

    private void CreateOrResetGame()
    {
        //Initialise Game
        DrawGameGrid();
    }

    private void DrawGrid()
    {
        GridHost.Rows = Rows;
        GridHost.Columns = Cols;
        GridHost.Children.Clear();

        for (int x = 0; x < Rows; x++)
            for (int y = 0; y < Cols; y++)
            {
                var b = new Border
                {
                    Width = CellSize,
                    Height = CellSize,
                    BorderThickness = new Thickness(0.5),
                    BorderBrush = SystemColors.ActiveBorderBrush,
                    Background = SystemColors.ControlLightBrush,
                    Tag = (x, y)
                };
                b.MouseLeftButtonDown += SelectInitialCell;
                GridHost.Children.Add(b);
            }
    }

    private void DrawGameGrid()
    {
        if (game is null) return;

        foreach (var child in GridHost.Children)
        {
            if (child is not Border b) continue;
            var (x, y) = ((int, int))b.Tag;
            b.Background = //Check if celle is alive
                ? SystemColors.ControlTextBrush
                : SystemColors.ControlLightBrush;
        }
    }

    private void SelectInitialCell(object sender, MouseButtonEventArgs e)
    {
        if (running) return;
        if (sender is not Border b) return;

        var (x, y) = ((int, int))b.Tag;

        var idx = initialAliveCells.FindIndex(p => p.X == x && p.Y == y);
        if (idx >= 0)
        {
            initialAliveCells.RemoveAt(idx);
            b.Background = SystemColors.ControlLightBrush;
        }
        else
        {
            initialAliveCells.Add((x, y));
            b.Background = SystemColors.ControlTextBrush;
        }

        CreateOrResetGame();
    }

    private void Timer_Tick(object? sender, EventArgs e)
    {
        if (game is null) CreateOrResetGame();
        //Update game here

        DrawGameGrid();
    }

    private void Start(object sender, RoutedEventArgs e)
    {
        running = !running;
        if (running) timer.Start();
        else timer.Stop();
    }

    private void ClearState(object sender, EventArgs e)
    {
        if (running) return;
        initialAliveCells.Clear();
        CreateOrResetGame();
    }

    private void InitialStateChange(object sender, SelectionChangedEventArgs e)
    {
        if (InitialStatesCombo.SelectedItem is not InitialState state) return;

        initialAliveCells = //Get A list of X, Y point from the state
        CreateOrResetGame();
    }

    private void SaveInitialState(object sender, RoutedEventArgs e)
    {
        if (running) return;

        var name = Microsoft.VisualBasic.Interaction.InputBox(
            "Name of the new initial state:", "Save Initial State", "CustomState");

        if (string.IsNullOrWhiteSpace(name)) return;

       //Add new state to the db here

        LoadData();
        InitialStatesCombo.SelectedItem = repo.Get(name.Trim());
    }

    private void DeleteInitState(object sender, RoutedEventArgs e)
    {
        if (running) return;
        if (InitialStatesCombo.SelectedItem is not InitialState state) return;

        if (MessageBox.Show($"Delete '{state.Name}'?", "Confirm",
                MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes)
            return;

        //Delete state from the db
        LoadData();
        if (InitialStatesCombo.Items.Count > 0) InitialStatesCombo.SelectedIndex = 0;
        else ClearState(sender, EventArgs.Empty);
    }
}
