class NepaliDate {
    constructor() {
        this.NEPALI_NUMBER = ["०", "१", "२", "३", "४", "५", "६", "७", "८", "९"];
        this.NEPALI_WEEK = [
            "आइतबार",
            "सोमबार",
            "मङ्गलबार",
            "बुधबार",
            "बिहिबार",
            "शुक्रबार",
            "शनिवार",
        ];
        this.ENLISH_WEEK = [
            "Sunday",
            "Monday",
            "Tuesday",
            "Wednesday",
            "Thursday",
            "Friday",
            "Saturday",
        ];
        this.ENGLISH_MONTH = [
            "January",
            "February",
            "March",
            "April",
            "May",
            "June",
            "July",
            "August",
            "September",
            "October",
            "November",
            "December",
        ];
        this.NEPALI_MONTH = [
            "वैशाख",
            "जेठ",
            "असार",
            "साउन",
            "भदौ",
            "असोज",
            "कार्तिक",
            "मङ्सिर",
            "पुस",
            "माघ",
            "फाल्गुण",
            "चैत",
        ];
        this.NEPALI_YEARS = {
            1975: [31, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31],
            1976: [31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 30],
            1977: [31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30],
            1978: [31, 31, 31, 32, 31, 31, 30, 29, 30, 29, 30, 30],
            1979: [31, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31],
            1980: [31, 32, 31, 32, 31, 30, 30, 29, 30, 29, 30, 30],
            1981: [31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30],
            1982: [31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 30, 30],
            1983: [31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31],
            1984: [31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30],
            1985: [31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30],
            1986: [31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 30, 30],
            1987: [31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31],
            1988: [31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30],
            1989: [31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30],
            1990: [31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 29, 31],
            1991: [31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31],
            1992: [31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30],
            1993: [31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30],
            1994: [30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31],
            1995: [31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31],
            1996: [31, 31, 32, 32, 31, 30, 29, 30, 30, 29, 30, 30],
            1997: [31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30],
            1998: [30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31],
            1999: [31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31],
            2000: [30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31],
            2001: [31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30],
            2002: [31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30],
            2003: [31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31],
            2004: [30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31],
            2005: [31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30],
            2006: [31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30],
            2007: [31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31],
            2008: [31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 29, 31],
            2009: [31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30],
            2010: [31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30],
            2011: [31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31],
            2012: [31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 30, 30],
            2013: [31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30],
            2014: [31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30],
            2015: [31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31],
            2016: [31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 30, 30],
            2017: [31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30],
            2018: [31, 32, 31, 32, 31, 30, 30, 29, 30, 29, 30, 30],
            2019: [31, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31],
            2020: [31, 31, 31, 32, 31, 31, 30, 29, 30, 29, 30, 30],
            2021: [31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30],
            2022: [31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 30],
            2023: [31, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31],
            2024: [31, 31, 31, 32, 31, 31, 30, 29, 30, 29, 30, 30],
            2025: [31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30],
            2026: [31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31],
            2027: [30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31],
            2028: [31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30],
            2029: [31, 31, 32, 31, 32, 30, 30, 29, 30, 29, 30, 30],
            2030: [31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31],
            2031: [30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31],
            2032: [31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30],
            2033: [31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30],
            2034: [31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31],
            2035: [30, 32, 31, 32, 31, 31, 29, 30, 30, 29, 29, 31],
            2036: [31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30],
            2037: [31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30],
            2038: [31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31],
            2039: [31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 30, 30],
            2040: [31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30],
            2041: [31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30],
            2042: [31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31],
            2043: [31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 30, 30],
            2044: [31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30],
            2045: [31, 32, 31, 32, 31, 30, 30, 29, 30, 29, 30, 30],
            2046: [31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31],
            2047: [31, 31, 31, 32, 31, 31, 30, 29, 30, 29, 30, 30],
            2048: [31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30],
            2049: [31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 30],
            2050: [31, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31],
            2051: [31, 31, 31, 32, 31, 31, 30, 29, 30, 29, 30, 30],
            2052: [31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30],
            2053: [31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 30],
            2054: [31, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31],
            2055: [31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30],
            2056: [31, 31, 32, 31, 32, 30, 30, 29, 30, 29, 30, 30],
            2057: [31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31],
            2058: [30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31],
            2059: [31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30],
            2060: [31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30],
            2061: [31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31],
            2062: [30, 32, 31, 32, 31, 31, 29, 30, 29, 30, 29, 31],
            2063: [31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30],
            2064: [31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30],
            2065: [31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31],
            2066: [31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 29, 31],
            2067: [31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30],
            2068: [31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30],
            2069: [31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31],
            2070: [31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 30, 30],
            2071: [31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30],
            2072: [31, 32, 31, 32, 31, 30, 30, 29, 30, 29, 30, 30],
            2073: [31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31],
            2074: [31, 31, 31, 32, 31, 31, 30, 29, 30, 29, 30, 30],
            2075: [31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30],
            2076: [31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 30],
            2077: [31, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31],
            2078: [31, 31, 31, 32, 31, 31, 30, 29, 30, 29, 30, 30],
            2079: [31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30],
            2080: [31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 30],
            2081: [31, 32, 31, 32, 31, 30, 30, 30, 29, 30, 30, 30],
            2082: [30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 30, 30],
            2083: [31, 31, 32, 31, 31, 30, 30, 30, 29, 30, 30, 30],
            2084: [31, 31, 32, 31, 31, 30, 30, 30, 29, 30, 30, 30],
            2085: [31, 32, 31, 32, 30, 31, 30, 30, 29, 30, 30, 30],
            2086: [30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 30, 30],
            2087: [31, 31, 32, 31, 31, 31, 30, 30, 29, 30, 30, 30],
            2088: [30, 31, 32, 32, 30, 31, 30, 30, 29, 30, 30, 30],
            2089: [30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 30, 30],
            2090: [30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 30, 30],
            2091: [31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30],
            2092: [30, 31, 32, 32, 31, 30, 30, 30, 29, 30, 30, 30],
            2093: [30, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 30],
            2094: [31, 31, 32, 31, 31, 30, 30, 30, 29, 30, 30, 30],
            2095: [31, 31, 32, 31, 31, 31, 30, 29, 30, 30, 30, 30],
            2096: [30, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30],
            2097: [31, 32, 31, 32, 31, 30, 30, 30, 29, 30, 30, 30],
            2098: [31, 31, 32, 31, 31, 31, 29, 30, 29, 30, 29, 31],
            2099: [31, 31, 32, 31, 31, 31, 30, 29, 29, 30, 30, 30],
            2100: [31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31],
            2101: [31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30],
            2102: [31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30],
            2103: [31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31],
            2104: [30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31],
            2105: [31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30],
            2106: [31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30],
            2107: [31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31],
            2108: [31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 29, 31],
            2109: [31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30],
            2110: [31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30],
            2111: [31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31],
            2112: [31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 30, 30],
            2113: [31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30],
            2114: [31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30],
            2115: [31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31],
            2116: [31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 30, 30],
            2117: [31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30],
            2118: [31, 32, 31, 32, 31, 30, 30, 29, 30, 29, 30, 30],
            2119: [31, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31],
            2120: [31, 31, 31, 32, 31, 31, 30, 29, 30, 29, 30, 30],
            2121: [31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30],
            2122: [31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 30],
            2123: [31, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31],
            2124: [31, 31, 31, 32, 31, 31, 30, 29, 30, 29, 30, 30],
            2125: [31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30],
            2126: [31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31],
            2127: [30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31],
            2128: [31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30],
            2129: [31, 31, 32, 31, 32, 30, 30, 29, 30, 29, 30, 30],
            2130: [31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31],
            2131: [30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31],
            2132: [31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30],
            2133: [31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30],
            2134: [31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31],
            2135: [30, 32, 31, 32, 31, 31, 29, 30, 30, 29, 29, 31],
            2136: [31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30],
            2137: [31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30],
            2138: [31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31],
            2139: [31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 30, 30],
            2140: [31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30],
            2141: [31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30],
            2142: [31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31],
            2143: [31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 30, 30],
            2144: [31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30],
            2145: [31, 32, 31, 32, 31, 30, 30, 29, 30, 29, 30, 30],
            2146: [31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31],
            2147: [31, 31, 31, 32, 31, 31, 30, 29, 30, 29, 30, 30],
            2148: [31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30],
            2149: [31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 30],
            2150: [31, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31],
        };

        this.FIRST_DAY = "1918-4-13 00:00";
        this.START_NP = 1975;
        this.START_EN = 1918;
        this.END_NP = 2150;
        this.END_EN = 2092;
    }

