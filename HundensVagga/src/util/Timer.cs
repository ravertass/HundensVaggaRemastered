using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundensVagga {
    class Timer {
        private double elapsedTime;
        private double maxTime;

        public Timer(double time) {
            elapsedTime = 0;
            maxTime = time;
        }

        public void Update(GameTime gameTime) {
            double delta = gameTime.ElapsedGameTime.TotalSeconds;
            elapsedTime += delta;
        }
        
        public bool IsDone() {
            return elapsedTime >= maxTime;
        }

        public void Stop() {
            elapsedTime = maxTime;
        }
    }
}
