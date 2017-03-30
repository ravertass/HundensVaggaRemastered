using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundensVagga {
    public abstract class IInventoryUIState {
        abstract public void Update(InventoryUI ui);
    }

    public class InventoryUIStill : IInventoryUIState {
        public override void Update(InventoryUI ui) {
            // do nothing
        }
    }    

    public class InventoryGoingUp : IInventoryUIState {
        public override void Update(InventoryUI ui) {
            if (ui.IsUp())
                ui.State = new InventoryUIStill();
            else
                ui.Y -= InventoryUI.Y_SPEED;
        }
    }

    public class InventoryGoingDown : IInventoryUIState {
        public override void Update(InventoryUI ui) {
            if (ui.IsDown())
                ui.State = new InventoryUIStill();
            else
                ui.Y += InventoryUI.Y_SPEED;
        }
    }
}
