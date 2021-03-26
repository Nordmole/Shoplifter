﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StardewModdingAPI;
using StardewValley;
using StardewModdingAPI.Events;
using Harmony;

namespace Shoplifter
{
    public class ModEntry
        : Mod
    {
        public static bool StolenToday = false;

        public static bool CaughtToday = false;

        public static ArrayList ShopsBannedFrom = new ArrayList();
        public override void Entry(IModHelper helper)
        {
            ShopMenuPatcher.gethelpers(this.Monitor, this.Helper);
            ShopStock.gethelpers(this.Monitor, this.Helper);
            helper.Events.GameLoop.DayStarted += this.DayStarted;

            var harmony = HarmonyInstance.Create(this.ModManifest.UniqueID);
            ShopMenuPatcher.Hook(harmony, this.Monitor);
        }

        private void DayStarted(object sender, DayStartedEventArgs e)
        {
            // Reset stolentoday boolean so player can shoplift again when the new day starts
            StolenToday = false;
            CaughtToday = false;
            ShopsBannedFrom.Clear();
        }
    }
}