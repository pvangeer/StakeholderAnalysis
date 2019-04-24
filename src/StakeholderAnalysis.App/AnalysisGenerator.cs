using System;
using System.Collections.Generic;
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

            var diagram1 = new OnionDiagram("Beoordelen",new ObservableCollection<OnionRing>()
            {
                new OnionRing(1.0) {BackgroundColor = Colors.LightBlue},
                new OnionRing(0.65) {BackgroundColor = Colors.CornflowerBlue},
                new OnionRing(0.3) {BackgroundColor = Colors.DarkSlateBlue}
            })
            {
                Asymmetry = 0.7
            };
            analysis.OnionDiagrams.Add(diagram1);

            //Team
            var wlvStakeholder = AddStakeholderToAnalysis(analysis, "WVL", 0.9, 1.0, 0.95, 0.9, StakeholderType.Rijksoverheid);
            var dgwbStakeholder = AddStakeholderToAnalysis(analysis, "DGWB", 0.94, 1.0, 0.9, 0.95, StakeholderType.Rijksoverheid);
            var deltaresStakeholder = AddStakeholderToAnalysis(analysis, "Deltares", 0.9, 0.9, 0.9, 0.8, StakeholderType.Kennisinstituut);
            var markedStakeholder = AddStakeholderToAnalysis(analysis, "Ontwikkelaars (overig)", 0.8, 0.8, 0.85, 0.6, StakeholderType.Kennisinstituut);

            // Groepen
            var kkpStakeholder = AddStakeholderToAnalysis(analysis, "KKP", 1.0, 0.5, 0.9, 0.9, StakeholderType.Stakeholdergroep);
            var enwStakeholder = AddStakeholderToAnalysis(analysis, "ENW", 0.6, 0.4, 0.7, 0.7, StakeholderType.Stakeholdergroep);
            var enwCoastStakeholder = AddStakeholderToAnalysis(analysis, "ENW-Kust", 1.0, 0.5, 0.5, 0.4, StakeholderType.Stakeholdergroep);
            var enwRiversStakeholder = AddStakeholderToAnalysis(analysis, "ENW-Rivieren", 1.0, 0.5, 0.6, 0.4, StakeholderType.Stakeholdergroep);
            var enwTechnicStakeholder = AddStakeholderToAnalysis(analysis, "ENW-Techniek", 1.0, 0.5, 0.55, 0.4, StakeholderType.Stakeholdergroep);
            var enwSafetyStakeholder = AddStakeholderToAnalysis(analysis, "ENW-Veiligheid", 1.0, 0.5, 0.5, 0.43, StakeholderType.Stakeholdergroep);

            var uvwStakeholder = AddStakeholderToAnalysis(analysis, "UvW", 0.9, 0.6, 0.4, 0.8, StakeholderType.Stakeholdergroep);
            var wwkStakeholder = AddStakeholderToAnalysis(analysis, "WWK", 0.85, 0.55, 0.4, 0.6, StakeholderType.Stakeholdergroep);
            var cwkStakeholder = AddStakeholderToAnalysis(analysis, "CWK", 0.85, 0.5, 0.4, 0.5, StakeholderType.Stakeholdergroep);
            var aioStakeholder = AddStakeholderToAnalysis(analysis, "AIO", 0.7, 0.7, 0.7, 0.7, StakeholderType.Stakeholdergroep);
            var dkiStakeholder = AddStakeholderToAnalysis(analysis, "DKI", 0.6, 0.7, 0.6, 0.8, StakeholderType.Stakeholdergroep);
            var nlIngenieursStakeholder = AddStakeholderToAnalysis(analysis, "NL-ingenieurs", 0.1, 0.2, 0.8, 0.1, StakeholderType.Stakeholdergroep);
            var hwbpStakeholder = AddStakeholderToAnalysis(analysis, "HWBP", 0.5, 0.55, 0.3, 0.98, StakeholderType.Overig);

            // kpr, POVs
            var iltStakeholder = AddStakeholderToAnalysis(analysis, "ILT", 0.8, 0.5, 0.7, 0.55, StakeholderType.Rijksoverheid);
            var wateropleidingenStakeholder = AddStakeholderToAnalysis(analysis, "Water- opleidingen", 0.6, 0.2, 0.9, 0.3, StakeholderType.Overig);
            var kvkStakeholder = AddStakeholderToAnalysis(analysis, "Kennis voor keringen", 0.4, 0.6, 0.75, 0.4, StakeholderType.Kennisinstituut);

            var themagroepKustStakeholder = AddStakeholderToAnalysis(analysis, "Themagroep Kust", 0.9, 0.3, 0.7, 0.4, StakeholderType.Stakeholdergroep);
            var stowaStakeholder = AddStakeholderToAnalysis(analysis, "STOWA", 0.7, 0.3, 0.6, 0.5, StakeholderType.Stakeholdergroep);

            // Waterschappen
            var hhnkStakeholder = AddStakeholderToAnalysis(analysis, "Hollands Noorderkwartier", 0.8, 0.55, 0.8, 0.8, StakeholderType.Waterkeringbeheerder);
            var scheldeStromenStakeholder = AddStakeholderToAnalysis(analysis, "Scheldestromen", 0.8, 0.5, 0.5, 0.65, StakeholderType.Waterkeringbeheerder);
            var wetterskipStakeholder = AddStakeholderToAnalysis(analysis, "Wetterskip", 0.8, 0.5, 0.55, 0.68, StakeholderType.Waterkeringbeheerder);
            var rijnlandStakeholder = AddStakeholderToAnalysis(analysis, "Rijnland", 0.8, 0.5, 0.8, 0.45, StakeholderType.Waterkeringbeheerder);
            var delflandStakeholder = AddStakeholderToAnalysis(analysis, "Delfland", 0.8, 0.5, 0.7, 0.4, StakeholderType.Waterkeringbeheerder);
            var hollandseDeltaStakeholder = AddStakeholderToAnalysis(analysis, "Hollandse Delta", 0.8, 0.5, 0.7, 0.4, StakeholderType.Waterkeringbeheerder);
            var rwsZenDStakeholder = AddStakeholderToAnalysis(analysis, "RWS - Z&D", 0.8, 0.4, 0.6, 0.2, StakeholderType.Waterkeringbeheerder);
            var rwsNNStakeholder = AddStakeholderToAnalysis(analysis, "RWS - NN", 0.8, 0.4, 0.6, 0.2, StakeholderType.Waterkeringbeheerder);
            var nzvStakeholder = AddStakeholderToAnalysis(analysis, "Noordezijlvest", 0.8, 0.4, 0.6, 0.67, StakeholderType.Waterkeringbeheerder);
            var henaStakeholder = AddStakeholderToAnalysis(analysis, "Hunze en Aa's", 0.8, 0.4, 0.7, 0.4, StakeholderType.Waterkeringbeheerder);
            var wdodStakeholder = AddStakeholderToAnalysis(analysis, "Drents Overijsselse Delta", 0.8, 0.4, 0.7, 0.4, StakeholderType.Waterkeringbeheerder);
            var zzlStakeholder = AddStakeholderToAnalysis(analysis, "Zuiderzeeland", 0.8, 0.5, 0.78, 0.6, StakeholderType.Waterkeringbeheerder);
            var agenvStakeholder = AddStakeholderToAnalysis(analysis, "Amstel Gooi en Vecht", 0.8, 0.4, 0.6, 0.2, StakeholderType.Waterkeringbeheerder);
            var wsvvStakeholder = AddStakeholderToAnalysis(analysis, "Vallei en Veluwen", 0.8, 0.5, 0.6, 0.43, StakeholderType.Waterkeringbeheerder);
            var wrijStakeholder = AddStakeholderToAnalysis(analysis, "Rijn en IJssel", 0.8, 0.5, 0.6, 0.49, StakeholderType.Waterkeringbeheerder);
            var aaenmStakeholder = AddStakeholderToAnalysis(analysis, "Aa en Maas", 0.8, 0.5, 0.61, 0.43, StakeholderType.Waterkeringbeheerder);
            var limburgStakeholder = AddStakeholderToAnalysis(analysis, "Limburg", 0.8, 0.55, 0.7, 0.8, StakeholderType.Waterkeringbeheerder);
            var bdeltaStakeholder = AddStakeholderToAnalysis(analysis, "Brabantse Delta", 0.8, 0.4, 0.65, 0.43, StakeholderType.Waterkeringbeheerder);
            var rivierenlandStakeholder = AddStakeholderToAnalysis(analysis, "Rivierenland", 0.8, 0.5, 0.82, 0.8, StakeholderType.Waterkeringbeheerder);
            var srijStakeholder = AddStakeholderToAnalysis(analysis, "Stichtse Rijnlanden", 0.8, 0.4, 0.6, 0.41, StakeholderType.Waterkeringbeheerder);
            var hhskStakeholder = AddStakeholderToAnalysis(analysis, "Schieland en de krimpenerwaard", 0.8, 0.4, 0.6, 0.72, StakeholderType.Waterkeringbeheerder);

            var hkvStakeholder = AddStakeholderToAnalysis(analysis, "HKV", 0.7, 0.65, 0.75, 0.4, StakeholderType.Ingenieursbureaus);
            var rhdhvStakeholder = AddStakeholderToAnalysis(analysis, "RHDHV", 0.64, 0.4, 0.72, 0.3, StakeholderType.Ingenieursbureaus);
            var fugroStakeholder = AddStakeholderToAnalysis(analysis, "Fugro", 0.5, 0.4, 0.6, 0.15, StakeholderType.Ingenieursbureaus);
            var wiboStakeholder = AddStakeholderToAnalysis(analysis, "Witteveen en Bos", 0.6, 0.5, 0.6, 0.15, StakeholderType.Ingenieursbureaus);
            var arcStakeholder = AddStakeholderToAnalysis(analysis, "Arcadis", 0.6, 0.6, 0.6, 0.15, StakeholderType.Ingenieursbureaus);
            var wnetStakeholder = AddStakeholderToAnalysis(analysis, "Waternet", 0.5, 0.2, 0.6, 0.1, StakeholderType.Ingenieursbureaus);
            var ivInfraStakeholder = AddStakeholderToAnalysis(analysis, "Iv - Infra", 0.5, 0.25, 0.6, 0.05, StakeholderType.Ingenieursbureaus);
            var anteaStakeholder = AddStakeholderToAnalysis(analysis, "Antea Group", 0.6, 0.2, 0.6, 0.01, StakeholderType.Ingenieursbureaus);
            var greenRiversStakeholder = AddStakeholderToAnalysis(analysis, "Greenrivers", 0.7, 0.65, 0.6, 0.35, StakeholderType.Ingenieursbureaus);
            var bwzStakeholder = AddStakeholderToAnalysis(analysis, "BWZ Ingenieurs", 0.5, 0.2, 0.6, 0.02, StakeholderType.Ingenieursbureaus);
            var inframStakeholder = AddStakeholderToAnalysis(analysis, "Infram", 0.6, 0.5, 0.6, 0.15, StakeholderType.Ingenieursbureaus);
            var swecoStakeholder = AddStakeholderToAnalysis(analysis, "Sweco", 0.55, 0.5, 0.6, 0.15, StakeholderType.Ingenieursbureaus);
            var tauwStakeholder = AddStakeholderToAnalysis(analysis, "Tauw", 0.45, 0.35, 0.6, 0.15, StakeholderType.Ingenieursbureaus);
            var movaresStakeholder = AddStakeholderToAnalysis(analysis, "Movares", 0.6, 0.5, 0.6, 0.25, StakeholderType.Ingenieursbureaus);
            var csoStakeholder = AddStakeholderToAnalysis(analysis, "CSO Lievense", 0.58, 0.55, 0.6, 0.3, StakeholderType.Ingenieursbureaus);
            var hydrologicStakeholder = AddStakeholderToAnalysis(analysis, "HydroLogic", 0.5, 0.22, 0.6, 0.02, StakeholderType.Ingenieursbureaus);
            var avecoStakeholder = AddStakeholderToAnalysis(analysis, "Aveco de Bondt", 0.5, 0.2, 0.6, 0.01, StakeholderType.Ingenieursbureaus);
            var rpsStakeholder = AddStakeholderToAnalysis(analysis, "RPS", 0.56, 0.4, 0.6, 0.15, StakeholderType.Ingenieursbureaus);
            var cruxStakeholder = AddStakeholderToAnalysis(analysis, "CRUX", 0.6, 0.15, 0.6, 0.01, StakeholderType.Ingenieursbureaus);
            var nensStakeholder = AddStakeholderToAnalysis(analysis, "Nelen & Schuurmans", 0.45, 0.65, 0.6, 0.25, StakeholderType.Ingenieursbureaus);
            var geobestStakeholder = AddStakeholderToAnalysis(analysis, "Geobest", 0.57, 0.26, 0.6, 0.01, StakeholderType.Ingenieursbureaus);
            var bzimStakeholder = AddStakeholderToAnalysis(analysis, "BZIM", 0.54, 0.18, 0.6, 0.03, StakeholderType.Ingenieursbureaus);
            var zzpersStakeholder = AddStakeholderToAnalysis(analysis, "ZZPers", 0.6, 0.5, 0.6, 0.35, StakeholderType.Ingenieursbureaus);
            var boskalisStakeholder = AddStakeholderToAnalysis(analysis, "Boskalis", 0.6, 0.5, 0.6, 0.15, StakeholderType.Ingenieursbureaus);

            var ihwStakeholder = AddStakeholderToAnalysis(analysis, "IHW", 0.3, 0.5, 0.56, 0.4, StakeholderType.Overig);
            var waterschapshuisStakeholder = AddStakeholderToAnalysis(analysis, "Waterschapshuis", 0.25, 0.4, 0.6, 0.15, StakeholderType.Overig);
            var technolutionStakeholder = AddStakeholderToAnalysis(analysis, "Technolution", 0.1, 0.3, 0.6, 0.03, StakeholderType.Kennisinstituut);
            var vortechStakeholder = AddStakeholderToAnalysis(analysis, "Vortech", 0.2, 0.5, 0.7, 0.26, StakeholderType.Kennisinstituut);
            var altenStakeholder = AddStakeholderToAnalysis(analysis, "Alten", 0.14, 0.7, 0.6, 0.15, StakeholderType.Kennisinstituut);
            //CIO
            // BIT
            // CIV

            var tudStakeholder = AddStakeholderToAnalysis(analysis, "TU Delft", 0.25, 0.45, 0.75, 0.49, StakeholderType.Kennisinstituut);
            var tutStakeholder = AddStakeholderToAnalysis(analysis, "TU Twente", 0.25, 0.45, 0.75, 0.43, StakeholderType.Kennisinstituut);
            var vuStakeholder = AddStakeholderToAnalysis(analysis, "VU Amsterdam", 0.16, 0.15, 0.72, 0.40, StakeholderType.Kennisinstituut);
            var uuStakeholder = AddStakeholderToAnalysis(analysis, "UU", 0.15, 0.17, 0.76, 0.41, StakeholderType.Kennisinstituut);
            var tnoStakeholder = AddStakeholderToAnalysis(analysis, "TNO", 0.23, 0.56, 0.8, 0.3, StakeholderType.Kennisinstituut);
            var knmiStakeholder = AddStakeholderToAnalysis(analysis, "KNMI", 0.6, 0.5, 0.76, 0.36, StakeholderType.Kennisinstituut);
            var alterraStakeholder = AddStakeholderToAnalysis(analysis, "Alterra", 0.6, 0.5, 0.6, 0.15, StakeholderType.Kennisinstituut);
            
            var wvl = AddStakeholderToOnionDiagram(wlvStakeholder, diagram1, 0.5, 0.8);
            var dgwb = AddStakeholderToOnionDiagram(dgwbStakeholder, diagram1, 0.45, 0.75);
            var deltares = AddStakeholderToOnionDiagram(deltaresStakeholder, diagram1, 0.55, 0.75);
            var markt = AddStakeholderToOnionDiagram(markedStakeholder, diagram1, 0.5, 0.65);

            var kkp = AddStakeholderToOnionDiagram(kkpStakeholder, diagram1, 0.5, 0.55);
            var enw = AddStakeholderToOnionDiagram(enwStakeholder, diagram1, 0.62, 0.6);
            var enwkust = AddStakeholderToOnionDiagram(enwCoastStakeholder, diagram1, 0.63, 0.49);
            var enwrivieren = AddStakeholderToOnionDiagram(enwRiversStakeholder, diagram1, 0.70, 0.5);
            var enwtechniek = AddStakeholderToOnionDiagram(enwTechnicStakeholder, diagram1, 0.75, 0.59);
            var enwveiligheid = AddStakeholderToOnionDiagram(enwSafetyStakeholder, diagram1, 0.72, 0.69);

            var uvw = AddStakeholderToOnionDiagram(uvwStakeholder, diagram1, 0.32, 0.67);
            var wwk = AddStakeholderToOnionDiagram(wwkStakeholder, diagram1, 0.29, 0.75);
            var cwk = AddStakeholderToOnionDiagram(cwkStakeholder, diagram1, 0.29, 0.84);
            var aio = AddStakeholderToOnionDiagram(aioStakeholder, diagram1, 0.4, 0.56);
            var dki = AddStakeholderToOnionDiagram(dkiStakeholder, diagram1, 0.46, 0.42);
            var nlingenieurs = AddStakeholderToOnionDiagram(nlIngenieursStakeholder, diagram1, 0.67, 0.4);
            var hwbp = AddStakeholderToOnionDiagram(hwbpStakeholder, diagram1, 0.5, 0.45);

            var ilt = AddStakeholderToOnionDiagram(iltStakeholder, diagram1, 0.4, 0.45);
            var wateropleidingen = AddStakeholderToOnionDiagram(wateropleidingenStakeholder, diagram1, 0.5, 0.3);
            var kvk = AddStakeholderToOnionDiagram(kvkStakeholder, diagram1, 0.5, 0.9);

            var themagroepKust = AddStakeholderToOnionDiagram(themagroepKustStakeholder, diagram1, 0.35, 0.4);
            var stowa = AddStakeholderToOnionDiagram(stowaStakeholder, diagram1, 0.3, 0.5);

            var hhnk = AddStakeholderToOnionDiagram(hhnkStakeholder, diagram1, 0.16, 0.8);
            var scheldeStromen = AddStakeholderToOnionDiagram(scheldeStromenStakeholder, diagram1, 0.44, 0.08);
            var wetterskip = AddStakeholderToOnionDiagram(wetterskipStakeholder, diagram1, 0.14, 0.61);
            var rijnland = AddStakeholderToOnionDiagram(rijnlandStakeholder, diagram1, 0.26, 0.26);
            var delfland = AddStakeholderToOnionDiagram(delflandStakeholder, diagram1, 0.43, 0.2);
            var hollandseDelta = AddStakeholderToOnionDiagram(hollandseDeltaStakeholder, diagram1, 0.36, 0.1);
            var rwsZenD = AddStakeholderToOnionDiagram(rwsZenDStakeholder, diagram1, 0.04, 0.40);
            var rwsNN = AddStakeholderToOnionDiagram(rwsNNStakeholder, diagram1, 0.03, 0.49);
            var nzv = AddStakeholderToOnionDiagram(nzvStakeholder, diagram1, 0.1, 0.72);
            var hena = AddStakeholderToOnionDiagram(henaStakeholder, diagram1, 0.21, 0.16);
            var wdod = AddStakeholderToOnionDiagram(wdodStakeholder, diagram1, 0.07, 0.60);
            var zzl = AddStakeholderToOnionDiagram(zzlStakeholder, diagram1, 0.14, 0.50);
            var agenv = AddStakeholderToOnionDiagram(agenvStakeholder, diagram1, 0.11, 0.36);
            var wsvv = AddStakeholderToOnionDiagram(wsvvStakeholder, diagram1, 0.18, 0.39);
            var wrij = AddStakeholderToOnionDiagram(wrijStakeholder, diagram1, 0.21, 0.29);
            var aaenm = AddStakeholderToOnionDiagram(aaenmStakeholder, diagram1, 0.31, 0.23);
            var limburg = AddStakeholderToOnionDiagram(limburgStakeholder, diagram1, 0.37, 0.22);
            var bdelta = AddStakeholderToOnionDiagram(bdeltaStakeholder, diagram1, 0.29, 0.11);
            var rivierenland = AddStakeholderToOnionDiagram(rivierenlandStakeholder, diagram1, 0.48, 0.16);
            var srij = AddStakeholderToOnionDiagram(srijStakeholder, diagram1, 0.5, 0.08);
            var hhsk = AddStakeholderToOnionDiagram(hhskStakeholder, diagram1, 0.13, 0.21);

            var hkv = AddStakeholderToOnionDiagram(hkvStakeholder, diagram1, 0.56, 0.25);
            var rhdhv = AddStakeholderToOnionDiagram(rhdhvStakeholder, diagram1, 0.62, 0.25);
            var fugro = AddStakeholderToOnionDiagram(fugroStakeholder, diagram1, 0.59, 0.15);
            var wibo = AddStakeholderToOnionDiagram(wiboStakeholder, diagram1, 0.67, 0.3);
            var arc = AddStakeholderToOnionDiagram(arcStakeholder, diagram1, 0.74, 0.36);
            var wnet = AddStakeholderToOnionDiagram(wnetStakeholder, diagram1, 0.72, 0.25);
            var ivInfa = AddStakeholderToOnionDiagram(ivInfraStakeholder, diagram1, 0.79, 0.29);
            var antea = AddStakeholderToOnionDiagram(anteaStakeholder, diagram1, 0.85, 0.38);
            var greenrivers = AddStakeholderToOnionDiagram(greenRiversStakeholder, diagram1, 0.78, 0.41);
            var bwz = AddStakeholderToOnionDiagram(bwzStakeholder, diagram1, 0.92, 0.35);
            var infram = AddStakeholderToOnionDiagram(inframStakeholder, diagram1, 0.8, 0.5);
            var sweco = AddStakeholderToOnionDiagram(swecoStakeholder, diagram1, 0.95, 0.54);
            var tauw = AddStakeholderToOnionDiagram(tauwStakeholder, diagram1, 0.9, 0.53);
            var movares = AddStakeholderToOnionDiagram(movaresStakeholder, diagram1, 0.86, 0.48);
            var cso = AddStakeholderToOnionDiagram(csoStakeholder, diagram1, 0.82, 0.60);
            var hydrologic = AddStakeholderToOnionDiagram(hydrologicStakeholder, diagram1, 0.92, 0.62);
            var aveco = AddStakeholderToOnionDiagram(avecoStakeholder, diagram1, 0.94, 0.43);
            var rps = AddStakeholderToOnionDiagram(rpsStakeholder, diagram1, 0.53, 0.18);
            var crux = AddStakeholderToOnionDiagram(cruxStakeholder, diagram1, 0.56, 0.1);
            var nenS = AddStakeholderToOnionDiagram(nensStakeholder, diagram1, 0.65, 0.12);
            var geobest = AddStakeholderToOnionDiagram(geobestStakeholder, diagram1, 0.71, 0.14);
            var bzim = AddStakeholderToOnionDiagram(bzimStakeholder, diagram1, 0.77, 0.16);
            var zzpers = AddStakeholderToOnionDiagram(zzpersStakeholder, diagram1, 0.83, 0.23);
            var boskalis = AddStakeholderToOnionDiagram(boskalisStakeholder, diagram1, 0.875, 0.27);

            var ihw = AddStakeholderToOnionDiagram(ihwStakeholder, diagram1, 0.24, 0.59);
            var waterschapshuis = AddStakeholderToOnionDiagram(waterschapshuisStakeholder, diagram1, 0.24, 0.5);
            var technolution = AddStakeholderToOnionDiagram(technolutionStakeholder, diagram1, 0.68, 0.94);
            var vortech = AddStakeholderToOnionDiagram(vortechStakeholder, diagram1, 0.61, 0.93);
            var alten = AddStakeholderToOnionDiagram(altenStakeholder, diagram1, 0.56, 0.89);

            var tud = AddStakeholderToOnionDiagram(tudStakeholder, diagram1, 0.73, 0.83);
            var tut = AddStakeholderToOnionDiagram(tutStakeholder, diagram1, 0.84, 0.78);
            var vu = AddStakeholderToOnionDiagram(vuStakeholder, diagram1, 0.8, 0.87);
            var uu = AddStakeholderToOnionDiagram(uuStakeholder, diagram1, 0.85, 0.68);
            var tno = AddStakeholderToOnionDiagram(tnoStakeholder, diagram1, 0.67, 0.68);
            var knmi = AddStakeholderToOnionDiagram(knmiStakeholder, diagram1, 0.67, 0.8);
            var alterra = AddStakeholderToOnionDiagram(alterraStakeholder, diagram1, 0.88, 0.7);

            // Connectiongroepen
            var coastGroup = AddConnectionGroup(diagram1, "Themagroep kust", Colors.DarkRed);
            var uvWgroup = AddConnectionGroup(diagram1, "Unie van Waterschappen", Colors.Blue);
            var aiodekiGroup = AddConnectionGroup(diagram1, "DKI/AIO", Colors.BlanchedAlmond);
            var kkpGroup = AddConnectionGroup(diagram1, "KKP netwerk", Colors.DarkGreen);
            var marketDevelopersGroup = AddConnectionGroup(diagram1, "Marktontwikkelaars", Colors.DeepPink);
            var enwGroup = AddConnectionGroup(diagram1, "ENW", Colors.DarkCyan,true);
            var enwInternal = AddConnectionGroup(diagram1, "ENW - intern", Colors.Black, true);
            var enwCoast = AddConnectionGroup(diagram1, "ENW - Kust", Colors.Black);
            var enwTechniek = AddConnectionGroup(diagram1, "ENW - Techniek", Colors.Black);
            var enwVeiligheid = AddConnectionGroup(diagram1, "ENW - Veiligheid", Colors.Black);
            var enwRivers = AddConnectionGroup(diagram1, "ENW - Rivieren", Colors.Black);
            var nlIngenieurs = AddConnectionGroup(diagram1, "NL-ingenieurs", Colors.Purple);
            //Bestuurlijk platform waterveiligheid
            // Taskforce Delta Technologie

            // Connections
            // NL-ingenieurs
            AddMultipleConnections(diagram1,nlIngenieurs, nlingenieurs,new[]
            {
                sweco, rhdhv, antea, aveco, fugro, wibo, tauw, arc, hydrologic, cso, ivInfa, movares
            });

            // ENW intern
            AddMultipleConnections(diagram1, enwInternal, enw, new[] {enwkust, enwrivieren, enwveiligheid, enwtechniek});

            // Themagroep kust
            AddMultipleConnections(diagram1, coastGroup, themagroepKust,
                new[] {hhnk, scheldeStromen, wetterskip, rijnland, delfland, hollandseDelta, rwsNN});

            // Unie van Waterschappen
            AddMultipleConnections(diagram1, uvWgroup, uvw,
                new[]
                {
                    wwk, hhnk, scheldeStromen, wetterskip, rijnland, delfland, hollandseDelta, nzv, hena, wdod, zzl,
                    agenv, wsvv, wrij, aaenm, limburg, bdelta, rivierenland, srij, hhsk
                });
            diagram1.Connections.Add(new StakeholderConnection(uvWgroup, cwk, wwk));

            // KKP
            AddMultipleConnections(diagram1, kkpGroup, kkp, new[]
            {
                hhnk, scheldeStromen, wetterskip, rijnland, delfland, hollandseDelta, nzv, hena, wdod, zzl, agenv, wsvv,
                wrij, aaenm, limburg, bdelta, rivierenland, hkv, rhdhv, fugro, wibo, arc, wnet, ivInfa, antea,
                greenrivers, bwz, infram, sweco, tauw, movares, cso, hydrologic, aveco, rps, crux, nenS, geobest, bzim,
                zzpers, hhsk
            });

            // AIO en DKI
            AddMultipleConnections(diagram1, aiodekiGroup, aio, new[] {dki, dgwb, wvl, kvk, uvw, stowa, ilt});

            // Markt ontwikkelars
            AddMultipleConnections(diagram1, marketDevelopersGroup, markt, new []{hkv,arc,rhdhv,cso,alten,vortech,wibo,rps,infram,greenrivers,tno,knmi, zzpers});

            // ENW plenair
            AddMultipleConnections(diagram1, enwGroup, enw, new[] {dgwb, wdod, tud, deltares, tut, vu, hkv, hhnk, aaenm, wvl});

            // ENW - Kust
            AddMultipleConnections(diagram1, enwCoast, enwkust, new []{tud, delfland, hhnk, tut, wetterskip, deltares, rijnland, zzpers, arc, boskalis, rwsZenD});

            //ENW - Rivieren
            AddMultipleConnections(diagram1, enwRivers, enwrivieren, new []{tut,zzpers,hkv,deltares,tud,alterra,limburg,uu, wvl});
            // provincie N-Brabant

            //ENW-Veiligheid
            AddMultipleConnections(diagram1, enwVeiligheid, enwveiligheid, new[] {hkv, tud, tno, fugro, deltares, zzpers, scheldeStromen, wvl, arc});
            //cpb
            // Provincie Overijssel
            // Planbureau voor de leefomgeving

            //ENW-Techniek
            AddMultipleConnections(diagram1, enwTechniek, enwtechniek, new []{deltares, wibo, hhsk, wvl, tud, fugro, zzpers, rivierenland});
            // Volker staal en funderingen
            // Hogeschool Rotterdam
            // GPO
            // WL Vlaanderen

            var forceFieldDiagram = new ForceFieldDiagram("BOI-krachtenveld");
            foreach (var stakeholder in analysis.Stakeholders)
            {
                forceFieldDiagram.Stakeholders.Add(stakeholder);
            }
            analysis.ForceFieldDiagrams.Add(forceFieldDiagram);

            var attitudeImpactDiagram = new AttitudeImpactDiagram("BOI-houding/impact");
            foreach (var stakeholder in analysis.Stakeholders)
            {
                attitudeImpactDiagram.Stakeholders.Add(stakeholder);
            }
            analysis.AttitudeImpactDiagrams.Add(attitudeImpactDiagram);

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
            var coastGroup = new StakeholderConnectionGroup(groupName, groupColor, isVisible);
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

        private static Stakeholder AddStakeholderToAnalysis(Analysis analysis, string name, double interest, double influence,
            double attitude, double impact, StakeholderType type)
        {
            var stakeholder = new Stakeholder(name, interest, influence, attitude, impact, type);
            analysis.Stakeholders.Add(stakeholder);
            return stakeholder;
        }
    }
}