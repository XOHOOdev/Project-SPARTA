namespace Sparta.Modules.HllServerStatus.Templates
{
    public static class StaticResources
    {
        public static Dictionary<string, string> Graphics;
        public static List<string> SovietMaps;
        public static List<string> BritainMaps;

        static StaticResources()
        {
            Graphics = new Dictionary<string, string>
            {
                { "allied", "<:allies:1050391368406667294>" },
                { "axis", "<:achsen:1050391399654248479>" },
                { "soviet", "<:soviet:1087494789966082098>" },
                { "british", "<:british:1182431903270314136>" },
                { "error", "<:error:1084589482566754364>" },

                {
                    "error_uri",
                    @"https://upload.wikimedia.org/wikipedia/commons/thumb/8/8f/Flat_cross_icon.svg/1200px-Flat_cross_icon.svg.png"
                },
                { "taurus", @"https://qinaii.de/taurus/img/gsstatus/BullHead.png" },

                { "Carentan Warfare", @"https://qinaii.de/taurus/img/gsstatus/carentan.jpg" },
                { "Carentan Warfare (Night)", @"https://qinaii.de/taurus/img/gsstatus/carentannight.jpg" },
                { "Carentan Offensive (US)", @"https://qinaii.de/taurus/img/gsstatus/carentanusus.jpg" },
                { "Carentan Offensive (GER)", @"https://qinaii.de/taurus/img/gsstatus/carentanger.jpg" },

                { "Driel Warfare", @"https://qinaii.de/taurus/img/gsstatus/driel.jpg" },
                { "Driel Warfare (Night)", @"https://qinaii.de/taurus/img/gsstatus/drielnight.jpg" },
                { "Driel Offensive (UK)", @"https://qinaii.de/taurus/img/gsstatus/drieluk.jpg" },
                { "Driel Offensive (GER)", @"https://qinaii.de/taurus/img/gsstatus/drielger.jpg" },
                { "Driel Skirmish (Dawn)", @"https://qinaii.de/taurus/img/gsstatus/drielskirmishday.jpg" },
                { "Driel Skirmish (Night)", @"https://qinaii.de/taurus/img/gsstatus/drielskirmishnight.jpg" },
                { "Driel Skirmish", @"https://qinaii.de/taurus/img/gsstatus/drielskirmishday.jpg" },


                { "El Alamein Warfare", @"https://qinaii.de/taurus/img/gsstatus/elalamein.jpg" },
                { "El Alamein Warfare (Night)", @"https://qinaii.de/taurus/img/gsstatus/elalameinnight.jpg" },
                { "El Alamein Offensive (UK)", @"https://qinaii.de/taurus/img/gsstatus/elalameinuk.jpg" },
                { "El Alamein Offensive (GER)", @"https://qinaii.de/taurus/img/gsstatus/elalameinger.jpg" },
                { "El Alamein Skirmish", @"https://qinaii.de/taurus/img/gsstatus/elalameinskirmish.jpg" },
                { "El Alamein Skirmish (Dusk)", @"https://qinaii.de/taurus/img/gsstatus/elalameinskirmishnight.jpg" },

                { "Foy Warfare", @"https://qinaii.de/taurus/img/gsstatus/foy.jpg" },
                { "Foy Warfare (Night)", @"https://qinaii.de/taurus/img/gsstatus/foynight.jpg" },
                { "Foy Offensive (US)", @"https://qinaii.de/taurus/img/gsstatus/foyus.jpg" },
                { "Foy Offensive (GER)", @"https://qinaii.de/taurus/img/gsstatus/foyger.jpg" },

                { "Hill 400 Warfare", @"https://qinaii.de/taurus/img/gsstatus/hill400.jpg" },
                { "Hill 400 Warfare (Night)", @"https://qinaii.de/taurus/img/gsstatus/hill400night.jpg" },
                { "Hill 400 Offensive (US)", @"https://qinaii.de/taurus/img/gsstatus/hill400us.jpg" },
                { "Hill 400 Offensive (GER)", @"https://qinaii.de/taurus/img/gsstatus/hill400ger.jpg" },

                { "Hürtgen Forest Warfare", @"https://qinaii.de/taurus/img/gsstatus/hurtgen.jpg" },
                { "Hürtgen Forest Warfare (Night)", @"https://qinaii.de/taurus/img/gsstatus/hurtgennight.jpg" },
                { "Hürtgen Forest Offensive (US)", @"https://qinaii.de/taurus/img/gsstatus/hurtgenus.jpg" },
                { "Hürtgen Forest Offensive (GER)", @"https://qinaii.de/taurus/img/gsstatus/hurtgenger.jpg" },

                { "Kharkov Warfare", @"https://qinaii.de/taurus/img/gsstatus/kharkov.jpg" },
                { "Kharkov Warfare (Night)", @"https://qinaii.de/taurus/img/gsstatus/kharkovnight.jpg" },
                { "Kharkov Offensive (RUS)", @"https://qinaii.de/taurus/img/gsstatus/kharkovrus.jpg" },
                { "Kharkov Offensive (GER)", @"https://qinaii.de/taurus/img/gsstatus/kharkovger.jpg" },

                { "Kursk Warfare", @"https://qinaii.de/taurus/img/gsstatus/kursk.jpg" },
                { "Kursk Warfare (Night)", @"https://qinaii.de/taurus/img/gsstatus/kursknight.jpg" },
                { "Kursk Offensive (RUS)", @"https://qinaii.de/taurus/img/gsstatus/kurskrus.jpg" },
                { "Kursk Offensive (GER)", @"https://qinaii.de/taurus/img/gsstatus/kurskger.jpg" },

                { "Omaha Beach Warfare", @"https://qinaii.de/taurus/img/gsstatus/omaha.jpg" },
                { "Omaha Beach Warfare (Night)", @"https://qinaii.de/taurus/img/gsstatus/omahanight.jpg" },
                { "Omaha Beach Offensive (US)", @"https://qinaii.de/taurus/img/gsstatus/omahaus.jpg" },
                { "Omaha Beach Offensive (GER)", @"https://qinaii.de/taurus/img/gsstatus/omahager.jpg" },

                { "Purple Heart Lane Warfare", @"https://qinaii.de/taurus/img/gsstatus/phl.jpg" },
                { "Purple Heart Lane Warfare (Night)", @"https://qinaii.de/taurus/img/gsstatus/phlnight.jpg" },
                { "Purple Heart Lane Offensive (US)", @"https://qinaii.de/taurus/img/gsstatus/phlus.jpg" },
                { "Purple Heart Lane Offensive (GER)", @"https://qinaii.de/taurus/img/gsstatus/phlger.jpg" },

                { "Remagen Warfare", @"https://qinaii.de/taurus/img/gsstatus/remagen.jpg" },
                { "Remagen Warfare (Night)", @"https://qinaii.de/taurus/img/gsstatus/remagenight.jpg" },
                { "Remagen Offensive (US)", @"https://qinaii.de/taurus/img/gsstatus/remagenus.jpg" },
                { "Remagen Offensive (GER)", @"https://qinaii.de/taurus/img/gsstatus/remagenger.jpg" },

                { "Stalingrad Warfare", @"https://qinaii.de/taurus/img/gsstatus/stalingrad.jpg" },
                { "Stalingrad Warfare (Night)", @"https://qinaii.de/taurus/img/gsstatus/stalingradnight.jpg" },
                { "Stalingrad Offensive (RUS)", @"https://qinaii.de/taurus/img/gsstatus/stalingradrus.jpg" },
                { "Stalingrad Offensive (GER)", @"https://qinaii.de/taurus/img/gsstatus/stalingradger.jpg" },

                { "St. Marie Du Mont Warfare", @"https://qinaii.de/taurus/img/gsstatus/smdm.jpg" },
                { "St. Marie Du Mont Warfare (Night)", @"https://qinaii.de/taurus/img/gsstatus/smdmnight.jpg" },
                { "St. Marie Du Mont Offensive (US)", @"https://qinaii.de/taurus/img/gsstatus/smdmus.jpg" },
                { "St. Marie Du Mont Offensive (GER)", @"https://qinaii.de/taurus/img/gsstatus/smdmger.jpg" },
                { "St. Marie Du Mont Skirmish (Rain)", @"https://qinaii.de/taurus/img/gsstatus/smdmskirmishrain.jpg" },
                {
                    "St. Marie Du Mont Skirmish (Night)", @"https://qinaii.de/taurus/img/gsstatus/smdmskirmishnight.jpg"
                },
                { "St. Marie Du Mont Skirmish", @"https://qinaii.de/taurus/img/gsstatus/smdmskirmish.jpg" },

                { "St. Mere Eglise Warfare", @"https://qinaii.de/taurus/img/gsstatus/sme.jpg" },
                { "St. Mere Eglise Warfare (Night)", @"https://qinaii.de/taurus/img/gsstatus/smenight.jpg" },
                { "St. Mere Eglise OFfensive (US)", @"https://qinaii.de/taurus/img/gsstatus/smeus.jpg" },
                { "St. Mere Eglise Offensive (GER)", @"https://qinaii.de/taurus/img/gsstatus/smeger.jpg" },

                { "Utah Beach Warfare", @"https://qinaii.de/taurus/img/gsstatus/utahbeach.jpg" },
                { "Utah Beach Warfare (Night)", @"https://qinaii.de/taurus/img/gsstatus/utahbeachnight.jpg" },
                { "Utah Beach Offensive (US)", @"https://qinaii.de/taurus/img/gsstatus/utahbeachus.jpg" },
                { "Utah Beach Offensive (GER)", @"https://qinaii.de/taurus/img/gsstatus/utahbeachger.jpg" },

                { "Mortain Warfare", @"https://qinaii.de/taurus/img/gsstatus/mortain.jpg" },
                { "Mortain Off. US", @"https://qinaii.de/taurus/img/gsstatus/mortainUS.jpg" },
                { "Mortain Off. GER", @"https://qinaii.de/taurus/img/gsstatus/mortainGER.jpg" },
                { "Mortain Warfare (Overcast)", @"https://qinaii.de/taurus/img/gsstatus/mortainovercast.jpg" },
                { "Mortain Overcast Offensive (US)", @"https://qinaii.de/taurus/img/gsstatus/mortainovercastUS.jpg" },
                { "Mortain Overcast Offensive (GER)", @"https://qinaii.de/taurus/img/gsstatus/mortainovercastGER.jpg" },
                { "Mortain Skirmish", @"https://qinaii.de/taurus/img/gsstatus/mortainskirmishday.jpg" },
                { "Mortain Skirmish (Overcast)", @"https://qinaii.de/taurus/img/gsstatus/mortainskirmishovercast.jpg" },
            };

            SovietMaps = new List<string> { "Kharkov", "Kursk", "Stalingrad" };
            BritainMaps = new List<string> { "Driel", "El Alamein" };
        }
    }
}
