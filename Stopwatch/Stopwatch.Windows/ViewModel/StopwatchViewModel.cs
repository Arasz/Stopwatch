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

        private DispatcherTimer _timer;
        private StopwatchModel _stopwatchModel;

        public bool Running { get { return _stopwatchModel.Running; } }

        public int Hours
        {
            get
            {
                return _stopwatchModel.Elapsed.HasValue ? _stopwatchModel.Elapsed.Value.Hours : 0;
            }
        }
        public int Minutes
        {
            get
            {
                if (_stopwatchModel.Elapsed.HasValue)
                    return _stopwatchModel.Elapsed.Value.Minutes;
                else
                    return 0;
            }
        }
        public decimal Seconds
        {
            get
            {
                if (_stopwatchModel.Elapsed.HasValue)
                    return (decimal)_stopwatchModel.Elapsed.Value.Seconds+
                        (_stopwatchModel.Elapsed.Value.Milliseconds * .001M);
                else
                    return 0.0M;
            }
        }
        public int LapHours
        {
            get
            {
                if (_stopwatchModel.Elapsed.HasValue)
                    return _stopwatchModel.LapTime.Value.Hours;
                else
                    return 0;
            }
        }
        public int LapMinutes
        {
            get
            {
                if (_stopwatchModel.Elapsed.HasValue)
                    return _stopwatchModel.LapTime.Value.Minutes;
                else
                    return 0;
            }
        }
        public decimal LapSeconds
        {
            get
            {
                if (_stopwatchModel.Elapsed.HasValue)
                    return (decimal)_stopwatchModel.LapTime.Value.Seconds +
                        (_stopwatchModel.LapTime.Value.Milliseconds * .001M);
                else
                    return 0.0M;
            }
        }

        public void Start()
        {
            _stopwatchModel.Start();
        }

        public void Stop()
        {
            _stopwatchModel.Stop();
        }

        public void Reset()
        {
            bool running = Running;
            _stopwatchModel.Reset();
            if (running)
                Start();
        }

        public void Lap()
        {
           _stopwatchModel.Lap();
        }

        public StopwatchViewModel()
        {
            _stopwatchModel = new StopwatchModel();
            _stopwatchModel.LapTimeUpdated += _stopwatch_LapTimeUpdated;

            _timer = new DispatcherTimer();
            _timer.Tick += _timer_Tick;
            _timer.Interval = TimeSpan.FromMilliseconds(50);
            _timer.Start();
        }

        private int _lapLastHours;
        private int _lapLastMinutes;
        private decimal _lapLastSeconds;

        private void _stopwatch_LapTimeUpdated(object sender, LapEventArgs e)
        {
            if (_lapLastHours != LapHours)
            {
                _lapLastHours = LapHours;
                OnPropertyChanged("LapHours");
            }
            if (_lapLastMinutes != LapMinutes)
            {
                _lapLastMinutes = LapMinutes;
                OnPropertyChanged("LapMinutes");
            }
            if (_lapLastSeconds != LapSeconds)
            {
                _lapLastSeconds = LapSeconds;
                OnPropertyChanged("LapSeconds");
            }
        }

        private int _lastHours;
        private int _lastMinutes;
        private decimal _lastSeconds;
        private bool _lastRunning;

        private void _timer_Tick(object sender, object e)
        {
            if(_lastRunning != Running)
            {
                _lastRunning = Running;
                OnPropertyChanged("Running");
            }
            if (_lastHours != Hours)
            {
                _lastHours = Hours;
                OnPropertyChanged("Hours");
            }
            if (_lastMinutes != Minutes)
            {
                _lastMinutes = Minutes;
                OnPropertyChanged("Minutes");
            }
            if (_lastSeconds != Seconds)
            {
                _lastSeconds = Seconds;
                OnPropertyChanged("Seconds");
            }
        }
    }
}
