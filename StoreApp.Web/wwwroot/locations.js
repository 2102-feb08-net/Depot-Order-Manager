'use strict';

const table = document.getElementById("tableBody");

function buildTable(table, locations) {

    for (let i = table.rows.length - 1; i >= 0; i--)
        table.deleteRow(0);

    for (const location of locations) {
        addLocationRow(location.id, location.name, location.addressLines);
    }
}

async function loadTable(tableName) {
    const response = await fetch(`/api/${tableName}/getall`);

    document.getElementById("loadingTable").hidden = true;

    if (!response.ok) {
        document.getElementById("failedToLoadTable").hidden = false;
        throw new Error(`Unable to download ${tableName} from server: (${response.status}) ${response.statusText}`);
    }

    const rows = await response.json();

    buildTable(table, rows);
    showSearchResultTitle(false, "");
}

function addLocationRow(id, name, address) {
    let row = table.insertRow();
    let idCell = row.insertCell();
    let firstNameCell = row.insertCell();
    let lastNameCell = row.insertCell();

    idCell.innerHTML = id;
    firstNameCell.innerHTML = name;

    let singleLineAddress = "";
    for (let i = 0; i < address.length; i++) {
        singleLineAddress += address[i];

        if (i != address.length - 1)
            singleLineAddress += " ";
    }
    lastNameCell.innerHTML = singleLineAddress;

    row.addEventListener('click', () => showLocationOrders(id));
}

async function searchTable() {
    const query = document.getElementById("searchQuery_Input").value;

    if (!query) {
        loadTable();
        return;
    }

    const response = await fetch(`/api/locations/search?query=${query}`);

    if (!response.ok) {
        throw new Error("Unable to search for locations.");
    }

    const locations = await response.json();

    buildTable(table, locations);

    showSearchResultTitle(true, query);
}

function showSearchResultTitle(enabled, searchQuery) {
    document.getElementById("searchTitle_Value").innerHTML = searchQuery;
    document.getElementById("mainTitle").hidden = enabled;
    document.getElementById("searchTitle").hidden = !enabled;
}

function showLocationOrders(id) {
    window.location = "orders.html?location=" + id;
}

let customerSearch = document.getElementById("searchButton");
customerSearch.onclick = searchTable;

loadTable("locations");