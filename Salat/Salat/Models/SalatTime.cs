using System;
using System.Collections.Generic;
using System.Text;

namespace Salat.Models
{
    public class SalatTime
    {
        public TimeSpan assr{ get; set; }
        public String day{ get; set; }
        public String description{ get; set; }
        public TimeSpan fajir { get; set; }
        public int hijjriDay{ get; set; }
        public int hijjriMonth{ get; set; }
        public String hijjriMonthName{ get; set; }
        public int hijjriYear{ get; set; }
        public int id{ get; set; }
        public TimeSpan ishaa { get; set; }
        public DateTime milaidiDate{ get; set; }
        public TimeSpan moghrib { get; set; }
        public TimeSpan shourouk { get; set; }
        public TimeSpan zohor { get; set; }

        public String Fajir
        {
            get
            {
                return fajir.ToString(@"hh\:mm");
            }
            set
            {
                fajir = TimeSpan.Parse(value);
            }
        }

        public String Shourouk
        {
            get
            {
                return shourouk.ToString(@"hh\:mm");
            }
            set
            {
                shourouk = TimeSpan.Parse(value);
            }
        }

        public String Zohor
        {
            get
            {
                return zohor.ToString(@"hh\:mm");
            }
            set
            {
                zohor = TimeSpan.Parse(value);
            }
        }

        public String Assr
        {
            get
            {
                return assr.ToString(@"hh\:mm");
            }
            set
            {
                assr = TimeSpan.Parse(value);
            }
        }

        public String Moghrib
        {
            get
            {
                return moghrib.ToString(@"hh\:mm");
            }
            set
            {
                moghrib = TimeSpan.Parse(value);
            }
        }

        public String Ishaa
        {
            get
            {
                return ishaa.ToString(@"hh\:mm");
            }
            set
            {
                ishaa = TimeSpan.Parse(value);
            }
        }

    }
}
