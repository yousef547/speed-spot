
var stateLoadParams = function (settings, data) {
  var searchData = data.search.search;
  $("#searchbox").val(searchData);
}

openTablePayment = function (element, tableId) {
  $(`#${tableId}`).removeClass('d-none').siblings().addClass('d-none');
  $(element).addClass('active').parent().siblings().children("div").removeClass("active");
}

showActionBtn = function (btnsId, tableId) {
  $(`#${btnsId}`).addClass('d-none');
  $(`#${btnsId} .add-payment-btn`).removeClass('d-none');
  $(`#${btnsId} .edit-payment-btn`).removeClass('d-none');
  $(`#${btnsId} .approve-payment-btn`).removeClass('d-none');
  $(`#${btnsId} .reject-payment-btn`).removeClass('d-none');

  var inputsCheckBox = $(`#${tableId} input:checked`);
  if (inputsCheckBox.length >= 1) {
    $(`#${btnsId}`).removeClass('d-none');
  }

  if (inputsCheckBox.length == 1) {
    var selectedCB = $(`#${tableId} input:checked`)[0];

    let hasPayment = $(selectedCB).data('has-payment');
    if (hasPayment == 'True') {
      $(`#${btnsId} .add-payment-btn`).addClass('d-none');
    } else {
      $(`#${btnsId} .edit-payment-btn`).addClass('d-none');
    }
  }

  if (inputsCheckBox.length >= 2) {
    $(`#${btnsId} .add-payment-btn`).addClass('d-none');
    $(`#${btnsId} .edit-payment-btn`).addClass('d-none');
    $(`#${btnsId} .approve-payment-btn`).addClass('d-none');
    $(`#${btnsId} .reject-payment-btn`).addClass('d-none');
  }
}

getProjectCode = function () {
  let projectId = $("#ProjectId").val();
  if (projectId) {
    let code = $("#ProjectId option:selected").data("code");
    $("#projectCode").val(code);
  } else {
    $("#projectCode").val("");
  }
}

getProjectId = function () {
  let code = $("#projectCode").val();

  if (code) {
    showLoader();
    $.ajax({
      url: `/Payments/GetProjectByCode?code=${code}`,
      success: function (projectId) {
        $("#ProjectId").val(projectId);
      },
      complete: function () {
        hideLoader();
      }
    })
  } else {
    $("#ProjectId").val("");
  }
}

changeSpanAmount = function () {
  let amount = $("#TotalAmount").val();
  if (amount) {
    $(".AmountSpan").text(`/ ${amount}`);
  } else {
    $(".AmountSpan").text(`/`);
  }
}

checkIsProject = function () {
  let isProject = $("[name='IsProject']:checked").val();
  if (isProject == "true") {
    $(".project-on").removeClass('d-none');
    $(".direction-on").addClass('d-none');
  } else {
    $(".direction-on").removeClass('d-none');
    $(".project-on").addClass('d-none');
  }
}

addDirection = function () {
  let model = $("#directionVMs input, #directionVMs select").serialize();

  showLoader();

  $.ajax({
    url: '/Payments/AddDirection',
    data: model,
    method: "POST",
    success: function (result) {
      $("#directionVMs").html(result);
      changeSpanAmount();
    },
    complete: function () {
      hideLoader();
    }
  })
}

saveForm = function () {
  let isValid = $("#paymentRequestForm").valid();

  if (isValid) {
    let isValidAmount = checkValidAmount();
    if (isValidAmount) {
      showLoader();

      $("#paymentRequestForm").submit();
    }
  }
}

checkValidAmount = function () {
  $("#TotalAmount").removeClass("border-danger");
  $(".child-amount").removeClass("border-danger");

  let amount = parseFloat($("#TotalAmount").val());

  let isProject = $("[name='IsProject']:checked").val();
  if (isProject == "true") {
    let childsAmount = 0;
    $(".child-amount").each(function () {
      childsAmount += parseFloat($(this).val());
    })

    if (amount != childsAmount) {
      $("#TotalAmount").addClass("border-danger");
      $(".child-amount").addClass("border-danger");
      toastr.error(INVALID_AMOUNTS);
      return false;
    }
    return true;
  }
  return true;
}

