// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {

    set_papers()
    populate_papers()
        $(document).on('change', 'input[name="files"]', function () {
        var file = $(this).prop('files')[0];
        var allowedExtensions = /(\.pdf)$/i;

        if (!allowedExtensions.exec(file.name)) {
            $(this).val('');
            alert('Please select a PDF file.')
        } else {
        }
    });
});


$(document).on('click', '.btn-add', function (e) {
    $(inputs()).insertBefore('#notes-area')
})

$(document).on('click', '.btn-remove', function (e) {
    $(this).parents()[1].remove()
})

$(document).on('submit', '.ccsform', function (e) {
    e.preventDefault();
    const self = $(this);
    const data = new FormData(self[0]);
    const type_of_paper = $('select[name="type_of_paper[]"]');

    data.append("id", $(this).data('id'));
    data.append("status", $(this).val());

    var fileInputs = document.getElementsByClassName('pdf-file-input');
    var totalFiles = fileInputs.length;
    var processedFiles = 0;

    // Function to handle processing of each file input
    function processFile(file) {
        var fileReader = new FileReader();

        fileReader.onload = function () {
            var typedArray = new Uint8Array(this.result);

            pdfjsLib.getDocument(typedArray).promise.then(function (pdf) {
                var pageCount = pdf.numPages;
                console.log("Page Count:", pageCount);
                data.append("paper_count[]", pageCount);

                processedFiles++;

                // Check if all files have been processed
                if (processedFiles === totalFiles) {
                    // All files have been processed, proceed with AJAX request
                    ajax(data, 'home/AddPrints').then(res => {
                        $.each(type_of_paper, function (key, res) {
                            console.log('asd');
                            set_paper_count(res.value);
                        });
                        self[0].reset();
                        setTimeout(() => {
                            alert(res.message);
                        }, 1500);
                    });
                }
            });
        };

        fileReader.readAsArrayBuffer(file);
    }

    // Iterate over each file input
    for (var i = 0; i < totalFiles; i++) {
        var file = fileInputs[i].files[0];
        processFile(file);
    }
});


$(document).on('click', '.mark-as-done', function (e) {
    const data = new FormData()
    data.append("id", $(this).data('id'))
    data.append("status", 1)

    ajax(data, 'admin/UpdateRow').then(res => {
        alert(res.message)
        setTimeout(() => {
            location.reload()
        }, 1500)
    })

    console.log(data)
})

$(document).on('change', '.chnage-status', function () {
    const data = new FormData()

    data.append("id", $(this).data('id'))
    data.append("status", $(this).val())

    ajax(data, 'admin/UpdateRow').then(res => {
        alert(res.message)
        setTimeout(() => {
            location.reload()
        }, 1500)
    })

    console.log(data)

})

const populate_papers = () => {
    $('.sp-short').html(get_paper_count('sp-short'))
    $('.sp-long').html(get_paper_count('sp-long'))
    $('.np-short').html(get_paper_count('np-short'))
    $('.np-long').html(get_paper_count('np-long'))
    $('.ws-short').html(get_paper_count('ws-short'))
    $('.ws-long').html(get_paper_count('ws-long'))
}

const get_paper_count = (key = '') => {
    return localStorage.getItem(key)
}

const set_paper_count = (value) => {
    var paper 
    switch (value) {
        case "Scratch Paper (short)":
            paper = get_paper_count("sp-short")
            localStorage.setItem("sp-short", parseInt(paper)+1)
            break;
        case "Scratch Paper (long)":
            paper = get_paper_count("sp-long")
            localStorage.setItem("sp-long", parseInt(paper) +1)
            break;
        case "News Print (long)":
            paper = get_paper_count("np-long")
            localStorage.setItem("np-long", parseInt(paper) +1)
            break;
        case "News Print (short)":
            paper = get_paper_count("np-short")
            localStorage.setItem("np-short", parseInt(paper) +1)
            break;
        case "BONDPAPER WHITE-SUBSTANCE 16 (LONG)":
            paper = get_paper_count("ws-long")
            localStorage.setItem("ws-long", parseInt(paper) +1)
            break;
        case "BONDPAPER WHITE-SUBSTANCE 16 (SHORT)":
            paper = get_paper_count("ws-short")
            localStorage.setItem("ws-short", parseInt(paper) +1)
            break;
    }
}

const set_papers = () => {
    if (localStorage.getItem('sp-short') === null) {
        localStorage.setItem('sp-short', 0)
        localStorage.setItem('sp-long', 0)
        localStorage.setItem('np-short', 0)
        localStorage.setItem('np-long', 0)
        localStorage.setItem('ws-short', 0)
        localStorage.setItem('ws-long', 0)
    }
}


const ajax = (data, url) => {
    return $.ajax({
        url: url,
        type: "post",
        dataType: "json",
        data: data,
        async: true,
        cache: false,
        enctype: "multipart/form-data",
        processData: false,
        contentType: false,
        success: function (res) {
            return res;
        },
        error: function (err) {
            return err;
        },
    });
}



const inputs = () => {
    return `
                    <div class="mb-3 row">
                    <div class="col-9">
                        <label for="files" class="form-label">file Name</label>
                        <input type="file" class="form-control pdf-file-input" id="files" name="files" accept="application/pdf" multiple />
                    </div>
                    <div class="col-3 btns">
                        <button class="btn btn-success btn-md btn-add" type="button">Add</button>
                        <button class="btn btn-danger btn-md btn-remove" type="button">Remove</button>
                    </div>

                    <div class="col-4">
                        <label for="no_of_copies" class="form-label">No. of Copies</label>
                        <input type="number" class="form-control" id="no_of_copies" name="no_of_copies[]" />
                    </div>
                    <div class="col-4">
                        <label for="no_of_copies" class="form-label">Type of Paper</label>
                        <select class="form-select" name="type_of_paper[]">
                                <option selected>Type of Paper</option>
                                <option value="Scratch Paper (short)">Scratch Paper (short)</option>
                                <option value="Scratch Paper (long)">Scratch Paper (long)</option>
                                <option value="News Print (long)">News Print (long)</option>
                                <option value="News Print (short)">News Print (short)</option>
                                <option value="BONDPAPER WHITE-SUBSTANCE 16 (LONG)">BONDPAPER WHITE-SUBSTANCE 16 (LONG)</option>
                                <option value="BONDPAPER WHITE-SUBSTANCE 16 (SHORT)">BONDPAPER WHITE-SUBSTANCE 16 (SHORT)</option>
                        </select>
                    </div>
                    <div class="col-4">
                        <label for="no_of_copies" class="form-label">Printer Name</label>
                        <select class="form-select" name="printer_name[]">
                            <option selected>Printer Name</option>
                                <option value="Standard">Standard</option>
                                <option value="Basic">Basic</option>
                        </select>
                    </div>
                </div>
    `
}