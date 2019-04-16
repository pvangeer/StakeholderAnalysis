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
            analysis.Onion.Rings.Add(new OnionRing("", 0.65) { BackgroundColor = Colors.CornflowerBlue });
            analysis.Onion.Rings.Add(new OnionRing("", 0.3) { BackgroundColor = Colors.DarkSlateBlue });

            //Team
            var wvl = AddStakeholder(analysis, "WVL", 0.5, 0.8, 0.7,1.0, StakeholderType.Rijksoverheid);
            var dgwb = AddStakeholder(analysis, "DGWB", 0.45, 0.75,0.5,1.0, StakeholderType.Rijksoverheid);
            var deltares = AddStakeholder(analysis, "Deltares", 0.55, 0.75, 0.7, 0.8, StakeholderType.Kennisinstituut);
            var markt = AddStakeholder(analysis, "Ontwikkelaars (overig)", 0.5, 0.65, 0.75,0.7, StakeholderType.Kennisinstituut);

            // Groepen
            var kkp = AddStakeholder(analysis, "KKP", 0.5, 0.55, 1.0, 0.5, StakeholderType.Stakeholdergroep);
            var enw = AddStakeholder(analysis, "ENW", 0.62, 0.6, 0.4, 0.4, StakeholderType.Stakeholdergroep);
            var uvw = AddStakeholder(analysis, "UvW", 0.32, 0.67, 0.8, 0.6, StakeholderType.Stakeholdergroep);
            var wwk = AddStakeholder(analysis, "WWK", 0.29, 0.75, 0.6, 0.5, StakeholderType.Stakeholdergroep);
            var cwk = AddStakeholder(analysis, "CWK", 0.29, 0.84, 0.6, 0.5, StakeholderType.Stakeholdergroep);
            var aio = AddStakeholder(analysis, "AIO", 0.4, 0.56, 0.5, 0.7, StakeholderType.Stakeholdergroep);
            var dki = AddStakeholder(analysis, "DKI", 0.46, 0.42, 0.5, 0.7, StakeholderType.Stakeholdergroep);
            var hwbp = AddStakeholder(analysis, "HWBP", 0.5, 0.45, 0.5, 0.7, StakeholderType.Overig);

            var ilt = AddStakeholder(analysis, "ILT", 0.4, 0.45, 0.5, 0.7, StakeholderType.Rijksoverheid);
            var wateropleidingen = AddStakeholder(analysis, "Wateropleidingen", 0.5, 0.3, 0.5, 0.7, StakeholderType.Overig);
            var kennisvoorkeringen = AddStakeholder(analysis, "Kennis voor keringen", 0.5, 0.9, 0.5, 0.7, StakeholderType.Kennisinstituut);

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

            var hkv = AddStakeholder(analysis, "HKV", 0.56, 0.25, 0.8, 0.4, StakeholderType.Ingenieursbureaus);
            var rhdhv = AddStakeholder(analysis, "RHDHV", 0.62, 0.25, 0.8, 0.4, StakeholderType.Ingenieursbureaus);
            var fugro = AddStakeholder(analysis, "Fugro", 0.59, 0.15, 0.8, 0.4, StakeholderType.Ingenieursbureaus);
            var wibo = AddStakeholder(analysis, "Witteveen en Bos", 0.67, 0.3, 0.8, 0.4, StakeholderType.Ingenieursbureaus);
            var arc = AddStakeholder(analysis, "Arcadis", 0.74, 0.36, 0.8, 0.4, StakeholderType.Ingenieursbureaus);
            var wnet = AddStakeholder(analysis, "Waternet", 0.72, 0.25, 0.8, 0.4, StakeholderType.Ingenieursbureaus);
            var ivInfa = AddStakeholder(analysis, "Iv - Infra", 0.79, 0.29, 0.8, 0.4, StakeholderType.Ingenieursbureaus);
            var antea = AddStakeholder(analysis, "Antea Group", 0.85, 0.38, 0.8, 0.4, StakeholderType.Ingenieursbureaus);
            var greenrivers = AddStakeholder(analysis, "Greenrivers", 0.78, 0.41, 0.8, 0.4, StakeholderType.Ingenieursbureaus);
            var bwz = AddStakeholder(analysis, "BWZ Ingenieurs", 0.92, 0.35, 0.8, 0.4, StakeholderType.Ingenieursbureaus);
            var infram = AddStakeholder(analysis, "Infram", 0.8, 0.5, 0.8, 0.4, StakeholderType.Ingenieursbureaus);
            var sweco = AddStakeholder(analysis, "Sweco", 0.95, 0.54, 0.8, 0.4, StakeholderType.Ingenieursbureaus);
            var tauw = AddStakeholder(analysis, "Tauw", 0.9, 0.53, 0.8, 0.4, StakeholderType.Ingenieursbureaus);
            var movares = AddStakeholder(analysis, "Movares", 0.86, 0.48, 0.8, 0.4, StakeholderType.Ingenieursbureaus);
            var cso = AddStakeholder(analysis, "CSO Lievense", 0.82, 0.60, 0.8, 0.4, StakeholderType.Ingenieursbureaus);
            var hydrologic = AddStakeholder(analysis, "HydroLogic", 0.92, 0.62, 0.8, 0.4, StakeholderType.Ingenieursbureaus);
            var aveco = AddStakeholder(analysis, "Aveco de Bondt", 0.94, 0.43, 0.8, 0.4, StakeholderType.Ingenieursbureaus);
            var rps = AddStakeholder(analysis, "RPS", 0.53, 0.18, 0.8, 0.4, StakeholderType.Ingenieursbureaus);
            var crux = AddStakeholder(analysis, "CRUX", 0.56, 0.1, 0.8, 0.4, StakeholderType.Ingenieursbureaus);
            var nenS = AddStakeholder(analysis, "Nelen & Schuurmans", 0.65, 0.12, 0.8, 0.4, StakeholderType.Ingenieursbureaus);
            var geobest = AddStakeholder(analysis, "Geobest", 0.71, 0.14, 0.8, 0.4, StakeholderType.Ingenieursbureaus);
            var bzim = AddStakeholder(analysis, "BZIM", 0.77, 0.16, 0.8, 0.4, StakeholderType.Ingenieursbureaus);
            var zzpers = AddStakeholder(analysis, "ZZPers", 0.83, 0.23, 0.8, 0.4, StakeholderType.Ingenieursbureaus);

            var ihw = AddStakeholder(analysis, "IHW", 0.57, 0.48, 0.8, 0.4, StakeholderType.Overig);
            var waterschapshuis = AddStakeholder(analysis, "Waterschapshuis", 0.58, 0.4, 0.8, 0.4, StakeholderType.Overig);
            var technolution = AddStakeholder(analysis, "Technolution", 0.68, 0.94, 0.8, 0.4, StakeholderType.Kennisinstituut);
            var vortech = AddStakeholder(analysis, "Vortech", 0.61, 0.93, 0.8, 0.4, StakeholderType.Kennisinstituut);
            var alten = AddStakeholder(analysis, "Alten", 0.56, 0.89, 0.8, 0.4, StakeholderType.Kennisinstituut);

            var tud = AddStakeholder(analysis, "TU Delft", 0.73, 0.83, 0.8, 0.4, StakeholderType.Kennisinstituut);
            var tut = AddStakeholder(analysis, "TU Twente", 0.84, 0.78, 0.8, 0.4, StakeholderType.Kennisinstituut);
            var vu = AddStakeholder(analysis, "VU Amsterdam", 0.8, 0.87, 0.8, 0.4, StakeholderType.Kennisinstituut);
            var uu = AddStakeholder(analysis, "UU", 0.85, 0.68, 0.8, 0.4, StakeholderType.Kennisinstituut);
            var tno = AddStakeholder(analysis, "TNO", 0.67, 0.68, 0.8, 0.4, StakeholderType.Kennisinstituut);
            var knmi = AddStakeholder(analysis, "KNMI", 0.67, 0.8, 0.8, 0.4, StakeholderType.Kennisinstituut);

            // Connectiongroepen
            var coastGroup = new ConnectionGroup("Themagroep kust",Colors.DarkRed);
            var uvWgroup = new ConnectionGroup("Unie van Waterschappen", Colors.Blue);
            var aiodekiGroup = new ConnectionGroup("DKI/AIO", Colors.BlanchedAlmond);
            var kkpGroup = new ConnectionGroup("KKP netwerk",Colors.DarkGreen);
            var marketDevelopersGroup = new ConnectionGroup("Marktontwikkelaars", Colors.DeepPink);
            var enwGroup = new ConnectionGroup("ENW", Colors.DarkCyan);

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

            analysis.Connections.Add(new StakeholderConnection(kkpGroup, kkp, hhnk));
            analysis.Connections.Add(new StakeholderConnection(kkpGroup, kkp, scheldestromen));
            analysis.Connections.Add(new StakeholderConnection(kkpGroup, kkp, wetterskip));
            analysis.Connections.Add(new StakeholderConnection(kkpGroup, kkp, rijnland));
            analysis.Connections.Add(new StakeholderConnection(kkpGroup, kkp, delfland));
            analysis.Connections.Add(new StakeholderConnection(kkpGroup, kkp, hollandseDelta));
            analysis.Connections.Add(new StakeholderConnection(kkpGroup, kkp, nzv));
            analysis.Connections.Add(new StakeholderConnection(kkpGroup, kkp, hena));
            analysis.Connections.Add(new StakeholderConnection(kkpGroup, kkp, dod));
            analysis.Connections.Add(new StakeholderConnection(kkpGroup, kkp, zzl));
            analysis.Connections.Add(new StakeholderConnection(kkpGroup, kkp, agenv));
            analysis.Connections.Add(new StakeholderConnection(kkpGroup, kkp, vv));
            analysis.Connections.Add(new StakeholderConnection(kkpGroup, kkp, renij));
            analysis.Connections.Add(new StakeholderConnection(kkpGroup, kkp, aaenm));
            analysis.Connections.Add(new StakeholderConnection(kkpGroup, kkp, limburg));
            analysis.Connections.Add(new StakeholderConnection(kkpGroup, kkp, bdelta));
            analysis.Connections.Add(new StakeholderConnection(kkpGroup, kkp, rivierenland));
            analysis.Connections.Add(new StakeholderConnection(kkpGroup, kkp, hkv));
            analysis.Connections.Add(new StakeholderConnection(kkpGroup, kkp, rhdhv));
            analysis.Connections.Add(new StakeholderConnection(kkpGroup, kkp, fugro));
            analysis.Connections.Add(new StakeholderConnection(kkpGroup, kkp, wibo));
            analysis.Connections.Add(new StakeholderConnection(kkpGroup, kkp, arc));
            analysis.Connections.Add(new StakeholderConnection(kkpGroup, kkp, wnet));
            analysis.Connections.Add(new StakeholderConnection(kkpGroup, kkp, ivInfa));
            analysis.Connections.Add(new StakeholderConnection(kkpGroup, kkp, antea));
            analysis.Connections.Add(new StakeholderConnection(kkpGroup, kkp, greenrivers));
            analysis.Connections.Add(new StakeholderConnection(kkpGroup, kkp, bwz));
            analysis.Connections.Add(new StakeholderConnection(kkpGroup, kkp, infram));
            analysis.Connections.Add(new StakeholderConnection(kkpGroup, kkp, sweco));
            analysis.Connections.Add(new StakeholderConnection(kkpGroup, kkp, tauw));
            analysis.Connections.Add(new StakeholderConnection(kkpGroup, kkp, movares));
            analysis.Connections.Add(new StakeholderConnection(kkpGroup, kkp, cso));
            analysis.Connections.Add(new StakeholderConnection(kkpGroup, kkp, hydrologic));
            analysis.Connections.Add(new StakeholderConnection(kkpGroup, kkp, aveco));
            analysis.Connections.Add(new StakeholderConnection(kkpGroup, kkp, rps));
            analysis.Connections.Add(new StakeholderConnection(kkpGroup, kkp, crux));
            analysis.Connections.Add(new StakeholderConnection(kkpGroup, kkp, nenS));
            analysis.Connections.Add(new StakeholderConnection(kkpGroup, kkp, geobest));
            analysis.Connections.Add(new StakeholderConnection(kkpGroup, kkp, bzim));
            analysis.Connections.Add(new StakeholderConnection(kkpGroup, kkp, zzpers));

            analysis.Connections.Add(new StakeholderConnection(aiodekiGroup, aio, dki));
            analysis.Connections.Add(new StakeholderConnection(aiodekiGroup, aio, dgwb));
            analysis.Connections.Add(new StakeholderConnection(aiodekiGroup, aio, wvl));
            analysis.Connections.Add(new StakeholderConnection(aiodekiGroup, aio, kennisvoorkeringen));
            analysis.Connections.Add(new StakeholderConnection(aiodekiGroup, aio, uvw));
            analysis.Connections.Add(new StakeholderConnection(aiodekiGroup, aio, stowa));
            analysis.Connections.Add(new StakeholderConnection(aiodekiGroup, aio, ilt));

            analysis.Connections.Add(new StakeholderConnection(marketDevelopersGroup, markt, hkv));
            analysis.Connections.Add(new StakeholderConnection(marketDevelopersGroup, markt, arc));
            analysis.Connections.Add(new StakeholderConnection(marketDevelopersGroup, markt, rhdhv));
            analysis.Connections.Add(new StakeholderConnection(marketDevelopersGroup, markt, cso));
            analysis.Connections.Add(new StakeholderConnection(marketDevelopersGroup, markt, alten));
            analysis.Connections.Add(new StakeholderConnection(marketDevelopersGroup, markt, vortech));
            analysis.Connections.Add(new StakeholderConnection(marketDevelopersGroup, markt, wibo));
            analysis.Connections.Add(new StakeholderConnection(marketDevelopersGroup, markt, rps));
            analysis.Connections.Add(new StakeholderConnection(marketDevelopersGroup, markt, infram));
            analysis.Connections.Add(new StakeholderConnection(marketDevelopersGroup, markt, greenrivers));
            analysis.Connections.Add(new StakeholderConnection(marketDevelopersGroup, markt, tno));
            analysis.Connections.Add(new StakeholderConnection(marketDevelopersGroup, markt, knmi));

            analysis.Connections.Add(new StakeholderConnection(enwGroup, enw, dgwb));
            analysis.Connections.Add(new StakeholderConnection(enwGroup, enw, dod));
            analysis.Connections.Add(new StakeholderConnection(enwGroup, enw, tud));
            analysis.Connections.Add(new StakeholderConnection(enwGroup, enw, deltares));
            analysis.Connections.Add(new StakeholderConnection(enwGroup, enw, tut));
            analysis.Connections.Add(new StakeholderConnection(enwGroup, enw, vu));
            analysis.Connections.Add(new StakeholderConnection(enwGroup, enw, hkv));
            analysis.Connections.Add(new StakeholderConnection(enwGroup, enw, hhnk));
            analysis.Connections.Add(new StakeholderConnection(enwGroup, enw, aaenm));
            analysis.Connections.Add(new StakeholderConnection(enwGroup, enw, wvl));

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