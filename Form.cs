using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static HealthManager;

public class ReportForm : Form
{
    private Dictionary<string, decimal> activityTracking = new Dictionary<string, decimal>();
    private Dictionary<string, decimal> nutritionTracking = new Dictionary<string, decimal>();
    private Dictionary<string, decimal> sleepTracking = new Dictionary<string, decimal>();

    public Dictionary<string, decimal> ActivityTracking { set { activityTracking = value; } }
    public Dictionary<string, decimal> NutritionTracking { set { nutritionTracking = value; } }
    public Dictionary<string, decimal> SleepTracking { set { sleepTracking = value; } }

    public ReportForm()
    {
        this.Text = "Отчёт по здоровью";
        this.Width = 420;
        this.Height = 340;
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        CreateControls();
    }

    private void CreateControls()
    {
        var reportRichTextBox = new RichTextBox
        {
            Name = "reportRichTextBox",
            Location = new Point(10, 10),
            Size = new Size(380, 280),
            ReadOnly = true
        };
        reportRichTextBox.AppendText("Отчёт по активностям:\n");
        foreach (var activity in activityTracking)
            reportRichTextBox.AppendText($"  {activity.Key}: {activity.Value} минут.\n");

        reportRichTextBox.AppendText("\nОтчёт по питанию:\n");
        foreach (var food in nutritionTracking)
            reportRichTextBox.AppendText($"  {food.Key}: {food.Value} калорий.\n");

        reportRichTextBox.AppendText("\nОтчёт по сну:\n");
        foreach (var sleep in sleepTracking)
            reportRichTextBox.AppendText($"  {sleep.Key}: {sleep.Value} часов.\n");

        this.Controls.Add(reportRichTextBox);
    }
}

public class ActivityForm : Form
{
    private TextBox activityTypeTextBox;
    private TextBox durationTextBox;
    private Label activityTypeLabel;
    private Label durationLabel;

    public string ActivityType { get; private set; }
    public decimal Duration { get; private set; }

    public ActivityForm()
    {
        this.Text = "Добавить активность";
        this.Width = 260;
        this.Height = 200;
        CreateControls();
    }

    private void CreateControls()
    {
        activityTypeLabel = new Label { Text = "Тип активности:", Location = new Point(10, 10), AutoSize = true };
        activityTypeTextBox = new TextBox { Name = "activityTypeTextBox", Location = new Point(10, 30), Size = new Size(200, 20) };
        durationLabel = new Label { Text = "Продолжительность (минут):", Location = new Point(10, 55), AutoSize = true };
        durationTextBox = new TextBox { Name = "durationTextBox", Location = new Point(10, 75), Size = new Size(200, 20) };

        var okButton = new Button { Text = "OK", Location = new Point(10, 110), Size = new Size(80, 25) };
        okButton.Click += (sender, e) =>
        {
            if (decimal.TryParse(durationTextBox.Text, out decimal duration))
            {
                ActivityType = activityTypeTextBox.Text;
                Duration = duration;
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите корректное значение продолжительности.");
            }
        };

        var cancelButton = new Button { Text = "Отмена", Location = new Point(130, 110), Size = new Size(80, 25) };
        cancelButton.Click += (sender, e) =>
        {
            DialogResult = DialogResult.Cancel;
            Close();
        };

        this.Controls.Add(activityTypeLabel);
        this.Controls.Add(activityTypeTextBox);
        this.Controls.Add(durationLabel);
        this.Controls.Add(durationTextBox);
        this.Controls.Add(okButton);
        this.Controls.Add(cancelButton);
    }
}

public class NutritionForm : Form
{
    private TextBox foodItemTextBox;
    private TextBox caloriesTextBox;
    private Label foodItemLabel;
    private Label caloriesLabel;

    public string FoodItem { get; private set; }
    public decimal Calories { get; private set; }

    public NutritionForm()
    {
        this.Text = "Добавить питание";
        this.Width = 260;
        this.Height = 200;
        CreateControls();
    }

