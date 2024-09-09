﻿using Sparta.Modules.Dto;

namespace Sparta.Modules.Interface.ModuleParameters
{
    public class DiscordRoleParameter : ModuleParameterBase
    {
        public override ParameterType GetParameterType()
        {
            return ParameterType.DiscordRole;
        }
    }
}
