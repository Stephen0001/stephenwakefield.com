window.onload = (function ImageRotator() {
var myImage = document.getElementById("mainImage");

var imageArray = [
  "http://stephenwakefield.com/wp-content/uploads/2017/03/20161031_082228-Optimized-810x510-1-e1488864796463.jpg",
  "http://stephenwakefield.com/wp-content/uploads/2017/03/20160913_111124-Optimized-810x510-1-e1488864934500.jpg",
  "http://stephenwakefield.com/wp-content/uploads/2017/03/20160813_155207-Optimized-e1480056236508-810x510-1-e1488865010851.jpg",
  "http://stephenwakefield.com/wp-content/uploads/2017/03/20160723_214215-Optimized-810x510-1-e1488865088764.jpg"
];

var imageIndex = 0;

function changeImage() {
    myImage.setAttribute("src",imageArray[imageIndex]);
    imageIndex++;
    if (imageIndex >= imageArray.length) {
        imageIndex = 0;
    }
}

var intervalID = setInterval(changeImage,3000);

if (typeof myImage == "null" && myImage == "undefined") {
  clearInterval(intervalID);
}

});