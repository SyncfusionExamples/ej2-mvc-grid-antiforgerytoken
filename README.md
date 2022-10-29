# Performing CRUD operation with AntiForgeryToken

This sample explains about how to perform the CRUD operation with AntiForgeryToken in ASP.NET MVC DataGrid.

Anti-forgery token is used between the client and server to prevent cross-site request forgery (CSRF) attack. For more information on preventing CSRF attack, please refer to the
[link]( https://docs.microsoft.com/en-us/aspnet/core/security/anti-request-forgery?view=aspnetcore-2.1#authentication-fundamentals).

The below custom adaptor is used to send anti-forgery token while performing CRUD operation.

```javascript

window.customAdaptor = new ej.data.UrlAdaptor();

    customAdaptor = ej.base.extend(customAdaptor, {

        processResponse: function (data, ds, query, xhr, request, changes) {
            request.data = JSON.stringify(data);
            return ej.data.UrlAdaptor.prototype.processResponse.call(this,data, ds, query, xhr, request, changes)
        },
        insert: function (dm, data, tableName) {
            return { 
                url: dm.dataSource.insertUrl || dm.dataSource.crudUrl || dm.dataSource.url,
                data: $.param({
                    __RequestVerificationToken: document.getElementsByName("__RequestVerificationToken")[0].value,
                    value: data,
                    table: tableName,
                    action: 'insert'
                }),
                contentType: 'application/x-www-form-urlencoded; charset=UTF-8'
            }
        },
        update: function (dm, keyField, value, tableName) {
            return {
                url: dm.dataSource.updateUrl || dm.dataSource.crudUrl || dm.dataSource.url,
                data: $.param({
                    __RequestVerificationToken: document.getElementsByName("__RequestVerificationToken")[0].value,
                    value: value,
                    table: tableName,
                    action: 'insert'
                }),
                contentType: 'application/x-www-form-urlencoded; charset=UTF-8'
            };
        }
    });

```

## Prerequisites

* Visual Studio 2022

## How to run the project

* Checkout this project to a location in your disk.
* Open the solution file using the Visual Studio 2022.
* Restore the NuGet packages by rebuilding the solution.
* Run the project.
