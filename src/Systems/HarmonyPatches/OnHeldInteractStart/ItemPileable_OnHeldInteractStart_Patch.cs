using HarmonyLib;
using Vintagestory.API.Common;
using Vintagestory.GameContent;

namespace PlaceEveryItem;

public static class ItemPileable_OnHeldInteractStart_Patch
{
    [HarmonyPrefix]
    [HarmonyPatch(typeof(ItemPileable), nameof(ItemPileable.OnHeldInteractStart))]
    public static bool Prefix(ItemSlot slot, EntityAgent byEntity, BlockSelection blockSel, EntitySelection entitySel, bool firstEvent, ref EnumHandHandling handling)
    {
        return slot.OnHeldInteractStart(byEntity, blockSel, entitySel, firstEvent, ref handling);
    }
}
