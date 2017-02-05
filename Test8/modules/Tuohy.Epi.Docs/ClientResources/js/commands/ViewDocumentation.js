

define([
    "dojo/_base/declare",
    "epi/shell/command/_Command",
    "epi-cms/_ContentContextMixin"
],
function (declare, _Command, _ContentContextMixin) {
  return declare([_Command, _ContentContextMixin], {
    name: "ViewDocumentation",
    label: "Test command",
    tooltip: "Click to execute me",
    canExecute: true,

    _execute: function () {
    //  alert(this.model.contentData.name);
      window.open("/modules/tuohy.epi.docs/documentation?name=" + this.getCurrentContext().dataType, "_blank");
    }
  });
});