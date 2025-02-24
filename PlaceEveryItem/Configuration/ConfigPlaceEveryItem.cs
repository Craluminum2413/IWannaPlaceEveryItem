using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Vintagestory.API.Common;
using Vintagestory.API.Util;

namespace PlaceEveryItem.Configuration;

public class ConfigPlaceEveryItem : IModConfig
{
    [JsonProperty(Order = 1)]
    public bool AutoFill { get; set; } = true;

    [JsonProperty(Order = 2)]
    public string Description => "AutoFill defaults entire config file every time you load any world. You need to disable it in order to make changes";

    [JsonProperty(Order = 3)]
    public List<string> BlacklistItems { get; set; } = new();

    [JsonProperty(Order = 4)]
    public List<string> BlacklistBlocks { get; set; } = new();

    [JsonProperty(Order = 5)]
    public GroundStorables Items { get; set; } = new();

    [JsonProperty(Order = 6)]
    public GroundStorables Blocks { get; set; } = new();

    public ConfigPlaceEveryItem(ICoreAPI api, ConfigPlaceEveryItem previousConfig = null)
    {
        if (previousConfig != null)
        {
            AutoFill = previousConfig.AutoFill;

            Items.SingleCenter.AddRange(previousConfig.Items.SingleCenter);
            Items.Halves.AddRange(previousConfig.Items.Halves);
            Items.WallHalves.AddRange(previousConfig.Items.WallHalves);
            Items.Quadrants.AddRange(previousConfig.Items.Quadrants);

            Blocks.SingleCenter.AddRange(previousConfig.Blocks.SingleCenter);
            Blocks.Halves.AddRange(previousConfig.Blocks.Halves);
            Blocks.WallHalves.AddRange(previousConfig.Blocks.WallHalves);
            Blocks.Quadrants.AddRange(previousConfig.Blocks.Quadrants);

            BlacklistItems.AddRange(previousConfig.BlacklistItems.Where(item => !BlacklistItems.Contains(item)));
            BlacklistBlocks.AddRange(previousConfig.BlacklistBlocks.Where(block => !BlacklistBlocks.Contains(block)));
        }

        if (api != null && AutoFill)
        {
            Items.SingleCenter.Clear();
            Items.Halves.Clear();
            Items.WallHalves.Clear();
            Items.Quadrants.Clear();

            Blocks.SingleCenter.Clear();
            Blocks.Halves.Clear();
            Blocks.WallHalves.Clear();
            Blocks.Quadrants.Clear();

            BlacklistItems.Clear();
            BlacklistBlocks.Clear();

            FillDefault(api);
        }
    }

    public void FillDefault(ICoreAPI api)
    {
        Core core = Core.GetInstance(api);

        Items.SingleCenter.AddRange(core.DefaultGroundStorableItems.SingleCenter);
        Items.Halves.AddRange(core.DefaultGroundStorableItems.Halves);
        Items.WallHalves.AddRange(core.DefaultGroundStorableItems.WallHalves);
        Items.Quadrants.AddRange(core.DefaultGroundStorableItems.Quadrants);

        Blocks.SingleCenter.AddRange(core.DefaultGroundStorableBlocks.SingleCenter);
        Blocks.Halves.AddRange(core.DefaultGroundStorableBlocks.Halves);
        Blocks.WallHalves.AddRange(core.DefaultGroundStorableBlocks.WallHalves);
        Blocks.Quadrants.AddRange(core.DefaultGroundStorableBlocks.Quadrants);

        BlacklistItems.AddRange(core.DefaultBlacklistItems.Where(item => !BlacklistItems.Contains(item)));
        BlacklistBlocks.AddRange(core.DefaultBlacklistBlocks.Where(block => !BlacklistBlocks.Contains(block)));
    }
}