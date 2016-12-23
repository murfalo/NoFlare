using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.ModLoader;
using Terraria.World.Generation;

namespace NoFlare
{
  public class NoFlare : Mod
  {
    public NoFlare()
    {
      Properties = new ModProperties() { Autoload = true };
    }
  }

  public class NoFlareWorld : ModWorld
  {
    private const int FlareGun = 930;
    private static readonly Random R = new Random();
    private static readonly int[] GoldChestRareItems =
    {
      49,  // Band of Regeneration
      50,  // Magic Mirror
      53,  // Cloud in a Bottle
      54,  // Hermes Boots
      55,  // Enchanted Boomerang
      975, // Shoe Spikes
      997  // Extractinator
    };
    private static readonly int[] GoldChestItems =
    {
      40, // Wooden Arrow
      42, // Shuriken
    };
    private static int GoldChestRareItem => GoldChestRareItems[R.Next(GoldChestRareItems.Length)];
    private static int GoldChestItem => GoldChestItems[R.Next(GoldChestRareItems.Length)];

    public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
    {
      var LastIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Final Cleanup"));
      if (LastIndex != -1)
      {
        tasks.Insert(LastIndex - 1, new PassLegacy("Remove Flare Guns", delegate (GenerationProgress progress)
        {
          progress.Message = "Removing flare guns from chests";

          foreach (var c in Main.chest)
          {
            if (c?.item[0]?.netID != FlareGun) continue;
            c.item[0].SetDefaults(GoldChestRareItem);
            c.item[0].Prefix(-1);
            c.item[1].SetDefaults(GoldChestItem);
            c.item[1].Prefix(-1);
          }
        }));
      }
    }
  }
}