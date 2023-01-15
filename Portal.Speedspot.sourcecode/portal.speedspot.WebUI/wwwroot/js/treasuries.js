
deleteConfirmationTreasuries = function () {
    $(".treasuries-confirm-delete").confirmation({
        rootSelector: '[data-toggle=confirmation]',
        popout: true,
        singleton: true,
        title: ARCHIVE,
        btnOkLabel: YES,
        btnCancelLabel: NO,
        onConfirm: function () {
            switch ($(this).data('source')) {
                case "archiveTreasuries":
                    ArchiveTreasury($(this).data('itemid'));
                    break;
                case "UnarchiveTreasuries":
                    UnArchiveTreasury($(this).data('itemid'));
                    break;

            }
        }
    })
}

typeChanged = function () {
    let typeValue = $("input[name='Type']:checked").val()

    if (typeValue == '0') {
        $(".type-bank").removeClass("d-none");
        $(".type-cash").addClass("d-none");
    } else {
        $(".type-bank").addClass("d-none");
        $(".type-cash").removeClass("d-none");
    }
}

getTreasuries = function () {
    let departmentId = $("#incomeDepartmentId").val();

    showLoader()
    $.ajax({
        url: `/Treasuries/GetTreasuries/?departmentId=${departmentId}`,
        success: function (result) {
            $(`#trasuries`).html(result);
            deleteConfirmationTreasuries();
        },
        complete: function () {
            hideLoader();
        }
    })
}

ArchiveTreasury = function (id) {
    showLoader()
    $.get(`/Treasuries/Archive/${id}`, function (data) {
        if (data) {
            getTreasuries();
        }
    })
}

UnArchiveTreasury = function (id) {
    showLoader()
    $.get(`/Treasuries/UnArchive/${id}`, function (data) {
        if (data) {
            getTreasuries();
        }
    })
}

saveForm = function () {
    let isValid = $("#treasuryForm").valid();

    if (isValid) {
        $("#treasuryForm").submit();
    }
}

editForm = function () {
    let isValid = $("#treasuryFormEdit").valid();
    if (isValid) {
        $("#treasuryFormEdit").submit();
    }
}