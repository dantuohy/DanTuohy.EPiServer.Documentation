(function () {
  // using this pattern helps keeping your privates private
  inUse = {};
  inUse.init = function () {
    var $container = $(".in-use");

    $(".content-type a", $container).click(
      function () {
        var url = "inuse/GetLinks/" + $(this).attr("data-id");
        
        getLinks($(this).attr("data-id"));

      });

    $(".paging a").click(function() {
      var pageNumber = $("#pageNumber").val();
      var id = $("#id").val();
      if ($(this).hasClass("previous")) {

        pageNumber--;
      } else {
        pageNumber++;
      }
      getLinks(id, pageNumber);
    });

    $("#back").click(function() {
      $.ajax({
        type: "get",
        url: "inuse/index/",
        success: function (view) {
          $container.html(view);
        }
      });
    });

    function getLinks(id, pageNumber) {

      var url = "inuse/GetLinks/" + id;

      if (pageNumber != null) {
        url = url + "?page=" + pageNumber;
      }

      $.ajax({
        type: "get",
        url: url,
        success: function (view) {
          $container.html(view);
        }
      });
    }
  };


})();