    private void CreateControls()
    {
        foodItemLabel = new Label { Text = "Название пищи:", Location = new Point(10, 10), AutoSize = true };
        foodItemTextBox = new TextBox { Name = "foodItemTextBox", Location = new Point(10, 30), Size = new Size(200, 20) };
        caloriesLabel = new Label { Text = "Калорийность:", Location = new Point(10, 55), AutoSize = true };
        caloriesTextBox = new TextBox { Name = "caloriesTextBox", Location = new Point(10, 75), Size = new Size(200, 20) };

        var okButton = new Button { Text = "OK", Location = new Point(10, 110), Size = new Size(80, 25) };
        okButton.Click += (sender, e) =>
        {
            if (decimal.TryParse(caloriesTextBox.Text, out decimal calories))
            {
                FoodItem = foodItemTextBox.Text;
                Calories = calories;
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите корректное значение калорийности.");
            }
        };

        var cancelButton = new Button { Text = "Отмена", Location = new Point(130, 110), Size = new Size(80, 25) };
        cancelButton.Click += (sender, e) =>
        {
            DialogResult = DialogResult.Cancel;
            Close();
        };

        this.Controls.Add(foodItemLabel);
        this.Controls.Add(foodItemTextBox);
        this.Controls.Add(caloriesLabel);
        this.Controls.Add(caloriesTextBox);
        this.Controls.Add(okButton);
        this.Controls.Add(cancelButton);
    }
}

public class SleepForm : Form
{
    private TextBox dateTextBox;
    private TextBox hoursTextBox;
    private Label dateLabel;
    private Label hoursLabel;

    public string Date { get; private set; }
    public decimal Hours { get; private set; }

    public SleepForm()
    {
        this.Text = "Добавить сон";
        this.Width = 260;
        this.Height = 200;
        CreateControls();
    }

    private void CreateControls()
    {
        dateLabel = new Label { Text = "Дата:", Location = new Point(10, 10), AutoSize = true };
        dateTextBox = new TextBox { Name = "dateTextBox", Location = new Point(10, 30), Size = new Size(200, 20) };
        hoursLabel = new Label { Text = "Количество часов:", Location = new Point(10, 55), AutoSize = true };
        hoursTextBox = new TextBox { Name = "hoursTextBox", Location = new Point(10, 75), Size = new Size(200, 20) };

        var okButton = new Button { Text = "OK", Location = new Point(10, 110), Size = new Size(80, 25) };
        okButton.Click += (sender, e) =>
        {
            if (decimal.TryParse(hoursTextBox.Text, out decimal hours))
            {
                Date = dateTextBox.Text;
                Hours = hours;
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите корректное значение часов.");
            }
        };

        var cancelButton = new Button { Text = "Отмена", Location = new Point(130, 110), Size = new Size(80, 25) };
        cancelButton.Click += (sender, e) =>
        {
            DialogResult = DialogResult.Cancel;
            Close();
        };

        this.Controls.Add(dateLabel);
        this.Controls.Add(dateTextBox);
        this.Controls.Add(hoursLabel);
        this.Controls.Add(hoursTextBox);
        this.Controls.Add(okButton);
        this.Controls.Add(cancelButton);
    }
}

public class GoalsForm : Form
{
    private TextBox activityGoalTextBox;
    private TextBox nutritionGoalTextBox;
    private TextBox sleepGoalTextBox;

    public decimal ActivityGoal { get; private set; }
    public decimal NutritionGoal { get; private set; }
    public decimal SleepGoal { get; private set; }

    public GoalsForm()
    {
        this.Text = "Установить цели";
        this.Width = 280;
        this.Height = 260;
        CreateControls();
    }

    private void CreateControls()
    {
        var activityLabel = new Label { Text = "Цель по активности (минут в день):", Location = new Point(10, 10), AutoSize = true };
        activityGoalTextBox = new TextBox { Name = "activityGoalTextBox", Location = new Point(10, 30), Size = new Size(220, 20) };
        var nutritionLabel = new Label { Text = "Цель по питанию (калорий в день):", Location = new Point(10, 60), AutoSize = true };
        nutritionGoalTextBox = new TextBox { Name = "nutritionGoalTextBox", Location = new Point(10, 80), Size = new Size(220, 20) };
        var sleepLabel = new Label { Text = "Цель по сну (часов в день):", Location = new Point(10, 110), AutoSize = true };
        sleepGoalTextBox = new TextBox { Name = "sleepGoalTextBox", Location = new Point(10, 130), Size = new Size(220, 20) };

        var okButton = new Button { Text = "OK", Location = new Point(10, 170), Size = new Size(80, 25) };
        okButton.Click += (sender, e) =>
        {
            if (decimal.TryParse(activityGoalTextBox.Text, out decimal a) &&
                decimal.TryParse(nutritionGoalTextBox.Text, out decimal n) &&
                decimal.TryParse(sleepGoalTextBox.Text, out decimal s))
            {
                ActivityGoal = a;
                NutritionGoal = n;
                SleepGoal = s;
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите корректные числовые значения целей.");
            }
        };

        var cancelButton = new Button { Text = "Отмена", Location = new Point(150, 170), Size = new Size(80, 25) };
        cancelButton.Click += (sender, e) =>
        {
            DialogResult = DialogResult.Cancel;
            Close();
        };

        this.Controls.Add(activityLabel);
        this.Controls.Add(activityGoalTextBox);
        this.Controls.Add(nutritionLabel);
        this.Controls.Add(nutritionGoalTextBox);
        this.Controls.Add(sleepLabel);
        this.Controls.Add(sleepGoalTextBox);
        this.Controls.Add(okButton);
        this.Controls.Add(cancelButton);
    }
}

