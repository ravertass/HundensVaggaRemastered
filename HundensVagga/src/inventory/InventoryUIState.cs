﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HundensVagga {
    /// <summary>
    /// Used to manage when the inventory UI moves up and down.
    /// </summary>
    internal abstract class IInventoryUIState {
        abstract public void Update(InventoryUI ui);
    }

    internal class InventoryUIStill : IInventoryUIState {
        public override void Update(InventoryUI ui) {
            // do nothing
        }
    }    

    internal class InventoryGoingUp : IInventoryUIState {
        public override void Update(InventoryUI ui) {
            if (ui.IsUp())
                ui.State = new InventoryUIStill();
            else
                ui.Y -= InventoryUI.Y_SPEED;
        }
    }

    internal class InventoryGoingDown : IInventoryUIState {
        public override void Update(InventoryUI ui) {
            if (ui.IsDown())
                ui.State = new InventoryUIStill();
            else
                ui.Y += InventoryUI.Y_SPEED;
        }
    }
}
