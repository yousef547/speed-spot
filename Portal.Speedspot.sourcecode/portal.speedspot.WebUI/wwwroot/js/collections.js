
clearPurchaseOrderData = function () {
  $("#department").val("");
  $("#customer").val("");
  $("#vatno").text("#");
  clearProducts();
}

getPurchaseOrderData = function () {
  let purchaseOrderId = $("#PurchaseOrderId").val();
  if (purchaseOrderId) {
    let selectedOption = $("#PurchaseOrderId option:selected");

    $("#customerPONumber").val(selectedOption.data("customerpo"));
    $("#department").val(selectedOption.data("departmentname"));
    $("#customer").val(selectedOption.data("customername"));
    $("#vatno").text("#" + selectedOption.data("vatno"));

    getProducts(purchaseOrderId);
  } else {
    $("#customerPONumber").val("");
    clearPurchaseOrderData();
  }
}

getPurchaseOrderDataByPoNumber = function () {
  let poNumber = $("#customerPONumber").val();

  if (poNumber) {
    showLoader();
    $.ajax({
      url: `/Collections/GetPurchaseOrderByCustomerPO?poNumber=${poNumber}`,
      success: function (data) {
        if (data) {
          $("#PurchaseOrderId").val(data.Id);
          $("#vatno").text("#" + data.CustomerVatNo);
          if (CURRENT_LANG == "Ar") {
            $("#department").val(data.DepartmentVM.NameAr);
            $("#customer").val(data.CustomerNameAr);
          } else {
            $("#department").val(data.DepartmentVM.Name);
            $("#customer").val(data.CustomerName);
          }

          getProducts(data.Id);
        } else {
          $("#PurchaseOrderId").val("");
          clearPurchaseOrderData();
        }

        getGeneratedCode();
      },
      complete: function () {
        hideLoader();
      }
    })
  } else {
    $("#PurchaseOrderId").val("");
    clearPurchaseOrderData();
  }
}

calculateDueDate = function () {
  let invoiceDate = $("#InvoiceDate").val();
  let daysNo = parseInt($("#PaymentTermId option:selected").data("daysno"));

  if (invoiceDate && !isNaN(daysNo)) {
    showLoader();
    $.ajax({
      url: "/Collections/CalculateDueDate",
      data: { invoiceDate, daysNo },
      success: function (result) {
        $("#duedate").text(result);
      },
      complete: function () {
        hideLoader();
      }
    })
  } else {
    $("#duedate").text("");
  }
}

getGeneratedCode = function () {
  let departmentId = parseInt($("#PurchaseOrderId option:selected").data("departmentid"));
  let invoiceDate = $("#InvoiceDate").val();

  if (departmentId && invoiceDate) {
    showLoader();
    $.ajax({
      url: "/Collections/GenerateCode",
      data: { departmentId, invoiceDate },
      success: function (result) {
        $("#generatedcode").text("#" + result);
      },
      complete: function () {
        hideLoader();
      }
    })
  } else {
    $("#generatedcode").text("#");
  }
}

isTaxIncludedChanged = function () {
  let isTaxIncluded = parseInt($("#IsTaxIncluded").val());

  if (isTaxIncluded) {
    $(".tax-column").addClass("invisible");
  } else {
    $(".tax-column").removeClass("invisible");
  }

  calculateAllItemsTotal();
}

calculateItemTotal = function (index) {
  let itemPrice = parseFloat($(`#item_${index}`).find(".item-price").text());
  let itemQTY = parseFloat($(`#item_${index}`).find(".item-quantity").val());
  let itemTax = parseFloat($(`#item_${index}`).find(".item-tax").val() / 100);
  let itemSubTotal = itemPrice * itemQTY;
  let itemTotal = 0;

  let isTaxIncluded = parseInt($("#IsTaxIncluded").val());

  if (isTaxIncluded) {
    itemTotal = itemSubTotal;
  } else {
    itemTotal = itemSubTotal + itemSubTotal * itemTax;
  }

  $(`#item_${index}`).find(".item-total").text(itemTotal.toFixed(2));
}

calculateAllItemsTotal = function () {
  $(".item-row").each(function (index, value) {
    calculateItemTotal(index);
  })
}

getProducts = function (purchaseOrderId) {
  showLoader();
  $.ajax({
    url: `/Collections/GetPurchaseOrderProducts?purchaseOrderId=${purchaseOrderId}`,
    success: function (result) {
      $("#products").html(result);

      isTaxIncludedChanged();

      calculateAllItemsTotal();
    },
    complete: function () {
      hideLoader();
    }
  })
}

clearProducts = function () {
  $("#products table tbody tr").remove();
}
