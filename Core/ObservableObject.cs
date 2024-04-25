using System.ComponentModel;
using System.Runtime.CompilerServices;

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