    get = () => ({
        year: this.NEPALI_YEARS,
        month: this.NEPALI_MONTH,
        week: this.NEPALI_WEEK,
        firstDay: this.FIRST_DAY,
        startNP: this.START_NP,
        startEN: this.START_EN,
        endNP: this.END_NP,
        endEN: this.END_EN,
    });

    validateNP = (year, month, day) => {
        if (!this.NEPALI_YEARS[year]) return false;
        if (month > 12 || month < 1) return false;
        if (day > this.NEPALI_YEARS[year][month - 1] || day < 1) return false;
        return true;
    };

    validateEN = (year, month, day) => {
        if (year < this.START_EN || year > this.END_EN) return false;
        if (month > 12 || month < 1) return false;
        if (day > new Date(year, month, 0).getDate() || day < 1) return false;
        return true;
    };

    getNepaliDate = (year, month, day) => {
        if (!this.validateEN(year, month, day)) return "Invalid Date";

        const providedDate = `${year}-${month}-${day}`;
        const startDate = new Date(this.FIRST_DAY);
        startDate.setHours(0, 0, 0, 0);
        const endDate = new Date(providedDate);
        endDate.setHours(0, 0, 0, 0);
        const difference = Math.abs(endDate - startDate);
        const days = Math.ceil(difference / (1000 * 3600 * 24));

        let totalDays = 0;
        let acquiredDays;

        for (let i = this.START_NP; i < this.END_NP; i++) {
            const element = this.NEPALI_YEARS[i];
            const currentTotalDays = element.reduce((a, i) => a + i, 0);
            totalDays += currentTotalDays;

            if (totalDays === days) {
                year = i + 1;
                month = 1;
                day = 1;
            }
            if (totalDays > days) {
                year = i;
                let prevTotalDays = totalDays - currentTotalDays;
                for (let j = 0; j < 12; j++) {
                    prevTotalDays += this.NEPALI_YEARS[i][j];
                    if (prevTotalDays > days) {
                        month = j + 1;
                        const tempDays = prevTotalDays - days;
                        acquiredDays = this.NEPALI_YEARS[i][j] - tempDays + 1;
                        break;
                    } else if (prevTotalDays === days) {
                        year = i;
                        month = j + 1;
                        day = 1;
                    }
                }
                break;
            }
        }

        return {
            year,
            month: month,
            day: acquiredDays,
        };
    };

