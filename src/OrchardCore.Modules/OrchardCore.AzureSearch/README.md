# AzureSearch (OrchardCore.AzureSearch)

The AzureSearch module allows to manage AzureSearch indices.

## Recipe step

AzureSearch indices can be created during recipe execution using the `AzureSearch-index` step.
Here is a sample step:

```json
{
    "name": "AzureSearch-index",
    "Indices": "Indices": [ "Search" ]
}
```

### Queries recipe step

Here is an example for creating a AzureSearch query from a Queries recipe step:

```json
{
    "Source": "AzureSearch",
    "Name": "RecentBlogPosts",
    "Index": "Search",
    "Template": "...", // json encoded query template
    "ReturnContentItems": true
}
```

## Web APIs

### `api/AzureSearch/content`

Executes a query with the specified name and returns the corresponding content items.

Verbs: `POST` and `GET`

| Parameter | Example | Description |
| --------- | ---- |------------ |
| `indexName` | `search` | The name of the index to query. |
| `query` | `{ "query": { "match_all": {} } }` | A JSON object representing the query. |
| `parameters` | `{ size: 3}` | A JSON object representing the parameters of the query. |

### `api/AzureSearch/documents`

Executes a query with the specified name and returns the corresponding AzureSearch documents.
Only the stored fields are returned.

Verbs: `POST` and `GET`

| Parameter | Example | Description |
| --------- | ---- |------------ |
| `indexName` | `search` | The name of the index to query. |
| `query` | `{ "query": { "match_all": {} } }` | A JSON object representing the query. |
| `parameters` | `{ size: 3}` | A JSON object representing the parameters of the query. |

## AzureSearch Worker (`OrchardCore.AzureSearch.Worker`)

This feature creates a background task that will keep the local file system index synchronized with
other instances that could have their own local index. It is recommended to use it only if you are 
running the same tenant on multiple instances (farm) and are using a AzureSearch file system index.

If you are running on Azure App Services or if you are using Elasticsearch, then you don't need this 
feature.

## CREDITS

### AzureSearch.net

<http://AzureSearchnet.apache.org/index.html>
Copyright 2013 The Apache Software Foundation  
Licensed under the Apache License, Version 2.0.
