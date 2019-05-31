﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.AttitudeImpactDiagrams;
using StakeholderAnalysis.Data.ForceFieldDiagrams;
using StakeholderAnalysis.Data.OnionDiagrams;

namespace StakeholderAnalysis.App
{
    public static class AnalysisGenerator
    {
        public static Analysis GetAnalysis()
        {
            var analysis = new Analysis();

            var onionDiagram = new OnionDiagram("Beoordelen",new ObservableCollection<OnionRing>
            {
                new OnionRing(1.0) {BackgroundColor = Colors.LightBlue},
                new OnionRing(0.65) {BackgroundColor = Colors.CornflowerBlue},
                new OnionRing(0.3) {BackgroundColor = Colors.DarkSlateBlue}
            })
            {
                Asymmetry = 0.7
            };
            analysis.OnionDiagrams.Add(onionDiagram);

            var forceFieldDiagram = new ForceFieldDiagram("BOI-krachtenveld");
            analysis.ForceFieldDiagrams.Add(forceFieldDiagram);

            var attitudeImpactDiagram = new AttitudeImpactDiagram("BOI-houding/impact");
            analysis.AttitudeImpactDiagrams.Add(attitudeImpactDiagram);

            #region Add stakeholders
            //Team
            var wlvStakeholder = AddStakeholderToAnalysis(analysis, "WVL", StakeholderType.Rijksoverheid);
            var dgwbStakeholder = AddStakeholderToAnalysis(analysis, "DGWB", StakeholderType.Rijksoverheid);
            var deltaresStakeholder = AddStakeholderToAnalysis(analysis, "Deltares", StakeholderType.Kennisinstituut);
            var markedStakeholder = AddStakeholderToAnalysis(analysis, "Ontwikkelaars (overig)", StakeholderType.Kennisinstituut);
            
            // Groepen
            var kkpStakeholder = AddStakeholderToAnalysis(analysis, "KKP", StakeholderType.Stakeholdergroep);
            var enwStakeholder = AddStakeholderToAnalysis(analysis, "ENW", StakeholderType.Stakeholdergroep);
            var enwCoastStakeholder = AddStakeholderToAnalysis(analysis, "ENW-Kust", StakeholderType.Stakeholdergroep);
            var enwRiversStakeholder = AddStakeholderToAnalysis(analysis, "ENW-Rivieren", StakeholderType.Stakeholdergroep);
            var enwTechnicStakeholder = AddStakeholderToAnalysis(analysis, "ENW-Techniek", StakeholderType.Stakeholdergroep);
            var enwSafetyStakeholder = AddStakeholderToAnalysis(analysis, "ENW-Veiligheid", StakeholderType.Stakeholdergroep);

            var uvwStakeholder = AddStakeholderToAnalysis(analysis, "UvW", StakeholderType.Stakeholdergroep);
            var wwkStakeholder = AddStakeholderToAnalysis(analysis, "WWK", StakeholderType.Stakeholdergroep);
            var cwkStakeholder = AddStakeholderToAnalysis(analysis, "CWK", StakeholderType.Stakeholdergroep);
            var aioStakeholder = AddStakeholderToAnalysis(analysis, "AIO", StakeholderType.Stakeholdergroep);
            var dkiStakeholder = AddStakeholderToAnalysis(analysis, "DKI", StakeholderType.Stakeholdergroep);
            var nlIngenieursStakeholder = AddStakeholderToAnalysis(analysis, "NL-ingenieurs", StakeholderType.Stakeholdergroep);
            var hwbpStakeholder = AddStakeholderToAnalysis(analysis, "HWBP", StakeholderType.Overig);

            // kpr, POVs
            var iltStakeholder = AddStakeholderToAnalysis(analysis, "ILT", StakeholderType.Rijksoverheid);
            var wateropleidingenStakeholder = AddStakeholderToAnalysis(analysis, "Water- opleidingen", StakeholderType.Overig);
            var kvkStakeholder = AddStakeholderToAnalysis(analysis, "Kennis voor keringen", StakeholderType.Kennisinstituut);

            var themagroepKustStakeholder = AddStakeholderToAnalysis(analysis, "Themagroep Kust", StakeholderType.Stakeholdergroep);
            var stowaStakeholder = AddStakeholderToAnalysis(analysis, "STOWA", StakeholderType.Stakeholdergroep);

            // Waterschappen
            var waterAuthoritiesStakeholder = AddStakeholderToAnalysis(analysis, "Waterkeringbeheerders", StakeholderType.Waterkeringbeheerder);
            var hhnkStakeholder = AddStakeholderToAnalysis(analysis, "Hollands Noorderkwartier", StakeholderType.Waterkeringbeheerder);
            var scheldeStromenStakeholder = AddStakeholderToAnalysis(analysis, "Scheldestromen", StakeholderType.Waterkeringbeheerder);
            var wetterskipStakeholder = AddStakeholderToAnalysis(analysis, "Wetterskip", StakeholderType.Waterkeringbeheerder);
            var rijnlandStakeholder = AddStakeholderToAnalysis(analysis, "Rijnland", StakeholderType.Waterkeringbeheerder);
            var delflandStakeholder = AddStakeholderToAnalysis(analysis, "Delfland", StakeholderType.Waterkeringbeheerder);
            var hollandseDeltaStakeholder = AddStakeholderToAnalysis(analysis, "Hollandse Delta", StakeholderType.Waterkeringbeheerder);
            var rwsZenDStakeholder = AddStakeholderToAnalysis(analysis, "RWS - Z&D", StakeholderType.Waterkeringbeheerder);
            var rwsNNStakeholder = AddStakeholderToAnalysis(analysis, "RWS - NN", StakeholderType.Waterkeringbeheerder);
            var nzvStakeholder = AddStakeholderToAnalysis(analysis, "Noordezijlvest", StakeholderType.Waterkeringbeheerder);
            var henaStakeholder = AddStakeholderToAnalysis(analysis, "Hunze en Aa's", StakeholderType.Waterkeringbeheerder);
            var wdodStakeholder = AddStakeholderToAnalysis(analysis, "Drents Overijsselse Delta", StakeholderType.Waterkeringbeheerder);
            var zzlStakeholder = AddStakeholderToAnalysis(analysis, "Zuiderzeeland", StakeholderType.Waterkeringbeheerder);
            var agenvStakeholder = AddStakeholderToAnalysis(analysis, "Amstel Gooi en Vecht", StakeholderType.Waterkeringbeheerder);
            var wsvvStakeholder = AddStakeholderToAnalysis(analysis, "Vallei en Veluwen", StakeholderType.Waterkeringbeheerder);
            var wrijStakeholder = AddStakeholderToAnalysis(analysis, "Rijn en IJssel", StakeholderType.Waterkeringbeheerder);
            var aaenmStakeholder = AddStakeholderToAnalysis(analysis, "Aa en Maas", StakeholderType.Waterkeringbeheerder);
            var limburgStakeholder = AddStakeholderToAnalysis(analysis, "Limburg", StakeholderType.Waterkeringbeheerder);
            var bdeltaStakeholder = AddStakeholderToAnalysis(analysis, "Brabantse Delta", StakeholderType.Waterkeringbeheerder);
            var rivierenlandStakeholder = AddStakeholderToAnalysis(analysis, "Rivierenland", StakeholderType.Waterkeringbeheerder);
            var srijStakeholder = AddStakeholderToAnalysis(analysis, "Stichtse Rijnlanden", StakeholderType.Waterkeringbeheerder);
            var hhskStakeholder = AddStakeholderToAnalysis(analysis, "Schieland en de krimpenerwaard", StakeholderType.Waterkeringbeheerder);

            var allMarkedPartiesStakeholder = AddStakeholderToAnalysis(analysis, "Marktpartijen", StakeholderType.Ingenieursbureaus);
            var hkvStakeholder = AddStakeholderToAnalysis(analysis, "HKV", StakeholderType.Ingenieursbureaus);
            var rhdhvStakeholder = AddStakeholderToAnalysis(analysis, "RHDHV", StakeholderType.Ingenieursbureaus);
            var fugroStakeholder = AddStakeholderToAnalysis(analysis, "Fugro", StakeholderType.Ingenieursbureaus);
            var wiboStakeholder = AddStakeholderToAnalysis(analysis, "Witteveen en Bos", StakeholderType.Ingenieursbureaus);
            var arcStakeholder = AddStakeholderToAnalysis(analysis, "Arcadis", StakeholderType.Ingenieursbureaus);
            var wnetStakeholder = AddStakeholderToAnalysis(analysis, "Waternet", StakeholderType.Ingenieursbureaus);
            var ivInfraStakeholder = AddStakeholderToAnalysis(analysis, "Iv - Infra", StakeholderType.Ingenieursbureaus);
            var anteaStakeholder = AddStakeholderToAnalysis(analysis, "Antea Group", StakeholderType.Ingenieursbureaus);
            var greenRiversStakeholder = AddStakeholderToAnalysis(analysis, "Greenrivers", StakeholderType.Ingenieursbureaus);
            var bwzStakeholder = AddStakeholderToAnalysis(analysis, "BWZ Ingenieurs", StakeholderType.Ingenieursbureaus);
            var inframStakeholder = AddStakeholderToAnalysis(analysis, "Infram", StakeholderType.Ingenieursbureaus);
            var swecoStakeholder = AddStakeholderToAnalysis(analysis, "Sweco", StakeholderType.Ingenieursbureaus);
            var tauwStakeholder = AddStakeholderToAnalysis(analysis, "Tauw", StakeholderType.Ingenieursbureaus);
            var movaresStakeholder = AddStakeholderToAnalysis(analysis, "Movares", StakeholderType.Ingenieursbureaus);
            var csoStakeholder = AddStakeholderToAnalysis(analysis, "CSO Lievense", StakeholderType.Ingenieursbureaus);
            var hydrologicStakeholder = AddStakeholderToAnalysis(analysis, "HydroLogic", StakeholderType.Ingenieursbureaus);
            var avecoStakeholder = AddStakeholderToAnalysis(analysis, "Aveco de Bondt", StakeholderType.Ingenieursbureaus);
            var rpsStakeholder = AddStakeholderToAnalysis(analysis, "RPS", StakeholderType.Ingenieursbureaus);
            var cruxStakeholder = AddStakeholderToAnalysis(analysis, "CRUX", StakeholderType.Ingenieursbureaus);
            var nensStakeholder = AddStakeholderToAnalysis(analysis, "Nelen & Schuurmans", StakeholderType.Ingenieursbureaus);
            var geobestStakeholder = AddStakeholderToAnalysis(analysis, "Geobest", StakeholderType.Ingenieursbureaus);
            var bzimStakeholder = AddStakeholderToAnalysis(analysis, "BZIM", StakeholderType.Ingenieursbureaus);
            var zzpersStakeholder = AddStakeholderToAnalysis(analysis, "ZZPers", StakeholderType.Ingenieursbureaus);
            var boskalisStakeholder = AddStakeholderToAnalysis(analysis, "Boskalis", StakeholderType.Ingenieursbureaus);

            var ihwStakeholder = AddStakeholderToAnalysis(analysis, "IHW", StakeholderType.Overig);
            var waterschapshuisStakeholder = AddStakeholderToAnalysis(analysis, "Waterschapshuis", StakeholderType.Overig);
            var technolutionStakeholder = AddStakeholderToAnalysis(analysis, "Technolution", StakeholderType.Kennisinstituut);
            var vortechStakeholder = AddStakeholderToAnalysis(analysis, "Vortech", StakeholderType.Kennisinstituut);
            var altenStakeholder = AddStakeholderToAnalysis(analysis, "Alten", StakeholderType.Kennisinstituut);
            //CIO
            // BIT
            // CIV

            var tudStakeholder = AddStakeholderToAnalysis(analysis, "TU Delft", StakeholderType.Kennisinstituut);
            var tutStakeholder = AddStakeholderToAnalysis(analysis, "TU Twente", StakeholderType.Kennisinstituut);
            var vuStakeholder = AddStakeholderToAnalysis(analysis, "VU Amsterdam", StakeholderType.Kennisinstituut);
            var uuStakeholder = AddStakeholderToAnalysis(analysis, "UU", StakeholderType.Kennisinstituut);
            var tnoStakeholder = AddStakeholderToAnalysis(analysis, "TNO", StakeholderType.Kennisinstituut);
            var knmiStakeholder = AddStakeholderToAnalysis(analysis, "KNMI", StakeholderType.Kennisinstituut);
            var alterraStakeholder = AddStakeholderToAnalysis(analysis, "Alterra", StakeholderType.Kennisinstituut);
            #endregion

            #region Add Onion diagram
            var wvl = AddStakeholderToOnionDiagram(wlvStakeholder, onionDiagram, 0.5, 0.8);
            var dgwb = AddStakeholderToOnionDiagram(dgwbStakeholder, onionDiagram, 0.45, 0.75);
            var deltares = AddStakeholderToOnionDiagram(deltaresStakeholder, onionDiagram, 0.55, 0.75);
            var markt = AddStakeholderToOnionDiagram(markedStakeholder, onionDiagram, 0.5, 0.65);

            var kkp = AddStakeholderToOnionDiagram(kkpStakeholder, onionDiagram, 0.5, 0.55);
            var enw = AddStakeholderToOnionDiagram(enwStakeholder, onionDiagram, 0.62, 0.6);
            var enwkust = AddStakeholderToOnionDiagram(enwCoastStakeholder, onionDiagram, 0.63, 0.49);
            var enwrivieren = AddStakeholderToOnionDiagram(enwRiversStakeholder, onionDiagram, 0.70, 0.5);
            var enwtechniek = AddStakeholderToOnionDiagram(enwTechnicStakeholder, onionDiagram, 0.75, 0.59);
            var enwveiligheid = AddStakeholderToOnionDiagram(enwSafetyStakeholder, onionDiagram, 0.72, 0.69);

            var uvw = AddStakeholderToOnionDiagram(uvwStakeholder, onionDiagram, 0.32, 0.67);
            var wwk = AddStakeholderToOnionDiagram(wwkStakeholder, onionDiagram, 0.29, 0.75);
            var cwk = AddStakeholderToOnionDiagram(cwkStakeholder, onionDiagram, 0.29, 0.84);
            var aio = AddStakeholderToOnionDiagram(aioStakeholder, onionDiagram, 0.4, 0.56);
            var dki = AddStakeholderToOnionDiagram(dkiStakeholder, onionDiagram, 0.46, 0.42);
            var nlingenieurs = AddStakeholderToOnionDiagram(nlIngenieursStakeholder, onionDiagram, 0.67, 0.4);
            var hwbp = AddStakeholderToOnionDiagram(hwbpStakeholder, onionDiagram, 0.5, 0.45);

            var ilt = AddStakeholderToOnionDiagram(iltStakeholder, onionDiagram, 0.4, 0.45);
            var wateropleidingen = AddStakeholderToOnionDiagram(wateropleidingenStakeholder, onionDiagram, 0.5, 0.3);
            var kvk = AddStakeholderToOnionDiagram(kvkStakeholder, onionDiagram, 0.5, 0.9);

            var themagroepKust = AddStakeholderToOnionDiagram(themagroepKustStakeholder, onionDiagram, 0.35, 0.4);
            var stowa = AddStakeholderToOnionDiagram(stowaStakeholder, onionDiagram, 0.3, 0.5);

            var hhnk = AddStakeholderToOnionDiagram(hhnkStakeholder, onionDiagram, 0.16, 0.8);
            var scheldeStromen = AddStakeholderToOnionDiagram(scheldeStromenStakeholder, onionDiagram, 0.44, 0.08);
            var wetterskip = AddStakeholderToOnionDiagram(wetterskipStakeholder, onionDiagram, 0.14, 0.61);
            var rijnland = AddStakeholderToOnionDiagram(rijnlandStakeholder, onionDiagram, 0.26, 0.26);
            var delfland = AddStakeholderToOnionDiagram(delflandStakeholder, onionDiagram, 0.43, 0.2);
            var hollandseDelta = AddStakeholderToOnionDiagram(hollandseDeltaStakeholder, onionDiagram, 0.36, 0.1);
            var rwsZenD = AddStakeholderToOnionDiagram(rwsZenDStakeholder, onionDiagram, 0.04, 0.40);
            var rwsNN = AddStakeholderToOnionDiagram(rwsNNStakeholder, onionDiagram, 0.03, 0.49);
            var nzv = AddStakeholderToOnionDiagram(nzvStakeholder, onionDiagram, 0.1, 0.72);
            var hena = AddStakeholderToOnionDiagram(henaStakeholder, onionDiagram, 0.21, 0.16);
            var wdod = AddStakeholderToOnionDiagram(wdodStakeholder, onionDiagram, 0.07, 0.60);
            var zzl = AddStakeholderToOnionDiagram(zzlStakeholder, onionDiagram, 0.14, 0.50);
            var agenv = AddStakeholderToOnionDiagram(agenvStakeholder, onionDiagram, 0.11, 0.36);
            var wsvv = AddStakeholderToOnionDiagram(wsvvStakeholder, onionDiagram, 0.18, 0.39);
            var wrij = AddStakeholderToOnionDiagram(wrijStakeholder, onionDiagram, 0.21, 0.29);
            var aaenm = AddStakeholderToOnionDiagram(aaenmStakeholder, onionDiagram, 0.31, 0.23);
            var limburg = AddStakeholderToOnionDiagram(limburgStakeholder, onionDiagram, 0.37, 0.22);
            var bdelta = AddStakeholderToOnionDiagram(bdeltaStakeholder, onionDiagram, 0.29, 0.11);
            var rivierenland = AddStakeholderToOnionDiagram(rivierenlandStakeholder, onionDiagram, 0.48, 0.16);
            var srij = AddStakeholderToOnionDiagram(srijStakeholder, onionDiagram, 0.5, 0.08);
            var hhsk = AddStakeholderToOnionDiagram(hhskStakeholder, onionDiagram, 0.13, 0.21);

            var hkv = AddStakeholderToOnionDiagram(hkvStakeholder, onionDiagram, 0.56, 0.25);
            var rhdhv = AddStakeholderToOnionDiagram(rhdhvStakeholder, onionDiagram, 0.62, 0.25);
            var fugro = AddStakeholderToOnionDiagram(fugroStakeholder, onionDiagram, 0.59, 0.15);
            var wibo = AddStakeholderToOnionDiagram(wiboStakeholder, onionDiagram, 0.67, 0.3);
            var arc = AddStakeholderToOnionDiagram(arcStakeholder, onionDiagram, 0.74, 0.36);
            var wnet = AddStakeholderToOnionDiagram(wnetStakeholder, onionDiagram, 0.72, 0.25);
            var ivInfa = AddStakeholderToOnionDiagram(ivInfraStakeholder, onionDiagram, 0.79, 0.29);
            var antea = AddStakeholderToOnionDiagram(anteaStakeholder, onionDiagram, 0.85, 0.38);
            var greenrivers = AddStakeholderToOnionDiagram(greenRiversStakeholder, onionDiagram, 0.78, 0.41);
            var bwz = AddStakeholderToOnionDiagram(bwzStakeholder, onionDiagram, 0.92, 0.35);
            var infram = AddStakeholderToOnionDiagram(inframStakeholder, onionDiagram, 0.8, 0.5);
            var sweco = AddStakeholderToOnionDiagram(swecoStakeholder, onionDiagram, 0.95, 0.54);
            var tauw = AddStakeholderToOnionDiagram(tauwStakeholder, onionDiagram, 0.9, 0.53);
            var movares = AddStakeholderToOnionDiagram(movaresStakeholder, onionDiagram, 0.86, 0.48);
            var cso = AddStakeholderToOnionDiagram(csoStakeholder, onionDiagram, 0.82, 0.60);
            var hydrologic = AddStakeholderToOnionDiagram(hydrologicStakeholder, onionDiagram, 0.92, 0.62);
            var aveco = AddStakeholderToOnionDiagram(avecoStakeholder, onionDiagram, 0.94, 0.43);
            var rps = AddStakeholderToOnionDiagram(rpsStakeholder, onionDiagram, 0.53, 0.18);
            var crux = AddStakeholderToOnionDiagram(cruxStakeholder, onionDiagram, 0.56, 0.1);
            var nenS = AddStakeholderToOnionDiagram(nensStakeholder, onionDiagram, 0.65, 0.12);
            var geobest = AddStakeholderToOnionDiagram(geobestStakeholder, onionDiagram, 0.71, 0.14);
            var bzim = AddStakeholderToOnionDiagram(bzimStakeholder, onionDiagram, 0.77, 0.16);
            var zzpers = AddStakeholderToOnionDiagram(zzpersStakeholder, onionDiagram, 0.83, 0.23);
            var boskalis = AddStakeholderToOnionDiagram(boskalisStakeholder, onionDiagram, 0.875, 0.27);

            var ihw = AddStakeholderToOnionDiagram(ihwStakeholder, onionDiagram, 0.24, 0.59);
            var waterschapshuis = AddStakeholderToOnionDiagram(waterschapshuisStakeholder, onionDiagram, 0.24, 0.5);
            var technolution = AddStakeholderToOnionDiagram(technolutionStakeholder, onionDiagram, 0.68, 0.94);
            var vortech = AddStakeholderToOnionDiagram(vortechStakeholder, onionDiagram, 0.61, 0.93);
            var alten = AddStakeholderToOnionDiagram(altenStakeholder, onionDiagram, 0.56, 0.89);

            var tud = AddStakeholderToOnionDiagram(tudStakeholder, onionDiagram, 0.73, 0.83);
            var tut = AddStakeholderToOnionDiagram(tutStakeholder, onionDiagram, 0.84, 0.78);
            var vu = AddStakeholderToOnionDiagram(vuStakeholder, onionDiagram, 0.8, 0.87);
            var uu = AddStakeholderToOnionDiagram(uuStakeholder, onionDiagram, 0.85, 0.68);
            var tno = AddStakeholderToOnionDiagram(tnoStakeholder, onionDiagram, 0.67, 0.68);
            var knmi = AddStakeholderToOnionDiagram(knmiStakeholder, onionDiagram, 0.67, 0.8);
            var alterra = AddStakeholderToOnionDiagram(alterraStakeholder, onionDiagram, 0.88, 0.7);

            // Connectiongroepen
            var coastGroup = AddConnectionGroup(onionDiagram, "Themagroep kust", Colors.DarkRed);
            var uvWgroup = AddConnectionGroup(onionDiagram, "Unie van Waterschappen", Colors.Blue);
            var aiodekiGroup = AddConnectionGroup(onionDiagram, "DKI/AIO", Colors.BlanchedAlmond);
            var kkpGroup = AddConnectionGroup(onionDiagram, "KKP netwerk", Colors.DarkGreen);
            var marketDevelopersGroup = AddConnectionGroup(onionDiagram, "Marktontwikkelaars", Colors.DeepPink);
            var enwGroup = AddConnectionGroup(onionDiagram, "ENW", Colors.DarkCyan,true);
            var enwInternal = AddConnectionGroup(onionDiagram, "ENW - intern", Colors.Black, true);
            var enwCoast = AddConnectionGroup(onionDiagram, "ENW - Kust", Colors.Black);
            var enwTechniek = AddConnectionGroup(onionDiagram, "ENW - Techniek", Colors.Black);
            var enwVeiligheid = AddConnectionGroup(onionDiagram, "ENW - Veiligheid", Colors.Black);
            var enwRivers = AddConnectionGroup(onionDiagram, "ENW - Rivieren", Colors.Black);
            var nlIngenieurs = AddConnectionGroup(onionDiagram, "NL-ingenieurs", Colors.Purple);
            //Bestuurlijk platform waterveiligheid
            // Taskforce Delta Technologie

            // Connections
            // NL-ingenieurs
            AddMultipleConnections(onionDiagram,nlIngenieurs, nlingenieurs,new[]
            {
                sweco, rhdhv, antea, aveco, fugro, wibo, tauw, arc, hydrologic, cso, ivInfa, movares
            });

            // ENW intern
            AddMultipleConnections(onionDiagram, enwInternal, enw, new[] {enwkust, enwrivieren, enwveiligheid, enwtechniek});

            // Themagroep kust
            AddMultipleConnections(onionDiagram, coastGroup, themagroepKust,
                new[] {hhnk, scheldeStromen, wetterskip, rijnland, delfland, hollandseDelta, rwsNN});

            // Unie van Waterschappen
            AddMultipleConnections(onionDiagram, uvWgroup, uvw,
                new[]
                {
                    wwk, hhnk, scheldeStromen, wetterskip, rijnland, delfland, hollandseDelta, nzv, hena, wdod, zzl,
                    agenv, wsvv, wrij, aaenm, limburg, bdelta, rivierenland, srij, hhsk
                });
            onionDiagram.Connections.Add(new StakeholderConnection(uvWgroup, cwk, wwk));

            // KKP
            AddMultipleConnections(onionDiagram, kkpGroup, kkp, new[]
            {
                hhnk, scheldeStromen, wetterskip, rijnland, delfland, hollandseDelta, nzv, hena, wdod, zzl, agenv, wsvv,
                wrij, aaenm, limburg, bdelta, rivierenland, hkv, rhdhv, fugro, wibo, arc, wnet, ivInfa, antea,
                greenrivers, bwz, infram, sweco, tauw, movares, cso, hydrologic, aveco, rps, crux, nenS, geobest, bzim,
                zzpers, hhsk
            });

            // AIO en DKI
            AddMultipleConnections(onionDiagram, aiodekiGroup, aio, new[] {dki, dgwb, wvl, kvk, uvw, stowa, ilt});

            // Markt ontwikkelars
            AddMultipleConnections(onionDiagram, marketDevelopersGroup, markt, new []{hkv,arc,rhdhv,cso,alten,vortech,wibo,rps,infram,greenrivers,tno,knmi, zzpers});

            // ENW plenair
            AddMultipleConnections(onionDiagram, enwGroup, enw, new[] {dgwb, wdod, tud, deltares, tut, vu, hkv, hhnk, aaenm, wvl});

            // ENW - Kust
            AddMultipleConnections(onionDiagram, enwCoast, enwkust, new []{tud, delfland, hhnk, tut, wetterskip, deltares, rijnland, zzpers, arc, boskalis, rwsZenD});

            //ENW - Rivieren
            AddMultipleConnections(onionDiagram, enwRivers, enwrivieren, new []{tut,zzpers,hkv,deltares,tud,alterra,limburg,uu, wvl});
            // provincie N-Brabant

            //ENW-Veiligheid
            AddMultipleConnections(onionDiagram, enwVeiligheid, enwveiligheid, new[] {hkv, tud, tno, fugro, deltares, zzpers, scheldeStromen, wvl, arc});
            //cpb
            // Provincie Overijssel
            // Planbureau voor de leefomgeving

            //ENW-Techniek
            AddMultipleConnections(onionDiagram, enwTechniek, enwtechniek, new []{deltares, wibo, hhsk, wvl, tud, fugro, zzpers, rivierenland});
            // Volker staal en funderingen
            // Hogeschool Rotterdam
            // GPO
            // WL Vlaanderen
            #endregion

            #region Add Force field diagram
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(wlvStakeholder, 0.9, 1.0));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(dgwbStakeholder, 0.94, 1.0));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(deltaresStakeholder, 0.9, 0.9));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(markedStakeholder, 0.8, 0.8));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(kkpStakeholder, 1.0, 0.5));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(enwStakeholder, 0.6, 0.4));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(enwCoastStakeholder, 1.0, 0.5));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(enwRiversStakeholder, 1.0, 0.5));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(enwTechnicStakeholder, 1.0, 0.5));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(enwSafetyStakeholder, 1.0, 0.5));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(uvwStakeholder, 0.9, 0.6));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(wwkStakeholder, 0.85, 0.55));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(cwkStakeholder, 0.85, 0.5));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(aioStakeholder, 0.7, 0.7));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(dkiStakeholder, 0.6, 0.7));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(nlIngenieursStakeholder, 0.1, 0.2));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(hwbpStakeholder, 0.5, 0.55));

            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(iltStakeholder, 0.8, 0.5));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(wateropleidingenStakeholder, 0.6, 0.2));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(kvkStakeholder, 0.4, 0.6));

            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(themagroepKustStakeholder, 0.9, 0.3));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(stowaStakeholder, 0.7, 0.3));

            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(hhnkStakeholder, 0.8, 0.55));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(scheldeStromenStakeholder, 0.8, 0.5));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(wetterskipStakeholder, 0.8, 0.5));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(rijnlandStakeholder, 0.8, 0.5));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(delflandStakeholder, 0.8, 0.5));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(hollandseDeltaStakeholder, 0.8, 0.5));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(rwsZenDStakeholder, 0.8, 0.4));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(rwsNNStakeholder, 0.8, 0.4));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(nzvStakeholder, 0.8, 0.4));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(henaStakeholder, 0.8, 0.4));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(wdodStakeholder, 0.8, 0.4));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(zzlStakeholder, 0.8, 0.5));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(agenvStakeholder, 0.8, 0.4));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(wsvvStakeholder, 0.8, 0.5));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(wrijStakeholder, 0.8, 0.5));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(aaenmStakeholder, 0.8, 0.5));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(limburgStakeholder, 0.8, 0.55));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(bdeltaStakeholder, 0.8, 0.4));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(rivierenlandStakeholder, 0.8, 0.5));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(srijStakeholder, 0.8, 0.4));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(hhskStakeholder, 0.8, 0.4));

            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(hkvStakeholder, 0.7, 0.65));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(rhdhvStakeholder, 0.64, 0.4));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(fugroStakeholder, 0.5, 0.4));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(wiboStakeholder, 0.6, 0.5));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(arcStakeholder, 0.6, 0.6));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(wnetStakeholder, 0.5, 0.2));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(ivInfraStakeholder, 0.5, 0.25));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(anteaStakeholder, 0.6, 0.2));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(greenRiversStakeholder, 0.7, 0.65));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(bwzStakeholder, 0.5, 0.2));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(inframStakeholder, 0.6, 0.5));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(swecoStakeholder, 0.55, 0.5));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(tauwStakeholder, 0.45, 0.35));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(movaresStakeholder, 0.6, 0.5));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(csoStakeholder, 0.58, 0.55));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(hydrologicStakeholder, 0.5, 0.22));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(avecoStakeholder, 0.5, 0.2));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(rpsStakeholder, 0.56, 0.4));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(cruxStakeholder, 0.6, 0.15));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(nensStakeholder, 0.45, 0.65));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(geobestStakeholder, 0.57, 0.26));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(bzimStakeholder, 0.54, 0.18));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(zzpersStakeholder, 0.6, 0.5));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(boskalisStakeholder, 0.6, 0.5));

            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(ihwStakeholder, 0.3, 0.5));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(waterschapshuisStakeholder, 0.25, 0.4));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(technolutionStakeholder, 0.1, 0.3));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(vortechStakeholder, 0.2, 0.5));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(altenStakeholder, 0.14, 0.7));

            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(tudStakeholder, 0.25, 0.45));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(tutStakeholder, 0.25, 0.45));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(vuStakeholder, 0.16, 0.15));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(uuStakeholder, 0.15, 0.17));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(tnoStakeholder, 0.23, 0.56));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(knmiStakeholder, 0.6, 0.5));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(alterraStakeholder, 0.6, 0.5));
            #endregion

            #region Add Force field diagram 2

            forceFieldDiagram = new ForceFieldDiagram("BOI-krachtenveld (simpel)");
            analysis.ForceFieldDiagrams.Add(forceFieldDiagram);

            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(wlvStakeholder, 0.9, 1.0));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(dgwbStakeholder, 0.94, 1.0));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(deltaresStakeholder, 0.9, 0.9));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(markedStakeholder, 0.8, 0.8));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(kkpStakeholder, 1.0, 0.5));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(enwStakeholder, 0.6, 0.4));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(uvwStakeholder, 0.9, 0.6));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(wwkStakeholder, 0.85, 0.55));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(cwkStakeholder, 0.85, 0.5));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(aioStakeholder, 0.7, 0.7));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(dkiStakeholder, 0.6, 0.7));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(nlIngenieursStakeholder, 0.1, 0.2));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(hwbpStakeholder, 0.5, 0.55));

            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(iltStakeholder, 0.8, 0.5));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(wateropleidingenStakeholder, 0.6, 0.2));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(kvkStakeholder, 0.4, 0.6));

            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(themagroepKustStakeholder, 0.9, 0.3));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(stowaStakeholder, 0.7, 0.3));

            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(waterAuthoritiesStakeholder, 0.8, 0.55));

            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(allMarkedPartiesStakeholder, 0.7, 0.65));

            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(ihwStakeholder, 0.3, 0.5));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(waterschapshuisStakeholder, 0.25, 0.4));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(technolutionStakeholder, 0.1, 0.3));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(vortechStakeholder, 0.2, 0.5));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(altenStakeholder, 0.14, 0.7));

            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(tudStakeholder, 0.25, 0.45));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(tutStakeholder, 0.25, 0.45));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(vuStakeholder, 0.16, 0.15));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(uuStakeholder, 0.15, 0.17));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(tnoStakeholder, 0.23, 0.56));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(knmiStakeholder, 0.6, 0.5));
            forceFieldDiagram.Stakeholders.Add(new ForceFieldDiagramStakeholder(alterraStakeholder, 0.6, 0.5));
            #endregion

            #region Attitude impact diagram
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(wlvStakeholder, 0.95, 0.9));
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(dgwbStakeholder, 0.9, 0.95));
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(deltaresStakeholder, 0.9, 0.8));
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(markedStakeholder, 0.85, 0.6));

            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(kkpStakeholder, 0.9, 0.9));
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(enwStakeholder, 0.7, 0.7));
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(enwCoastStakeholder, 0.5, 0.4));
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(enwRiversStakeholder, 0.6, 0.4));
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(enwTechnicStakeholder, 0.55, 0.4));
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(enwSafetyStakeholder, 0.5, 0.43));

            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(uvwStakeholder, 0.4, 0.8));
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(wwkStakeholder, 0.4, 0.6));
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(cwkStakeholder, 0.4, 0.5));
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(aioStakeholder, 0.7, 0.7));
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(dkiStakeholder, 0.6, 0.8));
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(nlIngenieursStakeholder, 0.8, 0.1));
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(hwbpStakeholder, 0.3, 0.98));

            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(iltStakeholder, 0.7, 0.55));
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(wateropleidingenStakeholder, 0.9, 0.3));
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(kvkStakeholder, 0.75, 0.4));

            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(themagroepKustStakeholder, 0.7, 0.4));
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(stowaStakeholder, 0.6, 0.5));

            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(hhnkStakeholder, 0.8, 0.8));
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(scheldeStromenStakeholder, 0.5, 0.65));
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(wetterskipStakeholder, 0.55, 0.68));
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(rijnlandStakeholder, 0.8, 0.45));
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(delflandStakeholder, 0.7, 0.4));
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(hollandseDeltaStakeholder, 0.7, 0.4));
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(rwsZenDStakeholder, 0.6, 0.2));
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(rwsNNStakeholder, 0.6, 0.2));
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(nzvStakeholder, 0.6, 0.67));
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(henaStakeholder, 0.7, 0.4));
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(wdodStakeholder, 0.7, 0.4));
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(zzlStakeholder, 0.78, 0.6));
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(agenvStakeholder, 0.6, 0.2));
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(wsvvStakeholder, 0.6, 0.43));
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(wrijStakeholder, 0.6, 0.49));
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(aaenmStakeholder, 0.61, 0.43));
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(limburgStakeholder, 0.7, 0.8));
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(bdeltaStakeholder, 0.65, 0.43));
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(rivierenlandStakeholder, 0.82, 0.8));
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(srijStakeholder, 0.6, 0.41));
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(hhskStakeholder, 0.6, 0.72));

            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(hkvStakeholder, 0.75, 0.4));
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(rhdhvStakeholder, 0.72, 0.3));
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(fugroStakeholder, 0.6, 0.15));
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(wiboStakeholder, 0.6, 0.15));
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(arcStakeholder, 0.6, 0.15));
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(wnetStakeholder, 0.6, 0.1));
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(ivInfraStakeholder, 0.6, 0.05));
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(anteaStakeholder, 0.6, 0.01));
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(greenRiversStakeholder, 0.6, 0.35));
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(bwzStakeholder, 0.6, 0.02));
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(inframStakeholder, 0.6, 0.15));
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(swecoStakeholder, 0.6, 0.15));
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(tauwStakeholder, 0.6, 0.15));
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(movaresStakeholder, 0.6, 0.25));
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(csoStakeholder, 0.6, 0.3));
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(hydrologicStakeholder, 0.6, 0.02));
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(avecoStakeholder, 0.6, 0.01));
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(rpsStakeholder, 0.6, 0.15));
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(cruxStakeholder, 0.6, 0.01));
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(nensStakeholder, 0.6, 0.25));
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(geobestStakeholder, 0.6, 0.01));
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(bzimStakeholder, 0.6, 0.03));
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(zzpersStakeholder, 0.6, 0.35));
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(boskalisStakeholder, 0.6, 0.15));

            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(ihwStakeholder, 0.56, 0.4));
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(waterschapshuisStakeholder, 0.6, 0.15));
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(technolutionStakeholder, 0.6, 0.03));
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(vortechStakeholder, 0.7, 0.26));
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(altenStakeholder, 0.6, 0.15));

            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(tudStakeholder, 0.75, 0.49));
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(tutStakeholder, 0.75, 0.43));
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(vuStakeholder, 0.72, 0.40));
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(uuStakeholder, 0.76, 0.41));
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(tnoStakeholder, 0.8, 0.3));
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(knmiStakeholder, 0.76, 0.36));
            attitudeImpactDiagram.Stakeholders.Add(new AttitudeImpactDiagramStakeholder(alterraStakeholder, 0.6, 0.15));
            #endregion

            return analysis;
        }

        private static void AddMultipleConnections(OnionDiagram diagram, StakeholderConnectionGroup stakeholderConnectionGroup, OnionDiagramStakeholder baseStakeholder, IEnumerable<OnionDiagramStakeholder> stakeholders)
        {
            foreach (var stakeholder in stakeholders)
            {
                diagram.Connections.Add(new StakeholderConnection(stakeholderConnectionGroup,baseStakeholder,stakeholder));
            }
        }

        private static StakeholderConnectionGroup AddConnectionGroup(OnionDiagram diagram, string groupName, Color groupColor, bool isVisible = true)
        {
            var coastGroup = new StakeholderConnectionGroup(groupName, groupColor, 1.0, isVisible);
            diagram.ConnectionGroups.Add(coastGroup);
            return coastGroup;
        }

        private static OnionDiagramStakeholder AddStakeholderToOnionDiagram(Stakeholder stakeholder, OnionDiagram diagram,
            double leftPercentage, double rightPercentage)
        {
            var diagramStakeholder = new OnionDiagramStakeholder(stakeholder, leftPercentage, rightPercentage);
            diagram.Stakeholders.Add(diagramStakeholder);
            return diagramStakeholder;
        }

        private static Stakeholder AddStakeholderToAnalysis(Analysis analysis, string name, StakeholderType type)
        {
            var stakeholder = new Stakeholder(name, type);
            analysis.Stakeholders.Add(stakeholder);
            return stakeholder;
        }
    }
}