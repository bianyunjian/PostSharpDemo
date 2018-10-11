using PostSharp.Patterns.Diagnostics;
using PostSharp.Patterns.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostSharpSample.PropertyChanged
{
    public class PropertyChange
    {
        public static void Test()
        {
            var user = new UserModel();
            (user as INotifyChildPropertyChanged).PropertyChanged += PropertyChange_PropertyChanged;

            user.UserName = "byj";
            user.Props = new List<string>() { "Prop1", "Prop2" };
        }


        private static void PropertyChange_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var type = sender.GetType();
            var value = type.GetProperty(e.PropertyName).GetValue(sender);

            Logger.GetLogger().Write(LogLevel.Trace, "PropertyChange_PropertyChanged. " + e.PropertyName + "==>" + value.ToString());

        }
          
    }

    [NotifyPropertyChanged]
    public class UserModel
    {
        public string UserName { get; set; }
        public int Age { get; set; }
        public List<string> Props { get; set; }
    }
}

