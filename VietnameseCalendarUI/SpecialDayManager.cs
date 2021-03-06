﻿/*************************************************************
 * ===// The Vietnamese Calendar Project | 2014 - 2017 //=== *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 *  // Copyright (C) Augustine Bùi Nhã Đạt 2017      //      *
 * // Melbourne, December 2017                      //       *
 * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
 *              https://github.com/datbnh/SolarLunarCalendar *
 *************************************************************/

using Augustine.VietnameseCalendar.Core.LuniSolarCalendar;
using System;
using System.Collections.Generic;

namespace Augustine.VietnameseCalendar.UI
{
    public static class SpecialDayManager
    {
        public static readonly Dictionary<string, SpecialDateInfo> SpecialSolarDays = new Dictionary<string, SpecialDateInfo>()
        {
            { "0101", new SpecialDateInfo("Tết Dương Lịch", "🎆", DayType.SpecialLevel1) },
            { "1402", new SpecialDateInfo("Valentine", "❤", DayType.SpecialLevel2) },
            { "2512", new SpecialDateInfo("Giáng Sinh", "🎄", DayType.SpecialLevel1) },
        };

        public static readonly Dictionary<string, SpecialDateInfo> SpecialLunarDays = new Dictionary<string, SpecialDateInfo>()
        {
            { "0101", new SpecialDateInfo("Tết Nguyên Đán", "🎆", DayType.SpecialLevel1) },
            { "0201", new SpecialDateInfo("Mồng Hai Tết", "②", DayType.SpecialLevel2) },
            { "0301", new SpecialDateInfo("Mồng Ba Tết", "③", DayType.SpecialLevel3) },
            { "1501", new SpecialDateInfo("Rằm tháng Giêng", "⓯", DayType.SpecialLevel2) },
            { "1003", new SpecialDateInfo("Giỗ Tổ Hùng Vương", "", DayType.SpecialLevel2) },
            { "1504", new SpecialDateInfo("Phật Đản", "", DayType.SpecialLevel2) },
            { "0505", new SpecialDateInfo("Tết Đoan Ngọ", "", DayType.SpecialLevel2) },
            { "1507", new SpecialDateInfo("Vu Lan", "🌹",DayType.SpecialLevel2) },
            { "1508", new SpecialDateInfo("Tết Trung Thu", "🎑",DayType.SpecialLevel2) },
            { "2312", new SpecialDateInfo("Ông Táo Chầu Trời", "🐟",DayType.SpecialLevel2) },
        };

        /// <summary>
        /// Returns special day info of a luni-solar date.
        /// Returns null if there is nothing special.
        /// </summary>
        /// <param name="luniSolarDate"></param>
        /// <returns></returns>
        public static bool GetSpecialDateInfo(this LuniSolarDate<VietnameseLocalInfoProvider> luniSolarDate, out SpecialDateInfo specialDateInfo)
        {
            SpecialDateInfo spInfo = null;
            var key = GetSolarKey(luniSolarDate);
            if (SpecialSolarDays.ContainsKey(key))
            {
                spInfo = (SpecialDateInfo)SpecialSolarDays[key].Clone();
            }
            key = GetLunarKey(luniSolarDate);
            if (SpecialLunarDays.ContainsKey(key))
            {
                if (spInfo == null)
                {
                    spInfo = (SpecialDateInfo)SpecialLunarDays[key].Clone();
                }
                else
                {
                    spInfo.Label += "/" + SpecialLunarDays[key].Label;
                    spInfo.Decorator += SpecialLunarDays[key].Decorator;
                }
            }
            specialDateInfo = spInfo;
            if (spInfo == null)
                return false;
            else
                return true;
        }

        public class SpecialDateInfo : ICloneable
        {
            public string Label { get; set; }
            public string Decorator { get; set; }
            public DayType DayType { get; set; }
            public SpecialDateInfo(string label, string decorator = "", DayType dayType = DayType.SpecialLevel1)
            {
                Label = label;
                Decorator = decorator;
                DayType = dayType;
            }

            public object Clone()
            {
                return MemberwiseClone();
            }
        }

        public static string GetSolarKey(LuniSolarDate<VietnameseLocalInfoProvider> lsDate)
        {
            return string.Format("{0:ddMM}", lsDate.SolarDate);
        }

        public static string GetLunarKey(LuniSolarDate<VietnameseLocalInfoProvider> lsDate)
        {
            return string.Format("{0:00}{1:00}{2}", lsDate.Day, lsDate.Month, lsDate.IsLeapMonth ? "n" : "");
        }

        public static class SolarTermDecorator
        {
            public static readonly string VernalEquinox = "🌿";
            public static readonly string AutumnalEquinox = "🍃";
            public static readonly string SummerSolstice = "☀";
            public static readonly string WinterSolstice = "❄";
        }
    }
}
