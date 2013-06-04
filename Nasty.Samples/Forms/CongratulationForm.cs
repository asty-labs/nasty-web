using Nasty.Core;
using System;
namespace Nasty.Samples.Forms 
{
    [Serializable]
    public class CongratulationForm : Form {

        private readonly int _numberOfTries;

        public CongratulationForm(int numberOfTries) {
            _numberOfTries = numberOfTries;
        }

        public override object PrepareModel() {
            return _numberOfTries;
        }

        public void StartNewGame(Core.EventArgs e)
        {
            ReplaceWith(new MainForm());
        }
    }
}