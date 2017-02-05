define([

    "dojo/_base/declare",
    "epi/_Module",
    "epi/dependency",

    "tuohy/commands/toolbarProvider"
], function 
   (

        declare,
        _Module,
        dependency,

        toolbarProvider
    ) {
  return declare([_Module], {
    initialize: function () {

      // var handle;

      var commandregistry = dependency.resolve("epi.globalcommandregistry");

      //The first parameter is the "area" that your provider should add commands to
      //For the global toolbar the area is "epi.cms.globalToolbar"
      commandregistry.registerProvider("epi.cms.globalToolbar", new toolbarProvider());
    }
  });
});