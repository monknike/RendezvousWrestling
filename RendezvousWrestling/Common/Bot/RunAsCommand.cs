﻿using FChatSharpLib.Entities.Plugin.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RendezvousWrestling.Common.Bot
{
    public class RunAsCommand<TFightingGame, TAchievement, TActionFactory, TActiveAction, TFeature, TFeatureFactory, TFight, TFighterState, TModifier, TUser, OptionalParameterType> : BaseCommand<TFightingGame>
    where TActionFactory : IActionFactory<TAchievement, TActionFactory, TActiveAction, TFeature, TFeatureFactory, TFight, TFighterState, TModifier, TUser, OptionalParameterType>, new()
    where TFeature : BaseFeature<TAchievement, TActionFactory, TActiveAction, TFeature, TFeatureFactory, TFight, TFighterState, TModifier, TUser, OptionalParameterType>, new()
    where TFeatureFactory : IFeatureFactory<TAchievement, TActionFactory, TActiveAction, TFeature, TFeatureFactory, TFight, TFighterState, TModifier, TUser, OptionalParameterType>, new()
    where TUser : BaseUser<TAchievement, TActionFactory, TActiveAction, TFeature, TFeatureFactory, TFight, TFighterState, TModifier, TUser, OptionalParameterType>, new()
    where TFight : BaseFight<TAchievement, TActionFactory, TActiveAction, TFeature, TFeatureFactory, TFight, TFighterState, TModifier, TUser, OptionalParameterType>, new()
    where TFighterState : BaseFighterState<TAchievement, TActionFactory, TActiveAction, TFeature, TFeatureFactory, TFight, TFighterState, TModifier, TUser, OptionalParameterType>, new()
    where TActiveAction : BaseActiveAction<TAchievement, TActionFactory, TActiveAction, TFeature, TFeatureFactory, TFight, TFighterState, TModifier, TUser, OptionalParameterType>, new()
    where OptionalParameterType : BaseFeatureParameter<TAchievement, TActionFactory, TActiveAction, TFeature, TFeatureFactory, TFight, TFighterState, TModifier, TUser, OptionalParameterType>, new()
    where TAchievement : BaseAchievement<TAchievement, TActionFactory, TActiveAction, TFeature, TFeatureFactory, TFight, TFighterState, TModifier, TUser, OptionalParameterType>, new()
    where TModifier : BaseModifier<TAchievement, TActionFactory, TActiveAction, TFeature, TFeatureFactory, TFight, TFighterState, TModifier, TUser, OptionalParameterType>, new()
    where TFightingGame : BaseFightingGame<TAchievement, TActionFactory, TActiveAction, TFeature, TFeatureFactory, TFight, TFighterState, TModifier, TUser, OptionalParameterType>, new()

    {

        public override void ExecuteCommand(string characterCalling, IEnumerable<string> args, string channel)
        {
            if (this.Plugin.FChatClient.IsUserMaster(characterCalling))
            {
                var command = args.First();
                this.Plugin.ExecuteCommand(Plugin.DebugImpersonatedCharacter, command, args.Skip(1), channel);
            }
        }
    }
}
