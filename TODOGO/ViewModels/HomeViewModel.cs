using DevExpress.Mvvm;
using System;
using System.Collections.ObjectModel;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using System.Linq;
using System.Drawing;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System.Collections.Generic;

namespace TODOGO;

public class HomeViewModel:ViewModelBase
{
    public ObservableCollection<TaskViewModel> Tasks { get; set; }
        
    public ISeries[] Series { get; set; } 
    public Axis[] AxesDate { get; set; }
    public List<Axis> AxesCount { get; set; }
    public int[] CountFineshedTasks { get; set; }
    public HomeViewModel(ObservableCollection<TaskViewModel> tasks) 
    {
        Tasks = tasks;
        CountFineshedTasks = TaskManager.GetCountFinishedTasks(tasks);
        Series = new ISeries[]
        {
            new LineSeries<int>
            {
                Values = CountFineshedTasks,
                Stroke = new SolidColorPaint(SKColors.Red) { StrokeThickness = 4 },
                GeometryStroke = new SolidColorPaint(SKColors.OrangeRed) { StrokeThickness = 4 },
                Fill = null,
                Name = "Выполнено"
            }
        };

        AxesDate = new Axis[]
        {
            new Axis
            {
                Labels = TaskManager.GetDateWithFinishedTasks(tasks).Select(x => x.ToString("d.MM.yyyy")).ToArray()
            }
        };

        AxesCount = new List<Axis>
        {
             new Axis
             {
                    MinStep= 1
             }
        };
        
    }
}

