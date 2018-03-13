﻿using System;
using Avalonia.Input;
using Avalonia.Threading;

namespace Avalonia.Controls
{
    public class RepeatButton : Button
    {
        /// <summary>
        /// Defines the <see cref="Delay"/> property.
        /// </summary>
        public static readonly StyledProperty<int> DelayProperty =
            AvaloniaProperty.Register<Button, int>(nameof(Delay), 100);

        /// <summary>
        /// Defines the <see cref="InitialDelay"/> property.
        /// </summary>
        public static readonly StyledProperty<int> InitialDelayProperty =
            AvaloniaProperty.Register<Button, int>(nameof(InitialDelay), 300);

        private DispatcherTimer _repeatTimer;

        /// <summary>
        /// Gets or sets the amount of time, in milliseconds, of repeating clicks.
        /// </summary>
        public int Delay
        {
            get { return GetValue(DelayProperty); }
            set { SetValue(DelayProperty, value); }
        }

        /// <summary>
        /// Gets or sets the amount of time, in milliseconds, to wait before repeating begins.
        /// </summary>
        public int InitialDelay
        {
            get { return GetValue(InitialDelayProperty); }
            set { SetValue(InitialDelayProperty, value); }
        }

        private void StartTimer()
        {
            if (_repeatTimer == null)
            {
                _repeatTimer = new DispatcherTimer();
                _repeatTimer.Tick += RepeatTimerOnTick;
            }

            if (_repeatTimer.IsEnabled) return;

            _repeatTimer.Interval = TimeSpan.FromMilliseconds(InitialDelay);
            _repeatTimer.Start();
        }

        private void RepeatTimerOnTick(object sender, EventArgs e)
        {
            var interval = TimeSpan.FromMilliseconds(Delay);
            if (_repeatTimer.Interval != interval)
            {
                _repeatTimer.Interval = interval;
            }
            OnClick();
        }

        private void StopTimer()
        {
            _repeatTimer?.Stop();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.Key == Key.Space)
            {
                StartTimer();
            }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);

            StopTimer();
        }

        protected override void OnPointerPressed(PointerPressedEventArgs e)
        {
            base.OnPointerPressed(e);

            if (e.MouseButton == MouseButton.Left)
            {
                StartTimer();
            }
        }

        protected override void OnPointerReleased(PointerReleasedEventArgs e)
        {
            base.OnPointerReleased(e);

            if (e.MouseButton == MouseButton.Left)
            {
                StopTimer();
            }
        }
    }
}