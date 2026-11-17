using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using System.Linq;
using static HealthForm;

public class HealthManager
{
    private readonly Dictionary<string, decimal> activityTracking =
        new Dictionary<string, decimal>(StringComparer.CurrentCultureIgnoreCase);
    private readonly Dictionary<string, decimal> nutritionTracking =
        new Dictionary<string, decimal>(StringComparer.CurrentCultureIgnoreCase);
    private readonly Dictionary<string, decimal> sleepTracking = new Dictionary<string, decimal>();
    private readonly Action<string> notify;

    public HealthManager() : this(null) { }

    public HealthManager(Action<string> notify)
    {
        this.notify = notify ?? (message => MessageBox.Show(message));
    }

    public IReadOnlyDictionary<string, decimal> Activities { get { return activityTracking; } }
    public IReadOnlyDictionary<string, decimal> Nutrition { get { return nutritionTracking; } }
    public IReadOnlyDictionary<string, decimal> Sleep { get { return sleepTracking; } }

    public bool TrackActivity(string activityType, decimal duration)
    {
        if (string.IsNullOrWhiteSpace(activityType))
        {
            notify("Введите тип активности.");
            return false;
        }
        if (duration <= 0)
        {
            notify("Продолжительность должна быть больше нуля.");
            return false;
        }

        activityType = activityType.Trim();
        if (activityTracking.ContainsKey(activityType))
            activityTracking[activityType] += duration;
        else
            activityTracking.Add(activityType, duration);

        notify($"Активность '{activityType}' отслежена на {duration} минут.");
        return true;
    }

    public bool TrackNutrition(string foodItem, decimal calories)
    {
        if (string.IsNullOrWhiteSpace(foodItem))
        {
            notify("Введите название пищи.");
            return false;
        }
        if (calories < 0)
        {
            notify("Калорийность не может быть отрицательной.");
            return false;
        }

        foodItem = foodItem.Trim();
        if (nutritionTracking.ContainsKey(foodItem))
            nutritionTracking[foodItem] += calories;
        else
            nutritionTracking.Add(foodItem, calories);

        notify($"Пища '{foodItem}' отслежена: {calories} калорий.");
        return true;
    }

    public bool TrackSleep(string date, decimal hours)
    {
        if (string.IsNullOrWhiteSpace(date))
        {
            notify("Введите дату.");
            return false;
        }

        DateTime parsed;
        if (!DateTime.TryParseExact(date.Trim(), new[] { "dd.MM.yyyy", "d.M.yyyy" },
                CultureInfo.InvariantCulture, DateTimeStyles.None, out parsed))
        {
            notify("Введите дату в формате ДД.ММ.ГГГГ.");
            return false;
        }

        if (hours <= 0 || hours > 24)
        {
            notify("Количество часов сна должно быть больше 0 и не больше 24.");
            return false;
        }

        date = parsed.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture);
        sleepTracking[date] = hours;

        notify($"Сон на {date} отслежен: {hours} часов.");
        return true;
    }

    // ===== Цели и рекомендации =====
    private decimal activityGoal, nutritionGoal, sleepGoal;

    public bool GoalsSet { get; private set; }
    public decimal ActivityGoal { get { return activityGoal; } }
    public decimal NutritionGoal { get { return nutritionGoal; } }
    public decimal SleepGoal { get { return sleepGoal; } }

    public decimal TotalActivity { get { return activityTracking.Values.Sum(); } }
    public decimal TotalCalories { get { return nutritionTracking.Values.Sum(); } }
    public decimal AverageSleep
    {
        get { return sleepTracking.Count == 0 ? 0 : sleepTracking.Values.Average(); }
    }

    public bool SetGoals(decimal activityMinutes, decimal calories, decimal sleepHours)
    {
        if (activityMinutes <= 0)
        {
            notify("Цель по активности должна быть больше нуля.");
            return false;
        }
        if (calories <= 0)
        {
            notify("Цель по питанию должна быть больше нуля.");
            return false;
        }
        if (sleepHours <= 0 || sleepHours > 24)
        {
            notify("Цель по сну должна быть больше 0 и не больше 24 часов.");
            return false;
        }

        activityGoal = activityMinutes;
        nutritionGoal = calories;
        sleepGoal = sleepHours;
        GoalsSet = true;

        notify("Цели установлены.");
        return true;
    }

    public List<ProgressItem> GetProgress()
    {
        var list = new List<ProgressItem>();
        if (!GoalsSet) return list;

        list.Add(new ProgressItem { Name = "Активность", Current = TotalActivity, Goal = activityGoal, Unit = "мин" });
        list.Add(new ProgressItem { Name = "Питание", Current = TotalCalories, Goal = nutritionGoal, Unit = "ккал" });
        list.Add(new ProgressItem { Name = "Сон", Current = AverageSleep, Goal = sleepGoal, Unit = "ч" });
        return list;
    }

    public List<string> GetRecommendations()
    {
        var result = new List<string>();
        if (!GoalsSet)
        {
            result.Add("Сначала установите цели.");
            return result;
        }

        if (TotalActivity < activityGoal)
            result.Add($"Добавьте физической активности: до цели не хватает {activityGoal - TotalActivity} мин.");
        else
            result.Add("Цель по активности выполнена.");

        if (TotalCalories > nutritionGoal)
            result.Add($"Калорийность превышает цель на {TotalCalories - nutritionGoal} ккал: сократите калорийность рациона.");
        else
            result.Add("Калорийность в пределах цели.");

        if (sleepTracking.Count == 0)
            result.Add("Добавьте данные о сне, чтобы получить рекомендацию.");
        else if (AverageSleep < sleepGoal)
            result.Add($"Увеличьте продолжительность сна: в среднем {AverageSleep:0.#} ч при цели {sleepGoal} ч.");
        else
            result.Add("Цель по сну выполнена.");

        return result;
    }

    public void DisplayProgress()
    {
        if (!GoalsSet)
        {
            notify("Сначала установите цели.");
            return;
        }
        new ProgressForm(GetProgress(), GetRecommendations()).ShowDialog();
    }

    public void DisplayActivityReport()
    {
        var reportForm = new ReportForm();
        reportForm.ActivityTracking = activityTracking;
        reportForm.NutritionTracking = nutritionTracking;
        reportForm.SleepTracking = sleepTracking;
        reportForm.ShowDialog();
    }

    public class ProgressItem
    {
        public string Name { get; set; }
        public decimal Current { get; set; }
        public decimal Goal { get; set; }
        public string Unit { get; set; }

        public int Percent
        {
            get { return Goal <= 0 ? 0 : (int)Math.Round(Current / Goal * 100); }
        }
    }
}