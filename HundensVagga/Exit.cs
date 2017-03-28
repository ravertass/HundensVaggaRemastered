using Microsoft.Xna.Framework;

namespace HundensVagga {
    public enum Direction {
        up,
        down,
        left,
        right
    }

    public class Exit {
        private readonly Rectangle rectangle;
        private readonly string room;
        private readonly Direction direction;

        public Exit(Rectangle rectangle, string room, Direction direction) {
            this.rectangle = rectangle;
            this.room = room;
            this.direction = direction;
        }
    }
}