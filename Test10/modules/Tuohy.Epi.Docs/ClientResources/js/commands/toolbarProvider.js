define([
    "dojo/_base/declare",
    "dijit/form/Button",

    "epi-cms/component/command/_GlobalToolbarCommandProvider",
    "tuohy/commands/ViewDocumentation"
], function (declare, Button, _GlobalToolbarCommandProvider, ViewDocumentationCommand) {
  return declare([_GlobalToolbarCommandProvider], {
    constructor: function () {
      this.inherited(arguments);

      // The Global Toolbar has three areas that you can add commands to ["leading", "center", "trailing"]
      // the _GlobalToolbarCommandProvider extends the _CommandProviderMixin with helper methods for easy adding 
      // of commands to the different areas

      //Create dijit/Form/Button in the leading area and bind the TestCommand to it
      this.addToCenter(new ViewDocumentationCommand({
        label: "View Documentation"
      }), { showLabel: true, widget: Button });

    }
  });
});