﻿using System;
using System.Collections.Generic;
using System.Globalization;

namespace ExplicitAboutDateTime
{
    public class LocationDateTime
    {
        private DateTimeOffset internalDateTimeOffset;

        public static Dictionary<string, TimeZoneInfo> LocationTimezoneMap = new Dictionary<string, TimeZoneInfo>()
        {
            { "TRV", TimeZoneInfo.FindSystemTimeZoneById("India Standard Time") },
            { "SYD", TimeZoneInfo.FindSystemTimeZoneById("AUS Eastern Standard Time") },
            { "SEA", TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time") }
        };

        private LocationDateTime(DateTimeOffset dateTimeOffset)
        {
            this.internalDateTimeOffset = dateTimeOffset;
        }

        public static bool TryCreateDateFromUTC(string dateCandidate, out LocationDateTime date)
        {
            date = null;
            DateTimeOffset temp;
            var isDate = DateTimeOffset.TryParse(dateCandidate, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out temp);
            var isOnlyUTCDate = isDate && temp.TimeOfDay.TotalMilliseconds == 0 && temp.Offset.TotalMilliseconds == 0;

            if (isOnlyUTCDate)
                date = new LocationDateTime(temp);

            return isOnlyUTCDate;
        }

        public override bool Equals(object obj)
        {
            var objAsLocationDateTime = obj as LocationDateTime;
            if (objAsLocationDateTime == null || this == null)
                return false;

            return this.internalDateTimeOffset.Equals(objAsLocationDateTime.internalDateTimeOffset);
        }

        public override int GetHashCode()
        {
            return this.internalDateTimeOffset == null
                ? 0
                : this.internalDateTimeOffset.GetHashCode();
        }
    }
}