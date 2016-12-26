using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace NoFlare
{
  public class NoFlareMerchant : GlobalNPC
  {
    public override void SetupShop(int type, Chest shop, ref int nextSlot)
    {
      if (type != NPCID.Merchant) return;
      shop.item[nextSlot++].SetDefaults(930);
    }
  }
}
