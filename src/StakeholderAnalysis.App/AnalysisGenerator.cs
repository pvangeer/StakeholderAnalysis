using System.Windows.Media;
using StakeholderAnalysis.Data;

namespace StakeholderAnalysis.App
{
    public static class AnalysisGenerator
    {
        
        public static Analysis GetAnalysis()
        {
            var analysis = new Analysis();
            // Ringen
            analysis.Onion.Rings.Add(new OnionRing("", 1.0) { BackgroundColor = Colors.LightBlue});
            analysis.Onion.Rings.Add(new OnionRing("", 0.7) { BackgroundColor = Colors.CornflowerBlue });
            analysis.Onion.Rings.Add(new OnionRing("", 0.3) { BackgroundColor = Colors.DarkSlateBlue });

            //Team
            var wvl = AddStakeholder(analysis, "WVL", 0.5, 0.8, 0.7,1.0, StakeholderType.Rijksoverheid);
            var dgwb = AddStakeholder(analysis, "DGWB", 0.45, 0.75,0.5,1.0, StakeholderType.Rijksoverheid);
            var deltares = AddStakeholder(analysis, "Deltares", 0.55, 0.75, 0.7, 0.8, StakeholderType.Rijksoverheid);
            var markt = AddStakeholder(analysis, "Markt (ontwikkelaars)", 0.5, 0.65, 0.75,0.7, StakeholderType.Rijksoverheid);

            // Groepen
            var kkp = AddStakeholder(analysis, "KKP", 0.5, 0.55, 1.0, 0.5, StakeholderType.Stakeholdergroep);
            var enw = AddStakeholder(analysis, "ENW", 0.7, 0.6, 0.4, 0.4, StakeholderType.Stakeholdergroep);
            var uvw = AddStakeholder(analysis, "UvW", 0.3, 0.7, 0.8, 0.6, StakeholderType.Stakeholdergroep);
            var wwk = AddStakeholder(analysis, "WWK", 0.2, 0.65, 0.6, 0.5, StakeholderType.Stakeholdergroep);
            var cwk = AddStakeholder(analysis, "CWK", 0.25, 0.6, 0.6, 0.5, StakeholderType.Stakeholdergroep);
            var aio = AddStakeholder(analysis, "AIO", 0.45, 0.4, 0.5, 0.7, StakeholderType.Stakeholdergroep);
            var dki = AddStakeholder(analysis, "DKI", 0.4, 0.48, 0.5, 0.7, StakeholderType.Stakeholdergroep);

            var ilt = AddStakeholder(analysis, "ILT", 0.4, 0.58, 0.5, 0.7, StakeholderType.Rijksoverheid);
            var wateropleidingen = AddStakeholder(analysis, "Wateropleidingen", 0.5, 0.3, 0.5, 0.7, StakeholderType.Overig);
            var kennisvoorkeringen = AddStakeholder(analysis, "Kennis voor keringen", 0.7, 0.7, 0.5, 0.7, StakeholderType.Kennisinstituut);

            var themagroepKust = AddStakeholder(analysis, "Themagroep Kust", 0.35, 0.4, 0.7, 0.3, StakeholderType.Stakeholdergroep);
            var stowa = AddStakeholder(analysis, "STOWA", 0.3, 0.5, 0.7, 0.3, StakeholderType.Stakeholdergroep);

            // Waterschappen
            var hhnk = AddStakeholder(analysis, "Hollands Noorderkwartier", 0.16, 0.8, 0.8, 0.4, StakeholderType.Waterschap);
            var scheldestromen = AddStakeholder(analysis, "Scheldestromen", 0.44, 0.08, 0.8, 0.4, StakeholderType.Waterschap);
            var wetterskip = AddStakeholder(analysis, "Wetterskip", 0.14, 0.61, 0.8, 0.4, StakeholderType.Waterschap);
            var rijnland = AddStakeholder(analysis, "Rijnland", 0.26, 0.26, 0.8, 0.4, StakeholderType.Waterschap);
            var delfland = AddStakeholder(analysis, "Delfland", 0.43, 0.2, 0.8, 0.4, StakeholderType.Waterschap);
            var hollandseDelta = AddStakeholder(analysis, "Hollandse Delta", 0.36, 0.1, 0.8, 0.4, StakeholderType.Waterschap);
            var rws = AddStakeholder(analysis, "RWS", 0.09, 0.47, 0.8, 0.4, StakeholderType.Waterschap);

            var nzv = AddStakeholder(analysis, "Noordezijlvest", 0.1, 0.72, 0.8, 0.4, StakeholderType.Waterschap);
            var hena = AddStakeholder(analysis, "Hunze en Aa's", 0.21, 0.16, 0.8, 0.4, StakeholderType.Waterschap);
            var dod = AddStakeholder(analysis, "Drents Overijsselse Delta", 0.07, 0.60, 0.8, 0.4, StakeholderType.Waterschap);
            var zzl = AddStakeholder(analysis, "Zuiderzeeland", 0.14, 0.50, 0.8, 0.4, StakeholderType.Waterschap);
            var agenv = AddStakeholder(analysis, "Amstel Gooi en Vecht", 0.11, 0.36, 0.8, 0.4, StakeholderType.Waterschap);
            var vv = AddStakeholder(analysis, "Vallei en Veluwen", 0.18, 0.39, 0.8, 0.4, StakeholderType.Waterschap);
            var renij = AddStakeholder(analysis, "Rijn en IJssel", 0.21, 0.29, 0.8, 0.4, StakeholderType.Waterschap);
            var aaenm = AddStakeholder(analysis, "Aa en Maas", 0.31, 0.23, 0.8, 0.4, StakeholderType.Waterschap);
            var limburg = AddStakeholder(analysis, "Limburg", 0.37, 0.22, 0.8, 0.4, StakeholderType.Waterschap);
            var bdelta = AddStakeholder(analysis, "Brabantse Delta", 0.29, 0.11, 0.8, 0.4, StakeholderType.Waterschap);
            var rivierenland = AddStakeholder(analysis, "Rivierenland", 0.48, 0.16, 0.8, 0.4, StakeholderType.Waterschap);
            var srij = AddStakeholder(analysis, "Stichtse Rijnlanden", 0.5, 0.08, 0.8, 0.4, StakeholderType.Waterschap);

            // Connectiongroepen
            var coastGroup = new ConnectionGroup("Themagroep kust",Colors.DarkRed);
            var uvWgroup = new ConnectionGroup("Unie van Waterschappen", Colors.Blue);
            var aiodekiGroup = new ConnectionGroup("DKI/AIO", Colors.BlanchedAlmond);

            // Connections
            analysis.Connections.Add(new StakeholderConnection(coastGroup, hhnk, themagroepKust));
            analysis.Connections.Add(new StakeholderConnection(coastGroup, scheldestromen, themagroepKust));
            analysis.Connections.Add(new StakeholderConnection(coastGroup, wetterskip, themagroepKust));
            analysis.Connections.Add(new StakeholderConnection(coastGroup, rijnland, themagroepKust));
            analysis.Connections.Add(new StakeholderConnection(coastGroup, delfland, themagroepKust));
            analysis.Connections.Add(new StakeholderConnection(coastGroup, hollandseDelta, themagroepKust));
            analysis.Connections.Add(new StakeholderConnection(coastGroup, rws, themagroepKust));

            analysis.Connections.Add(new StakeholderConnection(uvWgroup, uvw, wwk));
            analysis.Connections.Add(new StakeholderConnection(uvWgroup, cwk, wwk));
            analysis.Connections.Add(new StakeholderConnection(uvWgroup, uvw, hhnk));
            analysis.Connections.Add(new StakeholderConnection(uvWgroup, uvw, scheldestromen));
            analysis.Connections.Add(new StakeholderConnection(uvWgroup, uvw, wetterskip));
            analysis.Connections.Add(new StakeholderConnection(uvWgroup, uvw, rijnland));
            analysis.Connections.Add(new StakeholderConnection(uvWgroup, uvw, delfland));
            analysis.Connections.Add(new StakeholderConnection(uvWgroup, uvw, hollandseDelta));
            analysis.Connections.Add(new StakeholderConnection(uvWgroup, uvw, nzv));
            analysis.Connections.Add(new StakeholderConnection(uvWgroup, uvw, hena));
            analysis.Connections.Add(new StakeholderConnection(uvWgroup, uvw, dod));
            analysis.Connections.Add(new StakeholderConnection(uvWgroup, uvw, zzl));
            analysis.Connections.Add(new StakeholderConnection(uvWgroup, uvw, agenv));
            analysis.Connections.Add(new StakeholderConnection(uvWgroup, uvw, vv));
            analysis.Connections.Add(new StakeholderConnection(uvWgroup, uvw, renij));
            analysis.Connections.Add(new StakeholderConnection(uvWgroup, uvw, aaenm));
            analysis.Connections.Add(new StakeholderConnection(uvWgroup, uvw, limburg));
            analysis.Connections.Add(new StakeholderConnection(uvWgroup, uvw, bdelta));
            analysis.Connections.Add(new StakeholderConnection(uvWgroup, uvw, rivierenland));
            analysis.Connections.Add(new StakeholderConnection(uvWgroup, uvw, srij));

            analysis.Connections.Add(new StakeholderConnection(aiodekiGroup, aio, dki));
            analysis.Connections.Add(new StakeholderConnection(aiodekiGroup, aio, dgwb));
            analysis.Connections.Add(new StakeholderConnection(aiodekiGroup, aio, wvl));
            analysis.Connections.Add(new StakeholderConnection(aiodekiGroup, aio, kennisvoorkeringen));
            analysis.Connections.Add(new StakeholderConnection(aiodekiGroup, aio, uvw));
            analysis.Connections.Add(new StakeholderConnection(aiodekiGroup, aio, stowa));
            analysis.Connections.Add(new StakeholderConnection(aiodekiGroup, aio, ilt));

            return analysis;
        }

        private static Stakeholder AddStakeholder(Analysis analysis, string name, double leftPercentage, double rightPercentage, double interest, double influence, StakeholderType type)
        {
            var stakeholder = new Stakeholder(name, leftPercentage, rightPercentage, interest, influence, type);
            analysis.Stakeholders.Add(stakeholder);
            return stakeholder;
        }
    }
}