    getEnglishDate = (year, month, day) => {
        if (!this.validateNP(year, month, day)) return "Invalid Date";

        const startDate = new Date(this.FIRST_DAY);
        let dayCount = 0;
        const months = month - 1;
        let i = this.START_NP;
        for (i; i < year; i++) {
            dayCount += this.NEPALI_YEARS[i].reduce((a, item) => a + item, 0);
        }
        for (let j = 0; j < months; j++) {
            dayCount += this.NEPALI_YEARS[i][j];
        }
        dayCount += day - 1;

        const nep = new Date(startDate.setDate(startDate.getDate() + dayCount));
        return nep;
    };

    getTodayDate = () => {
        const date = new Date();
        const data = {
            AD: null,
            BS: null,
        };
        data.AD = {
            year: date.getFullYear(),
            month: date.getMonth() + 1,
            day: date.getDate(),
        };
        data.BS = this.getNepaliDate(data.AD.year, data.AD.month, data.AD.day);

        return data;
    };
    getNepaliFormat = (number) => {
        if (isNaN(number)) return number;
        const newData = number
            .toString()
            .split("")
            .map((i) => this.NEPALI_NUMBER[+i])
            .join("");

        return newData;
    };
}
const nepaliDate = new NepaliDate();
function meropatro(settings, id, rect) {
    const NMO = [
        { nepali: "बैशाख", english: "Apr-May" },
        { nepali: "जेष्ठ", english: "May-Jun" },
        { nepali: "अषाढ", english: "Jun-Jul" },
        { nepali: "श्रावण", english: "Jul-Aug" },
        { nepali: "भाद्र", english: "Aug-Sep" },
        { nepali: "असोज", english: "Sep-Oct" },
        { nepali: "कार्तिक", english: "Oct-Nov" },
        { nepali: "मंसिर", english: "Nov-Dec" },
        { nepali: "पुष", english: "Dec-Jan" },
        { nepali: "माघ", english: "Jan-Feb" },
        { nepali: "फाल्गुन", english: "Feb-Mar" },
        { nepali: "चैत्र", english: "Mar-Apr" },
    ];

    const nDA = ["आइत", "सोम", "मगल", "बुध", "बिहि", "शुक्र", "शनि"];

    const DIE = nepaliDate.NEPALI_YEARS;
    const ns = { year: 2080, month: 4, day: 14 };
    const nepaliNumerals = ["०", "१", "२", "३", "४", "५", "६", "७", "८", "९"];

    const startYear = Math.min(...Object.keys(DIE).map((year) => parseInt(year)));
    const endYear = Math.max(...Object.keys(DIE).map((year) => parseInt(year)));

    const sma = document.getElementById("sma");
    const min = $("#" + id).attr("min");
    const max = $("#" + id).attr("max");
    const readonly = $("#" + id).attr("readonly");
    const sYn = document.getElementById("sYn");

    function populateDropdowns() {
        sYn.innerHTML = "";
        sma.innerHTML = "";

        for (let year = startYear; year <= endYear; year++) {
            const optionYear = document.createElement("option");
            optionYear.value = year;
            optionYear.textContent = year.toString();
            sYn.appendChild(optionYear);
        }

        for (let i = 0; i < NMO.length; i++) {
            const oM = document.createElement("option");
            const nMn = NMO[i].nepali;
            const s = NMO[i].english;
            oM.value = i;
            oM.textContent = `${nMn} - ${s}`;
            sma.appendChild(oM);
        }
    }

    function addClickListenerToDates() {
        const nepaliDates = document.querySelectorAll(".calendar td.date-td");
        nepaliDates.forEach((date) => {
            date.addEventListener("click", () => {
                const selectedDate = date.textContent.trim();
                updSelDate(selectedDate);
                const cWRA = document.querySelector("#calendar-wrapper");
                cWRA.style.display = "none";
            });
        });
    }

    function uC() {
        const Syea = parseInt(sYn.value);
        const selectedMonth = parseInt(sma.value);
        createMonthCalendar(Syea, selectedMonth);
        addClickListenerToDates();
    }

    sYn.addEventListener("change", uC);
    sma.addEventListener("change", uC);

    function getYearToDayIndex(year) {
        const convertedToEnglish = nepaliDate.getEnglishDate(year, 1, 1);
        return convertedToEnglish.getDay();
    }

    function createMonthCalendar(year, month) {
        const table = document.querySelector(".calendar");
        table.innerHTML = "";

        let startDay = 14;
        if (month > 0) {
            for (let i = 0; i < month; i++) {
                startDay += DIE[year][i];
            }
        }

        let sI;
        sI = (getYearToDayIndex(year) + startDay - 1) % nDA.length;

        const daysRow = document.createElement("tr");
        for (let i = 0; i < nDA.length; i++) {
            const td = document.createElement("th");
            td.textContent = nDA[i];
            td.style.fontFamily = "Poppins, sans-serif";

            daysRow.appendChild(td);
        }
        table.appendChild(daysRow);

        const totalDaysInMonth = DIE[year][month];

        let weeks = Math.ceil((totalDaysInMonth + sI) / nDA.length);

        if (sI > 0 && weeks === 5) {
            weeks += 1;
        }

        let dayCounter = 1;
        let blankPage = true;
        for (let week = 0; week < weeks; week++) {
            const dateRow = document.createElement("tr");
            for (let i = 0; i < nDA.length; i++) {
                const td = document.createElement("td");
                if (blankPage) {
                    if (week === 0 && i < sI) {
                        td.className = "empty-cell";
                    } else {
                        blankPage = false;
                    }
                } else if (dayCounter <= totalDaysInMonth) {
                    td.textContent = toNepaliNumber(dayCounter);
                    td.style.fontSize = "14px";
                    td.style.fontWeight = "bold";
                    td.classList.add("date-td");

                    const smspan = document.createElement("span");

                    const englishDate = nepaliDate
                        .getEnglishDate(
                            parseInt(sYn.value),
                            parseInt(sma.value) + 1,
                            dayCounter
                        );

                    const englishDay = englishDate.getDate();

                    smspan.textContent = englishDay.toString();
                    smspan.className = "nepali-date";

                    td.appendChild(smspan);

                    if (i === 6) {
                        td.classList.add("red");
                        smspan.classList.add("red");
                    }

                    if (max !== undefined) {
                        const maxEnglishDate = new Date(max);
                        maxEnglishDate.setHours(0, 0, 0, 0);

                        if (englishDate > maxEnglishDate) {
                            td.classList.add("disabled");
                        }
                    }

                    if (min !== undefined) {
                        const minEnglishDate = new Date(min);
                        minEnglishDate.setHours(0, 0, 0, 0);

                        if (englishDate < minEnglishDate) {
                            td.classList.add("disabled");
                        }
                    }

                    settings.disabledDates.forEach((e) => {
                        if (e.endingDate === undefined) {
                            if (year > e.year) {
                                td.classList.add("disabled");
                            }
                            if (year === e.year && month > (e.month ? e.month - 1 : 0)) {
                                td.classList.add("disabled");
                            }

                            if (
                                year === e.year &&
                                month === (e.month ? e.month - 1 : 0) &&
                                dayCounter > e.startingDate &&
                                (e.endingDate === undefined || dayCounter > e.endingDate)
                            ) {
                                td.classList.add("disabled");
                            }
                        } else if (e.startingDate === undefined) {
                            if (year < e.year) {
                                td.classList.add("disabled");
                            }
                            if (year === e.year && month < (e.month ? e.month - 1 : 0)) {
                                td.classList.add("disabled");
                            }

                            if (year === e.year && month === (e.month ? e.month - 1 : 0) &&
                                dayCounter > 1 &&
                                dayCounter <= e.endingDate + 1) {
                                td.classList.add("disabled");
                            }
                        }
                        else {
                            if (
                                year === e.year &&
                                month === (e.month ? e.month - 1 : 0) &&
                                dayCounter > e.startingDate &&
                                dayCounter <= e.endingDate + 1
                            ) {
                                td.classList.add("disabled");
                            }
                        }
                    });

                    const dSel = document.getElementById(id).value;
                    datesplited = dSel.split("/");
                    const selectedDate = new Date(`${datesplited[2]}-${datesplited[1]}-${datesplited[0]}`);
                    selectedDate.setHours(0, 0, 0, 0);

                    const date = nepaliDate.getNepaliDate(
                        new Date().getFullYear(),
                        new Date().getMonth() + 1,
                        new Date().getDate()
                    );
                    if (
                        year == date.year &&
                        month == date.month - 1 &&
                        dayCounter == date.day
                    ) {
                        td.classList.add("todaydate");
                    }
                    if (selectedDate.getTime() === englishDate.getTime()) {
                        td.classList.add("selectedDate");
                    }
                    dayCounter++;
                } else {
                    td.textContent = "";
                    td.className = "empty-cell";
                }
                dateRow.appendChild(td);
            }
            table.appendChild(dateRow);
        }

        const pMA = document.getElementById("pMA");
        pMA.disabled = year === startYear && month === 0;

        const nMA = document.getElementById("nMA");
        nMA.disabled = year === 2081 && month === 11;
    }

    function prevMonth() {
        const Syea = parseInt(sYn.value);
        let selectedMonth = parseInt(sma.value);
        if (Syea === startYear && selectedMonth === 0) {
            return;
        }
        if (selectedMonth === 0) {
            selectedMonth = 11;
            sYn.value = Syea - 1;
        } else {
            selectedMonth--;
        }
        sma.value = selectedMonth;
        uC();
    }

    function nextMonth() {
        const Syea = parseInt(sYn.value);

        let selectedMonth = parseInt(sma.value);
        if (Syea === 2084 && selectedMonth === 11) {
            return;
        }
        if (selectedMonth === 11) {
            selectedMonth = 0;
            sYn.value = Syea + 1;
        } else {
            selectedMonth++;
        }
        sma.value = selectedMonth;
        uC();
    }

    function clearDate() {
        const inputField = document.getElementById(id);
        inputField.value = "";
        $(inputField).change();
        const cWRA = document.querySelector("#calendar-wrapper");
        cWRA.style.display = "none";
        uC();
    }

    function selectTodayDate() {
        const inputField = document.getElementById(id);
        inputField.valueAsDate = new Date();
        $(inputField).change();
        const cWRA = document.querySelector("#calendar-wrapper");
        cWRA.style.display = "none";
        uC();
    }

    function toNepaliNumber(number) {
        return number
            .toString()
            .replace(/\d/g, (digit) => nepaliNumerals[parseInt(digit)]);
    }

    populateDropdowns();
    uC();

    function showCalendar() {
        const cWRA = document.getElementById("calendar-wrapper");
        cWRA.style.display = "block";
        cWRA.style.position = "fixed";

        const inputField = document.getElementById(id);
        const inputRect = inputField.getBoundingClientRect();

        const pMA = document.getElementById("pMA");
        const nMA = document.getElementById("nMA");

        const clearBtn = document.getElementById("nepaliCalendarClearBtn");
        const todayBtn = document.getElementById("nepaliCalendarTodayBtn");

        clearBtn.addEventListener("click", clearDate);
        todayBtn.addEventListener("click", selectTodayDate);

        pMA.addEventListener("click", prevMonth);
        nMA.addEventListener("click", nextMonth);

        const selDate = inputField.value === '' ? new Date() : new Date(inputField.value);

        selDate.setHours(0, 0, 0, 0);

        const selDateNepali = nepaliDate.getNepaliDate(
            selDate.getFullYear(),
            selDate.getMonth() + 1,
            selDate.getDate()
        );

        // Calculate the position of the datepicker relative to the input field
        const datePickerHeight = cWRA.offsetHeight /* calculate the height of your datepicker */;
        const datePickerWidth = cWRA.offsetWidth /* calculate the height of your datepicker */;

        // Adjust for a zoom level of 0.9 (zoomed out)
        const zoomFactor = settings.zoomFactor;

        const unzoomedLeft = inputRect.left * zoomFactor;
        const unzoomedRight = inputRect.right * zoomFactor;
        const unzoomedBottom = (inputRect.bottom + 10) * zoomFactor;
        const unzoomedTop = inputRect.top * zoomFactor;
        const unzoomedDatePickerTop = unzoomedTop - datePickerHeight;
        const unzoomedDatePickerHeight = datePickerHeight * zoomFactor;
        const unzoomedDatePickerWidth = datePickerWidth * zoomFactor;

        // Check if there is enough space left of the input field
        const spaceRight = window.innerWidth - unzoomedRight;
        const spaceLeft = unzoomedLeft;

        if (spaceRight < unzoomedDatePickerWidth) {
            if (spaceLeft < unzoomedDatePickerWidth) {
                cWRA.style.right = `${10}px`;
            } else {
                cWRA.style.right = `${spaceRight * zoomFactor}px`;
            }
        } else {
            cWRA.style.left = `${unzoomedLeft}px`;
        }

        // Check if there is enough space below the input field
        const spaceBelow = window.innerHeight - unzoomedBottom;
        const spaceTop = unzoomedTop;

        if (spaceBelow < unzoomedDatePickerHeight) {
            // Not enough space below, display datepicker above
            // Set the top position of the datepicker

            const TopBottom = window.innerHeight - unzoomedDatePickerHeight;
            if (spaceTop < unzoomedDatePickerHeight) {
                cWRA.style.top = `${TopBottom / 2}px`;
            } else {
                cWRA.style.top = `${unzoomedDatePickerTop + 5}px`;
            }
        } else {
            // Enough space below, display datepicker below
            // Set the top position of the datepicker
            cWRA.style.top = `${unzoomedBottom}px`;
        }

        cWRA.style.zIndex = 1050;

        sma.value = selDateNepali.month - 1;
        sYn.value = selDateNepali.year;

        uC();
    }

    if (readonly !== 'readonly') {
        showCalendar();
    }

    function hideCalendar(event) {
        const cWRA = document.querySelector("#calendar-wrapper");
        if (
            event.target !== document.getElementById(event.target.id) &&
            !cWRA.contains(event.target)
        ) {
            cWRA.style.display = "none";
        }
    }

    document.addEventListener("click", hideCalendar);

    function updSelDate(selectedDate) {
        const inputField = document.getElementById(id);

        const Syea = parseInt(sYn.value);
        let selectedMonth = parseInt(sma.value) + 1;

        const nepaliNumeralsRegex = new RegExp(`[${nepaliNumerals.join("")}]`, "g");
        const formattedDateNepali = selectedDate
            .match(nepaliNumeralsRegex)
            .join("");

        const formattedDateEnglish = toEnglishNumber(formattedDateNepali);

        selectedMonth = selectedMonth.toString().padStart(2, "0");

        const convertedToEnglish = nepaliDate.getEnglishDate(Syea, selectedMonth, formattedDateEnglish);

        const month = (convertedToEnglish.getMonth() + 1).toString().padStart(2, '0');
        const day = convertedToEnglish.getDate().toString().padStart(2, '0');
        const year = convertedToEnglish.getFullYear();

        inputField.valueAsDate = new Date(Date.UTC(year, month - 1, day));

        $(inputField).change();
    }

    function toEnglishNumber(nepaliNumber) {
        const nepaliNumerals = ["०", "१", "२", "३", "४", "५", "६", "७", "८", "९"];
        return nepaliNumber.replace(/[०-९]/g, (digit) =>
            nepaliNumerals.indexOf(digit)
        );
    }
}

