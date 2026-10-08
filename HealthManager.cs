using System;
using System.Collections.Generic;
using System.Windows.Forms;

public class HealthManager
{
    private readonly Dictionary<string, decimal> activityTracking = new Dictionary<string, decimal>();
    private readonly Dictionary<string, decimal> nutritionTracking = new Dictionary<string, decimal>();
    private readonly Dictionary<string, decimal> sleepTracking = new Dictionary<string, decimal>();
    private readonly Action<string> notify;

    public HealthManager() : this(null) { }

    // notify позволяет в тестах подменить MessageBox
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
        if (hours <= 0 || hours > 24)
        {
            notify("Количество часов сна должно быть больше 0 и не больше 24.");
            return false;
        }

        sleepTracking[date] = hours;

        notify($"Сон на {date} отслежен: {hours} часов.");
        return true;
    }

    public void DisplayActivityReport()
    {
        var reportForm = new ReportForm();
        reportForm.ActivityTracking = activityTracking;
        reportForm.NutritionTracking = nutritionTracking;
        reportForm.SleepTracking = sleepTracking;
        reportForm.ShowDialog();
    }
}