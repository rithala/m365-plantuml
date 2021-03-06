# Azure/M365-PlantUML

Fork of [Azure-PlantUML](https://github.com/plantuml-stdlib/Azure-PlantUML) allowing create PlantUML diagrams with [Azure](https://azure.microsoft.com/en-us/) and [Microsoft 365](https://www.office.com/) components.

## Getting Started

To be able to use M365 PlantUML it is necessary to use specific `!includes`.  
After that the Azure service macros are available and can be used.  
A list of all supported Azure and Microsoft 365 services can be found in the [Symbols Documentation](/Elements%20Table.md).

### Prerequisites

At the top of your  `.puml` file, you need to include the `Common.puml` file found in the `dist` folder of this repo.

To be independent of any internet connectivity, you can also download `Common.puml` and reference it locally with

```c#
!include path/to/Common.puml
```

If you want to use the always up-to-date version in this repo, use the following:

```c#
!includeurl https://raw.githubusercontent.com/rithala/m365-plantuml/master/dist/Common.puml
```

The next step is to include specific `.puml` files.  
For each Azure service a specific `.puml` file exists, which contains sprite and macros definitions.  
It is also possible to include Azure services category `.puml` files, which contain all Azure services from this category.

```c#
!define MUml path/to
!include MUml/Common.puml
!include MUml/FunctionApp.puml
```

Or the always up-to-date version in this repo:

```c#
!define MUml https://raw.githubusercontent.com/rithala/m365-plantuml/master/dist
!includeurl MUml/Common.puml
!includeurl MUml/FunctionApp.puml
```

## Samples

### [Hello World](/samples/Hello%20World.puml)

![Hello World](http://www.plantuml.com/plantuml/proxy?idx=0&src=https%3A%2F%2Fraw.githubusercontent.com%2Frithala%2Fm365-plantuml%2Fmaster%2Fsamples%2FHello%2520World.puml)

PlantUML
```c#
@startuml Hello World
!pragma revision 1

!define MUml https://raw.githubusercontent.com/rithala/m365-plantuml/master/dist
!includeurl MUml/Common.puml
!includeurl MUml/AzureCosmosDB.puml
!includeurl MUml/FunctionApp.puml

actor "Person" as personAlias

FunctionApp(functionAlias, "Label", "Technology", "Optional Description")
AzureCosmosDB(cosmosDbAlias, "Label", "Technology", "Optional Description")

personAlias --> functionAlias
functionAlias --> cosmosDbAlias

@enduml
```


### [M365 Simple Approval](/samples/M365%20Simple%20Approval.puml)

![M365 Simple Approval](http://www.plantuml.com/plantuml/proxy?idx=0&src=https%3A%2F%2Fraw.githubusercontent.com%2Frithala%2Fm365-plantuml%2Fmaster%2Fsamples%2FM365%2520Simple%2520Approval.puml)

PlantUML
```c#
@startuml Simple Approval
!pragma revision 1

!includeurl https://raw.githubusercontent.com/plantuml-stdlib/C4-PlantUML/master/C4_Container.puml

!define MUml https://raw.githubusercontent.com/rithala/m365-plantuml/master/dist

!includeurl MUml/Common.puml
!includeurl MUml/C4Integration.puml
!includeurl MUml/Common.puml
!includeurl MUml/PowerApp.puml
!includeurl MUml/PowerAutomate.puml
!includeurl MUml/List.puml

LAYOUT_LEFT_RIGHT

Person(user, "User")
Person(approver, "Approver")

PowerApp(powerApp, "User Requests Form", "PA Form Customization")
List(list, "User Requests List", "SharePoint List")
PowerAutomate(flow, "Approval Flow", "Power Automate M365")

Rel(user, powerApp, "Uses")
Rel(powerApp, list, "Stores data")
Rel(list, flow, "Triggers")
Rel_U(flow, approver, "Sends approval")

@enduml
```

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

## Acknowledgments
* [PlantUML](http://www.plantuml.com) - for making an easy way to create diagrams
* [Azure-PlantUML](https://github.com/plantuml-stdlib/Azure-PlantUML) - for creating a great framework making possible to create this fork