public class ProgressForm : Form
{
    public ProgressForm(List<ProgressItem> items, List<string> recommendations)
    {
        this.Text = "Прогресс и рекомендации";
        this.Width = 420;
        this.Height = 400;

        int y = 10;
        foreach (var item in items)
        {
            var label = new Label
            {
                Text = $"{item.Name}: {item.Current:0.#} из {item.Goal:0.#} {item.Unit} ({item.Percent}%)",
                Location = new Point(10, y),
                AutoSize = true
            };
            var bar = new ProgressBar
            {
                Location = new Point(10, y + 20),
                Size = new Size(380, 20),
                Minimum = 0,
                Maximum = 100,
                Value = Math.Max(0, Math.Min(100, item.Percent))
            };
            this.Controls.Add(label);
            this.Controls.Add(bar);
            y += 55;
        }

        var recLabel = new Label { Text = "Рекомендации:", Location = new Point(10, y), AutoSize = true };
        var recBox = new RichTextBox
        {
            Name = "recommendationsBox",
            Location = new Point(10, y + 20),
            Size = new Size(380, 120),
            ReadOnly = true
        };
        foreach (var r in recommendations)
            recBox.AppendText("• " + r + "\n");

        this.Controls.Add(recLabel);
        this.Controls.Add(recBox);
    }
}

public class HealthForm : Form
{
    private HealthManager healthManager;
    private Button trackActivityButton;
    private Button trackNutritionButton;
    private Button trackSleepButton;
    private Button displayReportButton;
    private Button setGoalsButton;
    private Button progressButton;

    public HealthForm()
    {
        this.Text = "Управление здоровьем";
        this.Width = 360;
        this.Height = 200;
        healthManager = new HealthManager();
        CreateControls();
    }

    private void CreateControls()
    {
        trackActivityButton = new Button { Location = new Point(10, 20), Text = "Отслеживать активность", Size = new Size(150, 30) };
        trackActivityButton.Click += (sender, e) =>
        {
            var activityForm = new ActivityForm();
            if (activityForm.ShowDialog() == DialogResult.OK)
                healthManager.TrackActivity(activityForm.ActivityType, activityForm.Duration);
        };

        trackNutritionButton = new Button { Location = new Point(170, 20), Text = "Отслеживать питание", Size = new Size(150, 30) };
        trackNutritionButton.Click += (sender, e) =>
        {
            var nutritionForm = new NutritionForm();
            if (nutritionForm.ShowDialog() == DialogResult.OK)
                healthManager.TrackNutrition(nutritionForm.FoodItem, nutritionForm.Calories);
        };

        trackSleepButton = new Button { Location = new Point(10, 60), Text = "Отслеживать сон", Size = new Size(150, 30) };
        trackSleepButton.Click += (sender, e) =>
        {
            var sleepForm = new SleepForm();
            if (sleepForm.ShowDialog() == DialogResult.OK)
                healthManager.TrackSleep(sleepForm.Date, sleepForm.Hours);
        };

        displayReportButton = new Button { Location = new Point(170, 60), Text = "Показать отчёт", Size = new Size(150, 30) };
        displayReportButton.Click += (sender, e) => healthManager.DisplayActivityReport();

        setGoalsButton = new Button { Location = new Point(10, 100), Text = "Установить цели", Size = new Size(150, 30) };
        setGoalsButton.Click += (sender, e) =>
        {
            var goalsForm = new GoalsForm();
            if (goalsForm.ShowDialog() == DialogResult.OK)
                healthManager.SetGoals(goalsForm.ActivityGoal, goalsForm.NutritionGoal, goalsForm.SleepGoal);
        };

        progressButton = new Button { Location = new Point(170, 100), Text = "Прогресс и рекомендации", Size = new Size(150, 30) };
        progressButton.Click += (sender, e) => healthManager.DisplayProgress();

        this.Controls.Add(trackActivityButton);
        this.Controls.Add(trackNutritionButton);
        this.Controls.Add(trackSleepButton);
        this.Controls.Add(displayReportButton);
        this.Controls.Add(setGoalsButton);
        this.Controls.Add(progressButton);
    }

    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new HealthForm());
    }
}