function UseSwal(icon , text) {
    const Toast = Swal.mixin({
        toast: true,
        position: "top-start",
        showConfirmButton: false,
        timer: 3000,
        timerProgressBar: true,
        didOpen: (toast) => {
            toast.onmouseenter = Swal.stopTimer;
            toast.onmouseleave = Swal.resumeTimer;
        }
    });
    Toast.fire({
        icon: icon,
        title: text
    });
}


async function PostForm(url, formData) {
try {
    const response = await fetch(url, {
        method: 'POST',
        body: formData
    });
    const result = await response.json();

    if (response.ok) {
        UseSwal("success", "عملیات با موفقیت انجام شد");

    } else {
        UseSwal("error", result.details);

    }

} catch (error) {
    console.error('Fetch error:', error);
    }
}




async function PostJson(url, data , callback) {
    try {
        const response = await fetch(url, {
            method: 'POST',
            headers: {
                'Content-Type':'application/json'
            },
            body : JSON.stringify(data)
        });
        const result = await response.json();

        if (response.ok) {
            UseSwal("success", "عملیات با موفقیت انجام شد");

        } else {
            UseSwal("error", result.details);

        }

        if (callback) {
            callback();
        }

        return result;


    } catch (error) {
        console.error('Fetch error:', error);
    }
}



async function DeleteWithSwal(callback)
{
    Swal.fire({
        title: "حذف شه؟"
                icon: "warning"
                showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        cancelButtonText: "نه",
        confirmButtonText: "آره"
    }).then((result) => {
        if (result.isConfirmed) {

            if (callback) {
                callback();
            }

           
        }
    });
}
