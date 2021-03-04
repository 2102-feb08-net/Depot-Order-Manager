'use strict';

// Simple function found from StackOverflow to truncate the decimal places.
function truncateToDecimals(num, dec = 2) {
    const calcDec = Math.pow(10, dec);
    return Math.trunc(num * calcDec) / calcDec;
}