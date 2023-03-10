using DevExpress.Mvvm;
using System.Threading.Tasks;
using System.Windows.Input;
using System;
using System.Windows;
using Newtonsoft.Json;

namespace TODOGO;

public class SettingViewModel: ViewModelBase
{
    [JsonProperty("AutoSaving")]
    public bool AutoSaving { get; set; } = true;
    [JsonProperty("UserTelegramI")]
    public string UserTelegramId { get; set; }
    [JsonProperty("TelegramToken")]
    public string TelegramToken { get; set; }
}