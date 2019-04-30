$(function () {    

    $(".submitButton").on("click", function () {        
      newVal = 2
      document.getElementById('pdf_view').data = "PDF/" + newVal.toString() + ".pdf"   

    });

});