deleteDirection = function (index) {
  let model = $("#directionVMs input, #directionVMs select").serialize();

  showLoader();

  $.ajax({
    url: '/Payments/DeleteDirection?index=' + index,
    data: model,
    method: "POST",
    success: function (result) {
      $("#directionVMs").html(result);
      changeSpanAmount();
    },
    complete: function () {
      hideLoader();
    }
  })
}

checkTreasury = function (checkType = true) {
  var treasuryId = $("#departmentTreasuryId").val();

  if (treasuryId) {
    $("#paymentDetailsRow").removeClass('d-none');
    $.get(`/Payments/GetTreasuryInfo/${treasuryId}`, function (data) {
      if (checkType) {
        if (data.Type == 0) {
          $('#cashPaymentType').attr("disabled", true);
          $('#transferPaymentType').attr("disabled", false);
          $('#chequePaymentType').attr("disabled", false);

          $('#transferPaymentType').prop('checked', true).trigger('change');
        } else {
          $('#cashPaymentType').attr("disabled", false);
          $('#transferPaymentType').attr("disabled", true);
          $('#chequePaymentType').attr("disabled", true);

          $('#cashPaymentType').prop('checked', true).trigger('change');
        }
      }

      $(".treasury-currency-symbol").text(data.CurrencyVM.Symbol);
      $("#treasuryBalance").text(data.Balance);
    })
  } else {
    $(".treasury-currency-symbol").text('');
    $("#treasuryBalance").text('');

    $("#paymentDetailsRow").addClass('d-none');
  }
}

checkPaymentType = function () {
  let paymentType = parseInt($("input[name='Type']:checked").val());

  switch (paymentType) {
    case 0:
      $(".not-cash").addClass('d-none');
      $(".is-transfer").addClass('d-none');
      $(".is-cheque").addClass('d-none');
      $(".not-cheque").removeClass('d-none');
      break;
    case 1:
      $(".not-cash").removeClass('d-none');
      $(".is-transfer").removeClass('d-none');
      $(".is-cheque").addClass('d-none');
      $(".not-cheque").removeClass('d-none');
      break;
    case 2:
      $(".not-cash").removeClass('d-none');
      $(".is-transfer").addClass('d-none');
      $(".is-cheque").removeClass('d-none');
      $(".not-cheque").addClass('d-none');
      break;
  }
}

calculateTotalAmount = function () {
  let paymentAmount = parseFloat($("#paymentAmount").val());
  let exchangeRate = parseFloat($("#ExchangeRate").val());

  let totalAmount = paymentAmount * exchangeRate;
  $("#TotalAmount").val(totalAmount.toFixed(2));
  $(".total-amount").text(totalAmount.toFixed(2));
}

openAddTypeModal = function (id) {
  showLoader();
  $.get(`/Payments/AddPaymentType?requestId=${id}`, function (data) {
    $("#modelPayment").html(data);
    $("#modalAddPayment").modal('show');
    $.validator.unobtrusive.parse("#paymentTypeForm");
    hideLoader();
  })
}

openEditTypeModal = function (id) {
  showLoader();
  $.get(`/Payments/EditPaymentType?requestId=${id}`, function (data) {
    $("#modelPayment").html(data);

    checkTreasury(false);
    checkPaymentType();
    calculateTotalAmount();

    $("#modalAddPayment").modal('show');
    $.validator.unobtrusive.parse("#paymentTypeForm");
    hideLoader();
  })
}

savePaymentType = function () {
  let isValid = $("#paymentTypeForm").valid();

  if (isValid) {
    showLoader();
    $.ajax({
      url: '/Payments/SavePaymentType',
      method: 'post',
      data: $("#paymentTypeForm").serialize(),
      success: function (result) {
        if (result) {
          $("#modalAddPayment").modal('hide');
          reloadPayments();
        } else {
          toastr.error(ERROR_DB);
        }
      },
      complete: function () {
        hideLoader();
      }
    })
  }
}

reloadPayments = function () {
  showLoader();
  $.ajax({
    url: '/Payments/GetAllPayments',
    success: function (result) {
      $("#allPayments").html(result);

      handleDataTable({
        selector: "#tableRequestedPayments",
        stateloadcallback: stateLoadParams
      });
      handleDataTable({
        selector: "#tablePaidPayments",
        stateloadcallback: stateLoadParams
      });
      handleDataTable({
        selector: "#tableInprogressPayments",
        stateloadcallback: stateLoadParams
      });

      hideLoader();
    }
  })
}

