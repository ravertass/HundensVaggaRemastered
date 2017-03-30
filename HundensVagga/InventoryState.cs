using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundensVagga {
    public abstract class IInventoryState {
        abstract public void Update(InventoryUI inventory, InputManager inputManager);

        protected bool IsBagClicked(InventoryUI inventory, InputManager inputManager) {
            return inputManager.IsLeftButtonPressed() && inventory.BagRectangle().Contains(inputManager.GetMousePosition());
        }
    }

    public class InventoryStateUp : IInventoryState {
        public override void Update(InventoryUI inventory, InputManager inputManager) {
            if (IsBagClicked(inventory, inputManager))
                inventory.State = new InventoryStateGoingDown();
        }
    }

    public class InventoryStateDown : IInventoryState {
        public override void Update(InventoryUI inventory, InputManager inputManager) {
            if (IsBagClicked(inventory, inputManager))
                inventory.State = new InventoryStateGoingUp();
        }
    }

    public class InventoryStateGoingUp : IInventoryState {
        public override void Update(InventoryUI inventory, InputManager inputManager) {
            if (inventory.IsUp())
                inventory.State = new InventoryStateUp();
            else
                inventory.Y -= InventoryUI.Y_SPEED;
        }
    }

    public class InventoryStateGoingDown : IInventoryState {
        public override void Update(InventoryUI inventory, InputManager inputManager) {
            if (inventory.IsDown())
                inventory.State = new InventoryStateDown();
            else
                inventory.Y += InventoryUI.Y_SPEED;
        }
    }
}
