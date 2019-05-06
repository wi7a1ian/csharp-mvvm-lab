using System.Linq;
using System.Collections.Generic;
using MvvmSampleApp.Core;
using MvvmSampleApp.ViewModels.Helpers;

namespace MvvmSampleApp.ViewModels
{
    public class SampleControlViewModel : BaseViewModel
    {
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

        public SampleControlViewModel()
        {
            SampleText = "Loren Ipsum.\nLoren Ipsum.\nLoren Ipsum.\nLoren Ipsum.\n";
        }
    }
}
