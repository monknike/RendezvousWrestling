using RendezvousWrestling.Common.DataContext;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public abstract class BaseFighterStats<TFightingGame, TAchievement, TActionFactory, TActiveAction, TFeature, TFeatureFactory, TFight, TFighterState, TFighterStats, TModifier, TUser, OptionalParameterType> : BaseEntity
    where TActionFactory : IActionFactory<TFightingGame, TAchievement, TActionFactory, TActiveAction, TFeature, TFeatureFactory, TFight, TFighterState, TFighterStats, TModifier, TUser, OptionalParameterType>, new()
    where TFeature : BaseFeature<TFightingGame, TAchievement, TActionFactory, TActiveAction, TFeature, TFeatureFactory, TFight, TFighterState, TFighterStats, TModifier, TUser, OptionalParameterType>, new()
    where TFeatureFactory : IFeatureFactory<TFightingGame, TAchievement, TActionFactory, TActiveAction, TFeature, TFeatureFactory, TFight, TFighterState, TFighterStats, TModifier, TUser, OptionalParameterType>, new()
    where TUser : BaseUser<TFightingGame, TAchievement, TActionFactory, TActiveAction, TFeature, TFeatureFactory, TFight, TFighterState, TFighterStats, TModifier, TUser, OptionalParameterType>, new()
    where TFight : BaseFight<TFightingGame, TAchievement, TActionFactory, TActiveAction, TFeature, TFeatureFactory, TFight, TFighterState, TFighterStats, TModifier, TUser, OptionalParameterType>, new()
    where TFighterState : BaseFighterState<TFightingGame, TAchievement, TActionFactory, TActiveAction, TFeature, TFeatureFactory, TFight, TFighterState, TFighterStats, TModifier, TUser, OptionalParameterType>, new()
    where TActiveAction : BaseActiveAction<TFightingGame, TAchievement, TActionFactory, TActiveAction, TFeature, TFeatureFactory, TFight, TFighterState, TFighterStats, TModifier, TUser, OptionalParameterType>, new()
    where OptionalParameterType : BaseFeatureParameter<TFightingGame, TAchievement, TActionFactory, TActiveAction, TFeature, TFeatureFactory, TFight, TFighterState, TFighterStats, TModifier, TUser, OptionalParameterType>, new()
    where TAchievement : BaseAchievement<TFightingGame, TAchievement, TActionFactory, TActiveAction, TFeature, TFeatureFactory, TFight, TFighterState, TFighterStats, TModifier, TUser, OptionalParameterType>, new()
    where TModifier : BaseModifier<TFightingGame, TAchievement, TActionFactory, TActiveAction, TFeature, TFeatureFactory, TFight, TFighterState, TFighterStats, TModifier, TUser, OptionalParameterType>, new()
    where TFighterStats : BaseFighterStats<TFightingGame, TAchievement, TActionFactory, TActiveAction, TFeature, TFeatureFactory, TFight, TFighterState, TFighterStats, TModifier, TUser, OptionalParameterType>, new()
    where TFightingGame : BaseFightingGame<TFightingGame, TAchievement, TActionFactory, TActiveAction, TFeature, TFeatureFactory, TFight, TFighterState, TFighterStats, TModifier, TUser, OptionalParameterType>, new()
{

    [Key, ForeignKey("User")]
    public string UserName { get; set; }

    public virtual TUser User { get; set; }

    public int fightsCount { get; set; }
    public int fightsCountCS { get; set; }
    public int losses { get; set; }
    public int lossesSeason { get; set; }
    public int wins { get; set; }
    public int winsSeason { get; set; }
    public int currentlyPlaying { get; set; }
    public int currentlyPlayingSeason { get; set; }
    public int fightsPendingReady { get; set; }
    public int fightsPendingReadySeason { get; set; }
    public int fightsPendingStart { get; set; }
    public int fightsPendingStartSeason { get; set; }
    public int fightsPendingDraw { get; set; }
    public int fightsPendingDrawSeason { get; set; }
    public Team favoriteTeam { get; set; }
    public string favoriteTagPartner { get; set; }
    public int timesFoughtWithFavoriteTagPartner { get; set; }
    public string nemesis { get; set; }
    public int lossesAgainstNemesis { get; set; }
    public int averageDiceRoll { get; set; }
    public int missedAttacks { get; set; }
    public int actionsCount { get; set; }
    public int actionsDefended { get; set; }
    public int matchesInLast24Hours { get; set; }
    public int matchesInLast48Hours { get; set; }
    public int eloRating { get; set; } = 2000;
    public int globalRank { get; set; }
    public int forfeits { get; set; }
    public int quits { get; set; }

    public double winrate
    {
        get
        {
            var winRate = 0.00;
            if (this.fightsCount > 0 && this.wins > 0)
            {
                winRate = this.fightsCount / this.wins;
            }
            else if (this.fightsCount > 0 && this.losses > 0)
            {
                winRate = 1 - this.fightsCount / this.losses;
            }
            return winRate;
        }
    }
}