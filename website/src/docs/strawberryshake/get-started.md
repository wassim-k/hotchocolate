# Get started with Strawberry Shake
In this tutorial we will walk you through the basics of adding a Strawberry Shake GraphQL client to a .net project. <br>  
In this tutorial, we will teach you:
 - How to add the Strawberry Shake CLI tools to your project and how to use them to download schema and change settings
 - How to generate source code from .graphql files, that contain operations
 - How to use the generated client in a classical or reactive way

## Step 1: Add the StrawberryShake CLI tools to your project
To install the StrawberryShake CLI tools, you need a dotnet tool manifest in your project.
If you already have one, you can skip this step, if not, please run the following command in a terminal
in your solutions root folder:
```bash
dotnet new tool-manifest
```
<br>
After that install the StrawberryShake CLI tools using the following command:

```bash
dotnet tool install StrawberryShake.Tools --version 11.1.0-preview.58 --local
```

## Step 2: Install the required packages
Currently, Strawberry Shake only supports graphql via http, so to get the project running you need:
```bash
dotnet add StrawberryShake.Transport.Http
```
This package includes everything that is needed 

```bash
dotnet add package StrawberryShake.CodeGeneration.CSharp.Analyzers
```


```bash
dotnet add StrawberryShake.Transport.WebSockets
```

## Step 3: Add a client to your project using the CLI tools
To add a client to your project, you need to run the `dotnet graphql init {{ServerUrl}} -n {{ClientName}}`. If you wanted to create a client for our [Workshop example](https://github.com/ChilliCream/graphql-workshop), whose api live api you can find here [https://hc-conference-app.azurewebsites.net/graphql/](https://hc-conference-app.azurewebsites.net/graphql/), the command would look like this:
```bash
dotnet graphql init https://hc-conference-app.azurewebsites.net/graphql/ -n ConferenceClient
```

This will create three files in the root of your solutions:
1. `.graphqlrc.json` This file will provide information where your schema and the `.graphql` files, that contain your operations, are. This will enable other tools to work with Strawberry Shake. In addition to that, the file contains the Strawberry Shake configuration. For more information about this, check the Strawberry Shake [configuration documentation](./configuration.md).
2. `schema.graphql` This file contains the schema of the API you want to consume, which was downloaded by the CLI tool and saved to that file.
3. `schema.extensions.grpahql` This file contains local extensions of your remote schema. Currently, this is only used to define what types of you schema are entities and what are just simple data.


Now  you can define your operations in `.graphql` files. Using the default settings, Strawberry Shake will scan the whole solution for any `.graphql` files.
In our case, we could simply create a file `getSessions.graphql` at root level and add the following query to it, to query all existing sessions:
```graphql
query {
  sessions {
   nodes {
     id
     startTime
     title
     trackId
     duration
     endTime
     abstract
   } 
  }
}
```

After this simply build your project. The client will be regenerated on every build of you project.
It will be regenerated even if you have compile errors in your main project, because the generator runs independently of you project. 


## Step 3: Set up your generated client and use it
St
