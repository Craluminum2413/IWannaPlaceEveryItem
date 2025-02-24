﻿using HarmonyLib;
using Vintagestory.API.Common;
using Vintagestory.GameContent;

namespace PlaceEveryItem;

[HarmonyPatch(typeof(ItemCattailRoot), nameof(ItemCattailRoot.OnHeldInteractStart))]
public static class FixReedRootsGroundStoragePlacementPatch
{
    [HarmonyPrefix]
    public static bool Prefix(ItemSlot itemslot, EntityAgent byEntity, BlockSelection blockSel, EntitySelection entitySel, bool firstEvent, ref EnumHandHandling handHandling)
    {
        if (blockSel == null || byEntity == null)
        {
            return true;
        }

        bool pressedCtrlKey = byEntity.Controls.CtrlKey;
        CollectibleBehaviorGroundStorable behavior = byEntity.ActiveHandItemSlot?.GetGroundStorableBehavior();
        bool requiresCtrlKey = behavior?.StorageProps.CtrlKey == true;

        if (behavior == null || !behavior.CanFixPlacement() || (requiresCtrlKey && !pressedCtrlKey))
        {
            return true;
        }

        EnumHandling _handling = EnumHandling.PassThrough;

        behavior.OnHeldInteractStart(itemslot, byEntity, blockSel, null, true, ref handHandling, ref _handling);

        return false;
    }
}
