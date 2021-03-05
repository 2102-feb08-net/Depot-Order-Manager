'use strict';

// Regex function to convert the number value to a string of a specified number of digits.
function truncateToDecimals(num, dec = 2) {
    var re = new RegExp('^-?\\d+(?:\.\\d{0,' + (dec || -1) + '})?');
    return num.toString().match(re)[0];
}