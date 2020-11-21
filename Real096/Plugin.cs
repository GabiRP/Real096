using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Features;
using Exiled.API.Interfaces;
using Exiled.Events.EventArgs;
using MEC;

namespace Real096
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public float CalmTime { get; set; } = 10;
    }
    public class Plugin : Plugin<Config>
    {
        public override string Prefix => "real096";
        public override string Name => "Real096";
        public override string Author => "xRoier";
        public EventHandlers EventHandlers;
        public override Version Version { get; } = new Version(1, 0, 0);
        public override Version RequiredExiledVersion { get; } = new Version(2, 1, 3);
        public override void OnEnabled()
        {
            base.OnEnabled();
            EventHandlers = new EventHandlers(this);

            Exiled.Events.Handlers.Player.Dying += EventHandlers.OnPlayerDeath;
        }
        public override void OnDisabled()
        {
            base.OnDisabled();

            Exiled.Events.Handlers.Player.Dying -= EventHandlers.OnPlayerDeath;
            EventHandlers = null;
        }
        public override void OnReloaded() { } 
        
    }
    public class EventHandlers
    {
        private Plugin plugin;
        public EventHandlers(Plugin plugin) { this.plugin = plugin; }
        

        internal void OnPlayerDeath(DyingEventArgs ev)
        {
            if(ev.Killer.Role == RoleType.Scp096)
            {
                var s096 = PlayableScps.Scp096.Get096FromPlayerObject(ev.Killer.GameObject);
                if (!s096._targets.Contains(ev.Target.ReferenceHub))
                {
                    ev.IsAllowed = false;
                }
                
            }
        }
        
    }
}