//let isEventListenersAdded = false;

(function ($) {
    $.fn.nepaliDatePicker = function (options) {
        // This is the easiest way to have default options.
        var settings = $.extend({
            // These are the defaults.
            disabledDates: [],
            zoomFactor: 1.0,
        }, options);

        this.each(function () {
            var inputElement = this;

            inputElement.addEventListener("click", function (e) {
                e.preventDefault();
                const rect = document.getElementById(e.target.id).getBoundingClientRect();

                const cWRAs = document.getElementById("calendar-wrapper");

                // Check if the calendar already exists; if so, remove it
                if (cWRAs) {
                    cWRAs.parentNode.removeChild(cWRAs);
                }

                if (document.getElementById("calendar-wrapper") == null) {
                    const cWRA = document.createElement("div");
                    cWRA.id = "calendar-wrapper";
                    document.body.appendChild(cWRA);

                    var calendarNavigation = document.createElement("div");
                    calendarNavigation.className = "calendar-navigation";

                    var pMA = document.createElement("span");
                    pMA.className = "arrow";
                    pMA.id = "pMA";
                    pMA.innerHTML = '<i class="fa fa-chevron-left"></i>';

                    var sma = document.createElement("select");
                    sma.id = "sma";
                    sma.className = "month-dropdown";

                    var sYn = document.createElement("select");
                    sYn.id = "sYn";
                    sYn.className = "year-dropdown";

                    var nMA = document.createElement("span");
                    nMA.className = "arrow";
                    nMA.id = "nMA";
                    nMA.innerHTML = '<i class="fa fa-chevron-right"></i>';

                    var calendarTable = document.createElement("table");
                    calendarTable.className = "calendar noDataTable";

                    var tableRow = document.createElement("tr");
                    var tableHeader = document.createElement("th");
                    tableHeader.colSpan = "7";

                    var calendarFunction = document.createElement("div");
                    calendarFunction.className = "calendar-function";

                    var clearBtn = document.createElement("span");
                    clearBtn.className = "clearBtn";
                    clearBtn.id = "nepaliCalendarClearBtn";
                    clearBtn.innerHTML = 'Clear';

                    var todayBtn = document.createElement("span");
                    todayBtn.className = "todayBtn";
                    todayBtn.id = "nepaliCalendarTodayBtn";
                    todayBtn.innerHTML = 'Today';

                    calendarNavigation.appendChild(pMA);
                    calendarNavigation.appendChild(sma);
                    calendarNavigation.appendChild(sYn);
                    calendarNavigation.appendChild(nMA);

                    calendarFunction.appendChild(clearBtn);
                    calendarFunction.appendChild(todayBtn);

                    tableRow.appendChild(tableHeader);
                    calendarTable.appendChild(tableRow);

                    cWRA.appendChild(calendarNavigation);
                    cWRA.appendChild(calendarTable);
                    cWRA.appendChild(calendarFunction);
                }
                meropatro(settings, e.target.id, rect);
            });
        });
    };
}(jQuery));

window.addEventListener("scroll", () => {
    const cWRA = document.querySelector("#calendar-wrapper");
    if (cWRA) { cWRA.style.display = "none"; }
})