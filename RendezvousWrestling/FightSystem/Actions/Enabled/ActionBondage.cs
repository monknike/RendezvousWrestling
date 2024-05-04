using Microsoft.CodeAnalysis.CSharp.Syntax;
using RendezvousWrestling.Common.Actions;
using RendezvousWrestling.Common.Constants;
using RendezvousWrestling.Configuration;
using RendezvousWrestling.FightSystem.Configuration;
using RendezvousWrestling.FightSystem.Features;
using RendezvousWrestling.FightSystem.Modifiers;
using System;
using System.Collections.Generic;
using static System.Formats.Asn1.AsnWriter;

namespace RendezvousWrestling.FightSystem.Actions.Enabled
{
    public class ActionBondage : RWActiveAction
    {

        public ActionBondage() : base(
                RWActionType.Bondage,
                nameof(ActionType.Bondage), //Name
               false, //isHold
            true,  //requiresRoll
            false, //keepActorsTurn
            true,  //singleTarget
            true,  //requiresBeingAlive
            false, //requiresBeingDead
            true,  //requiresBeingInRing
            false, //requiresBeingOffRing
                false,  //requiresTier
                false, //requiresCustomTarget
            true,  //targetMustBeAlive
            false, //targetMustBeDead
            true, //targetMustBeInRing
            false,  //targetMustBeOffRing
            true, //targetMustBeInRange
            false, //targetMustBeOffRange
            false, //requiresBeingInHold,
            false, //requiresNotBeingInHold,
            true, //targetMustBeInHold,
            false, //targetMustNotBeInHold,
            false, //usableOnSelf
            false,  //usableOnAllies
            true, //usableOnEnemies
                TriggerEvent.SpecialAttack,
                ActionExplanation.Bondage)
        {

        }

        public override int RequiredDiceScore // I know that this is super bad code, but basically, I wanted the required dice roll before DEXDIFF/FPDIFF to be 10, and it wasn't doing it, so I did this code :'D
        {
            
            get
            { 
                AddRequiredScoreWithExplanation(5, "BDG ATTEMPT");
                return base.RequiredDiceScore+5;
            }
        }
        public override int AddBonusesToRollFromStats()
        {
            return base.AddBonusesToRollFromStats() + (int)Math.Ceiling(this.Attacker.CurrentSensuality / 20m);
        }

        public override void OnHit()
        {
            this.FpHealToAtk += GetIntValueForEnumByTier(typeof(FocusHealOnHit), Tier.Heavy);
            this.FpDamageToDef += GetIntValueForEnumByTier(typeof(FocusDamageOnHit), Tier.Heavy);

            var bdModifier = Fight.ModifierFactory.Build(RWModifierType.Bondage, this.Fight, this.Defender, this.Attacker);
            AddModifier(bdModifier);
        }
    }
}