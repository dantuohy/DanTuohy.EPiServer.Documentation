$(document).ready(function () {
  $('.collapsible').collapsible();

  initExpandCollapse();

  function initExpandCollapse() {
    var $collapsible = $(".document .collapsible");

    $(".document .expander")
      .on("click",
        function() {
          var $self = $(this);
          if (!$self.hasClass("expanded")) {
            $(".document .collapsible .collapsible-header").addClass("active");
            $collapsible.collapsible({ accordion: false });
            $self.addClass("expanded");
          } else {
            $self.removeClass("expanded");
            $(".document .collapsible .collapsible-header").removeClass(function () {
              return "active";
            });
            $collapsible.collapsible({ accordion: true });
            $collapsible.collapsible({ accordion: false });
          }
        });
  }
});