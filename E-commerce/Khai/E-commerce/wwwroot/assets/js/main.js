let deleteButtons = document.querySelectorAll(".delete-image-btn");


deleteButtons.forEach(btn => btn.addEventListener("click", function () {
    btn.parentElement.remove()
}))


let ItemDeleteBtns = document.querySelectorAll(".item-delete");

ItemDeleteBtns.forEach(btn => btn.addEventListener("click", function (e) {
    e.preventDefault();

    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCacelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            let url = btn.getAttribute("href")

            fetch(url)
                .then(response => {
                    if (response.status == 200) {
                        window.location.reload(true);
                    } else {
                        alert("Not Found")
                    }
                })
        }
    })
}))




let addToBasketBtns = document.querySelectorAll(".add-to-dasket-btn");

addToBasketBtns.forEach(btn => btn.addEventListener("click", function (e) {
    e.preventDefault();
    let url = btn.getAttribute("href");

    fetch(url)
        .then(response => {
            if (response.status == 200) {
                alert("Added to basket")
            } else {
                alert("Error")
            }
        })
}))