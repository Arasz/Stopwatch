using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Stopwatch.Model;
using System.ComponentModel;

namespace Stopwatch.ViewModel
{
    class StopwatchViewModel:INotifyPropertyChanged
    {
        #region INotifyPropertChangedImplementation
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name)
        {
            var propertyChanged = PropertyChanged;
            if (propertyChanged != null)
                propertyChanged(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        private int _ticksCounter; // Functionality not implemented
        private DispatcherTimer _timer;
        private StopwatchModel _stopwatch;

        public int Hours
        {
            get
            {
                if (_stopwatch.Running)
                    return _stopwatch.Elapsed.Value.Hours;
                else
                    return 0;
            }
        }
        public int Minutes
        {
            get
            {
                if (_stopwatch.Running)
                    return _stopwatch.Elapsed.Value.Minutes;
                else
                    return 0;
            }
        }
        public int Seconds
        {
            get
            {
                if (_stopwatch.Running)
                    return _stopwatch.Elapsed.Value.Seconds;
                else
                    return 0;
            }
        }
        public int LapHours
        {
            get
            {
                if (_stopwatch.Running)
                    return _stopwatch.LapTime.Value.Hours;
                else
                    return 0;
            }
        }
        public int LapMinutes
        {
            get
            {
                if (_stopwatch.Running)
                    return _stopwatch.LapTime.Value.Minutes;
                else
                    return 0;
            }
        }
        public int LapSeconds
        {
            get
            {
                if (_stopwatch.Running)
                    return _stopwatch.LapTime.Value.Seconds;
                else
                    return 0;
            }
        }

        public void Start()
        {
            _stopwatch.Start();
            _timer.Start();
        }

        public void Stop()
        {
            _stopwatch.Stop();
            _timer.Stop();
        }

        public void Reset()
        {
            _stopwatch.Reset();
            _timer.Stop();
            TimeChanged();
            LapChanged();
        }

        public void Lap()
        {
            _stopwatch.Lap();
        }

        private void LapChanged()
        {
            OnPropertyChanged("LapHours");
            OnPropertyChanged("LapMinutes");
            OnPropertyChanged("LapSeconds");
        }

        private void TimeChanged()
        {
            OnPropertyChanged("Hours");
            OnPropertyChanged("Minutes");
            OnPropertyChanged("Seconds");
        }

        public StopwatchViewModel()
        {
            _stopwatch = new StopwatchModel();
            _stopwatch.LapTimeUpdated += _stopwatch_LapTimeUpdated;

            _timer = new DispatcherTimer();
            _timer.Tick += _timer_Tick;
            _timer.Interval = TimeSpan.FromMilliseconds(1000);

            _ticksCounter = 0;
        }

        private void _stopwatch_LapTimeUpdated(object sender, LapEventArgs e)
        {
            LapChanged();
        }

        private void _timer_Tick(object sender, object e)
        {
            TimeChanged();
        }
    }
}
