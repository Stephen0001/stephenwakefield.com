//script for Web Developer Blog
jQuery(".hideShowContent").slideToggle();

jQuery(".toggleContent").click( function(event) {
   jQuery(this).next().slideToggle();
});
