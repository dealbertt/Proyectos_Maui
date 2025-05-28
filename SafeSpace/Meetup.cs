using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeSpace
{
    public class Meetup : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public DateTime Time { get; set; }
        public string Place { get; set; }
        public string Description { get; set; }

        public string Image { get; set; }

        private bool _isJoined;
        public bool isJoined
        {
            get => _isJoined;
            set
            {
                if (_isJoined != value)
                {
                    _isJoined = value;
                    OnPropertyChanged(nameof(isJoined));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
