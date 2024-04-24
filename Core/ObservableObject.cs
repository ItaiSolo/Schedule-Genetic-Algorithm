using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Core
{
    internal class ObservableObject : INotifyPropertyChanged //INotifyPropertyChanged is used to fire events when a property changes
    {
        public event PropertyChangedEventHandler PropertyChanged; //event that is fired when a property changes
        protected void OnPropertyChanged([CallerMemberName] string name = null) //CallerMemberName is used to get the name of the property that was changed
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name)); //invoke the event
        }
    }
}
