﻿using System;

namespace Gucm.Data
{
    public class ChangeLog
    {
        public int ChangeLogID { get; set; }

        public string ClassName { get; set; }

        public string PropertyName { get; set; }

        public string Key { get; set; }

        public string OriginalValue { get; set; }

        public string CurrentValue { get; set; }

        public string UserName { get; set; }

        public DateTime? ChangeDate { get; set; }
    }
}
