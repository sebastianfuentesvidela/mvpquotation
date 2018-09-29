        $(function() {
            $("#GeneraDocs").hide(0);
            $('.selector').datepick();
            $('.cotfechas').datepick({
                onSelect: function(dates) { TotalizaPlazos(); }
            });
            $('#sprFechaVctoEstimada').datepick({
                onSelect: function(dates) { return PlazoSegunVencimiento(); },
                onClose: function(dates) { return PlazoSegunVencimiento(); },
                showOnFocus: false,
                showTrigger: '#calImg'
            });
            $(".integer").numeric(false, function() { alert("Integers only"); this.value = ""; this.focus(); });
            $(".positive").numeric({ negative: false }, function() { alert("No negative values"); this.value = ""; this.focus(); });
            $(".positive-integer").numeric({ decimal: false, negative: false }, function() { alert("Positive integers only"); this.value = ""; this.focus(); });
            $(".decimal-2-places").numeric({ decimalPlaces: 2 });
            $(".decimal4").numeric({ decimalPlaces: 4 });

            /*       $('.numeric').numeric(
            { negative: false, decimal: false, precision: 12, scale: 2 },
            function() {
            alert('Positive Integers only');
            this.value = "";
            this.focus();
            }
            );*/
            //            $('#cboTipoOperacion').bind("change", function() {
            //                alert(this.toString());
            //            });
            $(".numerico2d").keydown(function(e) {
                // Allow: backspace, delete, tab, escape, enter and .
                if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
                // Allow: Ctrl/cmd+A
                (e.keyCode == 65 && (e.ctrlKey === true || e.metaKey === true)) ||
                // Allow: Ctrl/cmd+C
                (e.keyCode == 67 && (e.ctrlKey === true || e.metaKey === true)) ||
                // Allow: Ctrl/cmd+X
                (e.keyCode == 88 && (e.ctrlKey === true || e.metaKey === true)) ||
                // Allow: home, end, left, right
                (e.keyCode >= 35 && e.keyCode <= 39)) {
                    // let it happen, don't do anything
                    return;
                }
                // Ensure that it is a number and stop the keypress
                if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                    e.preventDefault();
                }
            }).click(function() { nozero(this) });
            $(".numerico4d").keydown(function(e) {
                // Allow: backspace, delete, tab, escape, enter and .
                if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
                // Allow: Ctrl/cmd+A
                (e.keyCode == 65 && (e.ctrlKey === true || e.metaKey === true)) ||
                // Allow: Ctrl/cmd+C
                (e.keyCode == 67 && (e.ctrlKey === true || e.metaKey === true)) ||
                // Allow: Ctrl/cmd+X
                (e.keyCode == 88 && (e.ctrlKey === true || e.metaKey === true)) ||
                // Allow: home, end, left, right
                (e.keyCode >= 35 && e.keyCode <= 39)) {
                    // let it happen, don't do anything
                    return;
                }
                // Ensure that it is a number and stop the keypress
                if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                    e.preventDefault();
                }
            }).click(function() { nozero(this) });
            $(".numerico0d").keydown(function(e) {
                // Allow: backspace, delete, tab, escape, enter and .
                if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
                // Allow: Ctrl/cmd+A
                (e.keyCode == 65 && (e.ctrlKey === true || e.metaKey === true)) ||
                // Allow: Ctrl/cmd+C
                (e.keyCode == 67 && (e.ctrlKey === true || e.metaKey === true)) ||
                // Allow: Ctrl/cmd+X
                (e.keyCode == 88 && (e.ctrlKey === true || e.metaKey === true)) ||
                // Allow: home, end, left, right
                (e.keyCode >= 35 && e.keyCode <= 39)) {
                    // let it happen, don't do anything
                    return;
                }
                // Ensure that it is a number and stop the keypress
                if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                    e.preventDefault();
                }
            }).click(function() { nozero(this) });
            
            $('#botCotizar').click(function(event) {alert('hasta aqui queda esta demo...'); return false;});

            var markup;
            $('#cmdBuscarCliente').click(function(event) {
                event.preventDefault();
                targetpoint = 'setcliente';
                $.ajax({ url: 'Templates/ListaClientes.htm',
                    async: false, cache: true, dataType: 'html',
                    success: function(data) { markup = data; 
                     $.template("lisclitempl", markup);
                    }
                });
                        $('#pronomcli').val('');
                        ShowListaClientes(1);
                        $('#listaClientes').modal({
                            fadeDuration: 2
                        });

             });
   


                //$('#marcoCli').attr('src', 'SelListaClientes.aspx');
                

            $('#btBuscSuby').click(function(event) {
                event.preventDefault();
                targetpoint = 'setsubyacente';
                $('#forCliente').modal({
                    fadeDuration: 250
                });
                $('#rotulocliente').text('Listado de Clientes');
                $('#marcoCliente').attr('src', 'SelListaClientes.aspx');
                // $("#divframe").html('<iframe src=\"ShowInfo.aspx\" width=\"100%\" height=\"100\" scrolling=\"no\" frameborder=\"0\"  style=\"background-color:Transparent;\" />');
                //$.get('ajax2.html', function(htmlo) {
                //    $(htmlo).appendTo('#lapata2a').modal();
                //});
                // $('#hidcommand').val($('#txtStockValue').val());
                //document.forms['formQuotLaunch'].submit();
            });

            $('#cboMoneda').change(function(event) {
                var request = {
                    'accion': 'paridad',
                    'pointer': $('#cboMoneda').val(),
                    'fecha': $('#sprFechaCotizacion').val()
                }
                $.ajax({
                    url: 'Quest.aspx',
                    data: JSON.stringify(request),
                    success: function(data) {
                        if (data[0].Key == 'paridad') {
                            $('#parMoneda').val(data[0].Value);
                            CalculaMontos();
                        }
                        $('#div_remoto').html('');
                        $('#div_remoto').html(JSON.stringify(data));
                    },
                    error: function(request, status, error) {
                        alert(request.responseText);
                        $('#div_showerror').html(request.responseText);
                    },
                    type: 'POST',
                    dataType: 'json',
                    cache: false,
                    contentType: 'application/json'
                });

            });

            $('#sprMontoOperacion').blur(function(event) {
                CalculaMontos();
            });
            $('#sprPorcentajeTolerancia').blur(function(event) {
                CalculaMontos();
            });
            $('#sprPorcenGarantia').blur(function(event) {
                CalculaMontos();
            });
            $('#sprValorTasaPago').blur(function(event) {
                CalculaSpreads();
            });
            $('#sprValorSpreadPago').blur(function(event) {
                CalculaSpreads();
            });
            $('#sprValorTasaPtmo').blur(function(event) {
                CalculaSpreads();
            });
            $('#sprValorSpreadPtmo').blur(function(event) {
                CalculaSpreads();
            });

            $('#sprPlazoMaxResidualCtg').blur(function(event) {
                TotalizaPlazos();
            });
            $('#sprFechaVctoEstimada').blur(function(event) {
                PlazoSegunVencimiento();
            });
            $('#sprFechaVctoEstimada').change(function(event) {
                PlazoSegunVencimiento();
            });
            CalculaMontos();
            TotalizaPlazos();
            CalculaSpreads();
            
            var comanda = $("#hidcommand").val();
            if (comanda == 'opensimuliz') {
                var json = $.parseJSON($("#pageMessage").html());

                var markup;
                $.ajax({ url: 'Templates/Simulacion.htm',
                    async: false, cache: true, dataType: 'html',
                    success: function(data) { markup = data; }
                });
                $.template("simultempl", markup);
                $.tmpl("simultempl", json).appendTo('#simuliz div#resultado')
                $('#simuliz').modal({
                    fadeDuration: 2
                });
            } else if (comanda == 'faltainfo') {

                var json = $.parseJSON($("#pageMessage").html());
                $('#simuliz').html('<span class=\"titulopanel\">Falta Información para Simular</span>' +
                        '<br /><ul>');
                $.each(json.faltainfo, function(index, value) {
                    $('#simuliz').append('<li>' + value + '</li>');
                });
                $('#simuliz').append('</ul>');

                $('#simuliz').modal({
                    fadeDuration: 2
                });
            } else if (comanda == 'badgenerate') {

                var json = $.parseJSON($("#pageMessage").html());
                $('#simuliz').html('<span class=\"titulopanel\">Hubo errores al Simular</span>' +
                        '<br /><ul>');
                $.each(json.generando, function(index, value) {
                    $('#simuliz').append('<li>' + value + '</li>');
                });
                $('#simuliz').append('</ul>');

                $('#simuliz').modal({
                    fadeDuration: 2
                });
            } else if (comanda == 'errores') {

                var json = $.parseJSON($("#pageMessage").html());
                $('#simuliz').html('<span class=\"titulopanel\">Hubo errores al Cargar la página, consulte al administrador.</span>' +
                        '<br /><ul>');
                       // alert(JSON.stringify(json));
                $.each(json.errores, function(index, value) {
                    $('#simuliz').append('<li>' + value + '</li>');
                });
                $('#simuliz').append('</ul>');

                $('#simuliz').modal({
                    fadeDuration: 2
                });
            }
            $("#hidcommand").val('')

        });
        function ShowListaClientes(pag) {
                var request = {
                    'accion': 'listaclientes',
                    'pointer': pag,
                    'numcli': $("#txtRutCliente").val(),
                    'nomcli': $("#pronomcli").val()
                }
                $.ajax({
                    url: 'Quest.aspx',
                    data: JSON.stringify(request),
                    success: function(data) {
                        $('#pageMessage').html(JSON.stringify(data));
                        $('#marcoCli').html('');
                        $.tmpl("lisclitempl", data.clientes).appendTo('#marcoCli')
                        $('#page_navigation').text(data.paginacion[0].actual);
                        var ene = JSON.stringify(data.paginacion[0]);
                        pagination(data.paginacion[0].actual, data.paginacion[0].totalpages);   //
                        $('#rotulocli').text('Listado de Clientes');
                        $('#pronomcli').focus();
                        
                    },
                    error: function(request, status, error) {
                        alert(request.responseText);
                        //$('#div_showerror').html(request.responseText);
                    },
                    type: 'POST',
                    dataType: 'json',
                    cache: false,
                    contentType: 'application/json'
                });
            }
            
        function AceptaSimular() {
            //sin linea
            var cli = $("#lbSinLineaCliente").text();
            var sby = $("#lbSinLineaSuby").text();
            if (cli != '' || sby != '') {
                if (!confirm("Esta a punto de Generar una Simulacion para un Cliente Sin Linea..\r\n Está Seguro?"))
                { return 0; } else return 1;
            }  else return 1;
            //alert(cli)
        }
        function printThisMe(divIdd) {
            var innerpage = $(divIdd).html();
            var windowUrl = 'about:blank';
            var uniqueName = new Date();
            var windowName = 'Print' + uniqueName.getTime();
            var printWindow = window.open(windowUrl, windowName, 'left=500,top=500,width=0,height=0');

            printWindow.document.write('<html');
            printWindow.document.write('><head');
            printWindow.document.write('><title');
            printWindow.document.write('>Informe</t');
            printWindow.document.write('itle>');
            printWindow.document.write('<link rel="stylesheet" type="text/css" href="Contents/quotlaunch.css"/>'); 
            printWindow.document.write('</head><body >');
            printWindow.document.write(innerpage);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            printWindow.focus();
            setTimeout(function(){ 
                printWindow.print(); 
                printWindow.close();
                }, 2)
        
        }
        function showReserva(limKey) { 

            var markup;
            $.ajax({ url: 'Templates/ReservaLimites.htm',
                async: false, dataType: 'html',
                success: function(data) {
                    markup = data; 
                }
            });
            var request = {
                'accion': 'relacionlimitecotizacion',
                'pointer': limKey,
                'numcli': $("#txtRutCliente").val(),
                'numcli': $("#txtNombreCliente").val(),
            }
            $.ajax({
                url: 'Quest.aspx',
                data: JSON.stringify(request),
                success: function(data) {
                    $.template("tReserva", markup);
                    $('#listacotiz').html('');
                    $.tmpl("tReserva", data).appendTo('#listacotiz')
                        $('#forReserva').modal({
                            fadeDuration: 2,
                            closeExisting: false
                        });                   
                },
                error: function(request, status, error) {
                    alert(request.responseText);
                    //$('#div_showerror').html(request.responseText);
                },
                type: 'POST',
                dataType: 'json',
                cache: false,
                contentType: 'application/json'
            });

        }
        function MostrarSimulacion() {
            $('#simuliz').modal({
                fadeDuration: 200
            });
        }
        function InformeEnIframe(infoname) {
            $('#infoPopup').modal({
                fadeDuration: 250
            });
            switch (infoname) {
                case 'restricciones':
                    $('#rotuloinf').text('Informe Nro 1');
                    $('#iframeinforme').attr('src', 'ShowInfo.aspx?.=2638769897498669564376283');
//contenediv
                    var request = {
                        'fecha': $('#sprFechaCotizacion').val(),
                        'iddrequest': 'pepe'
                    }
                    $.post('ShowInfo.aspx', JSON.stringify(request), function(htmlr) {
                        $('#contenediv').html(htmlr);
                    });

                    break;
                default:
            }
        }
        function postToIframe(data, url, target) {
            $('body').append('<form action="' + url + '" method="post" target="' + target + '" id="postToIframe"></form>');
            $.each(data, function(n, v) {
                $('#postToIframe').append('<input type="hidden" name="' + n + '" value="' + v + '" />');
            });
            $('#postToIframe').submit().remove();
        }
        function StartFreshOne() {
            if (confirm('Desea Limpiar el Formulario?')){
                $("#pageIdd").val('');
                $("#hidcommand").val('cleanedit')
            } else return false;
        }
        var targetpoint;
        function SetCliente(point) {

            $("#GeneraDocs").hide(200);
            $.modal.close();
            $("#hidcommand").val(targetpoint);
            $("#hidargument").val(point);

            document.forms['formQuotLaunch'].submit();
            /*parent.SetCliente(point)*/
        }
        function AcceptSimuliz(cancot) {
            $.modal.close();
            if (cancot == false) {
                $("#botCotizar").prop('disabled', true);
                //alert(cancot);
            } else {
                $("#botCotizar").removeAttr('disabled');
                //alert(cancot);
            }
            $("#GeneraDocs").show(200);
        }
        function ActivaSimular() {
            $("#GeneraDocs").hide(200);
        }
        function PrintFrame(id) {
            var frm = document.getElementById(id).contentWindow;
            frm.focus(); // focus on contentWindow is needed on some ie versions
            frm.print();
            //return false;
        }
        function PrintSubIFrame(id, sid) {
            var ifrm = document.getElementById(id);
            var innerDoc = ifrm.contentDocument || ifrm.contentWindow.document;

            var frm = innerDoc.getElementById(sid).contentWindow;
            frm.focus(); // focus on contentWindow is needed on some ie versions
            frm.print();
            //return false;
        }

        function printDetails() {
            $.get('ShowInfo.aspx?.=' + $("#pageIdd").val(), function(htmlr) {
                //                var printContent = document.getElementById('lapapaya');
                //                printContent = htmlr;
                //                alert(printContent);
                var windowUrl = 'about:blank';
                var uniqueName = new Date();
                var windowName = 'Print' + uniqueName.getTime();
                var printWindow = window.open(windowUrl, windowName, 'left=90,top=50,width=810,height=750,scrollbars=yes,resizable=yes,toolbar=no,location=0');

                printWindow.document.write(htmlr); //printContent.innerHTML);

                printWindow.document.close();
                printWindow.focus();
                printWindow.print();

      //          printWindow.close();
            });

            // event.preventDefault();
            // return false;
        }
        function SetInforme3b5() {
            var request = {
                'fecha': $('#sprFechaCotizacion').val(),
                'iddrequest': 'pepe'
            }
            $.post('ShowInfo.aspx', JSON.stringify(request), function(htmlr) {
                var newWindow;
                    var windowUrl = 'about:blank';
                    var uniqueName = new Date();
                    var windowName = 'Print' + uniqueName.getTime();
                    //newWindow  = window.open(windowUrl, windowName, 810, 750);
                     //alert('aqui');
                newWindow = window.open(windowUrl, windowName, 'left=90,top=50,width=810,height=750,scrollbars=yes,resizable=yes,toolbar=no,location=0');
                newWindow.document.write(htmlr);
                newWindow.document.close();
                newWindow.focus();
            });
        }

        function showDetails() {
           //return SetInforme3b5();
            
            var request = {
                'fecha': $('#sprFechaCotizacion').val(),
                'iddrequest': 'papo'
            }
            $.ajax({
                url: 'ShowInfo.aspx',
                data: JSON.stringify(request),
                success: function(data) {
                    var windowUrl = 'about:blank';
                    var uniqueName = new Date();
                    var windowName = 'Print' + uniqueName.getTime();
                    var printWindow = window.open(windowUrl, windowName, 'left=90,top=50,width=810,height=750,scrollbars=yes,resizable=yes,toolbar=no,location=0');

                    //var printWindow = window.open(windowUrl, windowName, 810, 750);

                    printWindow.document.write(data);

                    printWindow.document.close();
                    printWindow.focus();
                },
                error: function(request, status, error) {
                    alert('error: ' + (error));
                    alert('error: ' + (status));
                    alert('error: ' + (request.responseText));
                },
                type: 'POST',
                dataType: 'html',
                cache: false,
                contentType: 'application/json'
            });
        }

        function CalculaMontos() {
            var lcMontoEq = 0;
            var lcMonto = $('#sprMontoOperacion').val();
            if (lcMonto == '') lcMonto = 0;
            var lvPerc = $('#sprPorcentajeTolerancia').val();
            if (lvPerc == '') lvPerc = 0;
            var lcAfecto = +lcMonto * ((+lvPerc / 100) + 1);
            $('#sprMonto').val(roundToTwo(lcAfecto).toLocaleString('en-US'));
            if ($('#parMoneda').val() == 0) {
                alert('No existe Paridad vigente a la fecha.\n\rConsulte al Administrador del Sistema');
            } else {
                ldValorParidadDolar = $('#parMoneda').val();
                // alert(+lcAfecto * +ldValorParidadDolar);
                //var equiv = +lcAfecto * +ldValorParidadDolar
                //alert(+lcAfecto);
                //alert(ldValorParidadDolar);
                lcMontoEq = (+lcAfecto * +ldValorParidadDolar)
                $('#sprMontoEquivalente').val(roundToTwo(lcMontoEq).toLocaleString('en-US'))
            }
            $('#sprMontoOperacion').val((+lcMonto).toFixed(2));
//            
            $('#sprPorcentajeTolerancia').val((+lvPerc).toFixed(2));
            if ($('#chkNota:checked')) {
                var lgPerc = $('#sprPorcenGarantia').val();
                var mgar = (+lgPerc) / 100 * (+lcMontoEq);

                $('#sprMontoGarantia').val(roundToTwo(mgar).toLocaleString('en-US'));
            } else {
                $('#sprMontoGarantia').val((0).toFixed(2));
            }            
            $("#GeneraDocs").hide(200);
        }
        function CalculaSpreads() {
            var tpago = $('#sprValorTasaPago').val(); if (tpago == '') tpago = 0;
            var spago = $('#sprValorSpreadPago').val(); if (spago == '') spago = 0;
            var ttpago = 0 + (+tpago) + (+spago);
            
//            var mpago = ttpago.toFixed(4);
            $('#sprTasaTotalPago').val(ttpago.toFixed(4));
            $('#sprValorTasaPago').val((+tpago).toFixed(4))
            $('#sprValorSpreadPago').val((+spago).toFixed(4))

            var tptmo = $('#sprValorTasaPtmo').val(); if (tptmo == '') tptmo = 0;
            var sptmo = $('#sprValorSpreadPtmo').val(); if (sptmo == '') sptmo = 0;
            var ttptmo = 0 + (+tptmo) + (+sptmo);
            $('#sprTasaTotalPtmo').val(ttptmo.toFixed(4));
            $('#sprValorTasaPtmo').val((+tptmo).toFixed(4))
            $('#sprValorSpreadPtmo').val((+sptmo).toFixed(4))

        }
        function roundToTwo(num) {
            return +(Math.round(num + "e+2") + "e-2");
        }
        function nicenum(numb) {
            return parseFloat((+numb).toFixed(2)).toLocaleString('en-US', { minimumFractionDigits: 2 }); //.replace('/\.([0‌​-9])$/', ".$10");
        }
        function porcento(numb) {
            return parseFloat((+numb*100).toFixed(2)).toLocaleString('en-US', { minimumFractionDigits: 2 }); //.replace('/\.([0‌​-9])$/', ".$10");
        }
        function NextXdays(fecha, dias) {
            var trip = fecha.split('/');
            var datew = new Date(trip[2], trip[1] - 1, trip[0]);
            var plzo = isNaN(dias) ? 0 : +dias;
            nextday = new Date(datew.getFullYear(), datew.getMonth(), datew.getDate() + plzo);
            return nextday;

        }
        function DiffDays(fecha, fechafin) {
            var trip = fecha.split('/');
            var datep = new Date(trip[2], trip[1] - 1, trip[0]);
            var trif = fechafin.split('/');
            var datef = new Date(trif[2], trif[1] - 1, trif[0]);
            var oneDay = 24 * 60 * 60 * 1000; // hours*minutes*seconds*milliseconds
            var diffDays = Math.round(Math.abs((datef.getTime() - datep.getTime()) / (oneDay)));
            //var diffDays = datef.getDate() - datep.getDate(); 
            return diffDays;
        }
        function nozero(ctrl) {
            if (ctrl.value == '0'
                    || ctrl.value == '0.00'
                     || ctrl.value == '0.000'
                      || ctrl.value == '0.0000') { ctrl.value = '' }
        }
        function isValidDate(value) {
            var datep = new Date();
            var trip = value.split('/');
            if (trip.length >= 3) {
                datep = new Date(trip[2], trip[1] - 1, trip[0]);
                return !isNaN(datep.getDate());
            }
            var dateWrapper = new Date(value);
            return !isNaN(dateWrapper.getDate());
        }

        function PlazoSegunVencimiento() {
            var fechaCot = $('#sprFechaCotizacion').val();
            var fechaCur = $('#sprFechaCurse').val();
            var fechaVen = $('#sprFechaVctoEstimada').val();
            var plzo = 0;
            if (isValidDate(fechaVen)) {
                if (isValidDate(fechaCot) || isValidDate(fechaCur)) {
                    if (isValidDate(fechaCur)) {
                        plzo = DiffDays(fechaCur, fechaVen);
                    } else {
                        plzo = DiffDays(fechaCot, fechaVen);
                    }

                    $('#sprPlazoMaxResidualCtg').val(plzo);
                    var ven = new Date();
                    var trip = fechaVen.split('/');
                    if (trip.length >= 3) {
                        ven = new Date(trip[2], trip[1] - 1, trip[0]);
                    }
                    //$('#rDias').text(ven.toString());
                    var feria = days[sday.indexOf(ven.toString().slice(0, 3))];
                    $('#lblDiaFechaVctoEstimada').html(feria);

                }
            }
            $("#GeneraDocs").hide(200);
        }
        function ToSlashDate(dia) {
            return ('00' + dia.getDate()).slice(-2) + '/' + ('00' + (dia.getMonth() + 1)).slice(-2) + '/' + dia.getFullYear();
        }

        var sday = ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat'];
        var days = ['Domingo', 'Lunes', 'Martes', 'Miercoles', 'Jueves', 'Viernes', 'Sábado'];
        function TotalizaPlazos() {
            var plazoTotal = $('#sprPlazoMaxResidualCtg').val();
            var fechaCot = $('#sprFechaCotizacion').val();
            var fechaCur = $('#sprFechaCurse').val();
            if (isValidDate(fechaCot) || isValidDate(fechaCur)) {
                if (isValidDate(fechaCur)) {
                    vencto = NextXdays(fechaCur, plazoTotal);

                } else {
                    vencto = NextXdays(fechaCot, plazoTotal);
                }
                $('#sprFechaVctoEstimada').val(ToSlashDate(vencto));
                $('#lblDiaFechaVctoEstimada').text(days[vencto.getDay()]);
            } else {
                alert('No fue posible identificar las Fechas de la Cotización');
            }
            $("#GeneraDocs").hide(200);
        }

