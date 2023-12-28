# Taxer - PaySpace

## Description

A simple tax calculator in .NET capable of handling various tax types.

* Progressive tax
* Flat rate tax
* Flat value tax

The project is divided into two parts:

* A Web API backend
* A Javascript UI frontend

## Setup

Requires .NET 8 or higher.

```bash
dotnet restore
dotnet watch run --project Taxer.Web.API &
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
