using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;
using StakeholderAnalysis.Data;
using StakeholderAnalysis.Data.Diagrams;
using StakeholderAnalysis.Data.Diagrams.ForceFieldDiagrams;
using StakeholderAnalysis.Data.Diagrams.OnionDiagrams;

namespace StakeholderAnalysis.Storage.Test
{
    public static class AnalysisGenerator
    {
        public static Analysis GetAnalysis()
        {
            var analysis = new Analysis();

            var onionDiagram = new OnionDiagram("Beoordelen", new ObservableCollection<OnionRing>
            {
                new OnionRing { BackgroundColor = Colors.LightBlue },
                new OnionRing(0.65) { BackgroundColor = Colors.CornflowerBlue },
                new OnionRing(0.3) { BackgroundColor = Colors.DarkSlateBlue }
            })
            {
                Asymmetry = 0.7
            };
            analysis.OnionDiagrams.Add(onionDiagram);

            var forceFieldDiagram = new ForceFieldDiagram("BOI-krachtenveld")
            {
                BrushEndColor = Colors.Coral,
                BrushStartColor = Colors.DarkRed,
                BackgroundTextLeftBottom = "1",
                BackgroundTextLeftTop = "2",
                BackgroundTextRightBottom = "3",
                BackgroundTextRightTop = "4",
                BackgroundFontFamily = new FontFamily("Arial"),
                BackgroundFontColor = Colors.Bisque,
                BackgroundFontBold = false,
                BackgroundFontItalic = false,
                BackgroundFontSize = 124,
                XAxisMaxLabel = "5",
                XAxisMinLabel = "6",
                YAxisMaxLabel = "7",
                YAxisMinLabel = "8",
                AxisFontFamily = new FontFamily("Times New Roman"),
                AxisFontColor = Colors.CornflowerBlue,
                AxisFontBold = true,
                AxisFontItalic = true,
                AxisFontSize = 12
            };
            analysis.ForceFieldDiagrams.Add(forceFieldDiagram);

            var attitudeImpactDiagram = new TwoAxisDiagram("BOI-houding/impact")
            {
                BrushEndColor = Colors.Coral,
                BrushStartColor = Colors.DarkRed,
                BackgroundTextLeftBottom = "1",
                BackgroundTextLeftTop = "2",
                BackgroundTextRightBottom = "3",
                BackgroundTextRightTop = "4",
                BackgroundFontFamily = new FontFamily("Arial"),
                BackgroundFontColor = Colors.Bisque,
                BackgroundFontBold = false,
                BackgroundFontItalic = false,
                BackgroundFontSize = 124,
                XAxisMaxLabel = "5",
                XAxisMinLabel = "6",
                YAxisMaxLabel = "7",
                YAxisMinLabel = "8",
                AxisFontFamily = new FontFamily("Times New Roman"),
                AxisFontColor = Colors.CornflowerBlue,
                AxisFontBold = true,
                AxisFontItalic = true,
                AxisFontSize = 12
            };
            analysis.AttitudeImpactDiagrams.Add(attitudeImpactDiagram);

            #region Add stakeholders

            //Team
            var rijksoverheidStakeholderType =
                AddStakeholderType(analysis, "Rijksoverheid", StakeholderIconType.Suit, Colors.MistyRose);
            var knowledgeInstituteStakeholderType = AddStakeholderType(analysis, "Kennisinstituut",
                StakeholderIconType.Knowledge, Colors.DarkSeaGreen);
            var stakeholderGroupStakeholderType = AddStakeholderType(analysis, "Stakeholdergroep",
                StakeholderIconType.GroupTable, Colors.DarkGray);
            var miscStakeholderType =
                AddStakeholderType(analysis, "Other", StakeholderIconType.Other, Colors.Chocolate);
            var waterDefenceMaintainerStakeholderType = AddStakeholderType(analysis, "Waterkeringbeheerder",
                StakeholderIconType.Group5, Colors.AliceBlue);
            var marketStakeholderType = AddStakeholderType(analysis, "Ingenieursbureau", StakeholderIconType.Dollar,
                Colors.CadetBlue);

            var wlvStakeholder = AddStakeholderToAnalysis(analysis, "WVL", rijksoverheidStakeholderType);
            var dgwbStakeholder = AddStakeholderToAnalysis(analysis, "DGWB", rijksoverheidStakeholderType);
            var deltaresStakeholder = AddStakeholderToAnalysis(analysis, "Deltares", knowledgeInstituteStakeholderType);
            var markedStakeholder =
                AddStakeholderToAnalysis(analysis, "Ontwikkelaars (overig)", knowledgeInstituteStakeholderType);

            // Groepen
            var kkpStakeholder = AddStakeholderToAnalysis(analysis, "KKP", stakeholderGroupStakeholderType);
            var enwStakeholder = AddStakeholderToAnalysis(analysis, "ENW", stakeholderGroupStakeholderType);
            var enwCoastStakeholder = AddStakeholderToAnalysis(analysis, "ENW-Kust", stakeholderGroupStakeholderType);
            var enwRiversStakeholder =
                AddStakeholderToAnalysis(analysis, "ENW-Rivieren", stakeholderGroupStakeholderType);
            var enwTechnicStakeholder =
                AddStakeholderToAnalysis(analysis, "ENW-Techniek", stakeholderGroupStakeholderType);
            var enwSafetyStakeholder =
                AddStakeholderToAnalysis(analysis, "ENW-Veiligheid", stakeholderGroupStakeholderType);

            var uvwStakeholder = AddStakeholderToAnalysis(analysis, "UvW", stakeholderGroupStakeholderType);
            var wwkStakeholder = AddStakeholderToAnalysis(analysis, "WWK", stakeholderGroupStakeholderType);
            var cwkStakeholder = AddStakeholderToAnalysis(analysis, "CWK", stakeholderGroupStakeholderType);
            var aioStakeholder = AddStakeholderToAnalysis(analysis, "AIO", stakeholderGroupStakeholderType);
            var dkiStakeholder = AddStakeholderToAnalysis(analysis, "DKI", stakeholderGroupStakeholderType);
            var nlIngenieursStakeholder =
                AddStakeholderToAnalysis(analysis, "NL-ingenieurs", stakeholderGroupStakeholderType);
            var hwbpStakeholder = AddStakeholderToAnalysis(analysis, "HWBP", miscStakeholderType);

            // kpr, POVs
            var iltStakeholder = AddStakeholderToAnalysis(analysis, "ILT", rijksoverheidStakeholderType);
            var wateropleidingenStakeholder =
                AddStakeholderToAnalysis(analysis, "Water- opleidingen", miscStakeholderType);
            var kvkStakeholder =
                AddStakeholderToAnalysis(analysis, "Kennis voor keringen", knowledgeInstituteStakeholderType);

            var themagroepKustStakeholder =
                AddStakeholderToAnalysis(analysis, "Themagroep Kust", stakeholderGroupStakeholderType);
            var stowaStakeholder = AddStakeholderToAnalysis(analysis, "STOWA", stakeholderGroupStakeholderType);

            // Waterschappen
            var waterAuthoritiesStakeholder = AddStakeholderToAnalysis(analysis, "Waterkeringbeheerders",
                waterDefenceMaintainerStakeholderType);
            var hhnkStakeholder = AddStakeholderToAnalysis(analysis, "Hollands Noorderkwartier",
                waterDefenceMaintainerStakeholderType);
            var scheldeStromenStakeholder =
                AddStakeholderToAnalysis(analysis, "Scheldestromen", waterDefenceMaintainerStakeholderType);
            var wetterskipStakeholder =
                AddStakeholderToAnalysis(analysis, "Wetterskip", waterDefenceMaintainerStakeholderType);
            var rijnlandStakeholder =
                AddStakeholderToAnalysis(analysis, "Rijnland", waterDefenceMaintainerStakeholderType);
            var delflandStakeholder =
                AddStakeholderToAnalysis(analysis, "Delfland", waterDefenceMaintainerStakeholderType);
            var hollandseDeltaStakeholder =
                AddStakeholderToAnalysis(analysis, "Hollandse Delta", waterDefenceMaintainerStakeholderType);
            var rwsZenDStakeholder =
                AddStakeholderToAnalysis(analysis, "RWS - Z&D", waterDefenceMaintainerStakeholderType);
            var rwsNNStakeholder =
                AddStakeholderToAnalysis(analysis, "RWS - NN", waterDefenceMaintainerStakeholderType);
            var nzvStakeholder =
                AddStakeholderToAnalysis(analysis, "Noordezijlvest", waterDefenceMaintainerStakeholderType);
            var henaStakeholder =
                AddStakeholderToAnalysis(analysis, "Hunze en Aa's", waterDefenceMaintainerStakeholderType);
            var wdodStakeholder = AddStakeholderToAnalysis(analysis, "Drents Overijsselse Delta",
                waterDefenceMaintainerStakeholderType);
            var zzlStakeholder =
                AddStakeholderToAnalysis(analysis, "Zuiderzeeland", waterDefenceMaintainerStakeholderType);
            var agenvStakeholder =
                AddStakeholderToAnalysis(analysis, "Amstel Gooi en Vecht", waterDefenceMaintainerStakeholderType);
            var wsvvStakeholder =
                AddStakeholderToAnalysis(analysis, "Vallei en Veluwen", waterDefenceMaintainerStakeholderType);
            var wrijStakeholder =
                AddStakeholderToAnalysis(analysis, "Rijn en IJssel", waterDefenceMaintainerStakeholderType);
            var aaenmStakeholder =
                AddStakeholderToAnalysis(analysis, "Aa en Maas", waterDefenceMaintainerStakeholderType);
            var limburgStakeholder =
                AddStakeholderToAnalysis(analysis, "Limburg", waterDefenceMaintainerStakeholderType);
            var bdeltaStakeholder =
                AddStakeholderToAnalysis(analysis, "Brabantse Delta", waterDefenceMaintainerStakeholderType);
            var rivierenlandStakeholder =
                AddStakeholderToAnalysis(analysis, "Rivierenland", waterDefenceMaintainerStakeholderType);
            var srijStakeholder =
                AddStakeholderToAnalysis(analysis, "Stichtse Rijnlanden", waterDefenceMaintainerStakeholderType);
            var hhskStakeholder = AddStakeholderToAnalysis(analysis, "Schieland en de krimpenerwaard",
                waterDefenceMaintainerStakeholderType);

            var allMarkedPartiesStakeholder =
                AddStakeholderToAnalysis(analysis, "Marktpartijen", marketStakeholderType);
            var hkvStakeholder = AddStakeholderToAnalysis(analysis, "HKV", marketStakeholderType);
            var rhdhvStakeholder = AddStakeholderToAnalysis(analysis, "RHDHV", marketStakeholderType);
            var fugroStakeholder = AddStakeholderToAnalysis(analysis, "Fugro", marketStakeholderType);
            var wiboStakeholder = AddStakeholderToAnalysis(analysis, "Witteveen en Bos", marketStakeholderType);
            var arcStakeholder = AddStakeholderToAnalysis(analysis, "Arcadis", marketStakeholderType);
            var wnetStakeholder = AddStakeholderToAnalysis(analysis, "Waternet", marketStakeholderType);
            var ivInfraStakeholder = AddStakeholderToAnalysis(analysis, "Iv - Infra", marketStakeholderType);
            var anteaStakeholder = AddStakeholderToAnalysis(analysis, "Antea Group", marketStakeholderType);
            var greenRiversStakeholder = AddStakeholderToAnalysis(analysis, "Greenrivers", marketStakeholderType);
            var bwzStakeholder = AddStakeholderToAnalysis(analysis, "BWZ Ingenieurs", marketStakeholderType);
            var inframStakeholder = AddStakeholderToAnalysis(analysis, "Infram", marketStakeholderType);
            var swecoStakeholder = AddStakeholderToAnalysis(analysis, "Sweco", marketStakeholderType);
            var tauwStakeholder = AddStakeholderToAnalysis(analysis, "Tauw", marketStakeholderType);
            var movaresStakeholder = AddStakeholderToAnalysis(analysis, "Movares", marketStakeholderType);
            var csoStakeholder = AddStakeholderToAnalysis(analysis, "CSO Lievense", marketStakeholderType);
            var hydrologicStakeholder = AddStakeholderToAnalysis(analysis, "HydroLogic", marketStakeholderType);
            var avecoStakeholder = AddStakeholderToAnalysis(analysis, "Aveco de Bondt", marketStakeholderType);
            var rpsStakeholder = AddStakeholderToAnalysis(analysis, "RPS", marketStakeholderType);
            var cruxStakeholder = AddStakeholderToAnalysis(analysis, "CRUX", marketStakeholderType);
            var nensStakeholder = AddStakeholderToAnalysis(analysis, "Nelen & Schuurmans", marketStakeholderType);
            var geobestStakeholder = AddStakeholderToAnalysis(analysis, "Geobest", marketStakeholderType);
            var bzimStakeholder = AddStakeholderToAnalysis(analysis, "BZIM", marketStakeholderType);
            var zzpersStakeholder = AddStakeholderToAnalysis(analysis, "ZZPers", marketStakeholderType);
            var boskalisStakeholder = AddStakeholderToAnalysis(analysis, "Boskalis", marketStakeholderType);

            var ihwStakeholder = AddStakeholderToAnalysis(analysis, "IHW", miscStakeholderType);
            var waterschapshuisStakeholder = AddStakeholderToAnalysis(analysis, "Waterschapshuis", miscStakeholderType);
            var technolutionStakeholder =
                AddStakeholderToAnalysis(analysis, "Technolution", knowledgeInstituteStakeholderType);
            var vortechStakeholder = AddStakeholderToAnalysis(analysis, "Vortech", knowledgeInstituteStakeholderType);
            var altenStakeholder = AddStakeholderToAnalysis(analysis, "Alten", knowledgeInstituteStakeholderType);
            //CIO
            // BIT
            // CIV

            var tudStakeholder = AddStakeholderToAnalysis(analysis, "TU Delft", knowledgeInstituteStakeholderType);
            var tutStakeholder = AddStakeholderToAnalysis(analysis, "TU Twente", knowledgeInstituteStakeholderType);
            var vuStakeholder = AddStakeholderToAnalysis(analysis, "VU Amsterdam", knowledgeInstituteStakeholderType);
            var uuStakeholder = AddStakeholderToAnalysis(analysis, "UU", knowledgeInstituteStakeholderType);
            var tnoStakeholder = AddStakeholderToAnalysis(analysis, "TNO", knowledgeInstituteStakeholderType);
            var knmiStakeholder = AddStakeholderToAnalysis(analysis, "KNMI", knowledgeInstituteStakeholderType);
            var alterraStakeholder = AddStakeholderToAnalysis(analysis, "Alterra", knowledgeInstituteStakeholderType);

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
            var enwGroup = AddConnectionGroup(onionDiagram, "ENW", Colors.DarkCyan);
            var enwInternal = AddConnectionGroup(onionDiagram, "ENW - intern", Colors.Black);
            var enwCoast = AddConnectionGroup(onionDiagram, "ENW - Kust", Colors.Black);
            var enwTechniek = AddConnectionGroup(onionDiagram, "ENW - Techniek", Colors.Black);
            var enwVeiligheid = AddConnectionGroup(onionDiagram, "ENW - Veiligheid", Colors.Black);
            var enwRivers = AddConnectionGroup(onionDiagram, "ENW - Rivieren", Colors.Black);
            var nlIngenieurs = AddConnectionGroup(onionDiagram, "NL-ingenieurs", Colors.Purple);
            //Bestuurlijk platform waterveiligheid
            // Taskforce Delta Technologie

            // Connections
            // NL-ingenieurs
            AddMultipleConnections(onionDiagram, nlIngenieurs, nlingenieurs, new[]
            {
                sweco, rhdhv, antea, aveco, fugro, wibo, tauw, arc, hydrologic, cso, ivInfa, movares
            });

            // ENW intern
            AddMultipleConnections(onionDiagram, enwInternal, enw,
                new[] { enwkust, enwrivieren, enwveiligheid, enwtechniek });

            // Themagroep kust
            AddMultipleConnections(onionDiagram, coastGroup, themagroepKust,
                new[] { hhnk, scheldeStromen, wetterskip, rijnland, delfland, hollandseDelta, rwsNN });

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
            AddMultipleConnections(onionDiagram, aiodekiGroup, aio, new[] { dki, dgwb, wvl, kvk, uvw, stowa, ilt });

            // Markt ontwikkelars
            AddMultipleConnections(onionDiagram, marketDevelopersGroup, markt,
                new[] { hkv, arc, rhdhv, cso, alten, vortech, wibo, rps, infram, greenrivers, tno, knmi, zzpers });

            // ENW plenair
            AddMultipleConnections(onionDiagram, enwGroup, enw,
                new[] { dgwb, wdod, tud, deltares, tut, vu, hkv, hhnk, aaenm, wvl });

            // ENW - Kust
            AddMultipleConnections(onionDiagram, enwCoast, enwkust,
                new[] { tud, delfland, hhnk, tut, wetterskip, deltares, rijnland, zzpers, arc, boskalis, rwsZenD });

            //ENW - Rivieren
            AddMultipleConnections(onionDiagram, enwRivers, enwrivieren,
                new[] { tut, zzpers, hkv, deltares, tud, alterra, limburg, uu, wvl });
            // provincie N-Brabant

            //ENW-Veiligheid
            AddMultipleConnections(onionDiagram, enwVeiligheid, enwveiligheid,
                new[] { hkv, tud, tno, fugro, deltares, zzpers, scheldeStromen, wvl, arc });
            //cpb
            // Provincie Overijssel
            // Planbureau voor de leefomgeving

            //ENW-Techniek
            AddMultipleConnections(onionDiagram, enwTechniek, enwtechniek,
                new[] { deltares, wibo, hhsk, wvl, tud, fugro, zzpers, rivierenland });
            // Volker staal en funderingen
            // Hogeschool Rotterdam
            // GPO
            // WL Vlaanderen

            #endregion

            #region Add Force field diagram

            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(wlvStakeholder, 0.9, 1.0));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(dgwbStakeholder, 0.94, 1.0));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(deltaresStakeholder, 0.9, 0.9));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(markedStakeholder, 0.8, 0.8));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(kkpStakeholder, 1.0, 0.5));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(enwStakeholder, 0.6, 0.4));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(enwCoastStakeholder, 1.0, 0.5));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(enwRiversStakeholder, 1.0, 0.5));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(enwTechnicStakeholder, 1.0, 0.5));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(enwSafetyStakeholder, 1.0, 0.5));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(uvwStakeholder, 0.9, 0.6));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(wwkStakeholder, 0.85, 0.55));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(cwkStakeholder, 0.85, 0.5));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(aioStakeholder, 0.7, 0.7));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(dkiStakeholder, 0.6, 0.7));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(nlIngenieursStakeholder, 0.1, 0.2));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(hwbpStakeholder, 0.5, 0.55));

            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(iltStakeholder, 0.8, 0.5));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(wateropleidingenStakeholder, 0.6, 0.2));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(kvkStakeholder, 0.4, 0.6));

            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(themagroepKustStakeholder, 0.9, 0.3));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(stowaStakeholder, 0.7, 0.3));

            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(hhnkStakeholder, 0.8, 0.55));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(scheldeStromenStakeholder, 0.8, 0.5));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(wetterskipStakeholder, 0.8, 0.5));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(rijnlandStakeholder, 0.8, 0.5));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(delflandStakeholder, 0.8, 0.5));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(hollandseDeltaStakeholder, 0.8, 0.5));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(rwsZenDStakeholder, 0.8, 0.4));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(rwsNNStakeholder, 0.8, 0.4));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(nzvStakeholder, 0.8, 0.4));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(henaStakeholder, 0.8, 0.4));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(wdodStakeholder, 0.8, 0.4));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(zzlStakeholder, 0.8, 0.5));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(agenvStakeholder, 0.8, 0.4));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(wsvvStakeholder, 0.8, 0.5));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(wrijStakeholder, 0.8, 0.5));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(aaenmStakeholder, 0.8, 0.5));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(limburgStakeholder, 0.8, 0.55));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(bdeltaStakeholder, 0.8, 0.4));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(rivierenlandStakeholder, 0.8, 0.5));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(srijStakeholder, 0.8, 0.4));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(hhskStakeholder, 0.8, 0.4));

            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(hkvStakeholder, 0.7, 0.65));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(rhdhvStakeholder, 0.64, 0.4));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(fugroStakeholder, 0.5, 0.4));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(wiboStakeholder, 0.6, 0.5));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(arcStakeholder, 0.6, 0.6));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(wnetStakeholder, 0.5, 0.2));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(ivInfraStakeholder, 0.5, 0.25));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(anteaStakeholder, 0.6, 0.2));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(greenRiversStakeholder, 0.7, 0.65));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(bwzStakeholder, 0.5, 0.2));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(inframStakeholder, 0.6, 0.5));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(swecoStakeholder, 0.55, 0.5));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(tauwStakeholder, 0.45, 0.35));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(movaresStakeholder, 0.6, 0.5));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(csoStakeholder, 0.58, 0.55));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(hydrologicStakeholder, 0.5, 0.22));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(avecoStakeholder, 0.5, 0.2));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(rpsStakeholder, 0.56, 0.4));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(cruxStakeholder, 0.6, 0.15));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(nensStakeholder, 0.45, 0.65));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(geobestStakeholder, 0.57, 0.26));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(bzimStakeholder, 0.54, 0.18));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(zzpersStakeholder, 0.6, 0.5));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(boskalisStakeholder, 0.6, 0.5));

            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(ihwStakeholder, 0.3, 0.5));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(waterschapshuisStakeholder, 0.25, 0.4));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(technolutionStakeholder, 0.1, 0.3));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(vortechStakeholder, 0.2, 0.5));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(altenStakeholder, 0.14, 0.7));

            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(tudStakeholder, 0.25, 0.45));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(tutStakeholder, 0.25, 0.45));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(vuStakeholder, 0.16, 0.15));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(uuStakeholder, 0.15, 0.17));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(tnoStakeholder, 0.23, 0.56));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(knmiStakeholder, 0.6, 0.5));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(alterraStakeholder, 0.6, 0.5));

            for (var i = 0; i < forceFieldDiagram.Stakeholders.Count; i++) forceFieldDiagram.Stakeholders[i].Rank = i;

            #endregion

            #region Add Force field diagram 2

            forceFieldDiagram = new ForceFieldDiagram("BOI-krachtenveld (simpel)");
            analysis.ForceFieldDiagrams.Add(forceFieldDiagram);

            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(wlvStakeholder, 0.9, 1.0));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(dgwbStakeholder, 0.94, 1.0));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(deltaresStakeholder, 0.9, 0.9));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(markedStakeholder, 0.8, 0.8));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(kkpStakeholder, 1.0, 0.5));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(enwStakeholder, 0.6, 0.4));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(uvwStakeholder, 0.9, 0.6));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(wwkStakeholder, 0.85, 0.55));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(cwkStakeholder, 0.85, 0.5));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(aioStakeholder, 0.7, 0.7));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(dkiStakeholder, 0.6, 0.7));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(nlIngenieursStakeholder, 0.1, 0.2));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(hwbpStakeholder, 0.5, 0.55));

            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(iltStakeholder, 0.8, 0.5));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(wateropleidingenStakeholder, 0.6, 0.2));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(kvkStakeholder, 0.4, 0.6));

            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(themagroepKustStakeholder, 0.9, 0.3));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(stowaStakeholder, 0.7, 0.3));

            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(waterAuthoritiesStakeholder, 0.8,
                0.55));

            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(allMarkedPartiesStakeholder, 0.7,
                0.65));

            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(ihwStakeholder, 0.3, 0.5));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(waterschapshuisStakeholder, 0.25, 0.4));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(technolutionStakeholder, 0.1, 0.3));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(vortechStakeholder, 0.2, 0.5));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(altenStakeholder, 0.14, 0.7));

            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(tudStakeholder, 0.25, 0.45));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(tutStakeholder, 0.25, 0.45));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(vuStakeholder, 0.16, 0.15));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(uuStakeholder, 0.15, 0.17));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(tnoStakeholder, 0.23, 0.56));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(knmiStakeholder, 0.6, 0.5));
            forceFieldDiagram.Stakeholders.Add(new PositionedStakeholder(alterraStakeholder, 0.6, 0.5));
            for (var i = 0; i < forceFieldDiagram.Stakeholders.Count; i++) forceFieldDiagram.Stakeholders[i].Rank = i;

            #endregion

            #region Attitude impact diagram

            attitudeImpactDiagram.Stakeholders.Add(new PositionedStakeholder(wlvStakeholder, 0.95, 0.9));
            attitudeImpactDiagram.Stakeholders.Add(new PositionedStakeholder(dgwbStakeholder, 0.9, 0.95));
            attitudeImpactDiagram.Stakeholders.Add(new PositionedStakeholder(deltaresStakeholder, 0.9, 0.8));
            attitudeImpactDiagram.Stakeholders.Add(new PositionedStakeholder(markedStakeholder, 0.85, 0.6));

            attitudeImpactDiagram.Stakeholders.Add(new PositionedStakeholder(kkpStakeholder, 0.9, 0.9));
            attitudeImpactDiagram.Stakeholders.Add(new PositionedStakeholder(enwStakeholder, 0.7, 0.7));
            attitudeImpactDiagram.Stakeholders.Add(new PositionedStakeholder(enwCoastStakeholder, 0.5, 0.4));
            attitudeImpactDiagram.Stakeholders.Add(
                new PositionedStakeholder(enwRiversStakeholder, 0.6, 0.4));
            attitudeImpactDiagram.Stakeholders.Add(
                new PositionedStakeholder(enwTechnicStakeholder, 0.55, 0.4));
            attitudeImpactDiagram.Stakeholders.Add(
                new PositionedStakeholder(enwSafetyStakeholder, 0.5, 0.43));

            attitudeImpactDiagram.Stakeholders.Add(new PositionedStakeholder(uvwStakeholder, 0.4, 0.8));
            attitudeImpactDiagram.Stakeholders.Add(new PositionedStakeholder(wwkStakeholder, 0.4, 0.6));
            attitudeImpactDiagram.Stakeholders.Add(new PositionedStakeholder(cwkStakeholder, 0.4, 0.5));
            attitudeImpactDiagram.Stakeholders.Add(new PositionedStakeholder(aioStakeholder, 0.7, 0.7));
            attitudeImpactDiagram.Stakeholders.Add(new PositionedStakeholder(dkiStakeholder, 0.6, 0.8));
            attitudeImpactDiagram.Stakeholders.Add(
                new PositionedStakeholder(nlIngenieursStakeholder, 0.8, 0.1));
            attitudeImpactDiagram.Stakeholders.Add(new PositionedStakeholder(hwbpStakeholder, 0.3, 0.98));

            attitudeImpactDiagram.Stakeholders.Add(new PositionedStakeholder(iltStakeholder, 0.7, 0.55));
            attitudeImpactDiagram.Stakeholders.Add(
                new PositionedStakeholder(wateropleidingenStakeholder, 0.9, 0.3));
            attitudeImpactDiagram.Stakeholders.Add(new PositionedStakeholder(kvkStakeholder, 0.75, 0.4));

            attitudeImpactDiagram.Stakeholders.Add(
                new PositionedStakeholder(themagroepKustStakeholder, 0.7, 0.4));
            attitudeImpactDiagram.Stakeholders.Add(new PositionedStakeholder(stowaStakeholder, 0.6, 0.5));

            attitudeImpactDiagram.Stakeholders.Add(new PositionedStakeholder(hhnkStakeholder, 0.8, 0.8));
            attitudeImpactDiagram.Stakeholders.Add(
                new PositionedStakeholder(scheldeStromenStakeholder, 0.5, 0.65));
            attitudeImpactDiagram.Stakeholders.Add(
                new PositionedStakeholder(wetterskipStakeholder, 0.55, 0.68));
            attitudeImpactDiagram.Stakeholders.Add(
                new PositionedStakeholder(rijnlandStakeholder, 0.8, 0.45));
            attitudeImpactDiagram.Stakeholders.Add(new PositionedStakeholder(delflandStakeholder, 0.7, 0.4));
            attitudeImpactDiagram.Stakeholders.Add(
                new PositionedStakeholder(hollandseDeltaStakeholder, 0.7, 0.4));
            attitudeImpactDiagram.Stakeholders.Add(new PositionedStakeholder(rwsZenDStakeholder, 0.6, 0.2));
            attitudeImpactDiagram.Stakeholders.Add(new PositionedStakeholder(rwsNNStakeholder, 0.6, 0.2));
            attitudeImpactDiagram.Stakeholders.Add(new PositionedStakeholder(nzvStakeholder, 0.6, 0.67));
            attitudeImpactDiagram.Stakeholders.Add(new PositionedStakeholder(henaStakeholder, 0.7, 0.4));
            attitudeImpactDiagram.Stakeholders.Add(new PositionedStakeholder(wdodStakeholder, 0.7, 0.4));
            attitudeImpactDiagram.Stakeholders.Add(new PositionedStakeholder(zzlStakeholder, 0.78, 0.6));
            attitudeImpactDiagram.Stakeholders.Add(new PositionedStakeholder(agenvStakeholder, 0.6, 0.2));
            attitudeImpactDiagram.Stakeholders.Add(new PositionedStakeholder(wsvvStakeholder, 0.6, 0.43));
            attitudeImpactDiagram.Stakeholders.Add(new PositionedStakeholder(wrijStakeholder, 0.6, 0.49));
            attitudeImpactDiagram.Stakeholders.Add(new PositionedStakeholder(aaenmStakeholder, 0.61, 0.43));
            attitudeImpactDiagram.Stakeholders.Add(new PositionedStakeholder(limburgStakeholder, 0.7, 0.8));
            attitudeImpactDiagram.Stakeholders.Add(new PositionedStakeholder(bdeltaStakeholder, 0.65, 0.43));
            attitudeImpactDiagram.Stakeholders.Add(
                new PositionedStakeholder(rivierenlandStakeholder, 0.82, 0.8));
            attitudeImpactDiagram.Stakeholders.Add(new PositionedStakeholder(srijStakeholder, 0.6, 0.41));
            attitudeImpactDiagram.Stakeholders.Add(new PositionedStakeholder(hhskStakeholder, 0.6, 0.72));

            attitudeImpactDiagram.Stakeholders.Add(new PositionedStakeholder(hkvStakeholder, 0.75, 0.4));
            attitudeImpactDiagram.Stakeholders.Add(new PositionedStakeholder(rhdhvStakeholder, 0.72, 0.3));
            attitudeImpactDiagram.Stakeholders.Add(new PositionedStakeholder(fugroStakeholder, 0.6, 0.15));
            attitudeImpactDiagram.Stakeholders.Add(new PositionedStakeholder(wiboStakeholder, 0.6, 0.15));
            attitudeImpactDiagram.Stakeholders.Add(new PositionedStakeholder(arcStakeholder, 0.6, 0.15));
            attitudeImpactDiagram.Stakeholders.Add(new PositionedStakeholder(wnetStakeholder, 0.6, 0.1));
            attitudeImpactDiagram.Stakeholders.Add(new PositionedStakeholder(ivInfraStakeholder, 0.6, 0.05));
            attitudeImpactDiagram.Stakeholders.Add(new PositionedStakeholder(anteaStakeholder, 0.6, 0.01));
            attitudeImpactDiagram.Stakeholders.Add(
                new PositionedStakeholder(greenRiversStakeholder, 0.6, 0.35));
            attitudeImpactDiagram.Stakeholders.Add(new PositionedStakeholder(bwzStakeholder, 0.6, 0.02));
            attitudeImpactDiagram.Stakeholders.Add(new PositionedStakeholder(inframStakeholder, 0.6, 0.15));
            attitudeImpactDiagram.Stakeholders.Add(new PositionedStakeholder(swecoStakeholder, 0.6, 0.15));
            attitudeImpactDiagram.Stakeholders.Add(new PositionedStakeholder(tauwStakeholder, 0.6, 0.15));
            attitudeImpactDiagram.Stakeholders.Add(new PositionedStakeholder(movaresStakeholder, 0.6, 0.25));
            attitudeImpactDiagram.Stakeholders.Add(new PositionedStakeholder(csoStakeholder, 0.6, 0.3));
            attitudeImpactDiagram.Stakeholders.Add(
                new PositionedStakeholder(hydrologicStakeholder, 0.6, 0.02));
            attitudeImpactDiagram.Stakeholders.Add(new PositionedStakeholder(avecoStakeholder, 0.6, 0.01));
            attitudeImpactDiagram.Stakeholders.Add(new PositionedStakeholder(rpsStakeholder, 0.6, 0.15));
            attitudeImpactDiagram.Stakeholders.Add(new PositionedStakeholder(cruxStakeholder, 0.6, 0.01));
            attitudeImpactDiagram.Stakeholders.Add(new PositionedStakeholder(nensStakeholder, 0.6, 0.25));
            attitudeImpactDiagram.Stakeholders.Add(new PositionedStakeholder(geobestStakeholder, 0.6, 0.01));
            attitudeImpactDiagram.Stakeholders.Add(new PositionedStakeholder(bzimStakeholder, 0.6, 0.03));
            attitudeImpactDiagram.Stakeholders.Add(new PositionedStakeholder(zzpersStakeholder, 0.6, 0.35));
            attitudeImpactDiagram.Stakeholders.Add(
                new PositionedStakeholder(boskalisStakeholder, 0.6, 0.15));

            attitudeImpactDiagram.Stakeholders.Add(new PositionedStakeholder(ihwStakeholder, 0.56, 0.4));
            attitudeImpactDiagram.Stakeholders.Add(
                new PositionedStakeholder(waterschapshuisStakeholder, 0.6, 0.15));
            attitudeImpactDiagram.Stakeholders.Add(
                new PositionedStakeholder(technolutionStakeholder, 0.6, 0.03));
            attitudeImpactDiagram.Stakeholders.Add(new PositionedStakeholder(vortechStakeholder, 0.7, 0.26));
            attitudeImpactDiagram.Stakeholders.Add(new PositionedStakeholder(altenStakeholder, 0.6, 0.15));

            attitudeImpactDiagram.Stakeholders.Add(new PositionedStakeholder(tudStakeholder, 0.75, 0.49));
            attitudeImpactDiagram.Stakeholders.Add(new PositionedStakeholder(tutStakeholder, 0.75, 0.43));
            attitudeImpactDiagram.Stakeholders.Add(new PositionedStakeholder(vuStakeholder, 0.72, 0.40));
            attitudeImpactDiagram.Stakeholders.Add(new PositionedStakeholder(uuStakeholder, 0.76, 0.41));
            attitudeImpactDiagram.Stakeholders.Add(new PositionedStakeholder(tnoStakeholder, 0.8, 0.3));
            attitudeImpactDiagram.Stakeholders.Add(new PositionedStakeholder(knmiStakeholder, 0.76, 0.36));
            attitudeImpactDiagram.Stakeholders.Add(new PositionedStakeholder(alterraStakeholder, 0.6, 0.15));
            for (var i = 0; i < attitudeImpactDiagram.Stakeholders.Count; i++)
                attitudeImpactDiagram.Stakeholders[i].Rank = i;

            #endregion

            return analysis;
        }

        private static StakeholderType AddStakeholderType(Analysis analysis, string name, StakeholderIconType iconType,
            Color color)
        {
            var stakeholderType = new StakeholderType { Name = name, IconType = iconType, Color = color };
            analysis.StakeholderTypes.Add(stakeholderType);
            return stakeholderType;
        }

        private static void AddMultipleConnections(OnionDiagram diagram,
            StakeholderConnectionGroup stakeholderConnectionGroup, PositionedStakeholder baseStakeholder,
            IEnumerable<PositionedStakeholder> stakeholders)
        {
            foreach (var stakeholder in stakeholders)
                diagram.Connections.Add(new StakeholderConnection(stakeholderConnectionGroup, baseStakeholder,
                    stakeholder));
        }

        private static StakeholderConnectionGroup AddConnectionGroup(OnionDiagram diagram, string groupName,
            Color groupColor, LineStyle lineStyle = LineStyle.Solid, bool isVisible = true)
        {
            var coastGroup = new StakeholderConnectionGroup(groupName, groupColor, 1.0, lineStyle, isVisible);
            diagram.ConnectionGroups.Add(coastGroup);
            return coastGroup;
        }

        private static PositionedStakeholder AddStakeholderToOnionDiagram(Stakeholder stakeholder,
            OnionDiagram diagram,
            double leftPercentage, double rightPercentage)
        {
            var diagramStakeholder = new PositionedStakeholder(stakeholder, leftPercentage, rightPercentage)
            {
                Rank = diagram.Stakeholders.Count
            };
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