let customerForm = document.getElementById('customerForm');
customerForm.onsubmit = () => {

    let customer = new Customer(document.getElementById('firstName_Input').value, document.getElementById('lastName_Input').value);

    var xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function() {
        if (this.readyState == 4 && this.status == 200) 
        {
            document.getElementById("customerTable").appendChild();
        
        }
        else
        {
            alert("Failed to add customer: " + customer.firstName + " " + customer.lastName);
        }
    };

    xhttp.open("POST", "ajax_info.txt", true);
    xhttp.send();
};

class Customer {
    constructor(firstName, lastName) {
       this.firstName = firstName;
       this.lastName = lastName; 
    }
}