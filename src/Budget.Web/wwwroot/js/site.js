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
