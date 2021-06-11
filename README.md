# Azure/M365-PlantUML

Fork of [Azure-PlantUML](https://github.com/plantuml-stdlib/Azure-PlantUML) allowing create PlantUML diagrams with [Azure](https://azure.microsoft.com/en-us/) and [Microsoft 365](https://www.office.com/) components.

## Getting Started

To be able to use M365-PlantUML it is necessary to use specific `!includes`.  
After that the Azure service macros are available and can be used.  
A list of all supported Azure and Microsoft 365 services can be found in the [Symbols Documentation](Elements Table.md).

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

The next step is to include specific `.puml` files from Azure-PlantUML.  
For each Azure service a specific `.puml` file exists, which contains sprite and macros definitions.  
It is also possible to include Azure services category `.puml` files, which contain all Azure services from this category.

```c#
!define MS path/to
!include MS/Common.puml
!include MS/FunctionApp.puml
```

Or the always up-to-date version in this repo:

```c#
!define MS https://raw.githubusercontent.com/rithala/m365-plantuml/master/dist
!includeurl MS/Common.puml
!includeurl MS/FunctionApp.puml
```

## Samples


### [M365 Simple Approval](/samples/M365%20Simple%20Approval.puml)

![M365 Simple Approval](http://www.plantuml.com/plantuml/proxy?idx=0&src=https%3A%2F%2Fraw.githubusercontent.com%2Frithala%2Fm365-plantuml%2Fmaster%2Fsamples%2FM365%2520Simple%2520Approval.puml)

PlantUML
```c#
@startuml Simple Approval
!pragma revision 1

!includeurl https://raw.githubusercontent.com/plantuml-stdlib/C4-PlantUML/master/C4_Container.puml

!define MS https://raw.githubusercontent.com/rithala/m365-plantuml/master/dist

!includeurl MS/Common.puml
!includeurl MS/C4Integration.puml
!includeurl MS/Common.puml
!includeurl MS/PowerApp.puml
!includeurl MS/PowerAutomate.puml
!includeurl MS/List.puml

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

* [Azure-PlantUML](https://github.com/plantuml-stdlib/Azure-PlantUML) - for creating a great framework making possible to create this fork

