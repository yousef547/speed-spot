
showIncomeLoader = function () {
  $("#income-spinner").removeClass('d-none');
}

showExpenseLoader = function () {
  $("#expense-spinner").removeClass('d-none');
}

hideIncomeLoader = function () {
  $("#income-spinner").addClass('d-none');
}

hideExpenseLoader = function () {
  $("#expense-spinner").addClass('d-none');
}

incomeDepartmentChanged = function () {
  let departmentId = $("#incomeDepartmentId").val();

  showIncomeLoader();
  $.ajax({
    url: "/ChartOfAccounts/GetIncomeTree?departmentId=" + departmentId,
    success: function (data) {
      $("#incomeTree").html(data);
      financeTreeInitialize("#incomeTree");
    },
    complete: function () {
      hideIncomeLoader();
    }
  })
}

expenseDepartmentChanged = function () {
  let departmentId = $("#expenseDepartmentId").val();

  showExpenseLoader();
  $.ajax({
    url: "/ChartOfAccounts/GetExpenseTree?departmentId=" + departmentId,
    success: function (data) {
      $("#expenseTree").html(data);
      financeTreeInitialize("#expenseTree");
    },
    complete: function () {
      hideExpenseLoader();
    }
  })
}

clearAccountForm = function () {
  $('#Id').val('');
  $('#ParentId').val('');
  $('#Name').val('');
  $('#NameAr').val('');
  $('#Code').val('');
  $('#AutoCode').val('');
  $('#accountInfoForm').find('.text-danger').removeClass('field-validation-error').addClass('field-validation-valid').html('');
  $("#btnCreate").removeClass('d-none');
  $("#btnUpdate").addClass('d-none');
}

addParentIncomeAccount = function () {
  clearAccountForm();

  $("#Type").val(1);

  let departmentId = $("#incomeDepartmentId").val();
  $("#DepartmentId").val(departmentId);

  $("#btnCreate").removeClass('d-none');
  $("#btnUpdate").addClass('d-none');

  showIncomeLoader();
  $.ajax({
    url: "/ChartOfAccounts/GenerateNewParentCode?departmentId=" + departmentId + "&type=1",
    success: function (data) {
      $("#GeneratedCode").val(data);
      $("#modalCreate").modal('show');
    },
    complete: function () {
      hideIncomeLoader();
    }
  })
}

addParentExpenseAccount = function () {
  clearAccountForm();

  $("#Type").val(0);

  let departmentId = $("#expenseDepartmentId").val();
  $("#DepartmentId").val(departmentId);

  $("#btnCreate").removeClass('d-none');
  $("#btnUpdate").addClass('d-none');

  showExpenseLoader();
  $.ajax({
    url: "/ChartOfAccounts/GenerateNewParentCode?departmentId=" + departmentId + "&type=0",
    success: function (data) {
      $("#GeneratedCode").val(data);
      $("#modalCreate").modal('show');
    },
    complete: function () {
      hideExpenseLoader();
    }
  })
}

createAccount = function () {
  let isValid = $("#accountInfoForm").valid();
  let type = parseInt($("#Type").val());

  if (isValid) {
    let form = $("#accountInfoForm");
    showLoader();
    $.ajax({
      url: '/ChartOfAccounts/Create',
      method: 'POST',
      data: form.serialize(),
      success: function (data) {
        if (type == 1) {
          $("#incomeTree").html(data);
          financeTreeInitialize("#incomeTree");
          incomeDepartmentChanged();
        } else {
          $("#expenseTree").html(data);
          financeTreeInitialize("#expenseTree");
          expenseDepartmentChanged();
        }

        $("#modalCreate").modal('hide');
      },
      complete: function () {
        hideLoader();
      }
    })
  }

}

addAccount = function (parentId, type) {
  clearAccountForm();
  $('#ParentId').val(parentId);
  $("#Type").val(type);

  if (type == 0) {
    $("#DepartmentId").val($("#expenseDepartmentId").val())
  } else {
    $("#DepartmentId").val($("#incomeDepartmentId").val());
  }

  $("#btnCreate").removeClass('d-none');
  $("#btnUpdate").addClass('d-none');

  showLoader();
  $.ajax({
    url: "/ChartOfAccounts/GenerateNewCode?parentId=" + parentId,
    success: function (data) {
      $("#GeneratedCode").val(data);
      $("#modalCreate").modal('show');
    },
    complete: function () {
      hideLoader();
    }
  })
}

editAccount = function (accountId) {
  showLoader();
  $.ajax({
    url: '/ChartOfAccounts/GetAccountInfo/' + accountId,
    success: function (data) {
      $("#accountInfoModal").html(data);

      $("#btnCreate").addClass('d-none');
      $("#btnUpdate").removeClass('d-none');
      $("#modalCreate").modal('show');
    },
    complete: function () {
      hideLoader();
    }
  })
}

updateAccount = function () {
  let isValid = $("#accountInfoForm").valid();

  let type = parseInt($("#Type").val());

  if (isValid) {
    let form = $("#accountInfoForm");
    showLoader();
    $.ajax({
      url: '/ChartOfAccounts/Edit',
      method: 'POST',
      data: form.serialize(),
      success: function (data) {
        if (type == 0) {
          $("#expenseTree").html(data);
          financeTreeInitialize("#expenseTree");
          expenseDepartmentChanged();
        } else {
          $("#incomeTree").html(data);
          financeTreeInitialize("#incomeTree");
          incomeDepartmentChanged();
        }
        $("#modalCreate").modal('hide');
      },
      complete: function () {
        hideLoader();
      }
    })
  }
}

deleteAccount = function (accountId, departmentId, type) {
  showLoader();
  $.ajax({
    url: '/ChartOfAccounts/Delete/' + accountId + '?departmentId=' + departmentId + "&type=" + type,
    success: function (data) {
      if (type == 0) {
        $("#expenseTree").html(data);
        financeTreeInitialize("#expenseTree");
      } else {
        $("#incomeTree").html(data);
        financeTreeInitialize("#incomeTree");
      }
    },
    complete: function () {
      hideLoader();
    }
  })
}

viewAccount = function (accountId) {
  //checkIsIncome();
  showLoader();
  $.ajax({
    url: '/ChartOfAccounts/Details/' + accountId,
    success: function (data) {
      $("#viewInfoModal").html(data);
      $("#modalViewBox").modal('show');
    },
    complete: function () {
      hideLoader();
    }
  })
}

viewRun = function (id) {
  showLoader();
  $.get(`/ChartOfAccounts/GetEntiesOfAccount/${id}`, function (data, status) {
    $("#JournalEntry").html(data);
    $("#modalViewRun").modal('show');

    hideLoader();
  })
}
