﻿using HarmonyLib;
using Vintagestory.API.Common;
using Vintagestory.GameContent;

namespace PlaceEveryItem;

[HarmonyPatch(typeof(ItemScrapWeaponKit), nameof(ItemScrapWeaponKit.OnHeldInteractStart))]
public static class FixScrapWeaponKitGroundStoragePlacementPatch
{
    [HarmonyPrefix]
    public static bool Prefix(ItemSlot slot, EntityAgent byEntity, BlockSelection blockSel, EntitySelection entitySel, bool firstEvent, ref EnumHandHandling handling)
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

        behavior.OnHeldInteractStart(slot, byEntity, blockSel, null, true, ref handling, ref _handling);

        return false;
    }
}