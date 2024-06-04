namespace Sparta.BlazorUI.Permissions;

public class Permissions
{
    public class UserManagement
    {
        public const string View = "UserManagement.View";
        public const string Edit = "UserManagement.Edit";
        public const string Delete = "UserManagement.Delete";
        public const string Create = "UserManagement.Create";
    }

    public class HLLManagement
    {
        public const string View = "HLLManagement.View";
    }

    public class Configuration
    {
        public const string View = "Configuration.View";
        public const string Edit = "Configuration.Edit";
    }

    public class Discord
    {
        public const string View = "Discord.View";
        public const string Edit = "Discord.Edit";

        public class Embed
        {
            public const string View = "Discord.Embed.View";
            public const string Delete = "Discord.Embed.Delete";
            public const string Create = "Discord.Embed.Create";
            public const string Edit = "Discord.Embed.Edit";
        }

        public class Author
        {
            public const string View = "Discord.Author.View";
            public const string Delete = "Discord.Author.Delete";
            public const string Create = "Discord.Author.Create";
            public const string Edit = "Discord.Author.Edit";
        }

        public class Component
        {
            public const string View = "Discord.Component.View";
            public const string Delete = "Discord.Component.Delete";
            public const string Create = "Discord.Component.Create";
            public const string Edit = "Discord.Component.Edit";
        }

        public class Modal
        {
            public const string View = "Discord.Modal.View";
            public const string Delete = "Discord.Modal.Delete";
            public const string Create = "Discord.Modal.Create";
            public const string Edit = "Discord.Modal.Edit";
        }
    }
}