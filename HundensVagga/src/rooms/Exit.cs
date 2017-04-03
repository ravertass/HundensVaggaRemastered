using Microsoft.Xna.Framework;

namespace HundensVagga {
    /// <summary>
    /// For knowing which direction the cursor should point when hovering over an exit.
    /// </summary>
    public enum Direction {
        up,
        down,
        left,
        right
    }

    /// <summary>
    /// An exit in a room.
    /// </summary>
    public class Exit {
        private readonly Rectangle rectangle;
        public Rectangle Rectangle {
            get { return rectangle; }
        }
        private readonly string roomName;
        public string RoomName {
            get { return roomName; }
        }
        private readonly Direction direction;
        public Direction Direction {
            get { return direction; }
        }

        public Exit(Rectangle rectangle, string room, Direction direction) {
            this.rectangle = rectangle;
            this.roomName = room;
            this.direction = direction;
        }
    }
}