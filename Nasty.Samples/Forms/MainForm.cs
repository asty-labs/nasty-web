using Nasty.Core;
using Nasty.Components;
using System;

namespace Nasty.Samples.Forms 
{
    [Serializable]
    public class MainForm : Form {

        private readonly int _randomNumber = new Random().Next(100);
        private int _numberOfTries;

        public MainForm()
        {
            LowerBound = 1;
            UpperBound = 100;
        }

        public override object PrepareModel() {
            return "";
        }

        public void StartNewGame() {
            ReplaceWith(new MainForm());
        }

        public void ProcessGuess(JQuery stats, TextBox guessEntryField)
        {
            int guess;
            try {
                guess = int.Parse(guessEntryField.Value);
            } catch (FormatException) {
                var statusLabel = Query<JQuery>("#stats .status");
                statusLabel.Text("Your guess was not valid.");
                return;
            }


            ++_numberOfTries;

            if (guess == _randomNumber) {
                ReplaceWith(new CongratulationForm(_numberOfTries));
                return;
            }

            guessEntryField.Value = "";

            var statusText = "";

            if (guess < 1 || guess > 100) {
                statusText = "Your guess, " + guess + " was not between 1 and 100.";
            } else if (guess < _randomNumber) {
                if (guess >= LowerBound) {
                    LowerBound = guess + 1;
                }
                statusText = "Your guess, " + guess + " was too low.  Try again:";
            } else if (guess > _randomNumber) {
                statusText = "Your guess, " + guess + " was too high.  Try again:";
                if (guess <= UpperBound) {
                    UpperBound = guess - 1;
                }
            }

            stats.Html(RenderFragment("stats", statusText));
        }

        public int LowerBound
        {
            get;
            private set;
        }

        public int UpperBound
        {
            get;
            private set;
        }

        public String Counter {
            get
            {
                // Update number of tries label.
                if (_numberOfTries == 0)
                    return "You have made no guesses";
                if (_numberOfTries == 1)
                    return "You have made 1 guess.";
                return "You have made " + _numberOfTries + " guesses.";
            }
        }

    }
}