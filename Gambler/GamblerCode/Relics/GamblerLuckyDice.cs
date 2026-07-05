using Gambler.GamblerCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.Rooms;

namespace Gambler.GamblerCode.Relics;

public class GamblerLuckyDice () : GamblerRelic()
{
    public override RelicRarity Rarity => RelicRarity.Common;

    protected IEnumerable<DynamicVar> DynamicVar=> [new DynamicVar("Power", 25M)];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => new IHoverTip[1]
    {
        HoverTipFactory.FromPower<GamblerLuckPower>()
    };

    public override async Task AfterRoomEntered(AbstractRoom room)
    {
        GamblerLuckyDice dice = this;
        if (!(room is CombatRoom))
            return;
        GamblerLuckPower luckyDice = await PowerCmd.Apply<GamblerLuckPower>((PlayerChoiceContext) new ThrowingPlayerChoiceContext(), dice.Owner.Creature, 25, dice.Owner.Creature, (CardModel) null);
    }
}