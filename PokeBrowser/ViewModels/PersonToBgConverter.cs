using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;
using PokeBrowser.Data;

namespace PokeBrowser.ViewModels
{
    public class PersonToBgConverter : IValueConverter
    {
        public PersonalityParameter Value { get; set; }
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str)
            {
                var person = DataBaseService.DataBase.FindPersonality(str);

                if (person != null)
                {
                    if (person.Up == Value)
                        return Brushes.IndianRed;
                    if (person.Down == Value)
                        return Brushes.SkyBlue;
                }
                
            }

            return Brushes.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class PersonToBgExtension : MarkupExtension
    {
        public PersonalityParameter Person { get; set; }
        
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return new PersonToBgConverter()
            {
                Value = Person
            };
        }
    }
        
}