using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReCallVocabulary.Controls
{
    public class FormDatePicker : Microsoft.Maui.Controls.DatePicker, IDatePicker
    {
        DateTime IDatePicker.Date
        {
            get => Date;
            set
            {
                Date = value;
                OnPropertyChanged(nameof(Date));
            }
        }
    }
}
