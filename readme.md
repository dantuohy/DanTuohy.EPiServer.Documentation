# Content Editor Documentation For EPiServer
Enable content editors to easily create pages and blocks for EPiServer by giving them access to information about each of the content types available across the platform. 

Documentation is built up of names, descriptions and other information contained in EPi attributed (DisplayAttribute, RequiredAttribute...) provided by developers when they create content types


## Features

* Display property information for PageData, BlockData and MediaData
* Allows authorized users to add custom documentation for each content type with a WYSISYG
* Display information about Scheduled Jobs
* Adminstrator settings allows administrators to:
  * Change the logo and control some simple styling on the documentation to resemble their brand
  * Choose whether to display scheduled job information or not
  * Choose if custom documentation is allowed and who can create/edit this content
  * Choose whether to group content type by type (Page,Block..) or Group.

### Menu Item
Allows for easy access to documentation


### Command action
Allows for access to the documentation for the current content type

### Admin Settings



## Installation

```
Install-Package tuohy.epi.docs
```

The package can be found in the [EPiServer Nuget Feed.](http://nuget.episerver.com)

## Setup

* All Setup is managed in the administrator settings page
* To allow people who are not administrators to edit custom documentation
 * Create a role called **DocumentationEditors**
 * Add any users to the role


## Extend

Documentation is built up by reading the information in attributes decorated on properties. For example the description is sourced from the DisplayAttribute.

If you create your own custom attributes for IContent properties, implment the IDocumentable interface to enable documentation to show for the property. 

The below example is included in the package as an example

```c#
    /// <summary>
    /// Regular expression attribute that extends <see cref="IDocumentable"></see>/>
    /// </summary>
    public class DocumentableRegularExpressionAttribute : RegularExpressionAttribute, IDocumentable
    {
        public DocumentableRegularExpressionAttribute(string pattern, string name, string value) : base(pattern)
        {
            Name = name;
            Value = value;   
        }

        public string Name { get; set; }
        public string Value { get; set; }
    }
```