showPaymentDetails = function (id) {
  showLoader();

  $.ajax({
    url: `/Payments/GetPaymentDetails/${id}`,
    success: function (result) {
      $("#detailsVM").html(result);
      $("#modalPaymentDetails").modal('show');
    },
    complete: function () {
      hideLoader();
    }
  })
}

addPaymentType = function (tableId) {
  $("#modalCanNotApprove").modal('hide');

  var selectedCB = $(`#${tableId} input:checked`)[0];

  let paymentRequestId = $(selectedCB).data('id');
  openAddTypeModal(paymentRequestId);
}

editPaymentType = function (tableId) {
  $("#modalCanNotApprove").modal('hide');

  var selectedCB = $(`#${tableId} input:checked`)[0];

  let paymentRequestId = $(selectedCB).data('id');
  openEditTypeModal(paymentRequestId);
}

approveRequest = function (tableId) {
  var selectedCB = $(`#${tableId} input:checked`)[0];

  let paymentRequestId = $(selectedCB).data('id');

  let requestHasPayment = ($(selectedCB).data('has-payment') === 'True');
  if (!requestHasPayment) {
    $("#modalCanNotApprove").modal('show');
    return;
  }

  showLoader();
  $.ajax({
    url: `/Payments/ApproveRequest?requestId=${paymentRequestId}`,
    success: function (result) {
      if (result) {
        reloadPayments();
      }
    },
    complete: function () {
      hideLoader();
    }
  })
}

openRejectModal = function (tableId) {
  let selectedCB = $(`#${tableId} input:checked`)[0];

  let paymentRequestId = $(selectedCB).data('id');
  $("#reject_PaymentRequestId").val(paymentRequestId);
  $("#modalReject").modal('show');
}

rejectRequest = function (tableId) {
  let selectedCB = $(`#${tableId} input:checked`)[0];

  let paymentRequestId = $(selectedCB).data('id');
  let rejectReason = $("#reject_Reason").val();
  if (rejectReason) {
    showLoader();
    $.ajax({
      url: `/Payments/RejectRequest?requestId=${paymentRequestId}&reason=${rejectReason}`,
      success: function (result) {
        if (result) {
          $("#modalReject").modal('hide');
          reloadPayments();
        }
      },
      complete: function () {
        hideLoader();
      }
    })
  } else {
    toastr.error(INVALID_REJECT_REASON);
  }
}

showDeleteModal = function () {
  $("#modalDelete").modal('show');
}

deleteRequest = function (tableId) {
  let selectedCBs = $(`#${tableId} input:checked`);

  let requestIds = [];
  selectedCBs.each(function () {
    requestIds.push($(this).data('id'));
  })

  $.ajax({
    url: `/Payments/DeleteRequests`,
    method: 'POST',
    data: { requestIds: requestIds },
    success: function (result) {
      if (result) {
        $("#modalDelete").modal('hide');
        reloadPayments();
      }
    },
    complete: function () {
      hideLoader();
    }
  })
}

openAttachmentModal = function (id) {
  showLoader();
  $.ajax({
    url: `/Payments/GetAttachmentDetails?requestId=${id}`,
    success: function (result) {
      $("#attachmentVM").html(result);

      $(".payment-attachment-file").on('change', function (e) {
        var fileName = "";

        if (e.target.files.length > 0) {
          fileName = e.target.files[0].name;
        }

        $(this).siblings(".payment-attachment-file-title").text(fileName);

        checkAttachmentBtn();
      })

      checkAttachmentBtn();
      $("#modalAttachment").modal('show');
    },
    complete: function () {
      hideLoader();
    }
  })
}

checkAttachmentBtn = function () {
  let isValid = false;

  $(".payment-attachment-file-title").each(function () {
    let fileName = $(this).text();
    if (fileName) {
      isValid = true;
    }
  })

  if (isValid) {
    $(".btn-attachment").removeAttr('disabled');
  } else {
    $(".btn-attachment").attr('disabled', true);
  }
}

saveAttachments = function () {
  let form = $("#attachmentsForm")[0];

  var formData = new FormData(form);

  showLoader();
  $.ajax({
    url: '/Payments/UploadAttachments',
    method: 'POST',
    data: formData,
    enctype: 'multipart/form-data',
    processData: false,
    contentType: false,
    success: function (result) {
      $("#modalAttachment").modal('hide');
      reloadPayments();
    },
    complete: function () {
      hideLoader();
    }
  })
}
