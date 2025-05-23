﻿using HarmonyLib;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.API.Server;

namespace Gardenerz
{
    /// <summary>
    /// 
    /// </summary>
    [HarmonyPatch]
    public class Core : ModSystem
    {
        public static string id = "gardenerz";
        private ICoreAPI api;
        private Harmony harmony;
        
        // Called on server and client
        // Useful for registering block/entity classes on both sides
        public override void Start(ICoreAPI api)
        {
            this.api = api;
            api.Logger.Notification("gardenerz:init");
            
            harmony = new Harmony(id);
            harmony.PatchAll();
            
            api.Logger.Notification(api is ICoreServerAPI ? Lang.Get("gardenerz:init-server-loaded") : Lang.Get("gardenerz:init-client-loaded"));
            
            //adds new behavior
            //api.RegisterBlockBehaviorClass("Moving", typeof(BlockWorkBox));
            
            //adds new block
        } 

        public override void StartServerSide(ICoreServerAPI api)
        {
            api.Logger.Notification(Lang.Get("gardenerz:init-server"));
            //Easter egg for my bear <3
            api.Event.PlayerJoin += CheckBearIsJoined;
        }

        public override void StartClientSide(ICoreClientAPI api)
        {
            api.Logger.Notification(Lang.Get("gardenerz:init-client"));
        }
        
        public override void Dispose() 
        {
            harmony.UnpatchAll(id);
        }
        
        /// <summary>
        /// Easter egg function for my bear
        /// </summary>
        /// <param name="player"></param>
        private void CheckBearIsJoined(IServerPlayer player)
        {
            //if (player.PlayerName == "Syd")
            //{
            //    player.SendMessage(GlobalConstants.GeneralChatGroup, "Hewwo cute honey bear <3~", EnumChatType.CommandSuccess);
            //}
        }
    }
}