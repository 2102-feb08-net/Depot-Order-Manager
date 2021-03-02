'use strict';

const table = document.getElementById("customerTableBody");

let customerForm = document.getElementById('customerForm');
customerForm.onsubmit = () => {
    fetch("/api/customers/add")
        .then(
            customers => {
                const firstName = document.getElementById("firstName_Input").value;
                const lastName = document.getElementById("lastName_Input").value;

                addCustomerRow('?', firstName, lastName);
            },
            Error => {
                alert("Failed to add customer: " + customer.firstName + " " + customer.lastName);
            }
        );
};

async function loadCustomers() {
    const response = await fetch("/api/customers/getall");
    const customers = await response.json();

    for (const customer of customers) {
        addCustomerRow(customer.id, customer.firstName, customer.lastName);
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

class Customer {
    constructor(firstName, lastName) {
        this.firstName = firstName;
        this.lastName = lastName;
    }
}

loadCustomers();