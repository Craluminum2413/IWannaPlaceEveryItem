using HarmonyLib;
using Vintagestory.API.Common;
using Vintagestory.GameContent;

namespace PlaceEveryItem;

public static class ItemRustyGear_OnHeldInteractStart_Patch
{
    [HarmonyPrefix]
    [HarmonyPatch(typeof(ItemRustyGear), nameof(ItemRustyGear.OnHeldInteractStart))]
    public static bool Prefix(ItemSlot slot, EntityAgent byEntity, BlockSelection blockSel, EntitySelection entitySel, bool firstEvent, ref EnumHandHandling handling)
    {
        return slot.OnHeldInteractStart(byEntity, blockSel, entitySel, firstEvent, ref handling);
    }
}
