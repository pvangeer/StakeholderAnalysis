using System;
using System.Collections.Generic;
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
            var wvl = AddStakeholder(analysis, "WVL", 0.5, 0.8, 0.9,1.0, 0.95, 0.9, StakeholderType.Rijksoverheid);
            var dgwb = AddStakeholder(analysis, "DGWB", 0.45, 0.75,0.94,1.0, 0.9, 0.95, StakeholderType.Rijksoverheid);
            var deltares = AddStakeholder(analysis, "Deltares", 0.55, 0.75, 0.9, 0.9, 0.9, 0.8, StakeholderType.Kennisinstituut);
            var markt = AddStakeholder(analysis, "Ontwikkelaars (overig)", 0.5, 0.65, 0.8,0.8, 0.85, 0.6, StakeholderType.Kennisinstituut);

            // Groepen
            var kkp = AddStakeholder(analysis, "KKP", 0.5, 0.55, 1.0, 0.5,0.9, 0.9, StakeholderType.Stakeholdergroep);
            var enw = AddStakeholder(analysis, "ENW", 0.62, 0.6, 0.6, 0.4, 0.7, 0.7, StakeholderType.Stakeholdergroep);
            var enwkust = AddStakeholder(analysis, "ENW-Kust", 0.63, 0.49, 1.0, 0.5,0.5, 0.4, StakeholderType.Stakeholdergroep);
            var enwrivieren = AddStakeholder(analysis, "ENW-Rivieren", 0.70, 0.5, 1.0, 0.5, 0.6, 0.4, StakeholderType.Stakeholdergroep);
            var enwtechniek = AddStakeholder(analysis, "ENW-Techniek", 0.75, 0.59, 1.0, 0.5, 0.55, 0.4, StakeholderType.Stakeholdergroep);
            var enwveiligheid = AddStakeholder(analysis, "ENW-Veiligheid", 0.72, 0.69, 1.0, 0.5, 0.5, 0.43,StakeholderType.Stakeholdergroep);

            var uvw = AddStakeholder(analysis, "UvW", 0.32, 0.67, 0.9, 0.6, 0.4, 0.8, StakeholderType.Stakeholdergroep);
            var wwk = AddStakeholder(analysis, "WWK", 0.29, 0.75, 0.85, 0.55, 0.4, 0.6, StakeholderType.Stakeholdergroep);
            var cwk = AddStakeholder(analysis, "CWK", 0.29, 0.84, 0.85, 0.5, 0.4, 0.5, StakeholderType.Stakeholdergroep);
            var aio = AddStakeholder(analysis, "AIO", 0.4, 0.56, 0.7, 0.7, 0.7, 0.7, StakeholderType.Stakeholdergroep);
            var dki = AddStakeholder(analysis, "DKI", 0.46, 0.42, 0.6, 0.7, 0.6, 0.8, StakeholderType.Stakeholdergroep);
            var nlingenieurs = AddStakeholder(analysis, "NL-ingenieurs", 0.67, 0.4, 0.1, 0.2,0.8, 0.1, StakeholderType.Stakeholdergroep);
            var hwbp = AddStakeholder(analysis, "HWBP", 0.5, 0.45, 0.5, 0.55, 0.3, 0.98, StakeholderType.Overig);
            // kpr, POVs

            var ilt = AddStakeholder(analysis, "ILT", 0.4, 0.45, 0.8, 0.5, 0.7, 0.55, StakeholderType.Rijksoverheid);
            var wateropleidingen = AddStakeholder(analysis, "Water- opleidingen", 0.5, 0.3, 0.6, 0.2, 0.9, 0.3, StakeholderType.Overig);
            var kennisvoorkeringen = AddStakeholder(analysis, "Kennis voor keringen", 0.5, 0.9, 0.4, 0.6, 0.75, 0.4, StakeholderType.Kennisinstituut);

            var themagroepKust = AddStakeholder(analysis, "Themagroep Kust", 0.35, 0.4, 0.9, 0.3, 0.7, 0.4, StakeholderType.Stakeholdergroep);
            var stowa = AddStakeholder(analysis, "STOWA", 0.3, 0.5, 0.7, 0.3, 0.6, 0.5, StakeholderType.Stakeholdergroep);

            // Waterschappen
            var hhnk = AddStakeholder(analysis, "Hollands Noorderkwartier", 0.16, 0.8, 0.8, 0.55, 0.8, 0.8, StakeholderType.Waterkeringbeheerder);
            var scheldestromen = AddStakeholder(analysis, "Scheldestromen", 0.44, 0.08, 0.8, 0.5, 0.5, 0.65, StakeholderType.Waterkeringbeheerder);
            var wetterskip = AddStakeholder(analysis, "Wetterskip", 0.14, 0.61, 0.8, 0.5, 0.55, 0.68, StakeholderType.Waterkeringbeheerder);
            var rijnland = AddStakeholder(analysis, "Rijnland", 0.26, 0.26, 0.8, 0.5, 0.8, 0.45, StakeholderType.Waterkeringbeheerder);
            var delfland = AddStakeholder(analysis, "Delfland", 0.43, 0.2, 0.8, 0.5, 0.7, 0.4, StakeholderType.Waterkeringbeheerder);
            var hollandseDelta = AddStakeholder(analysis, "Hollandse Delta", 0.36, 0.1, 0.8, 0.5, 0.7, 0.4, StakeholderType.Waterkeringbeheerder);
            var rwsZenD = AddStakeholder(analysis, "RWS - Z&D", 0.04, 0.40, 0.8, 0.4, 0.6, 0.2, StakeholderType.Waterkeringbeheerder);
            var rwsNN = AddStakeholder(analysis, "RWS - NN", 0.03, 0.49, 0.8, 0.4, 0.6, 0.2, StakeholderType.Waterkeringbeheerder);
            var nzv = AddStakeholder(analysis, "Noordezijlvest", 0.1, 0.72, 0.8, 0.4, 0.6, 0.67, StakeholderType.Waterkeringbeheerder);
            var hena = AddStakeholder(analysis, "Hunze en Aa's", 0.21, 0.16, 0.8, 0.4, 0.7, 0.4, StakeholderType.Waterkeringbeheerder);
            var dod = AddStakeholder(analysis, "Drents Overijsselse Delta", 0.07, 0.60, 0.8, 0.4, 0.7, 0.4, StakeholderType.Waterkeringbeheerder);
            var zzl = AddStakeholder(analysis, "Zuiderzeeland", 0.14, 0.50, 0.8, 0.5, 0.78, 0.6, StakeholderType.Waterkeringbeheerder);
            var agenv = AddStakeholder(analysis, "Amstel Gooi en Vecht", 0.11, 0.36, 0.8, 0.4, 0.6, 0.2, StakeholderType.Waterkeringbeheerder);
            var vv = AddStakeholder(analysis, "Vallei en Veluwen", 0.18, 0.39, 0.8, 0.5, 0.6, 0.43, StakeholderType.Waterkeringbeheerder);
            var renij = AddStakeholder(analysis, "Rijn en IJssel", 0.21, 0.29, 0.8, 0.5, 0.6, 0.49, StakeholderType.Waterkeringbeheerder);
            var aaenm = AddStakeholder(analysis, "Aa en Maas", 0.31, 0.23, 0.8, 0.5, 0.61, 0.43, StakeholderType.Waterkeringbeheerder);
            var limburg = AddStakeholder(analysis, "Limburg", 0.37, 0.22, 0.8, 0.55, 0.7, 0.8, StakeholderType.Waterkeringbeheerder);
            var bdelta = AddStakeholder(analysis, "Brabantse Delta", 0.29, 0.11, 0.8, 0.4, 0.65, 0.43, StakeholderType.Waterkeringbeheerder);
            var rivierenland = AddStakeholder(analysis, "Rivierenland", 0.48, 0.16, 0.8, 0.5, 0.82, 0.8, StakeholderType.Waterkeringbeheerder);
            var srij = AddStakeholder(analysis, "Stichtse Rijnlanden", 0.5, 0.08, 0.8, 0.4, 0.6, 0.41, StakeholderType.Waterkeringbeheerder);
            var hhsk = AddStakeholder(analysis, "Schieland en de krimpenerwaard", 0.13, 0.21, 0.8, 0.4, 0.6, 0.72, StakeholderType.Waterkeringbeheerder);

            var hkv = AddStakeholder(analysis, "HKV", 0.56, 0.25, 0.7, 0.65, 0.75, 0.4, StakeholderType.Ingenieursbureaus);
            var rhdhv = AddStakeholder(analysis, "RHDHV", 0.62, 0.25, 0.64, 0.4, 0.72, 0.3, StakeholderType.Ingenieursbureaus);
            var fugro = AddStakeholder(analysis, "Fugro", 0.59, 0.15, 0.5, 0.4, 0.6, 0.15, StakeholderType.Ingenieursbureaus);
            var wibo = AddStakeholder(analysis, "Witteveen en Bos", 0.67, 0.3, 0.6, 0.5, 0.6, 0.15, StakeholderType.Ingenieursbureaus);
            var arc = AddStakeholder(analysis, "Arcadis", 0.74, 0.36, 0.6, 0.6, 0.6, 0.15, StakeholderType.Ingenieursbureaus);
            var wnet = AddStakeholder(analysis, "Waternet", 0.72, 0.25, 0.5, 0.2, 0.6, 0.1, StakeholderType.Ingenieursbureaus);
            var ivInfa = AddStakeholder(analysis, "Iv - Infra", 0.79, 0.29, 0.5, 0.25, 0.6, 0.05, StakeholderType.Ingenieursbureaus);
            var antea = AddStakeholder(analysis, "Antea Group", 0.85, 0.38, 0.6, 0.2, 0.6, 0.01, StakeholderType.Ingenieursbureaus);
            var greenrivers = AddStakeholder(analysis, "Greenrivers", 0.78, 0.41, 0.7, 0.65, 0.6, 0.35, StakeholderType.Ingenieursbureaus);
            var bwz = AddStakeholder(analysis, "BWZ Ingenieurs", 0.92, 0.35, 0.5, 0.2, 0.6, 0.02, StakeholderType.Ingenieursbureaus);
            var infram = AddStakeholder(analysis, "Infram", 0.8, 0.5, 0.6, 0.5, 0.6, 0.15, StakeholderType.Ingenieursbureaus);
            var sweco = AddStakeholder(analysis, "Sweco", 0.95, 0.54, 0.55, 0.5, 0.6, 0.15, StakeholderType.Ingenieursbureaus);
            var tauw = AddStakeholder(analysis, "Tauw", 0.9, 0.53, 0.45, 0.35, 0.6, 0.15, StakeholderType.Ingenieursbureaus);
            var movares = AddStakeholder(analysis, "Movares", 0.86, 0.48, 0.6, 0.5, 0.6, 0.25, StakeholderType.Ingenieursbureaus);
            var cso = AddStakeholder(analysis, "CSO Lievense", 0.82, 0.60, 0.58, 0.55, 0.6, 0.3, StakeholderType.Ingenieursbureaus);
            var hydrologic = AddStakeholder(analysis, "HydroLogic", 0.92, 0.62, 0.5, 0.22, 0.6, 0.02, StakeholderType.Ingenieursbureaus);
            var aveco = AddStakeholder(analysis, "Aveco de Bondt", 0.94, 0.43, 0.5, 0.2, 0.6, 0.01, StakeholderType.Ingenieursbureaus);
            var rps = AddStakeholder(analysis, "RPS", 0.53, 0.18, 0.56, 0.4, 0.6, 0.15, StakeholderType.Ingenieursbureaus);
            var crux = AddStakeholder(analysis, "CRUX", 0.56, 0.1, 0.6, 0.15, 0.6, 0.01, StakeholderType.Ingenieursbureaus);
            var nenS = AddStakeholder(analysis, "Nelen & Schuurmans", 0.65, 0.12, 0.45, 0.65, 0.6, 0.25, StakeholderType.Ingenieursbureaus);
            var geobest = AddStakeholder(analysis, "Geobest", 0.71, 0.14, 0.57, 0.26, 0.6, 0.01, StakeholderType.Ingenieursbureaus);
            var bzim = AddStakeholder(analysis, "BZIM", 0.77, 0.16, 0.54, 0.18, 0.6, 0.03, StakeholderType.Ingenieursbureaus);
            var zzpers = AddStakeholder(analysis, "ZZPers", 0.83, 0.23, 0.6, 0.5, 0.6, 0.35, StakeholderType.Ingenieursbureaus);
            var boskalis = AddStakeholder(analysis, "Boskalis", 0.875, 0.27, 0.6, 0.5, 0.6, 0.15, StakeholderType.Ingenieursbureaus);

            var ihw = AddStakeholder(analysis, "IHW", 0.24, 0.59, 0.3, 0.5, 0.56, 0.4, StakeholderType.Overig);
            var waterschapshuis = AddStakeholder(analysis, "Waterschapshuis", 0.24, 0.5, 0.25, 0.4, 0.6, 0.15, StakeholderType.Overig);
            var technolution = AddStakeholder(analysis, "Technolution", 0.68, 0.94, 0.1, 0.3, 0.6, 0.03, StakeholderType.Kennisinstituut);
            var vortech = AddStakeholder(analysis, "Vortech", 0.61, 0.93, 0.2, 0.5, 0.7, 0.26, StakeholderType.Kennisinstituut);
            var alten = AddStakeholder(analysis, "Alten", 0.56, 0.89, 0.14, 0.7, 0.6, 0.15, StakeholderType.Kennisinstituut);
            //CIO
            // BIT
            // CIV

            var tud = AddStakeholder(analysis, "TU Delft", 0.73, 0.83, 0.25, 0.45, 0.75, 0.49, StakeholderType.Kennisinstituut);
            var tut = AddStakeholder(analysis, "TU Twente", 0.84, 0.78, 0.25, 0.45, 0.75, 0.43, StakeholderType.Kennisinstituut);
            var vu = AddStakeholder(analysis, "VU Amsterdam", 0.8, 0.87, 0.16, 0.15, 0.72, 0.40, StakeholderType.Kennisinstituut);
            var uu = AddStakeholder(analysis, "UU", 0.85, 0.68, 0.15, 0.17, 0.76, 0.41, StakeholderType.Kennisinstituut);
            var tno = AddStakeholder(analysis, "TNO", 0.67, 0.68, 0.23, 0.56, 0.8, 0.3, StakeholderType.Kennisinstituut);
            var knmi = AddStakeholder(analysis, "KNMI", 0.67, 0.8, 0.6, 0.5, 0.76, 0.36, StakeholderType.Kennisinstituut);
            var alterra = AddStakeholder(analysis, "Alterra", 0.88, 0.7, 0.6, 0.5, 0.6, 0.15, StakeholderType.Kennisinstituut);

            // Connectiongroepen
            var coastGroup = AddConnectionGroup(analysis, "Themagroep kust", Colors.DarkRed);
            var uvWgroup = AddConnectionGroup(analysis, "Unie van Waterschappen", Colors.Blue);
            var aiodekiGroup = AddConnectionGroup(analysis, "DKI/AIO", Colors.BlanchedAlmond);
            var kkpGroup = AddConnectionGroup(analysis, "KKP netwerk", Colors.DarkGreen);
            var marketDevelopersGroup = AddConnectionGroup(analysis, "Marktontwikkelaars", Colors.DeepPink);
            var enwGroup = AddConnectionGroup(analysis, "ENW", Colors.DarkCyan,true);
            var enwInternal = AddConnectionGroup(analysis, "ENW - intern", Colors.Black, true);
            var enwCoast = AddConnectionGroup(analysis, "ENW - Kust", Colors.Black);
            var enwTechniek = AddConnectionGroup(analysis, "ENW - Techniek", Colors.Black);
            var enwVeiligheid = AddConnectionGroup(analysis, "ENW - Veiligheid", Colors.Black);
            var enwRivers = AddConnectionGroup(analysis, "ENW - Rivieren", Colors.Black);
            var nlIngenieurs = AddConnectionGroup(analysis, "NL-ingenieurs", Colors.Purple);
            //Bestuurlijk platform waterveiligheid
            // Taskforce Delta Technologie

            // NL-ingenieurs
            AddMultipleConnections(analysis,nlIngenieurs, nlingenieurs,new[]
            {
                sweco, rhdhv, antea, aveco, fugro, wibo, tauw, arc, hydrologic, cso, ivInfa, movares
            });

            // Connections
            analysis.Connections.Add(new StakeholderConnection(enwInternal, enw, enwkust));
            analysis.Connections.Add(new StakeholderConnection(enwInternal, enw, enwrivieren));
            analysis.Connections.Add(new StakeholderConnection(enwInternal, enw, enwveiligheid));
            analysis.Connections.Add(new StakeholderConnection(enwInternal, enw, enwtechniek));

            analysis.Connections.Add(new StakeholderConnection(coastGroup, hhnk, themagroepKust));
            analysis.Connections.Add(new StakeholderConnection(coastGroup, scheldestromen, themagroepKust));
            analysis.Connections.Add(new StakeholderConnection(coastGroup, wetterskip, themagroepKust));
            analysis.Connections.Add(new StakeholderConnection(coastGroup, rijnland, themagroepKust));
            analysis.Connections.Add(new StakeholderConnection(coastGroup, delfland, themagroepKust));
            analysis.Connections.Add(new StakeholderConnection(coastGroup, hollandseDelta, themagroepKust));
            analysis.Connections.Add(new StakeholderConnection(coastGroup, rwsNN, themagroepKust));

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
            analysis.Connections.Add(new StakeholderConnection(uvWgroup, uvw, hhsk));

            // KKP
            AddMultipleConnections(analysis, kkpGroup, kkp, new[]
            {
                hhnk, scheldestromen, wetterskip, rijnland, delfland, hollandseDelta, nzv, hena, dod, zzl, agenv, vv,
                renij, aaenm, limburg, bdelta, rivierenland, hkv, rhdhv, fugro, wibo, arc, wnet, ivInfa, antea,
                greenrivers, bwz, infram, sweco, tauw, movares, cso, hydrologic, aveco, rps, crux, nenS, geobest, bzim,
                zzpers, hhsk
            });

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

            analysis.Connections.Add(new StakeholderConnection(enwCoast, enwkust, tud));
            analysis.Connections.Add(new StakeholderConnection(enwCoast, enwkust, delfland));
            analysis.Connections.Add(new StakeholderConnection(enwCoast, enwkust, hhnk));
            analysis.Connections.Add(new StakeholderConnection(enwCoast, enwkust, tut));
            analysis.Connections.Add(new StakeholderConnection(enwCoast, enwkust, wetterskip));
            analysis.Connections.Add(new StakeholderConnection(enwCoast, enwkust, deltares));
            analysis.Connections.Add(new StakeholderConnection(enwCoast, enwkust, rijnland));
            analysis.Connections.Add(new StakeholderConnection(enwCoast, enwkust, zzpers));
            analysis.Connections.Add(new StakeholderConnection(enwCoast, enwkust, arc));
            analysis.Connections.Add(new StakeholderConnection(enwCoast, enwkust, boskalis));
            analysis.Connections.Add(new StakeholderConnection(enwCoast, enwkust, rwsZenD));

            //ENW - Rivieren
            analysis.Connections.Add(new StakeholderConnection(enwRivers, enwrivieren, tut));
            analysis.Connections.Add(new StakeholderConnection(enwRivers, enwrivieren, zzpers));
            analysis.Connections.Add(new StakeholderConnection(enwRivers, enwrivieren, hkv));
            analysis.Connections.Add(new StakeholderConnection(enwRivers, enwrivieren, deltares));
            analysis.Connections.Add(new StakeholderConnection(enwRivers, enwrivieren, tud));
            analysis.Connections.Add(new StakeholderConnection(enwRivers, enwrivieren, alterra));
            analysis.Connections.Add(new StakeholderConnection(enwRivers, enwrivieren, limburg));
            analysis.Connections.Add(new StakeholderConnection(enwRivers, enwrivieren, uu));
            // provincie N-Brabant
            analysis.Connections.Add(new StakeholderConnection(enwRivers, enwrivieren, wvl));

            //ENW-Veiligheid
            analysis.Connections.Add(new StakeholderConnection(enwVeiligheid, enwveiligheid, hkv));
            //cpb
            analysis.Connections.Add(new StakeholderConnection(enwVeiligheid, enwveiligheid, tud));
            // Provincie Overijssel
            analysis.Connections.Add(new StakeholderConnection(enwVeiligheid, enwveiligheid, tno));
            analysis.Connections.Add(new StakeholderConnection(enwVeiligheid, enwveiligheid, fugro));
            analysis.Connections.Add(new StakeholderConnection(enwVeiligheid, enwveiligheid, deltares));
            // Planbureau voor de leefomgeving
            analysis.Connections.Add(new StakeholderConnection(enwVeiligheid, enwveiligheid, zzpers));
            analysis.Connections.Add(new StakeholderConnection(enwVeiligheid, enwveiligheid, scheldestromen));
            analysis.Connections.Add(new StakeholderConnection(enwVeiligheid, enwveiligheid, wvl));
            analysis.Connections.Add(new StakeholderConnection(enwVeiligheid, enwveiligheid, arc));

            //ENW-Techniek
            analysis.Connections.Add(new StakeholderConnection(enwTechniek, enwtechniek, deltares));
            analysis.Connections.Add(new StakeholderConnection(enwTechniek, enwtechniek, wibo));
            // Volker staal en funderingen
            analysis.Connections.Add(new StakeholderConnection(enwTechniek, enwtechniek, hhsk));
            // Hogeschool Rotterdam
            analysis.Connections.Add(new StakeholderConnection(enwTechniek, enwtechniek, wvl));
            analysis.Connections.Add(new StakeholderConnection(enwTechniek, enwtechniek, tud));
            // GPO
            analysis.Connections.Add(new StakeholderConnection(enwTechniek, enwtechniek, fugro));
            analysis.Connections.Add(new StakeholderConnection(enwTechniek, enwtechniek, zzpers));
            // WL Vlaanderen
            analysis.Connections.Add(new StakeholderConnection(enwTechniek, enwtechniek, rivierenland));

            return analysis;
        }

        private static void AddMultipleConnections(Analysis analysis, ConnectionGroup connectionGroup, Stakeholder baseStakeholder, IEnumerable<Stakeholder> stakeholders)
        {
            foreach (var stakeholder in stakeholders)
            {
                analysis.Connections.Add(new StakeholderConnection(connectionGroup,baseStakeholder,stakeholder));
            }
        }

        private static ConnectionGroup AddConnectionGroup(Analysis analysis, string groupName, Color groupColor, bool isVisible = true)
        {
            var coastGroup = new ConnectionGroup(groupName, groupColor, isVisible);
            analysis.ConnectionGroups.Add(coastGroup);
            return coastGroup;
        }

        private static Stakeholder AddStakeholder(Analysis analysis, string name, double leftPercentage, double rightPercentage, double interest, double influence, double attitude, double impact, StakeholderType type)
        {
            var stakeholder = new Stakeholder(name, leftPercentage, rightPercentage, interest, influence, attitude, impact, type);
            analysis.Stakeholders.Add(stakeholder);
            return stakeholder;
        }
    }
}