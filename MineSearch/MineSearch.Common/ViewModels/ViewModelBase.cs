using System.ComponentModel;
using System.Runtime.CompilerServices;
using MineSearch.Common.Annotations;

namespace MineSearch.Common.ViewModels
{
    /// <summary>
    /// Base view model.
    /// </summary>
    public class ViewModelBase : INotifyPropertyChanged
    {
        #region Events

        /// <summary>
        /// Property changed event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
