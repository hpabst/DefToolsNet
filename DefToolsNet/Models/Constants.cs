﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefToolsNet.Models
{

    public enum WowClass
    {
        Unknown = 0,
        UNKNOWN = 0,
        Priest = 5,
        PRIEST = 5,
        Mage = 8,
        MAGE = 8,
        Warlock = 9,
        WARLOCK = 9,
        Rogue = 4,
        ROGUE = 4,
        DemonHunter = 12,
        DEMONHUNTER = 12,
        Monk = 10,
        MONK = 10,
        Druid = 11,
        DRUID = 11,
        Shaman = 7,
        SHAMAN = 7,
        Hunter = 3,
        HUNTER = 3,
        Paladin = 2,
        PALADIN = 2,
        Warrior = 1,
        WARRIOR = 1,
        DeathKnight = 6,
        DEATHKNIGHT = 6
    }

    public enum Zone : int
    {
        Unknown = int.MaxValue,

        World = -1,
        Kalimdor = 13,
        AhnQiraj = 772,
        AmmenVale = 894,
        Ashenvale = 43,
        Azshara = 181,
        AzuremystIsle = 464,
        BloodmystIsle = 476,
        CampNarache = 890,
        Darkshore = 42,
        Darnassus = 381,
        Desolace = 101,
        Durotar = 4,
        DustwallowMarsh = 141,
        EchoIsles = 891,
        Felwood = 182,
        Feralas = 121,
        MoltenFront = 795,
        Moonglade = 241,
        MountHyjal = 606,
        Mulgore = 9,
        NorthernBarrens = 11,
        Orgrimmar = 321,
        Shadowglen = 888,
        Silithus = 261,
        SouthernBarrens = 607,
        StonetalonMountains = 81,
        Tanaris = 161,
        Teldrassil = 41,
        TheExodar = 471,
        ThousandNeedles = 61,
        ThunderBluff = 362,
        Uldum = 720,
        UnGoroCrater = 201,
        ValleyOfTrials = 889,
        Winterspring = 281,

        EasternKingdoms = 14,
        AbyssalDepths = 614,
        ArathiHighlands = 16,
        Badlands = 17,
        BlastedLands = 19,
        BurningSteppes = 29,
        ColdridgeValley = 866,
        DeadwindPass = 32,
        Deathknell = 892,
        DunMorogh = 27,
        Duskwood = 34,
        EasternPlaguelands = 23,
        ElwynnForest = 30,
        EversongWoods = 462,
        Ghostlands = 463,
        Gilneas = 545,
        HillsbradFoothills = 24,
        Ironforge = 341,
        IsleOfQuelDanas = 499,
        KelptharForest = 610,
        LochModan = 35,
        NewTinkertown = 895,
        NothernStranglethorn = 37,
        Northshire = 864,
        RedridgeMountains = 36,
        RuinsOfGilneas = 684,
        RuinsOfGilneasCity = 685,
        SearingGorge = 28,
        ShimmeringExpanse = 615,
        SilvermoonCity = 480,
        SilverpineForest = 21,
        StormwindCity = 301,
        StranglethornValue = 689,
        SunstriderIsle = 893,
        SwampOfSorrows = 38,
        TheCapeOfStranglethorn = 673,
        TheHinterlands = 26,
        TheScarletEnclave = 502,
        TirisfalGlades = 20,
        TolBarad = 708,
        TolBaradPeninsula = 709,
        TwilightHighlands = 700,
        Undercity = 382,
        Vashjir = 613,
        WesternPlaguelands = 22,
        Westfall = 39,
        Wetlands = 40,

        Outland = 466,
        BladesEdgeMountains = 475,
        HellfirePeninsula = 465,
        Nagrand = 477,
        Netherstorm = 479,
        ShadowmoonValley = 473,
        ShattrathCity = 481,
        TerokkarForest = 478,
        Zangamarsh = 467,

        Northrend = 485,
        BoreanTundra = 486,
        CrystalsongForest = 510,
        Dalaran = 504,
        Dragonblight = 488,
        GrizzlyHills = 490,
        HowlingFjord = 491,
        HrothgarsLanding = 541,
        Icecrown = 492,
        SholazarBasin = 493,
        TheStormPeaks = 495,
        Wintergrasp = 501,
        ZulDrak = 496,

        TheMaelstrom = 751,
        Deepholm = 640,
        Kezan = 605,
        TheLostIsles = 544,
        TheMaelstrom2 = 737,

        Pandaria = 862,
        DreadWastes = 858,
        IsleOfGiants = 929,
        IsleOfThunder = 928,
        KrasarangWilds = 857,
        KunLaiSummit = 809,
        ShrineOfSevenStars = 905,
        ShrineOfTwoMoons = 903,
        TheJadeForest = 806,
        TheVeiledStair = 873,
        TheWanderingIsle = 808,
        TimelessIsle = 951,
        TownlongSteppes = 810,
        ValeOfEternalBlossoms = 811,
        ValleyOfTheFourWinds = 807,

        Draenor = 962,
        Ashran = 978,
        FrostfireRidge = 941,
        Frostwall = 976,
        Gorgrond = 949,
        Lunarfall = 971,
        NagrandDraenor = 950,
        ShadowmoonValleyDraenor = 947,
        SpiresOfArak = 948,
        Stormshield = 1009,
        Talador = 946,
        TanaanJungle = 945,
        TanaanJungleAssaultOnTheDarkPortal = 970,
        Warspear = 1011,

        BrokenIsles = 1007,
        Aszuna = 1015,
        BrokenShore = 1021,
        DalaranBrokenIsles = 1021,
        EyeOfAzsharaZone = 1098,
        Highmountain = 1024,
        Stormheim = 1017,
        Suramar = 1033,
        Valsharah = 1018,

        HallOfTheGuardian = 1068,
        MardumTheShatteredAbyss = 1052,
        NetherlightTemple = 1040,
        Skyhold = 1035,
        TheDreamgrove = 1077,
        TheHeartOfAzeroth = 1057,
        TheWanderingIsleOrderHall = 1044,
        TrueshotLodge = 1072,

        AlteracValley = 401,
        ArathiBasin = 461,
        DeepwindGorge = 935,
        EyeOfTheStorm = 482,
        IsleOfConquest = 540,
        SilvershardMines = 860,
        StrandOfTheAncients = 512,
        TempleOfKotmogu = 856,
        TheBattleForGilneas = 736,
        TwinPeaks = 626,
        WarsongGulch = 443,

        BlackfathomDeeps = 688,
        BlackrockDepths = 704,
        BlackrockSpire = 721,
        DireMaul = 699,
        Gnomeregan = 691,
        Maraudon = 750,
        RagefireChasm = 680,
        RazorfenDowns = 760,
        RazorfenKraul = 761,
        ShadowfangKeep = 764,
        Stratholme = 765,
        TheDeadmines = 756,
        TheStockade = 690,
        TheTempleOfAtalHakkar = 687,
        Uldaman = 692,
        WailingCaverns = 749,
        ZulFarrak = 686,

        BlackwingLair = 755,
        MoltenCore = 696,
        RuinsOfAhnQiraj = 717,
        TempleOfAhnQiraj = 766,

        AuchenaiCrypts = 722,
        HellfireRamparts = 797,
        MagistersTerrace = 798,
        ManaTombs = 732,
        OldHillisbradFoothills = 734,
        SethekkHalls = 723,
        ShadowLabyrinth = 724,
        TheArcatraz = 731,
        TheBlackMorass = 733,
        TheBloodFurance = 725,
        TheBotanica = 729,
        TheMechanar = 730,
        TheShatteredHalls = 710,
        TheSlacePens = 728,
        TheSteamvault = 727,
        TheUnderbog = 726,

        BlackTemple = 796,
        GruulsLair = 776,
        HyjalSummit = 775,
        Karazhan = 799,
        MagtheridonsLair = 779,
        SerpentshrineCavern = 780,
        SunwellPlateau = 789,
        TheEye = 782,

        AhnkahetTheOldKingdom = 522,
        AzjolNerub = 533,
        DrakTharonKeep = 534,
        Gundrak = 530,
        HallsOfLightning = 525,
        HallsOfReflection = 603,
        HallsOfStone = 526,
        PitOfSaron = 602,
        TheCullingOfStratholme = 521,
        TheForgeOfSouls = 601,
        TheNexus = 520,
        TheOculus = 528,
        TheVioletHold = 536,
        TrialOfTheChampion = 542,
        UtgardeKeep = 523,
        UtgardePinnacle = 524,

        IcecrownCitadel = 604,
        Naxxramas = 535,
        OnyxiasLair = 718,
        TheEyeOfEternity = 527,
        TheObsidianSanctum = 531,
        TheRubySanctum = 609,
        TrialOfTheCrusader = 543,
        Ulduar = 529,
        VaultOfArchavon = 532,

        BlackrockCaverns = 753,
        EndTime = 820,
        GrimBatol = 757,
        HallsOfOrigination = 759,
        HourOfTwilight = 759,
        LostCityOfTheTolvir = 747,
        TheStonecore = 768,
        TheVortexPinnacle = 769,
        ThroneOfTheTides = 767,
        WellOfEternity = 816,
        ZulAman = 781,
        ZulGurub = 793,

        BaradinHold = 752,
        BlackwingDescent = 754,
        DragonSoul = 824,
        Firelands = 800,
        TheBastionOfTwilight = 758,
        ThroneOfTheFourWinds = 773,

        GateOfTheSettingSun = 875,
        MoguShanPalace = 885,
        ScarletHalls = 871,
        ScarletMonastery = 877,
        Scholomance = 898,
        ShadopanMonastery = 877,
        SiegeOfNiuzaoTemple = 887,
        StormstoutBrewery = 876,
        TempleOfTheJadeSerpent = 867,

        HeartOfFear = 897,
        MogushanVaults = 896,
        SiegeOfOrgrimmar = 953,
        TerraceOfEndlessSpring = 886,
        ThroneOfThunder = 930,

        Auchindoun = 984,
        BloodmaulSlagMines = 964,
        GrimrailDepot = 993,
        IronDocks = 987,
        ShadowmoonBurialGrounds = 969,
        Skyreach = 998,
        TheEverbloom = 1008,
        UpperBlackrockSpire = 995,

        Highmaul = 994,
        BlackrockFoundry = 988,
        HellfireCitadel = 1026,

        BlackRookHold = 1081,
        CathedralOfEternalNight = 1146,
        CourtOfStars = 1087,
        DarkheartThicket = 1067,
        EyeOfAzshara = 1046,
        HallsOfValor = 1041,
        MawOfSouls = 1042,
        NeltharionsLair = 1065,
        ReturnToKarazhan = 1115,
        TheArcway = 1079,
        VaultOfTheWardens = 1045,
        VioletHoldLegion = 1066,

        TheEmeraldNightmare = 1094,
        TrialOfValor = 1114,
        TheNighthold = 1088,
        TombOfSargeras = 1147
    }

    class Constants
    {

    }
}
