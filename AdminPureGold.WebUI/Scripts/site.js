$(document).ready(function() {
    var pgtooltips = $(".pg-tooltip");
    pgtooltips.tooltip({ placement: 'bottom' });   
});

function getContextPath() {    
    if (window.location.pathname == '/puregold') {
        return "https://admin.weichertonetest.com/puregold/";
    } else {
        return window.location.pathname;
    }        
}