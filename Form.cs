using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

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
        activityTypeTextBox = new TextBox { Location = new Point(10, 30), Size = new Size(200, 20) };
        durationLabel = new Label { Text = "Продолжительность (минут):", Location = new Point(10, 55), AutoSize = true };
        durationTextBox = new TextBox { Location = new Point(10, 75), Size = new Size(200, 20) };

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
        foodItemTextBox = new TextBox { Location = new Point(10, 30), Size = new Size(200, 20) };
        caloriesLabel = new Label { Text = "Калорийность:", Location = new Point(10, 55), AutoSize = true };
        caloriesTextBox = new TextBox { Location = new Point(10, 75), Size = new Size(200, 20) };

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
        dateTextBox = new TextBox { Location = new Point(10, 30), Size = new Size(200, 20) };
        hoursLabel = new Label { Text = "Количество часов:", Location = new Point(10, 55), AutoSize = true };
        hoursTextBox = new TextBox { Location = new Point(10, 75), Size = new Size(200, 20) };

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

public class HealthForm : Form
{
    private HealthManager healthManager;
    private Button trackActivityButton;
    private Button trackNutritionButton;
    private Button trackSleepButton;
    private Button displayReportButton;

    public HealthForm()
    {
        this.Text = "Управление здоровьем";
        this.Width = 360;
        this.Height = 160;
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

        this.Controls.Add(trackActivityButton);
        this.Controls.Add(trackNutritionButton);
        this.Controls.Add(trackSleepButton);
        this.Controls.Add(displayReportButton);
    }

    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new HealthForm());
    }
}