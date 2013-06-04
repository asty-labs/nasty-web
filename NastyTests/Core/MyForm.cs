using System;
using Nasty.Core;
using EventArgs = Nasty.Core.EventArgs;

namespace NastyTests.Core
{
    [Serializable]
    public class MyForm : Form {

        public string Output;

        public void SuccessfulEvent(EventArgs e) {
            Output = e.SrcId + "/" + e["someParameter"];
        }

        public void ErroneousEvent(EventArgs e) {
            throw new Exception("some error");
        }
    }
}