﻿using HarmonyLib;
using ValheimPlus.Configurations;
using System.Collections.Generic;

namespace ValheimPlus.GameClasses
{
    [HarmonyPatch(typeof(Humanoid), "GetCurrentWeapon")]
    public static class ModifyCurrentWeapon
    {
        private static ItemDrop.ItemData Postfix(ItemDrop.ItemData __weapon, ref Character __instance)
        {
            if (Configuration.Current.Player.IsEnabled)
            {
                if (__weapon != null)
                {
                    if (__weapon.m_shared.m_name == "Unarmed")
                    {
                        Player CharacterPlayerInstance = (Player)__instance;
                        __weapon.m_shared.m_damages.m_blunt = CharacterPlayerInstance.GetSkillFactor(Skills.SkillType.Unarmed) * Configuration.Current.Player.baseUnarmedDamage;
                    }
                }
            }

            return __weapon;
        }
    }

    /// <summary>
    /// When equipping a one-handed weapon, also equip best shield from inventory.
    /// </summary>
    [HarmonyPatch(typeof(Humanoid), "EquipItem")]
    public static class Humanoid_EquipItem_Patch
    {
        private static bool Postfix(bool __result, Humanoid __instance, ItemDrop.ItemData item)
        {
            if (Configuration.Current.Player.IsEnabled &&
                Configuration.Current.Player.autoEquipShield &&
                __result && 
                __instance.IsPlayer() && 
                __instance.m_rightItem?.m_shared.m_itemType == ItemDrop.ItemData.ItemType.OneHandedWeapon &&
                item.m_shared.m_itemType != ItemDrop.ItemData.ItemType.Shield)
            {
                List<ItemDrop.ItemData> inventoryItems = __instance.m_inventory.GetAllItems();

                ItemDrop.ItemData bestShield = null;
                foreach (ItemDrop.ItemData inventoryItem in inventoryItems)
                {
                    if (inventoryItem.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Shield)
                    {
                        if (bestShield == null)
                        {
                            bestShield = inventoryItem;

                            continue;
                        }

                        if (bestShield.m_shared.m_blockPower < inventoryItem.m_shared.m_blockPower)
                        {
                            bestShield = inventoryItem;

                            continue;
                        }
                    }
                }

                if (bestShield != null)
                {
                    __instance.EquipItem(bestShield, false);
                }
            }

            return __result;
        }
    }

    public static class UpdateEquipmentState
    {
        public static bool shouldReequipItemsAfterSwimming = false;
    }

    [HarmonyPatch(typeof(Humanoid), "UpdateEquipment")]
    public static class Humanoid_UpdateEquipment_Patch
    {
        private static bool Prefix(Humanoid __instance)
        {
            if (!Configuration.Current.Player.IsEnabled || !Configuration.Current.Player.reequipItemsAfterSwimming)
                return true;

            if (__instance.IsPlayer() && __instance.IsSwiming() && !__instance.IsOnGround())
            {
                // The above is only enough to know we will eventually exit swimming, but we still don't know if the items were visible prior or not.
                // We only want to re-show them if they were shown to begin with, so we need to check.
                // This is also why this must be a prefix patch; in a postfix patch, the items are already hidden, and we don't know
                // if they were hidden by UpdateEquipment or by the user far earlier.

                if (__instance.m_leftItem != null || __instance.m_rightItem != null)
                    UpdateEquipmentState.shouldReequipItemsAfterSwimming = true;
            }
            else if (__instance.IsPlayer() && !__instance.IsSwiming() && __instance.IsOnGround() && UpdateEquipmentState.shouldReequipItemsAfterSwimming)
            {
                __instance.ShowHandItems();
                UpdateEquipmentState.shouldReequipItemsAfterSwimming = false;
            }

            return true;
        }
    }
}
