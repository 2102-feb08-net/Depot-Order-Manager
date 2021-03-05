'use strict';

const table = document.getElementById("customerTableBody");

function buildCustomerTable(customers) {
    for (let i = table.rows.length - 1; i >= 0; i--)
        table.deleteRow(0);

    for (const customer of customers) {
        addCustomerRow(customer.id, customer.firstName, customer.lastName);
    }
}

async function loadCustomers() {
    const response = await fetch("/api/customers/getall");

    document.getElementById("loadingTable").hidden = true;

    if (!response.ok) {
        document.getElementById("failedToLoadTable").hidden = false;
        throw new Error(`Unable to download customers from server: (${response.status}) ${response.statusText}`);
    }

    const customers = await response.json();

    buildCustomerTable(customers);
    showSearchResultTitle(false, "");
}

async function createCustomer() {
    const customer = {
        firstName: document.getElementById("firstName_Input").value,
        lastName: document.getElementById("lastName_Input").value,
    }

    const options = {
        method: "POST",
        body: JSON.stringify(customer),
        headers: {
            'Content-Type': 'application/json'
        }
    }

    let response = await fetch("/api/customers/add", options);

    if (!response.ok) {
        alert("Failed to add customer: " + customer.firstName + " " + customer.lastName);
    }
}

function addCustomerRow(id, firstName, lastName) {
    let row = table.insertRow();
    let idCell = row.insertCell();
    let firstNameCell = row.insertCell();
    let lastNameCell = row.insertCell();

    idCell.innerHTML = id;
    firstNameCell.innerHTML = firstName;
    lastNameCell.innerHTML = lastName;

    row.setAttribute("data-customer", id);
    row.addEventListener('click', () => showCustomerOrders(id));
}

async function searchCustomers() {
    const query = document.getElementById("searchQuery_Input").value;

    if (!query) {
        loadCustomers();
        return;
    }

    const response = await fetch(`/api/customers/search?query=${query}`);

    if (!response.ok) {
        throw new Error("Unable to search for customers");
    }

    const customers = await response.json();

    buildCustomerTable(customers);

    showSearchResultTitle(true, query);
}

function showSearchResultTitle(enabled, searchQuery) {
    document.getElementById("searchTitle_Value").innerHTML = searchQuery;
    document.getElementById("mainTitle").hidden = enabled;
    document.getElementById("searchTitle").hidden = !enabled;
}

function showCustomerOrders(id) {
    window.location = "orders.html?customer=" + id;
}

let customerForm = document.getElementById('customerForm');
customerForm.onsubmit = createCustomer;

let customerSearch = document.getElementById("searchCustomerButton");
customerSearch.onclick = searchCustomers;

loadCustomers();