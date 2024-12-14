using System;
using HarmonyLib;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;
using Vintagestory.GameContent;

namespace Gardenerz;

[HarmonyPatch]
public class DropPatch
{
    const int maxAdditionalDrop = 3;
    
    [HarmonyPostfix]
    [HarmonyPatch(typeof (BlockCrop), nameof(BlockCrop.GetDrops))]
    public static ItemStack[] PatchDrop(ItemStack[] __result, IWorldAccessor world, BlockPos pos, IPlayer byPlayer, float dropQuantityMultiplier, BlockCrop __instance)
    {
        if (__instance.CurrentCropStage > __instance.CropProps.GrowthStages - 2)
        {
            var classSystem = byPlayer.Entity.Api?.ModLoader?.GetModSystem<CharacterSystem>();
            if (classSystem?.HasTrait(byPlayer, "green-fingers") ?? false)
            {
                var multiplier = (float)classSystem.TraitsByCode["green-fingers"].Attributes["cropMultiplier"];
                var dropRate = (float)classSystem.TraitsByCode["green-fingers"].Attributes["cropDropRate"];
            
                foreach (var stack in __result)
                {
                    var add = 0;
                    while (Random.Shared.NextSingle() < dropRate && add < maxAdditionalDrop)
                    {
                        add++;
                    }
                    stack.StackSize += add;
                    stack.StackSize = (int) MathF.Round(stack.StackSize * multiplier, MidpointRounding.ToEven);
                }
            }
        }
        return __result;
    }
}