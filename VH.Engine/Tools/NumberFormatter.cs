using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace VH.Engine.Tools {
    public class NumberFormatter {

        private static CultureInfo info = (CultureInfo)CultureInfo.CurrentCulture.Clone();

        static NumberFormatter() {
            info.NumberFormat.NumberDecimalSeparator = ".";
        }

        public static IFormatProvider NumberFormat {
            get { return info.NumberFormat; }
        }
    }
}
