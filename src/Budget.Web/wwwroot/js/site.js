$('#transactionModal').on('show.bs.modal', function (event) {
    console.log("Modal triggered");

    var button = $(event.relatedTarget); // Button that triggered the modal
    var url = button.data('url'); // Extract info from data-url attribute
    var modal = $(this);

    $.get(url, function (data) {
        modal.find('.modal-body').html(data);
    });
});

$('#transactionModal').on('submit', 'form', function (e) {
    e.preventDefault(); // Prevent default form submission

    var form = $(this);
    var url = form.attr('action');
    var method = form.attr('method');

    $.ajax({
        url: url,
        type: method,
        data: form.serialize(),
        success: function (response) {
            // Check if validation errors exist by inspecting the returned response
            if ($(response).find('.validation-summary-errors').length > 0 ||
                $(response).find('.field-validation-error').length > 0) {
                // If there are validation errors, replace the modal content with the returned form
                $('#transactionModal .modal-body').html($(response)); // Inject the HTML into the modal
            } else {
                // If the form submission was successful (no validation errors)
                $('#transactionModal').modal('hide');
                location.reload(); // Refresh the page to update the transaction list
            }
        },
        error: function () {
            alert("An error occurred. Please try again.");
        }
    });
});

function deleteCategory(id) {
    fetch(`Categories/Details/${id}`)
        .then(response => {
            if (!response.ok) {
                throw new Error('Invalid response from server when getting Category');
            }
            return response.json();
        })
        .then(data => {
            console.log(data);

            const categoryId = document.querySelector('#delete-category-modal #Category_Id');
            if (categoryId) {
                categoryId.value = data.id.trim();
            } else {
                console.error('Required element #Category_Id not found');
            }

            const categoryName = document.querySelector('#delete-category-modal #Category_Name');
            if (categoryName) {
                categoryName.value = data.name.trim();
            } else {
                console.error('Required element #Category_Name not found');
            }

            new bootstrap.Modal(document.getElementById('delete-category-modal')).show();
        })
        .catch(error => {
            console.error('Error:', error);
        });
}

function editCategory(id) {
    fetch(`Categories/Details/${id}`)
        .then(response => {
            if (!response.ok) {
                throw new Error('Invalid response from server when getting Category');
            }
            return response.json();
        })
        .then(data => {
            console.log(data);

            const cid = document.querySelector('#edit-category-modal #CategoryId');
            if (cid) {
                cid.value = data.id.trim();
            } else {
                console.error('Required element #CategoryId not found');
            }

            const categoryId = document.querySelector('#edit-category-modal #Category_Id');
            if (categoryId) {
                categoryId.value = data.id.trim();
            } else {
                console.error('Required element #Category_Id not found');
            }

            const categoryName = document.querySelector('#edit-category-modal #Category_Name');
            if (categoryName) {
                categoryName.value = data.name.trim();
            } else {
                console.error('Required element #Category_Name not found');
            }

            new bootstrap.Modal(document.getElementById('edit-category-modal')).show();
        })
        .catch(error => {
            console.error('Error:', error);
        });
}
