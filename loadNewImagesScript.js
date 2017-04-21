//script for Brooklyn's Blog
var button = document.getElementById("loadNewImages");

button.onclick = (function (e) {
  e.preventDefault();
  jQuery("#startAjax_here").load("http://stephenwakefield.com/extrapics #gallery");
});