$(document).ready(function () {
    $('.status-select').each(function () {
        var selectElement = $(this);
        selectElement.data('original-status', selectElement.val());
    });

    $('.status-select').change(function () {
        var selectElement = $(this);
        var billId = selectElement.data('bill-id');
        var originalStatus = selectElement.data('original-status');
        var newStatus = selectElement.val();
        console.log(billId);
        // Remove existing color classes from the selected option
        selectedOption.removeClass('green-text red-text yellow-text');

        // Add the appropriate color class based on the option value
        if (optionValue == 2) {
            selectedOption.addClass('green-text');
        } else if (optionValue == 3 || optionValue == 4) {
            selectedOption.addClass('red-text');
        } else {
            selectedOption.addClass('yellow-text');
        }
        // Hiển thị hộp thoại xác nhận
        var confirmResult = confirm("Bạn có chắc muốn thay đổi trạng thái?");

        if (confirmResult) {
            $.ajax({
                url: '/Admin/Bill/ChangeStatus',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify({ billID: billId, status: newStatus }),

                success: function (response) {
                    console.log(response);
                    // Hiển thị thông báo thành công bằng alert
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    // Xử lý lỗi từ server
                    selectElement.val(originalStatus);
                }
            });
        } else {
            // Người dùng chọn "No", không thực hiện yêu cầu thay đổi trạng thái
            // Khôi phục lại trạng thái ban đầu
            selectElement.val(originalStatus);
        }
    });

    $('.status-option').each(function () {
        var index = $(this).val();
        var optionText = $(this).text();

        if (index == 2) {
            $(this).addClass('green-text');
        } else if (index == 3 || index == 4) {
            $(this).addClass('red-text');
        } else {
            $(this).addClass('yellow-text');
        }
    });
});
