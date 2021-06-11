# Azure/M365-PlantUML

Forked from [Azure-PlantUML](https://github.com/plantuml-stdlib/Azure-PlantUML)


[PlantUML](http://en.plantuml.com/) sprites, macros and stereotypes for creating PlantUML diagrams with [Azure](https://azure.microsoft.com/en-us/) components.

## Getting Started

To be able to use Azure-PlantUML it is necessary to use specific `!includes`.  
After that the Azure service macros are available and can be used.  
A list of all supported Azure services can be found in the [Azure-PlantUML Azure Symbols Documentation](AzureSymbols.md).

### Prerequisites

At the top of your Azure-PlantUML `.puml` file, you need to include the `AzureCommon.puml` file found in the `dist` folder of this repo.

To be independent of any internet connectivity, you can also download `AzureCommon.puml` and reference it locally with

```c#
!include path/to/AzureCommon.puml
```

If you want to use the always up-to-date version in this repo, use the following:

```c#
!includeurl https://raw.githubusercontent.com/rithala/m365-plantuml/master/dist/AzureCommon.puml
```

The next step is to include specific `.puml` files from Azure-PlantUML.  
For each Azure service a specific `.puml` file exists, which contains sprite and macros definitions.  
It is also possible to include Azure services category `.puml` files, which contain all Azure services from this category.

```c#
!define MS path/to
!include MS/AzureCommon.puml
!include MS/Databases/all.puml
!include MS/Compute/AzureFunction.puml
```

Or the always up-to-date version in this repo:

```c#
!define MS https://raw.githubusercontent.com/rithala/m365-plantuml/master/dist
!includeurl MS/AzureCommon.puml
!includeurl MS/Databases/all.puml
!includeurl MS/Compute/AzureFunction.puml
```


## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

## Acknowledgments

* [AWS-PlantUML](https://github.com/milo-minderbinder/AWS-PlantUML) - for the base structure
* [plantuml-office](https://github.com/Roemer/plantuml-office) - for the scripts idea
* [C4 Model](https://c4model.com/) - for the hope that it's possible to improve architecture documentations
* [Azure-PlantUML](https://github.com/plantuml-stdlib/Azure-PlantUML) - for creating a great framework making possible to create this fork

