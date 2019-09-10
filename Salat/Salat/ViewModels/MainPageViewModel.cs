using Newtonsoft.Json;
using Salat.DependencyServices;
using Salat.Models;
using Salat.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using Xamarin.Forms;

namespace Salat.ViewModels
{
    enum EnumSalat
    {
        NONE = -1,
        FAJIR = 0,
        SHROUK = 1,
        ZOHOR = 2,
        ASSR = 3,
        MOGHRIB = 4,
        ISHAA = 5
    }
    public class MainPageViewModel : BaseViewModel
    {

        private String _nextSalatName = "";
        private TimeSpan _nextSalatTime;
        private EnumSalat _nextSalat = EnumSalat.NONE;

        private ILocalNotificationService notification;

        public ObservableCollection<SalatTime> Items { get; set; }

        private SalatTime currentDay;
        public SalatTime CurrentDay { get => currentDay; set => SetProperty<SalatTime>(ref currentDay, value); }

        private String timeRemained;
        public String TimeRemained { get => timeRemained; set => SetProperty<String>(ref timeRemained, value); }

        private String nextSalat;
        public String NextSalat { get => nextSalat; set => SetProperty<String>(ref nextSalat, value); }
        public MainPageViewModel()
        {
            Title = "Salat";
            var data = Encoding.ASCII.GetString(Resources.timetable);
            Items = JsonConvert.DeserializeObject<ObservableCollection<SalatTime>>(data);

            DateTime thisTime = DateTime.Now;
            TimeZoneInfo tst = TimeZoneInfo.FindSystemTimeZoneById("Asia/Beirut");
            bool isDaylight = tst.IsDaylightSavingTime(thisTime);

            TimeSpan oneHour = new TimeSpan(1, 0, 0);

            var item = Items.FirstOrDefault(s => s.milaidiDate.Date.Equals(DateTime.Now.Date));

            if (isDaylight)
            {
                item.fajir = item.fajir.Add(oneHour);
                item.shourouk = item.shourouk.Add(oneHour);
                item.zohor = item.zohor.Add(oneHour);
                item.assr = item.assr.Add(oneHour);
                item.moghrib = item.moghrib.Add(oneHour);
                item.ishaa = item.ishaa.Add(oneHour);
            }

            CurrentDay = item;

            notification = DependencyService.Get<ILocalNotificationService>();
           
            GetNextSalat();

            Timer timer = new Timer(x => { GetRemainedTime(); }, null, 1000 - DateTime.Now.TimeOfDay.Milliseconds, 1000);

            //MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            //{
            //    var newItem = item as Item;
            //    Items.Add(newItem);
            //    await DataStore.AddItemAsync(newItem);
            //});
        }

        public void GetNextSalat()
        {
            TimeSpan currentTime = DateTime.Now.TimeOfDay;



            if (currentTime < CurrentDay.ishaa)
            {
                _nextSalat = EnumSalat.ISHAA;
                _nextSalatName = "العشاء";
                _nextSalatTime = CurrentDay.ishaa;
            }

            if (currentTime < CurrentDay.moghrib)
            {
                _nextSalat = EnumSalat.MOGHRIB;
                _nextSalatName = "المغرب";
                _nextSalatTime = CurrentDay.moghrib;
            }

            if (currentTime < CurrentDay.assr)
            {
                _nextSalat = EnumSalat.ASSR;
                _nextSalatName = "العصر";
                _nextSalatTime = CurrentDay.assr;
            }

            if (currentTime < CurrentDay.zohor)
            {
                _nextSalat = EnumSalat.ZOHOR;
                _nextSalatName = "الظهر";
                _nextSalatTime = CurrentDay.zohor;
            }

            if (currentTime < CurrentDay.shourouk)
            {
                _nextSalat = EnumSalat.SHROUK;
                _nextSalatName = "الشروق";
                _nextSalatTime = CurrentDay.shourouk;
            }

            if (currentTime < CurrentDay.fajir)
            {
                _nextSalat = EnumSalat.FAJIR;
                _nextSalatName = "الفجر";
                _nextSalatTime = CurrentDay.fajir;
            }


            DateTime nextSalatDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, _nextSalatTime.Hours, _nextSalatTime.Minutes, 0);


            switch (_nextSalat)
            {
                case EnumSalat.FAJIR:
                    notification.LocalNotification("صلاتي", "حان موعد صلاة الفجر", 0, nextSalatDate);
                    break;
                case EnumSalat.SHROUK:
                    notification.LocalNotification("صلاتي", "حان موعد شروق الشمس", 1, nextSalatDate);
                    break;
                case EnumSalat.ZOHOR:
                    notification.LocalNotification("صلاتي", "حان موعد صلاة الظهر", 2, nextSalatDate);
                    break;
                case EnumSalat.ASSR:
                    notification.LocalNotification("صلاتي", "حان موعد صلاة العصر", 3, nextSalatDate);
                    break;
                case EnumSalat.MOGHRIB:
                    notification.LocalNotification("صلاتي", "حان موعد صلاة المغرب", 4, nextSalatDate);
                    break;
                case EnumSalat.ISHAA:
                    notification.LocalNotification("صلاتي", "حان موعد صلاة العشاء", 5, nextSalatDate);
                    break;
            }


        }

        public void GetRemainedTime()
        {
            TimeSpan currentTime = DateTime.Now.TimeOfDay;
            var remainedTime = _nextSalatTime - currentTime;
            NextSalat = $"الوقت المتبقي لصلاة {_nextSalatName}";
            TimeRemained = remainedTime.ToString(@"hh\:mm\:ss");




        }

    }
}
