using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.API.Datastructures;
using Vintagestory.API.MathTools;

namespace Gardenerz.Gui;

public class GuiDialogBlockEntityWormBox : GuiDialogBlockEntity
{
    public GuiDialogBlockEntityWormBox(string dialogTitle, InventoryBase Inventory, BlockPos BlockEntityPosition,
        SyncedTreeAttribute tree, ICoreClientAPI capi)
        : base(dialogTitle, Inventory, BlockEntityPosition, capi)
    {
        if (IsDuplicate) return;
        tree.OnModified.Add(new TreeModifiedListener { listener = OnAttributesModified } );
        Attributes = tree;
    }
    
    private void OnAttributesModified()
    {
        if (!IsOpened()) return;

        float ftemp = Attributes.GetFloat("furnaceTemperature");
        float otemp = Attributes.GetFloat("oreTemperature");

        string fuelTemp = ftemp.ToString("#");
        string oreTemp = otemp.ToString("#");

        fuelTemp += fuelTemp.Length > 0 ? "°C" : "";
        oreTemp += oreTemp.Length > 0 ? "°C" : "";

        if (ftemp > 0 && ftemp <= 20) fuelTemp = Lang.Get("Cold");
        if (otemp > 0 && otemp <= 20) oreTemp = Lang.Get("Cold");

        SingleComposer.GetDynamicText("fueltemp").SetNewText(fuelTemp);
        SingleComposer.GetDynamicText("oretemp").SetNewText(oreTemp);

        //if (capi.ElapsedMilliseconds - lastRedrawMs > 500)
        //{
        //    if (SingleComposer != null) SingleComposer.GetCustomDraw("symbolDrawer").Redraw();
        //    lastRedrawMs = capi.ElapsedMilliseconds;
        //}
    }
}