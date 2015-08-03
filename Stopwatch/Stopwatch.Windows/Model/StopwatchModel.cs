using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stopwatch.Model
{
    class StopwatchModel
    {
        /// <summary>
        /// Time when stopwatch started work
        /// </summary>
        private DateTime? _started;

        /// <summary>
        /// Last measured time span.
        /// </summary>
        private TimeSpan? _previousElapsedTime;

        /// <summary>
        /// Indicates that stopwatch is running
        /// </summary>
        public bool Running{ get { return _started.HasValue; } }

        /// <summary>
        /// Total elapsed time 
        /// </summary>
        public TimeSpan? Elapsed
        {
            get
            {
                if (Running)
                {
                    if (_previousElapsedTime.HasValue)
                        return CalcualteTimeElapsedSinceStarted() + _previousElapsedTime;
                    else
                        return CalcualteTimeElapsedSinceStarted();
                }
                else
                    return _previousElapsedTime;
            }
        }

        /// <summary>
        /// Calculates time elapsed since stopwatch was started
        /// </summary>
        /// <returns>Time span from start to now</returns>
        private TimeSpan CalcualteTimeElapsedSinceStarted()
        {
            return DateTime.Now - _started.Value;
        }

        /// <summary>
        /// Starts stopwatch
        /// </summary>
        public void Start()
        {
            _started = DateTime.Now;
            if (!_previousElapsedTime.HasValue)
                _previousElapsedTime = new TimeSpan(0);
        }

        /// <summary>
        /// Stops stopwatch and increase previous elapsed time by elapsed time
        /// </summary>
        public void Stop()
        {
            if(Running)
                _previousElapsedTime += CalcualteTimeElapsedSinceStarted();
            _started = null;
        }

        /// <summary>
        /// Resets stopwatch
        /// </summary>
        public void Reset()
        {
            _previousElapsedTime = null;
            _started = null;
        }

        /// <summary>
        /// Resets stopwatch to it's default state
        /// </summary>
        public StopwatchModel()
        {
            Reset();
        }

    }
}
