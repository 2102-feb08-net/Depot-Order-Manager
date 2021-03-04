'use strict';

const table = document.getElementById("customerTableBody");

let customerForm = document.getElementById('customerForm');
customerForm.onsubmit = createCustomer;

async function loadCustomers() {
    const response = await fetch("/api/customers/getall");

    document.getElementById("loadingTable").hidden = true;

    if (!response.ok) {
        document.getElementById("failedToLoadTable").hidden = false;
        throw new Error(`Unable to download customers from server: (${response.status}) ${response.statusText}`);
    }

    const customers = await response.json();

    for (const customer of customers) {
        addCustomerRow(customer.id, customer.firstName, customer.lastName);
    }
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
}

loadCustomers();