using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Threading;
using MvvmSampleApp.Core;
using MvvmSampleApp.ViewModels.Helpers;
using MvvmSampleApp.Models;

namespace MvvmSampleApp.ViewModels
{
    public class SomeSubViewModel : ViewModelBase
    {
        private readonly DispatcherTimer timer = new DispatcherTimer {
            Interval = TimeSpan.FromSeconds(1),
        };

        private BusyState busy = BusyState.NotBusy;
        public BusyState Busy
        {
            get { return busy; }
            set { SetProperty(ref busy, value); }
        }

        private string sampleText = string.Empty;
        public string SampleText
        {
            get { return sampleText; }
            set { SetProperty(ref sampleText, value); }
        }

        private int textFontSize = 1;
        public int MyFontSize
        {
            get { return textFontSize; }
            set { SetProperty(ref textFontSize, value); }
        }

        public SomeSubViewModel()
        {
            SampleText = "Loren Ipsum.";

            timer.Tick += (_, __) => { Busy = (Busy == BusyState.Busy) ? BusyState.NotBusy : BusyState.Busy; timer.Start(); };
            timer.Start();
        }
    }
}
