$(document).ready(function () {
    
    showResults();
    cleanInputs();
    validateInputs();
    validateQuantiyInputs();
    
});

function validateInputs() {
    var totalClientCoins = 0;
    var totalClientProducts = 0;
    $('#vendingMachineForm input:not([type=hidden])').each(
        function (index) {
            var input = $(this);
            if (input.attr('name').includes("ClientCoins")) {
                totalClientCoins += parseInt(input.val());
                
            }
            else if (input.attr('name').includes("ClientProducts")) {
                totalClientProducts += parseInt(input.val());
              
            }
        }
    );

    if (totalClientCoins <= 0 || totalClientProducts <= 0) {
        $('#submitBtn').attr("disabled", true);
        $('#instructionsLabel').show();
    }
    else {
        $('#submitBtn').attr("disabled", false);
        $('#instructionsLabel').hide();
    }
    
}


function validateQuantiyInputs() {

    $("#vendingMachineForm").find('input[type="hidden"]').each(
        function (index) {
            var input = $(this);
            var elementQuantName = input.attr('name');
            if (elementQuantName.includes("Quantity") && input.val() == 0) {
                var ElementProdName = elementQuantName.replace("Quantity", "Name");
                var InvProdName = $("[name='" + ElementProdName + "']").val();
                var ClientProdNameId = $('#vendingMachineForm input[type="hidden"][value="' + InvProdName + '"][name^="ClientProducts"]').attr("name")
                var ClientProdQuantName = ClientProdNameId.replace("Name", "Quantity");
                $('#vendingMachineForm input[name="' + ClientProdQuantName+'"]').attr("disabled", true);
            }
        }
    );
}

function cleanInputs() {
    $('#vendingMachineForm input:not([type=hidden])').each(
        function (index) {
            var input = $(this);
            if (input.attr('type') == "number") {
                input.val(0);
            }
            
         
        }
    );
}


function showResults() {
    var resultMessage = $("#ResultMessage").val();
    if (!(!resultMessage || resultMessage.length === 0)) {
        $("#paragraphBody").empty();
        $("#paragraphBody").text(resultMessage);
        $('#messageModal').modal('show');
    }
    

}



function updateTotalAmount(data) {
    var total = 0;
    var labelType = "cents";
    // note this code is  dependent but it's what I have time to do :(
   for (var i = 0; i < 3 ; i++) {
        var idPrice = "#ClientProducts_" + i + "__Price";
       var idQuantity = "#ClientProducts_" + i + "__Quantity";
       var quantity = $(idQuantity).val() > 0 ? $(idQuantity).val() : 0; // if we add negatives then we won´t add to the total
       total += ($(idPrice).val() * quantity);
    }

    if (total >= 100) {
            total = parseFloat(total) / 100;
            labelType = "dollars"
    }
   


    $("#totalAmount").empty();
    $("#totalAmount").append(total + " " + labelType);
   }

    
