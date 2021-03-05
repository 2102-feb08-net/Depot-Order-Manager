'use strict';

function showSearchResultTitle(enabled, searchQuery) {
    let value = document.getElementById("searchTitle_Value");

    if (value !== null)
        value.innerHTML = searchQuery;

    document.getElementById("mainTitle").hidden = enabled;
    document.getElementById("searchTitle").hidden = !enabled;
}