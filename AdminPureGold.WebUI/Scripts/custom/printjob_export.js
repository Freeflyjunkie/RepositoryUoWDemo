$(document).ready(function () {
    $("#btnExport").click(function () {
        $("#dvData").btechco_excelexport({
            containerid: "dvData"
           , datatype: $datatype.Table
        });
    });
});