function pagination(curr, total){
	
	//how much items per page to show
	//var show_per_page = 20; 
	//getting the amount of elements inside content div
	var number_of_items = total; // $('#marcoCli').children().size();
	//calculate the number of pages we are going to have
	var number_of_pages = total; //Math.ceil(number_of_items/show_per_page);
	
	//set the value of our hidden input fields
	$('#current_page').val(curr);
	
	//now when we got all we need for the navigation let's make it '
	
	/* 
	what are we going to have in the navigation?
		- link to previous page
		- links to specific pages
		- link to next page
	*/
	var navigation_html = '<a class="first_link" href="javascript:go_to_page(1);">|&lt;&lt;</a>';
	    navigation_html += '<a class="previous_link" href="javascript:previous();">&nbsp;&nbsp;&lt;Ant</a>';
	var current_link = 1;
	    navigation_html += '<select onchange="gopage(this);">';
	while(number_of_pages >= current_link){
		navigation_html += '<option value="' + current_link +'" "';
		if (curr == current_link) navigation_html += ' selected="selected"' ;
		navigation_html += '>' + (current_link) + ' de ' + number_of_pages +'</option>';
		//navigation_html += '<a class="page_link" href="javascript:go_to_page(' + current_link +')" longdesc="' + current_link +'">'+ (current_link + 1) +'</a>';
		current_link++;
	}
    navigation_html += '</select>';
	navigation_html += '<a class="next_link" href="javascript:next();">Sig&gt;&nbsp;&nbsp;</a>';
	navigation_html += '<a class="last_link" href="javascript:go_to_page(' + number_of_pages + ');">&gt;&gt;|</a>';
	
	$('#page_navigation').html(navigation_html);
	
	//add active_page class to the first page link
	//$('#page_navigation .page_link:first').addClass('active_page');
	
	//hide all the elements inside content div
	//$('#marcoCli.lineac').children().css('display', 'none');
	
	//and show the first n (show_per_page) elements
	//$('#marcoCli.lineac').children().slice(0, show_per_page).css('display', 'block');
	
}
function gopage(contr) {
	    //alert(contr.selectedIndex)
		go_to_page(contr.selectedIndex + 1);
}
function previous(){
	
	new_page = parseInt($('#current_page').val()) - 1;
	//if there is an item before the current active link run the function
	//if($('.active_page').prev('.page_link').length==true){
		go_to_page(new_page);
	//}
	
}

function next(){
	new_page = parseInt($('#current_page').val()) + 1;
	//if there is an item after the current active link run the function
	//if($('.active_page').next('.page_link').length==true){
		go_to_page(new_page);
	//}
	
}
function go_to_page(page_num){
    ShowListaClientes(page_num);

   // $('.page_link[longdesc=' + page_num +']').addClass('active_page').siblings('.active_page').removeClass('active_page');

	$('#current_page').val(page_num);
}
