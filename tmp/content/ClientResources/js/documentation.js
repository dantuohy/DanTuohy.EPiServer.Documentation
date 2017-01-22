$(document).ready(function () {
  $('.collapsible').collapsible();
  var $loader = $("main .loader");
  var $documentContainer = $(".doc-container");

  function toggleLoader() {
    if ($loader.hasClass("active")) {
      $loader.removeClass("active");
    } else {
      $loader.addClass("active");
    }
  }

  function initContentTypeLinks() {
    $(".content-type-links a").on("click",
      function (e) {
        e.preventDefault();
        getContentTypeDoc($(this).data("id"));
      });
  }

  function getContentTypeDoc(id) {
    toggleLoader();
    $.ajax({
      url: '/documentation/getcontenttype/' + id,
      type: "get",
      contentType: 'json',
      success: function (data) {
        var source = $("#content-type-template").html();
        var template = Handlebars.compile(source);
        $documentContainer.html(template(data));
        initContentTypeLinks();
        initExpandCollapse();
        $(".document .collapsible").collapsible();
        $("html, body").animate({ scrollTop: 0 }, "fast");
        toggleLoader();
      }
    });
  }

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

  $(".content-types .content-type")
    .on("click",
      function (e) {
        e.preventDefault();
        getContentTypeDoc($(this).data("id"));
      });
});

Handlebars.registerHelper('ifeq', function (a, b, opts) {
  if (a == b) // Or === depending on your needs
    return opts.fn(this);
  else
    return opts.inverse(this);
});