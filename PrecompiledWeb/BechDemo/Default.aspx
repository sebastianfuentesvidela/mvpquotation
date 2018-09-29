<%@ page language="C#" autoeventwireup="true" inherits="_Default, App_Web_9_qslq2n" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <link href="Contents/jquery.datepick.css" rel="stylesheet" type="text/css" />
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
    <script src="Scripts/jquery.plugin.min.js"></script>
    <script src="Scripts/jquery.datepick.min.js"></script>
<script>
    $(function() {
        $('#popupDatepicker').datepick();
        $('#inlineDatepicker').datepick({ onSelect: showDate });
    });

    function showDate(date) {
        alert('The date chosen is ' + date);
    }
</script>
</head>
<body>
    <div id="divReportContent">pepe</div>
    <form id="form1" runat="server">
    <div>
    <p>A popup datepicker <input type="text" id="popupDatepicker"></p>
<p>Or inline</p>
<div id="inlineDatepicker"></div>
    </div>
    </form>
</body>
</html>
