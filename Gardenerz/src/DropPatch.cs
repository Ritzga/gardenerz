using System;
using HarmonyLib;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;
using Vintagestory.GameContent;

namespace Gardenerz;

[HarmonyPatch]
public class DropPatch
{
    
    [HarmonyPostfix]
    [HarmonyPatch(typeof (BlockCrop), nameof(BlockCrop.GetDrops))]
    public static ItemStack[] PatchDrop(ItemStack[] __result, IWorldAccessor world, BlockPos pos, IPlayer byPlayer, float dropQuantityMultiplier)
    {
        var classSystem = byPlayer.Entity.Api?.ModLoader?.GetModSystem<CharacterSystem>();
        if (classSystem?.HasTrait(byPlayer, "green-fingers") ?? false)
        {
            var multiplier = (float)classSystem.TraitsByCode["green-fingers"].Attributes["cropMultiplier"];
            var dropRate = (float)classSystem.TraitsByCode["green-fingers"].Attributes["cropDropRate"];
            
            foreach (var stack in __result)
            {
                stack.StackSize += Random.Shared.NextSingle() > dropRate ? 0 : 1;
                stack.StackSize = (int) MathF.Round(stack.StackSize * multiplier, MidpointRounding.ToEven);
            }
        }
        return __result;
    }
}