using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundensVagga {
    internal class SubtitlePage {
        private String upperLIne;
        public String UpperLine {
            get { return upperLIne; }
        }

        private String lowerLine;
        public String LowerLine {
            get { return lowerLine; }
        }

        private double duration;
        public double Duration {
            get { return duration; }
        }

        public SubtitlePage(String upperLine, String lowerLine, double duration) {
            this.upperLIne = upperLine;
            this.lowerLine = lowerLine;
            this.duration = duration;
        }

        public bool HasLowerText() {
            return lowerLine != null;
        }
    }
}
