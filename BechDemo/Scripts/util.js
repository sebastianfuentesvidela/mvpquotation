var articles=[
{title:"Lorem ipsum dolor sit amet, consectetur adipiscing elit",content:"test content 1",details:[{Category:"News"},{Category:"Hot Articles"},{Category:"Middle East"}]},
{title:"Vestibulum consectetur ipsum sit amet urna euismod imperdiet aliquam urna laoreet.",content:"test content 2"},
{title:"Curabitur a ipsum ut elit porttitor egestas non vitae libero"},
{title:"Pellentesque ac sem ac sem tincidunt euismod."},
{title:"Duis hendrerit purus vitae nibh tincidunt bibendum.",content:"test content 5"},
{title:"Nullam in nisi sit amet velit placerat laoreet."},
{title:"Vestibulum posuere ligula non dolor semper vel facilisis orci ultrices."},
{title:"Donec tincidunt lorem et dolor fringilla ut bibendum lacus fringilla."},
{title:"In non eros eu lacus vestibulum sodales."},
{title:"Duis ultrices metus sit amet sem adipiscing sit amet blandit orci convallis."},
{title:"Proin ullamcorper est vitae lorem mollis bibendum."},
{title:"Maecenas congue fringilla enim, tristique laoreet tortor adipiscing eget."},
{title:"Duis imperdiet metus et lorem venenatis nec porta libero porttitor."},
{title:"Maecenas lacinia lectus ac nulla commodo lacinia."},
{title:"Maecenas quis massa nisl, sed aliquet tortor."},
{title:"Quisque porttitor tellus ut ligula mattis luctus."},
{title:"In at mi dolor, at consectetur risus."},
{title:"Etiam id erat ut lorem fringilla dictum."},
{title:"Curabitur sagittis dolor ac nisi interdum sed posuere tellus commodo."},
{title:"Pellentesque quis magna vitae quam malesuada aliquet."},
{title:"Curabitur tempus tellus quis orci egestas condimentum.",details:[{Category:"Sport"},{Category:"North America"},{Category:"Soccer"}]},
{title:"Maecenas laoreet eros ac orci adipiscing pharetra."},
{title:"Nunc non mauris eu nibh tincidunt iaculis."},
{title:"Ut semper leo lacinia purus hendrerit facilisis."},
{title:"Praesent et eros lacinia massa sollicitudin consequat."},
{title:"Lorem ipsum dolor sit amet, consectetur adipiscing elit",content:"test content 55"},
{title:"Vestibulum consectetur ipsum sit amet urna euismod imperdiet aliquam urna laoreet.",content:"test content 66"},
{title:"Curabitur a ipsum ut elit porttitor egestas non vitae libero"},
{title:"Pellentesque ac sem ac sem tincidunt euismod."},
{title:"Duis hendrerit purus vitae nibh tincidunt bibendum.",content:"test content 43"},
{title:"Nullam in nisi sit amet velit placerat laoreet."},
{title:"Vestibulum posuere ligula non dolor semper vel facilisis orci ultrices."},
{title:"Donec tincidunt lorem et dolor fringilla ut bibendum lacus fringilla."},
{title:"Proin ullamcorper est vitae lorem mollis bibendum."},
{title:"Maecenas congue fringilla enim, tristique laoreet tortor adipiscing eget."},
{title:"Duis imperdiet metus et lorem venenatis nec porta libero porttitor."},
{title:"Maecenas lacinia lectus ac nulla commodo lacinia."},
{title:"Maecenas quis massa nisl, sed aliquet tortor."},
{title:"Quisque porttitor tellus ut ligula mattis luctus."},
{title:"In at mi dolor, at consectetur risus."}
];



function pagination(){
	
	//how much items per page to show
	var show_per_page = 5; 
	//getting the amount of elements inside content div
	var number_of_items = $('#content').children().size();
	//calculate the number of pages we are going to have
	var number_of_pages = Math.ceil(number_of_items/show_per_page);
	
	//set the value of our hidden input fields
	$('#current_page').val(0);
	$('#show_per_page').val(show_per_page);
	
	//now when we got all we need for the navigation let's make it '
	
	/* 
	what are we going to have in the navigation?
		- link to previous page
		- links to specific pages
		- link to next page
	*/
	var navigation_html = '<a class="previous_link" href="javascript:previous();">Prev</a>';
	var current_link = 0;
	while(number_of_pages > current_link){
		navigation_html += '<a class="page_link" href="javascript:go_to_page(' + current_link +')" longdesc="' + current_link +'">'+ (current_link + 1) +'</a>';
		current_link++;
	}
	navigation_html += '<a class="next_link" href="javascript:next();">Next</a>';
	
	$('#page_navigation').html(navigation_html);
	
	//add active_page class to the first page link
	$('#page_navigation .page_link:first').addClass('active_page');
	
	//hide all the elements inside content div
	$('#content').children().css('display', 'none');
	
	//and show the first n (show_per_page) elements
	$('#content').children().slice(0, show_per_page).css('display', 'block');
	
}

function previous(){
	
	new_page = parseInt($('#current_page').val()) - 1;
	//if there is an item before the current active link run the function
	if($('.active_page').prev('.page_link').length==true){
		go_to_page(new_page);
	}
	
}

function next(){
	new_page = parseInt($('#current_page').val()) + 1;
	//if there is an item after the current active link run the function
	if($('.active_page').next('.page_link').length==true){
		go_to_page(new_page);
	}
	
}
function go_to_page(page_num){
	//get the number of items shown per page
	var show_per_page = parseInt($('#show_per_page').val());
	
	//get the element number where to start the slice from
	start_from = page_num * show_per_page;
	
	//get the element number where to end the slice
	end_on = start_from + show_per_page;
	
	//hide all children elements of content div, get specific items and show them
	$('#content').children().css('display', 'none').slice(start_from, end_on).css('display', 'block');
	
	/*get the page link that has longdesc attribute of the current page and add active_page class to it
	and remove that class from previously active page link*/
	$('.page_link[longdesc=' + page_num +']').addClass('active_page').siblings('.active_page').removeClass('active_page');
	
	//update the current page input field
	$('#current_page').val(page_num);
}
  
function inject(fileurl,place,type){
  if (typeof place === "undefined" || place===null) place = "head";
  
  
$.get(fileurl+"?random="+randomString(5),function (data){


$(place).append(data);

});  
  
  }
  
function randomString(len, charSet) {
    charSet = charSet || 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
    var randomString = '';
    for (var i = 0; i < len; i++) {
    	var randomPoz = Math.floor(Math.random() * charSet.length);
    	randomString += charSet.substring(randomPoz,randomPoz+1);
    }
    return randomString;
}  
