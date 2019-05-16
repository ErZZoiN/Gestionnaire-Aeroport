using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Data;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Media;

namespace AeroportLibrary
{
    public class Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch(value)
            {
                case STATE.SCHEDULED: return Brushes.White;
                case STATE.BOARDING: return Brushes.Green;
                case STATE.LASTCALL: return Brushes.Orange;
                case STATE.GATE_CLOSED: return Brushes.Red;
                case STATE.AIRBORNE: return Brushes.Purple;
                case STATE.FLYING:
                default: return Brushes.Black;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
