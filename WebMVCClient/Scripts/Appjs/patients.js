<script>
    $(document).ready(function () {
         var oTable = $('#myDatatable').DataTable({
        "ajax": {
        "url": '/PatientClient/LoadPatient',
                 "type": "get",
                 "datatype": "json"
             },
             "columns": [
                 {"data": "ID", "name": "ID", "autoWidth": true },
                 {"data": "patient_registration_no", "name": "CustomerID", "autoWidth": true },
                 {"data": "patient_first_name", "name": "CompanyName", "autoWidth": true },
                 {"data": "patient_last_name", "title": "ContactName", "name": "ContactName", "autoWidth": true },
                 {"data": "patient_dob", "name": "DOB", "autoWidth": true },
                 {"data": "patient_blood_type", "name": "BloodGroup", "autoWidth": true },
                 {"data": "patient_phone", "name": "Phone", "autoWidth": true },
                 {"data": "patient_city", "name": "City", "autoWidth": true },
                 {"data": "patient_sex", "name": "Sex", "title": "Status", "autoWidth": true },
                 {
        "data": "ID", "width": "50px", "render": function (data) {
                         return '<a class="popup" href="/PatientClient/RegisterPatient/' + data + '">Edit</a>';
                     }
                 },
                 {
        "data": "ID", "width": "50px", "render": function (data) {
                         return '<a class="popup" href="/home/delete/' + data + '">Delete</a>';
                     }
                 }
             ]
         })
         $('.tablecontainer').on('click', 'a.popup', function (e) {
        e.preventDefault();
    OpenPopup($(this).attr('href'));
         })
         function OpenPopup(pageUrl) {
             var $pageContent = $('<div />');
             $pageContent.load(pageUrl, function () {
        $('#popupForm', $pageContent).removeData('validator');
    $('#popupForm', $pageContent).removeData('unobtrusiveValidation');
                 $.validator.unobtrusive.parse('form');

             });

             $dialog = $('<div class="popupWindow" style="overflow:auto"></div>')
                 .html($pageContent)
                 .dialog({
        draggable: false,
                     autoOpen: false,
                     resizable: false,
                     model: true,
                     title: 'Popup Dialog',
                     height: 550,
                     width: 600,
                     close: function () {
        $dialog.dialog('destroy').remove();
    }
                 })

             $('.popupWindow').on('submit', '#popupForm', function (e) {
                 var url = $('#popupForm')[0].action;
                 $.ajax({
        type: "POST",
                     url: url,
                     data: $('#popupForm').serialize(),
                     success: function (data) {
                         if (data.status) {
        $dialog.dialog('close');
    oTable.ajax.reload();
                         }
                     }
                 })

                 e.preventDefault();
             })

             $dialog.dialog('open');
         }
     })
</script>