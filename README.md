# Taxer - PaySpace

![Taxer Demo](https://github.com/karagulamos/taxer/assets/16248072/5b603b44-95c2-4ad0-b60f-60ab2b878beb)

## Description

A simple tax calculator in .NET that implements the following tax types:

* Progressive tax
* Flat rate tax
* Flat value tax

The project is divided into two parts:

* A Web API Back-end
* An MVC Razor Front-end

## Setup

Requires .NET 8 or higher.

* Restore the packages

```bash
dotnet restore
```

* Run the Web API project (<http://localhost:5035/swagger>)

```bash
dotnet watch run --project Taxer.Web.API 
```

* Run the Web UI project (<http://localhost:5078>)

```bash
dotnet watch run --project Taxer.Web.UI 
```

## Summary

| Command                       | Description                     |
| :---------------------------- | :------------------------------ |
| `dotnet build`                | To build the solution           |
| `dotnet watch run`            | To run the solution in dev      |

## Packages

| Name                  | Description                       |
| :-------------------- | :-------------------------------- |
| `EF Core`             | Persistence and data retrieval    |
| `NUnit`               | For unit testing                  |
| `Moq`                 | For mocking in tests              |
